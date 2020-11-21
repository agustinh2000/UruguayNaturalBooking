import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { LodgingService } from '../services/lodging.service';
import { ReserveModelForRequest } from '../../models/ReserveModelForRequest';
import { ActivatedRoute, Router } from '@angular/router';
import { LodgingModelForResponse } from 'src/app/models/LodgingModelForResponse';

@Component({
  selector: 'app-create-reserve',
  templateUrl: './create-reserve.component.html',
  styleUrls: ['./create-reserve.component.css'],
})
export class CreateReserveComponent implements OnInit {
  public lodgingForReserve: LodgingModelForResponse;

  private lodgingsService: LodgingService;

  public reserve: ReserveModelForRequest;

  public formGroup: FormGroup;

  public checkIn: Date;
  public checkOut: Date;
  public quantityOfGuest: number[];
  public lodgingId: string;

  constructor(
    private formBuilder: FormBuilder,
    aLodgingService: LodgingService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.lodgingsService = aLodgingService;
    this.formGroup = this.formBuilder.group({
      name: ['', [Validators.required, this.noWhitespaceValidator]],
      lastName: ['', [Validators.required, this.noWhitespaceValidator]],
      email: ['', [Validators.required, Validators.email]],
    });
    this.route.queryParams.subscribe((params) => {
      this.checkIn = params.CheckIn;
      this.checkOut = params.CheckOut;
      this.quantityOfGuest = params.QuantityOfGuest;
      this.lodgingId = params.LodgingId;
    });
  }

  ngOnInit(): void {
    this.lodgingForReserve = this.lodgingsService.getLodgingById(
      this.lodgingId
    );
  }

  getErrorMessageName(): string {
    if (this.formGroup.controls.name.hasError('required')) {
      return 'Error. El nombre es requerido.';
    }
    return this.formGroup.controls.name.hasError('whitespace')
      ? 'Error. El nombre ingresado no puede ser vacío.'
      : '';
  }

  getErrorMessageLastName(): string {
    if (this.formGroup.controls.lastName.hasError('required')) {
      return 'Error. El apellido es requerido.';
    }
    return this.formGroup.controls.lastName.hasError('whitespace')
      ? 'Error. El apellido ingresado no puede ser vacío.'
      : '';
  }

  getErrorMessageEmail(): string {
    if (this.formGroup.controls.email.hasError('required')) {
      return 'Error. El email es requerido.';
    }
    return this.formGroup.controls.email.hasError('email')
      ? 'Error. El email ingresado debe tener un formato valido.'
      : '';
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  };

  public Reserve(): void {
    this.reserve = new ReserveModelForRequest();
    this.reserve.Name = this.formGroup.controls.name.value;
    this.reserve.LastName = this.formGroup.controls.lastName.value;
    this.reserve.Email = this.formGroup.controls.email.value;
    this.reserve.CheckIn = this.checkIn;
    this.reserve.CheckOut = this.checkOut;
    this.reserve.QuantityOfAdult = this.quantityOfGuest[0];
    this.reserve.QuantityOfChild = this.quantityOfGuest[1];
    this.reserve.QuantityOfBaby = this.quantityOfGuest[2];
    this.reserve.QuantityOfRetired = this.quantityOfGuest[3];
    this.reserve.IdOfLodgingToReserve = this.lodgingId;
    // call web api post method
    this.router.navigate(['reserve-confirmation', '123']);
  }
}
