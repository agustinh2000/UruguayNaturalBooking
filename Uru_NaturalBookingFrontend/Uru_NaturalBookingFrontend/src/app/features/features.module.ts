import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';
import {MatSidenavModule} from '@angular/material/sidenav';
import { RegionComponent } from './region/region.component';
import { RegionServiceService } from './services/region-service.service';
import { ReviewComponent } from './review/review.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { RegisterUserComponent } from './register-user/register-user.component';


@NgModule({
  declarations: [RegionComponent, ReviewComponent, LoginComponent, RegisterUserComponent],
  imports: [
    CommonModule,
    FlexLayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatSidenavModule,
    BrowserAnimationsModule,
    ReactiveFormsModule
  ],
  exports: [RegionComponent, ReviewComponent, LoginComponent, RegisterUserComponent],
  providers: [RegionServiceService]
})

export class FeaturesModule { }
