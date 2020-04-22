<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp2.aspx.cs" Inherits="SignUp2"
   MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
   <head runat="server">
      <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
      <meta name="theme-color" content="#00b6bc" />
      <link rel="shortcut icon" type="image/x-icon" href="logoSkorkel.ico" />
        <link href='https://fonts.googleapis.com/css?family=Quicksand:400,700,400italic' rel='stylesheet' type='text/css'>
      <title>:: Skorkel ::</title>
<!--
      <link href="<%=ResolveUrl("css/style.css")%>" rel="stylesheet" type="text/css" />
      <link href="<%=ResolveUrl("Styles/style.css")%>" rel="stylesheet" type="text/css" />
      <link href="<%=ResolveUrl("css/stylever-2.css")%>" rel="stylesheet" type="text/css" />
      <link href="<%=ResolveUrl("Styles/MyStyle.css")%>" rel="stylesheet" type="text/css" />
      <link rel="stylesheet"  href="http://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" type="text/css" media="all" />
-->
<!--      <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css">-->
       
       
       
      <script language="javascript" src="js/jquery.1.12.4.min.js"></script>
       <link href="css/bootstrap.css" rel="stylesheet">
       <link href="css/bootstrap-reboot.css" rel="stylesheet">
       <link href="css/bootstrap-grid.css" rel="stylesheet">
       <link href="css/font-icon.css" rel="stylesheet">
       <link href="css/style.css" rel="stylesheet">
       
   
      <%--<script type="text/javascript" language="javascript" src="<%=ResolveUrl("js/jquery-1.8.2.min.js")%>"></script>--%>
      <script language="javascript" src="<%=ResolveUrl("css/placeholders.min.js")%>" type="text/javascript"></script>
   </head>
   <body>

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
      <form id="form1" runat="server">
         <asp:UpdatePanel ID="upanel" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
            <ContentTemplate>
               <!--header starts-->
               <div class="outterContainerResearch home" id="documents">
<!--
               <div class="blue_bg">
                  <div class="container container_sign_up">
                     <div class="topContainer">
                        <div class="logo">
                           <a  href="Landing.aspx"><img src="images/blue-logo.svg" alt="" class="img-fluid"></a>
                        </div>
                        
                     </div>
                  </div>
               </div>
-->
               <div class="container container_sign_up">
<!--
                  <div class="cls">
                     <asp:ScriptManager ID="ScriptManager1" runat="server">
                     </asp:ScriptManager>
                  </div>
-->
                   
                   
                    
                   
                  <div class="innerDocumentContainerSpc">
                     <div class="innerContainer">
                        <div class="innerGroupBox position_relative">
                           <div class="innerSignUp" id="signup2">
                               
<!--
                              <div class="signupDtl step-two">
                                 <span class="digit_one">1</span><span  class="sign_up_details"> Sign Up - Your Details</span>
                              </div>
                              <div class="dtlforUpdateProfile  step-one">
                                 <span class="digit_one">2</span><span sclass="detials_update_sign"> Details for an updated Skorkel Profile</span>
                              </div>
                               
                             
                              <div class="signupDtl dts rightImg">
                                 <div class="signUp2Steps">
                                    <p class="two" >
                                       Sign up in 2 steps
                                    </p>
                                 </div>
                              </div>
                               
-->                           
                               
                                
                             <div class="login-cover login-modal" id="signUpModal" runat="server" clientidmode="Static"  tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                              <div class="modal-dialog modal-dialog-centered" role="document">
                                <div class="modal-content">
                                    <div class="login-container">
                                        
                                        <div class="login-header d-flex align-items-center justify-content-between">
                                             <span class="login-logo">
                                               <img src="images/blue-logo.svg" alt="" class="img-fluid" />
                                            </span>
                                            <ul class="login-nav">
                                                <li><a href="Landing.aspx">Login</a></li>
                                                <li class="active"><a href="javascript:void(0);">Sign Up</a></li>
                                            </ul>
                                        </div><!-- login-header ended -->
                                        
                                        <div class="welcome-con">
                                            <h3>Welcome to Skorkel</h3>
                                            <p class="text-center">Lorem ipsum dolor sit dgdconsec tetur adipiscing elit, sed do.</p>
                                        </div><!-- welcome-con ended -->
                                        
                                        <div class="login-form-con">
                                            <div class="progess-con">
                                                <span class="grey-text">Step 2 of 2</span>
                                                <h4>Educational Details</h4>
                                                <div class="progress">
                                                  <div class="progress-bar bg-primary" role="progressbar" style="width: 100%" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100"></div>
                                                </div>
                                            </div>
                                            
                                            
                                                <div class="form-row">
                                                    
                                                      <div class="col-12 form-group">
<!--                                                      <label class="sr-only" for="inlineFormInputGroup">First Name</label>-->
                                                      <div class="input-group mb-2 search-cover">
                                                        <div class="input-group-prepend">
                                                          <div class="input-group-text"><img src="images/lnr-apartment.svg"></div>
                                                        </div>
                                                          <asp:TextBox ID="txtInstituteName" runat="server" autocomplete="off" placeholder="Institute name" class="form-control outline-trig"></asp:TextBox>
                                    <ajax:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                       CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtInstituteName"
                                       ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" CompletionListCssClass="AutoCompletionList" CompletionListItemCssClass="AutoComp_listItem" CompletionListHighlightedItemCssClass="AutoComp_itemHighlighted">
                                    </ajax:AutoCompleteExtender>
                                                          

                                                      </div>
                                                    </div><!-- col-auto ended  -->
                                                    
                                                      <div class="col-12 form-group">
<!--                                                      <label class="sr-only" for="inlineFormInputGroup">Last Name</label>-->
                                                      <div class="input-group mb-2 search-cover">
                                                        <div class="input-group-prepend">
                                                          <div class="input-group-text"><img src="images/lnr-graduation-hat.svg"></div>
                                                        </div>
                                                             <asp:TextBox ID="txtDegree" runat="server" autocomplete="off" placeholder="Degree" class="form-control outline-trig" ></asp:TextBox>
                                    <ajax:AutoCompleteExtender ServiceMethod="GetDegreeList" MinimumPrefixLength="1"
                                       CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtDegree"
                                       ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false" CompletionListCssClass="AutoCompletionList" CompletionListItemCssClass="AutoComp_listItem" CompletionListHighlightedItemCssClass="AutoComp_itemHighlighted">
                                    </ajax:AutoCompleteExtender>

                                                      </div>
                                                    </div><!-- col-auto ended  -->
                                                    <asp:UpdatePanel ID="updates" class="col-sm-12 no-padding" runat="server">
                                                       <ContentTemplate>
                                                    <div class="form-group">
                                                     
                                                      
                                                           <div class="row">
                                                        <div class="col-md-6">
                                                                                                                 
                                                          <select name="select" id="fromMonth" runat="server" class="form-control">
                                                           <option>Month</option>
                                                           <option>Jan</option>
                                                           <option>Feb</option>
                                                           <option>Mar</option>
                                                           <option>Apr</option>
                                                           <option>May</option>
                                                           <option>Jun</option>
                                                           <option>Jul</option>
                                                           <option>Aug</option>
                                                           <option>Sep</option>
                                                           <option>Oct</option>
                                                           <option>Nov</option>
                                                           <option>Dec</option>
                                                          </select>
                                                          </div>
                                                          <div class="col-md-6">
                                                           <asp:DropDownList ID="txtFromYear" runat="server" CssClass="form-control signInputY" ClientIDMode="Static">
                                                           </asp:DropDownList>
                                                          </div>
                                                          <div class="col-sm-12 text-center">
                                                           <span class="d-block m-t-b-10">To</span>
                                                          </div>
                                                          <div class="col-sm-6">
                                                           <asp:DropDownList ID="toMonth" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                            <asp:ListItem Selected="True">Month</asp:ListItem>
                                                            <asp:ListItem>Jan</asp:ListItem>
                                                            <asp:ListItem>Feb</asp:ListItem>
                                                            <asp:ListItem>Mar</asp:ListItem>
                                                            <asp:ListItem>Apr</asp:ListItem>
                                                            <asp:ListItem>May</asp:ListItem>
                                                            <asp:ListItem>Jun</asp:ListItem>
                                                            <asp:ListItem>Jul</asp:ListItem>
                                                            <asp:ListItem>Aug</asp:ListItem>
                                                            <asp:ListItem>Sep</asp:ListItem>
                                                            <asp:ListItem>Oct</asp:ListItem>
                                                            <asp:ListItem>Nov</asp:ListItem>
                                                            <asp:ListItem>Dec</asp:ListItem>
                                                           </asp:DropDownList>
                                                          </div>
                                                          <div class="col-sm-6">
                                                           <asp:DropDownList ID="txtToYear" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                           </asp:DropDownList>
                                                          </div>
                                                          
                                                        </div>
                                                    </div><!-- col-auto ended  -->
                                                    
                                                   <div class="custom-checkbox m-b-15 m-t-15">
                                                    <asp:CheckBox ID="chkAtPresent" runat="server" ClientIDMode="Static" AutoPostBack="true"
                                                     Text=" Currently studying" OnCheckedChanged="chkAtPresent_CheckedChanged" />
                                                  <%--  <asp:Button ID="BtnCheckbox" runat="server" ClientIDMode="Static" Style="display: none" 
                                                     OnClick="chkAtPresent_CheckedChanged" />--%>
                                                   </div>
                                                    </ContentTemplate>
                                    <Triggers>
                                      <%-- <asp:AsyncPostBackTrigger ControlID="BtnCheckbox" />--%>
                                       <asp:AsyncPostBackTrigger ControlID="chkAtPresent" />
                                    </Triggers>
                                 </asp:UpdatePanel> 
                                                   <div class="col-12 form-group">
                                                    <textarea id="txtDescription" autocomplete="off" maxlength="500" runat="server" class="form-control" placeholder="Description" />
                                                   </div><!-- col-auto ended  -->
                                                    
                                                   <div class="col-12 form-group">
                                                    <div class="input-group mb-2 search-cover">
                                                     <div class="input-group-prepend">
                                                      <div class="input-group-text"><img src="images/lnr-license.svg"></div>
                                                     </div>
                                                       <asp:TextBox ID="txtAreaExpert" autocomplete="off" runat="server" placeholder="Add your area of expertise"
                                                       class="form-control outline-trig" maxlength="500" ClientIDMode="Static"></asp:TextBox>
                                                      </div>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAreaExpert"
                                                       Display="Dynamic" ValidationGroup="Regist" ErrorMessage="Area of expertise is required."
                                                       Style="font-size: 15px; margin-top: 2px;" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div><!-- col-auto ended  -->                                                   
                                                    
                                                    
                                                    <div class="submit-con m-t-b-0">


                                                        
                                                            <asp:LinkButton runat="server" ID="lnkSkipbtn" Text="Skip" CssClass="btn btn-outline-primary m-r-15" ValidationGroup="Registration"
                                                               ClientIDMode="Static" OnClientClick="javascript:lnlSkip_Click();return false;"></asp:LinkButton>
                                                         
                                                            <asp:LinkButton runat="server" ID="lnlNext" Text="Finish" CssClass="btn btn-primary" ValidationGroup="Registration"
                                                               ClientIDMode="Static" OnClientClick="javascript:lnlNext_Click();return false;"></asp:LinkButton>
                                                            <asp:Button runat="server" onclientclick="showLoader1()" ID="BtnSkip" Text="Skip" Style="display: none;" ValidationGroup="Registration"
                                                               ClientIDMode="Static" OnClick="lnlSkip_Click"></asp:Button>
                                                       
                                                            <asp:Button runat="server" onclientclick="showLoader1()" ID="BtnFinish" Text="Finish" Style="display: none;" ValidationGroup="Registration"
                                                               ClientIDMode="Static" OnClick="lnlNext_Click"></asp:Button>
                                                      
                                                        
                                                         <div class="cannextBtn">
                                                            <asp:LinkButton runat="server" ID="lnkSkipbtn2" Text="Skip" CssClass="cancelBtn"
                                                               ValidationGroup="Registration" Visible="false" OnClientClick="return false"></asp:LinkButton>
                                                           
                                                            <asp:LinkButton runat="server" ID="lnlNext2" Text="Finish" CssClass="nextBtn" ValidationGroup="Registration"
                                                               Visible="false" OnClientClick="return false"></asp:LinkButton>
                                                         </div>
                                                    </div>
                                                    
                                                </div><!-- form-group ended -->
                                                                                                                                
                                            
                                        </div><!-- form-container ended -->                                       
                                       
                                        
                                    </div><!-- login-container ended -->
                                    
                                    <%--<div class="form-footer">
                                        <span class="or-con">OR</span>
                                         
                                        <span class="title">Login with Social platforms</span>
                                                
                                        <ul class="list-inline">
                                         <li class="list-inline-item"><a href="#"><span class="linkedIn"></span></a></li>
                                         <li class="list-inline-item"><a href="#"><span class="googlePlus"></span></a></li>
                                        </ul>
                                       </div><!-- form-footer ended -->--%>
                                    
                                </div><!-- modal ended -->
                               </div>
                           </div><!-- Modal ended here -->

                   
                               
                              <div class="signupDtl dts grayBg">
                          
                                 <p>
                                    
                                    <asp:Button ID="btnAreaExpSave" runat="server" ClientIDMode="Static" Style="display: none;"
                                       Text="Add" OnClick="btnAreaExpSave_Click" ValidationGroup="Regist" />
                                 </p>
                                 <div class="signup2_inline">
                                    <asp:ListView ID="lstAreaExpert" runat="server" OnItemCommand="lstAreaExpert_ItemCommand"
                                       GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                                       <GroupTemplate>
                                          <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                       </GroupTemplate>
                                       <ItemTemplate>
                                          <asp:HiddenField ID="hdnintUserSkillId" runat="server" Value='<%#Eval("intUserSkillId")%>' />
                                          <div>
                                             <span class="firstTxt">
                                             <%#Eval("strSkillName")%>
                                             </span>
                                             <span class="secondTxt">
                                                <asp:ImageButton ID="lnkDelete" runat="server" CommandName="DeleteExp" ImageUrl="images/close.png">
                                                </asp:ImageButton>
                                             </span>
                                          </div>
                                       </ItemTemplate>
                                    </asp:ListView>
                                 </div>
                                 
                                 
                              </div>
                           </div>
                        
                           <!--left verticle search list ends-->
                        </div>
                     </div>
                  </div>
               </div>
              
               <!--footer starts-->
                   
<!--
               <div class="outterContainer" id="footer">
                  <div class="container">
                     <div class="footer_left">
                        <ul>
                           <li><a href="AboutUs.aspx">About Us</a></li>
                             <li><a href="#">Skorkel Team</a></li> 
                           <li><a href="TermsAndConditions.aspx">Privacy Policy</a></li>
                           <li><a href="#">Terms of Service</a></li>
                            <li><a href="#">Contact</a></li> 
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
-->
                   
               <div class=" modal_bg" id="divWelcome" runat="server" style="display: none;">
                   
                            <div class="login-modal global-popup login-success-popup">
                                <div class="modal-dialog modal-dialog-centered" role="document">
                                    <div class="modal-content">
                                        <div class="login-container text-center">

                                            <div class="welcome-con">
                                                 <span class="add-icon">
                                                     <img src="images/connected.svg" style="height:40px;">
                                                 </span>
                                                
                                                <h3>Welcome to Skorkel</h3>
                                            </div>
                                            <!-- welcome-con ended -->

                                            <p class="text-center">
                                                A confirmation email has been sent to
                                                <asp:Label ID="lblEmail" runat="server" class="brand-green-color"></asp:Label>
                                                .
                                                <br>
                                                Please click on the confirmation link in the email to activate your account.
                                            </p>

                                            <div class="text-center">
                                                <a href="Landing.aspx" id="aProceed" onclick="CallProceed()" class="btn btn-primary">Proceed</a>
                                            </div>

                                            <!-- form-container ended -->
                                        </div>
                                    </div>
                                </div>
                           </div><!-- modal ended -->                
<!--
                  <div class="grayBoxSign">
                     <div class="wlcBox">
                        <img src="images/ltr.jpg" class="ltr" />
                        <div class="cls">
                        </div>
                        <p class="wlc">
                           WELCOME TO SKORKEL!
                        </p>
                     
                     </div>
                     <div class="wlcBox">
                      <center>
                    
                      </center>
                     </div>
                  </div>
-->
                
               </div>
          
          
                     
          
               <asp:UpdateProgress ID="updateProgress" runat="server">
                <ProgressTemplate>
                 <div class="img_update_progress">
                  <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/loadingImage.gif"
                   AlternateText="Loading ..." ToolTip="Loading ..." class="divProgress sign_up_login" />
                 </div>
                </ProgressTemplate>
               </asp:UpdateProgress>
            </ContentTemplate>
            <Triggers>
               <asp:AsyncPostBackTrigger ControlID="btnAreaExpSave" EventName="Click" />
               <asp:AsyncPostBackTrigger ControlID="lstAreaExpert" />
               <asp:AsyncPostBackTrigger ControlID="txtAreaExpert" />
            </Triggers>
         </asp:UpdatePanel>
      </form>
   </body>

      <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"
       integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
      <script type="text/javascript" src="js/bootstrap.min.js"></script>
      <script type="text/javascript" src="js/typehead.js"></script>
      <script type="text/javascript" src="js/custom.js"></script>
    
      <script type="text/javascript">
             function showLoader1() {
            $("#globalLoader").show();
        }

        function hideLoader1() {
            $("#globalLoader").hide();
        }
         $(document).ready(function () {
            // $('#txtFromYear').find('option[value=2016], option[value=2025]').remove();
             //$('#txtToYear').find('option[value=2025]').remove();
             $("#txtAreaExpert").keydown(function (e) {
                 if (e.keyCode == 13) {
                     debugger;
                     document.getElementById("btnAreaExpSave").click();
                     return false;
                 }
             });
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_endRequest(function () {
                 //$('#txtFromYear').find('option[value=2016], option[value=2025]').remove();
                 //$('#txtToYear').find('option[value=2025]').remove();
                 var v = "";
                 $("#txtAreaExpert").keydown(function (e) {
                     if (e.keyCode == 13) {
                         document.getElementById("btnAreaExpSave").click();
                         return false;
                     }
                 });
             });
         });
      </script>
      <script type="text/javascript">
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_beginRequest(beginRequest);
         function beginRequest() {
             prm._scrollPosition = null;
         }
      </script>
      <script type="text/javascript">
         var xPos, yPos;
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_beginRequest(BeginRequestHandler);
         prm.add_endRequest(EndRequestHandler);
         function BeginRequestHandler() {
             yPos = $get('#upanel').scrollTop;
             alert(yPos);
         }
         function EndRequestHandler() {
             $get('#upanel').scrollTop = yPos;
         }
         function fnAddElement() {
             BeginRequestHandler();
             EndRequestHandler();
         }
         function callClose() {
             $('#Button1').click();
         }
         
         function lnlSkip_Click() {
             $('#lnkSkipbtn').css("box-shadow", "0px 0px 10px #BCBDCE");
             $('#lnlNext').css("border", "1px solid #3EBFE5");
             document.getElementById("BtnSkip").click();
         }
         function lnlNext_Click() {
             $('#lnkSkipbtn').css("border", "1px solid #BCBDCE");
             $('#lnlNext').css("background", "#00A5AA");
             $('#lnlNext').css("box-shadow", "0px 0px 2px #00B7E5");
             document.getElementById("BtnFinish").click();
         }
         function changeCheckboxText(checkbox) {
             if (checkbox.checked)
                 document.getElementById("BtnCheckbox").click();
             else
                 document.getElementById("BtnCheckbox").click();
         }
         function CallProceed() {
             $('#aProceed').css("background", "#00A5AA");
             $('#aProceed').css("box-shadow", "0px 0px 2px #00B7E5");
         }
      </script>
 
</html>
