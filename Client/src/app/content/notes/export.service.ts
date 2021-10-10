import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as JSZip from 'jszip';
import { forkJoin, Observable } from 'rxjs';
import { finalize, map, takeUntil } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { saveAs } from 'file-saver';
import * as moment from 'moment';
import {
  FileProcessTracker,
  SnackBarFileProcessHandlerService,
} from 'src/app/shared/services/snackbar/snack-bar-file-process-handler.service';
import {
  PhotosCollection,
  AudioModel,
  DocumentsCollection,
  Photo,
  AudiosCollection,
  VideosCollection,
  DocumentModel,
  VideoModel,
} from './models/content-model.model';
import { LongTermOperationsHandlerService } from '../long-term-operations-handler/services/long-term-operations-handler.service';
import { LongTermsIcons } from '../long-term-operations-handler/models/long-terms.icons';
import {
  LongTermOperation,
  OperationDetailMini,
} from '../long-term-operations-handler/models/long-term-operation';

@Injectable({
  providedIn: 'root',
})
export class ExportService {
  constructor(
    private httpClient: HttpClient,
    protected longTermOperationsHandler: LongTermOperationsHandlerService,
    protected snackBarFileProcessingHandler: SnackBarFileProcessHandlerService,
  ) {}

  zipFiles = async (tasks: Observable<{ blob: FileProcessTracker<Blob>; name: string }>[]) => {
    const resp = await forkJoin(tasks).toPromise();
    const zip = new JSZip();
    resp.forEach((x) => zip.file(x.name, x.blob.eventBody));
    const zipFile = await zip.generateAsync({ type: 'blob' });
    saveAs(zipFile, `noots-export ${moment().format('MM-DD, h-mm-ss a')}`);
  };

  getPath = (url: string, authorNoteId: string) =>
    `${environment.storage}/${authorNoteId}/${escape(url)}`;

  getBlobFile(url: string, mini: OperationDetailMini, operation: LongTermOperation) {
    return this.httpClient
      .get(url, {
        responseType: 'blob',
        reportProgress: true,
        observe: 'events',
      })
      .pipe(
        finalize(() => this.longTermOperationsHandler.finalize(operation, mini)),
        takeUntil(mini.obs),
        (x) => this.snackBarFileProcessingHandler.trackProcess(x, mini),
      );
  }

  // PHOTOS
  async exportAlbum(album: PhotosCollection) {
    const operation = this.longTermOperationsHandler.addNewExportOperation('uploader.exportPhotos');
    const tasks = album.photos.map((photo) => {
      const path = this.getPath(photo.photoFromBig, photo.authorId);
      const mini = this.longTermOperationsHandler.getNewMini(
        operation,
        LongTermsIcons.Image,
        photo.name,
        false,
      );
      return this.getBlobFile(path, mini, operation).pipe(
        map((blob) => {
          return {
            blob,
            name: photo.name,
          };
        }),
      );
    });

    await this.zipFiles(tasks);
  }

  async exportPhoto(photo: Photo) {
    const operation = this.longTermOperationsHandler.addNewExportOperation('uploader.exportPhotos');
    const mini = this.longTermOperationsHandler.getNewMini(
      operation,
      LongTermsIcons.Image,
      photo.name,
      false,
    );
    const path = this.getPath(photo.photoFromBig, photo.authorId);
    const blob = await this.getBlobFile(path, mini, operation).toPromise();
    saveAs(blob.eventBody, photo.name);
  }

  // AUDIOS
  async exportPlaylist(playlist: AudiosCollection) {
    const operation = this.longTermOperationsHandler.addNewExportOperation('uploader.exportAudios');
    const tasks = playlist.audios.map((audio) => {
      const mini = this.longTermOperationsHandler.getNewMini(
        operation,
        LongTermsIcons.Audio,
        audio.name,
        false,
      );
      const path = this.getPath(audio.audioPath, audio.authorId);
      return this.getBlobFile(path, mini, operation).pipe(
        map((blob) => {
          return {
            blob,
            name: audio.name,
          };
        }),
      );
    });

    await this.zipFiles(tasks);
  }

  async exportAudio(audio: AudioModel) {
    const operation = this.longTermOperationsHandler.addNewExportOperation('uploader.exportAudios');
    const mini = this.longTermOperationsHandler.getNewMini(
      operation,
      LongTermsIcons.Audio,
      audio.name,
      false,
    );
    const path = this.getPath(audio.audioPath, audio.authorId);
    const blob = await this.getBlobFile(path, mini, operation).toPromise();
    saveAs(blob.eventBody, audio.name);
  }

  // DOCUMENT
  async exportDocuments(collection: DocumentsCollection) {
    const operation = this.longTermOperationsHandler.addNewExportOperation(
      'uploader.exportDocuments',
    );
    const tasks = collection.documents.map((document) => {
      const mini = this.longTermOperationsHandler.getNewMini(
        operation,
        LongTermsIcons.Document,
        document.name,
        false,
      );
      const path = this.getPath(document.documentPath, document.authorId);
      return this.getBlobFile(path, mini, operation).pipe(
        map((blob) => {
          return {
            blob,
            name: document.name,
          };
        }),
      );
    });

    await this.zipFiles(tasks);
  }

  async exportDocument(document: DocumentModel) {
    const operation = this.longTermOperationsHandler.addNewExportOperation(
      'uploader.exportDocuments',
    );
    const mini = this.longTermOperationsHandler.getNewMini(
      operation,
      LongTermsIcons.Document,
      document.name,
      false,
    );
    const path = this.getPath(document.documentPath, document.authorId);
    const blob = await this.getBlobFile(path, mini, operation).toPromise();
    saveAs(blob.eventBody, document.name);
  }

  // VIDEOS
  async exportVideos(collection: VideosCollection) {
    const operation = this.longTermOperationsHandler.addNewExportOperation('uploader.exportVideos');
    const tasks = collection.videos.map((video) => {
      const mini = this.longTermOperationsHandler.getNewMini(
        operation,
        LongTermsIcons.Video,
        video.name,
        false,
      );
      const path = this.getPath(video.videoPath, video.authorId);
      return this.getBlobFile(path, mini, operation).pipe(
        map((blob) => {
          return {
            blob,
            name: video.name,
          };
        }),
      );
    });

    await this.zipFiles(tasks);
  }

  async exportVideo(video: VideoModel) {
    const operation = this.longTermOperationsHandler.addNewExportOperation('uploader.exportVideos');
    const mini = this.longTermOperationsHandler.getNewMini(
      operation,
      LongTermsIcons.Video,
      video.name,
      false,
    );
    const path = this.getPath(video.videoPath, video.authorId);
    const blob = await this.getBlobFile(path, mini, operation).toPromise();
    saveAs(blob.eventBody, video.name);
  }
}
