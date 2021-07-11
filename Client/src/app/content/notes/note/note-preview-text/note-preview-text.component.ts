import { Component, Input, OnInit } from '@angular/core';
import { BaseText } from '../../models/content-model.model';

@Component({
  selector: 'app-note-preview-text',
  templateUrl: './note-preview-text.component.html',
  styleUrls: ['./note-preview-text.component.scss'],
})
export class NotePreviewTextComponent implements OnInit {
  @Input()
  contentBaseText: BaseText;

  // eslint-disable-next-line class-methods-use-this
  ngOnInit(): void {}
}
