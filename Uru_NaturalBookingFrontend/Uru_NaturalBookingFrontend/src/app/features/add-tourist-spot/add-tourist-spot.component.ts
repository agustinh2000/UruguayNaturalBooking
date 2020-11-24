import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Region } from 'src/app/models/Region';
import { CategoryModel } from '../../models/CategoryModel';
import { RegionServiceService } from '../services/region-service.service';
import { TouristSpotForRequestModel } from '../../models/TouristSpotForRequestModel';
import { CategoryService } from '../services/category.service';
import { TouristSpotService } from '../services/tourist-spot.service';
import { TouristSpotModelForResponse } from '../../models/TouristSpotModelForResponse';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-tourist-spot',
  templateUrl: './add-tourist-spot.component.html',
  styleUrls: ['./add-tourist-spot.component.css'],
})
export class AddTouristSpotComponent implements OnInit {
  private regionService: RegionServiceService;

  private categoryService: CategoryService;

  private touristSpotService: TouristSpotService;

  regionsOfTheSystem;

  categoriesOfTheSystem;

  textValue: string;

  selectedFiles: FileList;

  touristSpotToAdd: TouristSpotForRequestModel;

  public formGroup: FormGroup;

  myFiles: string[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private aRegionService: RegionServiceService,
    private aCategoryService: CategoryService,
    private aTouristSpotService: TouristSpotService,
    private router: Router
  ) {
    this.regionsOfTheSystem = new Array<Region>();
    this.categoriesOfTheSystem = new Array<CategoryModel>();
    this.touristSpotService = aTouristSpotService;
    this.categoryService = aCategoryService;
    this.regionService = aRegionService;
    this.formGroup = this.formBuilder.group({
      name: ['', [Validators.required, this.noWhitespaceValidator]],
      description: ['', [Validators.required, this.noWhitespaceValidator]],
      imagePath: ['', [Validators.required, this.noWhitespaceValidator]],
    });
  }
  selectedRegion = new FormControl('', [Validators.required]);
  selectedCategories = new FormControl('', [Validators.required]);

  ngOnInit(): void {
    this.getRegions();
    this.getCategories();
  }

  private getRegions(): void {
    this.regionService.getRegions().subscribe(
      (res) => {
        this.regionsOfTheSystem = res;
      },
      (err) => {
        alert(err.error);
        this.router.navigate(['/regions']);
      }
    );
  }

  private getCategories(): void {
    this.categoryService.getCategories().subscribe(
      (res) => {
        this.categoriesOfTheSystem = res;
      },
      (err) => {
        alert(err.error);
        this.router.navigate(['/regions']);
      }
    );
  }

  public Add(): void {
    this.chargeInfo();
    this.suscribeToAddTouristSpot(this.touristSpotToAdd);
  }

  private suscribeToAddTouristSpot(
    touristSpotToAdd: TouristSpotForRequestModel
  ): void {
    this.touristSpotService.Add(touristSpotToAdd).subscribe(
      (res: TouristSpotModelForResponse) => {
        alert(
          'El punto turistico con nombre ' +
            res.name +
            ' ha sido agregado correctamente.'
        );
        this.router.navigate(['/regions']);
      },
      (err) => {
        alert(err.error);
        this.router.navigate(['/regions']);
      }
    );
  }

  private chargeInfo(): void {
    this.touristSpotToAdd = new TouristSpotForRequestModel(
      this.formGroup.value
    );
    this.touristSpotToAdd.regionId = this.selectedRegion.value;
    this.touristSpotToAdd.listOfCategoriesId = this.selectedCategories.value;
  }

  getErrorMessage(): string {
    if (this.formGroup.controls.name.hasError('required')) {
      return 'Error. El nombre es requerido.';
    }
    return this.formGroup.controls.name.hasError('whitespace')
      ? 'Error. El nombre ingresado no puede ser vacío.'
      : '';
  }

  getErrorMessageDescription(): string {
    if (this.formGroup.controls.description.hasError('required')) {
      return 'Error. La descripción es requerida.';
    }
    return this.formGroup.controls.description.hasError('whitespace')
      ? 'Error. La descripción ingresado no puede ser vacío.'
      : '';
  }

  getErrorMessageForPhoto(): string {
    if (this.formGroup.controls.imagePath.hasError('required')) {
      return 'Error. El link a la foto es requerido.';
    }
    return this.formGroup.controls.imagePath.hasError('whitespace')
      ? 'Error. El link a la foto ingresado no puede ser vacío.'
      : '';
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  };
}
