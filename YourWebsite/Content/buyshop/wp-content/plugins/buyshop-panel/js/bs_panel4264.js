(function ($) {
	$(document).ready(function () {

		console.log(777);

		var themeDemos = $("#themeDemos");
		var demoView = $('.demoView');
		var demoViews = $('.demoViews');

		demoViews.css({height: themeDemos.height() - 53});
		themeDemos.css({marginBottom: -themeDemos.height() + 50});
		themeDemos.find ('a').click(function() {
			if (themeDemos.hasClass('open-panel')){themeDemos.removeClass('open-panel'); themeDemos.animate({marginBottom: -themeDemos.height() + 50})}
			else {themeDemos.addClass('open-panel'); themeDemos.animate({marginBottom: 0});}
		});
		themeDemos.find('.item:not(".comingsoon")').mouseenter(function () {
			demoView.stop(true, true).show(0);
			$('.demoView img').attr('src', $(this).attr("data-img"));
			$('.demoView .title').html($(this).attr("data-title"));
		});

		themeDemos.find('.item:not(".comingsoon")').mouseleave(function () {
			demoView.stop(true, true).hide(0)
		});

		$(window).resize(function () {
			demoViews.css({height: themeDemos.height() - 53});
			if (!themeDemos.hasClass('open-panel')){themeDemos.animate({marginBottom: -themeDemos.height() + 50})}
			else {themeDemos.animate({marginBottom: 0});}
		})
	});

} (window.jQuery));