module PersonalHomePage.ViewModels {
    export class ContactViewModel {
        name: KnockoutObservable<string>;
        email: KnockoutObservable<string>;
        message: KnockoutObservable<string>;
        isReady: boolean;
        sendItButtonText: KnockoutObservable<string>;
        isAlertVisible: KnockoutObservable<boolean>;
        isSuccess: KnockoutObservable<boolean>;
        status: KnockoutObservable<string>;

        constructor() {
            this.isReady = true;
            this.name = ko.observable("");
            this.email = ko.observable("");
            this.message = ko.observable("");
            this.sendItButtonText = ko.observable("Send it");
            this.isAlertVisible = ko.observable(false);
            this.isSuccess = ko.observable(false);
            this.status = ko.observable("");
        }
        submit(form) {
            // Stop form from submitting normally
            event.preventDefault();

            // prevent multiply submit
            if (!this.isReady) {
                return false;
            }
            this.closeAlert();
            this.sendItButtonText("Sending...");
            this.isReady = false;
            var $form = $(form);
            $("#loader").fadeIn("slow");
            $.post($form.attr("data-action"), $form.serialize())
                .done(response => {
                    this.isSuccess(response.IsSuccess);
                    this.status(response.Message);
                    if (response.IsSuccess) {
                        (<HTMLFormElement>$form.get(0)).reset();
                        this.message("");
                    }
                })
                .fail(() => {
                    this.isSuccess(false);
                    this.status("Internal error. Please try again.");
                })
                .always(() => {
                    $("#loader").fadeOut("slow");
                    this.isReady = true;
                    this.sendItButtonText("Send it");
                    this.isAlertVisible(true);
                });

            return false;
        }

        closeAlert = () => {
            this.isAlertVisible(false);
            this.status("");
            this.isSuccess(false);
        }
    }
}