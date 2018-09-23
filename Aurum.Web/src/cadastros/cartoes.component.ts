import { Component, OnInit, Optional } from '@angular/core';
import { MdDialog, MdDialogRef } from '@angular/material';

import { ResultadosDialog } from '../enums';
import { Cartao, Conta } from '../model';
import { CartoesApiService, ContasApiService } from '../api.service';


@Component({
    moduleId: module.id,
    selector: '',
    templateUrl: './cartoes.component.html'
})
export class CartoesComponent implements OnInit {

    lista: Cartao[];
    cartao: Cartao;

    constructor(
        private cartoesApiService: CartoesApiService,
        private _dialog: MdDialog) { }

    ngOnInit() {
        // Está listando de forma assíncrona, para poder executar mais coisas ao mesmo tempo, caso seja necessário.
        this.listar();
    }

    async listar() {
        this.lista = await this.cartoesApiService.listar();
    }

    editar(cartao: Cartao): void {
        this.cartao = cartao;
        let dialogRef = this._dialog.open(CartoesFormComponent);
        dialogRef.componentInstance.cartao = Object.assign({}, cartao);

        dialogRef.afterClosed().subscribe(result => {
            if (result == ResultadosDialog.Salvar) {
                this.listar();
                //alert(JSON.stringify(dialogRef.componentInstance.cartao));
            }
        });
    }

    async excluir(cartao: Cartao) {
        if (confirm(`Deseja realmente excluir o cartão ${cartao.Descricao}?`)) {
            await this.cartoesApiService.excluir(cartao.Id);
            this.listar();
        }
    }
}


@Component({
    moduleId: module.id,
    templateUrl: './cartoes-form.component.html'
})
export class CartoesFormComponent implements OnInit {

    cartao: Cartao;
    contas: Conta[];

    salvando = false;

    constructor(
        @Optional() public dialogRef: MdDialogRef<CartoesFormComponent>,
        private cartoesApiService: CartoesApiService,
        private contasApiService: ContasApiService) { }

    async ngOnInit() {
        this.contas = await this.contasApiService.listar();
    }

    async salvar() {
        if (this.cartao.Id) {
            await this.cartoesApiService.atualizar(this.cartao.Id, this.cartao);
        }
        else {
            this.cartao = await this.cartoesApiService.inserir(this.cartao);
        }

        this.dialogRef.close(ResultadosDialog.Salvar);
    }

    onSubmit() {
        this.salvando = true;
        this.salvar();
    }

}