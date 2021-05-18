import {
  Component,
  Injector,
  OnInit,
  Output,
  EventEmitter
} from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  ProvinceServiceProxy,
  ProvinceDto,
} from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-edit-province',
  templateUrl: './edit-province.component.html',
  animations: [appModuleAnimation()]
})
export class EditProvinceComponent extends AppComponentBase implements OnInit {

  saving = false;
  province: ProvinceDto = new ProvinceDto();
  id: string;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _provinceService: ProvinceServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    console.log(this.id);
    this._provinceService.getProvinceById(this.id).subscribe((result: ProvinceDto) => {
      this.province = result;
    });
  }

  save(): void {
    this.saving = true;
    this._provinceService
      .updateProvince(this.province)
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
