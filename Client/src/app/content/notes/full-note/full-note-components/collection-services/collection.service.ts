import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { ThemeENUM } from 'src/app/shared/enums/theme.enum';
import { BaseCollection } from '../../../models/editor-models/base-collection';
import { BaseFile } from '../../../models/editor-models/base-file';
import { TextBlock } from '../../../models/editor-models/base-text';
import { ClickableContentService } from '../../content-editor-services/clickable-content.service';
import { UploadFileToEntity } from '../../models/upload-files-to-entity';
import { BaseHtmlComponent } from '../base-html-components';
import { TitleCollectionComponent } from '../collection-components/title-collection/title-collection.component';

@Component({
  template: '',
})
// eslint-disable-next-line @angular-eslint/component-class-suffix
export class CollectionService<T extends BaseCollection<BaseFile>> extends BaseHtmlComponent {
  
  @Output()
  uploadEvent = new EventEmitter<UploadFileToEntity>();

  @Output()
  deleteContentEvent = new EventEmitter<string>();

  @Output()
  deleteContentItemEvent = new EventEmitter<string>();

  @Output()
  someChangesEvent = new EventEmitter();

  @Input()
  isReadOnlyMode = false;

  @Input()
  isSelected = false;

  @Input()
  content: T;

  @Input()
  noteId: string;

  @Input()
  theme: ThemeENUM;

  themeE = ThemeENUM;

  @ViewChild(TitleCollectionComponent) titleComponent: TitleCollectionComponent;

  // eslint-disable-next-line @typescript-eslint/no-useless-constructor
  constructor(
    cdr: ChangeDetectorRef,
    protected clickableContentService: ClickableContentService,) {
    super(cdr);
  }

  syncHtmlWithLayout = () => {
    // TODO
  };

  updateHTML = () => {
    return null;
  };

  getTextBlocks = (): TextBlock[] => {
    return null;
  };

  checkForDelete() {
    const item = this.content.items.find((x) => this.clickableContentService.isClicked(x.fileId));
    if (item) {
      this.deleteContentItemEvent.emit(item.fileId);
    }
  }

  onTitleChangeInput(name: string) {
    this.content.name = name;
    this.someChangesEvent.emit();
  }
}
