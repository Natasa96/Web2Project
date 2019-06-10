import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ConnectionService } from 'src/app/connection.service';
import { Pricelist } from '../Pricelist';
import { BuyTicketModel } from '../BuyTicketModel';
import { TicketType } from '../TicketType';

@Component({
  selector: 'app-buy-ticket',
  templateUrl: './buy-ticket.component.html',
  styleUrls: ['./buy-ticket.component.css']
})
export class BuyTicketComponent implements OnInit {

  constructor(private fb: FormBuilder, private connectionService: ConnectionService) { }

  Pricelist: string[];
  selectedType;
  TicketPrice: number;

  ngOnInit() {
    this.getPricelist();
  }

  onChange(type){
    let data = new TicketType();
    data.Type = type;
    this.selectedType = type;
    this.connectionService.calculatePrice(data).subscribe((result) => {
      this.TicketPrice = result;
      console.log(this.TicketPrice);
    });
  }

  buyTicket(){
    let TicketData = new BuyTicketModel()
    TicketData.Price = this.TicketPrice;
    TicketData.Type = this.selectedType;
    this.connectionService.buyTicket(TicketData).subscribe((result) =>
    {
      console.log(result);
    });
  }

  getPricelist() {
    this.connectionService.getPricelist().subscribe((result) =>{
      this.Pricelist = result;
      console.log("start: " + this.Pricelist);
      let data = new TicketType();
      data.Type = this.Pricelist[0];
      this.connectionService.calculatePrice(data).subscribe((result) => {
        this.TicketPrice = result;
        console.log(this.TicketPrice);
    });
    });
  }

}
