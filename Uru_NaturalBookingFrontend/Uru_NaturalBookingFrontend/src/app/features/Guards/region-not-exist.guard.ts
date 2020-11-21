import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RegionServiceService } from '../services/region-service.service';

@Injectable({
  providedIn: 'root'
})
export class RegionNotExistGuard implements CanActivate {

  constructor(private router: Router, private regionService: RegionServiceService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
    ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const idRegion = route.url[1].path;
    if (!this.regionService.isValidRegion(idRegion)) {
      alert('Ups no existe la región seleccionada.');
      this.router.navigate(['regions']);
      return false;
    }
    return true;
  }
}
