import { Component, OnInit, Inject } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';

import { Transaction } from '../shared/model/transaction.model';
import { TransactionService } from '../shared/service/transaction.service';

import { TransactionComponent } from '../transaction/transaction.component';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss']
})
export class SummaryComponent implements OnInit {

  list: Observable<Transaction[]>;

  constructor(
    private transactionService: TransactionService,
    public dialog: MatDialog,
  ) {
    this.list = transactionService.getCollectionWithQuery();
  }

  ngOnInit() {
  }

  add(dia?: any) {
    this.openDialog();
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
