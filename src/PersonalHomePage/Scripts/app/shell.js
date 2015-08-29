var PersonalHomePage;
(function (PersonalHomePage) {
    var Shell;
    (function (Shell) {
        var SkillItem = PersonalHomePage.Models.SkillItem;
        var SocialProfile = PersonalHomePage.Models.SocialProfile;
        var HomeViewModel = PersonalHomePage.ViewModels.HomeViewModel;
        var ContactViewModel = PersonalHomePage.ViewModels.ContactViewModel;
        var ContentItem = PersonalHomePage.Base.ContentItem;
        var bgContentItem = new ContentItem("content/images/background.jpg");
        $("#intro").backstretch([bgContentItem.Uri]);
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
            var skillItems = [
                new SkillItem("Web Applications and Sites", 4),
                new SkillItem("Web Services and SOA", 4),
                new SkillItem("Security and Identity", 4),
                new SkillItem("Cloud Computing", 4),
                new SkillItem("Data Access, Integration, and Databases", 3),
                new SkillItem("Desktop Applications", 3),
                new SkillItem("Big Data", 3),
                new SkillItem("Mobile Client Applications", 2)
            ];
            var certifications = [
                "MCSD: Web Applications",
                "MS: Programming in C# Specialist",
                "MS: Programming in HTML5 with JavaScript and CSS3 Specialist",
                "MS: Delivering Continuous Value with Visual Studio Application Lifecycle Management"
            ];
            var socialProfiles = [
                new SocialProfile("https://github.com/Ky7m", "fa-github-alt"),
                new SocialProfile("https://www.linkedin.com/profile/view?id=182744142", "fa-linkedin"),
                new SocialProfile("https://twitter.com/ky7m", "fa-twitter")
            ];
            var contactViewModel = new ContactViewModel();
            var profileContentItem = new ContentItem("content/images/profile.jpg");
            var homeViewModel = new HomeViewModel(profileContentItem.Uri, certifications, skillItems, socialProfiles, contactViewModel);
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
            // tooltips
            $("[data-toggle='tooltip']").tooltip();
            ko.applyBindings(homeViewModel);
        });
    })(Shell = PersonalHomePage.Shell || (PersonalHomePage.Shell = {}));
})(PersonalHomePage || (PersonalHomePage = {}));
//# sourceMappingURL=shell.js.map