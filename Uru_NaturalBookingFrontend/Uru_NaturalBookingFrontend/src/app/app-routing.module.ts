import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddLodgingComponent } from './features/add-lodging/add-lodging.component';
import { AddTouristSpotComponent } from './features/add-tourist-spot/add-tourist-spot.component';
import { DeleteUserComponent } from './features/delete-user/delete-user.component';
import { LoginComponent } from './features/login/login.component';
import { RegisterUserComponent } from './features/register-user/register-user.component';
import { ModifyLodgingCapacityComponent } from './features/modify-lodging-capacity/modify-lodging-capacity.component';
import { ModifyReserveComponent } from './features/modify-reserve/modify-reserve.component';
import { ModifyUserComponent } from './features/modify-user/modify-user.component';
import { RegionComponent } from './features/region/region.component';
import { ReportComponent } from './features/report/report.component';
import { ReviewComponent } from './features/review/review.component';
import { SelectionOfTouristSpotComponent } from './features/selection-of-tourist-spot/selection-of-tourist-spot.component';
import { RegionNotExistGuard } from './features/Guards/region-not-exist.guard';
import { TouristSpotNotExistGuard } from './features/Guards/Tourist-Spot-Not-Exist.guard';
import { BookingFormComponent } from './features/booking-form/booking-form.component';
import { LodgingDetailComponent } from './features/lodging-detail/lodging-detail.component';
import { LodgingNotExistGuard } from './features/Guards/lodging-not-exist.guard';
import { ReserveConfirmationComponent } from './features/reserve-confirmation/reserve-confirmation.component';
import { CreateReserveComponent } from './features/create-reserve/create-reserve.component';

const routes: Routes = [
  { path: 'add-lodging', component: AddLodgingComponent },
  { path: 'add-touristSpot', component: AddTouristSpotComponent },
  { path: 'delete-user', component: DeleteUserComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register-user', component: RegisterUserComponent },
  {
    path: 'modify-lodging-capacity',
    component: ModifyLodgingCapacityComponent,
  },
  { path: 'modify-reserve', component: ModifyReserveComponent },
  { path: 'modify-user', component: ModifyUserComponent },
  { path: 'regions', component: RegionComponent },
  { path: 'report', component: ReportComponent },
  { path: 'review', component: ReviewComponent },
  {
    path: 'touristSpots/:idRegion',
    component: SelectionOfTouristSpotComponent,
    canActivate: [RegionNotExistGuard],
  },
  {
    path: 'lodgings/:idTouristSpot/:idRegion',
    component: BookingFormComponent,
    canActivate: [TouristSpotNotExistGuard],
  },
  {
    path: 'lodging-detail',
    component: LodgingDetailComponent,
    canActivate: [LodgingNotExistGuard],
  },
  {
    path: 'create-reserve',
    component: CreateReserveComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
