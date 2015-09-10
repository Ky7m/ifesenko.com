var PersonalHomePage;
(function (PersonalHomePage) {
    var Shell;
    (function (Shell) {
        var bgContentItem = new PersonalHomePage.Models.ContentItem("content/images/background.jpg");
        $("#intro").backstretch([bgContentItem.uri]);
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
            var homeViewModel = new PersonalHomePage.ViewModels.HomeViewModel();
            ko.validation.init({
                parseInputAttributes: true,
                decorateElement: true,
                errorElementClass: "has-error"
            });
            // setup toastr options
            toastr.options.closeButton = true;
            toastr.options.newestOnTop = true;
            toastr.options.progressBar = true;
            toastr.options.positionClass = "toast-top-center";
            // tooltips
            $("[data-toggle='tooltip']").tooltip();
            ko.applyBindings(homeViewModel);
        });
    })(Shell = PersonalHomePage.Shell || (PersonalHomePage.Shell = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=shell.js.map