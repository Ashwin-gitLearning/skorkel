<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="forum-detail.aspx.cs" Inherits="forum_detail" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
 <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
 <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
 <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:HiddenField ID="hdnrply" runat="server" ClientIDMode="Static" />
   <div class="main-section-inner">
      <div class="panel-cover clearfix">
         <!---Delete conformation popup-->
         <!---Sucess Popup  on delete post-->
         
         <div id="divDeletesucess" clientidmode="Static" runat="server" class="EditProfilepopupHome  modal backgroundoverlay"
          style="display: block;">
          <div id="divDeleteConfirm" runat="server" class="modal-dialog modal-dialog-centered" clientidmode="Static">
           <div class="modal-content">
            <div>
             <b>
              <asp:Label ID="Label2" runat="server"></asp:Label>
             </b>
            </div>
            <div class="modal-header">
             <h5 class="modal-title">Delete Confirmation</h5>                     
            </div>
            <div class="modal-body">
             <p><asp:Label ID="Label3" runat="server" Text="Do you want to delete ?" ></asp:Label></p>
            </div>   
            <div class="modal-footer border-top-0">                   
             <asp:LinkButton ID="lnkDeleteCancel" class="add-scroller btn btn-outline-primary m-r-15" runat="server" ClientIDMode="Static" Text="Cancel"
              OnClick="lnkDeleteCancel_Click" OnClientClick="divCancels();">
             </asp:LinkButton>
             <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes"
              CssClass="btn btn-primary" OnClick="lnkDeleteConfirm_Click" OnClientClick="divCancels();">
             </asp:LinkButton>
            </div>
           </div>   
          </div>
         </div>
         <!---Delete conformation popup Ended-->
         <asp:UpdatePanel ID="up1" runat="server">
          <ContentTemplate>
          <div id="divDeletesucessCmnts" class="modal backgroundoverlay" clientidmode="Static" runat="server"
          style="display: none;">
          <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="modal-dialog modal-dialog-centered">
           <ContentTemplate>
            <div id="div2" runat="server" class="w-100" clientidmode="Static">
             <div class="modal-content">
              <div>
               <b>
                <asp:Label ID="lbl3" runat="server"></asp:Label>
               </b>
              </div>
              <div class="modal-header">
               <h5 class="modal-title"> Delete confirmation
               </h5>
              </div>
              <div class="modal-body">
               <asp:Label ID="lbl4" runat="server" Text="Do you want to delete?"></asp:Label>
              </div>
              <div class="modal-footer border-top-0">
               <asp:Button ID="lnkCmntDeleteCancel" runat="server" class="add-scroller btn btn-outline-primary m-r-15" ClientIDMode="Static" Text="Cancel"
                OnClientClick="javascript:divCancelsCmnts();return false;"></asp:Button>
               <asp:Button ID="lnkCmntDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes"
                CssClass="btn btn-primary success-popup" OnClick="lnkDeleteConfirm_Click"></asp:Button>
              </div>
          
             </div>
            </div>
           </ContentTemplate>
          </asp:UpdatePanel>
         </div>
         <div class="modal backgroundoverlay" id="post-delete" style="display:none;">
          <div class="modal-dialog modal-dialog-centered">
          
           <div  class="w-100" >
            <div class="modal-content">
           
                <div class="modal-header">
                    <h5 class="modal-title">Success
                    </h5>
                </div>
                <div class="modal-body">
                    Successfully deleted.
                </div>
                <div class="modal-footer border-top-0">
                    <asp:LinkButton runat="server" class="btn btn-primary add-scroller remove-popup"> Ok </asp:LinkButton>
                </div>
            </div>
           </div>
         
          </div>
         </div>
            <!---Center panel Start-->
            <div class="center-panel">
               <div class="custom-nav-con group-page-tab">
                   <ul class="custom-nav-control nav nav-tabs ">
                     <li class="nav-item">
                      <asp:LinkButton ID="lnkProfile" class="nav-link" runat="server" Text="Profile" ClientIDMode="Static"
                       OnClick="lnkProfile_Click">
                      </asp:LinkButton>
                     </li>
                     <li class="nav-item">
                      <div id="DivHome" runat="server" >
                       <asp:LinkButton ID="lnkHome" class="nav-link" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                      </div>
                     </li>
                     <li class="nav-item">
                      <div id="DivForumTab" runat="server" clientidmode="Static">
                       <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                        class="forumstabAcitve nav-link" OnClick="lnkForumTab_Click">
                       </asp:LinkButton>
                      </div>
                     </li>
                     <li class="nav-item">
                      <div id="DivUploadTab" runat="server" clientidmode="Static" >
                       <asp:LinkButton ID="lnkUploadTab" class="nav-link" runat="server" Text="Uploads" ClientIDMode="Static"
                        OnClick="lnkUploadTab_Click">
                       </asp:LinkButton>
                      </div>
                     </li>
                     <li class="nav-item">
                        <div id="DivPollTab" runat="server" clientidmode="Static" >
                           <asp:LinkButton ID="lnkPollTab" runat="server" Text="Poll" ClientIDMode="Static"
                              OnClick="lnkPollTab_Click" class="nav-link"></asp:LinkButton>
                        </div>
                     </li>
                     <li class="nav-item">
                        <div id="DivEventTab" runat="server" clientidmode="Static" >
                           <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                              OnClick="lnkEventTab_Click" class="nav-link"></asp:LinkButton>
                        </div>
                     </li>
                     <li class="nav-item">
                        <div id="DivMemberTab" runat="server" clientidmode="Static" >
                           <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                              OnClick="lnkEventMemberTab_Click" class="nav-link"></asp:LinkButton>
                        </div>
                     </li>
                  </ul> 
                  <div class="btn-title-con ">
                        <div class="back-link-cover mt-2 mb-0">
                           <asp:LinkButton ID="lnkAllForum" runat="server" class="backButton back-link pull-left" OnClick="lnkAllForum_Click"
                           Text="Back"><span class="lnr lnr-arrow-left"></span> Back to Forums</asp:LinkButton>
                        </div>   
                        <div>
                        <asp:LinkButton ID="lnkcreateForum" Text="Create Forum" runat="server" ClientIDMode="Static"
                           OnClick="lnkcreateForum_Click"
                           CssClass="createForum pull-right btn btn-outline-primary">                              
                        </asp:LinkButton>  
                        </div>                   
                  </div>    
                  <div class="card card-list-con list-comments-border">
                     <!-- end tag block -->
                     <div class="list-group-item top-list" >
                        <div class="post-con">
                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                              <ContentTemplate>
                                 <asp:ListView ID="lstForumDetails" runat="server" OnItemCommand="lstForumDetails_ItemCommand"
                                    OnItemDataBound="lstForumDetails_ItemDatabound">
                                    <LayoutTemplate>
                                     <table cellpadding="0" cellspacing="0">
                                      <tr id="itemPlaceholder" runat="server">
                                      </tr>
                                     </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                    <div class="post-body">
                                       <!---Edit Delete Buttons-->
                                       <asp:UpdatePanel ID="up3" runat="server">
                                        <ContentTemplate>
                                         <span class="more-btn float-right" id="spanEditDelete" visible="false" runat="server">
                                          <span class="dropdown show">
                                           <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                            <img src="images/more.svg" alt="" class="more-btn">
                                           </a>
                                           <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start">

                                            <asp:LinkButton ID="lnkEdit1" Font-Underline="false" Visible="false" class="dropdown-item"
                                             ClientIDMode="Static" ToolTip="Edit" Text="Edit" CommandName="Edit forum" CausesValidation="false"
                                             runat="server">
                                             <span class="lnr lnr-pencil"></span> Edit
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete1" Font-Underline="false" Visible="false" class="dropdown-item hide-body-scroll"
                                             ClientIDMode="Static" ToolTip="Delete" Text="Delete" CommandName="Delete forum"
                                             CausesValidation="false" runat="server" OnClientClick="docdelete();">
                                             <span class="lnr lnr-trash"></span> Delete
                                            </asp:LinkButton>
                                           </span>
                                          </span>
                                         </span>

                                        </ContentTemplate>
                                        <Triggers>
                                         <asp:AsyncPostBackTrigger ControlID="lnkDelete1" />
                                        </Triggers>
                                       </asp:UpdatePanel>
                                                  
                                       <h3 class="breakword">
                                        <asp:LinkButton ID="lnkTitle" CssClass="forumsTitle remove-hover-anchor" runat="server"
                                         Enabled="false" Text='<%#Eval("strTitle")%>' CommandName="Forum">
                                        </asp:LinkButton>
                                       </h3>
                                       <ul class="small-date">
                                        <%--<li>by Sumit Mehta</li>--%>
                                        <li>by&nbsp;<asp:LinkButton  CommandName="Details" ID="Label1" runat="server" Text='<%#Eval("Name") %>'></asp:LinkButton></li>
                                        <li><asp:Label ID="lblDate" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label></li>
                                       </ul>
                                       <div class="m-t-10">
                                        <p>
                                         <asp:LinkButton ID="LinkButton1" Enabled="false" Text='<%#Eval("strDescription") %>'
                                          CommandName="Forum" runat="server">
                                         </asp:LinkButton>
                                        </p>
                                       </div>
                                       <%--<span style="display: none;">
                                          <asp:LinkButton CommandName="Details" ID="Label1" runat="server" Text='<%#Eval("Name") %>'></asp:LinkButton>
                                          .
                                       </span>--%>               
                                     </div>   
                           
                                             <asp:HiddenField ID="hdnForumPostId" Value='<%#Eval("intForumPostingId")%>' ClientIDMode="Static"
                                                runat="server" />
                                             <asp:HiddenField ID="hdnRegid" Value='<%#Eval("intRegistrationId")%>' ClientIDMode="Static"
                                                runat="server" />
                                                
                                             <%--<div class="post-footer">
                                              <ul  class="list-inline">
                                               <li class="list-inline-item">                                                      
                                                <asp:LinkButton ID="btnLike" runat="server" class="active-toogle"
                                                 CommandName="LikeForum" ToolTip="Like">
                                                 <span class="like-btn"></span> 
                                                 <asp:Label ID="lblTotallike" runat="server"></asp:Label>
                                                </asp:LinkButton>
                                                   
                                               </li>
                                               <li class="list-inline-item">
                                                <asp:LinkButton ID="btnReply" runat="server" class="active-toogle" Text="Reply"
                                                 CommandName="ReplyPost" ToolTip="Add Comment">
                                                 <span class="comment-btn"></span> 
                                                 <asp:Label ID="lblreply" runat="server" Text=""></asp:Label>
                                                </asp:LinkButton>
                                               </li>
                                               <li class="list-inline-item">
                                                <asp:LinkButton ID="btnShare" runat="server" class="active-toogle" Text="Share"
                                                 CommandName="ShareForum" ToolTip="Share">
                                                 <span class="share-btn"></span>
                                                 <asp:Label ID="lblShare" runat="server" Text=""></asp:Label>
                                                </asp:LinkButton>
                                               </li>
                                              </ul>                                              
                                              <asp:Label ID="lbShareTitle" Visible="false" runat="server" Text='<%#Eval("shareTitle") %>'></asp:Label>
                                              <asp:Label ID="lbShareDesc" Visible="false" runat="server" Text='<%#Eval("shareDesc") %>'></asp:Label>
                                             </div>--%>
                                        
                                    </ItemTemplate>
                                   </asp:ListView>
                                  </ContentTemplate>
                                  <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="lstForumDetails" EventName="ItemCommand" />
                                  </Triggers>
                                 </asp:UpdatePanel>
                                </div>

                        <!---Add Comment Panel-->
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                           <ContentTemplate>
                            <div class="voteCommentsTxt post-form text-right mt-3">
                             <asp:Panel ID="pnlReply" runat="server">
                              
                              <asp:Textbox ID="txtPostForum" rows="3" TextMode="multiline" runat="server" autocomplete="off" placeholder="Reply" CssClass="form-control w-100 postcommentduplicate height-moz"
                               ValidationGroup="txt" ClientIDMode="Static" /> <%--eventTitleField commentA--%>
                               <p class="text-left">
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPostForum"
                               Display="Dynamic" ValidationGroup="txt" ErrorMessage="Please enter reply." ForeColor="Red" class="margin_left_rt">
                              </asp:RequiredFieldValidator>
                               </p>
                              <p>
                               <asp:Label ID="lblmsg" runat="server" class="inline-block text-left w-100" ForeColor="Red" Text="Please enter reply."
                                Visible="false">
                               </asp:Label>
                              </p>
                             
                              <asp:Button ID="lnkPostForum" runat="server" CssClass="btn btn-primary post-btn m-t-10" Text="Post"
                               ClientIDMode="Static" ValidationGroup="txt" OnClick="lnkPostForum_Click">
                              </asp:Button>
                              
                              <%--<div class="display_none">--%>
                              <asp:Button ID="btnSAve" runat="server" ClientIDMode="Static" OnClick="lnkPostForum_Click" Style="display:none" />
                              <%--</div>--%>
                              
                              <%--<asp:Button ID="lnkPostComent" runat="server" ClientIDMode="Static" CssClass="btn btn-primary post-btn m-t-10" OnClick="lnkPostComent_Click"
                               Text="Post" ValidationGroup="blog">
                              </asp:Button>--%>
                             </asp:Panel>
                            </div>
                              <!---Add Comment Panel Ended-->

                              <span id="totalreplies" runat="server" class="answer-counter" />

                     </div>
                  </div>   
                  <!---Comments Section Start-->
                         
                  <asp:ListView ID="lstReplyForum" runat="server" OnItemCommand="lstReplyForum_ItemCommand"
                     OnItemDataBound="lstReplyForum_ItemDatabound">
                  <LayoutTemplate>
                  <table cellpadding="0" cellspacing="0" class="table">
                  <tr id="itemPlaceholder" runat="server">
                  </tr>
                  </table>
                  </LayoutTemplate>
                  <ItemTemplate>
              
                  <div class="list-group-item">
                     <div class="post-con">
                        <asp:HiddenField ID="hdnForumPostId" Value='<%#Eval("intForumReplyLikeShareId")%>'
                           ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hdnRegid" Value='<%#Eval("intRegistrationId")%>' ClientIDMode="Static"
                           runat="server" />
                   
                              <div class="post-header">
                                 <!---Comment Image Avatar-->
                                 <div class="avatar-img">
                                  <img id="imgprofile" runat="server" src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' class="rounded-circle" />
                                   <%----%>

                                   <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath")%>' />
                                 </div>  
                                 <!---Comments Heading and Date--> 
                                    <span class="user-name-date">
                                     <span class="user-name">
                                      <asp:LinkButton Font-Underline="false" CommandName="Details" ID="Label1" CssClass="forumsTitle un-anchor"
                                       runat="server" Text='<%#Eval("Name") %>'></asp:LinkButton>
                                     </span> 
                                     <span class="date">
                                      <asp:Label ID="lblPostedDate" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                     </span> 
                                    </span>
                                    <!---Edit and Delete Buttons-->
                                    <asp:UpdatePanel ID="up2" runat="server">
                                     <ContentTemplate>
                                      <span class="more-btn float-right" id="spanCmntsEditDelete" visible="false" runat="server">
                                       <span class="dropdown">
                                        <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        <img src="images/more.svg" alt="" class="more-btn"></a>
                                        <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start">
                                         <asp:LinkButton ID="lnkEdit" runat="server" Font-Underline="false" Visible="false" class="dropdown-item"
                                          ClientIDMode="Static" ToolTip="Edit" Text="Edit" CommandName="Edits" CausesValidation="false" OnClientClick="showLoader1();">
                                          <span class="lnr lnr-pencil"></span> Edit
                                         </asp:LinkButton>                                               
                                         
                                         <asp:LinkButton ID="lnkDelete" Font-Underline="false" Visible="false" class="dropdown-item hide-body-scroll"
                                          ClientIDMode="Static" ToolTip="Delete" Text="Delete" CommandName="Deletes" CausesValidation="false"
                                          runat="server"><%--OnClientClick="docdelete();"--%>
                                          <span class="lnr lnr-trash"></span> Delete
                                         </asp:LinkButton>
                                        </span>
                                       </span>
                                      </span>  
                                     </ContentTemplate>
                                     <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="lnkEdit" />
                                      <asp:AsyncPostBackTrigger ControlID="lnkDelete" />
                                     </Triggers>
                                    </asp:UpdatePanel> 

                              </div>
                               <!---Comments description section-->
                    <div class="post-body">
                     <p><asp:Label ID="lblReplyComment" CssClass="comment moreQA" runat="server" Text='<%#Eval("strComment") %>'></asp:Label></p>
                     <%--<div  class="forum-like-btn float-left">                
                     
                      <asp:LinkButton ID="btnLikes" class="active-toogle" runat="server" CommandName="LikeForum" ToolTip="Like">
                       <span class="like-btn"></span>
                       <asp:Label ID="lblTotallikes" runat="server"></asp:Label>
                      </asp:LinkButton>
                     </div>--%>
                    </div>
                        
                   </div>  
                  </div>
                 </ItemTemplate>
                </asp:ListView>
               </ContentTemplate>
              </asp:UpdatePanel>
            </div>         
         </div> 

            <!---Center panel ended-->
            <!---Right Panel Start-->
    
               <Group:GroupDetails ID="grpDetails" runat="server" />
            
         
            <!---Right Panel Ended-->

      </div>
   </div>

   </ContentTemplate>
   <Triggers>
   <asp:AsyncPostBackTrigger ControlID="lnkPostForum" />
   <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
   <asp:AsyncPostBackTrigger ControlID="lnkDeleteCancel" />
   </Triggers>
   </asp:UpdatePanel>
 
   <asp:HiddenField ID="hdnForumrRefresh" runat="server" ClientIDMode="Static" />
   <script type="text/javascript">


     $(document).ready(function() {
        $('#txtPostForum').on('input propertychange', function() {
            CharLimit(this, 1000);
        });
    });

     function CharLimit(input, maxChar) {
        var len = $(input).val().length;
           
        if (len > maxChar) {
            $(input).val($(input).val().substring(0, maxChar));
          
        }
    }
          
 
      function showMoreLinks() {
          var showChar = 150;
          var ellipsestext = "...";
          var moretext = "Read more";
          var lesstext = "Less";
          $('.moreQA').each(function () {
              var content = $(this).html();
              if (content.length > showChar) {
                  var c = content.substr(0, showChar);
                  var h = content.substr(showChar - 1, content.length - showChar);
                  var html = c + '<span class="moreelipses">' + ellipsestext + '</span>&nbsp;<span class="morecontent"><span style="display:none;">' + h + '</span>&nbsp;&nbsp;<a href="" class="morelinkQA">' + moretext + '</a></span>';
                  $(this).html(html);
              }
          });
          $(".morelinkQA").click(function () {
              if ($(this).hasClass("less")) {
                  $(this).removeClass("less");
                  $(this).html(moretext);
              } else {
                  $(this).addClass("less");
                  $(this).html(lesstext);
              }
              $(this).parent().prev().toggle();
              $(this).prev().toggle();
              return false;
          });
      }
      $(document).ready(function () {
          showMoreLinks();
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              showMoreLinks();
          });

          
      });
   </script>
   <script type="text/javascript">
      function docdelete() {
          $('#divDeletesucess').css("display", "block");
      }
      function divCancels() {
          $('#divDeletesucess').css("display", "none");
      }
   </script>
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
          $("#txtPostForum").keypress(function (e) {
              if (e.keyCode == 13) {
                  $('#btnSAve').click();
                  return false; 
              }
          });

           $("#lnkPostForum").hide();

           $(".question-area").focusin(function() {
              $(this).removeClass("h-34");
              $(this).height(60);
              $("#lnkPostForum").show();    
           });

           $(".question-area").focusout(function () {
               if ($("#txtPostForum").val() == "") {
                   $(this).addClass("h-34");
                   $("#lnkPostForum").hide();
               }
           });

          $('#lnkDeleteConfirm').click(function (e) {
              $(this).css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $('#lnkDelete1').click(function (e) {
              $(this).css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $('#lnkEdit1').click(function (e) {
              $(this).css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $("span.spEditForum").click(function () {
              $(this).children('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $("span.spDeleteForum").click(function () {
              $(this).children('#lnkDelete').css("box-shadow", "0px 0px 5px #00B7E5");
          });
      
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {

               $(document).ready(function() {
                  $('#txtPostForum').on('input propertychange', function() {
                      CharLimit(this, 1000);
                  });
              });

               function CharLimit(input, maxChar) {
                  var len = $(input).val().length;
                     
                  if (len > maxChar) {
                      $(input).val($(input).val().substring(0, maxChar));

                    
                  }
              }
              $("#txtPostForum").keydown(function (e) {
                  if (e.keyCode == 13) {
                      $('#btnSAve').click();
                      return false; 
                  }
              });
              $('#lnkDeleteConfirm').click(function (e) {
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $('#lnkDelete1').click(function (e) {
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $('#lnkEdit1').click(function (e) {
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spEditForum").click(function () {
                  $(this).children('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spDeleteForum").click(function () {
                  $(this).children('#lnkDelete').css("box-shadow", "0px 0px 5px #00B7E5");
              });
      
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

           $("#lnkPostForum").hide();

           $(".question-area").focusin(function() {
              $(this).removeClass("h-34");
              $(this).height(60);
              $("#lnkPostForum").show();    
           });

           $(".question-area").focusout(function () {
               if ($("#txtPostForum").val() == "") {
                   $(this).addClass("h-34");
                   $("#lnkPostForum").hide();
               }
           });
          });





      });
    //    $(".question-area").focusin(function() {
    //    $(this).removeClass("h-34");
    //    $(this).height(60);
    //   $(".post-btn").show();    
    //});

    //$(".question-area").focusout(function() {
    //   $(this).addClass("h-34");
    //   $(".post-btn").hide();    
    //});
   </script>

</asp:Content>