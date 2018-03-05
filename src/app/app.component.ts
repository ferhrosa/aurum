import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Configurations } from './shared/configurations.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private router: Router) {
    if (!Configurations.isConfigured()) {
      this.router.navigateByUrl('/configure');
    }
  }

}
