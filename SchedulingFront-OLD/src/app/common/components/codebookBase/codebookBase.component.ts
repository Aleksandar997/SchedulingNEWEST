import { Component, OnInit, Input, AfterViewInit, ViewChild, Injector } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { CodebookService } from '../../services/codebook.service';
import { LoaderComponent } from '../loader/loader.component';
import { CodebookEditComponent, CodebookData } from '../../modals/codebookEdit/CodebookEdit.component';
import { ModalBase } from '../../models/modalBase';
import { ListFormBase } from '../../base/listFormBase';
import { CodebookColumn, CodebookColumns, ControlType } from '../../models/codebookModel';
import { ResponseBase } from '../../models/responseBase';
import { ICodebookBase } from '../../models/iCodebookBase';

@Component({
  selector: 'codebook',
  templateUrl: './codebookBase.component.html',
  styleUrls: ['./codebookBase.component.css']
})
export class CodebookBaseComponent extends ListFormBase<any> implements OnInit, AfterViewInit {
  @ViewChild('actions', { static: true }) actions;
  @Input() enableActionForm = false;
  @ViewChild('loader') loader: LoaderComponent;
  @Input() fields = new CodebookColumns();
  matColumnsDef = [];
  @Input() code = '';
  private url: string;
  @Input() disableAdd = false;
  constructor(private inj: Injector, private activatedRoute: ActivatedRoute, private codebookService: CodebookService) {
    super(inj,
          activatedRoute.snapshot.data.code,
          null,
          (p) => this.codebookService.getAll(p) as any,
          null);
    this.code = this.activatedRoute.snapshot.data.code;
    this.url = this.activatedRoute.snapshot.data.url;
    this.filters = this.fb.group({
      name: new FormControl(),
      code: new FormControl()
    });
  }
  ngAfterViewInit() {
    setTimeout(() => this.portalService.registerActions(this.actions), 1);
    this.codebookService.getSub().subscribe(res => {
      this.dataSource.data = res;
    });
    // super.ngAfterViewInit();
    this.getData();
  }

  getData() {
    this.execGetFunc(() => this.getFunc(this.paging) as Promise<ResponseBase<Array<ICodebookBase>>>);
  }

  edit(id: number = null) {
    if (this.enableActionForm) {
      this.router.navigateByUrl(this.router.url + '/edit/' + id);
      return;
    }
    this.modal.openDialog(
      new ModalBase(
        'confirm' + this.code.insertStringBetweenUpper('_') + '_edit_title',
        null,
        new CodebookData(id, this.code, this.fields),
        null,
        () => this.getData()
      ), CodebookEditComponent);
  }

  ngOnInit() {
    this.codebookService.setCodebookUrl(this.url);
    this.codebookService.setType(this.code);
    this.displayedColumns = this.fields.map(x => x.name);
    this.matColumnsDef = this.fields.filter(x => !x.isAction);
    super.ngOnInit();
  }

  isCheckbox = (col: CodebookColumn) => col.controlType === ControlType.Toggle;
}


