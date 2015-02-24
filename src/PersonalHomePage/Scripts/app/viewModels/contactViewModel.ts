module PersonalHomePage.ViewModels {
    export class ContactViewModel extends Base.ValidatableObject {
        name: KnockoutObservable<string>;
        email: KnockoutObservable<string>;
        message: KnockoutObservable<string>;
        private toastrOptions : ToastrOptions;
        constructor() {
            super();
            this.name = ko.observable("").extend({ required: true, minLength: 3 });
            this.email = ko.observable("").extend({ required: true, email: true });
            this.message = ko.observable("").extend({ required: true, minLength: 5 });
        }
        validate(): boolean {
            this.errors = ko.validation.group(this);
            this.isValid = this.errors().length === 0;
            return this.isValid;
        }
        submit() {
            if (this.validate()) {
                
            } else {
                
                toastr.warning("Please check your data and re-submit the form.");
            }
        }
    }
}