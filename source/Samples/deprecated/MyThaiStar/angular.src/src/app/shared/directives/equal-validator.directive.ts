import { Directive, forwardRef, Input } from '@angular/core';
import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';

@Directive({
    selector: '[validateEqual][formControlName],[validateEqual][formControl],[validateEqual][ngModel]',
    providers: [
        { provide: NG_VALIDATORS, useExisting: forwardRef(() => EqualValidatorDirective), multi: true },
    ],
})
export class EqualValidatorDirective implements Validator {

    @Input('validateEqual') public validateEqual: string;
    // reverse property is to check if both fields are equal not matter which one is modified first
    @Input('reverse') public reverse: string;

    validate(control: AbstractControl): { [key: string]: any } {

        // control value
        let controlValue: AbstractControl = control.root.get(this.validateEqual);

        if (!controlValue) {
            return undefined;
        }

        if (this.isReverse()) {
            if (this.isValueEqual(control, controlValue)) {
                delete controlValue.errors.validateEqual;
                if (!Object.keys(controlValue.errors).length) {
                    controlValue.setErrors(undefined);
                }
            } else {
                controlValue.setErrors({
                    validateEqual: false,
                });
            }
        } else {
            if (!this.isValueEqual(control, controlValue)) {
              return {
                validateEqual: false,
              };
            }
        }

        return undefined;
    }

    private isValueEqual(c1: AbstractControl, c2: AbstractControl): boolean {
        return c1.value === c2.value;
    }

    private isReverse(): boolean {
        return this.reverse && this.reverse === 'true';
    }
}
