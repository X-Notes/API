import { Component, OnInit, Input, OnDestroy, EventEmitter, Output } from '@angular/core';
import { Folder } from '../models/folder';
import { Store } from '@ngxs/store';
import { Subject } from 'rxjs';
import { FolderStore } from '../state/folders-state';
import { takeUntil, map, debounceTime } from 'rxjs/operators';
import { SelectIdFolder, UnSelectIdFolder, UpdateTitle } from '../state/folders-actions';
import { Router } from '@angular/router';
import { FontSize } from 'src/app/shared/enums/FontSize';
import { AppStore } from 'src/app/core/stateApp/app-state';

@Component({
  selector: 'app-folder',
  templateUrl: './folder.component.html',
  styleUrls: ['./folder.component.scss']
})
export class FolderComponent implements OnInit, OnDestroy {

  fontSize = FontSize;
  destroy = new Subject<void>();

  nameChanged: Subject<string> = new Subject<string>(); // CHANGE

  @Input() folder: Folder;

  constructor(private store: Store,
              private router: Router) { }

  ngOnDestroy(): void {
    this.destroy.next();
    this.destroy.complete();
  }

  ngOnInit(): void {

    this.nameChanged.pipe(
      takeUntil(this.destroy),
      debounceTime(250))
      .subscribe(title => {
        if (title) {
          const type = this.store.selectSnapshot(AppStore.getTypeFolder);
          this.store.dispatch(new UpdateTitle(title, this.folder.id, type));
        }
      });
  }

  tryFind(z: string[]): boolean {
    const exist = z.find(id => id === this.folder.id);
    return exist !== undefined ? true : false;
  }

  highlight(id: string) {
    if (!this.folder.isSelected) {
      this.store.dispatch(new SelectIdFolder(id));
    } else {
      this.store.dispatch(new UnSelectIdFolder(id));
    }
  }

  toFolder() {
    const flag = this.store.selectSnapshot(FolderStore.selectedCount) > 0 ? true : false;
    if (flag) {
      this.highlight(this.folder.id);
    } else {
      this.router.navigate([`folders/${this.folder.id}`]);
    }
  }

  shadeColor(color, percent) {

    let R = parseInt(color.substring(1, 3), 16);
    let G = parseInt(color.substring(3, 5), 16);
    let B = parseInt(color.substring(5, 7), 16);

    R = parseInt((R * (100 + percent) / 100).toString(), 10);
    G = parseInt((G * (100 + percent) / 100).toString(), 10);
    B = parseInt((B * (100 + percent) / 100).toString(), 10);

    R = (R < 255) ? R : 255;
    G = (G < 255) ? G : 255;
    B = (B < 255) ? B : 255;

    const RR = ((R.toString(16).length === 1) ? '0' + R.toString(16) : R.toString(16));
    const GG = ((G.toString(16).length === 1) ? '0' + G.toString(16) : G.toString(16));
    const BB = ((B.toString(16).length === 1) ? '0' + B.toString(16) : B.toString(16));

    return '#' + RR + GG + BB;
  }

  changed(text) {
    this.nameChanged.next(text);
  }

}
