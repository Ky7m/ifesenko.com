
class Certification extends BaseContentItem {
    public logoSource:string;
    constructor(relativePath: string, public description: string) {
        super(relativePath);
        this.logoSource = this.CdnPath;
    } 
    
    onError(imageContext: HTMLImageElement) {
        if (this.LocalPath) {
            imageContext.onerror = null; imageContext.src = this.LocalPath;
        }
    }
} 