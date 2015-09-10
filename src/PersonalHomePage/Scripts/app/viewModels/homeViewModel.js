var PersonalHomePage;
(function (PersonalHomePage) {
    var ViewModels;
    (function (ViewModels) {
        var SocialProfile = PersonalHomePage.Models.SocialProfile;
        var ContentItem = PersonalHomePage.Models.ContentItem;
        var SkillItem = PersonalHomePage.Models.SkillItem;
        var HomeViewModel = (function () {
            function HomeViewModel() {
                this.personalPhotoUri = new ContentItem("content/images/profile.jpg").uri;
                this.certifications = [
                    "MCSD: Web Applications",
                    "MS: Programming in C# Specialist",
                    "MS: Programming in HTML5 with JavaScript and CSS3 Specialist",
                    "MS: Delivering Continuous Value with Visual Studio Application Lifecycle Management"
                ];
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
                this.socialProfiles = [
                    new SocialProfile("http://ifesenko.com/go/github", "fa-github-alt"),
                    new SocialProfile("http://ifesenko.com/go/linkedin", "fa-linkedin"),
                    new SocialProfile("http://ifesenko.com/go/twitter", "fa-twitter")
                ];
                this.contactViewModel = new ViewModels.ContactViewModel();
                this.goalMessage = "I'm available for helping your team succeed through focused and effective consulting services in software solution architecture, design, development, security, operations, automated lifecycle management, and more.";
            }
            return HomeViewModel;
        })();
        ViewModels.HomeViewModel = HomeViewModel;
    })(ViewModels = PersonalHomePage.ViewModels || (PersonalHomePage.ViewModels = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=homeViewModel.js.map