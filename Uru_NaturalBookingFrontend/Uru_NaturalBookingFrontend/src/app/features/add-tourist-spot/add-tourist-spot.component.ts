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

@Component({
  selector: 'app-add-tourist-spot',
  templateUrl: './add-tourist-spot.component.html',
  styleUrls: ['./add-tourist-spot.component.css'],
})
export class AddTouristSpotComponent implements OnInit {
  private regionService: RegionServiceService;

  private categoryService: CategoryService;

  private touristSpotService: TouristSpotService;

  regionsOfTheSystem: Region[];

  categoriesOfTheSystem: CategoryModel[];

  textValue: string;

  selectedFiles: FileList;

  touristSpotToAdd: TouristSpotForRequestModel;

  public formGroup: FormGroup;

  myFiles: string[] = [];

  constructor(
    private formBuilder: FormBuilder,
    aRegionService: RegionServiceService,
    aCategoryService: CategoryService,
    aTouristSpotService: TouristSpotService
  ) {
    this.touristSpotService = aTouristSpotService;
    this.categoryService = aCategoryService;
    this.regionService = aRegionService;
    this.formGroup = this.formBuilder.group({
      name: ['', [Validators.required, this.noWhitespaceValidator]],
      description: ['', [Validators.required, this.noWhitespaceValidator]],
    });
  }

  selectedRegion = new FormControl('', [Validators.required]);

  selectedCategories = new FormControl('', [Validators.required]);

  ngOnInit(): void {
    //this.regionsOfTheSystem = this.regionService.getRegions();
    this.categoriesOfTheSystem = this.categoryService.getCategories();
  }

  public selectFiles(event) {
    for (let i = 0; i < event.target.files.length; i++) {
      this.myFiles.push(event.target.files[i]);
    }
  }

  public Add(): void {
    this.touristSpotToAdd = new TouristSpotForRequestModel(
      this.formGroup.value
    );
    this.touristSpotToAdd.RegionId = this.selectedRegion.value;
    this.touristSpotToAdd.ListOfCategoriesId = this.selectedCategories.value;
    this.touristSpotService.Add(this.touristSpotToAdd);
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

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  };
}
