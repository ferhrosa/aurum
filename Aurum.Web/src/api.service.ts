/////////////////////////////////////////////
// Código gerado por um template T4.       //
// Não modifique diretamente este arquivo. //
/////////////////////////////////////////////
import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { TipoConta, Cartao, Conta, Categoria, Movimentacao } from './model';

const baseUrl = 'http://localhost:33823/';
const headers = new Headers({ 'Content-Type': 'application/json' });


@Injectable()
export class CartoesApiService {

    constructor(private http: Http) { }

    listar(): Promise<Cartao[]> {
        const url = `${baseUrl}cartoes`
        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as Cartao[])
            .catch(this.handleError);
    }

    obter(id: number): Promise<Cartao> {
        const url = `${baseUrl}cartoes/${id}`
        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as Cartao)
            .catch(this.handleError);
    }

    inserir(cartao: Cartao): Promise<Cartao> {
        const url = `${baseUrl}cartoes`
        return this.http
            .post(url, JSON.stringify(cartao), { headers })
            .toPromise()
            .then(response => response.json() as Cartao)
            .catch(this.handleError);
    }

    atualizar(id: number, cartao: Cartao): Promise<void> {
        const url = `${baseUrl}cartoes/${id}`
        return this.http
            .put(url, JSON.stringify(cartao), { headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    excluir(id: number): Promise<void> {
        const url = `${baseUrl}cartoes/${id}`
        return this.http
            .delete(url)
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('Ocorreu um erro', error);
        return Promise.reject(error.message || error);
    }

} // class CartoesApiService


@Injectable()
export class MovimentacoesApiService {

    constructor(private http: Http) { }

    listar(): Promise<Movimentacao[]> {
        const url = `${baseUrl}movimentacoes`
        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as Movimentacao[])
            .catch(this.handleError);
    }

    obter(id: string): Promise<Movimentacao> {
        const url = `${baseUrl}movimentacoes/${id}`
        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as Movimentacao)
            .catch(this.handleError);
    }

    inserir(movimentacao: Movimentacao): Promise<Movimentacao> {
        const url = `${baseUrl}movimentacoes`
        return this.http
            .post(url, JSON.stringify(movimentacao), { headers })
            .toPromise()
            .then(response => response.json() as Movimentacao)
            .catch(this.handleError);
    }

    atualizar(id: string, movimentacao: Movimentacao): Promise<void> {
        const url = `${baseUrl}movimentacoes/${id}`
        return this.http
            .put(url, JSON.stringify(movimentacao), { headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    excluir(id: string): Promise<void> {
        const url = `${baseUrl}movimentacoes/${id}`
        return this.http
            .delete(url)
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('Ocorreu um erro', error);
        return Promise.reject(error.message || error);
    }

} // class MovimentacoesApiService


@Injectable()
export class ContasApiService {

    constructor(private http: Http) { }

    listar(): Promise<Conta[]> {
        const url = `${baseUrl}contas`
        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as Conta[])
            .catch(this.handleError);
    }

    obter(id: number): Promise<Conta> {
        const url = `${baseUrl}contas/${id}`
        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as Conta)
            .catch(this.handleError);
    }

    inserir(conta: Conta): Promise<Conta> {
        const url = `${baseUrl}contas`
        return this.http
            .post(url, JSON.stringify(conta), { headers })
            .toPromise()
            .then(response => response.json() as Conta)
            .catch(this.handleError);
    }

    atualizar(id: number, conta: Conta): Promise<void> {
        const url = `${baseUrl}contas/${id}`
        return this.http
            .put(url, JSON.stringify(conta), { headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    excluir(id: number): Promise<void> {
        const url = `${baseUrl}contas/${id}`
        return this.http
            .delete(url)
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('Ocorreu um erro', error);
        return Promise.reject(error.message || error);
    }

} // class ContasApiService


@Injectable()
export class CategoriasApiService {

    constructor(private http: Http) { }

    listar(): Promise<Categoria[]> {
        const url = `${baseUrl}categorias`
        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as Categoria[])
            .catch(this.handleError);
    }

    obter(id: number): Promise<Categoria> {
        const url = `${baseUrl}categorias/${id}`
        return this.http
            .get(url)
            .toPromise()
            .then(response => response.json() as Categoria)
            .catch(this.handleError);
    }

    inserir(categoria: Categoria): Promise<Categoria> {
        const url = `${baseUrl}categorias`
        return this.http
            .post(url, JSON.stringify(categoria), { headers })
            .toPromise()
            .then(response => response.json() as Categoria)
            .catch(this.handleError);
    }

    atualizar(id: number, categoria: Categoria): Promise<void> {
        const url = `${baseUrl}categorias/${id}`
        return this.http
            .put(url, JSON.stringify(categoria), { headers })
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    excluir(id: number): Promise<void> {
        const url = `${baseUrl}categorias/${id}`
        return this.http
            .delete(url)
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('Ocorreu um erro', error);
        return Promise.reject(error.message || error);
    }

} // class CategoriasApiService

