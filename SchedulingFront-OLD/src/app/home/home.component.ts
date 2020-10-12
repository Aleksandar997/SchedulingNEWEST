import { Component, OnInit, Injector } from '@angular/core';
import { FormBase } from '../common/base/formBase';

@Component({
  selector: 'home',
  styleUrls: ['./home.component.css'],
  templateUrl: './home.component.html'
})
export class HomeComponent extends FormBase implements OnInit {
  constructor(private inj: Injector) {
    super(inj, 'home');
  }
  ngOnInit() {
    this.portalService.clearFilters();
  }

  isChartAvailable(ref) {
    if (!ref) {
      return false;
    }
    return ref.isChartAvailable();
  }
}

