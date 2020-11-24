import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
  ValidatorFn,
} from '@angular/forms';
import { ReserveModelForResponse } from 'src/app/models/ReserveModelForResponse';
import { TouristSpotModelForLodgingResponseModel } from 'src/app/models/TouristSpotModelForLodgingResponseModel';
import { ReserveService } from '../services/reserve.service';
import { TouristSpotService } from '../services/tourist-spot.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css'],
})
export class ReviewComponent implements OnInit {
  public formGroup: FormGroup;

  private reserveService: ReserveService;

  private reserveExist: boolean;

  public reserveObtenied: ReserveModelForResponse;

  isShown = false;

  constructor(
    aServiceOfReserves: ReserveService,
    private formBuilder: FormBuilder
  ) {
    this.reserveObtenied = null;
    this.reserveService = aServiceOfReserves;
    this.formGroup = this.formBuilder.group({
      reserveSelected: new FormControl('', [
        Validators.required,
        this.noWhitespaceValidator,
      ]),
    });
  }

  ngOnInit(): void {}

  openCommentaryForm(): void {
    this.reserveService
      .getReserveById(this.formGroup.controls.reserveSelected.value)
      .subscribe(
        (res: ReserveModelForResponse) => {
          this.markShown(res);
        },
        (err) => {
          if (err.status === 400) {
            alert(
              'Error. No es un formato valido para los identificiadores, el mismo debe ser formato GUID.'
            );
          } else {
            alert(err.error);
          }
        }
      );
  }

  markShown(reserveModel: ReserveModelForResponse): void {
    this.isShown = reserveModel !== null;
  }

  getErrorMessage(): string {
    if (this.formGroup.controls.reserveSelected.hasError('required')) {
      return 'Error. El identificador es requerido.';
    }
    return this.formGroup.controls.reserveSelected.hasError('whitespace')
      ? 'Error. El indentificador de reserva ingresado no puede ser vacío.'
      : '';
  }


  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  }

}
