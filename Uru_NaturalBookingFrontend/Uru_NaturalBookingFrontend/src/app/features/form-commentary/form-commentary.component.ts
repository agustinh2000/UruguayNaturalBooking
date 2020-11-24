import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ReviewService } from '../services/review.service';
import { ReviewModelForRequest } from '../../models/ReviewModelForRequest';
import { Router } from '@angular/router';

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

  constructor(aReviewService: ReviewService, private formBuilder: FormBuilder,
              private router: Router) {
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

  getErrorMessage(): string {
    if (this.formGroup.controls.commentary.hasError('required')) {
      return 'Error. El comentario es requerido.';
    }
    return this.formGroup.controls.commentary.hasError('whitespace')
      ? 'Error. El comentario ingresado no puede ser vacío.'
      : '';
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
      this.sendComment(this.reviewToSend);
    }
  }

  sendComment(reviewToSend: ReviewModelForRequest): void{
    this.reviewService.comment(reviewToSend).subscribe(
      (res) => {
        alert('Comentario enviado con exito.');
        this.router.navigate(['/regions']);
      },
      (err) => {
        alert(err.error);
      }
    );
  }

}
