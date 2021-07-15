import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { ApartmentFilter, ApartmentModel } from '../models/apartment';
import { ApartmentService } from '../services/apartment.service';

@Component({
  selector: 'app-my-apartments',
  templateUrl:'./my-apartments.component.html' ,
  styleUrls: ['./my-apartments.component.css'  ]
})
export class MyApartmentsComponent implements OnInit {
  reloadData: Subject<ApartmentModel[]> = new Subject<ApartmentModel[]>();
  apartments: ApartmentModel[] = [];
  currentModel: ApartmentModel = new ApartmentModel(); 
  constructor(private apartmentService: ApartmentService) { }

  ngOnInit(): void {
    this.loadMyApartments();
  }

  loadMyApartments(){
    this.apartmentService.getMine().subscribe(data=>{
      this.apartments = data;
      this.reloadData.next(data);
    })
  }

  save(model:ApartmentModel){
 
  if(!model.id ) this.performSave(model);
  else this.performUpdate(model);
  }

  performSave(model:ApartmentModel){
    this.apartmentService.insert(model).subscribe(data=>{
      this.loadMyApartments();
      alert("apartment saved");
    })
  }
  performUpdate(model:ApartmentModel){
    this.apartmentService.update(model).subscribe(data=>{
      this.loadMyApartments();
      alert("apartment updated");
    })
  }
  delete(model:ApartmentModel){
    this.apartmentService.delete(model.id).subscribe(data=>{
      this.loadMyApartments();
      alert("apartment deleted");
    })
  }

}
