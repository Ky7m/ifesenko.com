module PersonalHomePage.Shell {
    import SkillItem = Models.SkillItem;
    import Certification = Models.Certification;
    import SocialProfile = Models.SocialProfile;
    import HomeViewModel = ViewModels.HomeViewModel;
    import Preloader = Helpers.Preloader;
    import ContactViewModel = ViewModels.ContactViewModel;

    declare var WOW: any;
    declare var WebFont: any;
    declare var Waypoint: any;

    new Preloader("#status", "#preloader").attach(window);

    $(() => {
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
            navbar.toggleClass("navbar-color",state);
        });

        $(window).resize(function() {
            var state = $(this).width() <= 767;
            navbar.toggleClass("custom-collapse", state);
        });

        // Count to
        new Waypoint({
            element: document.getElementById('stats'),
            handler: direction => {
                $(".timer").each(function() {
                    var counter = $(this).attr("data-count");
                    $(this).delay(6000).countTo({
                        from: 0,
                        to: counter,
                        speed: 3000, // Stats Counter Speed
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


        var skillItems: Array<SkillItem> = [
            new SkillItem("Web Applications and Sites", 4),
            new SkillItem("Web Services and SOA", 4),
            new SkillItem("Security and Identity", 4),
            new SkillItem("Data Access, Integration, and Databases", 3),
            new SkillItem("Desktop Applications", 3),
            new SkillItem("Cloud Computing", 3),
            new SkillItem("Mobile Client Applications", 3),
            new SkillItem("Big Data", 2)
        ];

        var certifications: Array<Certification> = [
            new Certification("content/images/mcsd_webapp.png", "MCSD: Web Applications"),
            new Certification("content/images/special_prog_csharp.png", "MS: Programming in C# Specialist"),
            new Certification("content/images/special_prog_html5.png", "MS: Programming in HTML5 with JavaScript and CSS3 Specialist")
        ];

        var socialProfiles: Array<SocialProfile> = [
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
}