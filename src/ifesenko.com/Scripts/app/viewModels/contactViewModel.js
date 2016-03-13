var PersonalHomePage;
(function (PersonalHomePage) {
    var ViewModels;
    (function (ViewModels) {
        var ContactViewModel = (function () {
            function ContactViewModel() {
                var _this = this;
                this.closeAlert = function () {
                    _this.isAlertVisible(false);
                    _this.status("");
                    _this.isSuccess(false);
                };
                this.isReady = true;
                this.name = ko.observable("");
                this.email = ko.observable("");
                this.message = ko.observable("");
                this.sendItButtonText = ko.observable("Send it");
                this.isAlertVisible = ko.observable(false);
                this.isSuccess = ko.observable(false);
                this.status = ko.observable("");
            }
            ContactViewModel.prototype.submit = function (form) {
                var _this = this;
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
                    .done(function (response) {
                    _this.isSuccess(response.IsSuccess);
                    _this.status(response.Message);
                    if (response.IsSuccess) {
                        $form.get(0).reset();
                        _this.message("");
                    }
                })
                    .fail(function () {
                    _this.isSuccess(false);
                    _this.status("Internal error. Please try again.");
                })
                    .always(function () {
                    $("#loader").fadeOut("slow");
                    _this.isReady = true;
                    _this.sendItButtonText("Send it");
                    _this.isAlertVisible(true);
                });
                return false;
            };
            return ContactViewModel;
        }());
        ViewModels.ContactViewModel = ContactViewModel;
    })(ViewModels = PersonalHomePage.ViewModels || (PersonalHomePage.ViewModels = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
