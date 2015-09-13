ko.bindingHandlers["textrotator"] = {
    init(element, valueAccessor) {
        var value = ko.unwrap(valueAccessor()); 
        $(element).textrotator(value);
    }
};