﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BI.Helpers;
using BI.Services.History;
using BI.SignalR;
using Common.DatabaseModels.Models.Files;
using Common.DatabaseModels.Models.NoteContent;
using Common.DTO.Notes.FullNoteContent;
using Domain.Commands.Files;
using Domain.Commands.NoteInner.FileContent.Documents;
using Domain.Queries.Permissions;
using MediatR;
using WriteContext.Repositories;
using WriteContext.Repositories.NoteContent;

namespace BI.Services.Notes
{
    public class FullNoteDocumentHandlerCommand :
        IRequestHandler<InsertDocumentsToNoteCommand, OperationResult<DocumentNoteDTO>>,
        IRequestHandler<RemoveDocumentCommand, OperationResult<Unit>>
    {

        private readonly IMediator _mediator;

        private readonly DocumentNoteRepository documentNoteRepository;

        private readonly BaseNoteContentRepository baseNoteContentRepository;

        private readonly FileRepository fileRepository;

        private readonly HistoryCacheService historyCacheService;

        private readonly AppSignalRService appSignalRService;

        public FullNoteDocumentHandlerCommand(
                                        IMediator _mediator,
                                        BaseNoteContentRepository baseNoteContentRepository,
                                        FileRepository fileRepository,
                                        DocumentNoteRepository documentNoteRepository,
                                        HistoryCacheService historyCacheService,
                                        AppSignalRService appSignalRService)
        {
            this._mediator = _mediator;
            this.baseNoteContentRepository = baseNoteContentRepository;
            this.fileRepository = fileRepository;
            this.documentNoteRepository = documentNoteRepository;
            this.historyCacheService = historyCacheService;
            this.appSignalRService = appSignalRService;
        }


        public async Task<OperationResult<DocumentNoteDTO>> Handle(InsertDocumentsToNoteCommand request, CancellationToken cancellationToken)
        {
            var command = new GetUserPermissionsForNote(request.NoteId, request.Email);
            var permissions = await _mediator.Send(command);
            var note = permissions.Note;

            if (permissions.CanWrite)
            {
                var contents = await baseNoteContentRepository.GetWhere(x => x.NoteId == note.Id);

                var contentForRemove = contents.First(x => x.Id == request.ContentId);

                // FILES LOGIC
                var filebyte = await request.File.GetFilesBytesAsync();
                var file = await _mediator.Send(new SaveDocumentsToNoteCommand(permissions.Author.Id, filebyte, note.Id));

                using var transaction = await baseNoteContentRepository.context.Database.BeginTransactionAsync();

                try
                {
                    await baseNoteContentRepository.Remove(contentForRemove);

                    await fileRepository.Add(file);

                    var documentNote = new DocumentNote()
                    {
                        AppFileId = file.Id,
                        Name = request.File.FileName,
                        Note = note,
                        Order = contentForRemove.Order,
                    };

                    await documentNoteRepository.Add(documentNote);

                    await transaction.CommitAsync();

                    var result = new DocumentNoteDTO(documentNote.Name, file.PathNonPhotoContent, documentNote.AppFileId, documentNote.Id, documentNote.UpdatedAt);

                    historyCacheService.UpdateNote(permissions.Note.Id, permissions.User.Id, permissions.Author.Email);

                    await appSignalRService.UpdateContent(request.NoteId, permissions.User.Email);

                    return new OperationResult<DocumentNoteDTO>(Success: true, result);
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine(e);
                    await _mediator.Send(new RemoveFilesCommand(permissions.User.Id.ToString(), file));
                }
            }

            // TODO MAKE LOGIC FOR HANDLE UNATHORIZE UPDATING
            return new OperationResult<DocumentNoteDTO>(Success: false, null);
        }

        public async Task<OperationResult<Unit>> Handle(RemoveDocumentCommand request, CancellationToken cancellationToken)
        {
            var command = new GetUserPermissionsForNote(request.NoteId, request.Email);
            var permissions = await _mediator.Send(command);
            var note = permissions.Note;

            if (permissions.CanWrite)
            {
                var contents = await baseNoteContentRepository.GetAllContentByNoteIdOrdered(note.Id);
                var contentForRemove = contents.FirstOrDefault(x => x.Id == request.ContentId) as DocumentNote;
                contents.Remove(contentForRemove);

                var orders = Enumerable.Range(1, contents.Count);
                contents = contents.Zip(orders, (content, order) =>
                {
                    content.Order = order;
                    content.UpdatedAt = DateTimeOffset.Now;
                    return content;
                }).ToList();

                using var transaction = await baseNoteContentRepository.context.Database.BeginTransactionAsync();

                try
                {
                    await baseNoteContentRepository.Remove(contentForRemove);
                    await baseNoteContentRepository.UpdateRange(contents);

                    await transaction.CommitAsync();

                    await _mediator.Send(new RemoveFilesCommand(permissions.User.Id.ToString(), contentForRemove.AppFile));

                    historyCacheService.UpdateNote(permissions.Note.Id, permissions.User.Id, permissions.Author.Email);

                    await appSignalRService.UpdateContent(request.NoteId, permissions.User.Email);

                    return new OperationResult<Unit>(Success: true, Unit.Value);
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine(e);
                }
            }

            return new OperationResult<Unit>(Success: false, Unit.Value);
        }
    }
}
