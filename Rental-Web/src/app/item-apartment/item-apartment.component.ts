import { Component, Input, OnInit } from '@angular/core';
import { ApartmentModel } from '../models/apartment';

@Component({
  selector: 'app-item-apartment',
  templateUrl: './item-apartment.component.html' ,
  styleUrls: [ './item-apartment.component.css'
  ]
})
export class ItemApartmentComponent implements OnInit {
  @Input() apartment:ApartmentModel  =new ApartmentModel();
  constructor() { }

  ngOnInit(): void {
  }

}
function Import() {
  throw new Error('Function not implemented.');
}

