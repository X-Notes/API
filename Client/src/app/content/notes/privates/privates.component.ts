import {
  Component,
  OnInit,
  OnDestroy,
  ViewChildren,
  ElementRef,
  QueryList,
  AfterViewInit,
} from '@angular/core';
import { PersonalizationService } from 'src/app/shared/services/personalization.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { NoteTypeENUM } from 'src/app/shared/enums/NoteTypesEnum';
import { UpdateRoute } from 'src/app/core/stateApp/app-action';
import { EntityType } from 'src/app/shared/enums/EntityTypes';
import { MurriService } from 'src/app/shared/services/murri.service';
import { Store } from '@ngxs/store';
import { AppStore } from 'src/app/core/stateApp/app-state';
import { FontSizeENUM } from 'src/app/shared/enums/FontSizeEnum';
import { NotesService } from '../notes.service';
import { NoteStore } from '../state/notes-state';
import { UnSelectAllNote } from '../state/notes-actions';

@Component({
  selector: 'app-privates',
  templateUrl: './privates.component.html',
  styleUrls: ['./privates.component.scss'],
  providers: [NotesService],
})
export class PrivatesComponent implements OnInit, OnDestroy, AfterViewInit {
  fontSize = FontSizeENUM;

  destroy = new Subject<void>();

  loaded = false;

  @ViewChildren('item', { read: ElementRef }) refElements: QueryList<ElementRef>;

  constructor(
    public pService: PersonalizationService,
    private store: Store,
    public murriService: MurriService,
    public noteService: NotesService,
  ) {}

  ngAfterViewInit(): void {
    this.noteService.murriInitialise(this.refElements, NoteTypeENUM.Private);
  }

  async ngOnInit() {
    await this.store.dispatch(new UpdateRoute(EntityType.NotePrivate)).toPromise();
    this.pService.setSpinnerState(true);

    this.store
      .select(AppStore.appLoaded)
      .pipe(takeUntil(this.destroy))
      .subscribe(async (x: boolean) => {
        if (x) {
          await this.loadContent();
        }
      });
  }

  async loadContent(typeENUM = NoteTypeENUM.Private) {
    await this.noteService.loadNotes(typeENUM);

    let notes = this.store.selectSnapshot(NoteStore.privateNotes);
    notes = this.noteService.transformNotes(notes);
    this.noteService.firstInit(notes);

    await this.pService.waitPreloading();
    this.pService.setSpinnerState(false);
    this.loaded = true;

    this.store
      .select(NoteStore.notesAddingPrivate)
      .pipe(takeUntil(this.destroy))
      .subscribe((x) => this.noteService.addToDom(x));
  }

  ngOnDestroy(): void {
    this.murriService.flagForOpacity = false;
    this.murriService.muuriDestroy();
    this.destroy.next();
    this.destroy.complete();
    this.store.dispatch(new UnSelectAllNote());
  }
}
