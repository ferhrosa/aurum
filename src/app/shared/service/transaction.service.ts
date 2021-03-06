import { Injectable } from '@angular/core';

import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFirestore, QueryFn } from '@angular/fire/firestore';

import { Transaction } from '../model/transaction.model';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';


@Injectable()
export class TransactionService extends BaseService<Transaction> {

    user: firebase.User;

    constructor(
        private afAuth: AngularFireAuth,
        protected db: AngularFirestore,
    ) {
        super(db, 'transactions');
        this.afAuth.user.subscribe(user => {
            this.user = user;
        });
    }

    public getCollectionWithQuery(): Observable<Transaction[]> {
        return super.getCollectionWithQuery(q => q.orderBy('date', 'desc'));
    }

    protected executeCustomMappings(doc: any, entity: Transaction) {
        entity.createdOn = doc.createdOn && doc.createdOn.toDate();
        entity.date = doc.date && doc.date.toDate();
    }

    public async save(transaction: Transaction) {
        // When it's a new transaction, fill the initial data.
        if (!transaction.id) {
            transaction.createdOn = new Date();
            transaction.createdBy = this.user.uid;
        }

        await super.save(transaction);
    }

}
