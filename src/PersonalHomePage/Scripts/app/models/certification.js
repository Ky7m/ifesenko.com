var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var PersonalHomePage;
(function (PersonalHomePage) {
    var Models;
    (function (Models) {
        var Certification = (function (_super) {
            __extends(Certification, _super);
            function Certification(relativePath, description) {
                _super.call(this, relativePath);
                this.description = description;
                this.logoSource = this.CdnPath;
            }
            Certification.prototype.onError = function (imageContext) {
                if (this.LocalPath) {
                    imageContext.onerror = null;
                    imageContext.src = this.LocalPath;
                }
            };
            return Certification;
        })(PersonalHomePage.Base.BaseContentItem);
        Models.Certification = Certification;
    })(Models = PersonalHomePage.Models || (PersonalHomePage.Models = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=certification.js.map