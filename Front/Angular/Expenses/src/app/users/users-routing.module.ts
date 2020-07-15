import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { UsersComponent } from './components/users.component';

const routes: Routes = [
    {
        path: '',
        component: UsersComponent
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes),
    ],
    exports: [
        RouterModule
    ]
})
export class UsersRoutingModule { }
