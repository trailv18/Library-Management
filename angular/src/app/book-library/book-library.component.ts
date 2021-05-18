import {
  Component,
  Injector,
  OnInit,
} from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import { finalize } from 'rxjs/operators';

import {
  BookLibraryServiceProxy,
  BorrowBookDetailDto,
  GetAllBookOfLibraryDto,
  LibraryDto,
  LibraryServiceProxy,
  BorrowBookDetaiServiceProxy,
  AuthorServiceProxy,
  PublisherServiceProxy,
  CategoryServiceProxy,
  PublisherDto,
  AuthorDto,
  CategoryDto,
} from '@shared/service-proxies/service-proxies';

import { ActivatedRoute } from '@angular/router'
import { CreateBooklibraryComponent } from './create-book-library/create-book-library.component';
import { EditBookLibraryComponent } from './edit-book-library/edit-book-library.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-book-library',
  templateUrl: './book-library.component.html',
  animations: [appModuleAnimation()]
})
export class BookLibraryComponent extends AppComponentBase implements OnInit {

  bookLibraries: GetAllBookOfLibraryDto[];
  advancedFiltersVisible = false;
  id: string;
  library: LibraryDto = new LibraryDto();

  name: string;
  categoryName: string = "";
  authorName: string = "";
  publisherName: string = "";

  libraryId: string;

  pageIndex: number = 1;;
  count: number;
  pageSize: number=9;
  listPageSize = [
    { size: 3 },
    { size: 6 },
    { size: 9 },
    { size: 12 },
  ];

  itemObject: BorrowBookDetailDto = new BorrowBookDetailDto();
  cart: Array<BorrowBookDetailDto> = [];

  saving = false;


  publishers: PublisherDto[];
  authors: AuthorDto[];
  categories: CategoryDto[];

  constructor(
    injector: Injector,
    private _bookLibraryService: BookLibraryServiceProxy,
    private _libraryService: LibraryServiceProxy,
    private _modalService: BsModalService,
    private _authorService: AuthorServiceProxy,
    private _publisherService: PublisherServiceProxy,
    private _categoryService: CategoryServiceProxy,
    private route: ActivatedRoute,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
    this.getLibrary();
    this.listPublisher();
    this.listAuthor();
    this.listCategory();
  }

  getLibrary() {
    this._libraryService.getLibraryById(this.libraryId).subscribe((result: LibraryDto) => {
      this.library = result;
    });
  }

  listCategory(): void {
    this._categoryService
      .getAllCategory()
      .subscribe(
        response => {
          this.categories = response;
        }
      );
  }


  listPublisher(): void {
    this._publisherService
      .getAllPublisher()
      .subscribe(
        response => {
          this.publishers = response;
        }
      );
  }

  listAuthor(): void {
    this._authorService
      .getAllAuthor()
      .subscribe(
        response => {
          this.authors = response;
        }
      );
  }

  list(): void {
    this.libraryId = this.route.snapshot.paramMap.get('id');
    this._bookLibraryService
      .getBookOfLibraryByCriteria(this.libraryId, this.name, this.categoryName, this.authorName, this.publisherName, this.pageIndex, this.pageSize)
      .subscribe(response => {
        this.bookLibraries = response.items;
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

  onChangePage(event) {
    this.pageIndex = event;
    this.list();
  }

  setup() {
    this.categoryName = "";
    this.authorName = "";
    this.publisherName = "";
    this.name = undefined;
    this.list();
  }

  refresh() {
    this.pageIndex = 1;
    this.list();
  }

  filter() {
    if (this.name === undefined && this.categoryName === "" && this.authorName === "" && this.publisherName === "") {
      this.list();
    } else {
      this.list();
    }
  }

  delete(bookLibrary: GetAllBookOfLibraryDto): void {
    abp.message.confirm(
      this.l('Delete warning message', bookLibrary.id),
      undefined,
      (result: boolean) => {
        if (result) {
          this._bookLibraryService
            .deleteBookLbrary(bookLibrary.id)
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

  createBookLibrary(): void {
    this.showCreateOrEditTenantDialog();
  }

  editBookLibrary(bookLibrary: GetAllBookOfLibraryDto): void {
    this.showCreateOrEditTenantDialog(bookLibrary.id);
  }


  showCreateOrEditTenantDialog(id?: string): void {
    let createOrEditTenantDialog: BsModalRef;
    if (!id) {
      createOrEditTenantDialog = this._modalService.show(
        CreateBooklibraryComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: this.libraryId,
          },
        }
      );
    } else {
      createOrEditTenantDialog = this._modalService.show(
        EditBookLibraryComponent,
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


