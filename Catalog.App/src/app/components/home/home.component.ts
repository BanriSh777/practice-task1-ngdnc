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
  hideCatSearchResults = false;
  hideProdSearchResults = false;
  constructor(private searchService: SearchService) {}
  search(event: any) {
    var key = event.target.value;
    if (key?.trim() && key) {
      let x = this.searchService.search(key);
      console.log(this.searchService.search(key));
      if (x) {
        this.categories$ = x[0];
        this.products$ = x[1];
        this.hideProdSearchResults = false;
        this.hideCatSearchResults = false;
      }
    } else {
      this.hideCatSearchResults = true;
      this.hideProdSearchResults = true;
    }
  }
}
