module PersonalHomePage.ViewModels {
    export class ContactViewModel {
        name: KnockoutObservable<string>;
        email: KnockoutObservable<string>;
        message: KnockoutObservable<string>;
        isReady: boolean;

        constructor() {
            this.isReady = true;
            this.name = ko.observable("").extend({ required: true, minLength: 3 });
            this.email = ko.observable("").extend({ required: true, email: true });
            this.message = ko.observable("").extend({ required: true, minLength: 5 });
        }
        validate(): boolean {
            const errors = ko.validation.group(this);
            return errors().length === 0;
        }
        submit(form) {
            // Stop form from submitting normally
            event.preventDefault();

            // prevent multiply submit
            if (!this.isReady) {
                return false;
            }

            toastr.clear();
            if (this.validate()) {
                this.isReady = false;
                var $form = $(form);
                $("#loader").fadeIn("slow");
                $.post($form.attr("data-action"), $form.serialize())
                    .done(response => {
                        var show = response.IsSuccess ? toastr.success : toastr.error;
                        show(response.Message);
                        if (response.IsSuccess) {
                            (<HTMLFormElement> $form.get(0)).reset();
                        }
                    })
                    .fail(() => {
                        toastr.error("Internal error. Please try again.");
                    })
                    .always(() => {
                        $("#loader").fadeOut("slow");
                        this.isReady = true;
                    });
            } else {
                toastr.warning("Please check your data and re-submit the form.");
            }

            return false;
        }
    }
}