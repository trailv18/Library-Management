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
  BookLibraryServiceProxy,
  BookLibraryDto,
  BookServiceProxy,
  GetBookDto
} from '@shared/service-proxies/service-proxies';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-create-book-library',
  templateUrl: './create-book-library.component.html',
  animations: [appModuleAnimation()]

})
export class CreateBooklibraryComponent extends AppComponentBase implements OnInit {

  saving = false;
  bookLibrary: BookLibraryDto = new BookLibraryDto();
  books: GetBookDto[];
  id: string;

  @Output() onSave = new EventEmitter<any>();

  constructor(injector: Injector,
    private _bookService: BookServiceProxy,
    private _bookLibraryService: BookLibraryServiceProxy,
    public bsModalRef: BsModalRef,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.listBook();     
  }

  listBook(): void {
    this._bookService
      .getAllBook()
      .subscribe(
        response => {
          this.books = response;
        }
      );
  }

  save(): void {
    this.saving = true;
    this.bookLibrary.libraryId = this.id;
    this._bookLibraryService
      .addBookLibrary(this.bookLibrary)
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
