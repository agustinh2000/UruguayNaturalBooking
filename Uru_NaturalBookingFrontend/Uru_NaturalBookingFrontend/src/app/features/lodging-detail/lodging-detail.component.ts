import { Component, OnInit } from '@angular/core';
import { LodgingForSearchModel } from 'src/app/models/LodgingForSearchModel';
import { LodgingService } from '../services/lodging.service';

@Component({
  selector: 'app-lodging-detail',
  templateUrl: './lodging-detail.component.html',
  styleUrls: ['./lodging-detail.component.css']
})
export class LodgingDetailComponent implements OnInit {

  private lodgingsService: LodgingService;

  starLodgingArr = Array;

  starReviewArr = Array;

  indexOfImageToShow = 0;

  public lodgingForReserve: LodgingForSearchModel;

  constructor(aLodgingService: LodgingService) {
    this.lodgingsService = aLodgingService;
  }

  ngOnInit(): void {
    this.lodgingForReserve = this.lodgingsService.getLodgingSearched();
  }

  public nextImage(): void {
    if (this.indexOfImageToShow === this.lodgingForReserve.Lodging.ReviewsForLodging.length - 1) {
      this.indexOfImageToShow = 0;
    }
    else {
      this.indexOfImageToShow++;
    }
  }

  public previousImage(): void {
    if (this.indexOfImageToShow === 0) {
      this.indexOfImageToShow = this.lodgingForReserve.Lodging.ReviewsForLodging.length - 1;
    }
    else {
      this.indexOfImageToShow--;
    }
  }

  public reserveNow(){
    //With routing change the component
  }

}
