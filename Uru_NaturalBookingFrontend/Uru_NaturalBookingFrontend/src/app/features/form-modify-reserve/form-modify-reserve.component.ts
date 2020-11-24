import { Component, Input, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { DescriptionOfState } from 'src/app/models/ReserveState';
import { ReviewModelForRequest } from 'src/app/models/ReviewModelForRequest';
import { ReserveModelForResponse } from '../../models/ReserveModelForResponse';
import { ReserveService } from '../services/reserve.service';
import { ReserveModelForRequestUpdate } from '../../models/ReserveModelForRequestUpdate';
import { Router } from '@angular/router';

@Component({
  selector: 'app-form-modify-reserve',
  templateUrl: './form-modify-reserve.component.html',
  styleUrls: ['./form-modify-reserve.component.css'],
})
export class FormModifyReserveComponent implements OnInit {
  @Input() reserveId: string;

  public formGroup: FormGroup;

  private reserveService: ReserveService;

  private reserveSelectedToModify: ReserveModelForResponse;

  public existingStates: Map<number, string>;

  public existingStatesKeys: number[];

  public stateSelected: number;

  constructor(
    aReserveService: ReserveService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.reserveService = aReserveService;
    this.formGroup = this.formBuilder.group({
      name: new FormControl(''),
      lastName: new FormControl(''),
      email: new FormControl(''),
      description: new FormControl('', [
        Validators.required,
        this.noWhitespaceValidator,
      ]),
      reserveState: new FormControl('', [Validators.required]),
      nameOfLodging: new FormControl(''),
    });
  }

  ngOnInit(): void {
    this.existingStates = DescriptionOfState;
    this.existingStatesKeys = Array.from(this.existingStates.keys());
    this.chargeInfoInFields();
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  };

  public chargeInfoInFields(): void {
    this.reserveService.getReserveById(this.reserveId).subscribe(
      (res: ReserveModelForResponse) => {
        this.chargeInfoOnFormGroup(res);
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

  private chargeInfoOnFormGroup(reserveModel: ReserveModelForResponse): void {
    this.reserveSelectedToModify = reserveModel;
    this.stateSelected = this.reserveSelectedToModify.stateOfReserve;
    this.formGroup = this.formBuilder.group({
      name: new FormControl(this.reserveSelectedToModify.name),
      lastName: new FormControl(this.reserveSelectedToModify.lastName),
      email: new FormControl(this.reserveSelectedToModify.email),
      description: new FormControl(
        this.reserveSelectedToModify.descriptionForGuest,
        [Validators.required, this.noWhitespaceValidator]
      ),
      reserveState: new FormControl(
        this.reserveSelectedToModify.stateOfReserve,
        [Validators.required]
      ),
      nameOfLodging: new FormControl(this.reserveSelectedToModify.lodging.name),
    });
  }

  public formIsValid(): boolean {
    return this.formGroup.valid;
  }

  modifyReserve(): void {
    if (this.formIsValid()) {
      const reserveToModify: ReserveModelForRequestUpdate = {
        Id: this.reserveId,
        Description: this.formGroup.controls.description.value,
        StateOfReserve: this.stateSelected,
      };
      this.reserveService
        .updateReserve(reserveToModify, this.reserveId)
        .subscribe(
          (res: ReserveModelForResponse) => {
            alert(
              'Se ha modificado correctamente el estado y/o descripción de la reserva con ID: ' +
                this.reserveId +
                ' pasando a tener el estado: ' +
                res.descriptionOfState
            );
            this.router.navigate(['/regions']);
          },
          (err) => {
            alert(err.error);
            this.router.navigate(['/regions']);
          }
        );
    }
  }
}
