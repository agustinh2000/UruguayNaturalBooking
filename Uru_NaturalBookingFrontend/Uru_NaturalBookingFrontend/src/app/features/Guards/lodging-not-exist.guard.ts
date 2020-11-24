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
    /*if (!this.lodgingService.isValidLodging(this.idLodging)) {
      alert('Error. El hospedaje buscado no existe.');
      this.router.navigate(['regions']);
      return false;
    }
    return true;
    */
    return true;
  }
}
