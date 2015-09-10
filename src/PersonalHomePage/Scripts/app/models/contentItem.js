var PersonalHomePage;
(function (PersonalHomePage) {
    var Models;
    (function (Models) {
        var ContentItem = (function () {
            function ContentItem(relativePath) {
                var options = window["options"];
                var cdnUrl = options.cdnUrl;
                var useCdn = options.useCdn;
                var host = window.location.host;
                this.uri = cdnUrl + "/" + relativePath;
                var localPath = "//" + host + "/" + relativePath;
                if (!useCdn) {
                    this.uri = localPath;
                }
            }
            return ContentItem;
        })();
        Models.ContentItem = ContentItem;
    })(Models = PersonalHomePage.Models || (PersonalHomePage.Models = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=contentItem.js.map