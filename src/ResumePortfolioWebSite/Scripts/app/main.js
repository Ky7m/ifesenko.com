/// <reference path="../typings/jquery/jquery.d.ts" />
var Main;
(function (Main) {
    /* ---------------------------------------------- /*
     * Preloader
    /* ---------------------------------------------- */
    $(window).load(function () {
        $('#status').fadeOut();
        $('#preloader').delay(350).fadeOut('slow');
    });
    $(document).ready(function () {
        console.log('loaded');
        $('body').scrollspy({
            target: '.navbar-custom',
            offset: 50
        });
        $(document).on('click', '.navbar-collapse.in', function (e) {
            if ($(e.target).is('a') && $(e.target).attr('class') != 'dropdown-toggle') {
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
        /* ---------------------------------------------- /*
         * Background image
        /* ---------------------------------------------- */
        $('#intro').backstretch(['assets/images/bg4.jpg']);
        /* ---------------------------------------------- /*
         * Navbar
        /* ---------------------------------------------- */
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
        /* ---------------------------------------------- /*
         * Count to
        /* ---------------------------------------------- */
        $('#stats').waypoint(function () {
            $('.timer').each(function () {
                var counter = $(this).attr('data-count');
                $(this).delay(6000).countTo({
                    from: 0,
                    to: counter,
                    speed: 3000,
                    refreshInterval: 50,
                });
            });
        }, { offset: '70%', triggerOnce: true });
        /* ---------------------------------------------- /*
         * WOW Animation When You Scroll
        /* ---------------------------------------------- */
        var wow = new WOW({
            mobile: false
        });
        wow.init();
        /* ---------------------------------------------- /*
         * Owl slider
        /* ---------------------------------------------- */
        $("#owl-clients").owlCarousel({
            items: 4,
            slideSpeed: 300,
            paginationSpeed: 400,
            autoPlay: 5000
        });
        /* ---------------------------------------------- /*
         * Rotate
        /* ---------------------------------------------- */
        $(".rotate").textrotator({
            animation: "dissolve",
            separator: "|",
            speed: 3000
        });
        /* ---------------------------------------------- /*
         * Portfolio pop up
        /* ---------------------------------------------- */
        $('#portfolio').magnificPopup({
            delegate: 'a.pop-up',
            type: 'image',
            gallery: {
                enabled: true,
                navigateByImgClick: true,
                preload: [0, 1]
            },
            image: {
                titleSrc: 'title',
                tError: 'The image could not be loaded.',
            }
        });
        $('.video-pop-up').magnificPopup({
            type: 'iframe'
        });
        /* ---------------------------------------------- /*
         * A jQuery plugin for fluid width video embeds
        /* ---------------------------------------------- */
        $(".video").fitVids();
        /* ---------------------------------------------- /*
         * Contact form ajax
        /* ---------------------------------------------- */
        $("#contact-form").submit(function (e) {
            e.preventDefault();
            var c_name = $("#c_name").val();
            var c_email = $("#c_email").val();
            var c_message = $("#c_message ").val();
            var responseMessage = $('.ajax-response');
            if ((c_name == "" || c_email == "" || c_message == "")) {
                responseMessage.fadeIn(500);
                responseMessage.html('<i class="fa fa-warning"></i> Check all fields.');
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "assets/php/contactForm.php",
                    dataType: 'json',
                    data: {
                        c_email: c_email,
                        c_name: c_name,
                        c_message: c_message
                    },
                    beforeSend: function (result) {
                        $('#contact-form button').empty();
                        $('#contact-form button').append('<i class="fa fa-cog fa-spin"></i> Wait...');
                    },
                    success: function (result) {
                        if (result.sendstatus == 1) {
                            responseMessage.html(result.message);
                            responseMessage.fadeIn(500);
                            $('#contact-form').fadeOut(500);
                        }
                        else {
                            $('#contact-form button').empty();
                            $('#contact-form button').append('<i class="fa fa-retweet"></i> Try again.');
                            responseMessage.html(result.message);
                            responseMessage.fadeIn(1000);
                        }
                    }
                });
            }
            return false;
        });
    });
})(Main || (Main = {}));
//# sourceMappingURL=main.js.map