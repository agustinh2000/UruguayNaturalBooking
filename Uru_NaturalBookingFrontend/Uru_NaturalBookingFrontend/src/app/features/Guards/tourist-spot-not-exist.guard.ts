import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from '@angular/router';
import { Observable } from 'rxjs';
import { TouristSpotService } from '../services/tourist-spot.service';

@Injectable({
  providedIn: 'root',
})
export class TouristSpotNotExistGuard implements CanActivate {
  constructor(
    private router: Router,
    private touristSpotService: TouristSpotService
  ) {}

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
    return new Observable<boolean | UrlTree>((obs) => {
      this.touristSpotService.getTouristSpotById(idTouristSpot).subscribe(
        (res) => {
          obs.next(true);
        },
        (err) => {
          alert(err.error);
          obs.next(false);
          this.router.navigate(['touristSpots', idRegion]);
        }
      );
    });
  }
}
