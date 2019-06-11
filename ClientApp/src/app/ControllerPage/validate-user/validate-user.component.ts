import { Component, OnInit } from '@angular/core';
import { UserModel } from '../UserModel';
import { ConnectionService } from 'src/app/connection.service';

@Component({
  selector: 'app-validate-user',
  templateUrl: './validate-user.component.html',
  styleUrls: ['./validate-user.component.css']
})
export class ValidateUserComponent implements OnInit {

  users: UserModel[];
  selectedUser: UserModel;
  constructor(private connectionService: ConnectionService) { }

  ngOnInit() {
    this.fetchUsers();
  }

  fetchUsers(){
    this.connectionService.getAllUsers().subscribe((res)=>{
      console.log(res);
      this.users = res;
    });
  }

  onSelect(data: UserModel){
    this.selectedUser = data;
  }

}
