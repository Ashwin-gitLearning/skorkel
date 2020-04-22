<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Landing.aspx.cs" Inherits="Landing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="usc" TagName="UserControl_LandingUC" Src="~/UserControl/LandingUC.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <link rel="shortcut icon" type="image/png" href="images/skorkel-fav-icon.png" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <!-- Chrome, Firefox OS and Opera -->
    <meta name="theme-color" content="#00b6bc">
    <!-- Windows Phone -->
    <meta name="msapplication-navbutton-color" content="#00b6bc">
    <!-- iOS Safari -->
    <meta name="apple-mobile-web-app-status-bar-style" content="#00b6bc">
    <!-- <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE11" /> -->
    <meta http-equiv="Content-Script-Type" content="text/javascript" />
    <title>Skorkel</title>
    <link href="<%=ResolveUrl("css/style.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("css/jquery.bxslider.css")%>" rel="stylesheet" type="text/css" />


    <link href='https://fonts.googleapis.com/css?family=Quicksand:400,700,400italic' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" href="https://cdn.linearicons.com/free/1.0.0/icon-font.min.css" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap-reboot.css" rel="stylesheet" />
    <link href="css/bootstrap-grid.css" rel="stylesheet" />
    <link href="css/font-icon.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/animate.css">

    <!--      <script type="text/javascript" src="<%=ResolveUrl("js/jquery-1.8.2.min.js")%>"></script>-->
    <!--      <script type="text/javascript" src="<%=ResolveUrl("js/jquery.bxslider.js")%>"></script>-->
    <script src="js/jquery.1.12.4.min.js"></script>
    <script>
        (function (i, s, o, g, r, a, m) {
        i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments)
        }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-77804115-1', 'auto');
        ga('send', 'pageview');



        var encodedUrl = window.location.search;

        if (encodedUrl) {
            // $("#loginModal").addClass("d-block");
            setTimeout(function () {
                $("#loginModal").addClass("d-block"); $('body').addClass('remove-scroller');
            }, 1000);


        };

       </script>
    <!--
      <script>
        $(document).ready(function(){
                $('.bxslider').bxSlider({
                onSliderLoad: function(){
                $(".bxslider-wrap").css("visibility", "visible");

                }
              
            });
        })
      </script>
-->

    <!--<script src="<%=ResolveUrl("js/jquery.iosslider.min.js")%>" type="text/javascript"></script> -->
    <script src="<%=ResolveUrl("js/hex_md5.js")%>" type="text/javascript"></script>
    <!---NEW ADD CSS FOR FONTS AND ICONS-->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />

    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />  
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />  --%>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">  
        //function myFunction() {
        //    var x = document.getElementById("myInput");
        //    if (x.type === "password") {
        //        x.type = "text";
        //    } else {
        //        x.type = "password";
        //    }
        //}

        $(document).ready(function () {

            //$('#show_password').hover(function show() {  
            //    //Change the attribute to text  
            //    $('#Password').attr('type', 'text');  
            //    $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');  
            //},  
            //CheckBox Show Password  
            $('#show_password').click(function () {
                //debugger;
                ////var cls_name = $('#span_show_pwd').find('span').attr('class');
                //var spanCls = document.getElementById("span_show_pwd").className;
                //$('#Password').attr('type', 'text');
                //$('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
                if ($('#span_show_pwd').hasClass('fa fa-eye-slash')) {
                    $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
                    $('#Password').attr('type', 'text');
                } else {
                    //$('#span_show_pwd').addClass('fa fa-eye-slash');
                    $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
                    $('#Password').attr('type', 'password');
                }
            });

            // isMobileVersion = document.getElementsByClassName('snake--mobile');
            //if (isMobileVersion !== null)
            //    alert('xx');
            //CheckBox Show Password  
            //$('#ShowPassword').click(function () {  
            //$('#Password').attr('type', $(this).is(':checked') ? 'text' : 'password');  
            //});  
        });
      </script>
</head>
<body>

    <form id="form2" runat="server">
        <usc:UserControl_LandingUC ID="UserControl1" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hdnEncpass" ClientIDMode="Static" runat="server" Value="" />
        <div class="main_page_home landingpage_home d-none">
            <div class="innerContainer">
                <div class="logo">
                    <a href="#">
                        <img src="images/logo-inner.png" alt="Skorkel"><span class="landing_logo"> SKORKEL </span>
                    </a>
                </div>
                <div class="navigationInner">
                    <ul class="landingMenu">
                        <li><a href="SignUp1.aspx">SIGN UP</a></li>
                        <li></li>
                        <li>
                            <asp:LinkButton ID="lnklogin" Text="LOGIN" runat="server"
                                ClientIDMode="Static" OnClientClick="javascript:lnklog();return false;"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="cls"></div>
        </div>

        <!---Sucess Popup-->
        <!--login pop up starts-->
        <%--<div id="dvSourceLinkErrorText" class="error_message" runat="server" clientidmode="Static"></div>--%>
        <div id="divLogin" runat="server" clientidmode="Static" class="popupLoginSocialOuterBox"
            style="display: none">

            <p class="closePopIconH d-none">
                <asp:LinkButton ID="lnkCloseBtn" runat="server" ClientIDMode="Static" OnClientClick="javascript:lnkCloseBtns();return false;">
                        <img src="images/close-pop.jpg" alt="" />
                     </asp:LinkButton>
            </p>

            <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" BorderStyle="None"
                RememberMeSet="false" Width="100%">
                <LayoutTemplate>

                    <!-- Modal start here -->
                    <div class="modal overflowAuto login-modal backgroundoverlay" id="loginModal">
                        <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                                <div class="login-container">
                                    <div class="clearfix"></div>
                                    <div class="login-header d-flex align-items-center justify-content-between">
                                        <span class="login-logo">
                                            <img src="images/blue-logo.svg" alt="" class="img-fluid" />
                                        </span>

                                        <ul class="login-nav">
                                            <li class="active"><a href="#">Login</a></li>
                                            <li><a href="SignUp1.aspx">Sign Up</a></li>
                                        </ul>
                                    </div>
                                    <!-- login-header ended -->

                                    <div class="welcome-con">
                                        <h3>Welcome to Skorkel</h3>
                                        <!-- <p class="text-center">Lorem Ipsum is simply dummy text of the printing and typesetting</p>-->
                                    </div>
                                    <!-- welcome-con ended -->

                                    <div class="login-form-con">

                                        <div class="form-row">
                                            <div class="col-12 form-group ">
                                                <label class="sr-only" for="inlineFormInputGroup">
                                                    Username or email</label>
                                                <div class="input-group flex-row search-cover">
                                                    <div class="input-group-prepend">
                                                        <div class="input-group-text">
                                                            <img src="images/form-user-icon.svg"></img>
                                                        </div>
                                                    </div>
                                                    <asp:TextBox ID="UserName" runat="server" ClientIDMode="Static" CssClass="form-control outline-trig" placeholder="Username or email"></asp:TextBox>
                                                </div>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" CssClass="" Display="Dynamic" ErrorMessage="Please enter valid email." ToolTip="Email is required." ValidationGroup="Login1">
                                          </asp:RequiredFieldValidator>
                                            </div>
                                            <!-- col-auto ended  -->
                                            <div class="col-12 form-group ">
                                                <label class="sr-only" for="inlineFormInputGroup">
                                                    Password</label>
                                                <div class="input-group flex-row search-cover position-relative iphone-cursor-issue">
                                                    <div class="input-group-prepend">
                                                        <div class="input-group-text">
                                                            <img src="images/form-lock-icon.svg"></img>
                                                        </div>
                                                    </div>
                                                    <asp:TextBox ID="Password" runat="server" ClientIDMode="Static" CssClass="form-control outline-trig pr-4" placeholder="Password" TextMode="Password"></asp:TextBox>
                                                    <%--Newly Added--%>
                                                    <div class="input-group-append position-absolute show-password">
                                                        <div id="show_password" class="">
                                                            <span id="span_show_pwd" class="fa fa-eye-slash icon"></span>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix">
                                                    </div>
                                                </div>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" CssClass="" Display="Dynamic" ErrorMessage="Please enter valid password." ToolTip="Password is required." ValidationGroup="Login1">
                                    </asp:RequiredFieldValidator>
                                                <asp:Label ID="FailureText" runat="server" ClientIDMode="static" EnableViewState="False" Style="color: red; float: left;" Visible="false"></asp:Label>
                                                <%--<asp:ValidationSummary ID="vldSummary" HeaderText="Invalid User" Visible="false"
                                    DisplayMode="BulletList" EnableClientScript="true" runat="server"/>--%>
                                            </div>
                                            <!-- col-auto ended  -->
                                            <div class="d-flex align-items-start justify-content-between w-full-100">
                                                <div id="contentslider" class="custom-checkbox w-auto for-ie">
                                                    <asp:CheckBox ID="RememberMe" runat="server" Checked="true" Text="Remember me" />
                                                </div>
                                                <a class="grey-text forgot-pwd" href="ForgetPassword.aspx">Forgot password?</a>
                                            </div>
                                            <div class="submit-con">
                                                <div class="modal-close-login btn btn-outline-primary m-r-15 add-scroller" data-dismiss="modal">
                                                    Cancel
                                                </div>
                                                <asp:Button ID="button" runat="server" class="btn btn-primary" Font-Bold="True" OnClick="LoginButton_Click" OnClientClick="LoginButton_Click();" TabIndex="2" Text="Login" ValidationGroup="Login1" />
                                                <%--input type="button"--%>
                                            </div>
                                        </div>
                                        <!-- form-group ended -->



                                    </div>
                                    <!-- form-container ended -->


                                </div>
                                <!-- login-container ended -->

                                <!--  <div class="form-footer">
                                <span class="or-con">OR</span>                                
                                <span class="title">Login with Social platforms</span>
                                
                                <ul class="list-inline">
                                 <li class="list-inline-item"><a href="javascript:void(0);" onclick="$('#btnLinkedInLogin').click();"><span class="linkedIn"></span></a></li>
                                 <li class="list-inline-item"><a href="javascript:void(0);" onclick="$('#btnGoogleLogin').click();"><span class="googlePlus"></span></a></li>
                                </ul>
                                
                                <ul class="list-inline d-none">
                                 <li class="list-inline-item"><asp:ImageButton ID="btnLinkedInLogin" ClientIdMode="Static" ImageUrl="images/log-in-linked-in.jpg" 
                                  runat="server" OnCommand="btnLinkedInLogin_Click" /></li>
                                 <li class="list-inline-item"><asp:ImageButton ID="btnGoogleLogin" ClientIdMode="Static" ImageUrl="images/google-link.jpg" 
                                  runat="server" OnCommand="btnGoogleLogin_Click" CommandArgument="https://www.google.com/accounts/o8/id" /></li>
                                </ul>              
                  
                               </div>form-footer ended -->

                            </div>
                            <!-- modal ended -->
                        </div>
                    </div>
                    <!-- Modal ended here -->

                    <p class="loginHomeTxtLink">
                        <%-- <asp:LinkButton ID="button1" runat="server" ValidationGroup="Login1" ClientIDMode="Static"
                Text="LOGIN" OnClientClick="javascript:LoginButton_Click();"></asp:LinkButton>--%>
                    </p>
                </LayoutTemplate>
            </asp:Login>
            <asp:Button ID="btnLogins" runat="server" ValidationGroup="Login1" CommandName="Login"
                ClientIDMode="Static" Style="display: none;" Text="LOGIN" OnClick="LoginButton_Click"></asp:Button>

            <p class="doyouAcc d-none">
                Do not have an account? <a href="SignUp1.aspx">Sign Up Now</a>
            </p>

        </div>
        <!-- divLogin ended -->

        <asp:HiddenField ID="hdnInFirstName" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnInLastName" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnInEmailId" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnInimgProfile" runat="server" ClientIDMode="Static" />
        <!--login pop up ends-->
    </form>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/typehead.js"></script>
    <script src="js/custom.js"></script>


    <script language="javascript" type="text/javascript">
        function getPass() {
            var pass = $('#Password').val();
            calc(pass);
        }
        function OnSuccess(response, userContext, methodName) {
            calc(response);
        }
        function replaceAll(find, replace, str) {
            return str.replace(new RegExp(find, 'g'), replace);
        }
        //function calc(strTxt1) {

        //    var strTxt = strTxt1;
        //    if (strTxt.length == 0) {

        //        document.getElementById('hdnEncpass').value = "";
        //        return;
        //    }

        //    if (strTxt.search("\r") > 0) strTxt = replaceAll("\r", "", strTxt);

        //    var strHash = hex_md5(strTxt);
        //    strHash = strHash.toUpperCase();

        //    document.getElementById('hdnEncpass').value = strHash;
        //    $('#Password').val(strHash);

        //    document.getElementById("btnLogins").click();
        // }      

        function calc(strTxt1) {

            var strTxt = strTxt1;
            if (strTxt.length == 0) {

                document.getElementById('hdnEncpass').value = "";
                return;
            }

            if (strTxt.search("\r") > 0) strTxt = replaceAll("\r", "", strTxt);

            var strHash = hex_md5(strTxt);
            strHash = strHash.toUpperCase();

            document.getElementById('hdnEncpass').value = strHash;
            $('#Password').val(strHash);

            document.getElementById("btnLogins").click();
        }

        $(document).ready(function () {

            setRemeberMeForApp();

        });

        function setRemeberMeForApp() {
            //$(document).ready(function() {
            if (getCookie('myScrlAppCookie') == 'hideRememberMe') {
                document.getElementById("contentslider").style.visibility = "hidden";
                document.getElementById("Login1_RememberMe").checked = true;
                $("#loginModal").addClass("d-block");
                $('#landing-page').addClass('d-none');
                $('.modal-close-login').addClass('d-none');
            }
            else {
                document.getElementById("contentslider").style.visibility = "visible";
            }
            //});
        }

        //setRemeberMeForApp();

        function getCookie(name) {
            var value = "; " + document.cookie;
            var parts = value.split("; " + name + "=");
            if (parts.length == 2) return parts.pop().split(";").shift();
        }

        function CloseMsg() {
            $('#divSuccess').css("display", "none");
        }
      </script>

    <script type="text/javascript" language="javascript">



        $(document).ready(function () {

            $('.carousel').carousel({
                interval: 2000,
                pause: false
            })
            $(".input-group input").focus(function () {
                $('#Login1_FailureText').hide();

            });
            checkLoginbutton();
            function checkLoginbutton() {
                if ($("#UserName").val() == '' || $("#Password").val() == '') {
                    $('#button').addClass('disabled');
                } else {
                    $('#button').removeClass('disabled');
                }
            }

            $("#UserName").on('input', checkLoginbutton);
            $("#Password").on('input', checkLoginbutton);

            $("#UserName").keypress(function (e) {
                if (e.keyCode == 13)
                    return false;
            });
            $("#Password").keypress(function (e) {
                if (e.keyCode == 13) {
                    document.getElementById("button").click();
                    return false;
                }
            });

        });
        function lnklog() {
            $("#divLogin").css("display", "block");
        }
        function lnkCloseBtns() {
            $("#divLogin").css("display", "none");
        }


        function openLoginPopup() {
            $('#loginModal').addClass('d-block')
            $('body').css('overflow', 'hidden');
        }
        function LoginButton_Click() {
            $('#FailureText').css("display", "none");
            // $('#button').css("box-shadow", "0px 0px 5px #BCBDCE");
            // $("#button").css("background", "#EB5451");
            //document.getElementById("Login1_FailureText").style.visibility = 'visible';

            getPass();
        }
      </script>
</body>
</html>
