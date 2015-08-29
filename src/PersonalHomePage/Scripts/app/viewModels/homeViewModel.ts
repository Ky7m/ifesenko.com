module PersonalHomePage.ViewModels {
    export class HomeViewModel {
        constructor(
            public personalPhotoUri: string,
            public certifications: Array<string>,
            public skillItems: Array<Models.SkillItem>,
            public socialProfiles: Array<Models.SocialProfile>,
            public contactViewModel: ContactViewModel
            ) { }
    }
}