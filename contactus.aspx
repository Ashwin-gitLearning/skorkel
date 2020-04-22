<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="AboutUs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" type="image/x-icon" href="logoSkorkel.ico" />
    <title>:: Skorkel ::</title>
    <link href="<%=ResolveUrl("css/style.css")%>" rel="stylesheet" type="text/css" />
           <link href="<%=ResolveUrl("Styles/style.css")%>" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("css/stylever-2.css")%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="<%=ResolveUrl("js/jquery-1.8.2.min.js")%>"></script>
    <script src="<%=ResolveUrl("css/placeholders.min.js")%>" type="text/javascript"></script>

                 <!---NEW ADD CSS FOR FONTS AND ICONS-->
    <link rel='stylesheet'   href='http://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css' type='text/css' media='all' />
    <link href='https://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css'>
     <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />


    <link href='https://fonts.googleapis.com/css?family=Quicksand:400,700,400italic' rel='stylesheet' type='text/css'>

</head>
<body>
    <form id="form1" runat="server">
 
    <!--header starts-->
    <div class="outterContainerResearch home" id="documents">
         <div class="blue_bg">
            <div class="container">
               <div class="topContainer">
                    <div class="logo">
                        <a href="<%=ResolveUrl("Home.aspx")%>">
                            <img src="images/logo-inner.png" alt="Skorkel" /> <span> SKORKEL </span>
                        </a>
                    </div>
                
                 
                    <!--header ends-->
                </div> 
            </div>      
        </div> 
        <div class="container">
            <!--heading ends-->
            <div class="cls">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </div>
            <!--inner container starts-->
            <div class="innerContainer" id="documentContainer">
                <div class="headingNew">
                </div>
            </div>
            <div class="cls">
            </div>
            <!--inner container ends-->
            <div class="innerDocumentContainerSpc">
                <div class="innerContainer">
                    <div class="innerGroupBox">
                        <div class="paragraph_text height_about">
                           
                               <h1> Contact Us</h1>
                           <iframe src="https://docs.google.com/forms/d/1DKmOCMr09IjzBZqYwN7x0WqjE5PbuAB8lxkabcs-NPY/viewform?embedded=true" width="100%" height="1000" frameborder="0" marginheight="0" marginwidth="0" style="background:none;">Loading...</iframe>
                        </div>
                        <div class="cls">
                        </div>
                        <!--left verticle search list ends-->
                    </div>
                </div>
            </div>
        </div>
        <div class="cls">
        </div>
      <!--footer starts-->
            <div class="outterContainer" id="footer">
               <div class="container position_relative">
                      <div class="footer_left">
                          <ul>
                            <li><a href="AboutUs.aspx" target="_blank">About Us</a></li>
                            <li><a href="team.aspx" target="_blank">Skorkel Team</a></li>
                            <li><a href="PrivacyPolicy.aspx" target="_blank">Privacy Policy</a></li>
                            <li><a href="TermsAndConditions.aspx" target="_blank">Terms of Service</a></li>
                            <li><a href="http://goo.gl/forms/rvlKhZCAPJY2d2t73" target="_blank">Contact Us</a></li>
                           
                          </ul>
                          <span class="">&copy; Skorkel.com</span>
                      </div>

                    <div class="footer_right">
                       <span><i class="fa fa-facebook"></i></span>
                       <span><i class="fa fa-twitter"></i></span>
                       <span><i class="fa fa-linkedin"></i></span>
                    </div>
                </div> 
             
            </div>
            <!--footer ends-->
        <div class="grayBoxSign" id="divWelcome" runat="server" style="display: none;">
            <div class="wlcBox">
                <img src="images/ltr.jpg" class="ltr" />
                <div class="cls">
                </div>
                <p class="wlc">
                    WELCOME TO SKORKEL!</p>
                <p>
                    A confirmation email has been sent to
                    <asp:Label ID="lblEmail" runat="server" Style="color: #3ebfc5"></asp:Label>. </br>
                    Please click on the confirmation link in the email to activate your account.</p>
            </div>
            <hr />
            <div class="wlcBox">
                <center>
                    <a href="Landing.aspx" id="aProceed" onclick="CallProceed()" class="nextBtn">Proceed</a></center>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
