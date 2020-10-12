import { ResponseBase } from './responseBase';

export interface ICrudServiceBase<Tmodel, Tpaging> {
    save(model: Tmodel): Promise<ResponseBase<number>>;
}
