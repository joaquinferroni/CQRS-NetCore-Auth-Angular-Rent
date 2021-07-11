import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  template: `
    <h1 class="welcome-title">
      Welcome!!!
    </h1>
  `,
  styleUrls: ['./register.component.css'  ]
})
export class RegisterComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
