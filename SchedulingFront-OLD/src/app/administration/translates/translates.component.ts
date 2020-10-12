import { Component, OnInit, Injector, AfterViewInit, ViewChild, EventEmitter } from '@angular/core';
import { FormBase } from 'src/app/common/base/formBase';
import { Resource, ResourcePaging } from 'src/app/common/models/resource';
import { LoaderComponent } from 'src/app/common/components/loader/loader.component';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { ModalBaseComponent } from 'src/app/common/modals/modalBase/modalBase.component';
import { LocalizationService } from 'src/app/common/services/localization.service';
import { ToasterService } from 'src/app/common/components/toaster/toaster.service';
import { DataGridComponent } from 'src/app/common/components/dataGrid/dataGrid.component';
import { ResponseBase } from 'src/app/common/models/responseBase';
import { ListFormBase } from 'src/app/common/base/listFormBase';

@Component({
  templateUrl: './translates.component.html',
  styleUrls: ['./translates.component.css']
})
export class TranslatesComponent extends ListFormBase<Resource> {
  @ViewChild(DataGridComponent) dataGrid: DataGridComponent<any>;
  constructor(private inj: Injector, private localizationService: LocalizationService) {
    super(inj,
      'localization',
      ['resource', 'translates', 'cultures', 'actions'],
      (p) => this.localizationService.selectAll(p),
      null);
    this.filters = this.fb.group({
      resource: new FormControl(),
      translate: new FormControl()
    });
  }

  // getData() {
  //   this.paging.onPageChange(this.dataGrid.getSize());
  //   this.execGetFunc(() => {
  //     return this.localizationService.selectAll(this.paging).then(res => {
  //       this.dataSource = new MatTableDataSource<Resource>(res.data);
  //       this.datasourceLength = res.count;
  //     }) as Promise<ResponseBase<Array<Resource>>>;
  //   });
  // }
  onRowClickLink = (resource: Resource) => '/edit/' + resource.resourceId;
}
