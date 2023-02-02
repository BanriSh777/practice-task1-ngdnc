import { Pagination } from './../../../../Models/Pagination';
import { Response } from './../../../../Models/Response';
import { ProductS } from './../../../../Models/ProductS';
import { Observable } from 'rxjs';
import { CategoryService } from './../../../services/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Category } from 'src/Models/Category';
import {
  FormGroup,
  AbstractControl,
  FormBuilder,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'cat-view-category',
  templateUrl: './view-category.component.html',
  styleUrls: ['./view-category.component.scss'],
})
export class ViewCategoryComponent implements OnInit {
  category$!: Observable<Category>;

  products$!: Observable<Response<ProductS[]>>;
  pageNumbers = [0];
  sorttedBy = 'pid';
  isFirst = false;
  isLast = false;
  pagination = new Pagination('productid', 10, 1, false);

  id!: string;

  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoryService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') ?? '';
    this.category$ = this.categoryService.viewCategory(this.id);
    this.loadCategory();
  }

  loadCategory() {
    this.category$.subscribe((data) => {
      console.log('data for id :', this.id, '=>', data);
    });
    this.page(1);
  }

  getProducts(pagination: Pagination) {
    this.products$ = this.categoryService.getProductsUnderCategory(
      this.id,
      pagination
    );
    this.products$.subscribe((d) => {
      this.pageNumbers = Array(d.page.totalPages)
        .fill(1)
        .map((x, i) => i);

      this.isFirst = d.page.pageNumber == 1;
      this.isLast = d.page.pageNumber == d.page.totalPages;
    });
  }

  page(page: number) {
    this.pagination.PageNumber = page;
    this.getProducts(this.pagination);
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
}
