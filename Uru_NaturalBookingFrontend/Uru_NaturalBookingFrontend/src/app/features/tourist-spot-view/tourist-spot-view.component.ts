import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { TouristSpotModelForResponse } from '../../models/TouristSpotModelForResponse';
import { TouristSpotService } from '../services/tourist-spot.service';
import { CategoryModel } from '../../models/CategoryModel';
import { Region } from 'src/app/models/Region';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tourist-spot-view',
  templateUrl: './tourist-spot-view.component.html',
  styleUrls: ['./tourist-spot-view.component.css']
})
export class TouristSpotViewComponent implements OnInit {

  @Input() categoriesSelectedId: string[];

  @Input() regionSelectedId: string;

  public touristSpotFilterByRegion: TouristSpotModelForResponse[];

  private touristSpotService: TouristSpotService;

  constructor(aTouristSpotService: TouristSpotService, private router: Router) {
    this.touristSpotService = aTouristSpotService;
   }

  ngOnInit(): void {
    this.touristSpotFilterByRegion =
    this.touristSpotService.getTouristSpotsFilterByRegionAndCategories(this.categoriesSelectedId, this.regionSelectedId);
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.touristSpotFilterByRegion =
    this.touristSpotService.getTouristSpotsFilterByRegionAndCategories(changes.categoriesSelectedId.currentValue, this.regionSelectedId);
  }

  navigateToLodgings(touristSpotId: string): void {
    this.router.navigate(['/lodgings', touristSpotId, this.regionSelectedId]);
  }

}
