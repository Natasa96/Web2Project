import { Component, OnInit, ViewChild, ElementRef, Input } from '@angular/core';
import { ConnectionService } from 'src/app/connection.service';
import { BuyTicketModel } from '../BuyTicketModel';

declare var paypal;

@Component({
  selector: 'app-paypal',
  templateUrl: './paypal.component.html',
  styleUrls: ['./paypal.component.css']
})
export class PaypalComponent implements OnInit {
  Feedback:string;
  @ViewChild('paypal') paypalElement: ElementRef;
  constructor(private connectionService: ConnectionService) { }
  @Input() price: any;
  @Input() type: any;
  ngOnInit() {
    paypal
      .Buttons({
        createOrder: (data, actions) => {
          return actions.order.create({
            purchase_units:[
              {
                description: "ticket: " + this.type,
                amount:{
                  currency_code: 'EUR',
                  value: Math.round(this.price / 118)
                }
              }
            ]
          })
        },
        onApprove: async (data, actions) => {
          const order = await actions.order.capture();
          console.log(order);
          this.PurchaseTicket(order);
        },
        onError: err => {
          console.log(err);
        }
      })
      .render(this.paypalElement.nativeElement)
  }


  private async PurchaseTicket(order: any) {
    var Shipping = order.purchase_units[0].shipping;
    let TicketData = new BuyTicketModel();
    TicketData.Price = this.price;
    TicketData.Type = this.type;
    TicketData.Id = order.id;
    TicketData.Address = Shipping.address.address_line_1 + ", " + Shipping.address.admin_area_2 + ", " + Shipping.address.country_code;
    TicketData.FullName = Shipping.name.full_name;
    TicketData.CreateTime = order.create_time;
    TicketData.UpdateTime = order.update_time;
    TicketData.Status = order.status;
    TicketData.PurchaseUnit = order.purchase_units[0].description;
    this.connectionService.buyTicket(TicketData).subscribe((result) => {
      console.log(result);
      this.Feedback = result;
    });
  }
}
