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
  GetProvinceDto,
} from '@shared/service-proxies/service-proxies';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-edit-library',
  templateUrl: './edit-library.component.html',
  animations: [appModuleAnimation()]

})
export class EditLibraryComponent extends AppComponentBase implements OnInit {

  saving = false;
  library: LibraryDto = new LibraryDto();
  districts: GetDistrictDto[];
  id: string;

  provinces: GetProvinceDto[];
  provinceId: string;

  @Output() onSave = new EventEmitter<any>();

  constructor(injector: Injector,
    private _districtService: DistrictServiceProxy,
    private _libraryService: LibraryServiceProxy,
    private _provinceService: ProvinceServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._libraryService.getLibraryById(this.id).subscribe((result: LibraryDto) => {
      this.library = result;   
      this._districtService
      .getDistrictByProvinceId(result.provinceId)
      .subscribe(
        response => {
          console.log(response);
          this.districts = response;
        }
      ); 
    });
        
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

  selectValue($event) {
    this.provinceId = $event.target.value;
    this.listDistrict(this.provinceId);
  }

  listDistrict(id): void {
    this._districtService
      .getDistrictByProvinceId(id)
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
      .updateLibrary(this.library)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      });
  }
}
