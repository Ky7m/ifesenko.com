/// <reference path="../models/certification.ts" />
/// <reference path="../models/skillitem.ts" />
/// <reference path="contactviewmodel.ts" />
/// <reference path="../models/socialprofile.ts" />
var PersonalHomePage;
(function (PersonalHomePage) {
    var ViewModels;
    (function (ViewModels) {
        var HomeViewModel = (function () {
            function HomeViewModel(personalPhotoUri, certifications, skillItems, socialProfiles, contactViewModel) {
                this.personalPhotoUri = personalPhotoUri;
                this.certifications = certifications;
                this.skillItems = skillItems;
                this.socialProfiles = socialProfiles;
                this.contactViewModel = contactViewModel;
            }
            return HomeViewModel;
        })();
        ViewModels.HomeViewModel = HomeViewModel;
    })(ViewModels = PersonalHomePage.ViewModels || (PersonalHomePage.ViewModels = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=homeviewmodel.js.map