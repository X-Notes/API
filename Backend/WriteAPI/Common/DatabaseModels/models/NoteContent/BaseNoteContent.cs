﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DatabaseModels.models.NoteContent
{
    public class BaseNoteContent : BaseEntity
    {
        [Required]
        public int Order { set; get; }
        public Guid NoteId { set; get; }
        public Note Note { set; get; }
    }
}
