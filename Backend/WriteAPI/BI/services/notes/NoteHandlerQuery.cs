﻿using AutoMapper;
using Common.DTO.labels;
using Common.DTO.notes;
using Common.DTO.users;
using Domain.Queries.notes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WriteContext.Repositories;

namespace BI.services.notes
{
    public class NoteHandlerQuery :
        IRequestHandler<GetPrivateNotesQuery, List<SmallNote>>,
        IRequestHandler<GetSharedNotesQuery, List<SmallNote>>,
        IRequestHandler<GetArchiveNotesQuery, List<SmallNote>>,
        IRequestHandler<GetDeletedNotesQuery, List<SmallNote>>,

        IRequestHandler<GetFullNoteQuery, FullNote>,
        IRequestHandler<GetOnlineUsersOnNote, List<OnlineUserOnNote>>
    {
        private readonly IMapper mapper;
        private readonly NoteRepository noteRepository;
        private readonly UserRepository userRepository;
        private readonly UserOnNoteRepository userOnNoteRepository;
        public NoteHandlerQuery(IMapper mapper, NoteRepository noteRepository, UserRepository userRepository, UserOnNoteRepository userOnNoteRepository)
        {
            this.mapper = mapper;
            this.noteRepository = noteRepository;
            this.userRepository = userRepository;
            this.userOnNoteRepository = userOnNoteRepository;
        }
        public async Task<List<SmallNote>> Handle(GetPrivateNotesQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmail(request.Email);
            if (user != null)
            {
                var notes = await noteRepository.GetPrivateNotesByUserId(user.Id);
                notes.ForEach(x => x.LabelsNotes = x.LabelsNotes.Where(x => x.Label.IsDeleted == false).ToList());
                notes = notes.OrderBy(x => x.Order).ToList();
                return mapper.Map<List<SmallNote>>(notes);
            }
            return new List<SmallNote>();
        }

        public async Task<FullNote> Handle(GetFullNoteQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmail(request.Email);
            if (user != null && Guid.TryParse(request.Id, out var guid))
            {
                var note = await noteRepository.GetFull(guid);
                note.LabelsNotes = note.LabelsNotes.Where(x => x.Label.IsDeleted == false).ToList();
                return mapper.Map<FullNote>(note);
            }
            return null;
        }

        public async Task<List<OnlineUserOnNote>> Handle(GetOnlineUsersOnNote request, CancellationToken cancellationToken)
        {
            if (Guid.TryParse(request.Id, out var guid))
            {
                var users = await userOnNoteRepository.GetUsersOnlineUserOnNote(guid);
                return mapper.Map<List<OnlineUserOnNote>>(users);
            }
            return null;
        }

        public async Task<List<SmallNote>> Handle(GetSharedNotesQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmail(request.Email);
            if (user != null)
            {
                var notes = await noteRepository.GetSharedNotesByUserId(user.Id);
                notes.ForEach(x => x.LabelsNotes = x.LabelsNotes.Where(x => x.Label.IsDeleted == false).ToList());
                notes = notes.OrderBy(x => x.Order).ToList();
                return mapper.Map<List<SmallNote>>(notes);
            }
            return new List<SmallNote>();
        }

        public async Task<List<SmallNote>> Handle(GetArchiveNotesQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmail(request.Email);
            if (user != null)
            {
                var notes = await noteRepository.GetArchiveNotesByUserId(user.Id);
                notes.ForEach(x => x.LabelsNotes = x.LabelsNotes.Where(x => x.Label.IsDeleted == false).ToList());
                notes = notes.OrderBy(x => x.Order).ToList();
                return mapper.Map<List<SmallNote>>(notes);
            }
            return new List<SmallNote>();
        }

        public async Task<List<SmallNote>> Handle(GetDeletedNotesQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmail(request.Email);
            if (user != null)
            {
                var notes = await noteRepository.GetDeletedNotesByUserId(user.Id);
                notes.ForEach(x => x.LabelsNotes = x.LabelsNotes.Where(x => x.Label.IsDeleted == false).ToList());
                notes = notes.OrderBy(x => x.Order).ToList();
                return mapper.Map<List<SmallNote>>(notes);
            }
            return new List<SmallNote>();
        }
    }
}
