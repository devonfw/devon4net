import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssistantsValidatorDirective } from './directives/assistant-validator.directive';
import { EmailValidatorDirective } from './directives/email-validator.directive';
import { EqualValidatorDirective } from './directives/equal-validator.directive';

@NgModule({
  imports: [ CommonModule ],
  declarations: [
    EmailValidatorDirective,
    AssistantsValidatorDirective,
    EqualValidatorDirective,
  ],
  exports: [
    EmailValidatorDirective,
    AssistantsValidatorDirective,
    EqualValidatorDirective,
    CommonModule,
  ],
})

export class SharedModule { }
