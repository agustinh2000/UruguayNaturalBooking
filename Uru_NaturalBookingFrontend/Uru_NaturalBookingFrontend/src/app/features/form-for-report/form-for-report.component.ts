import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { TouristSpotService } from '../services/tourist-spot.service';

@Component({
  selector: 'app-form-for-report',
  templateUrl: './form-for-report.component.html',
  styleUrls: ['./form-for-report.component.css']
})
export class FormForReportComponent implements OnInit {

  public formGroup: FormGroup;

  private touristSpotService: TouristSpotService;


  constructor(aTouristSpotService: TouristSpotService) {
    this.touristSpotService = aTouristSpotService;
   }

  ngOnInit(): void {
  }

}
