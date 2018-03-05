import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AngularFireModule } from 'angularfire2';
import { AngularFireDatabaseModule } from 'angularfire2/database';

import { Configurations } from '../shared/configurations.service';

import { IndexComponent } from './index/index.component';


@NgModule({
  imports: [
    CommonModule,
    AngularFireModule.initializeApp(Configurations.getFirebaseAppConfig()),
    AngularFireDatabaseModule,
  ],
  declarations: [IndexComponent]
})
export class AurumModule { }
