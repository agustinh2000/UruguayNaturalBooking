import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from '@angular/router';
import { Observable } from 'rxjs';
import { ReserveService } from '../services/reserve.service';

@Injectable({
  providedIn: 'root',
})
export class ReserveNotExistGuard implements CanActivate {
  constructor(private router: Router, private reserveService: ReserveService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const idReserve = route.url[1].path;
    if (!this.reserveService.reserveExist(idReserve)) {
      alert('Ups no existe la reserva buscada.');
      this.router.navigate(['regions']);
      return false;
    }
    return true;
  }
}
