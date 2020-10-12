import { Component, OnInit, AfterViewInit, ViewChild, Injector } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { DocumentService } from 'src/app/services/document.service';
import { DocumentPaging } from 'src/app/models/document';
import { Document } from '../../models/document';
import { DataGridComponent } from 'src/app/common/components/dataGrid/dataGrid.component';
import { SelectListModel } from 'src/app/common/models/selectListModel';
import { SystemService } from 'src/app/services/system.service';
import { CustomerService } from 'src/app/services/customer.service';
import { FormControl } from '@angular/forms';
import { ResponseBase } from 'src/app/common/models/responseBase';
import { OrganizationUnit } from 'src/app/models/organizationUnit';
import { PriceListType } from 'src/app/models/priceListType';
import { DocumentStatus } from 'src/app/models/documentStatus';
import { ListFormBase } from 'src/app/common/base/listFormBase';

@Component({
  selector: 'document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.css']
})
export class DocumentComponent extends ListFormBase<Document> implements OnInit, AfterViewInit {
  @ViewChild('documentGrid') documentGrid: DataGridComponent<Document>;
  documentType: string;
  reuseSub: Subscription;
  organizationUnits = new Array<SelectListModel>();
  customers = new Array<SelectListModel>();
  pricelistTypes = new Array<SelectListModel>();
  customersSub: Subscription;
  getDataInConstructor = false;
  documentStatuses = new Array<SelectListModel>();
  constructor(private inj: Injector, private _route: ActivatedRoute, private documentService: DocumentService,
              private systemService: SystemService, private customerService: CustomerService) {
    super(inj, null, null, (paging) => this.documentService.selectAllByType(paging), (id) => this.documentService.cancel(id));
    this.reuseSub = this._route.params.subscribe(params => {
      if (this.documentType) {
        this.getDataInConstructor = true;
      }
      this.documentType = params.code;
      if (this.documentType === 'pricelists') {
        this.filters = this.fb.group({
          documentNumber: new FormControl(),
          documentStatusId: new FormControl(),
          date: new FormControl(),
          pricelistTypes: new FormControl([]),
          dateFrom: new FormControl(),
          dateTo: new FormControl(),
          organizationUnits: new FormControl([])
        });
        this.displayedColumns = [
          'fullNumber',
          'documentStatus',
          'date',
          'dateTo',
          'dateFrom',
          'pricelistType',
          'organizationUnit',
          'actions'
        ];
      } else {
        this.filters = this.fb.group({
          documentNumber: new FormControl(),
          documentStatusId: new FormControl(),
          date: new FormControl(),
          customers: new FormControl([]),
          organizationUnits: new FormControl([])
        });
        this.displayedColumns = [
          'fullNumber',
          'documentStatus',
          'date',
          'customer',
          'organizationUnit',
          'sum',
          'actions'
        ];
      }
      this.paging = new DocumentPaging(this.documentType);
      this._route.snapshot.data.title = this._route.snapshot.data.title + '_' + this.documentType;
      if (this.getDataInConstructor) {
        this.getData();
      }
    });
    this.customersSub = this.customerService.customers.subscribe(res => {
      this.customers = res.map(x => new SelectListModel(x.customerId, x.firstName + ' ' + x.lastName));
    });
  }

  ngAfterViewInit() {
    this.selectList();
    super.ngAfterViewInit();
  }

  selectList() {
    this.customerService.selectAll();
    this.execGetFunc(() => {
      return this.systemService.getOrganizationUnits().then(res => {
        this.organizationUnits = res.data.map(x => new SelectListModel(x.organizationUnitId, x.name));
      }) as Promise<ResponseBase<Array<OrganizationUnit>>>;
    });

    this.execGetFunc(() => {
      return this.systemService.getPricelistTypes().then(res => {
        this.pricelistTypes = res.data.map(x => new SelectListModel(x.pricelistTypeId,
          this.getLocalization('label' + x.name.insertStringBetweenUpper('_')))
        );
      }) as Promise<ResponseBase<Array<PriceListType>>>;
    });

    this.execGetFunc(() => {
      return this.systemService.getDocumentStatuses().then(res => {
        this.documentStatuses = res.data.map(x => new SelectListModel(x.documentStatusId,
          this.getLocalization('label' + x.code.insertStringBetweenUpper('_'))
        ));
      }) as Promise<ResponseBase<Array<DocumentStatus>>>;
    });
  }

  onRowClickLink = (document: Document) => (this.checkType('pricelists') ? '/edit/' : '/view/') + document.documentId;

  checkType = (type: string) => this.documentType === type;

  viewSchedule(scheduleId: number, index: number) {
    this.documentGrid.setCacheActivePage(index);
    this.router.navigateByUrl('/schedules/view/' + scheduleId, { state: { prevUrl: 'documents/' + this.documentType } });
  }
}
