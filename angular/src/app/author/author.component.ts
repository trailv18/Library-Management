import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  AuthorServiceProxy,
  AuthorDto,
  GetAuthorDto
} from '@shared/service-proxies/service-proxies';
import { CreateAuthorComponent } from './create-author/create-author.component';
import { EditAuthorComponent } from './edit-author/edit-author.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';


@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  animations: [appModuleAnimation()]

})
export class AuthorComponent extends AppComponentBase implements OnInit {

  authors: GetAuthorDto[];
  name: string;
  advancedFiltersVisible = false;

  pageIndex: number = 1;
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
    private _authorService: AuthorServiceProxy,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this._authorService
      .getAuthorByPage(this.name, this.pageIndex, this.pageSize)
      .subscribe(response => {
        this.authors = response.items;
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
  
  setup(){
    this.pageIndex =1;
    this.name = undefined;
    this.list();
  }

  refresh(){
    this.pageIndex=1;
    this.list();
  }

  delete(author: AuthorDto): void {
    abp.message.confirm(
      this.l('Author delete warning message', author.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._authorService
            .deleteAuthor(author.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('Successfully deleted'));
                this.list()
              })
            )
            .subscribe(() => { });
        }
      }
    );
  }

  createAuthor(): void {
    this.showCreateOrEditTenantDialog();
  }

  editAuthor(author: AuthorDto): void {
    console.log(author.id)
    this.showCreateOrEditTenantDialog(author.id);
  }


  showCreateOrEditTenantDialog(id?: string): void {
    let createOrEditTenantDialog: BsModalRef;
    if (!id) {
      createOrEditTenantDialog = this._modalService.show(
        CreateAuthorComponent,
        {
          class: 'modal-lg',
        }
      );

    } else {
      createOrEditTenantDialog = this._modalService.show(
        EditAuthorComponent,
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
