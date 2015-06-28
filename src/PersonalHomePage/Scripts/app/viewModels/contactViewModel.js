var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
/// <reference path="../../typings/toastr/toastr.d.ts" />
/// <reference path="../../typings/nprogress/nprogress.d.ts" />
/// <reference path="../base/validatableobject.ts" />
var PersonalHomePage;
(function (PersonalHomePage) {
    var ViewModels;
    (function (ViewModels) {
        var ContactViewModel = (function (_super) {
            __extends(ContactViewModel, _super);
            function ContactViewModel() {
                _super.call(this);
                this.isReady = true;
                this.name = ko.observable("").extend({ required: true, minLength: 3 });
                this.email = ko.observable("").extend({ required: true, email: true });
                this.message = ko.observable("").extend({ required: true, minLength: 5 });
            }
            ContactViewModel.prototype.validate = function () {
                this.errors = ko.validation.group(this);
                this.isValid = this.errors().length === 0;
                return this.isValid;
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
                    NProgress.start();
                    $.post($form.attr("data-action"), $form.serialize()).done(function (response) {
                        var show = response.IsSuccess ? toastr.success : toastr.error;
                        show(response.Message);
                        if (response.IsSuccess) {
                            $form.get(0).reset();
                        }
                    }).fail(function () {
                        toastr.error("Internal error. Please try again.");
                    }).always(function () {
                        NProgress.done();
                        _this.isReady = true;
                    });
                }
                else {
                    toastr.warning("Please check your data and re-submit the form.");
                }
                return false;
            };
            return ContactViewModel;
        })(PersonalHomePage.Base.ValidatableObject);
        ViewModels.ContactViewModel = ContactViewModel;
    })(ViewModels = PersonalHomePage.ViewModels || (PersonalHomePage.ViewModels = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=contactViewModel.js.map