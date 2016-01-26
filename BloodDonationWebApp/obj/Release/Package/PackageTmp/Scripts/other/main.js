(function($) {
	'use strict';

	var $scrollButton = $('#scroll-top');
	$(document).on('scroll', function(event) {
		if ($(this).scrollTop() > 500) {
		    $scrollButton.addClass('show-me');
		} else {
		    $scrollButton.removeClass('show-me');
		}
	});

	$('.open-side-widgets, .close-canvas-widgets').on('click', function(event) {
		event.preventDefault();
		$('body').toggleClass('show-canvas-widgets');
	});

	$('.has-submenu').on('click', '> a', function(event) {
		event.preventDefault();
		$(this).next('ul').slideToggle('fast');
	});

	$('.open-nav, .close-canvas').on('click', function(event) {
		event.preventDefault();
		$('.offset-canvas-mobile').toggleClass('open-canvas');
	});

	$scrollButton.on('click', function(event) {
		event.preventDefault();
		$("html, body").animate({ scrollTop: 0 }, 600);
	});

	var $clients = $('.clients-slides');
	if ($clients.length > 0) {
		$clients.slick({
			infinite       : true,
			slidesToShow   : 5,
			slidesToScroll : 3,
			arrows         : false,
			responsive: [
			    {
			      breakpoint: 1199,
			      settings: {
			        slidesToShow: 3
			      }
			    },
			    {
			      breakpoint: 500,
			      settings: {
			        slidesToShow: 1,
			        dots: true
			      }
			    }
			]
		});
	}

	var $foodies = $('.foodie-slider');
	if ($foodies.length > 0) {
		$foodies.slick({
			infinite       : true,
			slidesToShow   : 4,
			slidesToScroll : 1,
			arrows         : false,
			responsive: [
			    {
			      breakpoint: 1199,
			      settings: {
			        slidesToShow: 3
			      }
			    },
			    {
			      breakpoint: 767,
			      settings: {
			        slidesToShow: 1,
			        dots: false
			      }
			    }
			]
		});
	}

	$foodies.on('afterChange', function(event, slick, currentSlide){
	  $('.dishes-count span i:first-child').html('0' + (currentSlide + 1));
	});

	$('.dishes-left').on('click', function(event) {
		event.preventDefault();
		$foodies.slick('slickPrev');
	});
	$('.dishes-right').on('click', function(event) {
		event.preventDefault();
		$foodies.slick('slickNext');
	});

	var $searchPanel = $('.search-panel'),
		$searchInput = $('#searchy-term');
	$('.search-pan-open').on('click', function(event) {
		event.preventDefault();
		$searchPanel.addClass('show-pan');
		setTimeout(function() {
			$searchInput.focus();
		}, 200);
	});

		var allPanels = $('.ilny-accordion > dd:not(:first-of-type)').hide(),
			panelsAll = $('.ilny-accordion > dd');
		$('.ilny-accordion > dt > a').on('click', function(event) {
			event.preventDefault();
			panelsAll.slideUp();
			$('.ilny-accordion').find('a').removeClass('active-accord');
			$(this).addClass('active-accord');
			$(this).parent().next().slideDown();
		});

	$(document).on('keyup', function(e) {
		if (e.keyCode == 27) {
			$searchPanel.removeClass('show-pan');
			$searchInput.val('');
		}
	});

	$('.close-panel').on('click', function(event) {
		event.preventDefault();
		$searchPanel.removeClass('show-pan');
		$searchInput.val('');
	});

	var $counter = $('.count-num');
	if ($counter.length > 0) {
		$counter.waypoint(function() {
			$counter.countTo();
			this.destroy();
		}, {offset: '80%'});
	}

	$( '.swipebox' ).swipebox();

	$(document).on('scroll', function(event) {
		var scrollingPos = $(this).scrollTop(),
			$headerWrapp = $('.header-wrapper');

		if(!$headerWrapp.hasClass('contact-form'))
		{
			if (scrollingPos > 450) {
				$headerWrapp.addClass('fixed-nav');
			} else {
				$headerWrapp.removeClass('fixed-nav');
			}
		}
	});

	var $screenSlider     = $('.screen-slides'),
		$testimonials     = $('.testimonials-slide'),
		$twoHalfSlider    = $('.two-half-slider');
	if ($screenSlider.length > 0 || $testimonials.length > 0) {
		$screenSlider.slick({
			infinite       : true,
			slidesToShow   : 1,
			slidesToScroll : 1
		});
		$testimonials.slick({
			infinite       : true,
			slidesToShow   : 1,
			slidesToScroll : 1,
			autoplay       : true,
			autoplaySpeed  : 3000,
			arrows         : false,
			dots           : true
		});
	}
	$screenSlider.on('afterChange', function(event, slick, currentSlide){
	  $('.count-holder span:first-child').html('0' + (currentSlide + 1));
	});

	if ($twoHalfSlider.length > 0) {
		$twoHalfSlider.slick({
			infinite       : true,
			slidesToShow   : 2,
			slidesToScroll : 2,
			dots           : true,
			arrows         : false,
			adaptiveHeight : true,
			responsive: [
			    {
			      breakpoint: 991,
			      settings: {
			        slidesToShow: 1,
			        slidesToScroll : 1,
			      }
			    }
			]
		});
	}

	$(document).ready(function() {

		$('.svg-mockup').waypoint(function() {
			new Vivus('ilny-svg-mockup', {type: 'async', duration: 50}, function() {
				$('.svg-mockup').addClass('svg-animation-done');
			});
		}, {offset: '80%'});

		$('video,audio').mediaelementplayer(/* Options */);

		// $('.tweet-feed').twittie({
		// 	username   : 'avathemes',
		// 	count      : 1,
		// 	hideReplies : true,
		// 	dateFormat : '%d.%b.%Y',
		// 	template   : '<a href="{{url}}" target="_blank" title="Tweet by {{screen_name}}">{{tweet}}</a><span>{{date}}</span>'
		// });
	});


	$(window).load(function() {

		if (!Modernizr.touch) {
			var $progressBars = $('.progress-bar');
				$progressBars.css('width', 0);

			$progressBars.waypoint(function() {
				var $this = $(this.element),
					progressWidth = $this.attr('aria-valuenow');

				$this.animate({width: progressWidth + '%'}, 300);

				this.destroy();
			}, {offset: '80%'});

			var $wayElements = $('.animate-el');
			$wayElements.css('opacity', 0);

			$wayElements.waypoint(function() {
				var $this = $(this.element),
					timing = $this.data('timing'),
					animationType = $this.data('animation-type');
				setTimeout(function() {
					$this.addClass('animated ' + animationType);
				}, timing);

				this.destroy();
			},{offset: '80%'});
		}


		$('.ilny-loader').fadeOut('fast', function() {
			$(this).remove();
		});

		$('.match-me').matchHeight();

		var numOfSlides = $('.screen-slides .slick-slide:not(.slick-cloned)').length;
		$('.count-holder span:last-child').html('/ 0' + numOfSlides);

		var numOfDishes = $('.foodie-slider .slick-slide:not(.slick-cloned)').length;
		$('.dishes-count span i:last-child').html('0' + numOfDishes);

        var fixHeight = $('.fullscreen-wrapper, .header-page-wrapp').outerHeight();
		$('.screen-fix').css('height', fixHeight);

		setTimeout(function() {
			if (!Modernizr.touch) {
				$.stellar();
			}
		}, 500);

		var $mkpHolder     = $('.mockup-holder'),
			extractDiv     = $mkpHolder.find('> div').height(),
			$imgElement    = $mkpHolder.find(' div img'),
			imgHeight      = $imgElement.height();

		$mkpHolder.on({
		    mouseenter: function () {
		        $imgElement.velocity("stop").velocity({
		        	top: -imgHeight + extractDiv
		        }, 3500);
		    },
		    mouseleave: function () {
		        $imgElement.velocity("stop").velocity({
		        	top: 0
		        }, 500);
		    }
		});

	});

})(jQuery);