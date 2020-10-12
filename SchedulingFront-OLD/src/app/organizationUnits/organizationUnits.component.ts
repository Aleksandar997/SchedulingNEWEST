import { Component, Injector} from '@angular/core';
import { OrganizationUnit } from '../models/organizationUnit';
import { FormControl } from '@angular/forms';
import { ListFormBase } from '../common/base/listFormBase';
import { OrganizationUnitService } from '../services/organization-unit.service';

@Component({
  templateUrl: './organizationUnits.component.html'
})
export class OrganizationUnitsComponent extends ListFormBase<OrganizationUnit> {
  constructor(private inj: Injector, private service: OrganizationUnitService) {
    super(
      inj,
      'organizationUnits',
      ['name', 'code', 'active', 'actions'],
      (p) => this.service.getAll(p),
      (id) => this.service.delete(id));
    this.filters = this.fb.group({
      name: new FormControl(),
      code: new FormControl(),
      productTypes: new FormControl(),
      organizationUnits: new FormControl([])
    });
  }

  onRowClickLink = (organizationUnit: OrganizationUnit) => '/edit/' + organizationUnit.organizationUnitId;

}
