import { Component, OnInit } from '@angular/core';
import { ConnectionService } from 'src/app/connection.service';
import { PricelistModel } from 'src/app/AdminPage/PricelistModel';

@Component({
  selector: 'app-home-price-list',
  templateUrl: './home-price-list.component.html',
  styleUrls: ['./home-price-list.component.css']
})
export class HomePriceListComponent implements OnInit {

  constructor(private connectionService: ConnectionService) { }
  Pricelist: PricelistModel;
  ngOnInit() {
    this.getPricelist();
  }

  getPricelist(){
    this.connectionService.getAnonPricelist().subscribe((res) =>{
      this.Pricelist = res;
      console.log(this.Pricelist);
    });
  }

}
