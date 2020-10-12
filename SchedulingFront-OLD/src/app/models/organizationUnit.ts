import { Company } from '../common/models/company';
import { BasePaging } from '../common/models/basePaging';
import { SelectListModel } from '../common/models/selectListModel';

export class OrganizationUnit {
    id: number;
    organizationUnitId: number;
    companyId: number;
    company: Company;
    code: string;
    name: string;
    active: boolean;
    bindScheduleToEmployee: boolean;
}

export class OrganizationUnitPaging extends BasePaging {
    code: string;
    name: string;
}

export class OrganizationUnitSelectListModel extends SelectListModel {
    bindScheduleToEmployee: boolean;

    constructor(id: number, name: string, bindScheduleToEmployee: boolean) {
        super(id, name);
        this.bindScheduleToEmployee = bindScheduleToEmployee;
    }
}
