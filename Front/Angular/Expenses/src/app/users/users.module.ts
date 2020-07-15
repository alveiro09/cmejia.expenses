import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { UsersComponent } from './components/users.component';
import { MaterialModule } from './../material/material.module';

import { UsersRoutingModule } from './users-routing.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    declarations: [
        UsersComponent,
    ],
    imports: [
        CommonModule,
        SharedModule,
        UsersRoutingModule,
        ReactiveFormsModule,
        MaterialModule
    ]
})
export class UsersModule {

}
