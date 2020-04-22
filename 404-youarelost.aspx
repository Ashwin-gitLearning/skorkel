<%@ Page Language="C#" AutoEventWireup="true" CodeFile="404-youarelost.aspx.cs" Inherits="_404_youarelost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
   <head>

      <link rel="shortcut icon" type="image/png" href="images/skorkel-fav-icon.png" />
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
      <!-- Chrome, Firefox OS and Opera -->
      <meta name="theme-color" content="#00b6bc" />
      <!-- Windows Phone -->
      <meta name="msapplication-navbutton-color" content="#00b6bc" />
      <!-- iOS Safari -->
      <meta name="apple-mobile-web-app-status-bar-style" content="#00b6bc" />
      <!-- <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE11" /> -->
      <meta http-equiv="Content-Script-Type" content="text/javascript" />
      <title>Skorkel</title>
      <link href="<%=ResolveUrl("css/style.css")%>" rel="stylesheet" type="text/css" />
      <link href="<%=ResolveUrl("css/jquery.bxslider.css")%>" rel="stylesheet" type="text/css" />            
      <link href='https://fonts.googleapis.com/css?family=Quicksand:400,700,400italic' rel='stylesheet' type='text/css' />
      <link rel="stylesheet" href="https://cdn.linearicons.com/free/1.0.0/icon-font.min.css" />
      <link href="css/bootstrap.css" rel="stylesheet" />
      <link href="css/bootstrap-reboot.css" rel="stylesheet" />
      <link href="css/bootstrap-grid.css" rel="stylesheet" />
      <link href="css/font-icon.css" rel="stylesheet" />
      <link href="css/style.css" rel="stylesheet" />
      <link rel="stylesheet" href="css/animate.css" />      
      
   </head>
    <body>
        <form id="form2" runat="server">
        <asp:HiddenField ID="hdnEncpass" ClientIDMode="Static" runat="server" Value="" />
        <div class="container-fluid p-0 m-0 landing-page">
        <!---Top Section-->
         <section class="slider-bg page-not-found-section">
             <div class="container pt-3">
                 <!---Header Section-->  
                 <div class="row m-header">
                     <div class="col-sm-5 col-xs-5 col-5">
                         <%--<img src="images/landing-page/skorkel-logo.png" class="m-width" alt="skorkel" />--%>
                         <asp:ImageButton ID="ImageButton1" runat="server" src="images/landing-page/skorkel-logo.png" class="m-width" alt="skorkel"
                             OnClick="ImageButton1_Click" />
                     </div>
                     <div class="col-sm-7 col-xs-7 col-7 text-right">
                         <div class="btn btn-outline-primary mr-3"><a href="SignUp1.aspx">SIGN UP</a></div>
                         <div class="btn btn-primary login-popup">
                          <%--<asp:LinkButton ID="lnklogin" Text="LOGIN" runat="server" ClientIDMode="Static"  
                           OnClientClick="javascript:lnklog();return false;">
                          </asp:LinkButton>--%>
                          <a href="Landing.aspx?flag404=true">LOGIN</a>
                         </div>
                     </div>
                 </div>
             </div>
             <div class="container not-found-page">                 
                 <img src="images/404.png" alt="404 not found" />
                 <p>Sorry, the page you are looking for does not exist.</p> 
             </div>
         </section>   
        </div>        
       </form>
    </body>    
</html>

