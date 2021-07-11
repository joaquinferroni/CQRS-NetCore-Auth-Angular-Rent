import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ApartmentFilter } from '../models/apartment';

@Component({
  selector: 'app-filter-apartment',
  templateUrl: './filter-apartment.component.html',
  styleUrls: ['./filter-apartment.component.css']
})
export class FilterApartmentComponent implements OnInit {
  @Output() doSearch = new EventEmitter<ApartmentFilter>();
  filter:ApartmentFilter = new ApartmentFilter();
  constructor() { }

  ngOnInit(): void {
  }

  search(){
    this.doSearch.emit(this.filter);
  }

}
