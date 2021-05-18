import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  PublisherServiceProxy,
  Publisher,
  GetPublisherDto,
  PublisherDto,
} from '@shared/service-proxies/service-proxies';
import { CreatePublisherComponent } from './create-publisher/create-publisher.component';
import { EditPublisherComponent } from './edit-publisher/edit-publisher.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';


@Component({
  selector: 'app-publisher',
  templateUrl: './publisher.component.html',
  animations: [appModuleAnimation()]
})
export class PublisherComponent extends AppComponentBase implements OnInit {

  publishers: GetPublisherDto[];
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
    private _publisherService: PublisherServiceProxy,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this._publisherService
      .getPublisherByPage(this.name, this.pageIndex, this.pageSize)
      .subscribe(response => {
        this.publishers = response.items;
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

  onChangePage(event) {
    this.pageIndex = event;
    this.list();
  }

  setup(){
    this.pageIndex =1;
    this.name = undefined;
    this.list();
  }

  refresh(){
    this.pageIndex = 1;
    this.list();
  }

  delete(publisher: Publisher): void {
    abp.message.confirm(

      this.l('Publisher delete warning message', publisher.name),
      undefined,
      (result: boolean) => {
        if (result) {
          console.log(publisher.id);
          this._publisherService
            .deletePublisher(publisher.id)
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

  createPublisher(): void {
    this.showCreateOrEditTenantDialog();
  }

  editPublisher(publisher: PublisherDto): void {
    console.log(publisher.id)
    this.showCreateOrEditTenantDialog(publisher.id);
  }


  showCreateOrEditTenantDialog(id?: string): void {
    let createOrEditTenantDialog: BsModalRef;
    if (!id) {
      createOrEditTenantDialog = this._modalService.show(
        CreatePublisherComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditTenantDialog = this._modalService.show(
        EditPublisherComponent,
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
