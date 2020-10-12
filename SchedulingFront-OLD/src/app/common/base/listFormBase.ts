import { FormBase, ActionType } from './formBase';
import { Injector, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { BasePaging } from '../models/basePaging';
import { FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material';
import { ResponseBase } from '../models/responseBase';
import { DataGridComponent } from '../components/dataGrid/dataGrid.component';

export abstract class ListFormBase<T> extends FormBase implements OnInit, AfterViewInit {
    @ViewChild(DataGridComponent) dataGrid: DataGridComponent<T>;
    dataSource = new MatTableDataSource<T>();
    displayedColumns;
    datasourceLength;
    paging = new BasePaging();
    filters: FormGroup;
    getFunc: (paging: BasePaging) => Promise<ResponseBase<Array<T>>>;
    delFunc: (id: number) => Promise<ResponseBase<number>>;
    constructor(
        private _injector: Injector,
        name: string,
        displayedColumns: string[],
        getFunc: (paging: any) => Promise<ResponseBase<Array<T>>>,
        delFunc: (id: number) => Promise<ResponseBase<number>>) {
        super(_injector, name);
        this.getFunc = getFunc;
        this.delFunc = delFunc;
        this.displayedColumns = displayedColumns;
    }
    ngAfterViewInit() {
        this.getData();
    }
    ngOnInit() {
        this.filters.valueChanges.subscribe(res => {
            this.paging.assign(res);
            this.awaitExecution(() => this.getData());
        });
    }
    getData() {
        if (!this.getFunc) {
            return;
        }
        this.execGetFunc(() => {
            this.paging.onPageChange(this.dataGrid.getSize());
            return this.getFunc(this.paging).then(res => {
              this.dataSource = new MatTableDataSource<T>(res.data);
              this.datasourceLength = res.count;
            }) as Promise<ResponseBase<Array<T>>>;
          });
    }

    onPageChange(size: any) {
        // this.paging.onPageChange(size);
        this.getData();
    }
    onSortChange(sort) {
        this.paging.onSortChange(sort);
        this.getData();
    }

    delete(id: number) {
        this.execFunc(() => {
            return this.delFunc(id);
        }, ActionType.Delete);
    }
}
