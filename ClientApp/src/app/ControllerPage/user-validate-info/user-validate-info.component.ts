import { Component, OnInit, Input } from '@angular/core';
import { UserModel } from '../UserModel';
import { ValidateModel } from '../ValidateModel';
import { ConnectionService } from 'src/app/connection.service';

@Component({
  selector: 'app-user-validate-info',
  templateUrl: './user-validate-info.component.html',
  styleUrls: ['./user-validate-info.component.css']
})
export class UserValidateInfoComponent implements OnInit {
  @Input() user: UserModel;
  
  constructor(private connectionService: ConnectionService) { }

  Feedback:string;
  ngOnInit() {
  }

  DenyClick(){
    let data = new ValidateModel();
    data.id = this.user.Id;
    data.option = "Denied";
    this.connectionService.validateUser(data).subscribe((res)=>{
      console.log(res);
      this.Feedback=res;
      this.user.Validation = "Invalid";
    });
  }
  ApproveClick(){
    let data = new ValidateModel();
    data.id = this.user.Id;
    data.option = "Accepted";
    this.connectionService.validateUser(data).subscribe((res)=>{
      console.log(res);
      this.Feedback = res;
      this.user.Validation = "Valid";
    });
  }

}
