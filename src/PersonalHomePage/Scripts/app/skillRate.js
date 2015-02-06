var SkillRate = (function () {
    function SkillRate(index, totalRate) {
        this.skillRate = ko.pureComputed(function () {
            return index < totalRate ? "skill-rate-on" : "skill-rate-off";
        });
    }
    return SkillRate;
})();
//# sourceMappingURL=skillRate.js.map