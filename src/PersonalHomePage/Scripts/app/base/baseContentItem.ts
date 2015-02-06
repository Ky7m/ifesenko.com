module PersonalHomePage.Base {
    export class BaseContentItem {
        protected CdnPath: string;
        protected LocalPath: string;

        constructor(relativePath: string) {
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
    } 
}