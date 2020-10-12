import { Component, ViewChild, AfterViewInit, Injector } from '@angular/core';
import { CalendarComponent } from '../common/components/calendar/calendar.component';
import { ScheduleService } from '../services/schedule.service';
import { SystemService } from '../services/system.service';
import { FormControl } from '@angular/forms';
import { SelectListModel } from '../common/models/selectListModel';
import { LocalData } from '../common/data/localData';
import { ActionType } from '../common/base/formBase';
import { Schedule } from '../models/schedule';
import { ResponseBase } from '../common/models/responseBase';
import { User } from '../common/models/user';
import { OrganizationUnit } from '../models/organizationUnit';
import { CalendarPaging } from '../common/models/calendarPaging';
import { ListFormBase } from '../common/base/listFormBase';
import { MatTabGroup } from '@angular/material';
import { MatTabModel } from '../common/models/matTabModel';
import { CodebookService } from '../common/services/codebook.service';
import { ScheduleType } from '../models/scheduleType';
import { ToasterService } from '../common/components/toaster/toaster.service';

@Component({
  templateUrl: './schedules.component.html',
  styleUrls: ['./schedules.component.css']
})
export class SchedulesComponent extends ListFormBase<Schedule> implements AfterViewInit {
  @ViewChild(MatTabGroup) matTabGroup: MatTabGroup;
  @ViewChild(CalendarComponent) calendarComponent: CalendarComponent;
  selectedEmployees = new FormControl();
  employees: Array<SelectListModel>;
  organizationUnits: Array<SelectListModel>;
  tabs = new Array<ScheduleType>();

  constructor(private inj: Injector, public scheduleService: ScheduleService,
              private systemService: SystemService, private codebookService: CodebookService) {
    super(inj,
      'schedule',
      LocalData.isUserAdmin() ?
        ['time', 'customer.customerName', 'customer.phoneNumber', 'employees', 'actions'] :
        ['time', 'customer.customerName', 'customer.phoneNumber', 'actions'],
      null,
      (id) => this.scheduleService.deleteScheduleById(id)
    );
    this.filters = this.fb.group({
      employees: new FormControl([]),
      organizationUnits: new FormControl([])
    });
    this.paging = new CalendarPaging();
    this.codebookService.setCodebookUrl('codebook/scheduleType');
  }

  navigateToAction() {
    this.calendarComponent.navigateToAction();
  }
  ngAfterViewInit() {
    if (this.tabs.length === 0) {
      this.matTabGroup._tabs.first.isActive = false;
    }
    this.getLists();
  }

  getScheduleTypes() {
    this.execGetFunc(() => {
      return this.codebookService.getAll().then(res => {
        // this.tabs = res;
      }) as Promise<ResponseBase<Array<ScheduleType>>>;
    });
  }

  getLists() {
    this.execGetFunc(() => {
      return this.systemService.getEmployees().then(res => {
        this.employees = res.data.map(x => new SelectListModel(x.employee.employeeId, `${x.firstName} ${x.lastName}`));
      }) as Promise<ResponseBase<Array<User>>>;
    });
    this.execGetFunc(() => {
      return this.systemService.getOrganizationUnits().then(res => {
        this.organizationUnits = res.data.map(x => new SelectListModel(x.organizationUnitId, x.name));
      }) as Promise<ResponseBase<Array<OrganizationUnit>>>;
    });
  }

  selectedTabChange(event) {
  }

  // getDataFunc(calendarPaging: CalendarPaging = null) {
  //   if (calendarPaging) {
  //     this.paging['dateFrom'] = calendarPaging.dateFrom;
  //     this.paging['dateTo'] = calendarPaging.dateTo;
  //   }
  //   this.getData();
  // }

  getData(calendarPaging: CalendarPaging = null) {
    if (calendarPaging) {
      this.paging['dateFrom'] = calendarPaging.dateFrom;
      this.paging['dateTo'] = calendarPaging.dateTo;
    }
    this.execGetFunc(() => {
      return this.scheduleService.getScheduleInMonth(this.paging as CalendarPaging).then(res => {
        this.calendarComponent.setTiles(res.data);
        // this.dataSource.data = res.data;
        // this.datasourceLength = res.count;
      }) as Promise<ResponseBase<Array<Schedule>>>;
    });
  }

  deleteDetail(id: number) {
    this.execFunc(() => {
      this.scheduleService.deleteScheduleById(id);
    }, ActionType.Delete);
  }

  addScheduleType() {
    this.codebookService.save(new ScheduleType(this.getLocalization('label_new_tab'))).then(res => {
      this.tabs.push(res.data);
    }).catch(err => {
      ToasterService.openError(`schedule_type_save_error`);
    });
  }
}
