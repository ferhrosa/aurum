import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { CommonModule, registerLocaleData } from '@angular/common';
import { RouterModule } from '@angular/router';

import ptBr from '@angular/common/locales/pt';

import { AngularFireModule } from '@angular/fire';
import { AngularFireAuthModule } from '@angular/fire/auth';
import { AngularFirestoreModule } from '@angular/fire/firestore';

import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule, MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';

import { tokens } from 'src/environments/tokens';

import { CustomCurrencyPipe } from './shared/pipe/currency.pipe';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';

import { IndexComponent } from './index/index.component';
import { SummaryComponent } from './summary/summary.component';
import { TransactionComponent } from './transaction/transaction.component';

import { TransactionService } from './shared/service/transaction.service';


registerLocaleData(ptBr, 'pt');

@NgModule({
  declarations: [
    AppComponent,
    IndexComponent,
    SummaryComponent,
    TransactionComponent,
    CustomCurrencyPipe,
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
    AngularFireAuthModule, AngularFirestoreModule,
    // Angular Material
    MatSidenavModule, MatListModule, MatTabsModule, MatTableModule,
    MatButtonModule, MatButtonToggleModule, MatCardModule, MatDialogModule,
    MatIconModule, MatInputModule, MatDatepickerModule, MatNativeDateModule,
    MatRadioModule, MatSelectModule,
  ],
  entryComponents: [
    TransactionComponent,
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt' },
    { provide: MAT_DATE_LOCALE, useValue: 'pt' },
    { provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: { disableClose: true } },
    TransactionService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
