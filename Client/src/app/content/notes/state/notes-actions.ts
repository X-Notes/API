import { Order } from 'src/app/shared/services/order.service';
import { NoteType } from 'src/app/shared/models/noteType';
import { NoteTypeENUM } from 'src/app/shared/enums/NoteTypesEnum';
import { Label } from '../../labels/models/label';
import { Notes } from './Notes';
import { SmallNote } from '../models/smallNote';

export class LoadNotes {
  static type = '[Notes] Load private notes';

  constructor(public id: string, public type: NoteType) {}
}

export class AddNote {
  static type = '[Notes] Add note';
}

export class UpdateNotes {
  static type = '[Notes] Update notes';

  constructor(public notes: Notes, public typeNote: NoteTypeENUM) {}
}

// UPPER MENU FUNCTIONS

export class ChangeColorNote {
  static type = '[Notes] Change color note';

  constructor(public color: string, public selectedIds: string[]) {}
}

export class ClearColorNotes {
  static type = '[Notes] Clear color note';
}

export class CopyNotes {
  static type = '[Notes] Copy notes';

  constructor(public typeNote: NoteType, public selectedIds: string[]) {}
}

export class ClearAddedPrivateNotes {
  static type = '[Notes] ClearAddedPrivate notes';
}

// CHANGE STATE
export class SetDeleteNotes {
  static type = '[Notes] SetDelete notes';

  constructor(public typeNote: NoteType, public selectedIds: string[]) {}
}

export class DeleteNotesPermanently {
  static type = '[Notes] Delete notes';

  constructor(public selectedIds: string[], public typeNote: NoteType) {}
}

export class ArchiveNotes {
  static type = '[Notes] Archive notes';

  constructor(public typeNote: NoteType, public selectedIds: string[]) {}
}

export class MakeSharedNotes {
  static type = '[Notes] Archive notes';

  constructor(public typeNote: NoteType, public selectedIds: string[]) {}
}

export class MakePrivateNotes {
  static type = '[Notes] MakePrivate notes';

  constructor(public typeNote: NoteType, public selectedIds: string[]) {}
}

// Labels

export class UpdateLabelOnNote {
  static type = '[Notes] Update label';

  constructor(public label: Label) {}
}

export class AddLabelOnNote {
  static type = '[Notes] Add label';

  constructor(public label: Label, public typeNote: NoteType, public selectedIds: string[]) {}
}

export class RemoveLabelFromNote {
  static type = '[Notes] Remove label';

  constructor(public label: Label, public typeNote: NoteType, public selectedIds: string[]) {}
}

export class ClearUpdatelabelEvent {
  static type = '[Notes] Clear labels';
}

export class RemoveFromDomMurri {
  static type = '[Notes] MurriRemove notes';
}

export class PositionNote {
  static type = '[Notes] Position notes';

  constructor(public order: Order, public typeNote: NoteType) {}
}

// SHARING

export class GetInvitedUsersToNote {
  static type = '[Notes] Get InvitedUsersToNote';

  constructor(public noteId: string) {}
}

export class LoadOnlineUsersOnNote {
  static type = '[Notes] Get OnlineUsersOnNote';

  constructor(public noteId: string) {}
}

// SELECTION
export class SelectIdNote {
  static type = '[Notes] Select note';

  constructor(public id: string, public labelIds: string[]) {}
}

export class UnSelectIdNote {
  static type = '[Notes] Unselect note';

  constructor(public id: string) {}
}

export class UnSelectAllNote {
  static type = '[Notes] Unselect all';
}

export class SelectAllNote {
  static type = '[Notes] Select all';

  constructor(public typeNote: NoteType) {}
}

// UPDATING FROM FULL NOTE

export class UpdateOneNote {
  static type = '[Notes] update one one';

  constructor(public note: SmallNote, public typeNote: NoteTypeENUM) {}
}

export class CancelAllSelectedLabels {
  static type = '[Notes] Cancel all selected labels';

  constructor(public isCanceled: boolean) {}
}

export class UpdateSelectLabel {
  static type = '[Notes] Updated selected label';

  constructor(public id: string) {}
}
// FULL NOTE

export class LoadFullNote {
  static type = '[Notes] Load full note';

  constructor(public id: string) {}
}

export class DeleteCurrentNote {
  static type = '[Notes] delete full note';
}

export class UpdateTitle {
  static type = '[Notes] update title';

  constructor(public str: string) {}
}

export class UpdateLabelFullNote {
  static type = '[Notes] update label full note';

  constructor(public label: Label, public remove: boolean) {}
}

export class ChangeColorFullNote {
  static type = '[Notes] change color fullNote';

  constructor(public color: string) {}
}

export class ChangeTypeFullNote {
  static type = '[Notes] change type fullNote';

  constructor(public type: NoteType) {}
}

export class TransformTypeNotes {
  static type = '[Notes] transform type notes';

  constructor(
    public typeFrom: NoteTypeENUM,
    public typeTo: NoteTypeENUM,
    public selectedIds: string[],
  ) {}
}
