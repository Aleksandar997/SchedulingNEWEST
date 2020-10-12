import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ScheduleType } from '../models/scheduleType';
import { SubjectAdapter } from '../common/base/subjectAdapter';

@Injectable({
  providedIn: 'root'
})
export class ScheduleTypeService {
  scheduleTypes$ = new SubjectAdapter<Array<ScheduleType>>();
  constructor(private http: HttpClient) { }

  url = '/scheduleType';



}
