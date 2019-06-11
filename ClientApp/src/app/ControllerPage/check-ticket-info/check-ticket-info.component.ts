import { Component, OnInit, Input } from '@angular/core';
import { TicketModel } from '../TicketModel';

@Component({
  selector: 'app-check-ticket-info',
  templateUrl: './check-ticket-info.component.html',
  styleUrls: ['./check-ticket-info.component.css']
})
export class CheckTicketInfoComponent implements OnInit {

  constructor() { }

  @Input() ticket: TicketModel;

  ngOnInit() {
  }

}
