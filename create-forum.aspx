<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="create-forum.aspx.cs"
   Inherits="create_forum" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Src="~/UserControl/Groups-m.ascx" TagName="GroupDetailsM" TagPrefix="GroupM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
   <script src="<%=ResolveUrl("js/jquery.custom-scrollbar.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:UpdatePanel ID="up1" runat="server">
      <ContentTemplate>
       
     
    
            <div class="main-section-inner">
               <div class="panel-cover clearfix">
                  <div class="custom-nav-con group-page-tab">
                    <!---Center Panel Start-->
                    <div class="center-panel">
                       <GroupM:GroupDetailsM ID="grpDetailsM" runat="server" />
                        <!--- Tab Section Start-->
                        <ul class="custom-nav-control nav nav-tabs ">
                           <li class="nav-item">
                              <asp:LinkButton ID="lnkProfile" class="nav-link" runat="server" Text="Profile" ClientIDMode="Static"
                                 OnClick="lnkProfile_Click"></asp:LinkButton>
                           </li>
                           <li id="DivHome" runat="server" class="nav-item">
                             
                                 <asp:LinkButton class="nav-link" ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                              
                           </li>
                           <li id="DivForumTab" runat="server" clientidmode="Static" class="nav-item">
                             
                                 <asp:LinkButton ID="tagfor" runat="server" Text="Forums" ClientIDMode="Static" class="forumstabAcitve nav-link"
                                    OnClick="lnkForumTab_Click"></asp:LinkButton>
                             
                           </li>
                           <li id="DivUploadTab" runat="server" clientidmode="Static" class="nav-item">
                              
                                 <asp:LinkButton ID="lnkUploadTab" class="nav-link" runat="server" Text="Uploads" ClientIDMode="Static"
                                    OnClick="lnkUploadTab_Click"></asp:LinkButton>
                              
                           </li>
                           <li id="DivPollTab" runat="server" clientidmode="Static" class="nav-item">
                              
                                 <asp:LinkButton ID="lnkPollTab" class="nav-link" runat="server" Text="Polls" ClientIDMode="Static"
                                    OnClick="lnkPollTab_Click"></asp:LinkButton>
                              
                           </li>
                           <li id="DivEventTab" runat="server" clientidmode="Static" class="nav-item">
                             
                                 <asp:LinkButton ID="lnkEventTab" class="nav-link" runat="server" Text="Events" ClientIDMode="Static"
                                    OnClick="lnkEventTab_Click"></asp:LinkButton>
                             
                           </li>
                           <li id="DivMemberTab" runat="server" clientidmode="Static" class="nav-item">
                           
                                 <asp:LinkButton ID="lnkMemberTab" class="nav-link" runat="server" Text="Members" ClientIDMode="Static"
                                    OnClick="lnkEventMemberTab_Click"></asp:LinkButton>
                             
                           </li>
                        </ul>
                        <!--- Tab Section Ended--> 
                      <div class="btn-title-con">
                        <div class="back-link-cover mb-0 mt-0">
                          <asp:LinkButton ID="lnkBack" runat="server" class="backButton back-link" OnClick="lnkBack_Click" Text="Back">
                            <span class="lnr lnr-arrow-left"></span> Back
                          </asp:LinkButton>
                       </div>
                      </div>
                  
                      <div class="card">

                    <!--      <p class="headingCreateNewForum">
                            <asp:Label ID="lblcreatefrm" runat="server" Text="Create New Forum"></asp:Label>
                         </p> -->
                         <div id="divForumSuccess" style="display: none;" runat="server" class=" modal backgroundoverlay" clientidmode="Static">
                          <div class="modal-dialog modal-dialog-centered">
                           <div class="modal-content">
                            <div class="modal-header">
                             <h5 class="modal-title"> Success</h5>
                            </div>
                            <div class="modal-body">
                             <asp:Label ID="lblSuccess"  runat="server" Text="Forum created successfully.">
                             </asp:Label>
                             <asp:Label ID="lblEditSuccess" runat="server" class="form_updated_s" Text="Forum updated successfully.">
                             </asp:Label>
                            </div>
                            <div class="modal-footer border-top-0">
                             <asp:LinkButton ID="lnkBacks" class="add-scroller btn btn-outline-primary" runat="server" Text="Close" ClientIDMode="Static" CausesValidation="false"
                              OnClick="lnkBacks_Click">
                             </asp:LinkButton>
                            </div>
                           </div>   
                          </div>
                         </div>
                         <div align="left">
                          <b>
                           <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                          </b>
                         </div>
                         <div class="createForumbox">
                          <div class="card-body">
                           <div class="form-group">
                            <label for="Title">Title</label>
                             <asp:TextBox ID="txtTitle" maxlength="500" autocomplete="off" class="entryForumsTitle form-control" placeholder="Title" ClientIDMode="Static" runat="server">
                             </asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                              Display="Dynamic" ValidationGroup="t" ErrorMessage="Please enter Title" ForeColor="Red">
                             </asp:RequiredFieldValidator>
                           </div>
                           <div class="form-group">
                            <label for="">Description</label>
                            <textarea rows="10" class="entryForumstArea form-control" maxlength="5000"  id="CKDescription" runat="server" placeholder="Description"
                             clientidmode="Static"></textarea>
                           </div>   
                           <div class="checkBox checkbox_new display-none">
                            <asp:UpdatePanel ID="upd" UpdateMode="Conditional" class="custom-checkbox" ClientIDMode="Static" runat="server">
                             <ContentTemplate>
                              <asp:CheckBox ID="chkPrivateForm" Text="Private Forum" ClientIDMode="Static" runat="server" />
                             </ContentTemplate>
                            </asp:UpdatePanel>
                           </div>                           
                          </div>
                          <div class="card-footer text-right">
                           <asp:LinkButton ID="Close" class="btn btn-outline-primary m-r-15" runat="server"
                            Text="Cancel" OnClick="btnClose_Click"></asp:LinkButton>
                           <asp:LinkButton ID="btnSave" class="btn btn-primary" runat="server" Text="Create Forum"
                            ValidationGroup="t" OnClick="btnSave_Click" OnClientClick="javascript:CallCreateForum();"></asp:LinkButton><%--OpenForumGrpMember();--%>
                        
                           <asp:LinkButton ID="lnkUpdate" class="vote  btn btn-primary" runat="server" Text="Update Forum" ValidationGroup="t"
                            OnClick="lnkUpdate_Click" OnClientClick="javascript:CallCreateForum();"></asp:LinkButton>
                          
                          </div>   
                          <div class="display-none">
                           <asp:Button ID="btnSave1" runat="server" Font-Bold="true" Text="Create Forum" ClientIDMode="Static"
                            ValidationGroup="t" OnClick="btnSave_Click" OnClientClick="javascript:CallCreateForum();" />
                           &nbsp;&nbsp;
                           <asp:Button ID="lnkUpdate1" runat="server" Text="Update Forum" ValidationGroup="t" ClientIDMode="Static"
                            6OnClick="lnkUpdate_Click" OnClientClick="javascript:CallCreateForum();" />
                          </div>
                         </div>
                      </div>
                    </div>
                    <!---Center Panel Ended-->
                     <!---Right panel-->
                  
                       <asp:UpdatePanel ID="uppln" runat="server">
                         <ContentTemplate>
                            <Group:GroupDetails ID="grpDetails" runat="server" />
                         </ContentTemplate>
                       </asp:UpdatePanel>
                 
                     <!---Right panel Ended-->          
                  </div>    
               </div>
            </div>
     
      </ContentTemplate>
      <Triggers>
         <asp:AsyncPostBackTrigger ControlID="btnSave" />
         <asp:AsyncPostBackTrigger ControlID="lnkUpdate" />
      </Triggers>
   </asp:UpdatePanel>
   <script type="text/javascript">
      $(document).ready(function () {
          $('#txtInvitee_chosen').css("width", "420px");
          $('#Close').click(function () {
              $("#txtTitle").val('Title');
              $("#CKDescription").val('Description');
          });
      });
      
   </script>
   <script type="text/javascript">


       //var config = {
       //    '.chosen-select': {},
       //    '.chosen-select-deselect': { allow_single_deselect: true },
       //    '.chosen-select-no-single': { disable_search_threshold: 10 },
       //    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
       //    '.chosen-select-width': { width: "95%" }
       
       //}
       //for (var selector in config) {
       //    $(selector).chosen(config[selector]);
       //}
       //$(".chosen-container").css("width", "670px");
       $(document).ready(function () {

          var config = {
                  '.chosen-select': {},
                  '.chosen-select-deselect': { allow_single_deselect: true },
                  '.chosen-select-no-single': { disable_search_threshold: 10 },
                  '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                  '.chosen-select-width': { width: "95%" }
              }
              for (var selector in config) {
                  $(selector).chosen(config[selector]);
              }
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              var config = {
                  '.chosen-select': {},
                  '.chosen-select-deselect': { allow_single_deselect: true },
                  '.chosen-select-no-single': { disable_search_threshold: 10 },
                  '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                  '.chosen-select-width': { width: "95%" }
              }
              for (var selector in config) {
                  $(selector).chosen(config[selector]);
              }
              $(".chosen-container").css("width", "670px");
          });
          $(".chosen-container").css("width", "670px");
      });
   </script>
   <script type="text/javascript">
      function AlertMsg(ctl) {
          if (ctl == "1") {
              alert('Forum Updated Successfully.');
              $("#btnGo").click();
          }
          else {
              alert('New forum created successfully.');
              $("#btnGo").click();
          }
      }
   </script>
   <script type="text/javascript">
      function MesForumClose() {
          document.getElementById("divForumSuccess").style.display = 'none';
      }
       function CallCreateForum() {
        $('#ctl00_ContentPlaceHolder1_btnSave').addClass('hide-body-scroll');
           //debugger;
          
           //$('#btnSave').addClass('disabled');
           if ($('#txtTitle').val() == '') {
             setTimeout(
                  function () {
                      $('#ctl00_ContentPlaceHolder1_btnSave').removeClass('hide-body-scroll');
                         $('body').removeClass('remove-scroller');
                  }, 1000);
           } else {
               showLoader1();
           }


      }
           function OverlayBody() {
            $('#bodyelement').addClass("remove-scroller");
        }
      $(document).ready(function () {
          $('#txtTitle').keypress(function (e) {
              if (e.keyCode == 13) {
                  $('#btnSave1').click();
                      return false;
              }
          });
      });
   </script>
   <asp:HiddenField ID="hdnForumID" runat="server" ClientIDMode="Static" />
   <div style="display: none">
      <asp:Button ID="btnGo" runat="server" ClientIDMode="Static" OnClick="btnGo_Click" />
   </div>
</asp:Content>