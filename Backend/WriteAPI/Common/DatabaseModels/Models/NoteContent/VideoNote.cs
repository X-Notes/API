﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Common.DatabaseModels.Models.Files;

namespace Common.DatabaseModels.Models.NoteContent
{
    [Table("VideoNote")]
    public class VideoNote : BaseNoteContent
    {
        public string Name { set; get; }

        public Guid AppFileId { get; set; }

        public AppFile AppFile { get; set; }

        public VideoNote()
        {
            this.UpdatedAt = DateTimeOffset.Now;
            this.ContentTypeId = ContentTypeENUM.Video;
        }

        public VideoNote(VideoNote entity, Guid appFileId, bool isHistory, Guid entityId)
        {
            this.SetId(isHistory, entityId);

            Order = entity.Order;
            Name = entity.Name;

            this.UpdatedAt = DateTimeOffset.Now;
            this.ContentTypeId = ContentTypeENUM.Video;

            AppFileId = appFileId;
        }

        public VideoNote(VideoNote entity, AppFile file, bool isHistory, Guid entityId)
        {
            this.SetId(isHistory, entityId);

            Order = entity.Order;
            Name = entity.Name;

            this.UpdatedAt = DateTimeOffset.Now;
            this.ContentTypeId = ContentTypeENUM.Video;

            AppFile = file;
        }
    }
}
