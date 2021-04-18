import {
  Component,
  OnInit,
  OnDestroy,
  ViewChildren,
  ElementRef,
  QueryList,
  AfterViewInit,
} from '@angular/core';
import { Subject } from 'rxjs';
import { Store } from '@ngxs/store';
import { PersonalizationService } from 'src/app/shared/services/personalization.service';
import { takeUntil } from 'rxjs/operators';
import { UpdateRoute } from 'src/app/core/stateApp/app-action';
import { EntityType } from 'src/app/shared/enums/EntityTypes';
import { MurriService } from 'src/app/shared/services/murri.service';
import { AppStore } from 'src/app/core/stateApp/app-state';
import { FontSizeENUM } from 'src/app/shared/enums/FontSizeEnum';
import { LabelsService } from '../labels.service';
import { LabelStore } from '../state/labels-state';
import { UpdateLabel, SetDeleteLabel, LoadLabels, AddLabel } from '../state/labels-actions';
import { Label } from '../models/label';

@Component({
  selector: 'app-all',
  templateUrl: './all.component.html',
  styleUrls: ['./all.component.scss'],
  providers: [LabelsService],
})
export class AllComponent implements OnInit, OnDestroy, AfterViewInit {
  @ViewChildren('item', { read: ElementRef }) refElements: QueryList<ElementRef>;

  fontSize = FontSizeENUM;

  destroy = new Subject<void>();

  loaded = false;

  constructor(
    public pService: PersonalizationService,
    private store: Store,
    public murriService: MurriService,
    public labelService: LabelsService,
  ) {}

  async ngOnInit() {
    await this.store.dispatch(new UpdateRoute(EntityType.LabelPrivate)).toPromise();

    this.store
      .select(AppStore.appLoaded)
      .pipe(takeUntil(this.destroy))
      .subscribe(async (x: boolean) => {
        if (x) {
          await this.loadContent();
        }
      });
  }

  ngAfterViewInit(): void {
    this.labelService.murriInitialise(this.refElements, false);
  }

  async loadContent() {
    this.pService.setSpinnerState(true);
    await this.store.dispatch(new LoadLabels()).toPromise();

    const labels = this.store.selectSnapshot(LabelStore.all);
    this.labelService.firstInit(labels);

    await this.pService.waitPreloading();
    this.pService.setSpinnerState(false);
    this.loaded = true;

    this.pService.newButtonSubject.pipe(takeUntil(this.destroy)).subscribe(() => this.newLabel());
  }

  async update(label: Label) {
    this.store.dispatch(new UpdateLabel(label));
  }

  async newLabel() {
    await this.store.dispatch(new AddLabel()).toPromise();

    const labels = this.store.selectSnapshot(LabelStore.all);
    this.labelService.labels.unshift(labels[0]);
    setTimeout(
      () =>
        this.murriService.grid.add(document.querySelector('.grid-item'), {
          index: 0,
          layout: true,
        }),
      0,
    );
  }

  async setDelete(label: Label) {
    await this.store.dispatch(new SetDeleteLabel(label)).toPromise();
    this.labelService.labels = this.labelService.labels.filter((x) => x.id !== label.id);
    setTimeout(() => this.murriService.grid.refreshItems().layout(), 0);
  }

  ngOnDestroy(): void {
    this.murriService.flagForOpacity = false;
    this.murriService.muuriDestroy();
    this.destroy.next();
    this.destroy.complete();
  }
}
