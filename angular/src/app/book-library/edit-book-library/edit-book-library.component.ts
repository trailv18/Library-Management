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
  LibraryServiceProxy,
  BookServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-edit-book-library',
  templateUrl: './edit-book-library.component.html',
  animations: [appModuleAnimation()]

})
export class EditBookLibraryComponent extends AppComponentBase implements OnInit {

  saving = false;
  bookLibrary: BookLibraryDto = new BookLibraryDto();
  id: string;

  @Output() onSave = new EventEmitter<any>();

  constructor(injector: Injector,
    private _bookLibraryService: BookLibraryServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._bookLibraryService.getBookLibraryById(this.id).subscribe((result: BookLibraryDto) => {
      this.bookLibrary = result;
    });
  }

  save(): void {
    this.saving = true;
    this._bookLibraryService
      .updateBookLibrary(this.bookLibrary)
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
