import { Component, OnInit } from '@angular/core';
import { LodgingService } from '../services/lodging.service';
import { LodgingModelForResponse } from '../../models/LodgingModelForResponse';
import { LodgingModelForRequest } from '../../models/LodgingModelForRequest';
import { Router } from '@angular/router';

@Component({
  selector: 'app-modify-lodging-capacity',
  templateUrl: './modify-lodging-capacity.component.html',
  styleUrls: ['./modify-lodging-capacity.component.css'],
})
export class ModifyLodgingCapacityComponent implements OnInit {
  private lodgingService: LodgingService;

  public lodgings;

  constructor(aLodgingService: LodgingService, private router: Router) {
    this.lodgings = new Array<LodgingModelForResponse>();
    this.lodgingService = aLodgingService;
  }

  ngOnInit(): void {
    this.chargeLodgings();
  }

  private chargeLodgings(): void{
    this.lodgingService.getLodgings().subscribe(
      res => {
        this.lodgings = res;
      },
      (err) => {
        alert(err.error);
        this.router.navigate(['regions']);
      }
    );
  }

  ngOnChange(): void {
    this.lodgings = this.lodgingService.getLodgings();
  }

  public changed($event): void {
    for (const lodging of this.lodgings) {
       const modifiedLodging = new LodgingModelForRequest();
       modifiedLodging.Name = lodging.name;
       modifiedLodging.QuantityOfStars = lodging.quantityOfStars;
       modifiedLodging.Description = lodging.description;
       modifiedLodging.Address = lodging.address;
       modifiedLodging.IsAvailable = lodging.isAvailable;
       this.modifyLodging(lodging.id, modifiedLodging);
    }
  }

  private modifyLodging(lodgingId: string, modifiedLodging: LodgingModelForRequest): void{
    this.lodgingService.modify(lodgingId, modifiedLodging).subscribe(
      (res) => {
      },
      (err) => {
        alert(err.error);
        this.lodgings = this.chargeLodgings();
      }
    );
  }

  public delete(event, lodgingId): void{
    this.lodgingService.delete(lodgingId).subscribe(
      res => {
        this.lodgings = this.chargeLodgings();
      },
      (err) => {
        alert(err.error);
      }
    );
  }

}
