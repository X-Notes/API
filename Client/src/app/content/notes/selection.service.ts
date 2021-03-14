import { ElementRef, Injectable, QueryList } from '@angular/core';
import { ApiBrowserTextService } from './api-browser-text.service';

@Injectable()
export class SelectionService {
  menuHeight = 49;

  sidebarWidth = 270;

  ismousedown = false;

  isResizingPhoto = false;

  isSelectionInside;

  constructor(private apiBrowserService: ApiBrowserTextService) {}

  selectionHandler(secondRect: DOMRect, refElements: QueryList<ElementRef>) {
    const refElementsArray = refElements.toArray();
    const length = refElementsArray.length - 1;

    const itemsSelect: HTMLElement[] = [];
    const itemsNoSelect: HTMLElement[] = [];

    for (let i = 0; i < length; i += 1) {
      const html = refElementsArray[i].nativeElement as HTMLElement;
      const firstRect = html.getBoundingClientRect();
      if (this.isRectToRect(firstRect, secondRect)) {
        itemsSelect.push(html.firstChild as HTMLElement);
      } else {
        itemsNoSelect.push(html.firstChild as HTMLElement);
      }
    }
    this.makeSelect(itemsSelect);
    this.makeNoSelect(itemsNoSelect);
  }

  makeSelect(items: HTMLElement[]) {
    const refElements = [...items];
    if (this.isSelectionInside) {
      if (refElements.length === 1) {
        refElements[0].style.backgroundColor = null;
        return;
      }
      this.apiBrowserService.getSelection().empty();
    }
    for (const elem of refElements) {
      elem.style.backgroundColor = '#2a2d32';
      elem.setAttribute('selectedByUser', 'true');
    }
  }

  makeNoSelect = (refElements: HTMLElement[]) => {
    for (const elem of refElements) {
      elem.style.backgroundColor = null;
      elem.removeAttribute('selectedByUser');
    }
  };

  isSelectionInZone(secondRect: DOMRect, refElements: QueryList<ElementRef>) {
    const refElementsArray = refElements.toArray();
    const length = refElementsArray.length - 1;
    for (let i = 0; i < length; i += 1) {
      const html = refElementsArray[i].nativeElement as HTMLElement;
      const firstRect = html.getBoundingClientRect();
      if (this.isRectToRect(firstRect, secondRect)) {
        return true;
      }
    }
    return false;
  }

  isRectToRect = (firstRect: DOMRect, secondRect: DOMRect) => {
    return (
      firstRect.x < secondRect.x + secondRect.width &&
      secondRect.x < firstRect.x + firstRect.width &&
      firstRect.y < secondRect.y + secondRect.height &&
      secondRect.y < firstRect.y + firstRect.height
    );
  };
}
