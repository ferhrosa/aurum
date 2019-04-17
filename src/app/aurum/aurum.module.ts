import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AngularFireModule } from '@angular/fire';
import { AngularFireDatabaseModule } from '@angular/fire/database';

import {
  MatSidenavModule, MatListModule, MatTabsModule, MatTableModule,
  MatButtonModule, MatCardModule, MatIconModule, MatInputModule,
  MatDatepickerModule, MatNativeDateModule, MAT_DATE_LOCALE,
  MatSelectModule,
} from '@angular/material';

import { Configurations } from '../shared/configurations.service';

import { IndexComponent } from './index/index.component';
import { LayoutComponent } from './layout/layout.component';
import { ResumoComponent } from './resumo/resumo.component';


@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    BrowserAnimationsModule,
    AngularFireModule.initializeApp(Configurations.getFirebaseAppConfig()),
    AngularFireDatabaseModule,
    MatSidenavModule, MatListModule, MatTabsModule, MatTableModule,
    MatButtonModule, MatCardModule, MatIconModule, MatInputModule,
    MatDatepickerModule, MatNativeDateModule, MatSelectModule,
  ],
  declarations: [
    IndexComponent,
    LayoutComponent,
    ResumoComponent,
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' },
  ],
})
export class AurumModule { }
