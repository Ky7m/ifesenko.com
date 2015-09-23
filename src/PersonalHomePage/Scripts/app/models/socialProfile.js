var PersonalHomePage;
(function (PersonalHomePage) {
    var Models;
    (function (Models) {
        var SocialProfile = (function () {
            function SocialProfile(link, iconClassName) {
                this.link = link;
                this.iconClassName = iconClassName;
                this.iconClassName = "fa " + iconClassName;
            }
            return SocialProfile;
        })();
        Models.SocialProfile = SocialProfile;
    })(Models = PersonalHomePage.Models || (PersonalHomePage.Models = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=socialProfile.js.map