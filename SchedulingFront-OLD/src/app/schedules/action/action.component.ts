import { Component, ViewChild, Injector, AfterViewInit, OnDestroy, ChangeDetectorRef, OnInit } from '@angular/core';
import { actionEnum } from 'src/app/common/enums';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { ActionType } from 'src/app/common/base/formBase';
import { SelectListModel } from 'src/app/common/models/selectListModel';
import { SystemService } from 'src/app/services/system.service';
import { ScheduleService } from 'src/app/services/schedule.service';
import { FormGroupHelper } from 'src/app/common/helpers/formGroupHelper';
import { ModalBase } from 'src/app/common/models/modalBase';
import { CustomerActionModalComponent } from 'src/app/modals/customer-action-modal/customerActionModal.component';
import { CustomerService } from 'src/app/services/customer.service';
import { ErrorStateMatcherAdapter } from 'src/app/common/adapters/errorStateMatcherAdapter';
import { Document } from '../../models/document';
import { DetailActionComponent } from 'src/app/common/components/detailAction/detailAction.component';
import { LocalData } from 'src/app/common/data/localData';
import { ActionFormBase } from 'src/app/common/base/actionFormBase';
import { ResponseBase } from 'src/app/common/models/responseBase';
import { OrganizationUnit, OrganizationUnitSelectListModel } from 'src/app/models/organizationUnit';
import { NgxTimepickerFieldComponent } from 'ngx-material-timepicker';

@Component({
  templateUrl: './action.component.html',
  styleUrls: ['./action.component.css']
})
// @AutoUnsub()
export class ActionComponent extends ActionFormBase<any> implements OnInit, AfterViewInit, OnDestroy {
  // @ViewChild('timeline') timeline: TimelineSliderComponent;
  @ViewChild('timePicker') timePicker: NgxTimepickerFieldComponent;
  @ViewChild('detailAction') detailAction: DetailActionComponent;
  displayedColumns = LocalData.isUserAdmin() ?
  ['employee', 'product', 'price', 'discount', 'priceWithDiscount', 'actions'] :
  ['product', 'price', 'discount', 'priceWithDiscount', 'actions'];

  // customerSub: Subscription;
  scheduleId = null;
  subs = new Array<Subscription>();
  matcher = new ErrorStateMatcherAdapter();
  form: FormGroup;
  customers: Array<SelectListModel>;
  customerPhoneNumbers = [{ customerId: null, phoneNumber: null }];
  prevUrl: string;
  organizationUnits = new Array<OrganizationUnitSelectListModel>();

  currentTime;
  constructor(private inj: Injector, private activatedRoute: ActivatedRoute,
              private changeDetector: ChangeDetectorRef, private customerService: CustomerService,
              private systemService: SystemService, private scheduleService: ScheduleService) {
    super(inj, 'schedule', null);
    const state = this.router.getCurrentNavigation().extras.state;
    this.prevUrl = state ? state.prevUrl : null;
    this.form = this.fb.group({
      sum: new FormControl({ value: null, disabled: true }),
      note: new FormControl({ value: null, disabled: this.areControlsDisabled() }),
      scheduleId: new FormControl(null),
      schedule: this.fb.group({
        customerId: new FormControl({ value: null, disabled: this.areControlsDisabled() }, [Validators.required]),
        date: new FormControl({ value: (state ? new Date(state.date) : new Date()), disabled: this.areControlsDisabled() }),
        phoneNumber: new FormControl({ value: null, disabled: true }, [Validators.required]),
        bindToEmployee: new FormControl(true),
      }),
      organizationUnitId: new FormControl({ value: null, disabled: this.areControlsDisabled() }, [Validators.required]),
      documentDetails: this.fb.array([])
    });
    this.subs.push(this.customerService.customers.subscribe(async res => {
      this.customerPhoneNumbers = await res.map(x => {
        return {
          customerId: x.customerId,
          phoneNumber: x.phoneNumber
        };
      });
      this.customers = res.map(x => new SelectListModel(x.customerId, `${x.firstName} ${x.lastName}`));
    }));
    this.navigateBackUrl = this.prevUrl ? this.prevUrl : '/schedules';
    this.currentTime = new Date().toTimeString().substring(0, 5);
  }
  ngAfterViewInit() {
    this.getLists();
    this.getData();
    this.subs.push(this.detailAction.sum.subscribe(res => {
      this.form.getControls('sum').setValue(res);
    }));
  }
  ngOnDestroy() {
    this.subs.forEach(x => x.unsubscribe);
  }

  ngOnInit() {
    this.scheduleId = this.getId();
    this.subs.push(this.form.getControls('schedule.customerId').valueChanges.subscribe(res => {
      const currentCustomer = this.customerPhoneNumbers.find(x => x.customerId === +res);
      if (!currentCustomer) {
        this.form.getControls('schedule.phoneNumber').setValue(null);
        return;
      }
      this.form.getControls('schedule.phoneNumber').setValue(currentCustomer.phoneNumber);
    }));
    this.subs.push(this.form.getControls('organizationUnitId').valueChanges.subscribe(res => {
      if (!res) {
        return;
      }
      this.detailAction.bindToEmployeeToggle(this.organizationUnits.find(x => x.id === res).bindScheduleToEmployee);
      this.detailAction.setOrganizationUnit(res);
    }));
    if (!LocalData.isUserAdmin()) {
      return;
    }
  }

  getData() {
    if (!this.scheduleId) {
      return;
    }
    this.getById(scheduleId => {
      return this.scheduleService.getScheduleById(scheduleId).then(res => {
        this.currentTime = new Date(res.data.schedule.date).toTimeString().substring(0, 5);
        this.timePicker.onTimeSet(this.currentTime);
        FormGroupHelper.mapObjectToFormGroup(res.data, this.form);
        const details = (this.form.getControls('documentDetails') as FormArray);
        this.detailAction.data.next(details);
      }) as Promise<ResponseBase<Document>>;
    });
  }

  getLists() {
    this.execGetFunc(() => {
      return this.systemService.getOrganizationUnits().then(res => {
        this.organizationUnits = res.data.map(x => new OrganizationUnitSelectListModel(
          x.organizationUnitId,
          x.name,
          x.bindScheduleToEmployee));
        if (this.organizationUnits.length === 1 && this.action == actionEnum.Add) {
          this.form.getControls('organizationUnitId').setValue(this.organizationUnits.map(x => x.id).firstElement());
        }
      }) as Promise<ResponseBase<OrganizationUnit>>;
    });

    this.customerService.selectAll();
  }


  submit() {
    this.detailAction.matcher.submit();
    return this.execFunc(() => {
      const time = this.currentTime.split(':');
      const document = this.form.getRawValue() as Document;
      document.schedule.date = new Date(document.schedule.date);
      document.schedule.date.setHours(+time[0], +time[1]);
      document.documentDetails = this.detailAction.formArray.getRawValue();
      return this.scheduleService.saveSchedule(document) as Promise<ResponseBase<number>>;
    }, ActionType.Save, this.form);
  }

  addNewCustomer() {
    this.modal.openDialog(
      ModalBase.InstanceWithoutText(this.form.getControls('schedule.customerId').value, this.loaderEmitter, () => {
        this.form.getControls('schedule.customerId').updateValueAndValidity();
      }),
      CustomerActionModalComponent
    );
  }

  areControlsDisabled = () => this.action === actionEnum.View;

}
