import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, ValidatorFn } from '@angular/forms';
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
      reserveSelected: new FormControl('', [Validators.required, this.noWhitespaceValidator])
    });
  }

  ngOnInit(): void {
  }

  reserveExist(reserveId: string): boolean {
    return this.reserveService.ReserveExist(reserveId);
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  }

  openCommentaryForm(): void {
    if (this.reserveExist(this.formGroup.controls.reserveSelected.value)) {
      this.isShown = true;
    }
  }
}
