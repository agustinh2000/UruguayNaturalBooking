import { Component, OnInit } from '@angular/core';
import { RegionServiceService } from '../services/region-service.service';
import { Region } from '../../models/region';

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
