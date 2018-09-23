import { Component } from '@angular/core';
import { MdDialog } from '@angular/material';

import { MovimentacoesFormComponent } from './movimentacoes/movimentacoes-form.component';

import { ResultadosDialog } from './enums';
import { Movimentacao } from './model';
import { MovimentacoesApiService } from './api.service';


@Component({
    moduleId: module.id,
    selector: 'view-resumo',
    templateUrl: './resumo.component.html',
    styleUrls: ['./resumo.component.css']
})
export class ResumoComponent {

    lista: Movimentacao[];

    constructor(
        private _dialog: MdDialog,
        private movimentacoesApiService: MovimentacoesApiService) { }

    async listar() {
        this.lista = await this.movimentacoesApiService.listar();
    }

    adicionar(data?: Date): void {
        //let dialogRef = this._dialog.open(MovimentacoesFormComponent);
        let dialogRef = MovimentacoesFormComponent.adicionar(this._dialog, data);

        dialogRef.afterClosed().subscribe(result => {
            if (result == ResultadosDialog.Salvar) {

            }
        });
    }

}
