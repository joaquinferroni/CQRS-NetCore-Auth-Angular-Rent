import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { ApartmentFilter, ApartmentModel } from '../models/apartment';
import { ApartmentService } from '../services/apartment.service';

@Component({
  selector: 'app-crud-apartment',
  templateUrl:'./crud-apartment.component.html' ,
  styleUrls: ['./crud-apartment.component.css'  ]
})
export class CrudApartmentComponent implements OnInit {
  @Input() apartments: ApartmentModel[] = [];
  currentModel: ApartmentModel = new ApartmentModel(); 
  @Output() onSave = new EventEmitter<ApartmentModel>();
  @Output() onDelete = new EventEmitter<ApartmentModel>();
  addMarkerSubject: Subject<any> = new Subject<any>();
  formGroup!: FormGroup;
  constructor(private apartmentService: ApartmentService) { }

  ngOnInit(): void {
    this.loadFormGroup();
    this.loadApartments();
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
        Validators.min(1)
      ])
      
    });
  }

  loadApartments(){
    this.apartmentService.getAllAdmin(new ApartmentFilter()).subscribe(data=>{
      this.apartments = data;
      console.table(data);
    })
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
  }

  cleanFields(){
    this.formGroup.reset();
    this.currentModel = new ApartmentModel();
    this.addMarkerSubject.next({});
  }

}
