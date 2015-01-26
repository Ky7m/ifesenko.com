/// <reference path="../typings/jquery/jquery.d.ts" />

module Main {
    declare var WOW: any;
    declare var $: any;
    /* ---------------------------------------------- /*
     * Preloader
    /* ---------------------------------------------- */

    $(window).load(() => {
        $('#status').fadeOut();
        $('#preloader').delay(1000).fadeOut('slow');
    });

    $(document).ready(() => {
        console.log('loaded');
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

        /* ---------------------------------------------- /*
         * Background image
        /* ---------------------------------------------- */

        $('#intro').backstretch(['content/images/bg1.png']);

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

        $('#stats').waypoint(() => {
            $('.timer').each(function () {
                var counter = $(this).attr('data-count');
                $(this).delay(6000).countTo({
                    from: 0,
                    to: counter,
                    speed: 3000,// Stats Counter Speed
                    refreshInterval: 50
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
                tError: 'The image could not be loaded.'
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

        $("#contact-form").submit(e => {

            e.preventDefault();

            var name = $("#c_name").val();
            var email = $("#c_email").val();
            var message = $("#c_message ").val();
            var responseMessage = $('.ajax-response');

            if ((name === "" || email === "" || message === "")) {
                responseMessage.fadeIn(500);
                responseMessage.html('<i class="fa fa-warning"></i> Check all fields.');
            }

            else {
                $.ajax({
                    type: "POST",
                    url: "assets/php/contactForm.php",
                    dataType: 'json',
                    data: {
                        c_email: email,
                        c_name: name,
                        c_message: message
                    },
                    beforeSend(result) {
                        $('#contact-form button').empty();
                        $('#contact-form button').append('<i class="fa fa-cog fa-spin"></i> Wait...');
                    },
                    success(result) {
                        if (result.sendstatus === 1) {
                            responseMessage.html(result.message);
                            responseMessage.fadeIn(500);
                            $('#contact-form').fadeOut(500);
                        } else {
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
}
