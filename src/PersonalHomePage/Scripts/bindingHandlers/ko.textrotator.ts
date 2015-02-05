/// <reference path="../typings/knockout/bindinghandlersregister.d.ts" />
/// <reference path="../typings/jquery/jquerypluginsregister.d.ts" />

ko.bindingHandlers.textrotator = {
    init(element, valueAccessor) {
        var value = ko.unwrap(valueAccessor()); 
        $(element).textrotator(value);
    }
};