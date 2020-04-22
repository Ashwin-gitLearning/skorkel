<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="forum-landing-page.aspx.cs"
   Inherits="forum_landing_page" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Src="~/UserControl/Groups-m.ascx" TagName="GroupDetailsM" TagPrefix="GroupM" %>
<%@ MasterType TypeName="Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
   <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:UpdatePanel ID="updtae" runat="server">
      <ContentTemplate>

         <div class="main-section-inner">
            <!---Delete Popup-->
            <div id="divDeletesucess" runat="server" ClientIDMode="Static" Class="EditProfilepopupHome modal backgroundoverlay" Style="display: none;">
             
             <div id="divDeleteConfirm" runat="server" class="confirmDeletes modal-dialog modal-dialog-centered" clientidmode="Static">
              <div class="modal-content">
             
               <div>
                <b>
                 <asp:Label ID="Label2" runat="server"></asp:Label>
                </b>
               </div>
               <div class="modal-header">
                <h5 class="modal-title">Delete Confirmation
                </h5>
               </div>
               <div class="modal-body">
                <asp:Label ID="Label3" runat="server" Text="Do you want to delete ?"></asp:Label>
               </div>   
               <div class="modal-footer border-top-0">
                <asp:LinkButton ID="lnkDeleteCancel" runat="server" ClientIDMode="Static" Text="Cancel" class="add-scroller btn btn-outline-primary m-r-15"
                 OnClick="lnkDeleteCancel_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" CssClass="btn btn-primary success-popup"
                 OnClick="lnkDeleteConfirm_Click"></asp:LinkButton>
               </div>
                  
              </div>
             </div>
            </div>

            <div id="divSuccess" runat="server" class="modal backgroundoverlay" clientidmode="Static" style="display: none;">
             <div class="modal-dialog modal-dialog-centered">
              <div class="modal-content">
               <div class="modal-header">
                <h5 class="modal-title">Success  
                </h5>
               </div>
               <div class="modal-body">
                <asp:Label ID="lblSuccess" runat="server" Text="Successfully deleted."></asp:Label>
               </div>   
               <div class="modal-footer border-top-0">
                <asp:Button ClientIDMode="Static" class="btn btn-primary add-scroller" id="btnOk" runat="server" Text="Ok" OnClientClick="javascript:CloseMsg();"></asp:Button>
               </div>
              </div>
             </div>
            </div>

            <!---Delete Popup Ended-->
            <div class="panel-cover clearfix">
               <!---Center Panel Start-->
               <div class="center-panel">
                  <div class="custom-nav-con group-page-tab">
                     <GroupM:GroupDetailsM ID="grpDetailsM" runat="server" />
                     <!--- Tab Section Start-->
                     <ul class="custom-nav-control nav nav-tabs ">
                        <li class="nav-item">
                           <asp:LinkButton ID="lnkProfile" class="nav-link" runat="server" Text="Profile" ClientIDMode="Static"
                              OnClick="lnkProfile_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item" id="DivHome" runat="server" >
                       
                              <asp:LinkButton ID="lnkHome" runat="server" class="nav-link" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                        
                        </li>
                        <li class="nav-item" id="DivForumTab" runat="server" clientidmode="Static">
                         
                              <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                 class="forumstabAcitve nav-link" OnClick="lnkForumTab_Click"></asp:LinkButton>
                          
                        </li>
                        <li class="nav-item" id="DivUploadTab" runat="server" clientidmode="Static" >
                           
                              <asp:LinkButton ID="lnkUploadTab" class="nav-link" runat="server" Text="Uploads" ClientIDMode="Static"
                                 OnClick="lnkUploadTab_Click"></asp:LinkButton>
                         
                        </li>
                        <li class="nav-item" id="DivPollTab" runat="server" clientidmode="Static" >
                           
                              <asp:LinkButton ID="lnkPollTab" class="nav-link" runat="server" Text="Polls" ClientIDMode="Static"
                                 OnClick="lnkPollTab_Click"></asp:LinkButton>
                          
                        </li>
                        <li class="nav-item" id="DivEventTab" runat="server" clientidmode="Static" s>
                          
                              <asp:LinkButton ID="lnkEventTab" class="nav-link" runat="server" Text="Events" ClientIDMode="Static"
                                 OnClick="lnkEventTab_Click"></asp:LinkButton>
                           
                        </li>
                        <li class="nav-item" id="DivMemberTab" runat="server" clientidmode="Static" >
                          
                              <asp:LinkButton ID="lnkMemberTab" class="nav-link" runat="server" Text="Members" ClientIDMode="Static"
                                 OnClick="lnkEventMemberTab_Click"></asp:LinkButton>
                           
                        </li>
                     </ul>
                     <!--- Tab Section Ended--> 
                     <div class="tab-content">
                        <div class="btn-title-con">
                        <div>
                          <h5 class="card-title float-left">Forums</h5>
                         </div>
                         <div>
                          <asp:LinkButton ID="lnkcreateForum" Text="Create Forum" runat="server" ClientIDMode="Static" OnClick="lnkcreateForum_Click"
                           CssClass="CreateForum btn btn-outline-primary float-right"></asp:LinkButton>
                         </div>
                        </div>   
                        <div class="forumContainerStripHeading">
                          
                           <div id="divheight" runat="server">
                              <div id="divCancelPoll" runat="server" class=" modal_bg" style=" display: none;" clientidmode="Static">
                                 <div class="modal-dialog">
                                    <div>
                                       <b>
                                          <asp:Label ID="lbl" runat="server"></asp:Label>
                                       </b>
                                    </div>
                                    <div class="modal-header">
                                       <strong>
                                          &nbsp;&nbsp;
                                          <asp:Label ID="lblConnDisconn" runat="server" Text="" ></asp:Label>
                                       </strong>
                                    </div>
                                    <div class="modal-footer">
                                       <asp:LinkButton ID="lnkConnDisconn" runat="server" ClientIDMode="Static" Text="Yes"
                                          CssClass="joinBtn default_btn " OnClick="lnkConnDisconn_Click"></asp:LinkButton>
                                       <a href="#" clientidmode="Static" class="cancel_btn" causesvalidation="false" 
                                          onclick="Cancel();">Cancel </a>
                                    </div>
                                 </div>
                              </div>
                           </div>
                           
                        </div>
                        <!---List Section start-->
                      
                           <asp:UpdatePanel ID="update" runat="server">
                              <ContentTemplate>
                                 <asp:ListView ID="lstForumDetails" runat="server" OnItemDataBound="lstForumDetails_ItemDatabound"
                                    OnItemCommand="lstForumDetails_ItemCommand">
                                    <LayoutTemplate>
                                       <table cellpadding="0" cellspacing="0" width="958">
                                          <tr id="itemPlaceholder" runat="server">
                                          </tr>
                                       </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                  
                                       <asp:HiddenField ID="hdnForumPostId" Value='<%#Eval("intForumPostingId") %>' ClientIDMode="Static"
                                          runat="server" />
                                       <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' runat="server" />
                                       <div class="card-list-con blog-list">
                                        <div class="card top-list">
                                         <div class="post-con">
                                          <div class="post-body">
                                           <span class="more-btn float-right" id="spanEditDelete" visible="false" runat="server">
                                            <asp:UpdatePanel ID="updatedelet" runat="server" UpdateMode="Conditional">
                                             <ContentTemplate>
                                              <span class="dropdown show">
                                               <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                                <img src="images/more.svg" alt="" class="more-btn">
                                               </a>
                                               <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="top-start">
                                                <asp:LinkButton ID="lnkEdit" class="dropdown-item" Font-Underline="false" Visible="false" 
                                                 ClientIDMode="Static" ToolTip="Edit" CommandName="EditForum" CausesValidation="false"
                                                 runat="server"><span class="lnr lnr-pencil"></span> Edit                                                        
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lnkDelete" Font-Underline="false" Visible="false" class="editBtn hide-body-scroll dropdown-item"
                                                 ClientIDMode="Static" ToolTip="Delete" CommandName="DeleteForum" CausesValidation="false" runat="server">
                                                 <span class="lnr lnr-trash"></span> Delete
                                                </asp:LinkButton>                                                        
                                               </span><%--<i class="fa fa-trash-o" aria-hidden="true"></i>--%>
                                              </span> 
                                             </ContentTemplate>
                                             <Triggers>
                                              <asp:AsyncPostBackTrigger ControlID="lnkDelete" EventName="Click" />
                                              <asp:AsyncPostBackTrigger ControlID="lnkEdit" EventName="Click" />
                                             </Triggers>
                                            </asp:UpdatePanel> 
                                           </span>
                                           <div id="divshare" style="display: none" runat="server">
                                            <asp:LinkButton ID="LinkButton1" Font-Underline="false" CommandName="Details" runat="server"
                                             Text='<%#Eval("SharedName") %>'>
                                            </asp:LinkButton>
                                            <br />
                                            <asp:Label ID="Label1" runat="server" Text="Shared">
                                            </asp:Label>
                                            &nbsp;
                                           </div>
                                           <h3 class="mb-1">
                                            <asp:LinkButton ID="lnkTitle" Text='<%#Eval("strTitle") %>' CommandName="Forum"  runat="server"></asp:LinkButton>
                                           </h3>
                                           <div class="small-date">
                                            <asp:Label ID="lblDate" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                           </div>
                                           <p>
                                            <asp:Label ID="lblDescrption" runat="server" Text='<%#Eval("strDescription") %>'></asp:Label>                                                         
                                           </p>
                                           </div>
                                           <div class="post-footer">
                                            <ul class="list-inline li-flex-align">
                                             <li class="list-inline-item"> 
                                              <span class="edit-comment-btn"></span>            
                                              <asp:Label ID="lblreply" runat="server" Text=""></asp:Label><%-- Replies--%>
                                             </li>
                                            </ul>                             
                                           </div>   
                                          </div>
                                         </div>
                                        </div>
                                        
                                    </ItemTemplate>
                                 </asp:ListView>
                              </ContentTemplate>
                           </asp:UpdatePanel>
                           <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                           <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                           <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
                           <asp:HiddenField ID="hdnLoadernew" runat="server" ClientIDMode="Static" />
                            <div id="pLoadMore" runat="server" align="center" ClientIDMode="Static">

                            <div id="imgLoadMore" runat="server" class="lds-ellipsis" ClientIDMode="Static" Style="display: none;">
                             <div></div>
                             <div></div>
                             <div></div>
                             <div></div>
                            </div>
                           
                           <asp:ImageButton ID="imgLoadMore2" runat="server" ClientIDMode="Static" OnClick="imgLoadMore_OnClick" Style="display: none;" />
                           </div>
                            <p align="center" Style="display: none;">
                             <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server"
                              ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                            </p>
                           <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                      
                     </div>   
                  </div>
               </div>
               <!---Center Panel Ended-->
               <!---Right panel-->
          
                  <asp:UpdatePanel ID="upGroups" runat="server">
                     <ContentTemplate>
                        <!--groups top box starts-->
                        <Group:GroupDetails ID="grpDetails" runat="server" />
                        <!--groups top box ends-->
                     </ContentTemplate>
                  </asp:UpdatePanel>
          
               <!---Right panel Ended-->  
            </div>
         </div>

         <script type="text/javascript">
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
            $(document).ready(function () {
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
                });
            });
              $(document).on('click', '.mobile_tab_icon', function() {
             $('ul.group_p').slideToggle('slow');
            });
         </script>
         <script type="text/javascript">
            $(document).ready(function () {
                var ID = "#" + $("#hdnLoader").val();
                $(ID).focus();
            });
            $(document).ready(function () {
                var ID = "#" + $("#hdnLoadernew").val();
                $(ID).focus();
            });
            function Cancel() {
                document.getElementById("divCancelPoll").style.display = 'none';
                return false;
            }
         </script>
      </ContentTemplate>
      <Triggers>
         <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
         <asp:AsyncPostBackTrigger ControlID="lnkDeleteCancel" />
      </Triggers>
   </asp:UpdatePanel>
   <script type="text/javascript" lang="javascript">
      $(document).ready(function () {
          $("#lblNoMoreRslt").css("display", "none");
      });
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          $(document).scroll(function () {
              if ($(document).height() <= $(window).scrollTop() + $(document).height()) {
                  $("#imgLoadMore").css("display", "block");
                  $(".divProgress").css("display", "none");
                  var v = $("#lblNoMoreRslt").text();
                  var maxCount = $("#hdnMaxcount").val();
                  if (parseInt(maxCount, 10) <= parseInt($("#hdnTotalItem").val(), 10) ) {
                      $("#lblNoMoreRslt").css("display", "none");
                      $("#imgLoadMore").css("display", "none");
                  } else {
                      document.getElementById('<%= imgLoadMore2.ClientID %>').click();
                  }
              }
          });
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              hideLoader1();
              $(window).scroll(function () {
                  if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                      $("#imgLoadMore").css("display", "block");
                      $(".divProgress").css("display", "none");
                      var v = $("#lblNoMoreRslt").text();
                      var maxCount = $("#hdnMaxcount").val();
                      if (parseInt(maxCount, 10) <= parseInt($("#hdnTotalItem").val(), 10) )  {
                          $("#lblNoMoreRslt").css("display", "none");
                          $("#imgLoadMore").css("display", "none");
                      } else {
                          document.getElementById('<%= imgLoadMore2.ClientID %>').click();
                      }
                  }
              });
          });
      });
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          $("span.spEditForum").click(function () {
              $(this).children('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $("span.spDeleteForum").click(function () {
              $(this).children('#lnkDelete').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $('#lnkDeleteConfirm').click(function (e) {
              $(this).css("box-shadow", "0px 0px 5px #00B7E5");
          });
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              $("span.spEditForum").click(function () {
                  $(this).children('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spDeleteForum").click(function () {
                  $(this).children('#lnkDelete').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $('#lnkDeleteConfirm').click(function (e) {
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
          });
      
          $('#divCancelPoll').center();
      });
      
   </script>
   <script type="text/javascript">
      function docdelete() {
          $('#divDeletesucess').css("display", "block");
      }
      function divCancels() {
          $('#divDeletesucess').css("display", "none");
      }
      function CloseMsg() {
          $('#divSuccess').css("display", "none");
      }
   </script>
</asp:Content>