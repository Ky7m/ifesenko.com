var PersonalHomePage;
(function (PersonalHomePage) {
    var Models;
    (function (Models) {
        var SkillRate = (function () {
            function SkillRate(index, totalRate) {
                this.skillRate = ko.pureComputed(function () {
                    return index < totalRate ? "skill-rate-on" : "skill-rate-off";
                });
            }
            return SkillRate;
        })();
        Models.SkillRate = SkillRate;
    })(Models = PersonalHomePage.Models || (PersonalHomePage.Models = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=skillRate.js.map