var config = {
        reset: true,
        init: true
      };


window.scrollReveal = new scrollReveal(config);

$(".scroll li").click(function() {
  var elem = $(this),
      class_name = elem.attr("class"),
      scene_num = class_name.substr(5,1),
      target = $("#scene-" + scene_num);

  $('html, body').animate({ scrollTop: (target.offset().top - 22) }, 'fast');
});

var fix_offset = $("#scroll-links").offset().top;

var fix_to_left = function() {
    var y = $(window).scrollTop();
    if ($("#scroll-links").length > 0) {
        if( y > fix_offset ){
            $("#scroll-links").addClass("fixed-to-left");
        } else {
            $("#scroll-links").removeClass("fixed-to-left");
        }
    }
 };

var which_scene = function() {
  var y = $(window).scrollTop();
  for (var i = 1, len = $(".scene-holder").length; i <= len; i++) {
    var offset = $("#scene-"+ i).offset().top - ($(window).height() / 2);
    if( y > offset){
          $("#scroll-links li:nth-of-type("+ i +")").addClass("passed");
      } else {
          $("#scroll-links li:nth-of-type("+ i +")").removeClass("passed");
      }
  };
  $(".progress").css("height",((y + $(window).height()) / $("body").height() * 100) + "vh")
 };

 $(window).scroll(function(){
    fix_to_left();
   which_scene();
 });

$(".modal-link").click(function() {
  var elem = $(this),
      who = elem.attr("id");
  $(".modal").addClass("show");
  $(".hide").hide();
  $(".who-"+ who).show();
  $(".close, .overlay").click(function() {
    $(".modal").removeClass("show");
  });
});