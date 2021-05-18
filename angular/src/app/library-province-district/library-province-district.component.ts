import { Component, Injectable, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import { 
  GetLibraryProvinceDto, 
  LibraryProvinceServiceProxy, 
  ProvinceDto, 
  ProvinceServiceProxy 
} from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-library-province-district',
  templateUrl: './library-province-district.component.html',
  animations: [appModuleAnimation()]
})
export class LibraryProvinceDistrictComponent extends AppComponentBase implements OnInit {

  libraryProvinceDistricts: GetLibraryProvinceDto[];
  provinceId: string;
  defaultProvince: string="00000000-0000-0000-0000-000000000000";
  provinces: ProvinceDto [];

  pageIndex: number = 1;;
  count: number;
  pageSize: number=9;

  listPageSize = [
    { size: 3 },
    { size: 6 },
    { size: 9 },
    { size: 12 },
  ];

  constructor(
    injector: Injector,
    private _libraryProvinceDistrictService: LibraryProvinceServiceProxy,
    private _modalService: BsModalService,
    private _provinceService: ProvinceServiceProxy,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
    this.listProvince();
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

  list(): void {
    this._libraryProvinceDistrictService
      .getLibraryProvinceBy(this.provinceId, this.pageIndex, this.pageSize)
      .subscribe(response => {
        this.libraryProvinceDistricts = response.items;
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


  filter() {
    if (!this.provinceId) {
      this.list();
    }
    else {
      this.list();
    }
  }

  setup() {
    this.provinceId = undefined;
    this.list();
  }

  onChangePage(event) {
    this.pageIndex = event;
    this.list();
  }

  refresh() {
    this.provinceId = undefined;
    this.pageIndex = 1;
    this.pageSize = 9;
    this.list();
  }
}
