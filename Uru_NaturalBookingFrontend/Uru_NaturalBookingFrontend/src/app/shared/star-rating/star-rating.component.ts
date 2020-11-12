import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-star-rating',
  templateUrl: './star-rating.component.html',
  styleUrls: ['./star-rating.component.css']
})
export class StarRatingComponent{

  @Output() ratingToSent = new EventEmitter<number>();

  currentRate: number;

  constructor() { }

  send($event){
    this.ratingToSent.emit(this.currentRate);
  }

}
