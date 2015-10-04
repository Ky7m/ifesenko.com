module PersonalHomePage.Shell {
    declare var WOW: any;

    $("#intro").backstretch("/Content/Images/Background.jpg");

    $(window).load(() => {
        $("#loader").fadeOut("slow");
    });

    $(() => {
        $(document).on("click", ".navbar-collapse.in", function (e) {
            if ($(e.target).is("a") && $(e.target).attr("class") !== "dropdown-toggle") {
                $(this).collapse("hide");
            }
        });

        $("a[href*=#]").bind("click", function (e) {
            const anchor = $(this);
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
            const state = $(this).scrollTop() >= navHeight;
            navbar.toggleClass("navbar-color", state);
        });

        $(window).resize(function () {
            const state = $(this).width() <= 767;
            navbar.toggleClass("custom-collapse", state);
        });

        // WOW Animation When You Scroll
        new WOW({
            mobile: false
        }).init();

        // tooltips
        $("[data-toggle='tooltip']").tooltip();

        ko.applyBindings(new ViewModels.HomeViewModel());
    });
}