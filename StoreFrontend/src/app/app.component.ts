import { Component } from '@angular/core';
import { ProductService } from './core/services/product.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'StoreFrontend';

  searchTerm: string = '';
  products: any[] = [];

  constructor(private productService: ProductService) {}

  searchProducts() {
    if (this.searchTerm) {
      this.productService.getProducts(this.searchTerm).subscribe(
        data => {
          this.products = data;
        },
        error => {
          console.error('Error fetching products:', error);
        }
      );
    }
  }

}
