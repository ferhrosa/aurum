import { Component, Optional, OnInit } from '@angular/core';
import { MdDialog, MdDialogRef } from '@angular/material';

import { ResultadosDialog } from '../enums';
import { Cartao, Categoria, Conta, Movimentacao } from '../model';
import { CartoesApiService, ContasApiService, CategoriasApiService, MovimentacoesApiService } from '../api.service';


@Component({
    moduleId: module.id,
    templateUrl: './movimentacoes-form.component.html'
})
export class MovimentacoesFormComponent implements OnInit {

    movimentacao: Movimentacao;

    categorias: Categoria[];
    contas: Conta[];
    cartoes: Cartao[];

    salvando = false;


    private static abrir(dialog: MdDialog): MdDialogRef<MovimentacoesFormComponent> {
        return dialog.open(
            MovimentacoesFormComponent,
            {
                disableClose: true
            }
        )
    }

    public static adicionar(dialog: MdDialog, data?: Date): MdDialogRef<MovimentacoesFormComponent> {
        var dialogRef = MovimentacoesFormComponent.abrir(dialog);

        dialogRef.componentInstance.movimentacao = <Movimentacao>{
            Data: data,
            Efetivada: false
        };

        return dialogRef;
    }

    public static editar(dialog: MdDialog, movimentacao: Movimentacao): MdDialogRef<MovimentacoesFormComponent> {
        var dialogRef = MovimentacoesFormComponent.abrir(dialog);

        dialogRef.componentInstance.movimentacao = movimentacao;

        return dialogRef;
    }


    constructor(
        @Optional() public dialogRef: MdDialogRef<MovimentacoesFormComponent>,
        private cartoesApiService: CartoesApiService,
        private contasApiService: ContasApiService,
        private categoriasApiService: CategoriasApiService,
        private movimentacoesApiService: MovimentacoesApiService,
    ) { }

    async ngOnInit() {
        this.categoriasApiService.listar().then((categorias) => this.categorias = categorias);
        this.contasApiService.listar().then((contas) => this.contas = contas);
        this.cartoesApiService.listar().then((cartoes) => this.cartoes = cartoes);

        console.dir(this.movimentacao);
    }

    async salvar() {
        if (this.movimentacao.Id) {
            await this.movimentacoesApiService.atualizar(this.movimentacao.Id, this.movimentacao);
        }
        else {
            this.movimentacao = await this.movimentacoesApiService.inserir(this.movimentacao);
        }

        this.dialogRef.close(ResultadosDialog.Salvar);
    }

    onSubmit() {
        this.salvando = true;
        this.salvar();
    }

}
