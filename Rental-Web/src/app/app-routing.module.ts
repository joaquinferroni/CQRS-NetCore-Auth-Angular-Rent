import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminManagementApartmentComponent } from './admin-management-apartment/admin-management-apartment.component';
import { AuthGuard } from './auth.guard';
import { LoginComponent } from './login/login.component';
import { MyApartmentsComponent } from './my-apartments/my-apartments.component';
import { SearchApartmentsComponent } from './search-apartments/search-apartments.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'search', component: SearchApartmentsComponent },
  { path: 'user', component: UserComponent,
    canActivate: [AuthGuard],
    data: {
      role: 'ADMIN'
    }
  },
  { path: 'apartments-management', component: AdminManagementApartmentComponent,
    canActivate: [AuthGuard],
    data: {
      role: 'ADMIN'
    }
  },
  { path: 'manage-my-apartments', component: MyApartmentsComponent,
    canActivate: [AuthGuard],
    data: {
      role: 'ADMIN, REALTOR'
    }
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
