import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { LodgingModelForRequest } from '../../models/LodgingModelForRequest';
import { LodgingService } from '../services/lodging.service';
import { TouristSpotService } from '../services/tourist-spot.service';
import { TouristSpotModelForLodgingResponseModel } from '../../models/TouristSpotModelForLodgingResponseModel';

@Component({
  selector: 'app-add-lodging',
  templateUrl: './add-lodging.component.html',
  styleUrls: ['./add-lodging.component.css']
})
export class AddLodgingComponent implements OnInit {

  informationOfLodgingToCreate: LodgingModelForRequest;

  public formGroup: FormGroup;

  private touristSpotService: TouristSpotService;

  private lodgingService: LodgingService;

  public selectedTouristSpot: string;

  public currentRate: number;

  public touristSpotExisting: TouristSpotModelForLodgingResponseModel[];

  public isAvailable: boolean = false;

  constructor(private formBuilder: FormBuilder, private lodgingServicePassed: LodgingService
    ,         private touristSpotServicePassed: TouristSpotService) {
    this.touristSpotService = touristSpotServicePassed;
    this.lodgingService = lodgingServicePassed;
    this.formGroup = this.formBuilder.group({
      name: ['', [Validators.required]],
      description: ['', [Validators.required]],
      address: ['', [Validators.required]],
      images: ['', [Validators.required]],
      pricePerNight: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.touristSpotExisting = this.touristSpotService.getTouristSpots();
  }

  /*
  modifyEmit(rating: number): void {
    this.quantityOfStars = rating;
  }
  */

  public Create(): void {
    this.informationOfLodgingToCreate = new LodgingModelForRequest(this.formGroup.value);
    this.informationOfLodgingToCreate.QuantityOfStars = this.currentRate;
    this.informationOfLodgingToCreate.IsAvailable = this.isAvailable;
    this.informationOfLodgingToCreate.TouristSpotId = this.selectedTouristSpot;
    this.lodgingService.CreateLodging(this.informationOfLodgingToCreate);
  }

  public ChangeStateOfAvailability(): void{
    this.isAvailable = !this.isAvailable;
  }
}
