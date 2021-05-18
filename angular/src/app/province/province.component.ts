import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  ProvinceServiceProxy,
  ProvinceDto,
  GetProvinceDto,
} from '@shared/service-proxies/service-proxies';
import { CreateProvinceComponent } from './create-province/create-province.component';
import { EditProvinceComponent } from './edit-province/edit-province.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';


@Component({
  selector: 'app-province',
  templateUrl: './province.component.html',
  animations: [appModuleAnimation()]

})
export class ProvinceComponent extends AppComponentBase implements OnInit {

  provinces: GetProvinceDto[];
  name:string;
  advancedFiltersVisible = false;

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
    private _provinceService: ProvinceServiceProxy,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this._provinceService
      .getProvinceByPage(this.name, this.pageIndex, this.pageSize)
      .subscribe(response => {
        console.log(response);
        this.provinces = response.items;
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

  filter(){
    if(!this.name){
      this.pageIndex =1;
      this.list();      
    }
    else{
      this.pageIndex =1;
      this.list();
    }
  }

  setup(){
    this.pageIndex =1;
    this.name = undefined;
    this.list();
  }


  onChangePage(event) {
    this.pageIndex = event;
    this.list()
  }

  refresh(){
    this.pageIndex=1;
    this.list();
  }

  delete(province: ProvinceDto): void {
    abp.message.confirm(

      this.l('Delete warning message', province.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._provinceService
            .deleteProvince(province.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('Successfully deleted'));
                this.list();
              })
            )
            .subscribe(() => { });
        }
      }
    );
  }

  createProvince(): void {
    this.showCreateOrEditTenantDialog();
  }

  editProvince(province: ProvinceDto): void {
    console.log(province.id)
    this.showCreateOrEditTenantDialog(province.id);
  }


  showCreateOrEditTenantDialog(id?: string): void {
    let createOrEditTenantDialog: BsModalRef;
    if (!id) {
      createOrEditTenantDialog = this._modalService.show(
        CreateProvinceComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditTenantDialog = this._modalService.show(
        EditProvinceComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }
    createOrEditTenantDialog.content.onSave.subscribe(() => {
      this.list();
    })
  }
}
