import { Injectable } from '@angular/core';
import { NoteTextTypeENUM } from '../../models/editor-models/base-text';
import { ContentModelBase } from '../../models/editor-models/content-model-base';
import { BaseTextElementComponent } from '../full-note-components/html-components/html-base.component';
import { ClickableSelectableEntities } from './models/clickable-selectable-entities.enum';

@Injectable()
export class ClickableContentService {
  setTime: Date;

  currentContent: ContentModelBase;

  currentItemId: string;

  type: ClickableSelectableEntities;

  curentTextItem: BaseTextElementComponent;

  get isEmptyTextItemFocus() {
    if (this.curentTextItem?.content?.noteTextTypeId === NoteTextTypeENUM.Default) {
      return this.curentTextItem.isActiveState;
    }
    return false;
  }

  isEqual(content: ContentModelBase): boolean {
    return content.id === this.currentContent?.id;
  }

  reset() {
    this.setSontent(null, null, this.type, this.curentTextItem);
  }

  isClicked(itemId: string): boolean {
    return this.currentItemId === itemId;
  }

  setSontent(
    content: ContentModelBase,
    itemId: string,
    type: ClickableSelectableEntities,
    curentTextItem: BaseTextElementComponent,
  ) {
    this.currentContent = content;
    this.currentItemId = itemId;
    this.type = type;
    this.curentTextItem = curentTextItem;
  }

  getTextContentIdOrNull(): string {
    if (this.type === ClickableSelectableEntities.Text) {
      return this.currentContent.id;
    }
    return null;
  }
}
