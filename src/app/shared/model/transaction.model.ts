import { Observable } from 'rxjs';
import { Entity } from './entity.model';

export class Transaction extends Entity {

    createdOn: Date;
    createdBy: string;

    year: number;
    month: number;
    day: number;
    date: Date;

    type: -1 | 1;
    value: number;

    description: string;

    constructor() {
        super();
        this.date = new Date();
        this.year = this.date.getFullYear();
        this.month = this.date.getMonth() + 1;
        this.day = this.date.getDate();
        this.type = -1;
    }

    dateFromString(stringValue: string) {
        if (!stringValue || !stringValue.toString().trim().length) {
            this.year = null;
            this.month = null;
            this.day = null;
        } else {
            const parts = stringValue.split('-');
            this.year = +parts[0];
            this.month = +parts[1];
            this.day = +parts[2];

            this.date = new Date(this.year, this.month, this.day);
        }
    }

}
