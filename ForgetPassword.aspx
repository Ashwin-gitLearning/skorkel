<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ForgetPassword.aspx.cs" Inherits="ForgetPassword" %><%--MasterPageFile="~/BeforeLogin.master" --%>
<%@ Register TagPrefix="usc" TagName="UserControl_LandingUC" Src="~/UserControl/LandingUC.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
 <head>
 <%--<asp:Content ID="Content1" ContentPlaceHolderID="headBefore" runat="Server">--%>
 <!--   <link href="Styles/MyStyle.css" rel="stylesheet" type="text/css" />-->
    <!---NEW ADD CSS FOR FONTS AND ICONS-->
 <!--   <link rel='stylesheet'   href='http://maxcdn.bootstrapcdn.com/font-awesome/4.6.1/css/font-awesome.min.css' type='text/css' media='all' />-->
 <!--   <link href='https://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css'>-->
   
       <link href='https://fonts.googleapis.com/css?family=Quicksand:400,700,400italic' rel='stylesheet' type='text/css'>
        <link href="css/bootstrap.css" rel="stylesheet">
        <link href="css/bootstrap-reboot.css" rel="stylesheet">
        <link href="css/bootstrap-grid.css" rel="stylesheet">
        <link href="css/font-icon.css" rel="stylesheet">
        <link href="css/style.css" rel="stylesheet">
        
       <script src="js/jquery.1.12.4.min.js"></script>
       <script src="js/custom.js"></script>
        <script src="js/cookie.js"></script>
        <script>
                $(document).ready(function () {

               setRemeberMeForApp();
             
          });
        </script>
     
 <!--   <script type="text/javascript" language="javascript" src="js/jquery-1.8.2.min.js"></script>-->
 <!--   <script src="js/WaterMark.min.js" type="text/javascript"></script>-->
     <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
 <!--
 
    <script type="text/javascript">
       $(function () {
           if (document.getElementById("UserName").value == "")
               $("[id*=UserName]").WaterMark();
       
           //To change the color of Watermark
           $("[id*=Email]").WaterMark(
           {
               WaterMarkTextColor: 'Black'
           });
       });
    </script>
 -->
 <!--   <link href="css/stylever-2.css" rel="stylesheet" type="text/css" />-->
 <%--</asp:Content>--%>
 </head>
                <%--<asp:Content ID="Content2" class="forgot_password" ContentPlaceHolderID="ContentPlaceHolderBefore" runat="Server">--%>
 <body class="overflow-hidden">
  <form id="form2" runat="server">
   <usc:UserControl_LandingUC ID="UserControl1" ClientIDMode="Static" runat="server" />               
                   <!-- Modal start here -->
                              <div class="login-cover login-modal forgot-password" id="loginModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                              <div class="modal-dialog modal-dialog-centered" role="document">
                                <div class="modal-content" id="divMain" runat="server">
                                    <div class="login-container">
                                        
                                        <div class="login-header d-flex align-items-center justify-content-between">
                                            <span  class="login-logo"><img src="images/blue-logo.svg" alt="" class="img-fluid"></span>
<!--
                                            <ul class="login-nav">
                                                <li class="active"><a href="#">Login</a></li>
                                                <li><a href="#">Sign Up</a></li>
                                            </ul>
-->
                                            <a href="Landing.aspx?id=popup" class="btn btn-outline-primary mt-0">Back to Login</a>
                                            
                                        </div><!-- login-header ended -->
                                        
                                        <div class="welcome-con">
                                            <h3>Forgot Your Password ?</h3>
<!--                                            <p>Lorem ipsum dolor sit dgdconsec tetur adipiscing elit, sed do.</p>-->
                                              <img src="images/cloud-image.png" alt="cloud" class="m-t-15 img-fluid" />

                                        </div><!-- welcome-con ended -->
                                        
                                        <div class="login-form-con">
                                            
                                            <form>
                                                <div class="form-row">
                                                    
                                                    <div class="col-12 form-group ">
                                                    
                                                      <div class="input-group mb-2 search-cover">
                                                        <div class="input-group-prepend">
                                                          <div class="input-group-text"><img src="images/form-user-icon.svg"></div>
                                                        </div>
                                                        <asp:TextBox ID="UserName" placeholder="Enter your email address" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control outline-trig"></asp:TextBox>

                                                      </div>
                                                         <asp:Label ID="lblErrorMsg" class="error_msz" runat="server"></asp:Label>
                                                          <p id="pMsg" runat="server" style=" display: none">
                                                           <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                          </p>
                                                          <p id="ptext1" runat="server" style=" display: none">
                                                           Please enter your username or email address.
                                                          </p>
                                                          <p id="ptext2" runat="server" style=" display: none">
                                                           You will receive a link to create a new password via email
                                                          </p>
                                                    </div><!-- col-auto ended  -->
                                               
                                                    <div class="d-flex m-t-b-10 text-center w-100 justify-content-center">
                                                        
                                                        <asp:LinkButton ID="lnkGetPassword" Text="Continue" runat="server" ClientIDMode="Static" OnClientClick="$(this).addClass('disabled');" OnClick="lnkGetPassword_Click" class="btn btn-primary w-200"></asp:LinkButton>
                                                    </div>
                                                    
                                                </div><!-- form-group ended -->
                                            </form>
                                            
                                           
                                            
                                        </div><!-- form-container ended -->
                                        
                                       
                                        
                                    </div><!-- login-container ended -->
                                    
<!--
                                    <div class="form-footer">
                                        <span class="or-con">OR</span>
                                         
                                                <span class="title">Login with Social platforms</span>
                                                
                                                <ul class="list-inline">
                                                    <li class="list-inline-item"><a href="#"><span class="linkedIn"></span></a></li>
                                                    <li class="list-inline-item"><a href="#"><span class="googlePlus"></span></a></li>
                                                </ul>
                                            </div>
                                    
-->
                                </div><!-- modal ended -->
                                  <div id="divSuccessMess" runat="server" class=" modal-content forgot-success-modal" style="display: none;" clientidmode="Static">
                                       <div class="modal-header">
                                            <h5 class="modal-title">Success
                                            </h5>
                                        </div>
                                         <div class="modal-body">
                                           <asp:Label ID="lblSuccess" runat="server" Text="Your account details has been mailed to you."></asp:Label>
                                         </div>
                                          <div class="modal-footer border-top-0">
                                              <a clientidmode="Static" class="btn btn-outline-primary" causesvalidation="false" href="Landing.aspx">Close </a>
                                          </div>
                                       
                                    </div>
                               </div>
                           </div><!-- Modal ended here -->
                             
                    
 <!--
                    <div class="heading">
                       <div class="container">
                          <div class="">
                             Forgot Password
                          </div>
                       </div>
                    </div>
 -->
               
                    <div class="white_bg_blue">
                      <!--left container starts-->
                      <div  class="leftContainer forget_p">
                      </div>
                      <!--left container ends-->
                      <!--right container starts-->
                      <div  class="rightContainer width_f">
                         <div class="loginContainer width_h">
<!--
                            <div class="documentHeading">
                               <h1>Forgot your Password?</h1>
                            </div>
-->
                          
                         
                             
<!--
                            <div class="signup">
                               <a  href='<%= ResolveUrl("Landing.aspx") %>'>
                               Back to login</a>
                            </div>
-->
                            
                         </div>
                      </div>
                
                   </div>
               
                <%--</asp:Content>--%>
  </form>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
  <script src="js/bootstrap.min.js"></script>
  <script type="text/javascript" src="js/typehead.js"></script>
  <script src="js/custom.js"></script>
 </body>
</html>
