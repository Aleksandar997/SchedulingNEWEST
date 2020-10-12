import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CodebookColumns, ControlType } from '../common/models/codebookModel';
@Component({
  templateUrl: './codebooks.component.html'
})
export class CodebooksComponent {
  fields = new CodebookColumns();
  code: string;
  disableAdd = false;
  constructor(private activatedRoute: ActivatedRoute) {
    this.code = this.activatedRoute.snapshot.data.code.split('/').pop();
    switch (this.code) {
      case 'documentType':
        this.fields
          .append('name', ControlType.Input)
          .append('code', ControlType.Input)
          .append('documentTypeCompany.year', ControlType.NumberInput)
          .append('documentTypeCompany.defaultNumber', ControlType.NumberInput)
          .appendActionColumn();
        this.disableAdd = true;
        break;
      case 'productType':
        this.fields
          .append('name', ControlType.Input)
          .append('active', ControlType.Toggle)
          .appendActionColumn();
        break;
      default:
        break;
    }
  }
}


