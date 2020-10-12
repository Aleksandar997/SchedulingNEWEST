import { Component, ViewChild, ElementRef, Injector, AfterViewInit, ComponentFactory, ComponentFactoryResolver, ViewContainerRef, OnDestroy } from '@angular/core';
import { FormBase } from '../common/base/formBase';
import { LoaderComponent } from '../common/components/loader/loader.component';
import { LocalizationService } from '../common/services/localization.service';
import { AuthenticationService } from '../common/services/authentication.service';
import { LocalData } from '../common/data/localData';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { Culture } from '../common/models/culture';
import { User } from '../common/models/user';
import { ToasterService } from '../common/components/toaster/toaster.service';
import { PasswordChangeComponent } from '../administration/users/passwordChange/passwordChange.component';
import { Subscription } from 'rxjs';
import { FullPageLoaderService } from '../common/components/fullPageLoader/fullPageLoader.service';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['../../assets/css/accountManagement.css']
})
export class LoginComponent extends FormBase implements AfterViewInit, OnDestroy {
  @ViewChild('passChange', { read: ViewContainerRef }) passChange: ViewContainerRef;
  @ViewChild('loader') loader: LoaderComponent;
  @ViewChild('username') username: ElementRef;
  returnUrl: string;
  loginForm: FormGroup;
  cultures: Array<Culture>;
  selectedCulture: Culture;
  expiredPassword = false;
  cultureSubscription: Subscription;
  constructor(private inj: Injector, private authService: AuthenticationService, private facResolver: ComponentFactoryResolver,
              private localizationService: LocalizationService) {
    super(inj, 'login');
    this.returnUrl = this.activeRouter.snapshot.queryParams.returnUrl || '/';
    this.loginForm = this.fb.group({
      userName: new FormControl(null),
      password: new FormControl(null),
      cultureId: new FormControl(null)
    });
    this.cultureSubscription = this.loginForm.getControls('cultureId').valueChanges.subscribe(c => {
      this.selectedCulture = this.cultures.find(x => x.cultureId === c);
      if (!this.selectedCulture) {
        return;
      }
      this.loadSelectedCulture(this.selectedCulture);
    });

  }

  ngAfterViewInit() {
    this.getLocalizationData();
  }

  ngOnDestroy() {
    this.cultureSubscription.unsubscribe();
  }

  login() {
    const user = this.loginForm.getRawValue() as User;
    FullPageLoaderService.loader$.next(true);
    this.authService.login(user.userName, user.password).then(res => {
      this.navigateOnLogin();
    }).catch(err => {
      FullPageLoaderService.loader$.next(false);
      if (err.error.status === 2) {
        ToasterService.openError(this.getLocalization('password_expired'));
        let compFactory: ComponentFactory<any>;
        compFactory = this.facResolver.resolveComponentFactory(PasswordChangeComponent);
        const comp = this.passChange.createComponent(compFactory);
        comp.instance.init(this.loginForm.getControls('userName').value);
        this.expiredPassword = true;
        return;
      }
      ToasterService.handleErrors(err, 'login_error');
    });
  }

  navigateOnLogin() {
    this.router.navigate([this.returnUrl]);
  }

  getLocalizationData() {
    if (FullPageLoaderService.showLoader) {
      FullPageLoaderService.loader$.next(true);
    }
    this.localizationService.getLocalizationData()
      .then(res => {
        this.cultures = res;
        const previousCulture = LocalData.getCulture();
        const firstCulture: Culture = this.cultures.firstElement();
        this.loginForm.getControls('cultureId').setValue(previousCulture ? +previousCulture : +firstCulture.cultureId);
        setTimeout(() => {
          this.username.nativeElement.focus();
        }, 1);
        FullPageLoaderService.loader$.next(false);
      }).catch((error) => {
        FullPageLoaderService.loader$.next(false);
        ToasterService.openError(this.getLocalization('login_error'));
      });
  }

  loadSelectedCulture(culture: Culture) {
    LocalData.setCulture(culture.cultureId.toString());
    LocalData.setTranslations(culture.localizationPair);
  }

  forgottenPassword() {
    this.router.navigateByUrl('forgotpassword');
  }
}
