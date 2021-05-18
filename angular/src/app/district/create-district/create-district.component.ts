import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
  Input,
} from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  DistrictServiceProxy,
  DistrictDto,
  ProvinceServiceProxy,
  ProvinceDto,
} from '@shared/service-proxies/service-proxies';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-create-district',
  templateUrl: './create-district.component.html',
  animations: [appModuleAnimation()]

})
export class CreateDistrictComponent extends AppComponentBase implements OnInit {

  saving = false;
  district: DistrictDto = new DistrictDto();
  id:string;

  @Output() onSave = new EventEmitter<any>();

  constructor(injector: Injector,
    private _districtService: DistrictServiceProxy,
    private _provinceService: ProvinceServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {    
  }


  save(): void {
    this.district.provinceId = this.id;
    this.saving = true;    
    this._districtService
      .addDistrict(this.district)
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
