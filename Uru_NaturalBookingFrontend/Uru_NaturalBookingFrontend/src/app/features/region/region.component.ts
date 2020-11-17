import { Component, OnInit } from '@angular/core';
import { RegionServiceService } from '../services/region-service.service';
import { Region } from '../../models/Region';

@Component({
  selector: 'app-region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.css']
})
export class RegionComponent implements OnInit {

  regions: Region[];
  private service: RegionServiceService;

  constructor(private servicePassed: RegionServiceService) {
    this.service = servicePassed;
  }

  ngOnInit(): void {
    this.regions = this.service.getRegions();
  }

}
