$(document).ready(function() {

    $("#divLogin").show(); 


    $(function() {
        $('[data-toggle="tooltip"]').tooltip();
    });
    $('.add-more-achivements').click(function () {
        $('#add-achivement').removeClass('d-none');
    });

    $(".headerDropDown").click(function(e) {
        e.preventDefault();
        $(this).parent("li").toggleClass("active");
        $(".collapse-able").slideToggle();
        $(".avatar-dropdown").toggleClass("rotateDropDown");
    });

    $(".collapse-able li").click(function() {
        $(".collapse-able li").removeClass("active");
        $(this).toggleClass("active").children("ul").slideToggle();
        $(this).find(".lnr-chevron-down").toggleClass("rotateDropDown")
    });

    $(".search-btn").click(function() {
        $(".search-bar").toggleClass("open");
        $(".m-logo").toggleClass('display-none');
        $(".search-bar input[type='text' ").focus();


    });

    //$("#txtblogsearch, #searchBtn, #txtSearchQuestion").focusin(function () {
    //    $("#bn4").breakingNews({
    //        effect: "slide-v",
    //        autoplay: true,
    //        timer: 3000,
    //        color: 'green',
    //        border: true
    //    });
    //    $(this).parent(".search-cover").addClass("box-outline")
    //});

    $("#txtblogsearch, #searchBtn, #searchBtn").focusout(function() {
        $(this).parent(".search-cover").toggleClass("box-outline")
    });



    //////////////////  Script for Mobile ended here ////////////////////////



    // $(".active-toogle").click(function(e){
    // e.preventDefault();
    // $(this).children("span").toggleClass("active");
    // });

    $(".nav-list li > a").click(function() {
        $(this).siblings("ul").slideToggle();
        //$(".nav-list li").removeClass("active");
        //$(this).parent("li").toggleClass("active");
        $(this).children(".drop-down").toggleClass("rotateDropDown");
    });

    $(".toggle").click(function() {
        $("header").toggleClass("openHeaderNavigation");
        $(".inner-header").toggleClass("openNav");
        $(".header-black-layer").toggleClass("active");
    });

    $(".header-black-layer").click(function() {
        $("header").removeClass("openHeaderNavigation");
        $(".inner-header").removeClass("openNav");
        $(".header-black-layer").removeClass("active");
    });


    $(".remove-body-fixed-class").click(function() {

        $("body").removeClass("overflowHidden");
    });

    $(".right-panel-back-layer").click(function() {
        $(".right-panel").removeClass("openRightPanel");
        $(".right-panel-back-layer").removeClass("active");
        $("body").removeClass("overflowHidden");
    });



    
    function showLoader() {
        $("#globalLoader").show();
    }


    function hideLoader() {
        $("#globalLoader").hide();
    }

    $(document).on('click', '.hide-body-scroll', function() {
        hideBodyScroller();
    });

    $(document).on('click', '.add-scroller', function() {
        showBodyScroller();
    });
    
    var divHeight = $('.summaryblock').height();
    $('.blankdivheight').css('height', divHeight + 'px');
    $(".userdropdown-menu li .usertitle a").click(function() {
        var selText = $(this).text();
        $(this).parents('.btn-group').find('.dropdown-toggle').html(selText + ' <span class="icon-caret-down"></span>');
    });
    $('.dropdown-menu a').click(function() {
        $('#selected').text($(this).text());
    });
    
    
    
    
   });


//$("#bn4").breakingNews({
//    effect: "slide-v",
//    autoplay: true,
//    timer: 3000,
//    color: 'green',
//    border: true
//});

$(document).on('click', '#close-popup', function() {

    $(".backgroundoverlay").hide();
     $("#event-popup").removeClass('display-blockk');
    
    $('body').removeClass('remove-scroller');


});
// $(document).on('click', '.crate-event-btn', function() {

    // $("#event-popup").addClass('display-blockk');

   

// });
$(document).ready(function(){
    var i = 0;
    //add the Selected class to the checked radio button
    
});

//Event Drop down

function postCommentDuplicateCheck(e) {
    if (e.keyCode == 13 && $(this).val() != '') {
        if ($postCommentPosted) {
            return false;
        } else {
            $postCommentPosted = true;
            showLoader1();
            return true;
        }
    }
    return true;
};

$(document).ready(function () {
    $postCommentPosted = false;
    $(".postcommentduplicate").keydown(postCommentDuplicateCheck);
    if (typeof Sys !== 'undefined') {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            hideLoader1();
            $postCommentPosted = false;
            $(".postcommentduplicate").keydown(postCommentDuplicateCheck);
        });
    }
     $(document).on('click', '.cross-icon', function() {
 
        $('.event-dtails-popup').hide();
     
         $("body").removeClass("remove-scroller-m");
    })

$('.login-popup').click(function(){
    $('#loginModal').addClass('d-block')
    $('body').css({'overflow':'hidden', 'position':'fixed', 'width':'100%'});
});
$('.modal-close-login').click(function(){
    $('#loginModal').removeClass('d-block');
    $('body').css({'overflow':'visible', 'position':'static'});
    $('#signUpModal').hide();
});

$('.about-us').click(function(){
    $('.about-us-page').addClass('display-blockk');
});
$('.privacy-policy').click(function(){
    $('.privacy-policy-page').addClass('display-blockk');
});
$('.terms-services').click(function(){
    $('.terms-services-page').addClass('display-blockk');
});
$('.Youtube').click(function(){
    $('.you-tube').addClass('display-blockk');
});
$('.close-page-popup').click(function(){
    $('.about-us-page, .privacy-policy-page, .terms-services-page, .you-tube').removeClass('display-blockk');
}); 
$('.hide-gmail').click(function(){
    $('.remove-inline').removeAttr("style");
}); 
// $('.hide-wts').click(function(){
//     alert('asdfasdf');
//     $('.remove-inline-wts').removeAttr("style");
// }); 

});

function goBack() {
  window.history.back();
}

window.onpageshow = function (event) { if (event.persisted) { window.location.reload(); } };

function setFocus(){

    $("#searchList, .outline-trig").focusin(function() { 
      $(this).parent(".search-cover").addClass("box-outline");
    });
    
    $("#searchList, .outline-trig").focusout(function() { 
      $(this).parent(".search-cover").toggleClass("box-outline")
    });
}
function showHideUpload(){
        var isiPhone = /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;
        if (isiPhone)
        {
          $('#lnkuploadDoc, .hide-button-ios').addClass('display-none');
          $('#lnkuploadIOS').addClass('display-blockk');
          $('.back-arrow-iphone').addClass('iphone-icons-align');
          $('.inner-header .m-logo').css({"margin": "10px 0px 0px 3px", "text-align": "center"});
          $('.inner-header .m-logo img').css('margin-top','-8px');
          $('.hide-ios').addClass('display-none');
          $('#lnkuploadIOS').click(function(){

            alert("This feature isn't supported on this device.");

          });
        }
}
function showBodyScroller() {
   $('body').removeClass('remove-scroller');
}

function hideBodyScroller() {
    $('body').addClass('remove-scroller');
}
/***********************Right Swipe*************/
function rightSwipe(){

        $(".m-aside-trigger").click(function() {
        $(".right-panel").addClass("openRightPanel");
        $(".right-panel-back-layer").addClass("active");
        $("body").toggleClass("overflowHidden");
    });

    $(".right-panel .back").click(function() {
        $(".right-panel").removeClass("openRightPanel");
        $(".right-panel-back-layer").removeClass("active");
        $("body").removeClass("overflowHidden");
    });
}

/************Height auto textarea*************/
function heightAuto(){
$('.post-footer').on( 'change keyup keydown paste cut', 'textarea', function (){
    $(this).height(0).height(this.scrollHeight);
})
}

/*********************Sctoll functopn**********/
function inputScroll(){
       $("#lnkEditComment").click(function (){
 
                $('html, body').animate({
                    scrollTop: $("#txtComment").offset().top
                }, 500);
            });
}

/*********************Radio Button**********/
function radioButton(){
 
     $('.radio-itmes input').on('change', function() {
            $('.radio-itmes input').not(this).prop('checked', false);  
        });
}

/*********************Onclick Scroll**********/
// function clickScroll(){
// $('a').click(function(){
//     $('html, body').animate({
//         scrollTop: $( $(this).attr('href') ).offset().top
//     }, 1000);
//     return false;
// });
// }

/***************** Delete success popup***********/
function deleteSuccess(){
    $('.success-popup').click(function(){
       $('#post-delete').addClass('display-blockk');
    });
    $('.remove-popup').click(function(){
        $('#post-delete').removeClass('display-blockk');
    });
}

$(document).mouseup(function(e) 
{
    var container = $(".hide-outside-click");

    // if the target of the click isn't the container nor a descendant of the container
    if (!container.is(e.target) && container.has(e.target).length === 0) 
    {
        container.hide();
    }
});


  
