module PersonalHomePage.Base {
    export class ContentItem {
        public Uri: string;

        constructor(relativePath: string) {
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
    }
}