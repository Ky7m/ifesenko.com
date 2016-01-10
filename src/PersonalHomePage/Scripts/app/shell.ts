module PersonalHomePage.Shell {
    $("#intro").backstretch("//ifesenko.azureedge.net/Content/images/background.jpg");

    $(window).load(() => {
        $("#loader").fadeOut("slow");
    });

    $(() => {
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

        ko.applyBindings(new ViewModels.ContactViewModel());
    });
}