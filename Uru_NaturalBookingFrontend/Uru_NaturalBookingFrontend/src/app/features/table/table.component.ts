import { Component, Input, OnInit } from '@angular/core';
import { ReportService } from '../services/report.service';
import { ReportModel } from '../../models/ReportModel';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})


export class TableComponent implements OnInit {

  @Input() checkIn: Date;
  @Input() checkOut: Date;
  @Input() touristSpot: string;

  reportService: ReportService;

  dataSource;

  displayedColumns = ['Lodging', 'QuantityOfReserves'];

  constructor(aReportService: ReportService) {
    this.dataSource = new Array();
    this.reportService = aReportService;
  }

  ngOnInit(): void {
   this.generateReport();
  }

  private generateReport(): void{
    this.reportService.getLodgingsForReport(this.checkIn, this.checkOut, this.touristSpot).subscribe(
      res => {
        this.dataSource = res;
      },
      (err) => {
        alert(err.error);
      }
    );
  }

}
