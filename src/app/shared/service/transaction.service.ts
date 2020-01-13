import { Injectable } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';

import { Transaction } from '../model/transaction.model';
import { TransactionComponent } from '../../transaction/transaction.component';

@Injectable()
export class TransactionService {

    constructor(public dialog: MatDialog) { }

    public async openDialog(transaction?: Transaction) {
        const dialogRef = this.dialog.open(TransactionComponent, {
            data: transaction || new Transaction(),
        });

        await dialogRef.afterClosed().toPromise();
    }

}
