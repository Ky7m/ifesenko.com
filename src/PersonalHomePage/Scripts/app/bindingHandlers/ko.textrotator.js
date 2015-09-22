ko.bindingHandlers["textrotator"] = {
    init: function (element, valueAccessor) {
        var value = ko.unwrap(valueAccessor());
        $(element).textrotator(value);
    }
};
