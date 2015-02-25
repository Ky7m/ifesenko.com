module PersonalHomePage.ViewModels {
    export class ContactViewModel extends Base.ValidatableObject {
        name: KnockoutObservable<string>;
        email: KnockoutObservable<string>;
        message: KnockoutObservable<string>;
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
        submit(form) {
            // Stop form from submitting normally
            event.preventDefault();
            toastr.clear();
            if (this.validate()) {
                var $form = $(form);
                $.post($form.attr("action"), $form.serialize()).done(data => {
                    toastr.info(JSON.stringify(data));
                    if (data.status === "success") {

                    } else {
                        if (data.errors) {
                            // display errors
                        }
                    }
                }).fail(() => {
                    toastr.error("Internal error. Please try again.");
                });
            } else {
                toastr.warning("Please check your data and re-submit the form.");
            }

            return false;
        }
    }
}