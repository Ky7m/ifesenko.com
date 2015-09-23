var PersonalHomePage;
(function (PersonalHomePage) {
    var Models;
    (function (Models) {
        var ContentItem = (function () {
            function ContentItem(relativePath) {
                var options = window["options"];
                this.uri = options.cdnUrl + "/" + relativePath;
                var localPath = "//" + window.location.host + "/" + relativePath;
                if (!options.useCdn) {
                    this.uri = localPath;
                }
            }
            return ContentItem;
        })();
        Models.ContentItem = ContentItem;
    })(Models = PersonalHomePage.Models || (PersonalHomePage.Models = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=contentItem.js.map