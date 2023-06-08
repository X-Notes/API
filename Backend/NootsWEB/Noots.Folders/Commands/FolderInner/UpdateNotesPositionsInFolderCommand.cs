﻿using Common.Attributes;
using Common.CQRS;
using Common.DTO;
using Common.DTO.Notes;
using MediatR;

namespace Noots.Folders.Commands.FolderInner;

public class UpdateNotesPositionsInFolderCommand : BaseCommandEntity, IRequest<OperationResult<Unit>>
{
    [ValidationGuid]
    public Guid FolderId { set; get; }

    [RequiredListNotEmpty]
    public List<EntityPositionDTO> Positions { set; get; }
}

