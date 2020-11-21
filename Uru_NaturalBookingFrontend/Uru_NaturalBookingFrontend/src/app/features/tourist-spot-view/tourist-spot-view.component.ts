import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { TouristSpotModelForResponse } from '../../models/TouristSpotModelForResponse';
import { TouristSpotService } from '../services/tourist-spot.service';
import { CategoryModel } from '../../models/CategoryModel';
import { Region } from 'src/app/models/Region';

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

  constructor(aTouristSpotService: TouristSpotService) {
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

}
