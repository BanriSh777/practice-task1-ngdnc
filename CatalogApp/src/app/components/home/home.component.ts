import { ProductS } from './../../../Models/ProductS';
import { Category } from './../../../Models/Category';
import { Response } from './../../../Models/Response';
import { Observable, of } from 'rxjs';
import { SearchService } from './../../services/search.service';
import { Component } from '@angular/core';

@Component({
  selector: 'cat-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  categories$!: Observable<Response<Category[]>>;
  products$!: Observable<Response<ProductS[]>>;
  constructor(private searchService: SearchService) {}
  search(event: any) {
    console.log(this.searchService.search(event.target.value));
    let x = this.searchService.search(event.target.value);
    if (x) {
      this.categories$ = x[0];
      this.products$ = x[1];
    } else {
    }
  }
}
