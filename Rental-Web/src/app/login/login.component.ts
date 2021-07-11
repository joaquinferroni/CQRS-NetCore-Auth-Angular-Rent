import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import { Auth } from '../models/auth';
import { AuthService } from '../services/auth.service';
import jwt_decode from 'jwt-decode';
import { Router } from '@angular/router';
import { UserModel } from '../models/UserModel';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl:'./login.component.html',
  styleUrls:['./login.component.css']
})
export class LoginComponent implements OnInit {
  public static logginSuccess = new BehaviorSubject<UserModel>(new UserModel(0,'',''));
  formGroup!: FormGroup;
  authModel!: Auth;
  constructor(public authService: AuthService
    ,private router: Router
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
}
