﻿interface JQuery {
    textrotator(options?: any): JQuery;
    backstretch(options?: any): JQuery;
}

declare var textrotator: JQueryStatic;
declare var backstretch: JQueryStatic;

module ifesenko.com.Shell {
    $("#home").backstretch($("#home").data("image-source"));

    $(window).on('load', function() {
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

        $("#textrotator").textrotator({ separator: '|', speed: 3000 });
    });
}