import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { IndexComponent } from './index/index.component';
import { SummaryComponent } from './summary/summary.component';


const routes: Routes = [
  {
    path: '',
    component: IndexComponent,
    pathMatch: 'full',
    /*redirectTo: '/summary'*/
  },
  // {
  //   path: 'summary',
  //   component: SummaryComponent
  // },
  // { path: 'cadastros/categorias', component: CategoriasComponent },
  // { path: 'cadastros/contas', component: ContasComponent },
  // { path: 'cadastros/cartoes', component: CartoesComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
