import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryComponent } from './category.component';
import { ListComponent } from './components/list/list.component';
import { AddComponent } from './components/add/add.component';
import { ViewCategoryComponent } from './components/view-category/view-category.component';
import { HttpClientModule } from '@angular/common/http';
import { UpdateComponent } from './components/update/update.component';

@NgModule({
  declarations: [
    CategoryComponent,
    ListComponent,
    AddComponent,
    ViewCategoryComponent,
    UpdateComponent,
  ],
  imports: [
    CommonModule,
    CategoryRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class CategoryModule {}
