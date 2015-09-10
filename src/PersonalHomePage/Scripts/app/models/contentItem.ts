module PersonalHomePage.Models {
    export class ContentItem {
        uri: string;

        constructor(relativePath: string) {
            const options = window["options"];
            this.uri = options.cdnUrl + "/" + relativePath;
            const localPath = `//${window.location.host}/${relativePath}`;
            if (!options.useCdn) {
                this.uri = localPath;
            }
        }
    }
}