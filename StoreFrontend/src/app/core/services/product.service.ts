import { Injectable, inject } from '@angular/core';
import {Observable, of} from 'rxjs';
import { HttpClient, HttpParams } from "@angular/common/http";
import { apiEndpoint } from "../constraints";
import { Product } from "../model/product.model";

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private _httpClient: HttpClient = inject(HttpClient);

  constructor() {}

  getProductsSimulated(searchTerm: string): Observable<any[]> {
    // Enhanced simulated response
    const products = [
      { name: 'Laptop', price: 999, stock: 20 },
      { name: 'Smartphone', price: 699, stock: 50 },
      { name: 'Tablet', price: 499, stock: 30 },
      { name: 'Monitor', price: 300, stock: 15 },
      { name: 'Keyboard', price: 50, stock: 85 },
      { name: 'Mouse', price: 25, stock: 100 },
      { name: 'Webcam', price: 100, stock: 40 },
      { name: 'Headphones', price: 150, stock: 60 }
    ];
    return of(products.filter(product => product.name.toLowerCase().includes(searchTerm.toLowerCase())));
  }

  getProducts(searchTerm: string): Observable<Product[]> {
    // HTTP method to fetch filtered data from the backend
    const params = new HttpParams().set('search', searchTerm); // Assuming 'search' is the query param your API uses
    return this._httpClient.get<Product[]>(`${apiEndpoint}`, { params });
  }
}
