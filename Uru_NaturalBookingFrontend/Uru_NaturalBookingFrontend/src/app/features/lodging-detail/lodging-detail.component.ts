import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LodgingForSearchModel } from 'src/app/models/LodgingForSearchModel';
import { LodgingModelForResponse } from 'src/app/models/LodgingModelForResponse';
import { LodgingModelForSearchResponse } from 'src/app/models/LodgingModelForSearchResponse';
import { LodgingService } from '../services/lodging.service';

@Component({
  selector: 'app-lodging-detail',
  templateUrl: './lodging-detail.component.html',
  styleUrls: ['./lodging-detail.component.css'],
})
export class LodgingDetailComponent implements OnInit {
  private lodgingsService: LodgingService;

  starLodgingArr = Array;

  starReviewArr = Array;

  indexOfImageToShow = 0;

  public lodgingForReserve: LodgingModelForResponse;

  public checkIn: Date;
  public checkOut: Date;
  public quantityOfGuest: number[];
  public lodgingId: string;
  public totalPriceForSearch: number;

  constructor(
    aLodgingService: LodgingService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.lodgingsService = aLodgingService;
    this.route.queryParams.subscribe((params) => {
      this.checkIn = params.CheckIn;
      this.checkOut = params.CheckOut;
      this.quantityOfGuest = params.QuantityOfGuest;
      this.lodgingId = params.LodgingId;
      this.totalPriceForSearch = params.TotalPriceForSearch;
    });
  }

  ngOnInit(): void {
    this.lodgingForReserve = this.lodgingsService.getLodgingById(
      this.lodgingId
    );
  }

  public nextImage(): void {
    if (
      this.indexOfImageToShow ===
      this.lodgingForReserve.reviewsForLodging.length - 1
    ) {
      this.indexOfImageToShow = 0;
    } else {
      this.indexOfImageToShow++;
    }
  }

  public previousImage(): void {
    if (this.indexOfImageToShow === 0) {
      this.indexOfImageToShow =
        this.lodgingForReserve.reviewsForLodging.length - 1;
    } else {
      this.indexOfImageToShow--;
    }
  }

  public reserveNow(): void {
    this.router.navigate(['create-reserve'], {
      queryParams: {
        CheckIn: this.checkIn,
        CheckOut: this.checkOut,
        QuantityOfGuest: this.quantityOfGuest,
        LodgingId: this.lodgingId,
      },
    });
  }
}
