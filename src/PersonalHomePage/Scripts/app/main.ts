﻿/// <reference path="../typings/jquery/jquery.d.ts" />

module MainModule {
    declare var WOW: any;
    declare var $: any;
    
    // Preloader
    $(window).load(() => {
        $("#status").fadeOut();
        $("#preloader").fadeOut("slow");
    });

    $(() => {
        $("body").scrollspy({
            target: ".navbar-custom",
            offset: 50
        });

        $(document).on("click", ".navbar-collapse.in", function (e) {
            if ($(e.target).is("a") && $(e.target).attr("class") !== "dropdown-toggle") {
                $(this).collapse("hide");
            }
        });

        $("a[href*=#]").bind("click", function (e) {
            var anchor = $(this);
            $("html, body").stop().animate({
                scrollTop: $(anchor.attr("href")).offset().top
            }, 1000);
            e.preventDefault();
        });
        
        // Background image
        var $intro = $("#intro");
        if ($intro) {
            var imgUrl = $intro.attr("data-backstretch-image");
            $intro.backstretch([imgUrl]);
           /* $.ajax({
                url: imgUrl,
                type: "HEAD"
            }).fail(() => {
                imgUrl = $intro.attr("data-fallback-image");
            }).always(() => {
                 $intro.backstretch([imgUrl]);
            });*/
        } 
        
        // Navbar
        var navbar = $(".navbar");
        var navHeight = navbar.height();

        $(window).scroll(function () {
            if ($(this).scrollTop() >= navHeight) {
                navbar.addClass("navbar-color");
            }
            else {
                navbar.removeClass("navbar-color");
            }
        });

        if ($(window).width() <= 767) {
            navbar.addClass("custom-collapse");
        }

        $(window).resize(function () {
            if ($(this).width() <= 767) {
                navbar.addClass("custom-collapse");
            }
            else {
                navbar.removeClass("custom-collapse");
            }
        });

        // Count to
        $("#stats").waypoint(() => {
            $(".timer").each(function () {
                var counter = $(this).attr("data-count");
                $(this).delay(6000).countTo({
                    from: 0,
                    to: counter,
                    speed: 3000,// Stats Counter Speed
                    refreshInterval: 50
                });
            });
        }, { offset: "70%", triggerOnce: true });

        // WOW Animation When You Scroll
        var wow = new WOW({
            mobile: false
        });
        wow.init();

        // Owl slider
        $("#owl-certifications").owlCarousel({
            items: 4,
            slideSpeed: 300,
            paginationSpeed: 400,
            autoPlay: 5000
        });

        // Rotate
        $(".rotate").textrotator({
            animation: "dissolve",
            separator: "|",
            speed: 3000
        });
    });
}
