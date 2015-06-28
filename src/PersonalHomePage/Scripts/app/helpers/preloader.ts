/// <reference path="../../typings/jquery/jquery.d.ts" />
module PersonalHomePage.Helpers {
    export class Preloader {
        private imageElementSelector:string;
        private statusElementSelector:string;

        constructor(statusElementSelector: string, imageElementSelector: string) {
            this.imageElementSelector = imageElementSelector;
            this.statusElementSelector = statusElementSelector;
        }

        attach(window: Window) {
            $(window).load(() => {
                $(this.statusElementSelector).fadeOut();
                $(this.imageElementSelector).fadeOut("slow");
            });
        }
    }
} 