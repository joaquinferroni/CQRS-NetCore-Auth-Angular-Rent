import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApartmentFilter, ApartmentModel } from '../models/apartment';

@Injectable({
  providedIn: 'root'
})
export class ApartmentService {

  constructor(private http: HttpClient) { }

  getAllAdmin(filter:ApartmentFilter):Observable<ApartmentModel[]>{
    const options =   { 
      params: new HttpParams().set('status', filter.status)
                              .set('sizeFrom',!filter.sizeFrom ?'':filter.sizeFrom ) 
                              .set('sizeTo',!filter.sizeTo?'':filter.sizeTo) 
                              .set('priceFrom',!filter.priceFrom ? '': filter.priceFrom) 
                              .set('priceTo',!filter.priceTo ?'': filter.priceTo) 
                              .set('roomsFrom',!filter.roomsFrom ? '':filter.roomsFrom) 
                              .set('roomsTo',!filter.roomsTo ? '':filter.roomsTo) 

    };
    return this.http.get<ApartmentModel[]>('/apartments/all',options);
  }

  get(filter:ApartmentFilter):Observable<ApartmentModel[]>{
    const options =   { 
      params: new HttpParams().set('sizeFrom',!filter.sizeFrom ?'':filter.sizeFrom ) 
                              .set('sizeTo',!filter.sizeTo?'':filter.sizeTo) 
                              .set('priceFrom',!filter.priceFrom ? '': filter.priceFrom) 
                              .set('priceTo',!filter.priceTo ?'': filter.priceTo) 
                              .set('roomsFrom',!filter.roomsFrom ? '':filter.roomsFrom) 
                              .set('roomsTo',!filter.roomsTo ? '':filter.roomsTo) 

    };
    return this.http.get<ApartmentModel[]>('/apartments',options);
  }

  getMine():Observable<ApartmentModel[]> {
   return this.http.get<ApartmentModel[]>('/user/apartments'); 
  }

  update(apartment: ApartmentModel):Observable<ApartmentModel>{
    return this.http.put<ApartmentModel>('/apartments/'+apartment.id,apartment);
  }
  insert(apartment: ApartmentModel):Observable<ApartmentModel>{
    return this.http.post<ApartmentModel>('/apartments',apartment);
  }
  delete(id:number):Observable<void>{
    return this.http.delete<void>('/apartments/'+id);
  }
}
