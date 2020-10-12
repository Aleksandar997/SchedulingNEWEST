export class MatTabModel {
    name: string;
    active: boolean;

    constructor(name: string) {
        this.name = name;
        this.active = false;
    }
}
