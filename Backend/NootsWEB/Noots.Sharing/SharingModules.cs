﻿using Common.DTO;
using Common.DTO.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Noots.Sharing.Commands.Folders;
using Noots.Sharing.Commands.Notes;
using Noots.Sharing.Handlers.Commands;
using Noots.Sharing.Handlers.Queries;
using Noots.Sharing.Queries;

namespace Noots.Sharing
{
    public static class SharingModules
    {
        public static void ApplySharingDI(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetUsersOnPrivateNoteQuery, List<InvitedUsersToFoldersOrNote>>, GetUsersOnPrivateNoteQueryHandler>();
            services.AddScoped<IRequestHandler<GetUsersOnPrivateFolderQuery, List<InvitedUsersToFoldersOrNote>>, GetUsersOnPrivateFolderQueryHandler>();

            services.AddScoped<IRequestHandler<ChangeRefTypeFolders, OperationResult<Unit>>, ChangeRefTypeFoldersHandler>();
            services.AddScoped<IRequestHandler<ChangeRefTypeNotes, OperationResult<Unit>>, ChangeRefTypeNotesHandler>();

            services.AddScoped<IRequestHandler<PermissionUserOnPrivateNotes, OperationResult<Unit>>, PermissionUserOnPrivateNotesHandler>();
            services.AddScoped<IRequestHandler<RemoveUserFromPrivateNotes, OperationResult<Unit>>, RemoveUserFromPrivateNotesHandler>();
            services.AddScoped<IRequestHandler<SendInvitesToUsersNotes, OperationResult<Unit>>, SendInvitesToUsersNotesHandler>();

            services.AddScoped<IRequestHandler<PermissionUserOnPrivateFolders, OperationResult<Unit>>, PermissionUserOnPrivateFoldersHandler>();
            services.AddScoped<IRequestHandler<RemoveUserFromPrivateFolders, OperationResult<Unit>>, RemoveUserFromPrivateFoldersHandler>();
            services.AddScoped<IRequestHandler<SendInvitesToUsersFolders, Unit>, SendInvitesToUsersFoldersHandler>();

            services.AddScoped<IRequestHandler<RemoveAllUsersFromNoteCommand, OperationResult<Unit>>, RemoveAllUsersFromNoteCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveAllUsersFromFolderCommand, OperationResult<Unit>>, RemoveAllUsersFromFolderCommandHandler>();
        }
    }
}