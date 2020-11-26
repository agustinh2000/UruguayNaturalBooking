import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { ReserveModelForResponse } from 'src/app/models/ReserveModelForResponse';
import { ReserveService } from '../services/reserve.service';

@Component({
  selector: 'app-ask-state-of-reserve',
  templateUrl: './ask-state-of-reserve.component.html',
  styleUrls: ['./ask-state-of-reserve.component.css'],
})
export class AskStateOfReserveComponent implements OnInit {
  public formGroup: FormGroup;

  private reserveService: ReserveService;

  isShown = false;

  private existReserve: boolean;

  constructor(
    aServiceOfReserves: ReserveService,
    private formBuilder: FormBuilder
  ) {
    this.reserveService = aServiceOfReserves;
    this.formGroup = this.formBuilder.group({
      reserveSelected: new FormControl('', [
        Validators.required,
        this.noWhitespaceValidator,
      ]),
    });
  }

  ngOnInit(): void {}

  openModifyForm(): void {
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

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  };

  getErrorMessage(): string {
    if (this.formGroup.controls.reserveSelected.hasError('required')) {
      return 'Error. El ID de la reserva es requerido.';
    }
    if (this.formGroup.controls.reserveSelected.hasError('whitespace')) {
      return 'Error. El ID de la reserva no puede ser vacio.';
    }
  }

  public clearFields(): void {
    this.isShown = false;
    this.formGroup = this.formBuilder.group({
      reserveSelected: ['', [Validators.required, this.noWhitespaceValidator]],
    });
  }
}
