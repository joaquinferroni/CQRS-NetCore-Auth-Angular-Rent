import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserModel } from '../models/UserModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getAll():Observable<UserModel[]>{
    return this.http.get<UserModel[]>('/user');
  }
  update(user: UserModel):Observable<UserModel>{
    return this.http.put<UserModel>('/user/'+user.id,user);
  }
  insert(user: UserModel):Observable<UserModel>{
    return this.http.post<UserModel>('/user',user);
  }
  delete(userName:string):Observable<void>{
    return this.http.delete<void>('/user/'+userName);
  }
}
