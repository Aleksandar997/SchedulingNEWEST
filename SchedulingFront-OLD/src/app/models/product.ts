import { ProductType } from './productType';
import { BasePaging } from '../common/models/basePaging';
import { SelectListModel } from '../common/models/selectListModel';

export class Product {
    productId: number;
    code: string;
    name: string;
    image: string;
    active: boolean;
    productTypeid: number;
    productType: ProductType;
    price: number;
    organizationUnitId: number;
    organizationUnits = new Array<number>();
    employees: Array<number>;
    productPricelist = new Array<ProductPricelist>();
    organizationUnitsString: string;
}

export class ProductPricelist {
    organizationUnitId: number;
    organizationUnitName: string;
    documentDetailId: number;
    price: number;
    documentId: number;
    productId: number;
    name: string;
    employeeId: number;
    constructor(organizationUnitId: number = null, organizationUnitName: string = null) {
        this.organizationUnitId = organizationUnitId;
        this.organizationUnitName = organizationUnitName;
        this.documentDetailId = null;
        this.price = null;
        this.documentId = null;
    }
}

export class ProductPaging extends BasePaging {
    name: string;
    code: string;
    productTypeId: number;
    organizationUnits = new Array<number>();
}

export class ProductSelectList {
    products: Array<Product>;
    productPricelist = new Array<ProductPricelist>();

}
export class ProductSelectListInput {
    allOrgUnits: boolean;
    organizationUnits = new Array<number>();
    constructor(organizationUnits: Array<number>, allOrgUnits = false) {
        this.organizationUnits = organizationUnits;
        this.allOrgUnits = allOrgUnits;
    }
}

export class ProductSelectListModel extends SelectListModel {
    price: number;
    employeeId: number;
    constructor(id: number, name: string, price: number, employeeId: number) {
        super(id, name);
        this.price = price;
        this.employeeId = employeeId;
    }
}
