import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogConfig, MatDialog } from '@angular/material/dialog';

import { Transaction } from '../shared/model/transaction.model';
import { TransactionService } from '../shared/service/transaction.service';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.scss']
})
export class TransactionComponent implements OnInit {

  @ViewChild('transactionForm', { static: false }) form: NgForm;

  constructor(
    private transactionService: TransactionService,
    public dialogRef: MatDialogRef<TransactionComponent>,
    @Inject(MAT_DIALOG_DATA) public transaction: Transaction,
  ) {
  }

  ngOnInit() {
  }

  async save() {
    if (this.form.valid) {
      await this.transactionService.save(this.transaction);
      return this.dialogRef.close(this.transaction);
    }
  }

  cancel() {
    this.dialogRef.close();
  }

}
