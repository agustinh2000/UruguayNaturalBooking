import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from '@angular/router';
import { Observable } from 'rxjs';
import { RegionServiceService } from '../services/region-service.service';

@Injectable({
  providedIn: 'root',
})
export class RegionNotExistGuard implements CanActivate {
  constructor(
    private router: Router,
    private regionService: RegionServiceService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const idRegion = route.url[1].path;
    return new Observable<boolean | UrlTree>((obs) => {
      this.regionService.getRegionById(idRegion).subscribe(
        (res) => {
          obs.next(true);
        },
        (err) => {
          alert(err.error);
          obs.next(false);
          this.router.navigate(['/regions']);
        }
      );
    });
  }
}
