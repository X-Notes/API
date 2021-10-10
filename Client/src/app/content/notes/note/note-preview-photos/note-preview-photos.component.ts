import { Component, Input, OnInit } from '@angular/core';
import { PhotosCollection } from '../../models/content-model.model';

@Component({
  selector: 'app-note-preview-photos',
  templateUrl: './note-preview-photos.component.html',
  styleUrls: ['./note-preview-photos.component.scss'],
})
export class NotePreviewPhotosComponent {
  @Input()
  album: PhotosCollection;
}
