import { ElementRef, Injectable, OnDestroy, QueryList } from '@angular/core';
import { PersonalizationService } from 'src/app/shared/services/personalization.service';
import { Store } from '@ngxs/store';
import { MurriService } from 'src/app/shared/services/murri.service';
import { Subscription } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AppStore } from 'src/app/core/stateApp/app-state';
import { NoteTypeENUM } from 'src/app/shared/enums/note-types.enum';
import { ActivatedRoute, Router } from '@angular/router';
import {
  CancelAllSelectedLabels,
  ClearAddToDomNotes,
  LoadNotes,
  UpdateOneNote,
} from './state/notes-actions';
import { NoteStore } from './state/notes-state';
import { SmallNote } from './models/small-note.model';
import { DialogsManageService } from '../navigation/dialogs-manage.service';
import { UserStore } from 'src/app/core/stateUser/user-state';
import { ApiServiceNotes } from './api-notes.service';
import { UpdaterEntitiesService } from '../../core/entities-updater.service';
import { SortedByENUM } from 'src/app/core/models/sorted-by.enum';
import { EntityType } from 'src/app/shared/enums/entity-types.enum';
import { IMurriEntityService } from 'src/app/shared/services/murri-entity.contract';
import { NoteEntitiesService } from 'src/app/shared/services/note-entities.service';

/** Injection only in component */
@Injectable()
export class NotesService
  extends NoteEntitiesService
  implements OnDestroy, IMurriEntityService<SmallNote, NoteTypeENUM>
{
  labelsIds: Subscription;

  firstInitFlag = false;

  prevSortedNoteByTypeId: SortedByENUM = null;

  constructor(
    public pService: PersonalizationService,
    store: Store,
    murriService: MurriService,
    private router: Router,
    private route: ActivatedRoute,
    dialogsManageService: DialogsManageService,
    apiService: ApiServiceNotes,
    private updateService: UpdaterEntitiesService,
  ) {
    super(dialogsManageService, store, murriService, apiService);

    this.store
      .select(NoteStore.removeFromMurriEvent)
      .pipe(takeUntil(this.destroy))
      .subscribe(async (x) => this.deleteFromDom(x));

    this.store
      .select(NoteStore.getIsCanceled)
      .pipe(takeUntil(this.destroy))
      .subscribe(async (x) => {
        if (x === true) {
          await this.destroyGridAsync();

          const tempNotes = this.transformSpread(this.getNotesByCurrentType);
          this.entities = this.orderBy(tempNotes, this.pageSortType);

          const roadType = this.store.selectSnapshot(AppStore.getTypeNote);

          const isDraggable = roadType !== NoteTypeENUM.Shared && this.isSortable;
          await this.murriService.initMurriNoteAsync(roadType, isDraggable);

          await this.murriService.setOpacityFlagAsync(0);
          this.store.dispatch(new CancelAllSelectedLabels(false));
        }
      });

    this.store
      .select(UserStore.getPersonalizationSettings)
      .pipe(takeUntil(this.destroy))
      .subscribe(async (pr) => {
        if (this.prevSortedNoteByTypeId && this.prevSortedNoteByTypeId !== pr.sortedNoteByTypeId) {
          this.prevSortedNoteByTypeId = pr.sortedNoteByTypeId;
          await this.changeOrderTypeHandler(this.pageSortType);
        } else {
          this.prevSortedNoteByTypeId = pr.sortedNoteByTypeId;
        }
      });

    this.store
      .select(NoteStore.notesAddingToDOM)
      .pipe(takeUntil(this.destroy))
      .subscribe((x) => {
        if (x.length > 0) {
          this.addToDom(x);
          this.store.dispatch(ClearAddToDomNotes);
        }
      });
  }

  get getNotesByCurrentType() {
    switch (this.store.selectSnapshot(AppStore.getTypeNote)) {
      case NoteTypeENUM.Private: {
        return this.store.selectSnapshot(NoteStore.privateNotes);
      }
      case NoteTypeENUM.Shared: {
        return this.store.selectSnapshot(NoteStore.sharedNotes);
      }
      case NoteTypeENUM.Archive: {
        return this.store.selectSnapshot(NoteStore.archiveNotes);
      }
      case NoteTypeENUM.Deleted: {
        return this.store.selectSnapshot(NoteStore.deletedNotes);
      }
      default: {
        throw new Error('Incorrect type');
      }
    }
  }

  get isSortable() {
    return this.sortNoteType === SortedByENUM.CustomOrder;
  }

  get isFiltedMode() {
    return this.store.selectSnapshot(NoteStore.getSelectedLabelFilter).length > 0;
  }

  get sortNoteType(): SortedByENUM {
    return this.store.selectSnapshot(UserStore.getPersonalizationSettings).sortedNoteByTypeId;
  }

  get pageSortType(): SortedByENUM {
    const isFullFolder = this.store.selectSnapshot(AppStore.isFolderInner);
    if (isFullFolder) {
      return SortedByENUM.DescDate;
    }
    const isSharedType = this.store.selectSnapshot(AppStore.getTypeNote) === NoteTypeENUM.Shared;
    if (isSharedType) {
      return SortedByENUM.DescDate;
    }
    return this.sortNoteType;
  }

  async changeOrderTypeHandler(sortedType: SortedByENUM) {
    await this.destroyGridAsync();
    this.entities = this.orderBy(this.entities, sortedType);
    const roadType = this.store.selectSnapshot(AppStore.getTypeNote);
    const isDraggable = roadType !== NoteTypeENUM.Shared && this.isSortable;
    this.murriService.initMurriNoteAsync(roadType, isDraggable);
    await this.murriService.setOpacityFlagAsync(0);
  }

  getIsDraggable(noteType: NoteTypeENUM) {
    return !this.isFiltedMode && noteType !== NoteTypeENUM.Shared && this.isSortable;
  }

  murriInitialise(refElements: QueryList<ElementRef>, noteType: NoteTypeENUM) {
    refElements.changes.pipe(takeUntil(this.destroy)).subscribe(async (z) => {
      if (this.getIsFirstInit(z)) {
        await this.murriService.initMurriNoteAsync(noteType, this.getIsDraggable(noteType));
        await this.setInitMurriFlagShowLayout();
        await this.loadNotesWithUpdates();
      }
      await this.synchronizeState(refElements, this.sortNoteType === SortedByENUM.AscDate);
    });
  }

  async loadNotesWithUpdates() {
    const pr = this.store.selectSnapshot(UserStore.getPersonalizationSettings);
    this.updateService.notesIds$.pipe(takeUntil(this.destroy)).subscribe(async (ids) => {
      if (ids.length > 0) {
        const notes = await this.apiService.getNotesMany(ids, pr).toPromise();
        const actionsForUpdate = notes.map((note) => new UpdateOneNote(note, note.noteTypeId));
        this.store.dispatch(actionsForUpdate);
        const transformNotes = this.transformSpread(notes);
        transformNotes.forEach((note) => {
          const index = this.entities.findIndex((x) => x.id === note.id);
          this.entities[index].contents = note.contents;
        });
        await this.murriService.refreshLayoutAsync();
        this.updateService.notesIds$.next([]);
      }
    });
  }

  async loadNotes(typeENUM: NoteTypeENUM) {
    const pr = this.store.selectSnapshot(UserStore.getPersonalizationSettings);
    await this.store.dispatch(new LoadNotes(typeENUM, pr)).toPromise();
    const types = Object.values(NoteTypeENUM).filter(
      (z) => typeof z === 'number' && z !== typeENUM,
    );
    const actions = types.map((t: NoteTypeENUM) => new LoadNotes(t, pr));
    this.store.dispatch(actions);
  }

  navigateFunc(note: SmallNote) {
    const routing = this.store.selectSnapshot(AppStore.getRouting);
    if (routing === EntityType.FolderInnerNote) {
      return this.router.navigate(['../', note.id], { relativeTo: this.route });
    } else {
      return this.router.navigate(['/notes/', note.id]);
    }
  }

  toNote(note: SmallNote) {
    super.baseToNote(note, () => this.navigateFunc(note));
  }

  ngOnDestroy(): void {
    console.log('note destroy');
    super.destroyLayout();
    this.destroy.next();
    this.destroy.complete();
    this.labelsIds?.unsubscribe();
  }

  async initializeEntities(notes: SmallNote[]) {
    let tempNotes = this.transformSpread(notes);
    tempNotes = this.orderBy(tempNotes, this.pageSortType);

    if (!this.isFiltedMode) {
      this.entities = tempNotes;
    } else {
      const ids = this.store.selectSnapshot(NoteStore.getSelectedLabelFilter);
      this.entities = tempNotes.filter((x) =>
        x.labels.some((label) => ids.some((z) => z === label.id)),
      );
    }

    super.initState();

    this.labelsIds = this.store
      .select(NoteStore.getSelectedLabelFilter)
      .pipe(takeUntil(this.destroy))
      .subscribe(async (x) => {
        if (x) {
          await this.updateLabelSelected(x);
        }
      });
    this.firstInitFlag = true;

    await super.loadAdditionNoteInformation();
  }

  async updateLabelSelected(ids: string[]) {
    if (ids.length !== 0 && this.firstInitFlag) {
      await this.destroyGridAsync();

      const tempNotes = this.transformSpread(this.getNotesByCurrentType).filter((x) =>
        x.labels.some((label) => ids.some((z) => z === label.id)),
      );
      this.entities = this.orderBy(tempNotes, this.pageSortType);

      const roadType = this.store.selectSnapshot(AppStore.getTypeNote);
      await this.murriService.initMurriNoteAsync(roadType, false);
      await this.murriService.setOpacityFlagAsync(0);
    }
  }
}
