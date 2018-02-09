import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { routing } from './app.routing';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { AppConfig } from './app.config';

import { AuthGuard } from './_guards/index';
import { AppComponent } from './app.component';
import { AuthenticationService } from './_services/index';
import { AuthComponent } from './auth/auth.component';
import { HomeComponent } from './home/home.component';



@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    routing
  ],
  providers: [AppConfig, AuthGuard, AuthenticationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
