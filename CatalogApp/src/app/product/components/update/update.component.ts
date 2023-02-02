import { Category } from './../../../../Models/Category';
import { Observable } from 'rxjs';
import { Product } from './../../../../Models/Product';
import { ProductService } from './../../../services/product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'cat-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss'],
})
export class UpdateComponent {
  id!: string;
  updateProductForm!: FormGroup;
  product$!: Observable<Product>;
  categoriesList$!: Observable<Category[]>;
  base64textString = '';

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {}
  loadProductDetails = () => {
    this.product$.subscribe((data) => {
      console.log(data);
      this.updateProductForm.patchValue({
        productName: data.productName,
        categoryId: data.categoryId,
        description: data.description,
        price: data.price,
        mrp: data.mrp,
        discount: data.discount,
        depth: data.depth,
        height: data.height,
        width: data.width,
        image: data.image,
      });
    });
  };
  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') ?? '';
    this.product$ = this.productService.viewProduct(this.id);
    this.loadProductDetails();

    this.updateProductForm = this.formBuilder.group({
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

  update(id: string, formData: AbstractControl) {
    console.log('to be updated: ', id);
    console.log('data : ', formData);
    this.productService
      .updateProduct(id, formData)
      .subscribe((d) => console.log(d));
    this.router.navigate(['/products/view/' + this.id]);
  }

  onSubmit(form: FormGroup) {
    if (form.valid) {
      this.update(this.id, form.value);
    } else {
      console.log('form is not valid');
    }
  }

  delete(id: string) {
    this.productService.deleteProduct(id).subscribe((d) => {
      this.router.navigate(['products']);
    });
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
    this.updateProductForm.value['image'] = this.base64textString;
  }
}
