var PersonalHomePage;
(function (PersonalHomePage) {
    var Helpers;
    (function (Helpers) {
        var Preloader = (function () {
            function Preloader(statusElementSelector, imageElementSelector) {
                this.imageElementSelector = imageElementSelector;
                this.statusElementSelector = statusElementSelector;
            }
            Preloader.prototype.attach = function (window) {
                var _this = this;
                $(window).load(function () {
                    $(_this.statusElementSelector).fadeOut();
                    $(_this.imageElementSelector).fadeOut("slow");
                });
            };
            return Preloader;
        })();
        Helpers.Preloader = Preloader;
    })(Helpers = PersonalHomePage.Helpers || (PersonalHomePage.Helpers = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=preloader.js.map