import { FormBase } from './formBase';
import { Injector, ViewChild } from '@angular/core';
import { ResponseBase } from '../models/responseBase';
import { LoaderComponent } from '../components/loader/loader.component';
import { ToasterService } from '../components/toaster/toaster.service';
import { actionEnum } from '../enums';
import { LoaderService } from '../components/loader/loader.service';

export class ActionFormBase<T> extends FormBase {
    @ViewChild('loader') loader: LoaderComponent;
    action: actionEnum;
    constructor(private injectorAction: Injector, name: string, navigateBackUrl: string) {
        super(injectorAction, name);
        this.action = this.activeRouter.snapshot.data.action as actionEnum;
        this.navigateBackUrl = navigateBackUrl;
        this.portalService.clearFilters();
    }

    getById(func: (id: number) => Promise<ResponseBase<T>>) {
        LoaderService.show();
        func(this.getId()).then(() => LoaderService.hide()).catch(err => {
            LoaderService.hide();
            ToasterService.handleErrors(err, 'error_global');
        });
    }

    areControlsDisabled = () => this.action === actionEnum.View;

    getId = () => {
        const id = +this.activeRouter.snapshot.params.id;
        return id ? id : 0;
    }

    navigateBack() {
        this.router.navigate([this.navigateBackUrl]);
      }

}
