var PersonalHomePage;
(function (PersonalHomePage) {
    var Base;
    (function (Base) {
        var ContentItem = (function () {
            function ContentItem(relativePath) {
                var options = window["options"];
                var cdnUrl = options.cdnUrl;
                var enableOptimizations = options.enableOptimizations;
                var host = window.location.host;
                this.Url = cdnUrl + "/" + relativePath;
                var localPath = "//" + host + "/" + relativePath;
                //if debug mode
                if (!enableOptimizations) {
                    this.Url = localPath;
                }
            }
            return ContentItem;
        })();
        Base.ContentItem = ContentItem;
    })(Base = PersonalHomePage.Base || (PersonalHomePage.Base = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=contentItem.js.map