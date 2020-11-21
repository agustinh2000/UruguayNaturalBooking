import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { CategoryModel } from '../../models/CategoryModel';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-selection-of-tourist-spot',
  templateUrl: './selection-of-tourist-spot.component.html',
  styleUrls: ['./selection-of-tourist-spot.component.css']
})
export class SelectionOfTouristSpotComponent implements OnInit {

  public allCategories: CategoryModel[];

  public categoriesChecked: boolean[];

  public categoriesSelectedId: string[];

  constructor(private categoryService: CategoryService, private currentRoute: ActivatedRoute) {}

  public idRegionSelected: string;

  ngOnInit(): void {
    this.idRegionSelected = this.currentRoute.snapshot.params['idRegion'];
    this.allCategories = this.categoryService.getCategories();
    this.initializeCategoriesChecked();
  }

  private initializeCategoriesChecked(): void{
    this.categoriesChecked = new Array<boolean>(this.allCategories.length);
    for (let i = 0; i < this.categoriesChecked.length; i++) {
      this.categoriesChecked[i] = false;
    }
  }

  public doCheck(categoryIndex: number): void{
    this.categoriesChecked[categoryIndex] = !this.categoriesChecked[categoryIndex];
    this.categoriesSelectedId = this.getSelectedOptions();
  }

  private getSelectedOptions(): string[] {
    const categoriesSelected: string[] = [];
    for (let i = 0; i < this.categoriesChecked.length; i++) {
      if (this.categoriesChecked[i]){
        categoriesSelected.push(this.allCategories[i].Id);
      }
    }
    return categoriesSelected;
  }

}
