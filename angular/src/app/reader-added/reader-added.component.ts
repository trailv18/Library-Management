import { stringify } from '@angular/compiler/src/util';
import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import {
  BookLibraryServiceProxy,
  BorrowBookDetailDto,
  GetAllBookOfLibraryDto,
  BorrowBookDetaiServiceProxy
} from '@shared/service-proxies/service-proxies';
import { BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-reader-added',
  templateUrl: './reader-added.component.html',
  styleUrls: ['./reader-added.component.css'],
  animations: [appModuleAnimation()]

})

export class ReaderAddedComponent extends AppComponentBase implements OnInit {

  localStorage: Storage;
  itemObject: BorrowBookDetailDto = new BorrowBookDetailDto();
  cart: Array<BorrowBookDetailDto> = [];

  total: number = 0;
  bookId: string;
  libraryId: string;

  saving = false;
  @Output() onSave = new EventEmitter<any>();
  mess:string;

  constructor(
    injector: Injector,
    private _borrowBookDetaiServiceProxy: BorrowBookDetaiServiceProxy,
    private _modalService: BsModalService,
    private route: ActivatedRoute,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.listAdded();
  }

  listAdded() {
    this.cart = JSON.parse(localStorage.getItem('object'));
    this.tinhtong();
  }

  tinhtong() {
    this.total = 0;
    this.cart = JSON.parse(localStorage.getItem('object'));
    if(!this.cart){
      this.mess = "This cart is empty!";
    }
    else{
      for(let i = 0; i< this.cart.length; i++){
        this.total +=  (this.cart[i].qty * this.cart[i].priceBorrow);
      }
      localStorage.setItem('total', String(this.total)); 
    }   
  }

  save() {
    this.saving = true;
    this.cart = JSON.parse(localStorage.getItem('object'))
    this._borrowBookDetaiServiceProxy
      .addBorrowBookDetail(this.cart)
      .pipe(
        finalize(() => {
          this.saving = false;
          this.listAdded();
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('Successfully'));
        localStorage.removeItem('object');
      });
  }


  deleteCart(item) {
    this.cart = JSON.parse(localStorage.getItem('object'));

    for (let i = 0; i < this.cart.length; i++) {
      if (this.cart[i].bookId == item) {
        this.cart.splice(i, 1);
      }
    }
    localStorage.setItem('object', JSON.stringify(this.cart));
    this.tinhtong();
  }

  update(item: string,qty:number){
    this.cart = JSON.parse(localStorage.getItem('object'));
    for (let i = 0; i < this.cart.length; i++) {
      if (this.cart[i].bookId == item){  
      this.cart[i].qty = qty;
      }
    }
    localStorage.setItem("object",JSON.stringify(this.cart));
    this.tinhtong();
  }

}
