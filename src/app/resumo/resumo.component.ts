import { Component, OnInit, Inject } from '@angular/core';

import { TransactionService } from '../shared/service/transaction.service';

@Component({
  selector: 'app-resumo',
  templateUrl: './resumo.component.html',
  styleUrls: ['./resumo.component.scss']
})
export class ResumoComponent implements OnInit {

  lista = [];

  constructor(
    private transactionService: TransactionService
  ) { }

  ngOnInit() {
  }

  adicionar(dia?: any) {
    this.transactionService.openDialog();
  }

}
