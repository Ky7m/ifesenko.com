/// <reference path="../models/certification.ts" />
/// <reference path="../models/skillitem.ts" />
/// <reference path="contactviewmodel.ts" />
/// <reference path="../models/socialprofile.ts" />
module PersonalHomePage.ViewModels {
    export class HomeViewModel {
        constructor(
            public personalPhotoUri: string,
            public certifications: Array<Models.Certification>,
            public skillItems: Array<Models.SkillItem>,
            public socialProfiles: Array<Models.SocialProfile>,
            public contactViewModel: ContactViewModel
            ) { }
    }
}