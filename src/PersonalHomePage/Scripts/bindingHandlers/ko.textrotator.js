/// <reference path="../typings/knockout/bindinghandlersregister.d.ts" />
/// <reference path="../typings/jquery/jquerypluginsregister.d.ts" />
ko.bindingHandlers.textrotator = {
    init: function (element, valueAccessor) {
        var value = ko.unwrap(valueAccessor());
        $(element).textrotator(value);
    }
};
//# sourceMappingURL=ko.textrotator.js.map