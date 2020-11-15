import { Injectable } from '@angular/core';
import { LodgingModelForReserveResponseModel } from 'src/app/models/LodgingModelForReserveResponseModel';
import { ReserveState } from 'src/app/models/ReserveState';
import { ReserveModelForResponse } from '../../models/ReserveModelForResponse';
import { DescriptionOfState } from '../../models/ReserveState';
import { ReserveModelForRequestUpdate } from '../../models/ReserveModelForRequestUpdate';

@Injectable({
  providedIn: 'root'
})
export class ReserveService {

  ReserveExist(reserveId: string): boolean {
    return true;
    // this is a call to the ReserveController in the webAPI to get the reserve by the id,
    // and in this case if we get a 200 means that reserve exist, in other case not.
  }

  getReserveById(reserveId: string): ReserveModelForResponse {
    const reserveModel: ReserveModelForResponse = {
      Name: 'Joaquin',
      LastName: 'Lamela',
      Email: 'joaquin.lamela@gmail.com',
      CheckIn: new Date(),
      CheckOut: new Date(),
      DescriptionForGuest: 'Lo invitamos a pasar un momento asombroso',
      ReserveState: ReserveState.Aceptada,
      DescriptionOfState: DescriptionOfState.get(ReserveState.Aceptada),
      Lodging: {
        Name: 'Hotel Enjoy Conrad',
        Address: 'Parada 21, Playa Mansa'
      }
    };
    return reserveModel;
    // this is a call to the ReserveController in the webAPI to get the reserve by the id,
    // and in this case if we get a 200 means that reserve exist and i need to charge info
  }

  updateReserve(reserveModelForUpdate: ReserveModelForRequestUpdate): ReserveModelForResponse{
    return;
    // this is a call to the ReserveController in the webAPI to update the reserve passed in the parameter
    // and return the reserve update in ReserveModelForResponse
  }

  constructor() { }
}
