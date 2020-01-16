import { Observable } from 'rxjs';
import { Entity } from './entity.model';

export class Transaction extends Entity {

    year: number;
    month: number;
    day: number;
    date: Date;
    description: string;
    value: number;

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
