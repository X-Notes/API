import { EntityRef } from 'src/app/shared/models/entityRef';
import { NoteType } from 'src/app/shared/models/noteType';
import { Label } from '../../labels/models/label';

export interface SmallNote {
  id: string;
  title: string;
  color: string;
  labels: Label[];
  refType: EntityRef;
  noteType: NoteType;
  isSelected?: boolean;
  lockRedirect?: boolean;
}
