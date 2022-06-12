import { ElementRef, Renderer2, ViewChild, Component } from '@angular/core';

import { Select, Store } from '@ngxs/store';
import { Observable } from 'rxjs';
import { FullNote } from 'src/app/content/notes/models/full-note.model';
import { OnlineUsersNote } from 'src/app/content/notes/models/online-users-note.model';
import { NoteStore } from 'src/app/content/notes/state/notes-state';
import { NoteTypeENUM } from 'src/app/shared/enums/note-types.enum';
import {
  PersonalizationService,
  showMenuLeftRight,
} from 'src/app/shared/services/personalization.service';
import { DialogsManageService } from '../../services/dialogs-manage.service';
import { MenuButtonsService } from '../../services/menu-buttons.service';
import { PermissionsButtonsService } from '../../services/permissions-buttons.service';

@Component({
  selector: 'app-interaction-inner-note',
  templateUrl: './interaction-inner-note.component.html',
  styleUrls: ['./interaction-inner-note.component.scss'],
  animations: [showMenuLeftRight],
})
export class InteractionInnerNoteComponent {
  @Select(NoteStore.oneFull)
  note$: Observable<FullNote>;

  @Select(NoteStore.fullNoteType)
  noteType$: Observable<NoteTypeENUM>;

  @Select(NoteStore.getOnlineUsersOnNote)
  onlineUsers$: Observable<OnlineUsersNote[]>;

  @ViewChild('heightPeople') heightPeople: ElementRef;

  @ViewChild('scrollbar') scrollbar: ElementRef;

  constructor(
    public pService: PersonalizationService,
    public renderer: Renderer2,
    public buttonService: MenuButtonsService,
    public dialogsManageService: DialogsManageService,
    public pB: PermissionsButtonsService,
    private store: Store,
  ) {}

  closeMenu(): void {
    if (this.pService.checkWidth()) {
      this.pService.users = false;
    }

    if (!this.pService.widthMoreThan1024()) {
      this.pService.hideInnerMenu = false;
    }
  }

  hideMenu() {
    this.pService.hideInnerMenu = !this.pService.hideInnerMenu;
  }

  showUsers() {
    this.pService.users = !this.pService.users;
  }

  openRelatedNotesPopup() {
    const noteId = this.store.selectSnapshot(NoteStore.oneFull).id;
    const isCanEdit = this.store.selectSnapshot(NoteStore.canEdit);
    this.dialogsManageService.openRelatedNotes(noteId, isCanEdit);
  }

  openHistoriesPopup() {
    const noteId = this.store.selectSnapshot(NoteStore.oneFull).id;
    this.dialogsManageService.openNoteHistoriesMobile(noteId);
  }

  disableTooltpUser(): boolean {
    if (this.pService.checkWidth()) {
      return true;
    }
    return false;
  }
}
