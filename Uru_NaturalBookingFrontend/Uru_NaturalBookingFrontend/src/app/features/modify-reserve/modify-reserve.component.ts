import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ReserveService } from '../services/reserve.service';

@Component({
  selector: 'app-modify-reserve',
  templateUrl: './modify-reserve.component.html',
  styleUrls: ['./modify-reserve.component.css']
})
export class ModifyReserveComponent implements OnInit {

  public formGroup: FormGroup;

  private reserveService: ReserveService;

  isShown = false;

  constructor(aServiceOfReserves: ReserveService, private formBuilder: FormBuilder) {
    this.reserveService = aServiceOfReserves;
    this.formGroup = this.formBuilder.group({
      reserveSelected: new FormControl('', [Validators.required, this.noWhitespaceValidator])
    });
  }

  ngOnInit(): void {
  }

  reserveExist(reserveId: string): boolean {
    return this.reserveService.ReserveExist(reserveId);
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  }

  openModifyForm(): void {
    if (this.reserveExist(this.formGroup.controls.reserveSelected.value)) {
      this.isShown = true;
    }
  }

  public clearFields(): void{
    this.isShown = false;
    this.formGroup = this.formBuilder.group({
      reserveSelected: ['', [Validators.required, this.noWhitespaceValidator]]
    });
  }

}
