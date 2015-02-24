var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var PersonalHomePage;
(function (PersonalHomePage) {
    var ViewModels;
    (function (ViewModels) {
        var ContactViewModel = (function (_super) {
            __extends(ContactViewModel, _super);
            function ContactViewModel() {
                _super.call(this);
                this.name = ko.observable("").extend({ required: true, minLength: 3 });
                this.email = ko.observable("").extend({ required: true, email: true });
                this.message = ko.observable("").extend({ required: true, minLength: 5 });
            }
            ContactViewModel.prototype.validate = function () {
                this.errors = ko.validation.group(this);
                this.isValid = this.errors().length === 0;
                return this.isValid;
            };
            ContactViewModel.prototype.submit = function () {
                if (this.validate()) {
                }
                else {
                    toastr.warning("Please check your data and re-submit the form.");
                }
            };
            return ContactViewModel;
        })(PersonalHomePage.Base.ValidatableObject);
        ViewModels.ContactViewModel = ContactViewModel;
    })(ViewModels = PersonalHomePage.ViewModels || (PersonalHomePage.ViewModels = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=contactViewModel.js.map