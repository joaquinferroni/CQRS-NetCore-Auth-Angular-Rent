import { Component, OnInit } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { Router } from '@angular/router';
import { UserModel } from './models/UserModel';

@Component({
  selector: 'app-root',
  templateUrl:'./app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Rental-Web';
  currentUser: UserModel = new UserModel(0,'','');
  constructor(
    private router: Router
    ){}
  ngOnInit(): void {
    let token = localStorage.getItem('token');
    this.suscribeLogin();
    if(!token) {
      this.initLogin();
    }
    else {
      this.initUserLogged();
    }
  }

  initLogin(){   
    localStorage.removeItem('token');
    this.currentUser = new UserModel(0,'','');
    this.router.navigate(['login']);
  }

  initUserLogged(){   
    this.currentUser = new UserModel(0,localStorage.getItem('userName')||'',localStorage.getItem('userRole')||'');
    //this.router.navigate(['home']);
  }

  logout(){
    this.initLogin();
  }

  suscribeLogin(){
    LoginComponent.logginSuccess.subscribe(value => {
        if(value.userName !== ''){
          this.currentUser = value;
        }
    });
  }
}
