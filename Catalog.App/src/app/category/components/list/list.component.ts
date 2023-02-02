import { Pagination } from './../../../../Models/Pagination';
import { CategoryService } from './../../../services/category.service';
import { Response } from './../../../../Models/Response';
import { Category } from '../../../../Models/Category';

import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'cat-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
})
export class ListComponent implements OnInit {
  categoriesResponse$!: Observable<Response<Category[]>>;
  pagination: Pagination = new Pagination('categoryname', 10, 1, false);

  pageNumbers = [0];
  sorttedBy = 'cid';
  isFirst = false;
  isLast = false;
  constructor(private categoryService: CategoryService) {}

  GetCategories(pagination: Pagination) {
    this.categoriesResponse$ = this.categoryService.getCategories(pagination);
    this.categoriesResponse$.subscribe((d) => {
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

  delete(id: string) {
    this.categoryService.deleteCategory(id).subscribe((d) => {
      this.page(1);
    });
  }

  page(page: number) {
    this.pagination.PageNumber = page;
    this.GetCategories(this.pagination);
  }

  next(page: number) {
    if (page < this.pageNumbers[this.pageNumbers.length - 1] + 1)
      this.page(page + 1);
    else this.page(1);
  }

  prev(page: number) {
    if (page > 1) this.page(page - 1);
    else this.page(1);
  }

  sort(sortby: string, page: number) {
    this.pagination.PageNumber = page;

    if (sortby == this.sorttedBy)
      this.pagination.SortDescending = !this.pagination.SortDescending;
    else this.pagination.SortDescending = false;
    if (sortby == 'cname') {
      this.pagination.SortBy = 'categoryname';
      this.sorttedBy = 'cname';
    } else {
      this.pagination.SortBy = 'categoryid';
      this.sorttedBy = 'cid';
    }

    this.GetCategories(this.pagination);
  }
}
