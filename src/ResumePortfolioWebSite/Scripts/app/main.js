/// <reference path="../typings/jquery/jquery.d.ts" />
var Main;
(function (Main) {
    // Preloader
    $(window).load(function () {
        $('#status').fadeOut();
        $('#preloader').delay(250).fadeOut('slow');
    });
    $(document).ready(function () {
        $('body').scrollspy({
            target: '.navbar-custom',
            offset: 50
        });
        $(document).on('click', '.navbar-collapse.in', function (e) {
            if ($(e.target).is('a') && $(e.target).attr('class') !== 'dropdown-toggle') {
                $(this).collapse('hide');
            }
        });
        $('a[href*=#]').bind("click", function (e) {
            var anchor = $(this);
            $('html, body').stop().animate({
                scrollTop: $(anchor.attr('href')).offset().top
            }, 1000);
            e.preventDefault();
        });
        // Background image
        $('#intro').backstretch(['../Content/images/bg1.png']);
        // Navbar
        var navbar = $('.navbar');
        var navHeight = navbar.height();
        $(window).scroll(function () {
            if ($(this).scrollTop() >= navHeight) {
                navbar.addClass('navbar-color');
            }
            else {
                navbar.removeClass('navbar-color');
            }
        });
        if ($(window).width() <= 767) {
            navbar.addClass('custom-collapse');
        }
        $(window).resize(function () {
            if ($(this).width() <= 767) {
                navbar.addClass('custom-collapse');
            }
            else {
                navbar.removeClass('custom-collapse');
            }
        });
        // Count to
        $('#stats').waypoint(function () {
            $('.timer').each(function () {
                var counter = $(this).attr('data-count');
                $(this).delay(6000).countTo({
                    from: 0,
                    to: counter,
                    speed: 3000,
                    refreshInterval: 50
                });
            });
        }, { offset: '70%', triggerOnce: true });
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
})(Main || (Main = {}));
//# sourceMappingURL=main.js.map