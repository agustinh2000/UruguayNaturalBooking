import { Component, Input, OnInit } from '@angular/core';
import { LodgingService } from '../services/lodging.service';
import { LodgingForSearchModel } from '../../models/LodgingForSearchModel';
import { SearchOfLodgingModelForRequest } from 'src/app/models/SearchOfLodgingModelForRequest';
import { NavigationExtras, Router } from '@angular/router';

@Component({
  selector: 'app-lodging-cards',
  templateUrl: './lodging-cards.component.html',
  styleUrls: ['./lodging-cards.component.css'],
})
export class LodgingCardsComponent implements OnInit {
  @Input() searchModel: SearchOfLodgingModelForRequest;

  Arr = Array;

  private lodgingsService: LodgingService;

  public lodgingsOfSearch: LodgingForSearchModel[];

  constructor(aLodgingService: LodgingService, private router: Router) {
    this.lodgingsService = aLodgingService;
  }

  ngOnInit(): void {
    this.lodgingsOfSearch = this.lodgingsService.getLodgingsOfSearch(
      this.searchModel
    );
  }

  displayDetailLodging(preReserve: LodgingForSearchModel): void {
    this.router.navigate(['/lodging-detail'], {
      queryParams: {
        CheckIn: preReserve.CheckIn,
        CheckOut: preReserve.CheckOut,
        QuantityOfGuest: preReserve.QuantityOfGuest,
        LodgingId: preReserve.Lodging.Id,
        TotalPriceForSearch: preReserve.TotalPriceForSearch,
      },
    });
  }
}
