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
  ProvinceServiceProxy,
  ProvinceDto,  
} from '@shared/service-proxies/service-proxies';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-create-province',
  templateUrl: './create-province.component.html',
  animations: [appModuleAnimation()]
})
export class CreateProvinceComponent extends AppComponentBase implements OnInit {

  saving = false;
  province : ProvinceDto = new ProvinceDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(injector: Injector,
    private _provinceService: ProvinceServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector); }

  ngOnInit(): void {
  }

  save(): void {
    this.saving = true;
    this._provinceService
      .addProvince(this.province)
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
