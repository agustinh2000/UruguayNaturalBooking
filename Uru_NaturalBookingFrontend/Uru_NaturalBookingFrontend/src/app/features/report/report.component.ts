import { Component, OnInit } from '@angular/core';
import { FormGroup, ValidatorFn, FormBuilder, Validators, FormControl } from '@angular/forms';
import { TouristSpotModelForLodgingResponseModel } from 'src/app/models/TouristSpotModelForLodgingResponseModel';
import { TouristSpotService } from '../services/tourist-spot.service';

@Component({
  selector: 'app-form-for-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})


export class ReportComponent implements OnInit {

  public formGroup: FormGroup;

  private touristSpotService: TouristSpotService;

  public touristSpotExisting: TouristSpotModelForLodgingResponseModel[];

  public maxDate: Date;

  isShown = false;

  public touristSpotSelected: FormControl;

  constructor(aTouristSpotService: TouristSpotService, private formBuilder: FormBuilder) {
    this.touristSpotService = aTouristSpotService;
    this.formGroup = this.formBuilder.group({
      touristSpotSelected: new FormControl('', [Validators.required]),
      checkInPicker: ['', Validators.required],
      checkOutPicker: ['', Validators.required],
    }, { validator: this.DateValidation });
    this.maxDate = new Date();
  }

  ngOnInit(): void {
    this.touristSpotExisting = this.touristSpotService.getTouristSpots();
  }

  DateValidation: ValidatorFn = (fg: FormGroup) => {
    const start = fg.get('checkInPicker').value;
    const end = fg.get('checkOutPicker').value;
    return start !== null && end !== null && start <= end ? null : { range: true };
  }

  formIsValid(): boolean {
    return this.formGroup.valid;
  }

  generateReport(): void {
    if (this.formIsValid()) {
      this.isShown = true;
    }
  }

}
