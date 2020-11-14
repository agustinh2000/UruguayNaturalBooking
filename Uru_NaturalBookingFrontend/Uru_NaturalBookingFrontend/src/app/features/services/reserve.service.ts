import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ReserveService {

  ReserveExist(reserveId: string): boolean {
    return true;
    // this is a call to the ReserveController in the webAPI to get the reserve by the id,
    // and in this case if we get a 200 means that reserve exist, in other case not.
  }

  constructor() { }
}
