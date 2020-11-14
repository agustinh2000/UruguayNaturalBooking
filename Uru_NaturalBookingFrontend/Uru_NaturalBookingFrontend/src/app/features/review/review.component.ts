import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { TouristSpotModelForLodgingResponseModel } from 'src/app/models/TouristSpotModelForLodgingResponseModel';
import { ReserveService } from '../services/reserve.service';
import { TouristSpotService } from '../services/tourist-spot.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css']
})

export class ReviewComponent implements OnInit {

  public formGroup: FormGroup;

  private reserveService: ReserveService;

  isShown = false;

  constructor(aServiceOfReserves: ReserveService, private formBuilder: FormBuilder) {
    this.reserveService = aServiceOfReserves;
    this.formGroup = this.formBuilder.group({
      reserveSelected: new FormControl('', [Validators.required])
    });
  }

  ngOnInit(): void {
  }

  reserveExist(reserveId: string): boolean {
    return this.reserveService.ReserveExist(reserveId);
  }

  openCommentaryForm(): void {
    if (this.reserveExist(this.formGroup.controls.reserveSelected.value)) {
      this.isShown = true;
    }
  }
}
