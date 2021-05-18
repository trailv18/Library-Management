import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  DistrictServiceProxy,
  DistrictDto,
  ProvinceDto,
  ProvinceServiceProxy,
} from '@shared/service-proxies/service-proxies';
import { CreateDistrictComponent } from './create-district/create-district.component';
import { EditDistrictComponent } from './edit-district/edit-district.component';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-district',
  templateUrl: './district.component.html',
  animations: [appModuleAnimation()]
})
export class DistrictComponent extends AppComponentBase implements OnInit {

  districts: DistrictDto[];
  province : ProvinceDto = new ProvinceDto();
  name:string;
  advancedFiltersVisible = false;

  provinceId: string;
  pageIndex: number = 1;;
  count: number;
  pageSize: number=9;
  province: ProvinceDto;

  
  listPageSize = [
    { size: 3 },
    { size: 6 },
    { size: 9 },
    { size: 12 },
  ];
  
  constructor(
    injector: Injector,
    private _districtService: DistrictServiceProxy,
    private _provinceService: ProvinceServiceProxy,
    private _modalService: BsModalService,
    private route: ActivatedRoute,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
    this.getProvince();
  }

  getProvince(){
    this._provinceService.getProvinceById(this.provinceId).subscribe((res: ProvinceDto)=>{
      this.province = res;
    })
  }

  list(): void {  
    this.provinceId = this.route.snapshot.paramMap.get('id')
    this._districtService
      .getDistrictPageByProvinceId(this.provinceId, this.name, this.pageIndex, this.pageSize)
      .subscribe(response => {
        console.log(response);
        this.districts = response.items;
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

  delete(district: DistrictDto): void {
    abp.message.confirm(
      this.l('Delete warning message', district.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._districtService
            .deleteDistrict(district.id)
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

  createDistrict(): void {
    this.showCreateOrEditTenantDialog();
  }

  editDistrict(district: DistrictDto): void {
    console.log(district.id)
    this.showCreateOrEditTenantDialog(district.id);
  }


  showCreateOrEditTenantDialog(id?: string): void {
    let createOrEditTenantDialog: BsModalRef;
    if (!id) {
      createOrEditTenantDialog = this._modalService.show(
        CreateDistrictComponent,
        {
          class: 'modal-lg',
          initialState:{
            id:this.provinceId,
          },
        }
      );
    } else {
      createOrEditTenantDialog = this._modalService.show(
        EditDistrictComponent,
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
