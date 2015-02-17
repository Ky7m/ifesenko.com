/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/jquery/jquerypluginsregister.d.ts" />
/// <reference path="../typings/bootstrap/bootstrap.d.ts" />
var PersonalHomePage;
(function (PersonalHomePage) {
    var Shell;
    (function (Shell) {
        var SkillItem = PersonalHomePage.Models.SkillItem;
        var Certification = PersonalHomePage.Models.Certification;
        var HomeViewModel = PersonalHomePage.ViewModels.HomeViewModel;
        var Preloader = PersonalHomePage.Helpers.Preloader;
        new Preloader("#status", "#preloader").attach(window);
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
            WebFont.load({
                google: {
                    families: ["Montserrat:400,700", "Raleway:300,400,700"]
                },
                timeout: 3000
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
            // Count to
            new Waypoint({
                element: document.getElementById('stats'),
                handler: function (direction) {
                    $(".timer").each(function () {
                        var counter = $(this).attr("data-count");
                        $(this).delay(6000).countTo({
                            from: 0,
                            to: counter,
                            speed: 3000,
                            refreshInterval: 50
                        });
                    });
                },
                offset: "70%",
                triggerOnce: true
            });
            // WOW Animation When You Scroll
            new WOW({
                mobile: false
            }).init();
            var skillItems = [];
            skillItems.push(new SkillItem("Web Applications and Sites", 4));
            skillItems.push(new SkillItem("Web Services and SOA", 4));
            skillItems.push(new SkillItem("Security and Identity", 4));
            skillItems.push(new SkillItem("Data Access, Integration, and Databases", 3));
            skillItems.push(new SkillItem("Desktop Applications", 3));
            skillItems.push(new SkillItem("Cloud Computing", 3));
            skillItems.push(new SkillItem("Mobile Client Applications", 3));
            skillItems.push(new SkillItem("Big Data", 2));
            var certifications = [];
            certifications.push(new Certification("content/images/mcsd_webapp.png", "MCSD: Web Applications"));
            certifications.push(new Certification("content/images/special_prog_csharp.png", "MS: Programming in C# Specialist"));
            certifications.push(new Certification("content/images/special_prog_html5.png", "MS: Programming in HTML5 with JavaScript and CSS3 Specialist"));
            var homeViewModel = new HomeViewModel(certifications, skillItems);
            ko.applyBindings(homeViewModel);
        });
    })(Shell = PersonalHomePage.Shell || (PersonalHomePage.Shell = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=shell.js.map