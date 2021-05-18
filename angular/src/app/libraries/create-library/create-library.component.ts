import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
} from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  DistrictServiceProxy,
  GetDistrictDto,
  LibraryServiceProxy,
  LibraryDto,
  ProvinceServiceProxy,
  ProvinceDto,
} from '@shared/service-proxies/service-proxies';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-create-library',
  templateUrl: './create-library.component.html',
  animations: [appModuleAnimation()]
})
export class CreateLibraryComponent extends AppComponentBase implements OnInit {

  saving = false;
  library: LibraryDto = new LibraryDto();
  districts: GetDistrictDto[];
  provinces: ProvinceDto[];

  id: string;

  @Output() onSave = new EventEmitter<any>();

  constructor(injector: Injector,
    private _districtService: DistrictServiceProxy,
    private _libraryService: LibraryServiceProxy,
    private _provinceService: ProvinceServiceProxy,
    public bsModalRef: BsModalRef,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.listProvince();
  }

  listProvince(): void {
    this._provinceService
      .getAllProvince()
      .subscribe(
        response => {
          console.log(response);
          this.provinces = response;
        }
      );
  }

  selectValue($event) {
    this.id = $event.target.value;
    this.listDistrict();
  }

  listDistrict(): void {
    this._districtService
      .getDistrictByProvinceId(this.id)
      .subscribe(
        response => {
          console.log(response);
          this.districts = response;
        }
      );
  }

  save(): void {
    this.saving = true;
    this._libraryService
      .addLibrary(this.library)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('Saved successfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      });
  }

}
