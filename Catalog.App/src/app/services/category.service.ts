import { ProductS } from './../../Models/ProductS';
import { Pagination } from './../../Models/Pagination';
import { Response } from './../../Models/Response';
import { Category } from '../../Models/Category';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  private readonly apiUrl = `https://localhost:${7218}/api`;
  private url = this.apiUrl + '/category';

  constructor(private http: HttpClient) {}

  getAllCategories = () => {
    let urlReq = this.url + '/all';
    return this.http.get<Category[]>(urlReq);
  };

  getProductsUnderCategory = (id: string, pagination: Pagination) => {
    let urlReq = this.apiUrl + '/products/category/' + id;
    return this.http.post<Response<ProductS[]>>(urlReq, pagination);
  };

  getCategories = (pagination: Pagination) => {
    let urlReq = this.url;
    return this.http.get<Response<Category[]>>(urlReq, {
      params: {
        sortBy: pagination.SortBy,
        pageNumber: pagination.PageNumber,
        isDesc: pagination.SortDescending,
        pageSize: pagination.PageSize,
      },
    });
  };

  createCategory = (formdata: object) => {
    console.log(
      '@Category Service, formdata arrived, now trying to push: ',
      formdata
    );
    let urlReq = this.url;
    return this.http.post<Category>(urlReq, formdata);
  };

  updateCategory = (id: string, formdata: object) => {
    console.log(
      '@Category Service, formdata arrived, now trying to update: ',
      id,
      'data:',
      formdata
    );

    let urlReq = this.url + '/' + id;
    return this.http.put<Category>(urlReq, formdata);
  };

  deleteCategory = (id: string) => {
    let urlReq = this.url + '/' + id;
    return this.http.delete<object>(urlReq);
  };

  viewCategory = (id: string) => {
    let urlReq = this.url + '/' + id;
    console.log(urlReq, '@cat service');

    return this.http.get<Category>(urlReq);
  };

  search(key: string) {
    let urlReq = this.url + '/search/?key=' + key;
    return this.http.post<Response<Category[]>>(urlReq, {
      pageNumber: 1,
      pageSize: 5,
      sortBy: 'categoryname',
      isDesc: false,
    });
  }
}
