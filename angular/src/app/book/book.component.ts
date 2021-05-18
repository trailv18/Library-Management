import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  BookServiceProxy,
  Book,
  BookDto,
  GetBookDto,
} from '@shared/service-proxies/service-proxies';
import { CreateBookComponent } from './create-book/create-book.component';
import { EditBookComponent } from './edit-book/edit-book.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  animations: [appModuleAnimation()]

})
export class BookComponent extends AppComponentBase implements OnInit {

  books: GetBookDto[];
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
    private _bookService: BookServiceProxy,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this._bookService
      .getBookByPage(this.name, this.pageIndex, this.pageSize)
      .subscribe(response => {
        console.log(response);
        this.books = response.items;
        this.count = response.count;
        this.pageIndex = response.pageIndex;
        this.pageSize = response.pageSize;
      }
      );
  }

  changePageSize(size: number) {
    this.pageIndex = 1;
    this.pageSize = size;
    this.list();
  }

  filter() {
    if (!this.name) {
      this.pageIndex = 1;
      this.list();
    }
    else {
      this.pageIndex = 1;
      this.list();
    }
  }

  setup() {
    this.pageIndex = 1;
    this.name = undefined;
    this.list();
  }

  refresh() {
    this.pageIndex = 1;
    this.list();
  }

  onChangePage(event) {
    this.pageIndex = event;
    this.list();
  }

  delete(book: Book): void {
    abp.message.confirm(
      this.l('Delete warning message'),
      undefined,
      (result: boolean) => {
        if (result) {
          console.log(book.id);
          this._bookService
            .deleteBook(book.id)
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

  createBook(): void {
    this.showCreateOrEditTenantDialog();
  }

  editBook(book: BookDto): void {
    this.showCreateOrEditTenantDialog(book.id);
  }


  showCreateOrEditTenantDialog(id?: string): void {
    let createOrEditTenantDialog: BsModalRef;
    if (!id) {
      createOrEditTenantDialog = this._modalService.show(
        CreateBookComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditTenantDialog = this._modalService.show(
        EditBookComponent,
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
