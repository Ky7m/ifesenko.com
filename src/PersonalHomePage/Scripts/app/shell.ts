﻿module PersonalHomePage.Shell {
    import SkillItem = Models.SkillItem;
    import SocialProfile = Models.SocialProfile;
    import HomeViewModel = ViewModels.HomeViewModel;
    import ContactViewModel = ViewModels.ContactViewModel;
    import ContentItem = Base.ContentItem;
    declare var WOW: any;


    var bgContentItem = new ContentItem("content/images/background.jpg");
    $("#intro").backstretch([bgContentItem.Uri]);

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


        var skillItems: Array<SkillItem> = [
            new SkillItem("Web Applications and Sites", 4),
            new SkillItem("Web Services and SOA", 4),
            new SkillItem("Security and Identity", 4),
            new SkillItem("Cloud Computing", 4),
            new SkillItem("Data Access, Integration, and Databases", 3),
            new SkillItem("Desktop Applications", 3),
            new SkillItem("Big Data", 3),
            new SkillItem("Mobile Client Applications", 2)
        ];

        var certifications: Array<string> = [
            "MCSD: Web Applications",
            "MS: Programming in C# Specialist",
            "MS: Programming in HTML5 with JavaScript and CSS3 Specialist",
            "MS: Delivering Continuous Value with Visual Studio Application Lifecycle Management"
        ];

        var socialProfiles: Array<SocialProfile> = [
            new SocialProfile("http://ifesenko.com/go/github", "fa-github-alt"),
            new SocialProfile("http://ifesenko.com/go/linkedin", "fa-linkedin"),
            new SocialProfile("http://ifesenko.com/go/twitter", "fa-twitter")
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
}