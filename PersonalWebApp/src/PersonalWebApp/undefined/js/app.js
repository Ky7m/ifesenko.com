/// <reference path="../typings/index.d.ts" />
!function ($) {
    var defaults = {
        animation: "dissolve",
        separator: ",",
        speed: 2000
    };
    $.fx.step.textShadowBlur = function (fx) {
        $(fx.elem).prop("textShadowBlur", fx.now).css({ textShadow: "0 0 " + Math.floor(fx.now) + "px black" });
    };
    $.fn.textrotator = function (options) {
        var settings = $.extend({}, defaults, options);
        return this.each(function () {
            var el = $(this);
            var array = [];
            $.each(el.text().split(settings.separator), function (key, value) {
                array.push(value);
            });
            el.text(array[0]);
            // animation option
            var rotate = function () {
                el.animate({
                    textShadowBlur: 20,
                    opacity: 0
                }, 500, function () {
                    var index = $.inArray(el.text(), array);
                    if ((index + 1) === array.length)
                        index = -1;
                    el.text(array[index + 1]).animate({
                        textShadowBlur: 0,
                        opacity: 1
                    }, 500);
                });
            };
            setInterval(rotate, settings.speed);
        });
    };
}(jQuery);

/// <reference path="../typings/index.d.ts" />
var ifesenko;
(function (ifesenko) {
    var com;
    (function (com) {
        var Shell;
        (function (Shell) {
            $("#home").backstretch("//ifesenko.azureedge.net/Content/images/background.jpg");
            $(window).load(function () {
                $("#loader").fadeOut("slow");
            });
            $(function () {
                $(document).on("click", ".navbar-collapse.in", function (e) {
                    if ($(e.target).is("a") && $(e.target).attr("class") !== "dropdown-toggle") {
                        $(this).collapse("hide");
                    }
                });
                $("a[href*='#']").bind("click", function (e) {
                    var anchor = $(this);
                    $("html, body").stop().animate({
                        scrollTop: $(anchor.attr("href")).offset().top
                    }, 1000);
                    e.preventDefault();
                });
                // Navbar
                var navbar = $(".navbar");
                var navHeight = navbar.height();
                if ($(window).width() <= 767) {
                    navbar.addClass("custom-collapse");
                }
                $(window).scroll(function () {
                    var state = $(this).scrollTop() >= navHeight;
                    navbar.toggleClass("navbar-color", state);
                });
                $(window).resize(function () {
                    var state = $(this).width() <= 767;
                    navbar.toggleClass("custom-collapse", state);
                });
                // tooltips
                $("[data-toggle='tooltip']").tooltip();
                $("#textrotator").textrotator({ separator: '|', speed: 3000 });
            });
        })(Shell = com.Shell || (com.Shell = {}));
    })(com = ifesenko.com || (ifesenko.com = {}));
})(ifesenko || (ifesenko = {}));

//# sourceMappingURL=app.js.map
