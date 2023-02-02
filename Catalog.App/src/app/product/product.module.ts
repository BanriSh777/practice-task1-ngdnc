import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductComponent } from './product.component';
import { ListComponent } from './components/list/list.component';
import { AddComponent } from './components/add/add.component';
import { ViewComponent } from './components/view-product/view.component';
import { UpdateComponent } from './components/update/update.component';

@NgModule({
  declarations: [ProductComponent, ListComponent, AddComponent, ViewComponent, UpdateComponent],
  imports: [
    CommonModule,
    ProductRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class ProductModule {}
