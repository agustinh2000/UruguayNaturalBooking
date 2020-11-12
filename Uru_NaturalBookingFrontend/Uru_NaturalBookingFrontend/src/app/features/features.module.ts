import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BarRatingModule } from 'ngx-bar-rating';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { RegionComponent } from './region/region.component';
import { RegionServiceService } from './services/region-service.service';
import { ReviewComponent } from './review/review.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { UserService } from './services/user.service';
import { ModifyUserComponent } from './modify-user/modify-user.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import { AddLodgingComponent } from './add-lodging/add-lodging.component';
import { DeleteUserComponent } from './delete-user/delete-user.component';
import { LodgingService } from './services/lodging.service';
import { TouristSpotService } from './services/tourist-spot.service';
import { NgbModule, NgbRating, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '../shared/shared.module';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import { AddTouristSpotComponent } from './add-tourist-spot/add-tourist-spot.component';
import { TableComponent } from './table/table.component';
import { MatTableModule } from '@angular/material/table';
import { ReportService } from './services/report.service';
import { ReportComponent } from './report/report.component';
import { FormForReportComponent } from './form-for-report/form-for-report.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatInputModule} from '@angular/material/input';

@NgModule({
  declarations: [RegionComponent, ReviewComponent, LoginComponent, RegisterUserComponent, ModifyUserComponent,
    DeleteUserComponent, AddLodgingComponent],
  declarations: [RegionComponent, ReviewComponent, LoginComponent, RegisterUserComponent,
     ModifyUserComponent, DeleteUserComponent, AddTouristSpotComponent, TableComponent, ReportComponent,
      FormForReportComponent],
  imports: [
    CommonModule,
    FlexLayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatSidenavModule,
    MatSelectModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    BarRatingModule,
    MatTableModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule
  ],
  exports: [RegionComponent, ReviewComponent, LoginComponent, RegisterUserComponent, ModifyUserComponent, DeleteUserComponent],
  providers: [RegionServiceService, UserService]
  exports: [RegionComponent, ReviewComponent, LoginComponent, RegisterUserComponent,
    ModifyUserComponent, DeleteUserComponent, AddTouristSpotComponent, TableComponent, ReportComponent,
    FormForReportComponent],
  providers: [RegionServiceService, UserService, ReportService]
})

export class FeaturesModule { }
