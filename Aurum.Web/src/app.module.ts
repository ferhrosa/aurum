import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { MdCardModule, MdButtonModule, MdInputModule, MdIconModule,
        MdSidenavModule, MdListModule, MdDialogModule, MdOptionModule,
        MdSlideToggleModule, MdToolbarModule, MdProgressSpinnerModule,
        MdRadioModule, MdSelectModule } from '@angular/material';

import { FlexLayoutModule } from '@angular/flex-layout';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { ResumoComponent } from './resumo.component';
import { CartoesComponent, CartoesFormComponent } from './cadastros/cartoes.component';
import { CategoriasComponent, CategoriasFormComponent } from './cadastros/categorias.component';
import { ContasComponent, ContasFormComponent } from './cadastros/contas.component';
import { MovimentacoesFormComponent } from './movimentacoes/movimentacoes-form.component';

import { CartoesApiService, CategoriasApiService, ContasApiService, MovimentacoesApiService } from './api.service';


@NgModule({
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule,
        HttpModule,
        FlexLayoutModule,
        MdCardModule, MdButtonModule, MdInputModule, MdIconModule,
            MdSidenavModule, MdListModule, MdDialogModule, MdOptionModule,
            MdSlideToggleModule, MdToolbarModule, MdProgressSpinnerModule,
            MdRadioModule, MdSelectModule,
        AppRoutingModule
    ],
    declarations: [
        AppComponent,
        ResumoComponent,
        CartoesComponent, CartoesFormComponent,
        CategoriasComponent, CategoriasFormComponent,
        ContasComponent, ContasFormComponent,
        MovimentacoesFormComponent,
    ],
    entryComponents: [
        CartoesFormComponent,
        CategoriasFormComponent,
        ContasFormComponent,
        MovimentacoesFormComponent,
    ],
    providers: [
        CartoesApiService,
        CategoriasApiService,
        ContasApiService,
        MovimentacoesApiService,
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
