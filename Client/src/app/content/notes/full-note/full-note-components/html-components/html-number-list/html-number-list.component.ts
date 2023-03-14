import {
  AfterViewInit,
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { BaseText } from 'src/app/content/notes/models/editor-models/base-text';
import { ThemeENUM } from 'src/app/shared/enums/theme.enum';
import { ContentTypeENUM } from '../../../../models/editor-models/content-types.enum';
import { ClickableSelectableEntities } from '../../../content-editor-services/models/clickable-selectable-entities.enum';
import { HtmlComponentsFacadeService } from '../../../content-editor/services/html-facade.service';
import { NoteTextTypeENUM } from '../../../content-editor/text/note-text-type.enum';
import { ParentInteraction } from '../../../models/parent-interaction.interface';
import { TransformContent } from '../../../models/transform-content.model';
import { HtmlTextChangesComponent } from '../../html-text-changes-component';

@Component({
  selector: 'app-html-number-list',
  templateUrl: './html-number-list.component.html',
  styleUrls: ['./html-number-list.component.scss', '../../../../../../../styles/innerNote.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HtmlNumberListComponent
  extends HtmlTextChangesComponent
  implements OnInit, OnDestroy, AfterViewInit, ParentInteraction, OnChanges
{
  @Output()
  transformTo = new EventEmitter<TransformContent>();

  @Input()
  prevContent: BaseText;

  @Input()
  prevType: ContentTypeENUM;

  @Input()
  theme: ThemeENUM;

  themeE = ThemeENUM;

  constructor(
    private host: ElementRef,
    cdr: ChangeDetectorRef,
    facade: HtmlComponentsFacadeService,
  ) {
    super(cdr, facade);
  }

  getHost() {
    return this.host;
  }

  ngOnChanges(): void {
    this.setNumber();
  }

  ngAfterViewInit(): void {
    this.setHandlers();
  }

  ngOnDestroy(): void {
    this.destroysListeners();
    this.destroy.next();
    this.destroy.complete();
  }

  ngOnInit(): void {
    this.initBaseHTML();
  }

  setNumber() {
    if (this.prevContent && this.prevContent.noteTextTypeId === NoteTextTypeENUM.numberList) {
      this.content.listNumber = this.prevContent.listNumber + 1;
    } else {
      this.content.listNumber = 1;
    }
  }

  isFocusToNext = () => true;

  // eslint-disable-next-line class-methods-use-this
  backspaceUp() {}

  // eslint-disable-next-line class-methods-use-this
  backspaceDown() {}

  // eslint-disable-next-line class-methods-use-this
  deleteDown() {}

  enter($event: any) {
    $event.preventDefault();
    if (this.isContentEmpty()) {
      this.transformTo.emit({
        id: this.content.id,
        textType: NoteTextTypeENUM.default,
        setFocusToEnd: true,
      });
    } else {
      const breakModel = this.facade.apiBrowserTextService.pressEnterHandler(
        this.getEditableNative(),
      );
      const event = super.eventEventFactory(
        breakModel,
        NoteTextTypeENUM.numberList,
        this.content.id,
      );
      this.enterEvent.emit(event);
    }
  }

  setFocusedElement(): void {
    this.facade.clickableService.setContent(
      this.content,
      null,
      ClickableSelectableEntities.Text,
      this,
    );
  }
}
