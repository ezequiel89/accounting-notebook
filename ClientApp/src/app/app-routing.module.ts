import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {TransactionComponent} from './transaction/transaction.component'

const routes: Routes = [
  {path: 'transaction', component:TransactionComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
