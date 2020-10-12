import {
  Component,
  AfterViewInit,
  Injector,
  Input,
  ViewChild,
  OnDestroy,
  EventEmitter,
  Output,
  HostListener,
  AfterContentInit
} from '@angular/core';
import { MatDialog } from '@angular/material';
import { Day } from '../../models/day';
import { FormBase } from '../../base/formBase';
import { DayDetailsComponent } from './dayDetails/dayDetails.component';
import { IDayDetail } from './base/dayDetail';
import { FormGroup } from '@angular/forms';
import { CalendarPaging } from '../../models/calendarPaging';
import { TranslatePipe } from '../../pipes/translate/translatePipe';
import { Router } from '@angular/router';

@Component({
  selector: 'calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements AfterViewInit, OnDestroy, AfterContentInit {

  constructor(private dialog: MatDialog, private translate: TranslatePipe, private router: Router) {
    this.activeDate = new Date();
    this.initCalendar();
  }
  @Output() deleteDetail = new EventEmitter<any>();
  @Output() detail = new EventEmitter<CalendarPaging>();
  @ViewChild(DayDetailsComponent) dayDetails: DayDetailsComponent<IDayDetail>;
  @Input() displayedColumns: string[];
  opened: Day;
  paging: CalendarPaging;
  tiles: Day[] = [];
  month: number;
  year: number;
  day: number;
  activeDate: Date;
  date: string;
  stayOnTile = false;
  @Input() form: FormGroup;
  months: string[] = [
    'label_january',
    'label_february',
    'label_march',
    'label_april',
    'label_may',
    'label_june',
    'label_july',
    'label_august',
    'label_september',
    'label_october',
    'label_november',
    'label_december',
  ];
  days: string[] = [
    'label_sunday',
    'label_monday',
    'label_tuesday',
    'label_wednesday',
    'label_thursday',
    'label_friday',
    'label_saturday'
  ];
  rowHeight;
  initCalendar() {
    this.month = this.activeDate.getMonth();
    this.day = this.activeDate.getDate();
    this.year = this.activeDate.getFullYear();
    this.date = this.setDate();
    this.tiles = [];
    this.initTiles();
  }

  @HostListener('window:resize')
  onResize() {
    this.rowHeight = this.calcRowHeight();
  }

  calcRowHeight = () => ((document.getElementById('calendar-card').clientHeight / 1.20) / 7) + 'px';
  initTiles() {
    const activeMonth = this.activeDate.getMonth();
    let day = new Date(this.year, this.month, 1).getDay();
    let row = 1;
    if (day !== 0) {
      for (let index = 0; index < day; index++) {
        const prevDay = (this.daysInMonth(this.month - 1, this.year) - (day - 1)) + index;
        this.tiles.push(new Day(prevDay, this.month - 1, new Date(this.year, this.month - 1, prevDay), activeMonth));
      }
    }
    const daysInCurrMonth = this.daysInMonth(this.month, this.year);
    for (let index = 0; index < daysInCurrMonth; index++) {
      if (day === 6) {
        row = row + 1;
      }
      day = day === 6 ? 0 : day + 1;
      this.tiles.push(new Day(index + 1, this.month, new Date(this.year, this.month, index + 1), activeMonth));
    }

    if (row < 6 || (row === 6 && day < 7)) {
      for (let index = 0; index <= (((6 - row) * 7) + 6 - day); index++) {
        this.tiles.push(new Day(index + 1, this.month + 1, new Date(this.year, this.month + 1, index + 1), activeMonth));
      }
    }
    this.paging = new CalendarPaging(this.tiles.firstElement().date, this.tiles.lastElement().date);
  }
  setDate() {
    return this.getLocalization(this.months[this.month]) + ' ' + this.year;
  }
  daysInMonth(month: number, year: number) {
    return new Date(year, month + 1, 0).getDate();
  }
  openTilePreview(tile: Day = null) {
    this.opened = tile ? tile : this.opened;
    this.dayDetails.data = this.opened.details;
    this.dayDetails.day = this.opened;
  }

  ngOnDestroy() {
  }
  ngAfterViewInit() {
    this.getData();
  }
  ngAfterContentInit() {
    setTimeout(() => this.rowHeight = this.calcRowHeight(), 1);
  }

  getData(stayOnTile = false) {
    this.stayOnTile = stayOnTile;
    this.detail.emit(this.paging);
  }

  previousMonth() {
    this.activeDate.setMonth(this.activeDate.getMonth() - 1);
    this.activeDate.setDate(1);
    this.initCalendar();
    this.getData();
  }

  nextMonth() {
    this.activeDate.setMonth(this.activeDate.getMonth() + 1);
    this.activeDate.setDate(1);
    this.initCalendar();
    this.getData();
  }

  setTiles(details: IDayDetail[]) {
    this.tiles.forEach(tile => {
      tile.details = details
        .filter(x => new Date(x.date).getMonth() === tile.month && new Date(x.date).getDate() === tile.day);

      if (!this.stayOnTile && new Date(tile.date).getMonth() === this.month && new Date(tile.date).getDate() === this.day) {
        this.openTilePreview(tile);
      }
    });
    if (this.stayOnTile) {
      this.openTilePreview(this.opened);
      this.stayOnTile = false;
    }
  }

  navigateToAction(url: string = '/add') {
    this.router.navigateByUrl(this.router.url + url, { state: { date: this.opened } });
    // this.dayDetails.navigateToAction();
  }
  getLocalization(key: string) {
    return this.translate.transform(key);
}
}
