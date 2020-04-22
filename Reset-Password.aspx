<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
   CodeFile="Reset-Password.aspx.cs" Inherits="Reset_Password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
   <!--   <link href="<%=ResolveUrl("css/stylever-2.css")%>" rel="stylesheet" type="text/css" />-->
   <script type="text/javascript">
       function showSuccessPopupRstPwd(title, msg) {
           $('#successPopupTitle').text(title);
           $("#successPopupMsg").text(msg);
           $("#successPopupModalRstPwd").show();
       }
       function hideSuccessPopupRstPwd() {
           $("#successPopupModalRstPwd").hide();
           document.getElementById("lnkOkButton").click();
           //window.location.reload()
       }
    </script>
   <!------------------------ Modal start here  ------------------------->
   <div id="successPopupModalRstPwd" class="modal backgroundoverlay show" role="dialog" aria-labelledby="confirmationTitle">
       <div class="modal-dialog modal-dialog-centered" role="document">
           <div class="modal-content">

               <div class="modal-header">
                   <h5 id="successPopupTitle" class="modal-title">Confirmation</h5>
               </div>
               <div class="modal-body">
                   <span id="successPopupMsg">Are you really want to join as friend ?</span>
               </div>
               <div class="border-top-0 text-right padding-15">
                   <a class="btn btn-primary add-scroller" onclick="hideSuccessPopupRstPwd();" data-dismiss="modal" href="#">Ok</a>
               </div>
           </div>
           <!--- modal-content ended -->
       </div>
   </div>
   <!------------------------ Modal Ended here  ------------------------->
   <script src="<%=ResolveUrl("js/hex_md5.js")%>" type="text/javascript"></script>
   <script type="text/javascript">
      $(function () {
          if ($("#lblNotifyCount").text() == '0') {
              document.getElementById("divNotification1").style.display = "none";
          }
          if ($("#lblInboxCount").text() == '0') {
              document.getElementById("divInbox").style.display = "none";
          } 
      });   
   </script>
   <script type="text/javascript">
       $(document).ready(function () {
           //$('#lnkResetPassword').focus();
           $("#txtConfPassword").keydown(function (e) {
               if (e.keyCode == 13) {
                   $('#btnsave').focus();
                   $('#btnsave').trigger('click');
               }
           });
           $("#txtConfPassword").css('margin-top', '-15');    //margin-top: -15px;
           $("#txtNewPassword").focusout(function () {
               if ($('#ctl00_ContentPlaceHolder1_Regex2').is(":hidden")) {
                   $("#txtConfPassword").css('margin-top', '-15');
               } else {
                   $("#txtConfPassword").css('margin-top', '0');
               }
           });
       });
       function calculatePass() {
           var strTxt = $('#txtNewPassword').val();
           var strHash = hex_md5(strTxt);
           strHash = strHash.toUpperCase();
           document.getElementById('hdnEncpass').value = strHash;
           //$('#txtNewPassword').val(strHash);
       }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" ID="up"><ContentTemplate>
        <asp:Button ID="lnkOkButton" runat="server" class="d-none" Text="Ok" ClientIDMode="Static" OnClick="lnkOkButton_Click"></asp:Button>
            <div class="main-section-inner">
                    <div class="panel-cover clearfix">
                        <div class="full-width-con">
                            <div class="card padding-15">
                            <div class="row">
                                <div class="col-sm-6 text-center d-none d-sm-block">
                                  <img src="images/cloud-image.png" alt="cloud" class="img-fluid" >
                                </div><!-- col-sm ended -->
                                <div class="col-sm-6 reset-con">
                                   <div  class="rightContainer reset_right">
                  
                                  <div class="form-group">
                                    <h5>Change Your Password</h5>    
                                  </div><!-- form-group ended -->
                 
                                 <div class="form-group">
                                     <label>Old Password</label>
                                     <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" placeholder="Old Password"
                     ClientIDMode="Static" CssClass="loginPasswordField form-control"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ClientIDMode="Static"
                     ValidationGroup="Login1" Display="Dynamic" ToolTip="Old password required." CssClass="errorTxt"
                     ErrorMessage="Please&nbsp;enter&nbsp;valid&nbsp;old&nbsp;password." ControlToValidate="txtOldPassword"></asp:RequiredFieldValidator>
                                 </div><!-- form-group ended -->       
                  
                                  <div class="form-group">
                                   <label>New Password</label>
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" placeholder="New Password"
                     ClientIDMode="Static" MaxLength="15" CssClass="loginPasswordField form-control"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Login1"
                     Display="Dynamic" ToolTip="New password required." CssClass="errorTxt" ErrorMessage="Please&nbsp;enter&nbsp;valid&nbsp;new&nbsp;password."
                     ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="Regex2" Display="Dynamic" runat="server" ControlToValidate="txtNewPassword" Font-Bold="false"
                     ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,}$" Font-Size="Small" 
                     ErrorMessage="Password must be minimum 6 characters contain at least one special symbol and one digit." 
                     ForeColor="Red" ValidationGroup="Login1" />  
                                  </div><!-- form-group ended -->      
                               <div class="form-group">
                               <label>Confirm New Password</label>
                     <asp:TextBox ID="txtConfPassword" runat="server" TextMode="Password"  placeholder="Confirm New Password"
                     ClientIDMode="Static" MaxLength="15" class="" CssClass="loginPasswordField form-control"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Login1"
                     Display="Dynamic" ToolTip="Confirm new password required." CssClass="errorTxt"
                     ErrorMessage="Please&nbsp;enter&nbsp;valid&nbsp;confirm&nbsp;new&nbsp;password."
                     ControlToValidate="txtConfPassword"></asp:RequiredFieldValidator>
                  <asp:CompareValidator ValidationGroup="Login1" ID="CompareValidator1" runat="server"
                     ControlToCompare="txtNewPassword" ClientIDMode="Static" ControlToValidate="txtConfPassword"
                     Display="Dynamic" ErrorMessage="Password&nbsp;does&nbsp;not&nbsp;matched"></asp:CompareValidator>
                                </div><!-- form-group ended -->
                  <asp:Label ID="lblMessage" ForeColor="Red" runat="server"></asp:Label>
                  <asp:Label ID="lblPwdLenMsg" ClientIDMode="Static" ForeColor="Red" runat="server"></asp:Label>
                          <div class="form-group password-hint">
                          <p><strong>Hint:</strong> Passwords are case-sensitive and must be at least 6 characters. A good password should contain a mix of capital and lower-case letters, numbers and symbols.</p>
                          </div><!-- form-group ended -->
                   
                            <div class="submit-con">  
                               <asp:LinkButton ID="lnkResetPassword" Text="Reset Password" Font-Bold="false" runat="server"
                        ClientIDMode="Static" OnClientClick="calculatePass();" OnClick="lnkResetPassword_Click" ValidationGroup="Login1"
                        class="resetPasswords btn btn-primary"></asp:LinkButton>
                     <div class="display_none">
                          <asp:HiddenField ID="hdnEncpass" ClientIDMode="Static" runat="server" Value="" />
                        <asp:Button ID="btnsave" Text="" Font-Bold="true" runat="server" Style="margin-top: 0px;
                           background: #EBEBEB; border: none; margin-left: 3px; cursor: pointer;" ClientIDMode="Static"
                           OnClick="lnkResetPassword_Click" ValidationGroup="Login1" />
                     </div>
                            </div><!-- submit con ended -->
            
                 
               </div><!-- rightContainer ended -->
                 
                                </div><!-- col-sm ended -->
                                
                            </div><!-- row ended -->
                            </div><!-- card ended  -->
                        </div>
                        <!-- full-width-con ended -->
                    </div>
                    <!-- panel-cover ended -->
                </div>
               
 </ContentTemplate></asp:UpdatePanel>
</asp:Content>
    