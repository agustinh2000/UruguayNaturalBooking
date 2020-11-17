import { Injectable } from '@angular/core';
import { CategoryModel } from 'src/app/models/CategoryModel';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  readonly categories: CategoryModel[] = [
    { Id: '1d18fa47-0e81-403a-8ca5-48f16a1c8895',
      Name: 'Playa'},
      {Id: 'f5381353-2ae5-4675-8757-531ad8cf9093',
      Name: 'Monte'},
      {Id: 'a41a014b-9135-470c-a418-e8eb329b92b7',
      Name: 'Compras'},
      {Id: '6714839a-ccf8-4e55-b858-476a9fe68606',
      Name: 'Atracciones'},
      {Id: '6714839a-ccf8-4e55-b858-476a9fe68608',
      Name: 'Shopping'},
      {Id: '6714839a-ccf8-4e55-b858-476a9fe68616',
      Name: 'Rutas'}
  ];

  constructor() { }

  getCategories(): CategoryModel[] {
    const categoriesObteined: CategoryModel[] = [];
    for (const category of this.categories){
      categoriesObteined.push(category);
    }
    return categoriesObteined;
  }
}
