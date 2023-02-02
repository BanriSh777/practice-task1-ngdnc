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
  selector: 'cat-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss'],
})
export class UpdateComponent implements OnInit {
  id!: string;
  updateCategoryForm!: FormGroup;
  category$!: Observable<Category>;

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

    this.updateCategoryForm = this.formBuilder.group({
      categoryName: ['', Validators.required],
      categoryId: ['', Validators.required],
      categoryDescription: [''],
    });
  }

  loadCategory() {
    this.category$.subscribe((data) => {
      console.log(data);
      this.updateCategoryForm.patchValue({
        categoryName: data.categoryName,
        categoryId: data.categoryId,
        categoryDescription: data.categoryDescription,
      });
    });
  }

  update(id: string, formData: AbstractControl) {
    console.log('to be updated: ', id);
    console.log('data : ', formData);
    this.categoryService
      .updateCategory(id, formData)
      .subscribe((d) => console.log(d));
    this.router.navigate(['/category/view/' + this.id]);
  }

  onSubmit(form: FormGroup) {
    if (form.valid) {
      this.update(this.id, form.value);
    } else {
      console.log('form is not valid');
    }
  }

  delete(id: string) {
    this.categoryService.deleteCategory(id).subscribe((d) => {
      this.router.navigate(['category']);
    });
  }
}
