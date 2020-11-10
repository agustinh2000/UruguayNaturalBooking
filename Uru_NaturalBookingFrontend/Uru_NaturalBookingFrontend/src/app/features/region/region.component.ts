import { Component, OnInit } from '@angular/core';
import { Region } from './region';
import { RegionServiceService } from '../services/region-service.service';

@Component({
  selector: 'app-region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.css']
})
export class RegionComponent implements OnInit {

  regions: Region[];

  constructor(private service: RegionServiceService) {
    this.regions = service.getRegions();
  }

  ngOnInit(): void {
  }

}
