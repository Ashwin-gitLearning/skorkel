<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp1.aspx.cs" Inherits="SignUp1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="usc" TagName="UserControl_LandingUC" Src="~/UserControl/LandingUC.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
   <head runat="server">
      <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
      <meta name="theme-color" content="#00b6bc" />
      <link rel="shortcut icon" type="image/png" href="images/skorkel-fav-icon.png" />
      <title>Skorkel</title>
<!--
      <link href="<%=ResolveUrl("css/style.css")%>" rel="stylesheet" type="text/css" />
      <link href="<%=ResolveUrl("Styles/style.css")%>" rel="stylesheet" type="text/css" />
      <link href="<%=ResolveUrl("css/stylever-2.css")%>" rel="stylesheet" type="text/css" />
      <link href="<%=ResolveUrl("Styles/MyStyle.css")%>" rel="stylesheet" type="text/css" />
-->
<!--      <link rel="stylesheet"  href="http://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" type="text/css" media="all" />-->
 
       <link href='https://fonts.googleapis.com/css?family=Quicksand:400,700,400italic' rel='stylesheet' type='text/css'>
      
       <link href="css/bootstrap.css" rel="stylesheet">
       <link href="css/bootstrap-reboot.css" rel="stylesheet">
       <link href="css/bootstrap-grid.css" rel="stylesheet">
       <link href="css/font-icon.css" rel="stylesheet">
       <link href="css/style.css" rel="stylesheet">
       
      <script src="js/jquery.1.12.4.min.js"></script>
       <script src="js/cookie.js"></script>
      <!--<script type="text/javascript" language="javascript" src="<%=ResolveUrl("js/jquery-1.8.2.min.js")%>"></script>-->
      <script src="<%=ResolveUrl("css/placeholders.min.js")%>" type="text/javascript"></script>
    
   </head>
   <body style="overflow:hidden; position:fixed; width:100%;">
      
                <div id="globalLoader">
                    <div class="loader-cover">
                        <div class="lds-ellipsis">
                            <div></div>
                            <div></div>
                            <div></div>
                            <div></div>
                        </div>
                    </div>
                </div>
      <!--[if IE 9]>  :root .somebox{height:100%  ;} <![endif]-->
      <form id="form1" runat="server">
        <usc:UserControl_LandingUC ID="uscLanding" ClientIDMode="Static" runat="server" />
       
         <div class="outterContainer" >
            <div class="container">
            </div>
         </div>
         <!--header starts-->
         <div class="outterContainerResearch home" id="documents">
             
             
            <div class="blue_bg">
               <div class="container container_sign_up">
                  <div class="topContainer">
                     <div class="logo">
                       
                     </div>
                     <!--header ends-->
                  </div>
               </div>
            </div>
             
             
             
            <div class="container container_sign_up">
         
              
<!--
               <div class="cls">
               </div>
              
               <div class="innerContainer" id="documentContainer">
                  <div class="headingNew">
                  </div>
               </div>
               <div class="cls">
               </div>
-->
            
                
                
               <div class="innerDocumentContainerSpc">
                  <div class="innerContainer">
                     <div class="innerGroupBox position_relative">
                        <div class="innerSignUp" id="signup1">
<!--
                           <div class="signupDtl step-one">
                              <span class="digit_one">1</span><span class="sign_up_details"> Sign Up - Your Details</span>
                           </div>
-->
<!--
                           <div class="dtlforUpdateProfile step-two">
                              <span class="digit_one">2</span><span class="detials_update_sign"> Details for an updated Skorkel Profile</span>
                           </div>
                       
-->
<!--
                          
-->
                            
                                <!-- Modal start here -->
                             <div class="login-cover  login-modal" id="signUpModal" >
                              <div class="modal-dialog modal-dialog-centered" role="document">
                                <div class="modal-content">
                                    <div class="login-container">                                       
                                        <div class="login-header d-flex align-items-center mt-3 justify-content-between">
                                       
                                             <span class="login-logo">
                                               <img src="images/blue-logo.svg" alt="" class="img-fluid" />
                                            </span>
                                            
                                            <ul class="login-nav">
                                                <li><a  id="show-login-pop" href="Landing.aspx?id=popup">Login</a></li>
                                                <li class="active"><a href="javascript:void(0);">Sign Up</a></li>
                                            </ul>
                                        </div><!-- login-header ended -->
                                        
                                        <div class="welcome-con">
                                            <p class="text-center">This functionality is not currently available. <br />  Please ask your college authorities to contact us at <a target="_blank" href="mailto:contact@skorkel.com">contact@skorkel.com</a> for an institutional invite.</p>
                                           
                                            <p class="text-center d-none">Lorem ipsum dolor sit dgdconsec tetur adipiscing elit, sed do.</p>
                                        </div><!-- welcome-con ended -->
                                         <div class="submit-con text-center">
                                            <a href="Landing.aspx"><div class="modal-close-login btn btn-primary" data-dismiss="modal">Cancel</div></a>
                                         </div>
                                        <div class="login-form-con d-none">
                                            <div class="progess-con">
                                                <span class="grey-text">Step 1 of 2</span>
                                                <h4>Basic information</h4>
                                                <div class="progress">
                                                  <div class="progress-bar bg-primary" role="progressbar" style="width: 50%" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                            </div>
                                            
                                            <form>
                                                <div class="form-row">
                                                    
                                                    <div class="col-12 form-group">
                                                      <label class="sr-only" for="inlineFormInputGroup">First Name</label>
                                                      <div class="input-group mb-0 search-cover">
                                                        <div class="input-group-prepend">
                                                          <div class="input-group-text"><img src="images/form-user-icon.svg"></div>
                                                        </div>
                                                           <asp:TextBox ID="txtFirstName" class="form-control outline-trig" ClientIDMode="Static" runat="server" placeholder="First Name" maxlength="13"></asp:TextBox>


                                                      </div>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" Display="Dynamic" ValidationGroup="Registration" ErrorMessage="First name is required." ForeColor="Red" class=""></asp:RequiredFieldValidator>                                                      
                                                    </div><!-- col-auto ended  -->
                                                    
                                                      <div class="col-12 form-group">
                                                      <label class="sr-only" for="inlineFormInputGroup">Last Name</label>
                                                      <div class="input-group mb-0 search-cover">
                                                        <div class="input-group-prepend">
                                                          <div class="input-group-text"><img src="images/form-user-icon.svg"></div>
                                                        </div>
                                                        <asp:TextBox ID="txtLastName" runat="server" class="form-control outline-trig" placeholder="Last Name" maxlength="13"></asp:TextBox>


                                                      </div>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLastName"
                                    Display="Dynamic" ValidationGroup="Registration" ErrorMessage="Last name is required."
                                    ForeColor="Red" class=""></asp:RequiredFieldValidator>                                                      
                                                    </div><!-- col-auto ended  -->
                                                    
                                                      <div class="col-12 form-group">
                                                      <label class="sr-only" for="inlineFormInputGroup">Email Address</label>
                                                      <div class="input-group mb-0 search-cover">
                                                        <div class="input-group-prepend">
                                                          <div class="input-group-text"><img src="images/form-envelop-icon.svg"></div>
                                                        </div>
                                                          <asp:TextBox ID="txtUname" runat="server" class="form-control outline-trig" placeholder="Email"  ></asp:TextBox>

                                                      </div>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUname" Display="Dynamic" ValidationGroup="Registration" ErrorMessage="Email id is required." ForeColor="Red" class=""></asp:RequiredFieldValidator>
                                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid email address." ControlToValidate="txtUname" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ValidationGroup="Registration" class=""></asp:RegularExpressionValidator>                                                     
                                                    </div><!-- col-auto ended  -->
                                                    
                                                      <div class="col-12 form-group">
                                                      <label class="sr-only" for="inlineFormInputGroup">Password</label>
                                                      <div class="input-group mb-0 search-cover">
                                                        <div class="input-group-prepend">
                                                          <div class="input-group-text"><img src="images/form-lock-icon.svg"></div>
                                                        </div>
                                                          <asp:TextBox ID="txtPassword" class="form-control outline-trig" runat="server" placeholder="Password" TextMode="Password" ></asp:TextBox>

                                                       
                                                      </div>
                                                      <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" class="password_error" ControlToValidate="txtPassword" ErrorMessage="Password&nbsp;must&nbsp;be&nbsp;atleast&nbsp;6&nbsp;characters."
                                    Text="Password&nbsp;must&nbsp;be&nbsp;atleast&nbsp;6&nbsp;characters." ToolTip="New password must be atleast 6 characters." Display="Dynamic" ValidationExpression=".{6}.*" ValidationGroup="Registration" />--%>
                                                       <%--<span class="remove_margin">--%>
                                                       <asp:RegularExpressionValidator ID="Regex2" runat="server" ControlToValidate="txtPassword"
                                                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,}$"
                                                        ErrorMessage="Password must be minimum 6 characters contain at least one special symbol and one digit."
                                                        ForeColor="Red" class="" ValidationGroup="Registration"  Display="Dynamic" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword"
                                    Display="Dynamic" ValidationGroup="Registration"
                                    ErrorMessage="Password is required." ForeColor="Red" class=" float"></asp:RequiredFieldValidator>                                                        

                                                      </div><!-- col-auto ended  -->
                                                    
                                                      <div class="col-12 form-group">
                                                      <label class="sr-only" for="inlineFormInputGroup">Confirm Password</label>
                                                      <div class="input-group mb-0 search-cover">
                                                        <div class="input-group-prepend">
                                                         <div class="input-group-text"><img src="images/form-lock-icon.svg"></div>
                                                        </div>
                                                         <asp:TextBox ID="txtConPassword" class="form-control outline-trig" runat="server" TextMode="Password" placeholder="Confirm Password" 
                                                           ClientIDMode="Static" ></asp:TextBox>                                                          

                                                      </div>
                                                          <asp:CompareValidator ID="CompareValidator1" runat="server" class="" ErrorMessage="Passwords do not match." Display="Dynamic" ControlToCompare="txtPassword"  ControlToValidate="txtConPassword" ValidationGroup="Registration"></asp:CompareValidator>                                                      
                                                    </div><!-- col-auto ended  -->
                                                    
                                                    <div class="custom-checkbox signup-checkbox">
                                                     <asp:CheckBox ID="chkIAgree" runat="server" ClientIDMode="Static" Checked="false" Text=" I agree to the " /> &nbsp; <a href="TermsAndConditions.aspx" target="_blank"><strong> Terms &amp; Conditions</strong></a>

                                                        
                                                    </div>
                                                     <asp:CustomValidator ID="CustomValidator7" runat="server" ClientValidationFunction="validateTermAgree"
                                    CssClass="failureNotification" ErrorMessage="Confirm password is required."
                                    Text="Please check Terms & Conditions." ForeColor="Red"  Display="Dynamic" ValidationGroup="Registration"></asp:CustomValidator>                                                    
                                                    
                                                    <asp:Label ID="lblMsgs" class="password_color" runat="server" ></asp:Label>
                                                    
                                                    <div class="submit-con">
                                                     <a id="aCancel" href="Landing.aspx" class="btn btn-outline-primary m-r-15" onclientclick="javascript:callCancel();">
                                                      Cancel
                                                     </a>
                                                     <asp:LinkButton ID="lnlNext" runat="server" Text="Next" ClientIDMode="Static" CssClass="btn btn-primary" 
                                                      ValidationGroup="Registration" OnClientClick="javascript:CallFunc();return false;"></asp:LinkButton>
                                                     <asp:Button ID="Btnnext" runat="server" Text="Next" ClientIDMode="Static" Style="font-family: Arial;
                                                      display: none; font-size: 15px;" CssClass="nextBtn blue_bg_button" ValidationGroup="Registration"
                                                      OnClick="lnlNext_Click">
                                                     </asp:Button>
<!--                                                  <input type="submit" value="Next" class="btn btn-primary w-200" />-->
                                                    </div>
                                                    
                                                </div><!-- form-group ended -->
                                            </form>
                                            
                                           
                                            
                                        </div><!-- form-container ended -->
                                        
                                       
                                        
                                    </div><!-- login-container ended -->
                                    
                                    <!-- <div class="form-footer">
                                        <span class="or-con">OR</span>
                                         
                                                <span class="title">Login with Social platforms</span>
                                                
                                        <ul class="list-inline">
                                            <li class="list-inline-item">
                                                <asp:LinkButton ID="btnLinkedInLogin" class="btnLinkedInLogin" runat="server" OnClientClick="javascript:btnLinkedInLogin_Click();return false;"><span class="linkedIn"></span></asp:LinkButton>
                                                <asp:Button ID="btnLinkdin" runat="server" ClientIDMode="Static" OnClick="btnLinkedInLogin_Click" Style="display: none;" />

                                            </li>
                                            <li class="list-inline-item">
                                                <asp:LinkButton ID="btnGoogleLogin" runat="server" OnClientClick="javascript:btnGoogleLogin_Click();return false;" CommandArgument="https://www.googleapis.com/plus/v1/people/me/openIdConnect"><span class="googlePlus"></span></asp:LinkButton>
                                                <asp:Button ID="btnGmail" runat="server" ClientIDMode="Static" OnClick="btnGoogleLogin_Click" Style="display: none;" />
                                            </li>


                                        </ul>
                               
                                            </div>form-footer ended -->
                                    
                                </div><!-- modal ended -->
                               </div>
                           </div><!-- Modal ended here -->

                            
                            
                           <div class="signupDtl font_size dts grayBg d-none">                      
                     
                            <p class="remove_margin">
                            
                             &nbsp;
                            </p>
                            <div class="cannextBtn">                            
                            <!---->
                            </div>
                            <p>
                             &nbsp;
                            </p>
                           </div>
                        </div>
                     </div>
                   
                     <!--left verticle search list ends-->
                  </div>
               </div>
            </div>
         </div>
       
         <!--footer starts-->
         <div class="outterContainer" id="footer">
          <div class="container position_relative">
           <div class="footer_left">
              <ul>
<!--                 <li><a href="AboutUs.aspx">About Us</a></li>-->
                 <!--   <li><a href="team.aspx">Skorkel Team</a></li> -->
<!--                 <li><a href="PrivacyPolicy.aspx">Privacy Policy</a></li>-->
<!--                 <li><a href="TermsAndConditions.aspx">Terms of Service</a></li>-->
                 <!--   <li><a href="contactus.aspx">Contact Us</a></li> -->
              </ul>
<!--              <span class="">&copy; Skorkel.com</span>-->
           </div>
           <div class="footer_right">
            <span><i class="fa fa-facebook"></i></span>
            <span><i class="fa fa-twitter"></i></span>
            <span><i class="fa fa-linkedin"></i></span>
           </div>
          </div>
         </div>
         <!--footer ends-->
      </form>
   </body>
    
      <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" 
       integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous">
      </script>
      <script type="text/javascript" src="js/bootstrap.min.js"></script>
      <script type="text/javascript" src="js/typehead.js"></script>
      <script type="text/javascript" src="js/custom.js"></script>   
    
      <script type="text/javascript" language="javascript">
        



              $(document).ready(function () {

               setRemeberMeForApp();
             
          });


           function showLoader1() {
            $("#globalLoader").show();
        }

        function hideLoader1() {
            $("#globalLoader").hide();
        }
         function validateTermAgree(source, args) {
             args.IsValid = true;
             var CuuEmail = $("#chkIAgree").val();
             if ($("#chkIAgree").is(':checked'))
                 args.IsValid = true; // checked
             else
                 args.IsValid = false;  // unchecked
         }
         function InvalidEmailID() {
             alert("Email id already exist.");
         }
      </script>
      <script type="text/javascript">
         function ValidateCheckBox(sender, args) {
             if (document.getElementById("<%=chkIAgree.ClientID %>").checked == true) {
                 args.IsValid = true;
             } else {
                 args.IsValid = false;
             }
         }       
      </script>
      <script type="text/javascript">
         $(document).ready(function () {
             $("#txtUname").keydown(function () {
                 $("#lblMsgs").text('');
             });
/*             $('#lnlNext').on('click', function () {
                 if ($.browser.msie) {
                     $('input').each(function () {
                         var theAttribute = $(this).data('placeholder');
                         if (theAttribute == this.value) {// check placeholder and value
                             $(this).val('');
                             alert('Please enter value - IE');
                         }
                     });
                 }
             });*/
             
         });
         function CallFunc() {
            var validation = true;
             // $('#lnlNext').css("background", "#00A5AA");
             // $('#lnlNext').css("border", "2px solid #3EBFE5");
             if ($('#txtFirstName').val() == '' || $('#txtLastName').val() == '' || $('#txtUname').val() == ''
             || $('#txtPassword').val() == '' || $('#txtConPassword').val() == '') {
              validation = false;
             }
             if ($("#chkIAgree").is(':checked'))
             { }
             else {   // unchecked
                 validation = false;
             }
             //debugger;
             if (validation) {
              showLoader1(); 
             }
             document.getElementById("Btnnext").click();
         }
         function callCancel() {
             $('#aCancel').css("border", "2px solid #BCBDCE");
         }
         function btnLinkedInLogin_Click() {
             document.getElementById("btnLinkdin").click();
         }
         function btnGoogleLogin_Click() {
             document.getElementById("btnGmail").click();
         }
         $(document).ready(function () {
             var ie_version = getIEVersion();
             var is_ie9 = ie_version.major == 9;
             if (is_ie9 == true) {
                 document.getElementById("CompareValidator1").style.display = "none";
                 document.getElementById("CompareValidator1").style.color = "#E0E2E2";
                 $("#txtConPassword").click(function () {
                     document.getElementById("CompareValidator1").style.display = "none";
                     document.getElementById("CompareValidator1").style.color = "Red";
                 });
             }
         });
         function getIEVersion() {
             var agent = navigator.userAgent;
             var reg = /MSIE\s?(\d+)(?:\.(\d+))?/i;
             var matches = agent.match(reg);
             if (matches != null) {
                 return { major: matches[1], minor: matches[2] };
             }
             return { major: "-1", minor: "-1" };
         }
         
      </script>
   
</html>
<script type="text/javascript">
   $("#commnetBtn1").click(function () {
       $("#commentTxt1").slideToggle("slow");
       $("#commnetBtn1 img").toggle();
   
   });
   $("#commnetBtn2").click(function () {
       $("#commentTxt2").slideToggle("slow");
       $("#commnetBtn2 img").toggle();
   
   });
</script>
