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

  public selectedTouristSpot: string;

  public  maxDate: Date;

  isShown = false;

  constructor(aTouristSpotService: TouristSpotService, private formBuilder: FormBuilder) {
    this.touristSpotService = aTouristSpotService;
    this.formGroup = this.formBuilder.group({
      checkInPicker: ['', Validators.required],
      checkOutPicker: ['', Validators.required],
  }, {validator: this.DateValidation});
    this.maxDate = new Date();
   }

   touristSpotSelected = new FormControl('', [Validators.required]);


  ngOnInit(): void {
    this.touristSpotExisting = this.touristSpotService.getTouristSpots();
  }

   DateValidation: ValidatorFn = (fg: FormGroup) => {
    const start = fg.get('checkInPicker').value;
    const end = fg.get('checkOutPicker').value;
    return start !== null && end !== null && start <= end ? null : { range: true };
  }

  generateReport():void{
this.isShown = true;
  }

}
