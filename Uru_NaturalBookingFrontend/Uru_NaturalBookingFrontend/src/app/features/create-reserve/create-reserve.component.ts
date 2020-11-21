import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { LodgingForSearchModel } from 'src/app/models/LodgingForSearchModel';
import { LodgingService } from '../services/lodging.service';
import { ReserveModelForRequest } from '../../models/ReserveModelForRequest';

@Component({
  selector: 'app-create-reserve',
  templateUrl: './create-reserve.component.html',
  styleUrls: ['./create-reserve.component.css']
})
export class CreateReserveComponent implements OnInit {

  public lodgingForReserve: LodgingForSearchModel;

  private lodgingsService: LodgingService;

  public reserve: ReserveModelForRequest;

  public formGroup: FormGroup;

  constructor(private formBuilder: FormBuilder, aLodgingService: LodgingService) {
    this.lodgingsService = aLodgingService;
    this.formGroup = this.formBuilder.group({
      name: ['', [Validators.required, this.noWhitespaceValidator]],
      lastName: ['', [Validators.required, this.noWhitespaceValidator]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit(): void {
    this.lodgingForReserve = this.lodgingsService.getLodgingSearched();
  }

  getErrorMessageName(): string {
    if (this.formGroup.controls.name.hasError('required')) {
      return 'Error. El nombre es requerido.';
    }
    return this.formGroup.controls.name.hasError('whitespace') ? 'Error. El nombre ingresado no puede ser vacío.' : '';
  }

  getErrorMessageLastName(): string {
    if (this.formGroup.controls.lastName.hasError('required')) {
      return 'Error. El apellido es requerido.';
    }
    return this.formGroup.controls.lastName.hasError('whitespace') ? 'Error. El apellido ingresado no puede ser vacío.' : '';
  }

  getErrorMessageEmail(): string {
    if (this.formGroup.controls.email.hasError('required')) {
      return 'Error. El email es requerido.';
    }
    return this.formGroup.controls.email.hasError('email') ? 'Error. El email ingresado debe tener un formato valido.' : '';
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  }

  public Reserve(): void {
    this.reserve = new ReserveModelForRequest();
    this.reserve.Name = this.formGroup.controls.name.value;
    this.reserve.LastName = this.formGroup.controls.lastName.value;
    this.reserve.Email = this.formGroup.controls.email.value;
    this.reserve.CheckIn = this.lodgingForReserve.CheckIn;
    this.reserve.CheckOut = this.lodgingForReserve.CheckIn;
    this.reserve.QuantityOfAdult = this.lodgingForReserve.QuantityOfGuest[0];
    this.reserve.QuantityOfChild = this.lodgingForReserve.QuantityOfGuest[1];
    this.reserve.QuantityOfBaby = this.lodgingForReserve.QuantityOfGuest[2];
    this.reserve.QuantityOfRetired = this.lodgingForReserve.QuantityOfGuest[3];
    this.reserve.IdOfLodgingToReserve = this.lodgingForReserve.Lodging.Id;
    //call web api post method
  }
}
