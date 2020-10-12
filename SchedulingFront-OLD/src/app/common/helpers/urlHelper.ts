import { HttpParams } from '@angular/common/http';

export class UrlHelper {
    static toQueryParam(param: any) {
        const queryParam = {};
        queryParam[param.constructor.name] = JSON.stringify(param);
        return queryParam;
    }

    static toHttpParams(param: any) {
        let params = new HttpParams();
        Object.keys(param).forEach(e => {
            if (param[e]) {
                params = params.set(e, this.httpParamValue(param[e]));
            }

        });
        return params;
    }

    private static httpParamValue(value: any) {
        return value === 'null' ? null : value;
    }
}
