/// <reference path="../../typings/nprogress/nprogress.d.ts" />
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
                NProgress.start();
                $.post($form.attr("action"), $form.serialize()).done(response => {
                    var show = response.IsSuccess ? toastr.success : toastr.error;
                    show(response.Message);

					if (response.IsSuccess) {
						(<HTMLFormElement> $form.get(0)).reset();
					}
                }).fail(() => {
                    toastr.error("Internal error. Please try again.");
                }).always(() => {
                    NProgress.done();
                });
            } else {
                toastr.warning("Please check your data and re-submit the form.");
            }

            return false;
        }
    }
}