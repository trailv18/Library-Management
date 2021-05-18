import {
    Component,
    Injector,
    OnInit,
  } from '@angular/core';
  import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
  import { AppComponentBase } from '@shared/app-component-base';
  import { finalize } from 'rxjs/operators';
  import { ActivatedRoute } from '@angular/router'
  
  
  import {
    BookLibraryServiceProxy,
    BorrowBookDetailDto,
    GetAllBookOfLibraryDto,
  } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
  
  @Component({
    selector: 'app-book-detail',
    templateUrl: './book-detail.component.html',
    styleUrls: ['./book-detail.component.css'],
    animations: [appModuleAnimation()]
  })
  export class BookDetailComponent extends AppComponentBase implements OnInit {
  
    bookLibrary: GetAllBookOfLibraryDto;
    names = '';
    advancedFiltersVisible = false;
    id:string;
    qty=1;

  localStorage: Storage;
  itemObject: BorrowBookDetailDto = new BorrowBookDetailDto();
  cart: Array<BorrowBookDetailDto> = [];

  constructor(
      injector: Injector,
      private _bookLibraryService: BookLibraryServiceProxy,
      private _modalService: BsModalService,
      private route: ActivatedRoute,
    ) {
      super(injector);
    }
  
    ngOnInit(): void {
      this.getBookLibrary();
    }
  
    getBookLibrary(){
      this.id=this.route.snapshot.paramMap.get('id');    
      this._bookLibraryService
        .getBookOfLibraryById(this.id)
        .subscribe(res =>{
          this.bookLibrary = res;                    
        })
    }
  
    plus(){
      this.qty=this.qty+1;
    }
  
    minus(){
      if(this.qty != 1){
        this.qty=this.qty-1;
      }
    }

    onClick(borrowBookDetail: GetAllBookOfLibraryDto): void {
      this.itemObject.bookId = borrowBookDetail.bookId;
      this.itemObject.libraryId = borrowBookDetail.libraryId;
      this.itemObject.priceBorrow = borrowBookDetail.priceBorrow;
      this.itemObject.book = borrowBookDetail.bookName;
      this.itemObject.qty = this.qty;
      
      if(JSON.parse(localStorage.getItem('object'))) {
        this.cart = JSON.parse(localStorage.getItem('object'))
        let existItemIndex = this.cart.findIndex((item) => item.bookId === this.itemObject.bookId)
        if(existItemIndex >= 0) {
          this.cart[existItemIndex].qty += this.itemObject.qty
        }
        else {
          this.cart.push(this.itemObject)
        }
      }
      else {
        this.cart.push(this.itemObject)
      }
      localStorage.setItem("object", JSON.stringify(this.cart));
      this.notify.info(this.l('Add successfully'));
      this.getBookLibrary();
    }
   
  }
  
