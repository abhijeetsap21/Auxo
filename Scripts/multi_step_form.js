$(document).ready(function(){
//jQuery time
var current_fs, next_fs, previous_fs; //fieldsets
var left, opacity, scale; //fieldset properties which we will animate
var animating; //flag to prevent quick multi-click glitches

$(".next").click(function () {
    debugger
	if(animating) return false;
	animating = true;
	//var form = $("#msform");
		//form.validate({
	
	
		//errorElement: 'span',
		//			errorClass: 'help-block',
		//			highlight: function(element, errorClass, validClass) {
		//				$(element).closest('.form-group').addClass("has-error");
		//			},
		//			unhighlight: function(element, errorClass, validClass) {
		//				$(element).closest('.form-group').removeClass("has-error");
		//			},
		//			rules: {
		//				fname: {
		//					required: true,
							
		//				},
		//				lname : {
		//					required: true,
							
		//				},
		//				phone : {
		//					required: true,
							
		//				},
		//				email : {
		//					required: true,
		//				},
		//				lUrl : {
		//					required: true,
		//				},
		//				passyear12 :{
		//					required: true,
							
		//				},
		//				percentage12 :{
		//					required : true,
							
		//				},
		//				gradcollege :{
		//					required : true,
		//				},
		//				gradname :{
		//					required : true,
		//				},
		//				gradpassyear :{
		//					required: true,
							
		//				},
		//				gradpercentage :{
		//					required: true,
							
		//				},
		//				pgradcollege :{
		//					required : true,
		//				},
		//				pgradname :{
		//					required : true,
		//				},
		//				pgradpassyear :{
		//					required: true,
							
		//				},
		//				pgradpercentage :{
		//					required: true,
							
		//				},
						
						
		//			},
		//		messages: {
		//				fname: {
		//					required: "First name required",
		//				},
		//				lname: {
		//					required: "Last name required",
		//				},
		//				phone: {
		//					required: "phone number required",
		//				},
		//				email: {
		//					required: "email required",
		//				},
		//				phone: {
		//					required: "phone number required",
		//				},
		//				lUrl: {
		//					required: "please provide Linkdin Url",
		//				},
		//				passyear12 :{
		//					required: "please enter passing year",
		//				},
		//				percentage12 :{
		//					required: "please enter your percentage obtained",
		//				},
		//				gradcollege :{
		//					required : "please enter your degree name",
		//				},
		//				gradname :{
		//					required : "please enter your degree name",
		//				},
		//				gradpassyear :{
		//					required : "please enter graduation year",
		//				},
		//				gradpercentage :{
		//					required : "please enter graduation percentage",
		//				},
		//				pgradcollege :{
		//					required : "please enter your college name",
		//				},
		//				pgradname :{
		//					required : "please enter your degree name",
		//				},
		//				pgradpassyear :{
		//					required : "please enter post graduation year",
		//				},
		//				pgradpercentage :{
		//					required : "please enter post graduation percentage",
		//				},
		//		}
		//});
		if (1 == 1)
		{
			current_fs = $(this).parent();
			next_fs = $(this).parent().next();
			next_fs.show();
			$("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
			 
			current_fs.animate({opacity: 0}, {
			step: function(now, mx) {
			//as the opacity of current_fs reduces to 0 - stored in "now"
			//1. scale current_fs down to 80%
			scale = 1 - (1 - now) * 0.2;
			//2. bring next_fs from the right(50%)
			left = (now * 50)+"%";
			//3. increase opacity of next_fs to 1 as it moves in
			opacity = 1 - now;
			current_fs.css({
        'transform': 'scale('+scale+')',
        'position': 'absolute'
      });
			next_fs.css({'left': left, 'opacity': opacity});
		}, 
		duration: 800, 
		complete: function(){
			current_fs.hide();
			animating = false;
		}, 
		//this comes from the custom easing plugin
		easing: 'easeInOutBack'
	});
	}
	
	
});

$(".previous").click(function(){
	if(animating) return false;
	animating = true;
	
	current_fs = $(this).parent();
	previous_fs = $(this).parent().prev();
	
	//de-activate current step on progressbar
	$("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");
	
	//show the previous fieldset
	previous_fs.show(); 
	//hide the current fieldset with style
	current_fs.animate({opacity: 0}, {
		step: function(now, mx) {
			//as the opacity of current_fs reduces to 0 - stored in "now"
			//1. scale previous_fs from 80% to 100%
			scale = 0.8 + (1 - now) * 0.2;
			//2. take current_fs to the right(50%) - from 0%
			left = ((1-now) * 50)+"%";
			//3. increase opacity of previous_fs to 1 as it moves in
			opacity = 1 - now;
			current_fs.css({'left': left});
			previous_fs.css({'transform': 'scale('+scale+')', 'opacity': opacity});
		}, 
		duration: 800, 
		complete: function(){
			current_fs.hide();
			animating = false;
		}, 
		//this comes from the custom easing plugin
		easing: 'easeInOutBack'
	});
});
});
