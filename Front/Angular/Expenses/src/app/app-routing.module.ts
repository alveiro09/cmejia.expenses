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
      canActivate: [AdminGuard],
      loadChildren: () => import('./expenses/expenses.module').then(m => m.ExpensesModule)
    },
    {
      path: 'expensesList',
      canActivate: [AdminGuard],
      loadChildren: () => import('./expenses-list/expenses-list.module').then(m => m.ExpensesListModule)
    },
  ]
},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
