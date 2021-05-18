import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  DistrictServiceProxy,
  GetAllStatisticByDto,
  GetDistrictDto,
  GetLibraryDto,
  GetProvinceDto,
  LibraryServiceProxy,
  ProvinceServiceProxy,
  StatisticServiceProxy
} from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ChartOptions, ChartType } from 'chart.js';
import { Label, monkeyPatchChartJsLegend, monkeyPatchChartJsTooltip, SingleDataSet } from 'ng2-charts';

@Component({
  selector: 'app-statistic-report-category-borrow',
  templateUrl: './statistic-report-category-borrow.component.html',
  styleUrls: ['./statistic-report-category-borrow.component.css'],
  animations: [appModuleAnimation()]

})
export class StatisticReportCategoryBorrowComponent extends AppComponentBase implements OnInit {

  statistics: GetAllStatisticByDto[];
  libraries: GetLibraryDto[];
  provinces: GetProvinceDto[];
  districts: GetDistrictDto[];
  advancedFiltersVisible = false;

  pageIndex: number=1;
  count: number;
  pageSize: number=9;

  libraryId: string;
  provinceId: string;
  districtId: string;
  fromDate;
  toDate;
  month: number;
  quarter: number;

  listMonth = [
    { m: 1 }, { m: 2 }, { m: 3 }, { m: 4 }, { m: 5 }, { m: 6 }, { m: 7 }, { m: 8 }, { m: 9 }, { m: 10 }, { m: 11 }, { m: 12 }
  ]

  listQuarter = [
    { q: 1 }, { q: 2 }, { q: 3 }, { q: 4 }
  ]

  valueFromDate(value: Date): void {
    this.fromDate = moment(value, "YYYY-MM-DD");
  }

  valueToDate(value: Date): void {
    this.toDate = moment(value, "YYYY-MM-DD");
  }


  other: number = 0;
  otherName: string ='Other categories';

  public pieChartOptions: ChartOptions = {
    responsive: true,
    tooltips: {
      enabled: true,
      callbacks: {
          label: function(tooltipItem, data) {
              return data.labels[tooltipItem.index] + ': ' + data.datasets[0].data[tooltipItem.index] + '%';
          }
      }
  }
  };

  pieChartLabels: Label[] = [];
  pieChartData: SingleDataSet = [];
  pieChartType: ChartType = 'pie';
  pieChartLegend = true;
  pieChartPlugins = [];

  public pieChartColors: Array<any> = [{
    backgroundColor: ['#fc5858', '#19d863', '#fdf57d', '#1b9980', '#230dc2', '#9b5913', '#ad5039'],
  }];

  constructor(
    injector: Injector,
    private _statisticService: StatisticServiceProxy,
    private _libraryService: LibraryServiceProxy,
    private _districtService: DistrictServiceProxy,
    private _provinceService: ProvinceServiceProxy,
    private _modalService: BsModalService,

  ) {
    super(injector);
    monkeyPatchChartJsTooltip();
    monkeyPatchChartJsLegend();
  }

  ngOnInit(): void {
    this.list();
    this.listProvince();
    this.listLibrary();
    this.listDistrict();
  }

  listLibrary(): void {
    this._libraryService
      .getAllLibrary()
      .subscribe(
        response => {
          this.libraries = response;
        }
      );
  }

  listProvince(): void {
    this._provinceService
      .getAllProvince()
      .subscribe(
        response => {
          this.provinces = response;
        }
      );
  }

  listDistrict(): void {
    this._districtService
      .getAllDistrict()
      .subscribe(
        response => {
          this.districts = response;
        }
      );
  }

  list(): void {
    this._statisticService
      .getAllStatisticBy(this.libraryId, this.provinceId, this.districtId, this.fromDate,
        this.toDate, this.month, this.quarter, this.pageIndex, this.pageSize)
      .subscribe(response => {
        this.statistics = response.items;
        this.count = response.count;
        this.pageIndex = response.pageIndex;
        this.pageSize = response.pageSize;
      }
      );
  }


  setup() {
    this.libraryId = undefined;
    this.provinceId = undefined;
    this.districtId = undefined;
    this.fromDate = undefined;
    this.toDate = undefined;
    this.month = undefined;
    this.quarter = undefined;
    this.pieChartLabels = [];
    this.pieChartData = [];
    this.statistics = undefined;
    this.list();
  }

  filter() {
    if (this.libraryId === undefined && this.provinceId === undefined && this.districtId === undefined
      && this.fromDate === undefined && this.toDate === undefined && this.month === undefined
      && this.quarter === undefined) {
      this.list();

    } else {
      this.list();
    }
  }

  chart() {
    this.pieChartLabels = [this.otherName];
    this.pieChartData = [this.other];
    let qty = 0;
    let total = 0;
    for (let i = 0; i < this.statistics.length; i++) {
      total = total + this.statistics[i].quantity
    }
    
    for (let i = 0; i < this.statistics.length; i++) {
      this.pieChartLabels.push(this.statistics[i].categoryName);
      qty = (this.statistics[i].quantity / total) * 100
      if (qty < 2) {
        this.other = this.other + qty;
      } else {
        this.pieChartData.push(Math.ceil(qty));
      }
    }
  }

}
