var PersonalHomePage;
(function (PersonalHomePage) {
    var Base;
    (function (Base) {
        var ContentItem = (function () {
            function ContentItem(relativePath) {
                var options = window["options"];
                var cdnUrl = options.cdnUrl;
                var useCdn = options.useCdn;
                var host = window.location.host;
                this.Uri = cdnUrl + "/" + relativePath;
                var localPath = "//" + host + "/" + relativePath;
                //if debug mode
                if (!useCdn) {
                    this.Uri = localPath;
                }
            }
            return ContentItem;
        })();
        Base.ContentItem = ContentItem;
    })(Base = PersonalHomePage.Base || (PersonalHomePage.Base = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
