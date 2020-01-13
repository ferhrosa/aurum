import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { AngularFireModule } from '@angular/fire';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { AngularFireDatabaseModule } from '@angular/fire/database';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule, MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';

import { tokens } from 'src/environments/tokens';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';

import { IndexComponent } from './index/index.component';
import { ResumoComponent } from './resumo/resumo.component';
import { TransactionComponent } from './transaction/transaction.component';

import { TransactionService } from './shared/service/transaction.service';


@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    ResumoComponent,
    TransactionComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    CommonModule,
    RouterModule,
    // Angular Fire (Firebase)
    AngularFireModule.initializeApp(tokens.firebase),
    AngularFireAuthModule, AngularFireDatabaseModule,
    // Angular Material
    MatSidenavModule, MatListModule, MatTabsModule, MatTableModule,
    MatButtonModule, MatCardModule, MatDialogModule, MatIconModule,
    MatInputModule, MatDatepickerModule, MatNativeDateModule, MatSelectModule,
  ],
  entryComponents: [
    TransactionComponent,
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' },
    { provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: { disableClose: true } },
    TransactionService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
