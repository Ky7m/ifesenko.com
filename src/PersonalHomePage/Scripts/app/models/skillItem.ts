module PersonalHomePage.Models {
    export class SkillItem {
        skillRates: Array<SkillRate> = [];
        constructor(public title: string, rate: number) {
            for (let i = 0; i < 4; i++) {
                this.skillRates.push(new SkillRate(i, rate));
            }
        }
    }
}