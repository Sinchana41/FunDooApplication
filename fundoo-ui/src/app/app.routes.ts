import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Signup } from './pages/signup/singup';
import { DashboardComponent } from './pages/dashboard/dashboard';

export const routes: Routes = [
    {path:'',redirectTo:'login',pathMatch:'full'},
    {path:'login',component:Login},
    {path:'signup',component:Signup},
    // {path: 'dashboard', loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.DashboardComponent) },
    // {path: 'archive', loadComponent: () => import('./pages/archive/archive').then(m => m.Archive) },
    // {path: 'trash', loadComponent: () => import('./pages/trash/trash').then(m => m.Trash) }
    {path:'dashboard',component:DashboardComponent}

];
