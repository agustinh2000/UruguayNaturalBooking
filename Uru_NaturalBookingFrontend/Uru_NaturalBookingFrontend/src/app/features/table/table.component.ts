import { Component, OnInit } from '@angular/core';
import { ReportService } from '../services/report.service';
import { ReportModel } from '../../models/ReportModel';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {

   reportService: ReportService;

  dataSource: ReportModel[];

  displayedColumns = ['Lodging', 'QuantityOfReserves'];

  constructor(aReportService: ReportService) {
    this.reportService = aReportService;
  }

  ngOnInit(): void {
   this.dataSource = this.reportService.getLodgingsForReport();
  }

}
