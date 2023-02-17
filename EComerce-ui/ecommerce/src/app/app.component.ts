import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Pagination } from './models/pagination';
import { Product } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'ecommerce';
  products: Product[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http
      .get<Pagination<Product[]>>(
        'https://localhost:7067/api/products/get-products?pageSize=50'
      )
      .subscribe({
        next: (response) => (this.products = response.data),
        error: (error) => console.log(error),
        complete: () => {
          console.log('request completed');
        },
      });
  }
}
