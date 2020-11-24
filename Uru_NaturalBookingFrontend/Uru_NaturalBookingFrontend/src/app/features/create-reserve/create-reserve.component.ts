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
import { ReserveService } from '../services/reserve.service';
import { ReserveModelForResponse } from 'src/app/models/ReserveModelForResponse';

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
    private router: Router,
    private reserveService: ReserveService
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
    this.lodgingsService.getLodgingById(this.lodgingId).subscribe(
      (res: LodgingModelForResponse) => {
        this.lodgingForReserve = res;
      },
      (err) => {
        alert(err.error);
      }
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

  public createReserve(): void {
    this.reserve = new ReserveModelForRequest();
    this.reserve.name = this.formGroup.controls.name.value;
    this.reserve.lastName = this.formGroup.controls.lastName.value;
    this.reserve.email = this.formGroup.controls.email.value;
    this.reserve.checkIn = this.checkIn;
    this.reserve.checkOut = this.checkOut;
    this.reserve.quantityOfAdult = this.quantityOfGuest[0];
    this.reserve.quantityOfChild = this.quantityOfGuest[1];
    this.reserve.quantityOfBaby = this.quantityOfGuest[2];
    this.reserve.quantityOfRetired = this.quantityOfGuest[3];
    this.reserve.idOfLodgingToReserve = this.lodgingId;

    this.reserveService.createReserve(this.reserve).subscribe(
      (res: ReserveModelForResponse) => {
        this.router.navigate(['reserve-confirmation', res.id]);
      },
      (err) => {
        alert(err);
      }
    );
  }
}
