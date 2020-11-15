import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { DescriptionOfState } from 'src/app/models/ReserveState';
import { ReviewModelForRequest } from 'src/app/models/ReviewModelForRequest';
import { ReserveModelForResponse } from '../../models/ReserveModelForResponse';
import { ReserveService } from '../services/reserve.service';
import { ReserveModelForRequestUpdate } from '../../models/ReserveModelForRequestUpdate';

@Component({
  selector: 'app-form-modify-reserve',
  templateUrl: './form-modify-reserve.component.html',
  styleUrls: ['./form-modify-reserve.component.css']
})
export class FormModifyReserveComponent implements OnInit {

  @Input() reserveId: string;

  public formGroup: FormGroup;

  private reserveService: ReserveService;

  private reserveSelectedToModify: ReserveModelForResponse;

  public existingStates: Map<number, string>;

  public stateSelected: number;

  constructor(aReserveService: ReserveService, private formBuilder: FormBuilder) {
    this.reserveService = aReserveService;
    this.formGroup = this.formBuilder.group({
      name: new FormControl(''),
      lastName: new FormControl(''),
      email: new FormControl(''),
      description: new FormControl('', [Validators.required, this.noWhitespaceValidator]),
      reserveState: new FormControl('', [Validators.required]),
      nameOfLodging: new FormControl('')
    });
  }

  ngOnInit(): void {
    this.existingStates = DescriptionOfState;
    this.ChargeInfoInFields();
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  }

  public ChargeInfoInFields(): void {
    this.reserveSelectedToModify = this.reserveService.getReserveById(this.reserveId);
    this.stateSelected = this.reserveSelectedToModify.ReserveState;
    this.formGroup = this.formBuilder.group({
      name: new FormControl(this.reserveSelectedToModify.Name),
      lastName: new FormControl(this.reserveSelectedToModify.LastName),
      email: new FormControl(this.reserveSelectedToModify.Email),
      description: new FormControl(this.reserveSelectedToModify.DescriptionForGuest, [Validators.required, this.noWhitespaceValidator]),
      reserveState: new FormControl(this.reserveSelectedToModify.ReserveState, [Validators.required]),
      nameOfLodging: new FormControl(this.reserveSelectedToModify.Lodging.Name)
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
        StateOfReserve: this.stateSelected
      };
      this.reserveService.updateReserve(reserveToModify);
    }
  }
}
