import { Component, OnInit } from '@angular/core';

import { AngularFireAuth } from '@angular/fire/auth';
import * as firebase from 'firebase';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  isAuthenticated: boolean = null;
  user: firebase.User;

  constructor(private afAuth: AngularFireAuth) {
  }

  ngOnInit() {
    this.afAuth.authState.subscribe(user => {
      if (user) {
        this.isAuthenticated = true;
        this.user = user;
      } else {
        this.isAuthenticated = false;
      }
    });
  }

  async logIn() {
    const provider = new firebase.auth.GoogleAuthProvider();

    try {
      await this.afAuth.auth.signInWithPopup(provider);
      this.isAuthenticated = true;
    } catch (err) {
      this.isAuthenticated = false;
    }
  }

  logOut() {
    this.afAuth.auth.signOut();
    this.isAuthenticated = false;
  }

}
