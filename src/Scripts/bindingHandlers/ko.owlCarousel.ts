ko.bindingHandlers["owlCarousel"] = {
    init(element, valueAccessor) {
        var value = ko.unwrap(valueAccessor());
        $(element).owlCarousel(value);
    }
};