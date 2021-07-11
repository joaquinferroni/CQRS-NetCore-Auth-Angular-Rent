import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { ApartmentFilter, ApartmentModel } from '../models/apartment';
import { ApartmentService } from '../services/apartment.service';

@Component({
  selector: 'app-search-apartments',
  templateUrl: './search-apartments.component.html' ,
  styleUrls: ['./search-apartments.component.css'  ]
})
export class SearchApartmentsComponent implements OnInit {
  apartments:ApartmentModel[]=[];
  apartmentFilter: ApartmentFilter = new ApartmentFilter();
  setMarkerEvent: Subject<any> = new Subject<any>();
  constructor(private apartmentServire:ApartmentService) { }

  ngOnInit(): void {
    this.findApartments();
  }

  filterChange(event:ApartmentFilter){
    this.apartmentFilter = event;
    this.findApartments();
  }
  findApartments(){
    this.apartmentServire.get(this.apartmentFilter).subscribe(data=>{
      this.apartments = data;
      this.addMarkers();
    })
  }

  addMarkers(){
    this.setMarkerEvent.next({});
    this.apartments.forEach(a=>{
      this.setMarkerEvent.next({
        eventType:'setMarker',
        lat: a.latitude,
        lng: a.longitude,
        title: a.name
      });
    })
  }

  moveMarker($event:ApartmentModel){
      this.setMarkerEvent.next({
        eventType:'moveMarker',
        lat: $event.latitude,
        lng: $event.longitude,
      });
  }

}
