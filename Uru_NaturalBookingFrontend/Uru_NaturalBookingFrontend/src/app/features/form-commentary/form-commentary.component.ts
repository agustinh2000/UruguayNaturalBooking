import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ReviewService } from '../services/review.service';
import { ReviewModelForRequest } from '../../models/ReviewModelForRequest';

@Component({
  selector: 'app-form-commentary',
  templateUrl: './form-commentary.component.html',
  styleUrls: ['./form-commentary.component.css']
})
export class FormCommentaryComponent {

  @Input() reserveId: string;

  public formGroup: FormGroup;

  private reviewService: ReviewService;

  public currentRate: number;

  private reviewToSend: ReviewModelForRequest;

  constructor(aReviewService: ReviewService, private formBuilder: FormBuilder) {
    this.reviewService = aReviewService;
    this.formGroup = this.formBuilder.group({
      commentary: new FormControl('', [Validators.required, this.noWhitespaceValidator])
    });
  }

  noWhitespaceValidator: ValidatorFn = (control: FormControl) => {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  }

  formIsValid(): boolean {
    return this.formGroup.valid && (this.currentRate > 0 && this.currentRate < 6);
  }

  comment(): void{
    if (this.formIsValid()){
      this.reviewToSend = new ReviewModelForRequest();
      this.reviewToSend.IdOfReserveAssociated = this.reserveId;
      this.reviewToSend.Description = this.formGroup.controls.commentary.value;
      this.reviewToSend.Score = this.currentRate;
      this.reviewService.comment(this.reviewToSend);
    }
  }

}
