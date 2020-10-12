import { AfterContentInit, AfterViewInit, Component, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { ToasterComponent } from './common/components/toaster/toaster.component';
import { ToasterService } from './common/components/toaster/toaster.service';
import { RouterOutlet, Router, RouteConfigLoadStart, RouteConfigLoadEnd, NavigationStart, NavigationEnd } from '@angular/router';
import { trigger, transition, query, animate, style } from '@angular/animations';
import { FullPageLoaderComponent } from './common/components/fullPageLoader/fullPageLoader.component';
import { FullPageLoaderService } from './common/components/fullPageLoader/fullPageLoader.service';
import { LoaderComponent } from './common/components/loader/loader.component';

function test(opacity: number, state) {
  return style({ opacity });
}

function asdf() {
  return [
    query(':enter', test(0, 'enter'), { optional: true }),
    query(':leave',
      [
        test(1, 'leave'),
        animate('0.5s', test(0, 'leave animate'))
      ], { optional: true },
    ),
    query(':enter',
      [
        test(0, 'enter'),
        animate('0.5s', test(1, 'enter animate'))
      ], { optional: true },
    )
  ];
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  // animations: [
  //   trigger('routeAnimations', [
  //     transition('* <=> *', asdf())
  //   ])
  // ]
})
export class AppComponent implements AfterViewInit {
  @ViewChild('toaster') toaster: ToasterComponent;
  @ViewChild('loader') fullPageLoaderComponent: LoaderComponent;
  toasterSubscription: Subscription;
  title = 'Schedulingfront';

  constructor(private router: Router) {
    this.toasterSubscription = ToasterService.toaster.subscribe(res => {
      this.toaster.open(res);
    });
    // this.router.events.subscribe(event => {
    //   if (event instanceof NavigationStart) {
    //     console.log('NavigationStart')
    //   } else if (event instanceof NavigationEnd) {
    //     console.log('NavigationEnd')
    //   }
    // });
    FullPageLoaderService.loader$.subscribe(res => {
      if (!this.fullPageLoaderComponent || res === null) {
        return;
      }
      // if (res) {
      //   this.fullPageLoaderComponent.toggle(res);
      //   return;
      // }
      setTimeout(() => {
        this.fullPageLoaderComponent.toggle(res);
      }, 1)
    });
  }
  ngAfterViewInit(): void {
    // this.fullPageLoaderComponent.initFullPageLoader();
    this.fullPageLoaderComponent.toggle(true);
  }

  prepareRoute(outlet: RouterOutlet) {
    return 'test';
  }


}
