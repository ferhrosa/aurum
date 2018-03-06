import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConfigureComponent } from './configure/configure.component';
import { IndexComponent } from './aurum/index/index.component';
import { LayoutComponent } from './aurum/layout/layout.component';
import { ResumoComponent } from './aurum/resumo/resumo.component';


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
  {
    path: 'configure',
    component: ConfigureComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
