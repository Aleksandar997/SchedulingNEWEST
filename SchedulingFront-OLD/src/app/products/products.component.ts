import { Component, OnInit, AfterViewInit, Injector } from '@angular/core';
import { Product } from '../models/product';
import { FormControl } from '@angular/forms';
import { ProductService } from '../services/product.service';
import { SystemService } from '../services/system.service';
import { SelectListModel } from '../common/models/selectListModel';
import { ResponseBase } from '../common/models/responseBase';
import { OrganizationUnit } from '../models/organizationUnit';
import { ProductType } from '../models/productType';
import { ListFormBase } from '../common/base/listFormBase';

@Component({
  selector: 'products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent extends ListFormBase<Product> implements OnInit, AfterViewInit {
  constructor(private inj: Injector, private service: ProductService, private systemService: SystemService) {
    super(inj,
          'product',
          ['name', 'code', 'productTypeName', 'active', 'actions'],
          (p) => this.service.getProducts(p),
          (id) => this.service.deleteProduct(id));
    this.filters = this.fb.group({
      name: new FormControl(),
      code: new FormControl(),
      productTypes: new FormControl(),
      organizationUnits: new FormControl([])
    });
  }
  organizationUnits = new Array<SelectListModel>();
  productTypes = new Array<SelectListModel>();


  ngAfterViewInit() {
    this.selectList();
    super.ngAfterViewInit();
  }

  selectList() {
    this.execGetFunc(() => {
      return this.systemService.getOrganizationUnits().then(res => {
        this.organizationUnits = res.data.map(x => new SelectListModel(x.organizationUnitId, x.name));
      }) as Promise<ResponseBase<Array<OrganizationUnit>>>;
    });
    this.execGetFunc(() => {
      return this.systemService.getProductTypes().then(res => {
        this.productTypes = res.data.map(x => new SelectListModel(x.productTypeId, x.name));
      }) as Promise<ResponseBase<Array<ProductType>>>;
    });

  }

  onRowClickLink = (product: Product) => '/edit/' + product.productId;
}
