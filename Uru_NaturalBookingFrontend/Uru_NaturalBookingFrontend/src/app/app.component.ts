import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './features/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Uru_NaturalBookingFrontend';

  pathLogo = '../assets/img/UruguayNatural.png';

  public logued: boolean = false;

  constructor(private router: Router, private usersService: UserService) {}

  ngOnInit(): void {
    this.isLogued();
  }

  navigateToRegions(): void {
    this.router.navigate(['regions']);
  }

  isLogued(): void {
    this.logued = this.usersService.isLogued();
  }

  logout(): void {
    this.usersService.logout().subscribe(
      (res: string) => {
        alert(res);
        this.router.navigate(['/regions']);
      },
      (err) => {
        alert(err.error);
        console.log(err);
      }
    );
    localStorage.clear();
    this.logued = false;
  }

  onActivate($event): void {
    this.isLogued();
  }
}
