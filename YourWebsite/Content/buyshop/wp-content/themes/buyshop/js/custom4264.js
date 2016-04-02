jQuery.noConflict();
jQuery(document).ready(function() {

	jQuery('#cuf_sender').attr('type', 'text');
	jQuery('#cuf_email').attr('type', 'email');
	jQuery('#cuf_subject').attr('type', 'text');
	jQuery('.contactform #contactsubmit').attr('value', 'Send Message');

	/*	NEW PAGES SCRIPT*/
	var PreviewSliderHeight = function() {

		var min_big_image_height= jQuery('.product-img-box .product-image').height();//jQuery('a.cloud-zoom img').height();


		//var preview_image_height= jQuery('div.more-views ul.slides li:first-child').height();
		var previews_height = 0;
		jQuery('div.more-views ul.slides li').each(function() {
			// add each image height + margin
			previews_height += jQuery(this).height() + 10;
		});

		// delete last margin
		previews_height -= 10;

		var slider_height = min_big_image_height + 10;

		jQuery(".flexslider.more-views .flex-viewport").css({
			"min-height": slider_height + "px"
		});

		if ( previews_height <= min_big_image_height ) {
			jQuery('.flexslider.more-views .flex-direction-nav').hide();
		}

	};

	jQuery('.flexslider.more-views').flexslider({
		animation: "slide",
		autoplay: false,
		minItems: 1,
		animationLoop: false,
		direction: "vertical",
		controlNav: false,
		slideshow: false,
		prevText: "<i class='icon-down'></i>",
		nextText: "<i class='icon-up'></i>",
		start: PreviewSliderHeight
	});

	// Collapsed menu
	jQuery('.collapsed-menu span.collapse_button').click(function () {
		if (!jQuery(this).parent().hasClass('active')) {
			jQuery(this).html('&#8211;');
			jQuery(this).addClass('collapsed');
			jQuery(this).parent().children('ul.children').show(300);
			jQuery(this).parent().addClass('active');
		} else {
			jQuery(this).html('+');
			jQuery(this).removeClass('collapsed');
			jQuery(this).parent().find('ul.children').hide(300);
			jQuery(this).parent().removeClass('active');
			jQuery(this).parent().find('li').removeClass('active');
			jQuery(this).parent().find('span.collapse_button').html('+');
		}
	});

	jQuery('#contentTab a').click(function (e) {
		e.preventDefault();
		jQuery(this).tab('show');
	})

	jQuery("table.striped tr:odd").addClass("odd");
	if (!jQuery("a[data-gal^='prettyPhoto']").length==0){
		jQuery("a[data-gal^='prettyPhoto']").prettyPhoto({theme: 'pp_neocart', counter_separator_label: ' of ', horizontal_padding: 10, hook: 'data-gal' });
	}

	jQuery('.flexslider.carousel-content').flexslider({
		animation: "slide",
		pauseOnHover: true,
		controlNav: false,
		animationSpeed: 300,
		prevText: "<i class='prev icon-left-thin'></i>",
		nextText: "<i class='next icon-right-thin'></i>"

	});

	jQuery('.carousel-testimonials .flexslider').flexslider({
		animation: "slide",
		pauseOnHover: true,
		controlNav: false,
		animationSpeed: 300,
		prevText: "<i class='prev icon-left-thin'></i>",
		nextText: "<i class='next icon-right-thin'></i>"

	});

	// 3D gallery

	if (jQuery("#hoverfold").length != 0) {

		//start the hoverfold plugin

		Modernizr.load({
			test: Modernizr.csstransforms3d && Modernizr.csstransitions,
			yep : hoverfold_js_url,
			nope: 'css/fallback.css',
			callback : function( url, result, key ) {

				if( url === hoverfold_js_url ) {
					jQuery( '#hoverfold div' ).hoverfold();
				}

			}
		});

		var $container = jQuery('#hoverfold');

		$container.isotope({
			itemSelector : '.span4'
		});


		var $optionSets = jQuery('#options .option-set'),
			$optionLinks = $optionSets.find('a');

		$optionLinks.click(function(){
			var $this = $(this);
			// don't proceed if already selected
			if ( $this.hasClass('selected') ) {
				return false;
			}
			var $optionSet = $this.parents('.option-set');
			$optionSet.find('.selected').removeClass('selected');
			$this.addClass('selected');

			// make option object dynamically, i.e. { filter: '.my-filter-class' }
			var options = {},
				key = $optionSet.attr('data-option-key'),
				value = $this.attr('data-option-value');
			// parse 'false' as false boolean
			value = value === 'false' ? false : value;
			options[ key ] = value;
			if ( key === 'layoutMode' && typeof changeLayoutMode === 'function' ) {
				// changes in layout modes need extra logic
				changeLayoutMode( $this, options )
			} else {
				// otherwise, apply new options
				$container.isotope( options );
			}

			return false;
		});
	}


	/*	END NEW PAGES SCRIPT*/

	productImagesInit();

	jQuery(".carousel .product-name a, .carousel .product-image-wrapper a").click(function() {
		window.location = jQuery(this).attr("href");
	});

	jQuery("select.custom").selectbox();

	jQuery("#footer").hover(function() {
		if (jQuery("#footer_popup").hasClass("allowHover") && jQuery("#footer_popup").css('position') == 'absolute') {
			jQuery('#footer_popup').stop(true, false).slideDown(300);
			jQuery(this).find("i.icon-up").addClass("icon-down");
		}
	}, function() {
		if (jQuery("#footer_popup").hasClass("allowHover") && jQuery("#footer_popup").css('position') == 'absolute') {
			jQuery('#footer_popup').stop(true, false).slideUp(100);
			jQuery(this).find("i.icon-up").removeClass("icon-down");
		}
	});

	jQuery('#footer_button').click(function() {

		if (jQuery("#footer_popup").hasClass("allowHover") && jQuery("#footer_popup").css('position') == 'absolute') {

			var footer_icon = jQuery(this).find("i.icon-up");

			if ( footer_icon.hasClass('icon-down') ) {
				jQuery('#footer_popup').stop(true, false).slideUp(100);
				footer_icon.removeClass("icon-down");
			} else {
				jQuery('#footer_popup').stop(true, false).slideDown(300);
				footer_icon.addClass("icon-down");
			}

		}

	});


	jQuery('div.noHover').hover(function() {
		jQuery('#footer_popup').toggleClass("allowHover");
	});

	jQuery("#right_toolbar").hide();

	jQuery(function() {
		jQuery(window).scroll(function() {
			if (jQuery(this).scrollTop() > 150) {
				jQuery('#right_toolbar').fadeIn();
			} else {
				//if (jQuery("#right_toolbar .shopping_cart_mini").css("display") == "block") { jQuery("#right_toolbar .shopping_cart_mini").fadeOut();}
				if ( jQuery("#right_toolbar .shopping_cart_mini").is(":visible") ) { jQuery("#right_toolbar .shopping_cart_mini").fadeOut();}

				jQuery('#right_toolbar').fadeOut();
			}
		});

		jQuery('#back-top a').hover(function() {
			jQuery(this).stop().animate({
				"opacity": 0.6
			});
		}, function() {
			jQuery(this).stop().animate({
				"opacity": 1
			});
		});

		jQuery('#back-top a').click(function() {
			jQuery('body,html').animate({
				scrollTop: 0
			}, 400);
			return false;
		});
	});

	jQuery('.carousel').each(function () {
		jQuery(this).elastislide({
			speed: 600
		});
	})

	jQuery('.flexslider.big').flexslider({
		animation: "slide",
		controlNav: false,
		prevText: "<i class='icon-left-thin'></i>",
		nextText: "<i class='icon-right-thin'></i>"

	});

	jQuery('.flexslider.banners').flexslider({
		animation: "slide",
		pauseOnHover: true,
		controlNav: false,
		prevText: "<i class='icon-left-thin'></i>",
		nextText: "<i class='icon-right-thin'></i>"

	});

	jQuery('.flexslider.vertical').flexslider({
		animation: "slide",
		autoplay: false,
		minItems: 2,
		direction: "vertical",
		pauseOnHover: true,
		controlNav: false,
		prevText: "<i class='icon-down'></i>",
		nextText: "<i class='icon-up'></i>"

	});

	jQuery('.flexslider.small').flexslider({
		animation: "slide",
		pauseOnHover: true,
		controlNav: false,
		prevText: "<i class='icon-left-thin'></i>",
		nextText: "<i class='icon-right-thin'></i>"

	});

	jQuery(".flexslider.big .flex-direction-nav .flex-prev").hover(function() {
		jQuery(".prev-slider").fadeToggle(200, "linear");
	});
	jQuery(".flexslider.big .flex-direction-nav .flex-next").hover(function() {
		jQuery(".next-slider").fadeToggle(200, "linear");
	});

	jQuery('#topline .fadelink, .header_v_2 .fadelink').hover(function() {
		jQuery(this).find(".ul_wrapper").stop(true).fadeToggle(200, "linear");
	});

	jQuery("#header .shoppingcart").mouseenter(function() {
		jQuery(this).find(".shopping_cart_mini").stop(true, true).fadeIn(200, "linear");
	});

	jQuery("#header .shoppingcart").mouseleave(function() {
		jQuery(this).find(".shopping_cart_mini").stop(true, true).fadeOut(200, "linear");
	});

	jQuery("#right_toolbar .shoppingcart").mouseenter(function() {
		jQuery(this).find(".shopping_cart_mini").stop(true, true).fadeIn(200, "linear");
	});

	jQuery("#right_toolbar .shoppingcart").mouseleave(function() {
		jQuery(this).find(".shopping_cart_mini").stop(true, true).fadeOut(200, "linear");
	});

	jQuery(".login_block .login_link").mouseenter(function() {
		jQuery(this).parent().find(".form-login-wrapper").stop(true, false).fadeIn(200, "linear");
	});

	jQuery(".login_block .form-login-wrapper").mouseenter(function() {
		jQuery(this).stop(true, true).fadeIn(0);
		jQuery(this).addClass("active");
	});

	jQuery(".login_block .login_link").mouseleave(function() {
		jQuery(this).parent().find(".form-login-wrapper").stop(true, false).fadeOut(200, "linear");
	});

	jQuery('.login_block .form-login-wrapper input').focusout(function(){
		if (!jQuery(".login_block .form-login-wrapper").hasClass("active")) {
			jQuery(".form-login-wrapper").stop(true, false).fadeOut(200, "linear");
		}
	});
	jQuery(".login_block .form-login-wrapper").mouseleave(function() {
		jQuery(this).removeClass("active");
		if (!jQuery(".login_block .form-login-wrapper input").is(":focus")) {
			jQuery(this).stop(true, false).fadeOut(200, "linear");
		}
	});

	jQuery("#right_toolbar .form-search ").mouseenter(function() {
		jQuery('#right_toolbar .form-search input').animate({
			right: 50,
			width: 275
		}, 300);
	});
	jQuery("#right_toolbar .form-search ").mouseleave(function() {
		jQuery('#right_toolbar .form-search input').stop(true, false).animate({
			right: 20,
			width: 0
		}, 300);
	});

	jQuery('#contentTab a').click(function(e) {
		e.preventDefault();
		jQuery(this).tab('show');
	})

	jQuery("#carousel_tabs>a").click(function() {

		jQuery("#carousel_tabs>a").removeClass("active");
		jQuery(this).addClass("active");

		jQuery("#carousel_tabs_content .carousel").hide();
		var t_content = jQuery(this).attr("href");
		jQuery(t_content).show();
	})
	jQuery("#carousel_tabs>a:first").trigger("click");

	jQuery(".es-nav-prev").hover(function() {
		if (!jQuery(this).hasClass("disable")) {
			jQuery(this).parent().parent().find(".small_preview.prev").stop(true, true).fadeToggle(400, "linear");
		}
	});

	jQuery(".es-nav-next").hover(function() {
		if (!jQuery(this).hasClass("disable")) {
			jQuery(this).parent().parent().find(".small_preview.next").stop(true, true).fadeToggle(400, "linear");
		}
	});
	jQuery('.es-nav-prev').mouseleave(function() {
		jQuery(".small_preview.prev").stop(true, true).fadeOut(100, "linear");
	});

	jQuery('.es-nav-next').mouseleave(function() {
		jQuery(".small_preview.next").stop(true, true).fadeOut(100, "linear");
	});

	jQuery(".direction-nav .prev a").hover(function() {
		jQuery(this).parent().parent().find(".small_preview.prev").stop(true, true).fadeToggle(400, "linear");
	});
	jQuery(".direction-nav .next a").hover(function() {
		jQuery(this).parent().parent().find(".small_preview.next").stop(true, true).fadeToggle(400, "linear");
	});

	jQuery(".flexslider.vertical .flex-prev").hover(function() {
		if (!jQuery(this).hasClass("disabled")) {
			jQuery(this).parent().parent().parent().find(".small_previews .small_preview.prev").stop(true, true).fadeToggle(400, "linear");
		}
	});
	jQuery(".flexslider.vertical .flex-next").hover(function() {
		if (!jQuery(this).hasClass("disabled")) {
			jQuery(this).parent().parent().parent().find(".small_previews .small_preview.next").stop(true, true).fadeToggle(400, "linear");
		}
	});

	jQuery('.carousel_prev').mouseleave(function() {
		jQuery(this).parent().parent().find(".small_preview.prev").stop(true, true).fadeOut(100, "linear");
	});

	jQuery('.carousel_next').mouseleave(function() {
		jQuery(this).parent().parent().find(".small_preview.next").stop(true, true).fadeOut(100, "linear");
	});

	jQuery('#password_text').show();

	jQuery('#password_real').hide();

	jQuery('#password_text input').focus(function(){
		jQuery(this).parent().hide();
		jQuery('#password_real').show();
		jQuery('#password_real input').focus();
	});

	jQuery('#password_real input').blur(function(){
		if(jQuery(this).val() == ""){
			jQuery(this).parent().hide();
			jQuery('#password_text').show();
		}
	});

	TopSlider();

	jQuery('#myTab li').click(function() {

		var tab_name = jQuery(this).attr('data-tab');

		jQuery('#myTab li').removeClass('active');
		jQuery(this).addClass('active');

		jQuery('#tab_content .tab-pane').removeClass('active');
		jQuery('#tab' + tab_name).addClass('active');

		return false;

	});


	var $optionSets = jQuery(".filters-by-category .option-set"),
		$optionLinks = $optionSets.find("a"),
		isotopeOuter = jQuery('.isotope-outer');

	$optionLinks.click(function () {
		var $this = jQuery(this);
		if ($this.hasClass("selected")) return false;
		var $optionSet = $this.parents(".option-set");
		$optionSet.find(".selected").removeClass("selected");
		$this.addClass("selected");
		var options = {}, key = $optionSet.attr("data-option-key"),
			value = $this.attr("data-option-value");
		value = value === "false" ? false : value;
		options[key] = value;

		if ( isotope_animation_enabled == 1 ) {
			if (key === "layoutMode" && typeof changeLayoutMode === "function") {
				changeLayoutMode($this, options);
			} else if (isotopeOuter.length != 0) {
				isotopeOuter.isotope(options);
			}
		} else {
			var data = 'type=' + value + '&per_page=' + $this.attr('data-per-page');

			jQuery('#ajax_loader').show();

			jQuery.ajax({
				url : woocommerce_params.ajax_url,
				method : 'GET',
				data : data + '&action=woocommerce_get_products_filtered&context=frontend',
				dataType : 'json',
				success: function(response) {
					var $row = $this.closest('.woocommerce').find('.row.big_with_description').first();

					$row.empty().html(response);
					jQuery('#ajax_loader').hide();
					productImagesInit();

				}
			});
		}
		return false
	});

	/*
	if ( zoomSliderEnabled ) {

		jQuery('#layerslider').layerSlider({
			thumbnailNavigation : 'hover',
			hoverPrevNext : true,
			keybNav                 : false,
			navPrevNext             : true,
			navStartStop            : false,
			navButtons              : false,
			thumbnailNavigation     : 'disabled',
			pauseOnHover : false,
			showCircleTimer: false,
			skin                    : 'fullwidth',
			skinsPath               : skinsUrl,
			cbInit      : function(element){
				jQuery('.ls-nav-prev').append('<i class="icon-left-thin"></i>');
				jQuery('.ls-nav-next').append('<i class="icon-right-thin"></i>')
			}
		});

	}
	*/

	// WooCommerce scripts
	if ( typeof woocommerce_params != 'undefined' ) {

		// Orderby

		jQuery('.orderby_ajax').val(jQuery('.orderby_ajax option[selected=selected]').val());

		jQuery('select.orderby_ajax').change(function(){

			if ( isotope_animation_enabled == 1 && jQuery('.wft_pagination .page-numbers').length == 0 ) {

				var sort_by = jQuery(this).val();

				if ( sort_by == 'price_desc' || sort_by == 'date' || sort_by == 'rating' || sort_by == 'popularity' ) {
					jQuery('.row.big_with_description').isotope({
						sortBy: sort_by,
						sortAscending : false
					});
				} else {
					jQuery('.row.big_with_description').isotope({
						sortBy: sort_by,
						sortAscending : true
					});
				}

			} else {

				jQuery('#ajax_loader').show();

				var data = jQuery(this).closest('form').serialize() + '&action=woocommerce_get_products_listing&context=frontend';

				if ( jQuery('.price_slider').length > 0 ) {
					data += '&min_price=' + jQuery('#min_price').val() + '&max_price=' + jQuery('#max_price').val();
				}

				jQuery.ajax({
					url : woocommerce_params.ajax_url,
					method : 'GET',
					data : data,
					dataType : 'json',
					success: function(response) {
						//console.log(response)
						if ( isotope_animation_enabled == 1 ) {

							var $container = jQuery('.row.big_with_description');

							$container.isotope('remove', $container.data('isotope').$allAtoms );

							//jQuery('.row.big_with_description').html(response.products);
							var newItems = jQuery(response.products);
							$container.isotope( 'insert', newItems );

						} else {
							jQuery('.row.big_with_description').empty().html(response.products);
						}
						jQuery('.wft_pagination').empty().html(response.pagination);
						jQuery('.listing_header_row2 .pull-left').empty().html(response.result_count);
						jQuery('#ajax_loader').hide();
						productImagesInit();
					}
				});

			}

		});

		jQuery('.pagination_ajax a').live('click', function () {

			jQuery('#ajax_loader').show();

			var current = parseInt(jQuery(this).closest('.pagination_ajax').find('.current').text(), 10);

			if ( jQuery(this).hasClass('next') ) {
				var paged = current + 1;
			} else if ( jQuery(this).hasClass('prev') ) {
				var paged = current - 1;
			} else {
				var paged =  parseInt(jQuery(this).text(), 10);
			}

			var data = jQuery('form.woocommerce-ordering').first().serialize() + '&paged='+ paged +'&action=woocommerce_get_products_listing&context=frontend';

			if ( jQuery('.price_slider').length > 0 ) {
				data += '&min_price=' + jQuery('#min_price').val() + '&max_price=' + jQuery('#max_price').val();
			}

			jQuery.ajax({
				url : woocommerce_params.ajax_url,
				method : 'GET',
				data : data,
				dataType : 'json',
				success: function(response) {

					if ( isotope_animation_enabled == 1 ) {

						var $container = jQuery('.row.big_with_description');

						$container.isotope('remove', $container.data('isotope').$allAtoms );

						var newItems = jQuery(response.products);
						$container.isotope( 'insert', newItems );

					} else {
						jQuery('.row.big_with_description').empty().html(response.products);
					}
					jQuery('.wft_pagination').empty().html(response.pagination);
					jQuery('.listing_header_row2 .pull-left').empty().html(response.result_count);
					jQuery('#ajax_loader').hide();
					productImagesInit();
				}
			});

			return false;
		});

		jQuery('.cart_delete_ajax').live('click', function() {

			jQuery('#ajax_loader').show();

			var $thisbutton = jQuery(this);

			var params = getUrlParams($thisbutton.attr('href'));

			jQuery.ajax({
				url : woocommerce_params.ajax_url,
				method : 'GET',
				data : 'action=woocommerce_cart_remove&context=frontend&ajax_remove_item=' + params['remove_item'] + '&_wpnonce=' + params['_wpnonce'],//out.join('&') + '&action=woocommerce_cart_remove',
				dataType : 'json',
				success : function(response) {

					if ( response.result ) {
						$thisbutton.closest('.item').remove();
						jQuery('.badge.badge-inverse').empty().text(response.item_count);
					}
					jQuery('#ajax_loader').hide();
					jQuery('#ajax_message .inside').empty().html(response.message);
					jQuery('#ajax_message').show().delay(3000).fadeOut('slow');
				}
			});

			return false;
		});

		jQuery(document).on('click', 'a.quickview', function() {

			var prod_id = jQuery(this).data('product-id');

			jQuery.fancybox({
				'href' : woocommerce_params.ajax_url + '?action=et_product_quick_view&context=frontend&prodid=' + prod_id,
				'type' : 'ajax',
				helpers : {
					overlay: {
						locked: true
					}

				},
				afterShow : function() {

					jQuery(function() {
						jQuery('.variations_form').wc_variation_form();
						jQuery('.variations_form .variations select').change();
					});

				}
				//'scrolling' : 'yes'
			});



			return false;
		});

		// Search autocomplete (jquery.autocomplete.js)
		if ( search_autocomplete_enabled > 0 ) {
			var sa_options = {
				serviceUrl: woocommerce_params.ajax_url + '?action=wft_search_autocomplete',
				minChars: 2
			};
			jQuery('#logo_area .search-query').autocomplete(sa_options);
			//jQuery('#right_toolbar .search-query').autocomplete(sa_options);
		}

		var price_sliding = false

		jQuery('.price_slider_wrapper').live('mousedown', function() {
			price_sliding = true
		});

		// Ajax price slider
		jQuery(document).live('mouseup', function() {

			if ( price_sliding ) {
				var data = jQuery('.price_slider_wrapper').closest('form').serialize();

				if ( jQuery('.orderby_ajax').length != 0 ) {
					data += '&orderby=' + jQuery('.orderby_ajax').val();
				} else if ( jQuery('.orderby').length != 0 ) {
					data += '&orderby=' + jQuery('.orderby').val();
				}

				jQuery('#ajax_loader').show();

				jQuery.ajax({
					url : woocommerce_params.ajax_url,
					method : 'GET',
					data : data + '&action=woocommerce_get_products_listing&context=frontend',
					dataType : 'json',
					success: function(response) {

						if ( isotope_animation_enabled == 1 ) {
							jQuery('.row.big_with_description').isotope('remove', jQuery('.row.big_with_description').data('isotope').$allAtoms )
							jQuery('.row.big_with_description').isotope( 'insert', jQuery(response.products) );
						} else {
							jQuery('.row.big_with_description').empty().html(response.products);
						}
						jQuery('.wft_pagination').empty().html(response.pagination);
						jQuery('.listing_header_row2 .pull-left').empty().html(response.result_count);
						jQuery('#ajax_loader').hide();
						productImagesInit();
					}
				});

				price_sliding = false;
			}
		});

		// Wishlist

		jQuery(document).on('click', '.wft_add_to_wishlist', function() {

			jQuery('#ajax_loader').show();

			jQuery.ajax({
				url : woocommerce_params.ajax_url,
				method : 'GET',
				data : '&action=add_to_wishlist&context=frontend&add_to_wishlist=' + jQuery(this).attr('data-product-id'),
				success: function(response) {
					//console.log(response)
					var msgs = response.split('##');

					jQuery('#ajax_loader').hide();
					jQuery('#ajax_message .inside').empty().html(msgs[1]);
					jQuery('#ajax_message').show().delay(3000).fadeOut('slow');


				}
			});

			return false;
		});
	}


	// PROGRESS BAR
	jQuery('.progress .bar').each(function() {

		var val = parseInt(jQuery(this).attr('data-width'), 10),
			len = val + '%';
		jQuery(this).css('width', len);

	});

	if (jQuery('#nav').length > 0) {
		jQuery('#spy').find('nav').html(jQuery('#nav.for_spy:first').clone());
		jQuery('#spy').find('nav li').removeClass('hover');

		if ( jQuery('#logo_area .widget_shopping_cart').length > 0 ) {
			jQuery('#spy').find('.spyshop').html(jQuery('#logo_area .widget_shopping_cart').clone());
		}
	} else {
		jQuery('#spy').remove();
	}


	// collapse top navigation menu
	jQuery('nav').find(".collapse").collapse();

	if ( !isTouchDevice() ) {
		if (jQuery(".parallax").length > 0) jQuery(".parallax").parallax({
			speed: 0,
			axis: "y"
		});
	}
	// isotope animation
	if ( isotope_animation_enabled == 1 ) {
		wft_isotope_init();
		elementsAnimate();
	}

	// Check variations
	jQuery('').on( 'check_variations', function( event, exclude, focus ) {
		var all_set = true,
			any_set = false,
			showing_variation = false,
			current_settings = {},
			$variation_form = $( this ),
			$reset_variations = $variation_form.find( '.reset_variations' );

		$variation_form.find( '.variations select' ).each( function() {

			if ( $( this ).val().length === 0 ) {
				all_set = false;
			} else {
				any_set = true;
			}

			if ( exclude && $( this ).attr( 'name' ) === exclude ) {

				all_set = false;
				current_settings[$( this ).attr( 'name' )] = '';

			} else {

				// Encode entities
				value = $( this ).val();

				// Add to settings array
				current_settings[ $( this ).attr( 'name' ) ] = value;
			}

		});

		var product_id = parseInt( $variation_form.data( 'product_id' ) ),
			all_variations = $variation_form.data( 'product_variations' );

		// Fallback to window property if not set - backwards compat
		if ( ! all_variations )
			all_variations = window.product_variations.product_id;
		if ( ! all_variations )
			all_variations = window.product_variations;
		if ( ! all_variations )
			all_variations = window['product_variations_' + product_id ];

		var matching_variations = $.fn.wc_variation_form.find_matching_variations( all_variations, current_settings );

		if ( all_set ) {

			var variation = matching_variations.shift();

			if ( variation ) {

				// Found - set ID
				$variation_form
					.find( 'input[name=variation_id]' )
					.val( variation.variation_id )
					.change();

				$variation_form.trigger( 'found_variation', [ variation ] );

			} else {

				// Nothing found - reset fields
				$variation_form.find( '.variations select' ).val( '' );

				if ( ! focus )
					$variation_form.trigger( 'reset_image' );

				alert( wc_add_to_cart_variation_params.i18n_no_matching_variations_text );

			}

		} else {

			$variation_form.trigger( 'update_variation_values', [ matching_variations ] );

			if ( ! focus )
				$variation_form.trigger( 'reset_image' );

			if ( ! exclude ) {
				$variation_form.find( '.single_variation_wrap' ).slideUp( 200 );
			}

		}

		if ( any_set ) {

			if ( $reset_variations.css( 'visibility' ) === 'hidden' )
				$reset_variations.css( 'visibility', 'visible' ).hide().fadeIn();

		} else {

			$reset_variations.css( 'visibility', 'hidden' );

		}

	} );

	if ( jQuery('.mega_main_menu .nav_woo_cart').length > 0 ) {
		jQuery('.mega_main_menu .nav_woo_cart').on('hover', function() {
			jQuery('.mega_main_menu .shopping_cart_mini').show();
		});
	}

});

jQuery(window).bind('load', function() {


	if ( isotope_animation_enabled == 1 ) {
		if (jQuery('.row.big_with_description').length != 0){
			jQuery('.row.big_with_description').isotope('reLayout');
		}
	}

	jQuery(window).resize(function() {
		jQuery(".collapse").collapse();
		jQuery(".preview").hide();
		jQuery(".small_preview").hide();
		jQuery(".shopping_cart_mini").hide();
		jQuery(".form-login-wrapper").hide();
		TopSlider();

		if ( isotope_animation_enabled == 1 ) {
			if (jQuery('.row.big_with_description').length != 0){
				jQuery('.row.big_with_description').isotope('reLayout');
			}
		}

	});
});

function TopSlider() {
	var w0 = jQuery(document).width();
	var w1 = (w0 - jQuery(".container").width()) * 0.5 - 0;
	jQuery(".flexslider.big .flex-direction-nav .flex-next").css({
		"right": w1 + "px"
	});
	jQuery(".flexslider.big .flex-direction-nav .flex-prev").css({
		"left": w1 + "px"
	});
	jQuery(".flexslider.big .next-slider").css({
		"right": w1 + "px"
	});
	jQuery(".flexslider.big .prev-slider").css({
		"left": w1 + "px"
	});
};
<!--SPY-->
jQuery(window).scroll(function() {
	if (jQuery(".container").width()>767){
		if (jQuery(this).scrollTop() > jQuery ('#header .wrapper_w').height() +60+ jQuery ('#topline').height()) {
			jQuery('#spy').addClass('fix');

		} else {
			jQuery('#spy').removeClass('fix');
		}}

	if ( isotope_animation_enabled == 1 ) {
		if (jQuery('.row.big_with_description').length != 0){
			jQuery('.row.big_with_description').isotope('reLayout');
		}
	}
});

function getUrlParams(url) {
	var params = {};
	url.substring(1).replace(/[?&]+([^=&]+)=([^&]*)/gi,
		function (str, key, value) {
			params[key] = value;
		});
	return params;

}

function productImagesInit() {
	jQuery('.product .product-image-wrapper').mouseenter(function() {
		var pos = jQuery(this).parent().position();
		var width = jQuery(this).outerWidth();
		var width1 = jQuery(this).parent().next(".preview").outerWidth();
		var width3 = width1 - width;
		jQuery(this).parent().next(".preview").css({
			top: pos.top + 10 + "px",
			left: (pos.left - width3 + 30) + "px"
		})
		jQuery(this).parent().next(".preview.small").css({
			top: pos.top + 10 + "px",
			left: (pos.left - width3 + 30) + "px"
		})

		jQuery(".preview").hide();
		jQuery(this).parent().next(".preview").show();
		//jQuery(this).parent().next(".preview").css({        "display": "inline-block"    });
		if ( isotope_animation_enabled == 1 ) {
			jQuery('.row.big_with_description').isotope('reLayout');
		}
	});

	jQuery('.preview').mouseleave(function() {
		jQuery(this).stop().hide();
	});

	jQuery(".preview .image").hover(function() {
		var image = jQuery(this).attr("data-rel");
		jQuery(this).parent().parent().find('.col-2 .big_image a img').attr('src',image);
		return false;

	});
	jQuery(".preview .image").click(function() {
		var image = jQuery(this).attr("data-rel");
		jQuery(this).parent().parent().find('.col-2 .big_image a img').attr('src',image);
		return false;

	});
	elementsAnimate();
}

function wft_isotope_init() {
	if (jQuery(".row.big_with_description").length != 0) {

		var $container = jQuery('.row.big_with_description');

		if (jQuery('.row.big_with_description .product')[0]){
			$container.isotope({
				itemSelector: '.product',
				containerStyle: {
					position: 'relative',
					overflow: 'visible'
				},
				getSortData: {
					menu_order: function ($elem) {
						return $elem.find('.product-name a ').text();
					},
					price: function ($elem) {
						return parseFloat($elem.find('.sort-price').text());
					},
					price_desc : function ($elem) {
						//this.sortAscending = false;
						return parseFloat($elem.find('.sort-price').text());
					},
					date : function($elem) {
						return parseInt($elem.find('.sort-date').text());
					},
					rating : function($elem) {
						return parseFloat($elem.find('.sort-rating').text());
					},
					popularity : function($elem) {
						return parseFloat($elem.find('.sort-rating').text());
					},
				}

			});
		}

		if (jQuery('.row.big_with_description .post-preview-small')[0]){
			$container.isotope({
				itemSelector: '.post-preview-small'
			});
			var $optionSets = jQuery('#options .option-set'),
				$optionLinks = $optionSets.find('a');
			$optionLinks.click(function(){
				var $this = $(this);
				// don't proceed if already selected
				if ( $this.hasClass('selected') ) {
					return false;
				}
				var $optionSet = $this.parents('.option-set');
				$optionSet.find('.selected').removeClass('selected');
				$this.addClass('selected');

				// make option object dynamically, i.e. { filter: '.my-filter-class' }
				var options = {},
					key = $optionSet.attr('data-option-key'),
					value = $this.attr('data-option-value');
				// parse 'false' as false boolean
				value = value === 'false' ? false : value;
				options[ key ] = value;
				if ( key === 'layoutMode' && typeof changeLayoutMode === 'function' ) {
					// changes in layout modes need extra logic
					changeLayoutMode( $this, options )
				} else {
					// otherwise, apply new options
					$container.isotope( options );
				}

				return false;
			});

		}

	}
}

function elementsAnimate() {
	var windowWidth = window.innerWidth || document.documentElement.clientWidth;
	var animate = jQuery('.animate');
	var animateDelay = jQuery('.animate-delay-outer');
	var animateDelayItem = jQuery('.animate-delay');
	if (windowWidth > 767 && !isiPhone()) {
		animate.bind('inview', function (event, visible) {
			if (visible && !jQuery(this).hasClass("animated")) {
				jQuery(this).addClass("animated");
			}
		});
		animateDelay.bind('inview', function (event, visible) {
			if (visible && !jQuery(this).hasClass("animated")) {
				var j = -1;
				var $this = jQuery(this).find(".animate-delay");
				$this.each(function () {
					var $this = jQuery(this);
					j++;
					setTimeout(function () {
						$this.addClass("animated");
					}, 200 * j);
				});
				jQuery(this).addClass("animated");
			}
		});
	} else {
		animate.each(function () {
			jQuery(this).removeClass('animate');
		});
		animateDelayItem.each(function () {
			jQuery(this).removeClass('animate-delay');
		});
	}
}

function isTouchDevice() {
	return (typeof (window.ontouchstart) != 'undefined') ? true : false;
}

function isiPhone() {
	return (
		(navigator.userAgent.toLowerCase().indexOf("iphone") > -1) ||
			(navigator.userAgent.toLowerCase().indexOf("ipod") > -1)
		);
}