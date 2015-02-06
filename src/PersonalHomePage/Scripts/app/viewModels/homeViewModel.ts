module PersonalHomePage.ViewModels {
    export class HomeViewModel {
        constructor(public certifications: Array<Models.Certification>, public skillItems: Array<Models.SkillItem>) {}
    }
}