import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { TicketModel } from '../TicketModel';
import { ConnectionService } from 'src/app/connection.service';

@Component({
  selector: 'app-check-ticket',
  templateUrl: './check-ticket.component.html',
  styleUrls: ['./check-ticket.component.css']
})
export class CheckTicketComponent implements OnInit {

  ticketForm = this.fb.group({
    TicketID: ['',Validators.required]
  });
  
  selectedTicket: TicketModel;
  constructor(private fb: FormBuilder, private connectionService: ConnectionService) { }

  ngOnInit() {
  }

  CheckTicket(){
    this.connectionService.CheckTicket(this.ticketForm.value).subscribe((res) =>
    {
      this.selectedTicket = res;
    })
  }

}
