import { Component, OnInit, Optional } from '@angular/core';
import { MdDialog, MdDialogRef } from '@angular/material';

import { ResultadosDialog } from '../enums';
import { Conta } from '../model';
import { ContasApiService } from '../api.service';


@Component({
    moduleId: module.id,
    selector: '',
    templateUrl: './contas.component.html'
})
export class ContasComponent implements OnInit {

    lista: Conta[];
    conta: Conta;

    constructor(
        private contasApiService: ContasApiService,
        private _dialog: MdDialog) { }

    ngOnInit() {
        // Está listando de forma assíncrona, para poder executar mais coisas ao mesmo tempo, caso seja necessário.
        this.listar();
    }

    async listar() {
        this.lista = await this.contasApiService.listar();
    }

    editar(conta: Conta): void {
        this.conta = conta;
        let dialogRef = this._dialog.open(ContasFormComponent);
        dialogRef.componentInstance.conta = Object.assign({}, conta);

        dialogRef.afterClosed().subscribe(result => {
            if (result == ResultadosDialog.Salvar) {
                this.listar();
                //alert(JSON.stringify(dialogRef.componentInstance.conta));
            }
        });
    }

    async excluir(conta: Conta) {
        if (confirm(`Deseja realmente excluir a conta ${conta.Descricao}?`)) {
            await this.contasApiService.excluir(conta.Id);
            this.listar();
        }
    }
}


@Component({
    moduleId: module.id,
    templateUrl: './contas-form.component.html'
})
export class ContasFormComponent {

    conta: Conta;

    salvando = false;

    constructor(
        @Optional() public dialogRef: MdDialogRef<ContasFormComponent>,
        private contasApiService: ContasApiService) { }

    async salvar() {
        if (this.conta.Id) {
            await this.contasApiService.atualizar(this.conta.Id, this.conta);
        }
        else {
            this.conta = await this.contasApiService.inserir(this.conta);
        }

        this.dialogRef.close(ResultadosDialog.Salvar);
    }

    onSubmit() {
        this.salvando = true;
        this.salvar();
    }

}