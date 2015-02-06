var PersonalHomePage;
(function (PersonalHomePage) {
    var ViewModels;
    (function (ViewModels) {
        var HomeViewModel = (function () {
            function HomeViewModel(certifications, skillItems) {
                this.certifications = certifications;
                this.skillItems = skillItems;
            }
            return HomeViewModel;
        })();
        ViewModels.HomeViewModel = HomeViewModel;
    })(ViewModels = PersonalHomePage.ViewModels || (PersonalHomePage.ViewModels = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=homeViewModel.js.map