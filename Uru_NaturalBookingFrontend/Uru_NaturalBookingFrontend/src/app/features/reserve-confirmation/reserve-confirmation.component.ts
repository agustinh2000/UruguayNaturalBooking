import { Component, OnInit } from '@angular/core';
import { ReserveModelForResponse } from '../../models/ReserveModelForResponse';
import { ReserveService } from '../services/reserve.service';

@Component({
  selector: 'app-reserve-confirmation',
  templateUrl: './reserve-confirmation.component.html',
  styleUrls: ['./reserve-confirmation.component.css']
})
export class ReserveConfirmationComponent implements OnInit {

  public reserveConfirmation: ReserveModelForResponse;

  private reserveService: ReserveService;

  constructor(aReserveService: ReserveService) {
    this.reserveService = aReserveService;
  }

  ngOnInit(): void {
    this.reserveConfirmation = this.reserveService.getReserveById('id');
  }

}
