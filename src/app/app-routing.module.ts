import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { IndexComponent } from './index/index.component';
import { LayoutComponent } from './layout/layout.component';
import { ResumoComponent } from './resumo/resumo.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: IndexComponent, pathMatch: 'full', /*redirectTo: '/resumo'*/ },
      { path: 'resumo', component: ResumoComponent },
      // { path: 'cadastros/categorias', component: CategoriasComponent },
      // { path: 'cadastros/contas', component: ContasComponent },
      // { path: 'cadastros/cartoes', component: CartoesComponent },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
