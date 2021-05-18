import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  CategoryServiceProxy,
  CategoryDto,
  Category,
  GetCategoryDto,
  GetCategoryDtoPageResult
} from '@shared/service-proxies/service-proxies';
import { CreateCategoryComponent } from './create-category/create-category.component';
import { EditCategoryComponent } from './edit-category/edit-category.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';


@Component({
  selector: 'app-categories',
  templateUrl: './category.component.html',
  animations: [appModuleAnimation()]

})

export class CategoryComponent extends AppComponentBase implements OnInit {

  categories: CategoryDto[];
  name : string;
  advancedFiltersVisible = false;
  pageIndex: number = 1;;
  count: number;
  pageSize: number = 9;

  listPageSize = [
    { size: 3 },
    { size: 6 },
    { size: 9 },
    { size: 12 },
  ];

  constructor(
    injector: Injector,
    private _categoryService: CategoryServiceProxy,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this
      ._categoryService
      .getCategoryByPage(this.name, this.pageIndex, this.pageSize)
      .subscribe(
        response => {
          console.log(response);
          this.categories = response.items;
          this.count = response.count;
          this.pageIndex = response.pageIndex;
          this.pageSize = response.pageSize;          
        }
      );
  }

  filter(){
    if(!this.name){
      this.pageIndex =1;
      this.list();      
    }
    else{
      this.pageIndex =1;
      this.list();
    }
  }

  changePageSize(size: number) {
    this.pageIndex = 1;
    this.pageSize = size;
    this.list();
  }

  onChangePage(event) {
    this.pageIndex = event;
    this.list();
  }

  refresh(){
    this.pageIndex =1;
    this.list();
  }

  setup(){
    this.pageIndex =1;
    this.name = undefined;
    this.list();
  }

  delete(category: Category): void {
    abp.message.confirm(
      this.l('Category delete warning message', category.name),
      undefined,
      (result: boolean) => {
        if (result) {
          console.log(category.id);
          this._categoryService
            .deleteCategory(category.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('Successfully deleted'));
                this.list();
              })
            )
            .subscribe(() => { });
        }
      }
    );
  }

  createCategory(): void {
    this.showCreateOrEditTenantDialog();
  }

  editCategory(category: CategoryDto): void {
    console.log(category.id)
    this.showCreateOrEditTenantDialog(category.id);
  }


  showCreateOrEditTenantDialog(id?: string): void {
    let createOrEditTenantDialog: BsModalRef;
    if (!id) {
      createOrEditTenantDialog = this._modalService.show(
        CreateCategoryComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditTenantDialog = this._modalService.show(
        EditCategoryComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }
    createOrEditTenantDialog.content.onSave.subscribe(() => {
      this.list();
    })
  }

}
