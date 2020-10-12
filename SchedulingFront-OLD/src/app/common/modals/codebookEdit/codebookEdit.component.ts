import { Component, OnInit, ViewChild, Inject, AfterViewInit, Injector } from '@angular/core';
import { LoaderComponent } from '../../components/loader/loader.component';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ModalBase } from '../../models/modalBase';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ICrudServiceBase } from '../../models/iCrudServiceBase';
import { CodebookColumns, ControlType } from '../../models/codebookModel';
import { FormBase } from '../../base/formBase';
import { FormGroupHelper } from '../../helpers/formGroupHelper';
import { CodebookService } from '../../services/codebook.service';

@Component({
  templateUrl: './codebookEdit.component.html',
  styleUrls: ['./codebookEdit.component.css']
})
export class CodebookEditComponent extends FormBase implements OnInit, AfterViewInit {
  @ViewChild('loader') loader: LoaderComponent;
  controlType = ControlType;
  controlTypeInput = 0;
  form = new FormGroup({});
  fields = new CodebookColumns();
  type;
  isConfirmed = false;
  constructor(
    private _injector: Injector,
    public dialogRef: MatDialogRef<CodebookEditComponent>,
    private codebookService: CodebookService,
    @Inject(MAT_DIALOG_DATA) public data: ModalBase) {
    super(_injector, 'cb');
    console.log(data.data.fields)
    this.codebookService.setType(this.data.data);
    // const data = this.data.data as CodebookData;
    if (this.data.eventEmitter) {
      this.data.eventEmitter.subscribe(res => {
        if (res) {
          this.loader.show();
          return;
        }
        this.loader.hide();
      });
    }
    this.codebookService.getSub().subscribe(res => {
      console.log(res)
      FormGroupHelper.mapControlsToFormGroup(
        [...data.data.fields.filter(x => x.editable && !x.isAction).map(x => x.name)],
        res.find(x => x['id'] === this.data.data.id), this.form);
      this.fields = data.data.fields.filter(x => x.editable && !x.isAction);
    });
    // this.data.data.service.columns.subscribe(res => {
    //   if (!res) {
    //     return;
    //   }
    //   FormGroupHelper.mapControlsToFormGroup(res.map(x => x.name), {}, this.form);
    //   this.columns = res.map(x => {

    //     x.value = (x.name.split('.').map(n => n.firstCharToLower()) as Array<string>).join('.');
    //     x.label = x.name.split('.').pop().firstCharToLower();
    //     return x;
    //   }).filter(x => x.editable);
    // });
    if (this.data.data.id === 0 || !this.data.data.id) {
      return;
    }
    this.codebookService.getAll();
    // this.data.data.service.getById(this.data.data.id).then(res => {
    //   FormGroupHelper.mapControlsToFormGroup(res.data.columns.map(x => x.name), res.data.data, this.form);
    //   this.columns = res.data.columns.map(x => {

    //     x.value = (x.name.split('.').map(n => n.firstCharToLower()) as Array<string>).join('.');
    //     x.label = x.name.split('.').pop().firstCharToLower();
    //     return x;
    //   }).filter(x => x.editable);
    // }).catch(err => {
    //   this.loader.hide();
    //   ToasterService.handleErrors(err, 'codebook_get_error');
    // });
  }

  isInput = (controlType: ControlType) =>
    controlType === ControlType.Input ||
    controlType === ControlType.NumberInput

  isToggle = (controlType: ControlType) => controlType === ControlType.Toggle;

  ngOnInit() {
  }

  ngAfterViewInit() {
    // if (this.data.data.id === 0 || !this.data.data.id) {
    //   return;
    // }
    // this.loader.show();
    // this.data.data.service.getById(this.data.data.id).then(res => {
    //   this.loader.hide();
    //   this.columns = res.data.columns.map(x => {
    //     x.name = x.name.split('.').pop().firstCharToLower();
    //     return x;
    //   }).filter(x => x.editable);
    //   this.columns.forEach(c => this.form.addControl(c.name, new FormControl()));
    //   FormGroupHelper.mapObjectToFormGroup(res.data.data, this.form);
    //   console.log(this.form)
    // }).catch(() => {
    //   this.loader.hide();
    //   ToasterService.openError('global_error');
    // });
  }

  onDecline() {
    this.dialogRef.close();
  }

  onConfirm() {
    // this.loader.show();
    // this.isConfirmed = true;
    const model = this.form.getRawValue();
    console.log(model)
    // this.data.data.service.save(model).then(res => {
    //   this.loader.hide();
    //   this.dialogRef.close();
    //   this.data.onConfirm();
    // }).catch(() => {
    //   this.loader.hide();
    //   ToasterService.openError('global_save_error');
    // });
  }
}

export class CodebookData {
  id: number;
  type: string;
  fields: CodebookColumns;
  constructor(id: number, type: string, fields: CodebookColumns) {
    this.id = id;
    this.type = type;
    this.fields = fields;
  }
}
