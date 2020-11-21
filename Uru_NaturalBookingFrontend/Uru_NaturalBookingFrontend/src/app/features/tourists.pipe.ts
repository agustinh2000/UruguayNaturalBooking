import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'tourists'
})
export class TouristsPipe implements PipeTransform {

  transform(value: number[]): string {
    let textToReturn = '';
    for (let i = 0; i < value.length; i++) {
      if (value[i] !== 0) {
        if (i === 0) {
          textToReturn += ' ' + value[i] + ' adultos,';
        }
        else if (i === 1) {
          textToReturn += ' ' + value[i] + ' niños,';
        }
        else if (i === 2) {
          textToReturn += ' ' + value[i] + ' bebés,';
        }
        else {
          textToReturn += ' ' + value[i] + ' jubilados';

        }
      }
    }
    return textToReturn;
  }

}
