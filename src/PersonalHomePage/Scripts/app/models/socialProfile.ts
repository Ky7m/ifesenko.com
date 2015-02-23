module PersonalHomePage.Models {
    export class SocialProfile {
        constructor(public link: string, public iconClassName: string) {
            this.iconClassName = "fa " + iconClassName;
        }
    }
} 