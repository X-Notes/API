﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.DatabaseModels.Models.NoteContent.TextContent
{
    [Table(nameof(TextNote), Schema = SchemeConfig.NoteContent)]
    public class TextNote : BaseNoteContent
    {
        public string Content { set; get; }

        public NoteTextTypeENUM NoteTextTypeId { set; get; }
        public NoteTextType NoteTextType { set; get; }

        public HTypeENUM? HTypeId { set; get; }
        public HType HType { set; get; }

        public bool? Checked { set; get; }

        public bool IsBold { set; get; }

        public bool IsItalic { set; get; }

        public TextNote()
        {
            ContentTypeId = ContentTypeENUM.Text;
        }


        public TextNote(TextNote text, Guid noteId)
        {
            NoteId = noteId;

            Order = text.Order;

            Content = text.Content;
            NoteTextTypeId = text.NoteTextTypeId;
            HTypeId = text.HTypeId;
            Checked = text.Checked;

            ContentTypeId = ContentTypeENUM.Text;
        }

        public TextNote(Guid noteId, NoteTextTypeENUM NoteTextTypeId, int Order, string Content = null)
        {
            NoteId = noteId;

            this.NoteTextTypeId = NoteTextTypeId;
            this.Content = Content;
            this.Order = Order;

            ContentTypeId = ContentTypeENUM.Text;
        }

        public TextNote(NoteTextTypeENUM NoteTextTypeId, string Content = null)
        {
            this.NoteTextTypeId = NoteTextTypeId;
            this.Content = Content;

            ContentTypeId = ContentTypeENUM.Text;
        }
    }
}
