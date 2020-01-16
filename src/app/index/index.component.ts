import { Component, OnInit } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';

import { Transaction } from '../shared/model/transaction.model';

import { TransactionComponent } from '../transaction/transaction.component';


@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class IndexComponent implements OnInit {

  constructor(
    public dialog: MatDialog,
  ) { }

  ngOnInit() {
  }

  openAddTransaction() {
    console.log('openAddTransaction');
    this.openDialog();
  }

  public async openDialog(transaction?: Transaction) {
    const dialogRef = this.dialog.open(TransactionComponent, {
      data: transaction || new Transaction(),
    });

    await dialogRef.afterClosed().toPromise();
  }

}
