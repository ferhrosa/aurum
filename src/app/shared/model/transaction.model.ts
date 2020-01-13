import { Entity } from './entity.model';

export class Transaction extends Entity {
    date: Date;
    value: number;
}
