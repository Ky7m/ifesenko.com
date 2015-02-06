var SkillItem = (function () {
    function SkillItem(title, rate) {
        this.title = title;
        this.skillRates = [];
        for (var i = 0; i < 4; i++) {
            this.skillRates.push(new SkillRate(i, rate));
        }
    }
    return SkillItem;
})();
//# sourceMappingURL=skillItem.js.map