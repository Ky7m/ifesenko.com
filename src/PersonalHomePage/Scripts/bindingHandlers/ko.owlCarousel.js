/// <reference path="../typings/knockout/bindinghandlersregister.d.ts" />
/// <reference path="../typings/jquery/jquerypluginsregister.d.ts" />
ko.bindingHandlers.owlCarousel = {
    init: function (element, valueAccessor) {
        var value = ko.unwrap(valueAccessor());
        $(element).owlCarousel(value);
    }
};
//# sourceMappingURL=ko.owlCarousel.js.map