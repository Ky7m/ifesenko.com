module PersonalHomePage.Models {
    export class ContentItem {
        uri: string;

        constructor(relativePath: string) {
            const options = window["options"];
            const cdnUrl = options.cdnUrl;
            const useCdn = options.useCdn;
            const host = window.location.host;
            this.uri = cdnUrl + "/" + relativePath;
            const localPath = `//${host}/${relativePath}`;
            if (!useCdn) {
                this.uri = localPath;
            }
        }
    }
}