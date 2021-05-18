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
  FavoriteServiceProxy,
  FavoriteBookDto,
} from '@shared/service-proxies/service-proxies';

import { ActivatedRoute } from '@angular/router'
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  animations: [appModuleAnimation()]
})
export class BookListComponent extends AppComponentBase implements OnInit {

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
  pageSize: number = 9;

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

  favorite : FavoriteBookDto = new FavoriteBookDto();

  constructor(
    injector: Injector,
    private _bookLibraryService: BookLibraryServiceProxy,
    private _libraryService: LibraryServiceProxy,
    private _modalService: BsModalService,
    private _authorService: AuthorServiceProxy,
    private _publisherService: PublisherServiceProxy,
    private _categoryService: CategoryServiceProxy,
    private _favoriteService : FavoriteServiceProxy,
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

  addFavorite(book: GetAllBookOfLibraryDto){
    this.favorite.bookId = book.bookId;
    this.favorite.libraryId = this.libraryId;
    this.favorite.bookLibraryId = book.id;
    
    this.saving = true;
    this._favoriteService
      .addFavoriteBook(this.favorite)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('Added favorite successfully'));
      });
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


}
