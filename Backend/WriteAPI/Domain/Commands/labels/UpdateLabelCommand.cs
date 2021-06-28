﻿using System;
using System.ComponentModel.DataAnnotations;
using Common.Attributes;
using MediatR;

namespace Domain.Commands.Labels
{
    public class UpdateLabelCommand : BaseCommandEntity, IRequest<Unit>
    {
        [ValidationGuid]
        public Guid Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string Color { set; get; }
        public UpdateLabelCommand(string email)
            :base(email)
        {

        }
    }
}
