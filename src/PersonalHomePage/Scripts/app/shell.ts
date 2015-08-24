/// <reference path="models/skillitem.ts" />
/// <reference path="models/certification.ts" />
/// <reference path="models/socialprofile.ts" />
/// <reference path="base/contentitem.ts" />
/// <reference path="viewmodels/contactviewmodel.ts" />
/// <reference path="viewmodels/homeviewmodel.ts" />
/// <reference path="../typings/jquery/jquerypluginsregister.d.ts" />
/// <reference path="../typings/bootstrap/bootstrap.d.ts" />
/// <reference path="helpers/preloader.ts" />
module PersonalHomePage.Shell {
    import SkillItem = Models.SkillItem;
    import Certification = Models.Certification;
    import SocialProfile = Models.SocialProfile;
    import HomeViewModel = ViewModels.HomeViewModel;
    import Preloader = Helpers.Preloader;
    import ContactViewModel = ViewModels.ContactViewModel;
    import ContentItem = Base.ContentItem;
    declare var WOW: any;

    new Preloader("#status", "#preloader").attach(window);
    var bgContentItem = new ContentItem("content/images/background.jpg");
    $("#intro").backstretch([bgContentItem.Uri]);

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
            new SkillItem("Data Access, Integration, and Databases", 3),
            new SkillItem("Desktop Applications", 3),
            new SkillItem("Cloud Computing", 3),
            new SkillItem("Mobile Client Applications", 3),
            new SkillItem("Big Data", 2)
        ];

        var certifications: Array<Certification> = [
            new Certification("MCSD: Web Applications"),
            new Certification("MS: Programming in C# Specialist"),
            new Certification("MS: Programming in HTML5 with JavaScript and CSS3 Specialist"),
            new Certification("MS: Delivering Continuous Value with Visual Studio Application Lifecycle Management")
        ];

        var socialProfiles: Array<SocialProfile> = [
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
}