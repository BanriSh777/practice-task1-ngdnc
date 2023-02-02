import { ProductComponent } from './product.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ListComponent } from './components/list/list.component';
import { ViewComponent } from './components/view-product/view.component';
import { AddComponent } from './components/add/add.component';
import { UpdateComponent } from './components/update/update.component';

const routes: Routes = [
  {
    path: '',
    component: ProductComponent,
    children: [
      {
        path: '',
        component: ListComponent,
      },
      {
        path: 'view/:id',
        component: ViewComponent,
      },
      {
        path: 'update/:id',
        component: UpdateComponent,
      },

      {
        path: 'add',
        component: AddComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductRoutingModule {}
