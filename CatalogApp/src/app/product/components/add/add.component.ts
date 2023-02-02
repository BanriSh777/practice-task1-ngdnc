import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Category } from '../../../../Models/Category';
import {
  FormGroup,
  AbstractControl,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { ProductService } from './../../../services/product.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'cat-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss'],
})
export class AddComponent implements OnInit {
  addProductForm!: FormGroup;
  base64textString = '';
  categoriesList$!: Observable<Category[]>;
  constructor(
    private productService: ProductService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit() {
    this.addProductForm = this.formBuilder.group({
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
  add = (formData: AbstractControl) => {
    this.productService.addProduct(formData).subscribe((d) => console.log(d));
    this.router.navigate(['../products']);
  };
  onSubmit(form: FormGroup) {
    if (form.valid) {
      this.add(form.value);
    } else {
      console.log('form is not valid');
    }
  }

  handleFileSelect(evt: any) {
    var files = evt?.target.files ?? null;
    if (files) var file = files[0];

    if (files && file) {
      var reader = new FileReader();

      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readerEvt: any) {
    var binaryString = readerEvt?.target.result ?? '';
    this.base64textString = btoa(binaryString);
    // console.log(btoa(binaryString));
    this.addProductForm.value['image'] = this.base64textString;
  }
}
