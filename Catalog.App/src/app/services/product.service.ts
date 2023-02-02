import { Category } from './../../Models/Category';
import { ProductS } from './../../Models/ProductS';
import { Product } from './../../Models/Product';
import { Pagination } from './../../Models/Pagination';
import { Response } from './../../Models/Response';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private readonly apiUrl = `https://localhost:${7218}/api`;
  private url = this.apiUrl + '/products';
  constructor(private http: HttpClient) {}

  getAllCategories = () => {
    let urlReq = this.apiUrl + '/category/all';
    return this.http.get<Category[]>(urlReq);
  };

  getProducts = (pagination: Pagination) => {
    let urlReq = this.url;
    return this.http.get<Response<ProductS[]>>(urlReq, {
      params: {
        sortBy: pagination.SortBy,
        pageNumber: pagination.PageNumber,
        isDesc: pagination.SortDescending,
        pageSize: pagination.PageSize,
      },
    });
  };

  addProduct = (productAddForm: object) => {
    console.log(
      '@Product Service, formdata arrived, now trying to push: ',
      productAddForm
    );

    let urlReq = this.url;

    return this.http.post<ProductS>(urlReq, productAddForm);
  };

  updateProduct = (id: string, productAddForm: object) => {
    console.log(
      '@Product Service, formdata arrived, now trying to update: ',
      id,
      'data',
      productAddForm
    );

    let urlReq = this.url + '/' + id;

    return this.http.put<Response<ProductS>>(urlReq, productAddForm);
  };

  deleteProduct = (id: string) => {
    let urlReq = this.url + '/' + id;
    return this.http.delete<object>(urlReq);
  };

  viewProduct = (id: string) => {
    let urlReq = this.url + '/' + id;
    return this.http.get<Product>(urlReq);
  };
  search(key: string) {
    let urlReq = this.url + '/search/?key=' + key;
    return this.http.post<Response<ProductS[]>>(urlReq, {
      pageNumber: 1,
      pageSize: 5,
      sortBy: 'productname',
      isDesc: false,
    });
  }
}
