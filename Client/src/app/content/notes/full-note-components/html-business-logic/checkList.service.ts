import { ElementRef, Injectable } from '@angular/core';
import { CheckedList, ContentModel } from '../../models/ContentMode';
import { HtmlService } from './html.service';



@Injectable()
export class CheckListService extends HtmlService {

    onInput(content: ContentModel<CheckedList>, contentHtml: ElementRef) {
        content.data.content = contentHtml.nativeElement.innerText;
    }

    onBlur(e: any) {
        // BLUR HANDLER
    }

    pasteCommandHandler(e: any) {
        throw new Error('Method not implemented.');
    }

    mouseUp(e: any) {
        super.mouseUp(e);
    }

    onSelectStart(e: any) {
        // SELECTIION
    }

    enter(e: any) {
        throw new Error('Method not implemented.');
    }

    backDown(e: any) {
        throw new Error('Method not implemented.');
    }

    backUp(e: any) {
        throw new Error('Method not implemented.');
    }

    getTextChild(contentHtml: ElementRef)
    {
      return contentHtml.nativeElement.children[0];
    }

}
