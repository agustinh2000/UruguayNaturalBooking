import { Injectable } from '@angular/core';
import { ReportModel } from '../../models/ReportModel';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  readonly lodgignsForReport: ReportModel[] = [
    { NameOfLodging: 'Hotel Enjoy',
      QuantityOfReserves: 10},
      { NameOfLodging: 'Hotel Sheraton',
      QuantityOfReserves: 7},
      { NameOfLodging: 'Hotel NH',
      QuantityOfReserves: 6},
      { NameOfLodging: 'Hotel Carmelo',
      QuantityOfReserves: 4},
      { NameOfLodging: 'Hotel IBIS',
      QuantityOfReserves: 2},
  ];

  constructor() { }

  getLodgingsForReport(checkIn : Date, checkOut : Date, touristSpotId: string): ReportModel[] {
    const lodgingsObteined: ReportModel[] = [];
    for (const lodging of this.lodgignsForReport){
      lodgingsObteined.push(lodging);
    }
    return lodgingsObteined;
  }
}
