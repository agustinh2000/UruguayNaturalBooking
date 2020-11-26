import { Component, Input, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ReserveModelForRequestUpdate } from 'src/app/models/ReserveModelForRequestUpdate';
import { ReserveModelForResponse } from 'src/app/models/ReserveModelForResponse';
import { DescriptionOfState } from 'src/app/models/ReserveState';
import { ReserveService } from '../services/reserve.service';

@Component({
  selector: 'app-form-of-reserve-info',
  templateUrl: './form-of-reserve-info.component.html',
  styleUrls: ['./form-of-reserve-info.component.css'],
})
export class FormOfReserveInfoComponent implements OnInit {
  @Input() reserveId: string;

  public formGroup: FormGroup;

  private reserveService: ReserveService;

  private reserveSelectedToModify: ReserveModelForResponse;

  public existingStates: Map<number, string>;

  public existingStatesKeys: number[];

  public stateSelected: number;

  constructor(
    private aReserveService: ReserveService,
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
        this.existingStates.get(this.stateSelected),
        [Validators.required]
      ),
      nameOfLodging: new FormControl(this.reserveSelectedToModify.lodging.name),
    });
  }
}
