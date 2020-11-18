import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ReserveService } from '../services/reserve.service';

@Component({
  selector: 'app-booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.css']
})
export class BookingFormComponent implements OnInit {
  @Input() reserveId: string;

  @Input() touristSpotSelected: string;

  public formGroup: FormGroup;

  private reserveService: ReserveService;

  public quantityOfAdults: number = 0;

  public quantityOfChilds: number = 0;

  public quantityOfBabies: number = 0;

  public quantityOfRetired: number = 0;

  public maxDate: Date;

  constructor(aReserveService: ReserveService, private formBuilder: FormBuilder) {
    this.reserveService = aReserveService;
    this.formGroup = this.formBuilder.group({
      touristSpotSelected: new FormControl('', [Validators.required]),
      checkInPicker: ['', Validators.required],
      checkOutPicker: ['', Validators.required],
    }, { validator: this.DateValidation });
    this.maxDate = new Date();
  }

  DateValidation: ValidatorFn = (fg: FormGroup) => {
    const start = fg.get('checkInPicker').value;
    const end = fg.get('checkOutPicker').value;
    return start !== null && end !== null && start <= end ? null : { range: true };
  }

  ngOnInit(): void {
    this.touristSpotSelected = 'Punta del este';
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  }

  public plus(typeOfGuest: string): void{
    if (typeOfGuest === 'adults'){
      this.quantityOfAdults++;
    }
    if (typeOfGuest === 'childs'){
      this.quantityOfChilds++;
    }
    if (typeOfGuest === 'babies'){
      this.quantityOfBabies++;
    }
    if (typeOfGuest === 'retired'){
      this.quantityOfRetired++;
    }
  }

  public minus(typeOfGuest: string): void{
    if (typeOfGuest === 'adults' && this.quantityOfAdults > 0){
      this.quantityOfAdults--;
    }
    if (typeOfGuest === 'childs' && this.quantityOfChilds > 0){
      this.quantityOfChilds--;
    }
    if (typeOfGuest === 'babies' && this.quantityOfBabies > 0){
      this.quantityOfBabies--;
    }
    if (typeOfGuest === 'retired' && this.quantityOfRetired > 0){
      this.quantityOfRetired--;
    }
  }
}




