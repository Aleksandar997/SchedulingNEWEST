import {
  Component,
  OnInit,
  Input,
  OnDestroy,
  ChangeDetectorRef,
  Injector,
  Renderer2,
  ViewChildren,
  ElementRef,
  QueryList,
  AfterViewInit,
  ViewChild
} from '@angular/core';
import { FormArray, FormBuilder, AbstractControl, FormControl, Validators, FormGroup } from '@angular/forms';
import { MatTableDataSource, MatSelect } from '@angular/material';
import { Subject, Subscription } from 'rxjs';
import { ProductPricelist, ProductSelectListModel } from 'src/app/models/product';
import { ErrorStateMatcherAdapter } from '../../adapters/errorStateMatcherAdapter';
import { DocumentDetail } from 'src/app/models/documentDetail';
import { SelectListModel } from '../../models/selectListModel';
import { SystemService } from 'src/app/services/system.service';
import { LocalData } from '../../data/localData';
import { ToasterService } from '../toaster/toaster.service';
import { FormBase } from '../../base/formBase';

@Component({
  selector: 'detail-action',
  templateUrl: './detailAction.component.html',
  styleUrls: ['./detailAction.component.css']
})
export class DetailActionComponent extends FormBase implements OnInit, OnDestroy {
  @ViewChildren('price') prices: QueryList<ElementRef>;
  @ViewChild('product') product: MatSelect;
  dataSource = new MatTableDataSource<AbstractControl>();
  @Input() gridName;
  @Input() displayedColumns = [];
  formArray = new FormArray([]);
  @Input() areControlsDisabled = false;
  @Input() matcher = new ErrorStateMatcherAdapter();
  organizationUnitId: number;
  productsInfo = new Array<ProductPricelist>();
  data = new Subject<FormArray>();
  sum = new Subject<number>();
  private subs = new Array<Subscription>();
  throwError = new Subject<string>();

  allEmployees = new Array<SelectListModel>();
  employees = new Array<SelectListModel>();

  isAdmin = LocalData.isUserAdmin();

  bindToEmployee = true;
  productSelectList = new Array<ProductSelectListModel>();

  constructor(private inj: Injector, private changeDetector: ChangeDetectorRef,
              private systemService: SystemService, private renderer: Renderer2) {
    super(inj, null);
    this.getLists();
    this.subs.push(
      this.data.subscribe(res => {
        this.formArray = res;

        this.formArray.controls.forEach((x: FormGroup) => {
          x.addControl('availableProducts', new FormArray([]));
          if (this.bindToEmployee) {
            this.setAvailableProducts(x, x.getControls('employeeId').value);
          }
          if (this.areControlsDisabled) {
            x.disable({ onlySelf: true });
          }
          x.get('priceWithDiscount').disable({ onlySelf: true });
          this.setOnValueChange(x as FormGroup);

        });
        this.dataSource.data = this.formArray.controls;
        this.changeDetector.detectChanges();
      })
    );
  }

  setOrganizationUnit(organizationUnitId: number | Array<number>) {
    if (!Array.isArray(organizationUnitId)) {
      this.organizationUnitId = organizationUnitId;
      this.productSelectList = this.productsInfo
        .filter(x => x.organizationUnitId === organizationUnitId)
        .map(x => new ProductSelectListModel(x.productId, x.name, x.price, x.employeeId));
      this.employees = this.allEmployees.filter(x => this.productSelectList.find(p => p.employeeId === x.id));
      this.formArray.controls.forEach((x: FormGroup) => {
        this.setAvailableProducts(x, x.get('employeeId').value);
        x.getControls('productId').updateValueAndValidity();
      });
    }
  }

  bindToEmployeeToggle(bind: boolean) {
    this.bindToEmployee = bind;
    if (bind && this.displayedColumns.indexOf('employee') <= -1) {
      this.displayedColumns.unshift('employee');
    } else if (this.displayedColumns.indexOf('employee') > -1 && !bind) {
      this.displayedColumns.splice(this.displayedColumns.indexOf('employee'), 1);
    }
  }


  calculation(formGroup: FormGroup, productId: number = null) {
    if (!productId) {
      return;
    }
    const fbVal = formGroup.value as DocumentDetail;
    const price = this.productSelectList.find(p => p.id === +productId);
    if (price) {
      ToasterService.openSuccess('price_taken_from_pricelist');
      const priceEl = this.prices.find((item, i) => i === this.formArray.controls.indexOf(formGroup));
      const orgUnitEl = document.getElementById('orgUnitSelect');
      this.renderer.removeClass(priceEl.nativeElement, 'priceFromPricelist');
      this.renderer.removeClass(orgUnitEl, 'priceFromPricelist');
      setTimeout(() => {
        this.renderer.addClass(priceEl.nativeElement, 'priceFromPricelist');
        this.renderer.addClass(orgUnitEl, 'priceFromPricelist');
      }, 1);
      fbVal.price = price.price;
    }
    formGroup.get('price').setValue(fbVal.price);
  }
  isNull = (val: any, elseValue: any) => val ? val : elseValue;

  ngOnInit() {
  }

  ngOnDestroy() {
    this.subs.forEach(x => x.unsubscribe());
  }

  isControlRequired(controlName: string) {
    return this.displayedColumns.indexOf(controlName) > -1 ? [Validators.required] : [];
  }

  isEmployeeRequired = () =>
    this.displayedColumns.indexOf('employee') > -1 && this.bindToEmployee ? [Validators.required] : []

  addDetail() {
    const formGroup = this.fb.group({
      documentDetailId: new FormControl(),
      productId: new FormControl({ value: null, disabled: this.areControlsDisabled }, this.isControlRequired('product')),
      availableProducts: new FormArray([]),
      employeeId: new FormControl({ value: null, disabled: this.areControlsDisabled }, this.isEmployeeRequired()),
      quantity: new FormControl({ value: null, disabled: this.areControlsDisabled }, this.isControlRequired('quantity')),
      price: new FormControl({ value: 0, disabled: this.areControlsDisabled },
        this.displayedColumns.indexOf('price') > -1 ? [Validators.required, Validators.min(1)] : []
      ),
      discount: new FormControl({ value: 0, disabled: this.areControlsDisabled }),
      priceWithDiscount: new FormControl({ value: 0, disabled: true })
    });
    this.setOnValueChange(formGroup);
    this.formArray.push(formGroup);
    this.dataSource.data = this.formArray.controls;
    this.dataSource._updateChangeSubscription();
  }

  setOnValueChange(formGroup: FormGroup) {
    if (this.bindToEmployee) {
      if (!this.isAdmin) {
        formGroup.getControls('employeeId').setValue(LocalData.getUser().employeeId);
      }
      formGroup.getControls('employeeId').valueChanges.subscribe(res => {
        this.setAvailableProducts(formGroup, res);
      });
    } else {
      this.setAvailableProducts(formGroup);
    }
    formGroup.getControls('productId').valueChanges.subscribe(res => {
      this.calculation(formGroup, res);
    });
    formGroup.getControls('discount').valueChanges.subscribe(res => {
      const fbVal = formGroup.value as DocumentDetail;
      formGroup.get('priceWithDiscount').setValue(fbVal.price * (1 - (res ? res : 0) / 100));
      this.calculateSum();
    });

    formGroup.getControls('price').valueChanges.subscribe(res => {
      const fbVal = formGroup.value as DocumentDetail;
      formGroup.get('priceWithDiscount').setValue(res * (1 - (fbVal.discount ? fbVal.discount : 0) / 100));
      this.calculateSum();
    });
  }

  setAvailableProducts(formGroup: FormGroup, employeeId: number = null) {
    formGroup.setControl('availableProducts',
      this.fb.array
      (
        this.productSelectList.
        filter(p => p.employeeId === employeeId || !this.bindToEmployee)
        .map(p => {
          return this.fb.group({
            id: new FormControl(p.id),
            name: new FormControl(p.name)
          }) as FormGroup;
        })
      )
    );
  }

  deleteDetail(index: number) {
    this.formArray.removeAt(index);
    this.dataSource.data = this.formArray.controls;
    this.dataSource._updateChangeSubscription();
  }

  calculateSum() {
    const arr = this.formArray.controls.map((x: FormGroup) => x.get('priceWithDiscount').value);
    if (arr && arr.length > 0) {
      this.sum.next(arr.reduce((a, b) => a + b));
    }
  }

  getLists() {
    this.systemService.getProducts(null, true).then(res => {
      this.productsInfo = res.data.productPricelist;
    }).catch(err => {
      this.throwError.next('product_get_error');
    });

    this.systemService.getEmployees().then(res => {
      this.allEmployees = res.data.map(x => new SelectListModel(x.employee.employeeId, `${x.firstName} ${x.lastName}`));

    }).catch(err => {
      this.throwError.next('employee_get_error');
    });
  }

  onlyUnique(value, index, self) {
    return self.indexOf(value) === index;
  }
}
