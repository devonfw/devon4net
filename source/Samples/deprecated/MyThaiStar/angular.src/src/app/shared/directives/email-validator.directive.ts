import { Directive, forwardRef } from '@angular/core';
import { NG_VALIDATORS } from '@angular/forms';
import { Validator, AbstractControl } from '@angular/forms';

// Function exported to be used in this directive and unit tests
export function emailValidator(c: string): boolean {
    let regExp: RegExp = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    return regExp.test(c);
}

@Directive({
    selector: '[validateEmail][formControlName], [validateEmail][formControl],[validateEmail][ngModel]',
    providers: [
        { provide: NG_VALIDATORS, useExisting: forwardRef(() => EmailValidatorDirective), multi: true },
    ],
})
export class EmailValidatorDirective implements Validator {
    validate(c: AbstractControl): { [key: string]: any } {
        return emailValidator(<string>c.value) ? undefined : {
            validateEmail: {
                valid: false,
            },
        };
    }
}
