import { Component, OnInit } from '@angular/core';
import { TransactionService } from '../transaction.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {

  constructor(private service:TransactionService) { }

  TransactionHistoryList: any[];
  balance: number = 0;

  ngOnInit(): void {
    this.getTransactionHistory();
    this.getAccountBalance();
  }  

  getTransactionHistory(){
    this.service.getTransactions().subscribe(data =>{
      this.TransactionHistoryList=data;  
    });
  }    

  getAccountBalance(){
    this.service.getAccountBalance().subscribe(data =>{      
      this.balance=data;  
    });
  } 

  handleClick(event: Event) { 
    this.getTransactionHistory();
    this.getAccountBalance();  
  } 
}
