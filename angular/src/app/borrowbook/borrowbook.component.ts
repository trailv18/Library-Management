import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  BorrowBookServiceProxy,
  GetAllBorrowBookDto
} from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import * as moment from 'moment';

@Component({
  selector: 'app-borrowbook',
  templateUrl: './borrowbook.component.html',
  animations: [appModuleAnimation()]
})
export class BorrowbookComponent extends AppComponentBase implements OnInit {

  borrowBooks: GetAllBorrowBookDto[];
  advancedFiltersVisible = false;

  pageIndex: number = 1;;
  count: number;
  pageSize: number=9;

  dateModel: Date = new Date();
  fromDate;
  toDate;
  month:number;

  listMonth =[
    {m: 1},{m: 2},{m:3},{m: 4},{m: 5},{m: 6},{m: 7},{m: 8},{m: 9},{m: 10},{m: 11},{m: 12}
  ]

  listPageSize = [
    { size: 3 },
    { size: 6 },
    { size: 9 },
    { size: 12 },
  ];

  constructor(
    injector: Injector,
    private _borrowBookService: BorrowBookServiceProxy,
    private _modalService: BsModalService,
    
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  valueFromDate(value: Date): void {
    this.fromDate = moment(value, "YYYY-MM-DD");
  }

  valueToDate(value: Date): void {
    this.toDate = moment(value, "YYYY-MM-DD");
  }

  list(): void {
    this._borrowBookService
      .getPageBorrowBook(this.fromDate, this.toDate , this.month, this.pageIndex, this.pageSize)
      .subscribe(response => {
        this.borrowBooks = response.items;
        this.count = response.count;
        this.pageIndex = response.pageIndex;
        this.pageSize = response.pageSize;
      });
  }

  changePageSize(size: number){
    this.pageIndex = 1;
    this.pageSize = size;
    this.list();    
  }


  filter(){
    if(!this.fromDate && !this.toDate){
      this.pageIndex =1;
      this.list();      
    }
    else{
      this.pageIndex =1;
      this.list();
    }
  }

  setup(){
    this.fromDate = undefined;
    this.toDate = undefined;
    this.month = undefined;
    this.pageIndex = 1;
    this.pageSize = 9;
    this.list();
  }

  onChangePage(event) {
    this.pageIndex = event;
    this.list();
  }

  refresh() {
    this.fromDate = undefined;
    this.toDate = undefined;
    this.month = undefined;
    this.pageIndex = 1;
    this.pageSize = 9;
    this.list();
  }
}

