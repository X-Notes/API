﻿using Common.Attributes;
using Common.CQRS;
using Common.DTO;
using MediatR;
using Noots.Editor.Services.Sync.Entities;

namespace Noots.Editor.Commands.Sync;

public class SyncEditorStateCommand : BaseCommandEntity, IRequest<OperationResult<SyncStateResult>>
{
    [ValidationGuid]
    public Guid NoteId { set; get; }

    public Guid? FolderId { set; get; }

    public List<SyncItem> SyncState { set; get; }
}
