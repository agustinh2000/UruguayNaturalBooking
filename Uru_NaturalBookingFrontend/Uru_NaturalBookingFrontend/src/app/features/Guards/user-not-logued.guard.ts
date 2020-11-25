import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';

@Injectable({
  providedIn: 'root',
})
export class UserNotLoguedGuard implements CanActivate {
  constructor(private usersService: UserService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    if (this.usersService.isLogued()) {
      return true;
    } else {
      alert(
        'Error. Necesita estar logueado para hacer uso de esta funcionalidad. Sera redirigido al inicio de sesión.'
      );
      this.router.navigate(['/login']);
      return false;
    }
  }
}
