import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { GoogleMapsModule } from '@angular/google-maps';
import { FormsModule , ReactiveFormsModule} from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgBootstrapFormValidationModule } from 'ng-bootstrap-form-validation';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { httpInterceptorProviders } from './services/httpInterceptorProviders';
import { UserComponent } from './user/user.component';
import { CrudApartmentComponent } from './crud-apartment/crud-apartment.component';
import { AdminManagementApartmentComponent } from './admin-management-apartment/admin-management-apartment.component';
import { MapComponent } from './map/map.component';
import { MyApartmentsComponent } from './my-apartments/my-apartments.component';
import { FilterApartmentComponent } from './filter-apartment/filter-apartment.component';
import { SearchApartmentsComponent } from './search-apartments/search-apartments.component';
import { ItemApartmentComponent } from './item-apartment/item-apartment.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    UserComponent,
    CrudApartmentComponent,
    AdminManagementApartmentComponent,
    MapComponent,
    MyApartmentsComponent,
    FilterApartmentComponent,
    SearchApartmentsComponent,
    ItemApartmentComponent
  ],
  imports: [
    BrowserModule,
    GoogleMapsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    NgBootstrapFormValidationModule.forRoot(),
    AppRoutingModule
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
