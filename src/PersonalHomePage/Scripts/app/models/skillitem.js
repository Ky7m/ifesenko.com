/// <reference path="skillrate.ts" />
var PersonalHomePage;
(function (PersonalHomePage) {
    var Models;
    (function (Models) {
        var SkillItem = (function () {
            function SkillItem(title, rate) {
                this.title = title;
                this.skillRates = [];
                for (var i = 0; i < 4; i++) {
                    this.skillRates.push(new Models.SkillRate(i, rate));
                }
            }
            return SkillItem;
        })();
        Models.SkillItem = SkillItem;
    })(Models = PersonalHomePage.Models || (PersonalHomePage.Models = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=skillitem.js.map