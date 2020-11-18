import { Component, OnInit } from '@angular/core';
import { LodgingService } from '../services/lodging.service';
import { LodgingForSearchModel } from '../../models/LodgingForSearchModel';

@Component({
  selector: 'app-lodging-cards',
  templateUrl: './lodging-cards.component.html',
  styleUrls: ['./lodging-cards.component.css']
})
export class LodgingCardsComponent implements OnInit {

  Arr = Array;

  private lodgingsService: LodgingService;

  public lodgingsOfSearch: LodgingForSearchModel[];

  constructor(aLodgingService: LodgingService) {
    this.lodgingsService = aLodgingService;
   }

  ngOnInit(): void {
  this.lodgingsOfSearch = this.lodgingsService.getLodgingsOfSearch();
  }
}
