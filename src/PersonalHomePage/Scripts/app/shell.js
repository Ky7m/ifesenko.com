var PersonalHomePage;
(function (PersonalHomePage) {
    var Shell;
    (function (Shell) {
        $("#intro").backstretch("//az717836.vo.msecnd.net/Content/images/background.jpg");
        $(window).load(function () {
            $("#loader").fadeOut("slow");
        });
        $(function () {
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
            // WOW Animation When You Scroll
            new WOW({
                mobile: false
            }).init();
            // tooltips
            $("[data-toggle='tooltip']").tooltip();
            ko.applyBindings(new PersonalHomePage.ViewModels.HomeViewModel());
        });
    })(Shell = PersonalHomePage.Shell || (PersonalHomePage.Shell = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=shell.js.map