import { Component, OnInit, Inject } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';

import { Transaction } from '../shared/model/transaction.model';
import { TransactionService } from '../shared/service/transaction.service';

import { TransactionComponent } from '../transaction/transaction.component';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-resumo',
  templateUrl: './resumo.component.html',
  styleUrls: ['./resumo.component.scss']
})
export class ResumoComponent implements OnInit {

  list: Observable<Transaction[]>;

  constructor(
    private transactionService: TransactionService,
    public dialog: MatDialog,
  ) {
    this.list = transactionService.getCollection();
  }

  ngOnInit() {
  }

  adicionar(dia?: any) {
    this.openDialog();
  }

  public async openDialog(transaction?: Transaction) {
    const dialogRef = this.dialog.open(TransactionComponent, {
      data: transaction || new Transaction(),
    });

    await dialogRef.afterClosed().toPromise();
  }
}
