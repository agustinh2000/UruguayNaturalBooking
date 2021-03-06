import { Component, OnInit } from '@angular/core';
import { RegionServiceService } from '../services/region-service.service';
import { Region } from '../../models/Region';
import { Router } from '@angular/router';

@Component({
  selector: 'app-region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.css'],
})
export class RegionComponent implements OnInit {
  public regions;
  private service: RegionServiceService;

  constructor(
    private servicePassed: RegionServiceService,
    private router: Router
  ) {
    this.regions = new Array<Region>();
    this.service = servicePassed;
  }

  ngOnInit(): void {
    this.service.getRegions().subscribe(
      (res) => {
        this.regions = res;
      },
      (err) => {
        alert(err.error);
        console.log(err);
      }
    );
  }

  getSelectionOfTouristSpot(idRegion: string): void {
    this.router.navigate(['touristSpots', idRegion]);
  }
}
