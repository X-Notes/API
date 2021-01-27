import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { CheckedList, ContentModel, ContentType, HtmlText } from '../../../models/ContentMode';
import { EnterEvent } from '../../../models/enterEvent';
import { ParentInteraction } from '../../../models/parent-interaction.interface';
import { TransformContent } from '../../../models/transform-content';
import { CheckListService } from '../../html-business-logic/checkList.service';

@Component({
  selector: 'app-html-check-list',
  templateUrl: './html-check-list.component.html',
  styleUrls: ['./html-check-list.component.scss'],
  providers: [CheckListService]
})
export class HtmlCheckListComponent implements OnInit, OnDestroy, AfterViewInit, ParentInteraction {

  @Output()
  transformTo = new EventEmitter<TransformContent>();

  @Output()
  enterEvent = new EventEmitter<EnterEvent>();

  @Output()
  deleteThis = new EventEmitter<string>();

  @Output()
  concatThisWithPrev = new EventEmitter<string>();

  @Input()
  content: ContentModel<CheckedList>;

  @ViewChild('contentHtml') contentHtml: ElementRef;

  constructor(public checkListService: CheckListService) { }

  getContent() {
    return this.content;
  }

  ngAfterViewInit(): void {
    this.checkListService.setHandlers(this.content, this.contentHtml, this.enterEvent, this.concatThisWithPrev, this.deleteThis);
  }

  ngOnDestroy(): void {
    this.checkListService.destroysListeners();
  }

  ngOnInit(): void {
    this.checkListService.contentStr = this.content.data.content;
    this.checkListService.transformTo = this.transformTo;
  }

  setFocus($event?) {
    this.checkListService.setFocus($event, this.contentHtml);
  }

  setFocusToEnd() {
    this.checkListService.setFocusToEnd(this.contentHtml);
  }

  updateHTML(content: string) {
    this.content.data.content = content;
    this.contentHtml.nativeElement.innerHTML = content;
  }

  getNative()
  {
    return this.contentHtml?.nativeElement;
  }

  mouseEnter($event) {
  }

  mouseOut($event) {
  }

  get isActive()
  {
    return this.content.data.content.length === 0 && document.activeElement === this.getNative();
  }

}
