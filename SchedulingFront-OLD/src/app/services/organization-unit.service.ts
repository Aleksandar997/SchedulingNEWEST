import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { OrganizationUnitPaging, OrganizationUnit } from '../models/organizationUnit';
import { ResponseBase } from '../common/models/responseBase';
import { UrlHelper } from '../common/helpers/urlHelper';
import { ICrudServiceBase } from '../common/models/iCrudServiceBase';

@Injectable({
  providedIn: 'root'
})
export class OrganizationUnitService implements ICrudServiceBase<OrganizationUnit, OrganizationUnitPaging> {

  constructor(private http: HttpClient) { }

  url = '/organizationUnit';

  delete(organizationUnitId: number): Promise<ResponseBase<number>> {
    return this.http.delete(this.url + '/delete/' + organizationUnitId, { headers: new HttpHeaders() })
    .toPromise() as Promise<ResponseBase<number>>;
  }
  getAll(organizationUnitPaging: OrganizationUnitPaging): Promise<ResponseBase<Array<OrganizationUnit>>> {
    return this.http.get(this.url + '/selectAll', {
      headers: new HttpHeaders(),
      params: UrlHelper.toHttpParams(organizationUnitPaging)
    }).toPromise() as Promise<ResponseBase<Array<OrganizationUnit>>>;
  }

  getById(organizationUnitId: number): Promise<ResponseBase<OrganizationUnit>> {
    return this.http.get(this.url + '/selectById/' + organizationUnitId).toPromise() as Promise<ResponseBase<OrganizationUnit>>;
  }

  save(organizationUnit: OrganizationUnit): Promise<ResponseBase<number>> {
    return this.http.post(this.url + '/save', organizationUnit, {
      headers: new HttpHeaders()
    }).toPromise() as Promise<ResponseBase<number>>;
  }
}
