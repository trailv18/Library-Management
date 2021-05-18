import { Component, Injector, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';

import {
  FavoriteServiceProxy,
  FavoriteBookDto,
  GetFavoriteBookDto,
} from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-reader-favoritebook',
  templateUrl: './reader-favoritebook.component.html',
  styles: [
  ],
  animations: [appModuleAnimation()]
})
export class ReaderFavoritebookComponent extends AppComponentBase implements OnInit {

  favoriebooks:  GetFavoriteBookDto[];
  names = '';
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
    private _favoriteService: FavoriteServiceProxy,
    private _modalService: BsModalService,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.list();
  }

  list(): void {
    this._favoriteService
      .getAllFavorite(this.pageIndex, this.pageSize)
      .subscribe(response => {        
        this.favoriebooks = response.items;
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

  onChangePage(event) {
    this.pageIndex = event;
    this.list();
  }

  refresh(){
    this.pageIndex = 1;
    this.list();
  }

  delete(favorite: FavoriteBookDto): void {
    abp.message.confirm(

      this.l('Delete warning message'),
      undefined,
      (result: boolean) => {
        if (result) {
          this._favoriteService
            .deleteFavoriteBook(favorite.id)
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

}
