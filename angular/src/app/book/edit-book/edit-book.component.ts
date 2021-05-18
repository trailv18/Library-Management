import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
} from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  BookServiceProxy,
  Book,
  BookDto,
  AuthorServiceProxy,
  AuthorDto,
  PublisherServiceProxy,
  PublisherDto,
  CategoryServiceProxy,
  CategoryDto,
} from '@shared/service-proxies/service-proxies';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  animations: [appModuleAnimation()]

})
export class EditBookComponent extends AppComponentBase implements OnInit {

  saving = false;
  book: BookDto = new BookDto();
  publishers: PublisherDto[];
  authors: AuthorDto[];
  categories: CategoryDto[];
  id: string;

  @Output() onSave = new EventEmitter<any>();

  constructor(injector: Injector,
    private _bookService: BookServiceProxy,
    private _authorService: AuthorServiceProxy,
    private _publisherService: PublisherServiceProxy,
    private _categoryService: CategoryServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._bookService.getBookById(this.id).subscribe((result: BookDto) => {
      this.book = result;
    });
    this.listPublisher();
    this.listAuthor();
    this.listCategory();
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

  save(): void {
    this.saving = true;
    this._bookService
      .updateBook(this.book)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('Saved successfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      });
  }

}
