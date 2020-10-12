import { BehaviorSubject, Subject } from 'rxjs';

export class FullPageLoaderService {
    static loader$ = new Subject<boolean>();
    static showLoader = false;
    static show() {
        this.showLoader = true;
        this.loader$.next(true);
    }

    static hide() {
        this.showLoader = false;
        this.loader$.next(false);
    }
}

