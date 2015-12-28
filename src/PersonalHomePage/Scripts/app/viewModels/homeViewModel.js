var PersonalHomePage;
(function (PersonalHomePage) {
    var ViewModels;
    (function (ViewModels) {
        var SkillItem = PersonalHomePage.Models.SkillItem;
        var HomeViewModel = (function () {
            function HomeViewModel() {
                this.skillItems = [
                    new SkillItem("Web Applications and Sites", 4),
                    new SkillItem("Web Services and SOA", 4),
                    new SkillItem("Security and Identity", 4),
                    new SkillItem("Cloud Computing", 4),
                    new SkillItem("Data Access, Integration, and Databases", 3),
                    new SkillItem("Desktop Applications", 3),
                    new SkillItem("Big Data", 3),
                    new SkillItem("Mobile Client Applications", 2)
                ];
                this.contactViewModel = new ViewModels.ContactViewModel();
            }
            return HomeViewModel;
        })();
        ViewModels.HomeViewModel = HomeViewModel;
    })(ViewModels = PersonalHomePage.ViewModels || (PersonalHomePage.ViewModels = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
