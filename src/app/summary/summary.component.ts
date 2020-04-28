import { Component, OnInit, Inject } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';

import { Transaction } from '../shared/model/transaction.model';
import { TransactionService } from '../shared/service/transaction.service';

import { TransactionComponent } from '../transaction/transaction.component';
import { Observable } from 'rxjs';
import { groupBy } from 'rxjs/operators';


@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss']
})
export class SummaryComponent implements OnInit {

  list: Observable<Transaction[]>;

  days: DayGroup[] = [];

  constructor(
    private transactionService: TransactionService,
    public dialog: MatDialog,
  ) {
    this.list = transactionService.getCollectionWithQuery();

    transactionService.getCollectionWithQuery().subscribe(transactions => {

      let dayGroup: DayGroup = null;

      transactions.forEach(t => {
        if (!dayGroup || !dayGroup.day || dayGroup.day.valueOf !== t.day.valueOf) {
          dayGroup = new DayGroup(t.date);
          this.days.push(dayGroup);
        }

        dayGroup.transactions.push(t);
        dayGroup.updateBalance();
      });
    });
  }

  ngOnInit() {
  }

  add(date?: Date) {
    this.openDialog(date ? new Transaction(date) : null);
  }

  edit(transaction: Transaction) {
    this.openDialog(transaction);
  }

  public async openDialog(transaction?: Transaction) {
    const dialogRef = this.dialog.open(TransactionComponent, {
      data: transaction || new Transaction(),
    });

    await dialogRef.afterClosed().toPromise();
  }

  delete(transaction: Transaction) {
    this.transactionService.delete(transaction);
  }

}

class DayGroup {

  day: number;
  date: Date;
  balance: number;
  transactions: Transaction[] = [];

  constructor(date: Date) {
    this.date = date;
  }

  updateBalance() {
    this.balance = this.transactions
      .map(t => t.value * t.type)
      .reduce((previous, current) => previous + current);
  }

}
