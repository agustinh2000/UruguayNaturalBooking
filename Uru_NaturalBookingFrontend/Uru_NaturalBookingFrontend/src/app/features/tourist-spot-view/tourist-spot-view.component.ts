import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { TouristSpotModelForResponse } from '../../models/TouristSpotModelForResponse';
import { TouristSpotService } from '../services/tourist-spot.service';
import { CategoryModel } from '../../models/CategoryModel';
import { Region } from 'src/app/models/Region';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tourist-spot-view',
  templateUrl: './tourist-spot-view.component.html',
  styleUrls: ['./tourist-spot-view.component.css'],
})
export class TouristSpotViewComponent {
  @Input() categoriesSelectedId: string[];

  @Input() regionSelectedId: string;

  public touristSpotFilterByRegion;

  private touristSpotService: TouristSpotService;

  constructor(aTouristSpotService: TouristSpotService, private router: Router) {
    this.touristSpotService = aTouristSpotService;
    this.touristSpotFilterByRegion = new Array<TouristSpotModelForResponse>();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.touristSpotService
      .getTouristSpotsFilterByRegionAndCategories(
        this.categoriesSelectedId,
        this.regionSelectedId
      )
      .subscribe(
        (res) => {
          this.touristSpotFilterByRegion = res;
        },
        (err) => {
          this.touristSpotFilterByRegion = [];
        }
      );
  }

  navigateToLodgings(touristSpotId: string): void {
    this.router.navigate(['/lodgings', touristSpotId, this.regionSelectedId]);
  }
}
