var PersonalHomePage;
(function (PersonalHomePage) {
    var ViewModels;
    (function (ViewModels) {
        var ContactViewModel = (function () {
            function ContactViewModel() {
                this.isReady = true;
                this.name = ko.observable("").extend({ required: true, minLength: 3 });
                this.email = ko.observable("").extend({ required: true, email: true });
                this.message = ko.observable("").extend({ required: true, minLength: 5 });
            }
            ContactViewModel.prototype.validate = function () {
                var errors = ko.validation.group(this);
                return errors().length === 0;
            };
            ContactViewModel.prototype.submit = function (form) {
                var _this = this;
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
                        .done(function (response) {
                        var show = response.IsSuccess ? toastr.success : toastr.error;
                        show(response.Message);
                        if (response.IsSuccess) {
                            $form.get(0).reset();
                        }
                    })
                        .fail(function () {
                        toastr.error("Internal error. Please try again.");
                    })
                        .always(function () {
                        $("#loader").fadeOut("slow");
                        _this.isReady = true;
                    });
                }
                else {
                    toastr.warning("Please check your data and re-submit the form.");
                }
                return false;
            };
            return ContactViewModel;
        })();
        ViewModels.ContactViewModel = ContactViewModel;
    })(ViewModels = PersonalHomePage.ViewModels || (PersonalHomePage.ViewModels = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
