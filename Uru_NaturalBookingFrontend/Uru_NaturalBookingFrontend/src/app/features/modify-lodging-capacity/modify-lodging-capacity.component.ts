import { Component, OnInit } from '@angular/core';
import { LodgingService } from '../services/lodging.service';
import { LodgingModelForResponse } from '../../models/LodgingModelForResponse';

@Component({
  selector: 'app-modify-lodging-capacity',
  templateUrl: './modify-lodging-capacity.component.html',
  styleUrls: ['./modify-lodging-capacity.component.css']
})
export class ModifyLodgingCapacityComponent implements OnInit {

  private lodgingService: LodgingService;

  public lodgings: LodgingModelForResponse[];

  public lodgingsAvailability: boolean[];

  constructor(aLodgingService: LodgingService) {
    this.lodgingService = aLodgingService;
  }
  ngOnInit(): void {
    this.lodgings = this.lodgingService.getLodgings();
  }

  ngOnChange(): void {
    this.lodgings = this.lodgingService.getLodgings();
  }


  public changed($event): void {
    for (const lodging of this.lodgings) {
      this.lodgingService.changeAvailability(lodging.Id, lodging.IsAvailable);
    }
    this.lodgings = this.lodgingService.getLodgings();
  }
}
