import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { TouristSpotService } from '../services/tourist-spot.service';

@Injectable({
  providedIn: 'root'
})
export class TouristSpotNotExistGuard implements CanActivate {

  constructor(private router: Router, private touristSpotService: TouristSpotService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
    ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const idTouristSpot = route.url[1].path;
    const idRegion = route.url[2].path;
    if (!this.touristSpotService.isValidTouristSpot(idTouristSpot)) {
      alert('Ups no existe el punto turístico seleccionado, espere a que sea agregado como un atractivo turístico proximamente.');
      this.router.navigate(['touristSpots', idRegion]);
      return false;
    }
    return true;
  }
}
