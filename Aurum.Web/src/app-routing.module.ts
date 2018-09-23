import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ResumoComponent } from './resumo.component';
import { CategoriasComponent } from './cadastros/categorias.component';
import { ContasComponent } from './cadastros/contas.component';
import { CartoesComponent } from './cadastros/cartoes.component';


const routes: Routes = [
    {
        path: '',
        redirectTo: '/resumo',
        pathMatch: 'full'
    },
    {
        path: 'resumo',
        component: ResumoComponent
    },
    {
        path: 'cadastros/categorias',
        component: CategoriasComponent
    },
    {
        path: 'cadastros/contas',
        component: ContasComponent
    },
    {
        path: 'cadastros/cartoes',
        component: CartoesComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
