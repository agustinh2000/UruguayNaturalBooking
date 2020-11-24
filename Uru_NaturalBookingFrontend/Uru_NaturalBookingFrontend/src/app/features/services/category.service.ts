import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryModel } from 'src/app/models/CategoryModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  uri = `${environment.baseUrl}api/categories`;

  constructor(private httpClient: HttpClient) {}

  getCategories(): Observable<CategoryModel> {
    return this.httpClient.get<CategoryModel>(this.uri);
  }
}
