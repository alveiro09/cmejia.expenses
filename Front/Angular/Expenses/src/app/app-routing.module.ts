import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';


import { AdminGuard } from '../app/admin.guard';

const routes: Routes = [{
  path: '',
  component: LayoutComponent,
  children: [
    {
      path: '',
      redirectTo: '/login',
      pathMatch: 'full'
    },
    {
      path: 'login',
      loadChildren: () => import('./login/login.module').then(m => m.LoginModule)
    },
    {
      path: 'users',
      loadChildren: () => import('./users/users.module').then(m => m.UsersModule)
    },
    {
      path: 'expenses',
      loadChildren: () => import('./expenses/expenses.module').then(m => m.ExpensesModule)
    },
  ]
},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
