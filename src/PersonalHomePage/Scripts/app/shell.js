var PersonalHomePage;
(function (PersonalHomePage) {
    var Shell;
    (function (Shell) {
        var SkillItem = PersonalHomePage.Models.SkillItem;
        var Certification = PersonalHomePage.Models.Certification;
        var SocialProfile = PersonalHomePage.Models.SocialProfile;
        var HomeViewModel = PersonalHomePage.ViewModels.HomeViewModel;
        var Preloader = PersonalHomePage.Helpers.Preloader;
        var ContactViewModel = PersonalHomePage.ViewModels.ContactViewModel;
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
            var skillItems = [
                new SkillItem("Web Applications and Sites", 4),
                new SkillItem("Web Services and SOA", 4),
                new SkillItem("Security and Identity", 4),
                new SkillItem("Data Access, Integration, and Databases", 3),
                new SkillItem("Desktop Applications", 3),
                new SkillItem("Cloud Computing", 3),
                new SkillItem("Mobile Client Applications", 3),
                new SkillItem("Big Data", 2)
            ];
            var certifications = [
                new Certification("data:image/png;base64,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", "MCSD: Web Applications"),
                new Certification("data:image/png;base64,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", "MS: Programming in C# Specialist"),
                new Certification("data:image/png;base64,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", "MS: Programming in HTML5 with JavaScript and CSS3 Specialist")
            ];
            var socialProfiles = [
                new SocialProfile("https://github.com/Ky7m", "fa-github-alt"),
                new SocialProfile("https://bitbucket.org/Ky7m", "fa-bitbucket"),
                new SocialProfile("https://www.linkedin.com/profile/view?id=182744142", "fa-linkedin"),
                new SocialProfile("https://twitter.com/ky7m", "fa-twitter"),
                new SocialProfile("https://tech.pro/igorfesenko", "fa-user-md")
            ];
            var contactViewModel = new ContactViewModel();
            var homeViewModel = new HomeViewModel(certifications, skillItems, socialProfiles, contactViewModel);
            ko.validation.init({
                /*
                parseInputAttributes is required for html5 attributes to work
                */
                parseInputAttributes: true,
                /*
                decorateElement: true allows knockout.validation to add
                or remove errorElementClass from input elements. This is
                also required for validationElement binding to work.
                ValidationElement binding is required for decorating
                Bootstrap's control-groups with 'error' class.
                */
                decorateElement: true,
                /*
                Bootstrap uses 'error' class annotate invalid fields.
                */
                errorElementClass: 'has-error'
            });
            // setup toastr options
            toastr.options.closeButton = true;
            toastr.options.newestOnTop = true;
            toastr.options.progressBar = true;
            toastr.options.positionClass = "toast-top-center";
            ko.applyBindings(homeViewModel);
        });
    })(Shell = PersonalHomePage.Shell || (PersonalHomePage.Shell = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=shell.js.map