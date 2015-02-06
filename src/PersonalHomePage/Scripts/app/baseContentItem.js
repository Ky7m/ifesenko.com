var BaseContentItem = (function () {
    function BaseContentItem(relativePath) {
        var options = window["options"];
        var cdnUrl = options.cdnUrl;
        var enableOptimizations = options.enableOptimizations;
        var host = window.location.host;
        this.CdnPath = cdnUrl + "/" + relativePath;
        this.LocalPath = "//" + host + "/" + relativePath;
        //if debug mode
        if (!enableOptimizations) {
            this.CdnPath = this.LocalPath;
        }
    }
    return BaseContentItem;
})();
//# sourceMappingURL=baseContentItem.js.map