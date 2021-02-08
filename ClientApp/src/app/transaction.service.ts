import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  constructor(private http: HttpClient) { }

  getTransactions():Observable<any[]>{
    return this.http.get<any>('/api/transaction')
  }

  getAccountBalance():Observable<number>{
    return this.http.get<number>('/api/transaction/account-balance')
  }
}
