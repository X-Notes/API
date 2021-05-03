import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Store } from '@ngxs/store';
import { environment } from 'src/environments/environment';
import { LoadOnlineUsersOnNote } from '../content/notes/state/notes-actions';
import { AppNotification } from './models/app-notification';
import { NewNotification } from './stateApp/app-action';
import { AppStore } from './stateApp/app-state';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  public hubConnection: signalR.HubConnection;

  constructor(private store: Store) {}

  init() {
    this.startConnection();
  }

  async joinNote(noteId: string) {
    try {
      await this.hubConnection.invoke('JoinNote', noteId);
    } catch (err) {
      console.error(err);
    }
  }

  async leaveNote(noteId: string) {
    try {
      await this.hubConnection.invoke('LeaveNote', noteId);
    } catch (err) {
      console.error(err);
    }
  }

  private startConnection = () => {
    const token = this.store.selectSnapshot(AppStore.getToken);
    this.hubConnection = new signalR.HubConnectionBuilder()
      // .configureLogging(signalR.LogLevel.None)
      .withUrl(`${environment.writeAPI}/hub`, { accessTokenFactory: () => token })
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((err) => console.log(`Error while starting connection: ${err}`));

    this.hubConnection.on('newNotification', (notifcationDTO: AppNotification) =>
      this.store.dispatch(new NewNotification(notifcationDTO)),
    );

    this.hubConnection.on('updateOnlineUsers', (noteId: string) => {
      this.store.dispatch(new LoadOnlineUsersOnNote(noteId));
    });
  };
}
