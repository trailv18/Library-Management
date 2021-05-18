import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  GetStatisticDto, StatisticServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styles: [
  ],
  animations: [appModuleAnimation()]
})
export class StatisticComponent extends AppComponentBase implements OnInit {

  statistics: GetStatisticDto[];
  names = '';
  advancedFiltersVisible = false;

  pageIndex: number = 1;
  libraryId: string;
  count: number;
  pageSize: number=9;

  month: number;

  listMonth = [
    { m: 1 }, { m: 2 }, { m: 3 }, { m: 4 }, { m: 5 }, { m: 6 }, { m: 7 }, { m: 8 }, { m: 9 }, { m: 10 }, { m: 11 }, { m: 12 }
  ]

  listPageSize = [
    { size: 3 },
    { size: 6 },
    { size: 9 },
    { size: 12 },
  ];

  constructor(
    injector: Injector,
    private _statisticService: StatisticServiceProxy,
    private _modalService: BsModalService,
    private route: ActivatedRoute,

  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this.libraryId = this.route.snapshot.paramMap.get('id');
    this._statisticService
      .getStatisticByCriteria(this.libraryId ,  this.month, this.pageIndex, this.pageSize)
      .subscribe(response => {        
        this.statistics = response.items;
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
    if (!this.month) {
      this.list();
    }
    else {
      this.list();
    }
  }

  setup() {
    this.month = undefined;
    this.list();
  }

  onChangePage(event) {
    this.month = undefined;
    this.pageIndex = event;
    this.list();
  }

  refresh() {
    this.month = undefined;
    this.pageIndex = 1;
    this.pageSize = 9;
    this.list();
  }

}
