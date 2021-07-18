import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import { Auth } from '../models/auth';
import { AuthService } from '../services/auth.service';
import jwt_decode from 'jwt-decode';
import { Router } from '@angular/router';
import { UserModel } from '../models/UserModel';
import { BehaviorSubject } from 'rxjs';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl:'./login.component.html',
  styleUrls:['./login.component.css']
})
export class LoginComponent implements OnInit {
  public static logginSuccess = new BehaviorSubject<UserModel>(new UserModel(0,'',''));
  formGroup!: FormGroup;
  formGroupRegister!: FormGroup;
  authModel!: Auth;
  registerModel: UserModel = new UserModel(0,'','USER');
  viewRegister:boolean = false;
  constructor(
    public authService: AuthService
    ,public userService:UserService
    ,private router: Router
    ,private formBuilder: FormBuilder
    
    ) { 
      localStorage.removeItem('token');
      localStorage.removeItem('userName');
      localStorage.removeItem('userRole');
    }

  ngOnInit(): void {
    this.authModel  =new Auth('','');
    this.formGroup = new FormGroup({
      UserName: new FormControl('', [
        Validators.required,
      ]),
      Password: new FormControl('', [
        Validators.required,
      ])
    });
    this.formGroupRegister = this.formBuilder.group({
      UserName: new FormControl('', [
        Validators.required,
      ]),
      Name: new FormControl('',[
        Validators.required
      ]),
      Password: new FormControl('', [
        Validators.required,
      ]),
      ConfirmPassword: new FormControl('', [Validators.required])
    },{
      validator: this.mustMatch('Password', 'ConfirmPassword')
    });
  }

  login(){
    this.authService.auth(this.authModel).subscribe(data=>{
      
      let info  = jwt_decode(data.token) as any;
      localStorage.setItem('userName',this.authModel.userName);
      localStorage.setItem('userRole',info.role);
      localStorage.setItem('token','Bearer '+ data.token);
      LoginComponent.logginSuccess.next(new UserModel(0,this.authModel.userName,info.role));
      this.router.navigate(['search']);
    });
  }

  register(){
    this.userService.insert(this.registerModel).subscribe(data=>{
      this.viewRegister = false;
      this.formGroupRegister.reset();
      alert("User registered correctly!!");
      this.registerModel = new UserModel(0,'','USER');
    });
  }

  mustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
        const control = formGroup.controls[controlName];
        const matchingControl = formGroup.controls[matchingControlName];

        if (matchingControl.errors && !matchingControl.errors.mustMatch) {
            return;
        }
        if (control.value !== matchingControl.value) {
            matchingControl.setErrors({ mustMatch: true });
        } else {
            matchingControl.setErrors(null);
        }
    }
}
}
