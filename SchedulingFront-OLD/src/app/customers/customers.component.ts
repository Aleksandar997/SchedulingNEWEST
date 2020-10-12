import { Component, Injector, OnInit } from '@angular/core';
import { ListFormBase } from '../common/base/listFormBase';
import { CodebookColumns, ControlType } from '../common/models/codebookModel';
import { Customer } from '../models/customer';
import { CustomerService } from '../services/customer.service';

@Component({
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent extends ListFormBase<Customer> implements OnInit {
  fields = new CodebookColumns();
  constructor(private inj: Injector, private customerService: CustomerService) {
    super(inj,
          'product',
          ['firstName', 'lastName', 'phoneNumber', 'actions'],
          null,
          null);
    // this.filters = this.fb.group({
    //   name: new FormControl(),
    //   code: new FormControl(),
    //   productTypes: new FormControl(),
    //   organizationUnits: new FormControl([])
    // });
    this.customerService.customers.subscribe(res => {

    })
  }

  ngOnInit() {
  }

}
