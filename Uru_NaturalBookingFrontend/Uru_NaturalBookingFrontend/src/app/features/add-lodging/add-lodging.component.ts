import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { LodgingModelForRequest } from '../../models/LodgingModelForRequest';
import { LodgingService } from '../services/lodging.service';
import { TouristSpotService } from '../services/tourist-spot.service';
import { TouristSpotModelForLodgingResponseModel } from '../../models/TouristSpotModelForLodgingResponseModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-lodging',
  templateUrl: './add-lodging.component.html',
  styleUrls: ['./add-lodging.component.css'],
})
export class AddLodgingComponent implements OnInit {
  informationOfLodgingToCreate: LodgingModelForRequest;

  public formGroup: FormGroup;

  private touristSpotService: TouristSpotService;

  private lodgingService: LodgingService;

  public selectedTouristSpot: string;

  public currentRate: number;

  public touristSpotExisting;

  public isAvailable: boolean = false;

  public photosOfLodging;

  constructor(
    private formBuilder: FormBuilder,
    private lodgingServicePassed: LodgingService,
    private touristSpotServicePassed: TouristSpotService,
    private router: Router
  ) {
    this.touristSpotExisting = new Array();
    this.photosOfLodging = new Array();
    this.touristSpotService = touristSpotServicePassed;
    this.lodgingService = lodgingServicePassed;
    this.formGroup = this.formBuilder.group({
      name: ['', [Validators.required, this.noWhitespaceValidator]],
      description: ['', [Validators.required, this.noWhitespaceValidator]],
      address: ['', [Validators.required, this.noWhitespaceValidator]],
      images: ['', [this.noEmptyList]],
      pricePerNight: ['', [Validators.required, this.noWhitespaceValidator]],
      selectedTouristSpotControl: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.chargeTouristSpots();
  }

  private chargeTouristSpots(): void {
    this.touristSpotService.getAllTouristSpots().subscribe(
      (res) => {
        this.touristSpotExisting = res;
      },
      (err) => {
        alert(err.error);
      }
    );
  }

  public create(): void {
    this.informationOfLodgingToCreate = new LodgingModelForRequest(
      this.formGroup.value
    );
    this.informationOfLodgingToCreate.QuantityOfStars = this.currentRate;
    this.informationOfLodgingToCreate.IsAvailable = this.isAvailable;
    this.informationOfLodgingToCreate.TouristSpotId = this.selectedTouristSpot;
    this.informationOfLodgingToCreate.Images = this.photosOfLodging;
    this.createLodging(this.informationOfLodgingToCreate);
  }

  private createLodging(informationOfLodging: LodgingModelForRequest): void {
    this.lodgingService.add(informationOfLodging).subscribe(
      (res) => {
        alert(
          'Hospedaje con nombre: ' +
            res.name +
            'en la dirección' +
            res.address +
            ' agregado correctamente'
        );
        this.router.navigateByUrl('/regions');
      },
      (err) => {
        alert(err.error);
      }
    );
  }

  public ChangeStateOfAvailability(): void {
    this.isAvailable = !this.isAvailable;
  }

  public addPhoto(): void {
    const path = this.formGroup.controls.images.value;
    if (path.trim() !== '') {
      this.photosOfLodging.push(path);
      this.formGroup.controls.images.reset();
    }
  }

  public isEmptyListOfPhotos(): boolean {
    return this.photosOfLodging.length === 0;
  }

  getErrorMessage(): string {
    if (this.formGroup.controls.name.hasError('required')) {
      return 'Error. El nombre es requerido.';
    }
    return this.formGroup.controls.name.hasError('whitespace')
      ? 'Error. El nombre ingresado no puede ser vacío.'
      : '';
  }

  getErrorMessageAddress(): string {
    if (this.formGroup.controls.address.hasError('required')) {
      return 'Error. La dirección es requerida.';
    }
    return this.formGroup.controls.address.hasError('whitespace')
      ? 'Error. La dirección ingresado no puede ser vacía.'
      : '';
  }

  getErrorMessageDescription(): string {
    if (this.formGroup.controls.description.hasError('required')) {
      return 'Error. La descripción es requerida.';
    }
    return this.formGroup.controls.description.hasError('whitespace')
      ? 'Error. La descripción ingresada no puede ser vacía.'
      : '';
  }

  getErrorMessagePrice(): string {
    if (this.formGroup.controls.pricePerNight.hasError('required')) {
      return 'Error. El precio es requerido.';
    }
    return this.formGroup.controls.pricePerNight.hasError('whitespace')
      ? 'Error. El precio ingresado no puede ser vacío.'
      : '';
  }

  getErrorMessageForPhoto(): string {
    if (this.formGroup.controls.images.hasError('emptyList')) {
      return 'Error. Debe ingresar al menos una foto para el hospedaje.';
    }
  }

  noEmptyList: ValidatorFn = (control: FormControl) => {
    const isEmptyList = this.isEmptyListOfPhotos();
    const isValid = !isEmptyList;
    return isValid ? null : { emptyList: true };
  };

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  };
}
