import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ServiceWorkerModule } from '@angular/service-worker';
import { NgxsModule } from '@ngxs/store';
import { NgxsStoragePluginModule } from '@ngxs/storage-plugin';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { ContentActiveteGuard } from './core/guards/content-activete.guard';
import { LabelStore } from './content/labels/state/labels-state';
import { ContentModule } from './content/content.module';
import { NoteStore } from './content/notes/state/notes-state';
import { UserStore } from './core/stateUser/user-state';
import { FolderStore } from './content/folders/state/folders-state';
import { AppStore } from './core/stateApp/app-state';
import { BackgroundStore } from './core/backgrounds/background-state';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CoreModule,
    AppRoutingModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    SharedModule,
    ContentModule,
    NgxsModule.forRoot([LabelStore, NoteStore, UserStore, BackgroundStore, FolderStore, AppStore], {
      developmentMode: !environment.production,
    }),
    NgxsStoragePluginModule.forRoot({
      key: UserStore,
    }),
  ],
  providers: [ContentActiveteGuard],
  bootstrap: [AppComponent],
})
export class AppModule {}
