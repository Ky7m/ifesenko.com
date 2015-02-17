/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/jquery/jquerypluginsregister.d.ts" />
/// <reference path="../typings/bootstrap/bootstrap.d.ts" />
module PersonalHomePage.Shell {
    import SkillItem = Models.SkillItem;
    import Certification = Models.Certification;
    import HomeViewModel = ViewModels.HomeViewModel;
    import Preloader = Helpers.Preloader;

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
               
		WebFont.load({
			google: {
				families: ["Montserrat:400,700", "Raleway:300,400,700"]
			},
			timeout: 3000
		});

        // Navbar
        var navbar = $(".navbar");
        var navHeight = navbar.height();

        $(window).scroll(function () {
            if ($(this).scrollTop() >= navHeight) {
                navbar.addClass("navbar-color");
            }
            else {
                navbar.removeClass("navbar-color");
            }
        });

        if ($(window).width() <= 767) {
            navbar.addClass("custom-collapse");
        }

        $(window).resize(function () {
            if ($(this).width() <= 767) {
                navbar.addClass("custom-collapse");
            }
            else {
                navbar.removeClass("custom-collapse");
            }
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


        var skillItems: Array<SkillItem> = [];
        skillItems.push(new SkillItem("Web Applications and Sites", 4));
        skillItems.push(new SkillItem("Web Services and SOA", 4));
        skillItems.push(new SkillItem("Security and Identity", 4));
        skillItems.push(new SkillItem("Data Access, Integration, and Databases", 3));
        skillItems.push(new SkillItem("Desktop Applications", 3));
        skillItems.push(new SkillItem("Cloud Computing", 3));
        skillItems.push(new SkillItem("Mobile Client Applications", 3));
        skillItems.push(new SkillItem("Big Data", 2));

        var certifications: Array<Certification> = [];
        certifications.push(new Certification("content/images/mcsd.png", "MCSD: Web Applications"));
        certifications.push(new Certification("content/images/ms_csharp.png", "MS: Programming in C# Specialist"));
        certifications.push(new Certification("content/images/ms_web.png", "MS: Programming in HTML5 with JavaScript and CSS3 Specialist"));

        var homeViewModel = new HomeViewModel(certifications, skillItems);

        ko.applyBindings(homeViewModel);
    });
}