import type { DropDownItemType } from "./DropDownItemType";

export class DropDownItem {
    name: string;
    type: DropDownItemType;
    url: string;

    constructor(name: string, type: DropDownItemType, url: string = '') {
        this.name = name;
        this.type = type;
        this.url = url;
    }
}