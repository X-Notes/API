import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogData } from '../dialog_data';
import { Select, Store } from '@ngxs/store';
import { LabelStore } from 'src/app/content/labels/state/labels-state';
import { Observable } from 'rxjs';
import { Label } from 'src/app/content/labels/models/label';
import { PersonalizationService } from '../../services/personalization.service';
import { UpdateLabel, DeleteLabel, SetDeleteLabel, AddLabel } from 'src/app/content/labels/state/labels-actions';
import { trigger, transition, animate, style } from '@angular/animations';


@Component({
  selector: 'app-editing-labels-note',
  templateUrl: './editing-labels-note.component.html',
  styleUrls: ['./editing-labels-note.component.scss'],
  animations: [trigger('fadeIn', [
    transition(':enter', [
      style({  height: 0,  }),
      animate('1.2s ease-out', style({ height: '*', })),
    ]),
    transition(':leave', [
      animate('1.2s ease', style({ opacity: 0, height: 0 }))
    ]),
  ]), ]
})
export class EditingLabelsNoteComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<EditingLabelsNoteComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              public pService: PersonalizationService,
              private store: Store) { }

  @Select(LabelStore.all)
  public labels$: Observable<Label[]>;

  ngOnInit(): void {
  }


  update(label: Label) {
    this.store.dispatch(new UpdateLabel(label));
  }

  delete(id: number) {
    this.store.dispatch(new SetDeleteLabel(id));
  }

  async newLabel() {
    await this.store.dispatch(new AddLabel()).toPromise();
  }
}
