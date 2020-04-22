 
      //$(document).ready(function() {
  function setRemeberMeForApp() {
              if (getCookie('myScrlAppCookie') == 'hideRememberMe') {

                $('.login-cover, #divEducation, #divAchivement').addClass('backgroundoverlay-image');
                $('.landing-page').addClass('d-none');

              }
              else
              {
    
                $('.login-cover, #divEducation, #divAchivement').removeClass('backgroundoverlay-image');
                $('.landing-page').removeClass('d-none');
              }
           //});
          }
          function getCookie(name) {
            var value = "; " + document.cookie;
            var parts = value.split("; " + name + "=");
            if (parts.length == 2) return parts.pop().split(";").shift();
          }
