import { Component, Input, ViewChild, EventEmitter, Output, AfterViewInit, Injector, OnInit } from '@angular/core';
import { DataGridComponent } from '../../dataGrid/dataGrid.component';
import { MatDialog, MatSort } from '@angular/material';
import { Day } from 'src/app/common/models/day';
import { IDayDetail } from '../base/dayDetail';
import { FormBase } from 'src/app/common/base/formBase';
import { Router } from '@angular/router';

@Component({
  selector: 'day-details',
  templateUrl: './dayDetails.component.html',
  styleUrls: ['./dayDetails.component.css']
})
export class DayDetailsComponent<T> extends FormBase implements OnInit {
  @Input() displayedColumns: string[];
  matColumnsDef: string[];
  @ViewChild('calendarDetailsGrid') calendarDetailsGrid: DataGridComponent<T>;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  loaderEmitter: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() deleteDetail = new EventEmitter<any>();
  @Input('data')
  set data(data: Array<T>) {
    this.calendarDetailsGrid.dataSource.data = data;
  }
  // @Input() data;
  @Input() day: Day;
  constructor(private inj: Injector) {
    super(inj, null);
  }

  ngOnInit() {
    this.matColumnsDef = this.displayedColumns.filter(x => x !== 'actions' && x !== 'time');
  }

  navigateToAction(url = '/add') {
    this.router.navigateByUrl(this.router.url + url, { state: { date: this.day.date } });
  }
  onDeleteDetail(id) {
    this.deleteDetail.emit(+id);
  }
  onRowClickLink = (dayDetail: IDayDetail) => '/edit/' + dayDetail.id;

  setCol(col) {
    return col;
  }
}
