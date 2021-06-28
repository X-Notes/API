﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.DTO.Folders;
using MediatR;

namespace Domain.Commands.Folders
{
    public class CopyFolderCommand : BaseCommandEntity, IRequest<List<SmallFolder>>
    {
        [Required]
        public List<Guid> Ids { set; get; }

        public CopyFolderCommand(string email): base(email)
        {

        }
    }
}
