import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/features/services/user.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
})
export class NavBarComponent implements OnInit {
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
    this.usersService.logout();
    localStorage.clear();
    this.logued = false;
  }

  onActivate($event): void {
    this.isLogued();
  }
}
