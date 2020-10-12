export class CodebookColumn {
    name: string;
    controlType: ControlType;
    editable: boolean;
    isAction: boolean;
    constructor(name: string, controlType: ControlType, editable: boolean) {
        this.name = name;
        this.controlType = controlType;
        this.editable = editable;
    }
}

export class CodebookColumns extends Array<CodebookColumn> {
    append(name: string, controlType: ControlType, editable: boolean = true) {
        this.push(new CodebookColumn(name, controlType, editable));
        return this;
    }

    appendActionColumn() {
        const cb = new CodebookColumn('actions', null, false);
        cb.isAction = true;
        this.push(cb);
    }
}

export enum ControlType { Input = 0, NumberInput = 1, DateTimePicker, SelectList, Toggle, File }
