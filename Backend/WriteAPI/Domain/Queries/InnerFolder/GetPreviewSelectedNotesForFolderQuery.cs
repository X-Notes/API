﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Attributes;
using Common.DTO.Notes;
using Common.DTO.Personalization;
using MediatR;

namespace Domain.Queries.InnerFolder
{
    public class GetPreviewSelectedNotesForFolderQuery : BaseQueryEntity, IRequest<List<PreviewNoteForSelection>>
    {
        [ValidationGuid]
        public Guid FolderId { set; get; }

        public string Search { set; get; }

        [Required]
        public PersonalizationSettingDTO Settings { set; get; }
    }
}
