/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/jquery/jquerypluginsregister.d.ts" />
/// <reference path="../typings/bootstrap/bootstrap.d.ts" />

module MainModule {
    declare var WOW: any;
    declare var Waypoint: any;
    
    // Preloader
    $(window).load(() => {
        $("#status").fadeOut();
        $("#preloader").fadeOut("slow");
    });

    $(() => {
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
        var waypoint = new Waypoint({
            element: document.getElementById('stats'),
            handler: direction => {
                $(".timer").each(function() {
                    var counter = $(this).attr("data-count");
                    $(this).delay(6000).countTo({
                        from: 0,
                        to: counter,
                        speed: 3000, // Stats Counter Speed
                        refreshInterval: 50
                    });
                });
            },
            offset: '70%',
            triggerOnce: true
        });


        // WOW Animation When You Scroll
        new WOW({
            mobile: false
        }).init();

        ko.applyBindings();
    });
}
