import { Router } from '@angular/router';
import { CategoryService } from './../../../services/category.service';
import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  AbstractControl,
  FormBuilder,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'cat-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss'],
})
export class AddComponent implements OnInit {
  addCategoryForm!: FormGroup;
  constructor(
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit() {
    this.addCategoryForm = this.formBuilder.group({
      categoryName: ['', Validators.required],
      categoryDescription: [''],
    });
  }

  add = (formData: AbstractControl) => {
    this.categoryService
      .createCategory(formData)
      .subscribe((d) => console.log(d));
    this.router.navigate(['../category']);
  };

  onSubmit(form: FormGroup) {
    if (form.valid) {
      this.add(form.value);
    } else {
      console.log('form is not valid');
    }
  }
}
