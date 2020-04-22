<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SuperAdminLanding.aspx.cs" Inherits="SuperAdminLanding" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link rel="shortcut icon" type="image/x-icon" href="logoSkorkel.ico" />
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
 <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" />
 <meta http-equiv="Content-Script-Type" content="text/javascript" />
 <title>:: Skorkel Super Administrator ::</title>
 <link href="<%=ResolveUrl("css/style.css")%>" rel="stylesheet" type="text/css" />
 <link href="<%=ResolveUrl("css/jquery.bxslider.css")%>" rel="stylesheet" type="text/css" />

 <link rel="shortcut icon" type="image/png" href="images/skorkel-fav-icon.png" />

      <link rel="stylesheet" href="https://cdn.linearicons.com/free/1.0.0/icon-font.min.css" />
      <link href="css/bootstrap.css" rel="stylesheet" />
      <link href="css/bootstrap-reboot.css" rel="stylesheet" />
      <link href="css/bootstrap-grid.css" rel="stylesheet" />
      <link href="css/font-icon.css" rel="stylesheet" />
      <link href="css/style.css" rel="stylesheet" />
       
      <script src="js/jquery.1.12.4.min.js" type="text/javascript"></script>
      <script type="text/javascript">

       (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
       (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
       m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
       })(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

       ga('create', 'UA-77804115-1', 'auto');
       ga('send', 'pageview');

      </script>

      <script src="<%=ResolveUrl("js/hex_md5.js")%>" type="text/javascript"></script>
      <!---NEW ADD CSS FOR FONTS AND ICONS-->
      <link id='fontawesomecss' href='http://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css' rel='stylesheet' type='text/css' media='all' />
     <link href='https://fonts.googleapis.com/css?family=Quicksand:400,700,400italic' rel='stylesheet' type='text/css'>
   
      <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
</head>
<body>
 <form id="form1" runat="server">
  <asp:HiddenField ID="hdnEncpass" ClientIDMode="Static" runat="server" Value="" />
<!--   <div class="main_page_home landingpage_home">
   <div class="innerContainer">
    <div class="logo">
     <a href="#">
      <img src="images/logo-inner.png" alt="Skorkel"><span class="landing_logo"> SKORKEL </span>
     </a>  
    </div>
    <div class="navigationInner">
     <ul class="landingMenu">
      <%--<li><a href="SignUp1.aspx" >SIGN UP</a></li>--%>
      <li>
       <asp:LinkButton ID="lnklogin" Text="LOGIN" runat="server" 
        ClientIDMode="Static" OnClientClick="javascript:lnklog();return false;"></asp:LinkButton>
      </li>
     </ul>
    </div>
   </div>
   <div class="cls"></div>
  </div> -->

  <!--login pop up starts-->
<div class="wrapper superadmin-loging-screen d-flex align-items-center justify-content-center">        
  <div id="divLogin" runat="server" class="super-admin" ClientIDMode="Static" style="display: none">

     <p class="closePopIconH d-none">
      <asp:LinkButton ID="lnkCloseBtn" runat="server" ClientIDMode="Static" OnClientClick="javascript:lnkCloseBtns();return false;">
       <img src="images/close-pop.jpg" alt="" />
      </asp:LinkButton>                
     </p>

     <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" BorderStyle="None" RememberMeSet="false" Width="100%">
      <LayoutTemplate>
       <!-- Modal start here -->
        <div class="login-modal d-block">
         <div class="card border-0" >

           <div class="login-container">
            <div class="login-header d-flex align-items-center justify-content-between">
             <span class="login-logo">
              <img src="images/skorel-color-logo.png" alt="" class="img-fluid" />
             </span>
            </div><!-- login-header ended -->

            <div class="welcome-con">
             <h3>Welcome to Super Admin</h3>
            <!--   -->
            </div><!-- welcome-con ended -->

            <div class="login-form-con">
             <form>
              <div class="form-row">
               <div class="col-12 form-group ">
                <label class="sr-only" for="inlineFormInputGroup">User name or email</label>
                <div class="input-group mb-2 search-cover">
                 <div class="input-group-prepend">
                 <div class="input-group-text"><img src="images/form-user-icon.svg"></div>
                 </div>
                 <asp:TextBox ID="UserName" placeholder="Username or email" ClientIDMode="Static" runat="server"
                  CssClass="form-control outline-trig"></asp:TextBox>                
                </div>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ValidationGroup="Login1"
                 Display="Dynamic" ToolTip="Please enter valid user name." CssClass=""
                 ErrorMessage="Please enter valid user name." ControlToValidate="UserName">
                </asp:RequiredFieldValidator>
                
               </div><!-- col-auto ended  -->

               <div class="col-12 form-group ">
                <label class="sr-only" for="inlineFormInputGroup">Password</label>
                <div class="input-group mb-2 search-cover">
                 <div class="input-group-prepend">
                  <div class="input-group-text"><img src="images/form-lock-icon.svg"></div>
                 </div>
                 <asp:TextBox ID="Password" runat="server" TextMode="Password" ClientIDMode="Static"
                  placeholder="Password" CssClass="form-control outline-trig">
                 </asp:TextBox>
                 <div class="clearfix"></div>                  
                </div>

                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ValidationGroup="Login1" ToolTip="Password is required."
                 Display="Dynamic" CssClass="" ErrorMessage="Please enter valid password." ControlToValidate="Password">
                </asp:RequiredFieldValidator>                  
                <asp:Label Style="color: red; float: left;" ID="FailureText" runat="server" EnableViewState="False"></asp:Label>
                  
               </div><!-- col-auto ended  -->

               <div class="submit-con text-center">
                <input id="button" type="button" class="btn btn-primary w-200" onclick="LoginButton_Click();"
                 value="Login" validationgroup="Login1" font-bold="True" tabindex="2" />                                             
               </div>
              </div><!-- form-group ended -->
             </form>
            </div>
           </div><!-- login-container ended -->
      
         </div>
        </div>
       <!-- Modal ended here -->
      </LayoutTemplate>
     </asp:Login>
     <asp:Button ID="btnLogins" runat="server" ValidationGroup="Login1" CommandName="Login"
      ClientIDMode="Static" Style="display: none;" Text="LOGIN" OnClick="LoginButton_Click">
     </asp:Button>

  </div><!-- divLogin ended -->
</div>  

  <asp:HiddenField ID="hdnInFirstName" runat="server" ClientIDMode="Static" />
  <asp:HiddenField ID="hdnInLastName" runat="server" ClientIDMode="Static" />
  <asp:HiddenField ID="hdnInEmailId" runat="server" ClientIDMode="Static" />
  <asp:HiddenField ID="hdnInimgProfile" runat="server" ClientIDMode="Static" />
 </form>

 <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" 
  integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
 <script type="text/javascript" src="js/bootstrap.min.js"></script>
 <script type="text/javascript" src="js/typehead.js"></script>
 <script type="text/javascript" src="js/custom.js"></script> 

 <script lang="javascript" type="text/javascript">
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
 </script>

 <script type="text/javascript" lang="javascript">
     $(document).ready(function () {
         
        function checkLoginbutton() {
            if ($("#UserName").val() == '' || $("#Password").val() == '') {
                $('#button').addClass('disabled');
            } else {
                $('#button').removeClass('disabled');
            }
        }
        checkLoginbutton();

       $("#UserName").on('input', checkLoginbutton);
       $("#Password").on('input',checkLoginbutton);

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
     function LoginButton_Click() {
         getPass();
     }
 </script>
</body>
</html>
