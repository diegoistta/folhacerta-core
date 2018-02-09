import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/index';
import { AuthComponent } from './auth/index';
import { AuthGuard } from './_guards/index';


const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: AuthComponent },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
