import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserModel } from '../models/UserModel';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user',
  templateUrl:'./user.component.html',
  styleUrls: ['./user.component.css'
  ]
})
export class UserComponent implements OnInit {
  me:string = localStorage.getItem('userName') ||'';
  users: UserModel[] = [];
  currentModel: UserModel = new UserModel(0,'','USER'); 
  formGroup!: FormGroup;
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.loadUsers();
    this.formGroup = new FormGroup({
      UserName: new FormControl('', [
        Validators.required,
      ]),
      Name: new FormControl('', [
        Validators.required,
      ]),
      Password: new FormControl('', [
        Validators.required,
      ]),
    });
  }

  loadUsers(){
    this.userService.getAll().subscribe(data=>{
      this.users = data;
    })
  }

  edit(user:UserModel){
    this.currentModel = Object.assign({}, user);
    this.currentModel.password = '.';
  }

  save(){
    if(!this.currentModel.id) this.performSave();
    else this.performUpdate();
  }
  performSave(){
    this.userService.insert(this.currentModel).subscribe(data=>{
      this.loadUsers();
      this.cleanFields();
      alert("user added correctly");
    })
  }

  performUpdate(){
    this.userService.update(this.currentModel).subscribe(data=>{
      this.loadUsers();
      this.cleanFields();
      alert("user modified correctly");
    })
  }

  performDelete(userName:string){
    this.userService.delete(userName).subscribe(_=>{
      this.loadUsers();
      this.cleanFields();
      alert("user deleted correctly");
    })
  }

  cleanFields(){
    this.currentModel = new UserModel(0,'','USER');
    this.formGroup.reset();
  }

}
