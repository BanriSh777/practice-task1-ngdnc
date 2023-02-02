import { CategoryComponent } from './category.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ListComponent } from './components/list/list.component';
import { AddComponent } from './components/add/add.component';
import { UpdateComponent } from './components/update/update.component';
import { ViewCategoryComponent } from './components/view-category/view-category.component';

const routes: Routes = [
  {
    path: '',
    component: CategoryComponent,
    children: [
      { path: '', component: ListComponent },
      { path: 'add', component: AddComponent },
      { path: 'view/:id', component: ViewCategoryComponent },
      { path: 'update/:id', component: UpdateComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CategoryRoutingModule {}
