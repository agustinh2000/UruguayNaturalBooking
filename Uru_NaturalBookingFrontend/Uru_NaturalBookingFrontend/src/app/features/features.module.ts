import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
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
import { MatSelectModule } from '@angular/material/select';
import { AddLodgingComponent } from './add-lodging/add-lodging.component';
import { DeleteUserComponent } from './delete-user/delete-user.component';
import { LodgingService } from './services/lodging.service';
import { TouristSpotService } from './services/tourist-spot.service';
import { NgbModule, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '../shared/shared.module';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { AddTouristSpotComponent } from './add-tourist-spot/add-tourist-spot.component';
import { TableComponent } from './table/table.component';
import { MatTableModule } from '@angular/material/table';
import { ReportService } from './services/report.service';
import { ReportComponent } from './report/report.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { CategoryService } from './services/category.service';
import { ReserveService } from './services/reserve.service';
import { FormCommentaryComponent } from './form-commentary/form-commentary.component';
import { ReviewService } from './services/review.service';
import { ModifyReserveComponent } from './modify-reserve/modify-reserve.component';
import { FormModifyReserveComponent } from './form-modify-reserve/form-modify-reserve.component';
import { ModifyLodgingCapacityComponent } from './modify-lodging-capacity/modify-lodging-capacity.component';
import { TouristSpotViewComponent } from './tourist-spot-view/tourist-spot-view.component';
import { SelectionOfTouristSpotComponent } from './selection-of-tourist-spot/selection-of-tourist-spot.component';
import { LodgingCardsComponent } from './lodging-cards/lodging-cards.component';
import { BookingFormComponent } from './booking-form/booking-form.component';
import { RouterModule } from '@angular/router';
import { MatMenuModule } from '@angular/material/menu';
import { LodgingDetailComponent } from './lodging-detail/lodging-detail.component';
import { CreateReserveComponent } from './create-reserve/create-reserve.component';
import { TouristsPipe } from './tourists.pipe';
import { ReserveConfirmationComponent } from './reserve-confirmation/reserve-confirmation.component';
import { SearchOfLodgingsService } from './services/search-of-lodgings.service';
import { ImporterComponent } from './importer/importer.component';

@NgModule({
  declarations: [
    RegionComponent,
    ReviewComponent,
    LoginComponent,
    RegisterUserComponent,
    ModifyUserComponent,
    DeleteUserComponent,
    AddTouristSpotComponent,
    TableComponent,
    ReportComponent,
    AddLodgingComponent,
    FormCommentaryComponent,
    ModifyReserveComponent,
    FormModifyReserveComponent,
    ModifyLodgingCapacityComponent,
    TouristSpotViewComponent,
    SelectionOfTouristSpotComponent,
    LodgingCardsComponent,
    BookingFormComponent,
    LodgingDetailComponent,
    CreateReserveComponent,
    TouristsPipe,
    ReserveConfirmationComponent,
    ImporterComponent,
  ],
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
    MatTableModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    NgbModule,
    FormsModule,
    NgbRatingModule,
    SharedModule,
    MatSlideToggleModule,
    MatToolbarModule,
    MatSidenavModule,
    RouterModule,
    MatMenuModule,
  ],
  exports: [
    RegionComponent,
    ReviewComponent,
    LoginComponent,
    RegisterUserComponent,
    ModifyUserComponent,
    DeleteUserComponent,
    AddTouristSpotComponent,
    TableComponent,
    ReportComponent,
    AddLodgingComponent,
    FormCommentaryComponent,
    ModifyReserveComponent,
    FormModifyReserveComponent,
    ModifyLodgingCapacityComponent,
    TouristSpotViewComponent,
    SelectionOfTouristSpotComponent,
    LodgingCardsComponent,
    BookingFormComponent,
    LodgingDetailComponent,
    CreateReserveComponent,
    TouristsPipe,
    ReserveConfirmationComponent,
    ImporterComponent,
  ],
  providers: [
    RegionServiceService,
    UserService,
    ReportService,
    TouristSpotService,
    CategoryService,
    LodgingService,
    ReserveService,
    ReviewService,
    SearchOfLodgingsService,
  ],
})
export class FeaturesModule {}
