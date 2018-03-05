import { Component, OnInit } from '@angular/core';
import { Configurations } from '../shared/configurations.service';
import { FirebaseAppConfig } from 'angularfire2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-configure',
  templateUrl: './configure.component.html',
  styleUrls: ['./configure.component.scss']
})
export class ConfigureComponent implements OnInit {

  configuration: FirebaseAppConfig;

  constructor(private router: Router) { }

  ngOnInit() {
    this.configuration = Configurations.getFirebaseAppConfig() || {};
  }

  form_submit() {
    Configurations.setFirebaseAppConfig(this.configuration);
    alert('Configurations saved!');

    location.href = document.getElementsByTagName('base')[0].href;

    // this.router.navigateByUrl('/');
  }

}
