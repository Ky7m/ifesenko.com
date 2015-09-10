module PersonalHomePage.Models {
    export class SkillRate {
        skillRate: KnockoutComputed<string>;
        constructor(index: number, totalRate: number) {
            this.skillRate = ko.pureComputed(() => {
                return index < totalRate ? "skill-rate-on" : "skill-rate-off";
            });
        }
    } 
}