import { Component, OnInit } from '@angular/core';
import { Theme } from 'src/app/shared/enums/Theme';
import { PersonalizationService, sideBarCloseOpen } from 'src/app/shared/services/personalization.service';
import { Store, Select } from '@ngxs/store';
import { LabelStore } from '../state/labels-state';
import { Observable } from 'rxjs';
import { UserStore } from 'src/app/core/stateUser/user-state';
import { ShortUser } from 'src/app/core/models/short-user';



@Component({
  selector: 'app-labels',
  templateUrl: './labels.component.html',
  styleUrls: ['./labels.component.scss'],
  animations: [ sideBarCloseOpen ],
})
export class LabelsComponent implements OnInit {

  @Select(UserStore.getUserTheme)
  public theme$: Observable<Theme>;

  @Select(LabelStore.countAll)
  countAll$: Observable<number>;

  @Select(LabelStore.countDeleted)
  countDeleted$: Observable<number>;

  @Select(UserStore.getUser)
  public user$: Observable<ShortUser>;

  theme = Theme;

  constructor(
    public pService: PersonalizationService) {}

  async ngOnInit() {

    this.pService.onResize();
  }
}
