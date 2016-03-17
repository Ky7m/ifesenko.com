/// <reference path="../typings/main.d.ts" />
!function($: any){
  var defaults = {
        animation: "dissolve",
        separator: ",",
        speed: 2000
    };

    $.fx.step.textShadowBlur = (fx:any) => {
        $(fx.elem).prop("textShadowBlur", fx.now).css({textShadow: "0 0 " + Math.floor(fx.now) + "px black"});
    };

  $.fn.textrotator = function(options:any){
    var settings = $.extend({}, defaults, options);

    return this.each(function(){
      var el = $(this);
        var array: any[] = [];
      $.each(el.text().split(settings.separator), function(key:any, value:any) {
        array.push(value);
      });
      el.text(array[0]);

      // animation option
      var rotate = function() {
            el.animate({
              textShadowBlur:20,
              opacity: 0
            }, 500 , function() {
              var index = $.inArray(el.text(), array);
                if((index + 1) === array.length) index = -1;
                el.text(array[index + 1]).animate({
                textShadowBlur:0,
                opacity: 1
              }, 500 );
            });
      };
      setInterval(rotate, settings.speed);
    });
  };
}(jQuery);