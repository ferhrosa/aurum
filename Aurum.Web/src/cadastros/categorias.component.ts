import { Component, OnInit, Optional } from '@angular/core';
import { MdDialog, MdDialogRef } from '@angular/material';

import { ResultadosDialog } from '../enums';
import { Categoria } from '../model';
import { CategoriasApiService } from '../api.service';


@Component({
    moduleId: module.id,
    selector: '',
    templateUrl: './categorias.component.html'
})
export class CategoriasComponent implements OnInit {

    lista: Categoria[];
    categoria: Categoria;

    constructor(
        private categoriasApiService: CategoriasApiService,
        private _dialog: MdDialog) { }

    ngOnInit() {
        // Está listando de forma assíncrona, para poder executar mais coisas ao mesmo tempo, caso seja necessário.
        this.listar();
    }

    async listar() {
        this.lista = await this.categoriasApiService.listar();
    }

    adicionar(): void { this.editar(<Categoria>{}); }

    editar(categoria: Categoria): void {
        this.categoria = categoria;
        let dialogRef = this._dialog.open(CategoriasFormComponent);
        dialogRef.componentInstance.categoria = Object.assign({}, categoria);

        dialogRef.afterClosed().subscribe(result => {
            if (result == ResultadosDialog.Salvar) {
                this.listar();
                //alert(JSON.stringify(dialogRef.componentInstance.categoria));
            }
        });
    }

    async excluir(categoria: Categoria) {
        if (confirm(`Deseja realmente excluir a categoria ${categoria.Nome}?`)) {
            await this.categoriasApiService.excluir(categoria.Id);
            this.listar();
        }
    }
}


@Component({
    moduleId: module.id,
    templateUrl: './categorias-form.component.html'
})
export class CategoriasFormComponent {

    categoria: Categoria;
    
    salvando = false;

    constructor(
        @Optional() public dialogRef: MdDialogRef<CategoriasFormComponent>,
        private categoriasApiService: CategoriasApiService) { }

    async salvar() {
        if (this.categoria.Id) {
            await this.categoriasApiService.atualizar(this.categoria.Id, this.categoria);
        }
        else {
            this.categoria = await this.categoriasApiService.inserir(this.categoria);
        }

        this.dialogRef.close(ResultadosDialog.Salvar);
    }

    onSubmit() {
        this.salvando = true;
        this.salvar();
    }

}