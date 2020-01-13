import { Injectable } from '@angular/core';

import { AngularFirestore } from '@angular/fire/firestore';

import { Transaction } from '../model/transaction.model';
import { BaseService } from './base.service';


@Injectable()
export class TransactionService extends BaseService<Transaction> {

    constructor(db: AngularFirestore) {
        super(db, 'transactions');
    }

}
