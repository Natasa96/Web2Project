import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-buy-ticket',
  templateUrl: './buy-ticket.component.html',
  styleUrls: ['./buy-ticket.component.css']
})
export class BuyTicketComponent implements OnInit {

  TicketPriceForm = this.fb.group({
    
  })
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
  }

}
