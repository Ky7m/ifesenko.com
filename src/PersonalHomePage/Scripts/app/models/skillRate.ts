/// <reference path="../../typings/knockout/knockout.d.ts" />
module PersonalHomePage.Models {
    export class SkillRate {
        public skillRate: KnockoutComputed<string>;
        constructor(index: number, totalRate: number) {
            this.skillRate = ko.pureComputed(() => {
                return index < totalRate ? "skill-rate-on" : "skill-rate-off";
            });
        }
    } 
}