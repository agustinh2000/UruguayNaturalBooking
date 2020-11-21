import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ReserveModelForResponse } from '../../models/ReserveModelForResponse';
import { ReserveService } from '../services/reserve.service';

@Component({
  selector: 'app-reserve-confirmation',
  templateUrl: './reserve-confirmation.component.html',
  styleUrls: ['./reserve-confirmation.component.css'],
})
export class ReserveConfirmationComponent implements OnInit {
  public reserveConfirmation: ReserveModelForResponse;

  private reserveService: ReserveService;

  private reserveId: string;

  constructor(
    aReserveService: ReserveService,
    private currentRoute: ActivatedRoute
  ) {
    this.reserveService = aReserveService;
  }

  ngOnInit(): void {
    this.reserveId = this.currentRoute.snapshot.params.idReserve;
    this.reserveConfirmation = this.reserveService.getReserveById(
      this.reserveId
    );
  }
}
