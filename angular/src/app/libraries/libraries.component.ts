import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  LibraryServiceProxy,
  LibraryDto,
  GetLibraryDto,
  Library,
} from '@shared/service-proxies/service-proxies';
import { CreateLibraryComponent } from './create-library/create-library.component';
import { EditLibraryComponent } from './edit-library/edit-library.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-libraries',
  templateUrl: './libraries.component.html',
  animations: [appModuleAnimation()]

})
export class LibrariesComponent extends AppComponentBase implements OnInit {

  libraries: GetLibraryDto[];
  name:string;
  advancedFiltersVisible = false;

  pageIndex: number = 1;;
  count: number;
  pageSize: number= 9;

  listPageSize = [
    { size: 3 },
    { size: 6 },
    { size: 9 },
    { size: 12 },
  ];

  constructor(
    injector: Injector,
    private _libraryService: LibraryServiceProxy,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this._libraryService
      .getLibraryByPage(this.name, this.pageIndex, this.pageSize)
      .subscribe(response => {        
        this.libraries = response.items;
        this.count = response.count;
        this.pageIndex = response.pageIndex;
        this.pageSize = response.pageSize;
      });
  }

  changePageSize(size: number) {
    this.pageIndex = 1;
    this.pageSize = size;
    this.list();
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

  setup(){
    this.pageIndex =1;
    this.name = undefined;
    this.list();
  }

  onChangePage(event) {
    this.pageIndex = event;
    this.list()
  }

  refresh(){
    this.pageIndex=1;
    this.list();
  }

  delete(library: Library): void {
    abp.message.confirm(
      this.l('DeleteWarning message', library.name),
      undefined,
      (result: boolean) => {
        if (result) {
          console.log(library.id);
          this._libraryService
            .deleteBook(library.id)
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

  createLibrary(): void {
    this.showCreateOrEditTenantDialog();
  }

  editLibrary(library: LibraryDto): void {
    console.log(library.id)
    this.showCreateOrEditTenantDialog(library.id);
  }


  showCreateOrEditTenantDialog(id?: string): void {
    let createOrEditTenantDialog: BsModalRef;
    if (!id) {
      createOrEditTenantDialog = this._modalService.show(
        CreateLibraryComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditTenantDialog = this._modalService.show(
        EditLibraryComponent,
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
