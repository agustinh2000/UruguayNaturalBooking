import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
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
      name: ['', [Validators.required, this.noWhitespaceValidator]],
      description: ['', [Validators.required, this.noWhitespaceValidator]],
      address: ['', [Validators.required, this.noWhitespaceValidator]],
      //images: ['', [Validators.required]],
      pricePerNight: ['', [Validators.required, this.noWhitespaceValidator]],
      selectedTouristSpotControl: ['', [Validators.required]]
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

  getErrorMessage(): string {
    if (this.formGroup.controls.name.hasError('required')) {
      return 'Error. El nombre es requerido.';
    }
    return this.formGroup.controls.name.hasError('whitespace') ? 'Error. El nombre ingresado no puede ser vacío.' : '';
  }

  getErrorMessageAddress(): string {
    if (this.formGroup.controls.address.hasError('required')) {
      return 'Error. La dirección es requerida.';
    }
    return this.formGroup.controls.address.hasError('whitespace') ? 'Error. La dirección ingresado no puede ser vacía.' : '';
  }

  getErrorMessageDescription(): string{
    if (this.formGroup.controls.description.hasError('required')) {
      return 'Error. La descripción es requerida.';
    }
    return this.formGroup.controls.description.hasError('whitespace') ? 'Error. La descripción ingresada no puede ser vacía.' : '';
  }

  getErrorMessagePrice(): string{
    if (this.formGroup.controls.pricePerNight.hasError('required')) {
      return 'Error. El precio es requerido.';
    }
    return this.formGroup.controls.pricePerNight.hasError('whitespace') ? 'Error. El precio ingresado no puede ser vacío.' : '';
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  }
}
