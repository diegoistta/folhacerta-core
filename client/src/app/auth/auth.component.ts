import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AuthenticationService } from '../_services/index';

import '../../assets/pages/scripts/login-5.js';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  model: any = {};
  loading = false;
  returnUrl: string;
  showForget: boolean;
  showLogin: boolean;

  constructor( private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) {
        this.showForget = false;
        this.showLogin = true;
     }

  ngOnInit() {

    // reset login status
    this.authenticationService.logout();

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  login() {
    this.loading = true;
    this.authenticationService.login(this.model.email, this.model.senha)
        .subscribe(
            data => {
                this.router.navigate([this.returnUrl]);
            },
            error => {
                // this.alertService.error('Username or password is incorrect');
                this.loading = false;
            });
}

  ShowForget(show: boolean) {
    this.showForget = show;
    this.showLogin = !show;
  }

}
