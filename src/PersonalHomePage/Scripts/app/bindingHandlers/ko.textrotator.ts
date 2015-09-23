ko.bindingHandlers["textrotator"] = {
    init(element, valueAccessor) {
        const value = ko.unwrap(valueAccessor());
        $(element).textrotator(value);
    }
};