import { Response } from './../../Models/Response';
import { Observable, of } from 'rxjs';
import { ProductS } from './../../Models/ProductS';
import { CategoryService } from './category.service';
import { Category } from './../../Models/Category';
import { ProductService } from './product.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  categories$!: Observable<Response<Category[]>>;
  products$!: Observable<Response<ProductS[]>>;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService
  ) {}

  search(
    key: string
  ): [Observable<Response<Category[]>>, Observable<Response<ProductS[]>>] {
    if (key?.trim() && key) {
      this.products$ = this.productService.search(key);
      this.categories$ = this.categoryService.search(key);
    }
    return [this.categories$, this.products$];
  }
}
