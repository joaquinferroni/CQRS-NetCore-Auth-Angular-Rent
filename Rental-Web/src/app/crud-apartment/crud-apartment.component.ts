import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {  FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable,  Subject } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { ApartmentFilter, ApartmentModel } from '../models/apartment';
import { ApartmentService } from '../services/apartment.service';

@Component({
  selector: 'app-crud-apartment',
  templateUrl:'./crud-apartment.component.html' ,
  styleUrls: ['./crud-apartment.component.css'  ]
})
export class CrudApartmentComponent implements OnInit {
  @Input() apartments: ApartmentModel[] = [];
  @Input() reloadEvent!: Observable<ApartmentModel[]>;
  @Output() onSave = new EventEmitter<ApartmentModel>();
  @Output() onDelete = new EventEmitter<ApartmentModel>();
  apartMentsFiltered$!: ApartmentModel[];
  currentModel: ApartmentModel = new ApartmentModel(); 
  addMarkerSubject: Subject<any> = new Subject<any>();
  formGroup!: FormGroup;
  searchFilter:string='';
  constructor(private apartmentService: ApartmentService) { }

  ngOnInit(): void {
    this.loadFormGroup();
    if(this.reloadEvent)
      this.reloadEvent.subscribe(data => {
        this.apartments = data;
        this.search('');
      });
  }

  search(text: string) {
    this.apartMentsFiltered$ = this.apartments.filter(a => {
      const term = text.toLowerCase();
      return a.name.toLowerCase().includes(term)
          || a.description.includes(term)
          || a.userName.includes(term);
    });
  }

  loadFormGroup(){
    this.formGroup = new FormGroup({
      Name: new FormControl('', [
        Validators.required,
      ]),
      Description: new FormControl('', [
        Validators.required,
        
      ]),
      Floor: new FormControl('', [
        Validators.required,
        Validators.min(0)
      ]),
      Size: new FormControl('', [
        Validators.required,
        Validators.min(1)
      ]),
      Price: new FormControl('', [
        Validators.required,
        Validators.min(1)
      ]),
      Rooms: new FormControl('', [
        Validators.required,
        Validators.min(1),
      ])
      
    });
  }


  addLatLong(event:google.maps.LatLngLiteral){
    this.currentModel.latitude = event.lat;
    this.currentModel.longitude = event.lng;
  }

  save(){
    if(this.currentModel.latitude === 0 && this.currentModel.longitude === 0){
      alert("please select a valid location on map");
      return;
    }
    this.onSave.emit(this.currentModel);
    this.cleanFields();
  }

  delete(model:ApartmentModel){
    this.onDelete.emit(model);
  }

  edit(model:ApartmentModel){
    this.currentModel = Object.assign({}, model);;
    this.addMarkerSubject.next({});
    this.addMarkerSubject.next({
      lat: this.currentModel.latitude,
      lng: this.currentModel.longitude,
      title: this.currentModel.name
    });
    window.scroll(0,0);
  }

  cleanFields(){
    this.formGroup.reset();
    this.currentModel = new ApartmentModel();
    this.addMarkerSubject.next({});
  }

}
