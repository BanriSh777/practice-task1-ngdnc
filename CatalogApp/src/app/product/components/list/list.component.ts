import { Pagination } from '../../../../Models/Pagination';
import { ProductS } from '../../../../Models/ProductS';
import { Response } from './../../../../Models/Response';

import { ProductService } from './../../../services/product.service';

import { Component } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'cat-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
})
export class ListComponent {
  productsResponse$!: Observable<Response<ProductS[]>>;
  pageNumbers = [0];
  sorttedBy = 'pid';
  isFirst = false;
  isLast = false;

  pagination = new Pagination('productid', 10, 1, false);

  constructor(private productService: ProductService) {}

  GetProducts(sortPagesProducts: Pagination) {
    this.productsResponse$ = this.productService.getProducts(sortPagesProducts);
    this.productsResponse$.subscribe((d) => {
      this.pageNumbers = Array(d.page.totalPages)
        .fill(1)
        .map((x, i) => i);
      this.isFirst = d.page.pageNumber == 1;
      this.isLast = d.page.pageNumber == d.page.totalPages;
    });
  }

  ngOnInit() {
    this.page(1);
  }

  page(page: number) {
    this.pagination.PageNumber = page;
    this.GetProducts(this.pagination);
  }

  next(page: number) {
    if (page < this.pageNumbers.length) this.page(page + 1);
    else this.page(1);
  }

  prev(page: number) {
    if (page > 1) this.page(page - 1);
    else this.page(1);
  }

  sort(sortby: string, page: number) {
    this.pagination.PageNumber = page;

    if (this.sorttedBy == sortby)
      this.pagination.SortDescending = !this.pagination.SortDescending;
    else this.pagination.SortDescending = false;

    if (sortby == 'cid') {
      this.pagination.SortBy = 'categoryid';
      this.sorttedBy = 'cid';
    } else if (sortby == 'pid') {
      this.pagination.SortBy = 'productid';
      this.sorttedBy = 'pid';
    } else if (sortby == 'price') {
      this.pagination.SortBy = 'price';
      this.sorttedBy = 'price';
    } else if (sortby == 'cname') {
      this.pagination.SortBy = 'categoryname';
      this.sorttedBy = 'cname';
    } else if (sortby == 'pname') {
      this.pagination.SortBy = 'productname';
      this.sorttedBy = 'pname';
    } else {
      this.pagination.SortBy = 'productid';
      this.sorttedBy = 'pid';
    }

    this.GetProducts(this.pagination);
  }

  delete(pid: string) {
    this.productService.deleteProduct(pid).subscribe((d) => {
      this.page(1);
    });
  }
}
