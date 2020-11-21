import { Injectable } from '@angular/core';
import { LodgingModelForReserveResponseModel } from 'src/app/models/LodgingModelForReserveResponseModel';
import { ReserveState } from 'src/app/models/ReserveState';
import { ReserveModelForResponse } from '../../models/ReserveModelForResponse';
import { DescriptionOfState } from '../../models/ReserveState';
import { ReserveModelForRequestUpdate } from '../../models/ReserveModelForRequestUpdate';

@Injectable({
  providedIn: 'root',
})
export class ReserveService {
  reserveExist(reserveId: string): boolean {
    return true;
    // this is a call to the ReserveController in the webAPI to get the reserve by the id,
    // and in this case if we get a 200 means that reserve exist, in other case not.
  }

  getReserveById(reserveId: string): ReserveModelForResponse {
    const reserveModel: ReserveModelForResponse = {
      Id: '40a749b8-6c68-4705-af8f-25b967b4c7aa',
      Name: 'Joaquin',
      PhoneNumberOfContact: 244087645,
      QuantityOfAdult: 1,
      QuantityOfBaby: 2,
      QuantityOfChild: 3,
      QuantityOfRetired: 4,
      TotalPrice: 1240,
      LastName: 'Lamela',
      Email: 'joaquin.lamela@gmail.com',
      CheckIn: new Date(),
      CheckOut: new Date(),
      DescriptionForGuest: 'Lo invitamos a pasar un momento asombroso',
      ReserveState: ReserveState.Aceptada,
      DescriptionOfState: DescriptionOfState.get(ReserveState.Aceptada),
      Lodging: {
        Name: 'Hotel Enjoy Conrad',
        Address: 'Parada 21, Playa Mansa',
      },
    };
    return reserveModel;
    // this is a call to the ReserveController in the webAPI to get the reserve by the id,
    // and in this case if we get a 200 means that reserve exist and i need to charge info
  }

  updateReserve(
    reserveModelForUpdate: ReserveModelForRequestUpdate
  ): ReserveModelForResponse {
    return;
    // this is a call to the ReserveController in the webAPI to update the reserve passed in the parameter
    // and return the reserve update in ReserveModelForResponse
  }

  constructor() {}
}
