import { Component, Input, OnInit } from '@angular/core';
import { LodgingService } from '../services/lodging.service';
import { LodgingForSearchModel } from '../../models/LodgingForSearchModel';
import { SearchOfLodgingModelForRequest } from 'src/app/models/SearchOfLodgingModelForRequest';
import { NavigationExtras, Router } from '@angular/router';
import { SearchOfLodgingsService } from '../services/search-of-lodgings.service';

@Component({
  selector: 'app-lodging-cards',
  templateUrl: './lodging-cards.component.html',
  styleUrls: ['./lodging-cards.component.css'],
})
export class LodgingCardsComponent implements OnInit {
  @Input() searchModel: SearchOfLodgingModelForRequest;

  Arr = Array;

  public lodgingsOfSearch;

  constructor(
    private searchService: SearchOfLodgingsService,
    private router: Router
  ) {
    this.lodgingsOfSearch = new Array<LodgingForSearchModel>();
  }

  ngOnInit(): void {
    this.searchService.getLodgingsOfSearch(this.searchModel).subscribe(
      (res) => {
        this.lodgingsOfSearch = res;
      },
      (err) => {
        alert(err.error);
      }
    );
  }

  displayDetailLodging(preReserve: LodgingForSearchModel): void {
    this.router.navigate(['/lodging-detail'], {
      queryParams: {
        CheckIn: preReserve.checkIn,
        CheckOut: preReserve.checkOut,
        QuantityOfGuest: preReserve.quantityOfGuest,
        LodgingId: preReserve.lodging.id,
        TotalPriceForSearch: preReserve.totalPriceForSearch,
      },
    });
  }
}
