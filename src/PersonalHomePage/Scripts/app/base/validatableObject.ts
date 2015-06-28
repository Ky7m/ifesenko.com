/// <reference path="../../typings/knockout.validation/knockout.validation.d.ts" />
module PersonalHomePage.Base {
    export class ValidatableObject {
        errors: KnockoutValidationErrors;
        isValid: boolean;
    } 
}