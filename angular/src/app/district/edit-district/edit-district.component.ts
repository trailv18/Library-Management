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
  DistrictServiceProxy,
  DistrictDto,
} from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  selector: 'app-edit-district',
  templateUrl: './edit-district.component.html',
  animations: [appModuleAnimation()]

})
export class EditDistrictComponent extends AppComponentBase implements OnInit {

  saving = false;
  district: DistrictDto = new DistrictDto();
  id: string;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _districtService: DistrictServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    console.log(this.id);
    this._districtService.getDistrictById(this.id).subscribe((result: DistrictDto) => {
      this.district = result;
    });
  }

  save(): void {
    this.saving = true;
    this._districtService
      .updateDistrict(this.district)
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
