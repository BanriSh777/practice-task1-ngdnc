import { Observable } from 'rxjs';
import { Product } from './../../../../Models/Product';
import { ProductService } from './../../../services/product.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Category } from '../../../../Models/Category';
import {
  FormGroup,
  AbstractControl,
  FormBuilder,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'cat-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.scss'],
})
export class ViewComponent implements OnInit {
  product$!: Observable<Product>;
  updateForm!: FormGroup;
  categoriesList$!: Observable<Category[]>;
  id!: string;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    if (id == null) this.router.navigate(['../products']);
    else {
      this.product$ = this.productService.viewProduct(id);
      this.id = id;
    }

    this.loadProductDetails();

    this.updateForm = this.formBuilder.group({
      productName: ['', Validators.required],
      categoryId: ['', Validators.required],
      description: [''],
      price: [0.0, Validators.required],
      mrp: [0.0, Validators.required],
      discount: [0.0],
      height: [0.0],
      depth: [0.0],
      width: [0.0],
      image: [''],
    });

    this.categoriesList$ = this.productService.getAllCategories();
  }

  loadProductDetails = () => {
    this.product$.subscribe((d) => console.log(d));
  };

  update = (id: string, formData: AbstractControl) => {
    this.productService
      .updateProduct(id, formData)
      .subscribe((d) => console.log(d));
    this.router.navigate([this.router.url]);
  };
  onSubmit(form: FormGroup) {
    if (form.valid) {
      this.update(this.id, form.value);
    } else {
      console.log('form is not valid');
    }
  }
}
