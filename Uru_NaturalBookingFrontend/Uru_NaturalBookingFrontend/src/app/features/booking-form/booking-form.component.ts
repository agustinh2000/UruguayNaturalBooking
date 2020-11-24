import { Component, Input, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { ReserveService } from '../services/reserve.service';
import { SearchOfLodgingModelForRequest } from '../../models/SearchOfLodgingModelForRequest';
import { TouristSpotService } from '../services/tourist-spot.service';
import { ActivatedRoute } from '@angular/router';
import { TouristSpotModelForResponse } from 'src/app/models/TouristSpotModelForResponse';
import { SearchOfLodgingsService } from '../services/search-of-lodgings.service';

@Component({
  selector: 'app-booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.css'],
})
export class BookingFormComponent implements OnInit {
  public touristSpotIdSelected: string;

  public formGroup: FormGroup;

  public quantityOfAdults: number = 0;

  public quantityOfChilds: number = 0;

  public quantityOfBabies: number = 0;

  public quantityOfRetired: number = 0;

  public maxDate: Date;

  public showLodgingsAvailables: boolean = false;

  public searchModel: SearchOfLodgingModelForRequest;

  public nameOfTouristSpot: string;

  private touristSpotService: TouristSpotService;

  constructor(
    aTouristSpotService: TouristSpotService,
    private formBuilder: FormBuilder,
    private currentRoute: ActivatedRoute
  ) {
    this.touristSpotService = aTouristSpotService;
    this.formGroup = this.formBuilder.group(
      {
        checkInPicker: ['', Validators.required],
        checkOutPicker: ['', Validators.required],
      },
      { validator: this.DateValidation }
    );
    this.maxDate = new Date();
  }

  DateValidation: ValidatorFn = (fg: FormGroup) => {
    const start = fg.get('checkInPicker').value;
    const end = fg.get('checkOutPicker').value;
    return start !== null && end !== null && start <= end
      ? null
      : { range: true };
  };

  ngOnInit(): void {
    this.touristSpotIdSelected = this.currentRoute.snapshot.params[
      'idTouristSpot'
    ];
    this.touristSpotService
      .getTouristSpotById(this.touristSpotIdSelected)
      .subscribe(
        (res: TouristSpotModelForResponse) => {
          this.nameOfTouristSpot = res.name;
        },
        (err) => {
          alert(err.error);
        }
      );
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  };

  public plus(typeOfGuest: string): void {
    if (typeOfGuest === 'adults') {
      this.quantityOfAdults++;
    }
    if (typeOfGuest === 'childs') {
      this.quantityOfChilds++;
    }
    if (typeOfGuest === 'babies') {
      this.quantityOfBabies++;
    }
    if (typeOfGuest === 'retired') {
      this.quantityOfRetired++;
    }
  }

  public minus(typeOfGuest: string): void {
    if (typeOfGuest === 'adults' && this.quantityOfAdults > 0) {
      this.quantityOfAdults--;
    }
    if (typeOfGuest === 'childs' && this.quantityOfChilds > 0) {
      this.quantityOfChilds--;
    }
    if (typeOfGuest === 'babies' && this.quantityOfBabies > 0) {
      this.quantityOfBabies--;
    }
    if (typeOfGuest === 'retired' && this.quantityOfRetired > 0) {
      this.quantityOfRetired--;
    }
  }

  private formIsValid(): boolean {
    return this.formGroup.valid;
  }

  public isInvalidQuantityOfGuest(): boolean {
    return (
      this.quantityOfAdults === 0 &&
      this.quantityOfBabies === 0 &&
      this.quantityOfChilds === 0 &&
      this.quantityOfRetired === 0
    );
  }

  public searchOfLodgings(): void {
    if (this.formIsValid() && !this.isInvalidQuantityOfGuest()) {
      this.showLodgingsAvailables = true;
      this.searchModel = {
        CheckIn: this.formGroup.controls.checkInPicker.value,
        CheckOut: this.formGroup.controls.checkOutPicker.value,
        QuantityOfAdult: this.quantityOfAdults,
        QuantityOfBabies: this.quantityOfBabies,
        QuantityOfChilds: this.quantityOfChilds,
        QuantityOfRetireds: this.quantityOfRetired,
        TouristSpotIdSearch: this.touristSpotIdSelected,
      };
    }
  }

  public clearFields(): void {
    this.quantityOfAdults = 0;
    this.quantityOfBabies = 0;
    this.quantityOfChilds = 0;
    this.quantityOfRetired = 0;
    this.showLodgingsAvailables = false;
    this.searchModel = new SearchOfLodgingModelForRequest();
    this.formGroup = this.formBuilder.group(
      {
        checkInPicker: ['', Validators.required],
        checkOutPicker: ['', Validators.required],
      },
      { validator: this.DateValidation }
    );
    this.maxDate = new Date();
  }
}
