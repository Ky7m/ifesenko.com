module PersonalHomePage.Base {
    export class ContentItem {
        public Url: string;

        constructor(relativePath: string) {
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
    }
}