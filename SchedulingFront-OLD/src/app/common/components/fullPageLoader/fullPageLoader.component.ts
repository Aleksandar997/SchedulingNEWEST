import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'full-page-loader',
  templateUrl: './fullPageLoader.component.html',
  styleUrls: ['./fullPageLoader.component.css']
})
export class FullPageLoaderComponent implements OnInit {

  constructor() { }
  showLoader;
  ngOnInit(): void {
  }
  toggle(val: boolean) {
    this.showLoader = val;
  }
}
