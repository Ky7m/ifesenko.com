var PersonalHomePage;
(function (PersonalHomePage) {
    var ViewModels;
    (function (ViewModels) {
        var HomeViewModel = (function () {
            function HomeViewModel(certifications, skillItems, socialProfiles) {
                this.certifications = certifications;
                this.skillItems = skillItems;
                this.socialProfiles = socialProfiles;
            }
            return HomeViewModel;
        })();
        ViewModels.HomeViewModel = HomeViewModel;
    })(ViewModels = PersonalHomePage.ViewModels || (PersonalHomePage.ViewModels = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=homeViewModel.js.map