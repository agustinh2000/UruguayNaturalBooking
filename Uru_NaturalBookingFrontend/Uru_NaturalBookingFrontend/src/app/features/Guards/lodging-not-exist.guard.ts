import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from '@angular/router';
import { Observable } from 'rxjs';
import { LodgingService } from '../services/lodging.service';

@Injectable({
  providedIn: 'root',
})
export class LodgingNotExistGuard implements CanActivate {
  constructor(private router: Router, private lodgingService: LodgingService) {}

  private idLodging: string;

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    this.idLodging = route.queryParams.LodgingId;
    return new Observable<boolean | UrlTree>((obs) => {
      this.lodgingService.getLodgingById(this.idLodging).subscribe(
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
