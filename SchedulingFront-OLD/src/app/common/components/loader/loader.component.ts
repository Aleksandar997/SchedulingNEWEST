import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'loader',
    templateUrl: './loader.component.html',
    styleUrls: ['./loader.component.css']
})
export class LoaderComponent {

    constructor() { }
    showLoader = false;
    isLoaderHidden = false;
    @Input() isFullPageLoader = false;

    initFullPageLoader = () => this.isFullPageLoader = true;

    show(forceShow = false) {
        if (forceShow) {
            setTimeout(() => {
                if (this.isLoaderHidden) {
                    return;
                }
                this.showLoader = true;
            }, 1);
            return;
        }
        setTimeout(() => {
            if (this.isLoaderHidden) {
                return;
            }
            this.showLoader = true;
        }, 500);
    }

    hide() {
        this.isLoaderHidden = true;
        this.showLoader = false;
    }

    toggle(val: boolean) {
        setTimeout(() => {
          this.showLoader = val;
        }, 1);
      }
}
