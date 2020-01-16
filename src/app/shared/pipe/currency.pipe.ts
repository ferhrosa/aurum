import { CurrencyPipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'currency'
})
export class CustomCurrencyPipe extends CurrencyPipe implements PipeTransform {
    constructor() {
        super('nl');
    }

    transform(
        value: any,
        code = 'BRL',
        display = 'symbol',
        digitsInfo = '1.2-2',
        locale = 'pt-BR'
    ) {
        return super.transform(value, code, display, digitsInfo, locale);
    }

}
