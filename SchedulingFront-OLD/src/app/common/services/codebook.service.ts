import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ResponseBase } from '../models/responseBase';
import { ICodebookBase, CodebookPaging } from '../models/iCodebookBase';
import { UrlHelper } from '../helpers/urlHelper';
import { ICrudServiceBase } from '../models/iCrudServiceBase';
import { BehaviorSubject, Observable } from 'rxjs';
import { SubjectBase } from 'src/app/models/subjectBase';
import { SubjectAdapter } from '../base/subjectAdapter';

@Injectable({
  providedIn: 'root'
})
export class CodebookService implements ICrudServiceBase<ICodebookBase, CodebookPaging> {
  private url;
  private subject = new Map<string, SubjectAdapter<Array<ICodebookBase>>>();
  private data = new Map<string, Array<ICodebookBase>>();
  type: string;
  getSub(): Observable<Array<ICodebookBase>> {
    if (!this.data.has(this.type)) {
      this.subject.set(this.type, new SubjectAdapter<Array<ICodebookBase>>());
    }
    return this.subject.get(this.type).get();
  }
  setType(type: string) {
    this.type = type;
  }
  // columns = new BehaviorSubject<Array<CodebookColumn>>(null);
  setCodebookUrl(url: string) {
    this.url = '/' + url;
  }
  constructor(private http: HttpClient) { }
  // getById(id: number): Promise<ResponseBase<ICodebookBase>> {
  //   return this.http.get(this.url + '/selectById/' + id).toPromise() as Promise<ResponseBase<ICodebookBase>>;
  // }
  save(request: any): Promise<ResponseBase<any>> {
    return this.http.post(this.url + '/save', request, { headers: new HttpHeaders() })
    .toPromise().then((res: ResponseBase<any>) => {
      this.subject.get(this.type).appendNext(res.data, 'id');
      return res as ResponseBase<any>;
    });
  }

  getAll(paging: CodebookPaging = null): Promise<ResponseBase<Array<ICodebookBase>>> {
    return this.http.get(this.url + '/selectAll').toPromise().then((res: ResponseBase<Array<ICodebookBase>>) => {
      this.subject.get(this.type).next(res.data);
      return res as ResponseBase<any>;
    });
  }
}
// Promise<ResponseBase<CodebookOutputModel<ICodebookBase>>>
