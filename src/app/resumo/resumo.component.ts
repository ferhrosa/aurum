import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-resumo',
  templateUrl: './resumo.component.html',
  styleUrls: ['./resumo.component.scss']
})
export class ResumoComponent implements OnInit {

  lista = [];

  constructor() { }

  ngOnInit() {
  }

  adicionar(dia?: any) {
    // TODO
  }

}
