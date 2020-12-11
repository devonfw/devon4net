import { Directive, forwardRef } from '@angular/core';
import { NG_VALIDATORS } from '@angular/forms';
import { Validator, AbstractControl } from '@angular/forms';
import { isInteger } from 'lodash';

@Directive({
    selector: '[validateAssistants][formControlName], [validateAssistants][formControl],[validateAssistants][ngModel]',
    providers: [
        { provide: NG_VALIDATORS, useExisting: forwardRef(() => AssistantsValidatorDirective), multi: true },
    ],
})
export class AssistantsValidatorDirective implements Validator {

    validate(c: AbstractControl): { [key: string]: any } {
        return isInteger(c.value) && c.value > 0 && c.value < 9 ? undefined : {
            validateAssistants: {
                valid: false,
            },
        };
    }}
