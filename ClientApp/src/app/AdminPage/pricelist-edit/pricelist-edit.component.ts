import { Component, OnInit } from '@angular/core';
import { PricelistModel } from '../PricelistModel';
import { ConnectionService } from 'src/app/connection.service';

@Component({
  selector: 'app-pricelist-edit',
  templateUrl: './pricelist-edit.component.html',
  styleUrls: ['./pricelist-edit.component.css']
})
export class PricelistEditComponent implements OnInit {

  Pricelist: PricelistModel;
  constructor(private Service: ConnectionService) { }

  ngOnInit() {
    this.getPricelist();
  }

  getPricelist(){
    this.Service.getEditPricelist().subscribe((res) => {
      this.Pricelist = res;
      console.log(res);
    })
  }
  ChangePrices(){
    console.log(this.Pricelist.TicketPrice);
    this.Service.UpdatePricelist(this.Pricelist).subscribe((res) => {
      console.log(res)
    });
  }
  ChangeValue(item: any){
    this.Pricelist.TicketPrice[item.name] = item.value;
  }
}
