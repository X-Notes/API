import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NoteHistory } from './models/history/NoteHistory';

@Injectable()
export class ApiNoteHistoryService {
  constructor(private httpClient: HttpClient) {}

  getHistory(noteId: string) {
    return this.httpClient.get<NoteHistory[]>(`${environment.writeAPI}/api/history/${noteId}`);
  }
}
