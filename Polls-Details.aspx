<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
   EnableEventValidation="false" CodeFile="Polls-Details.aspx.cs" Inherits="Polls_Details" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Src="~/UserControl/Groups-m.ascx" TagName="GroupDetailsM" TagPrefix="GroupM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ MasterType TypeName="Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
   <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("js/PopupCenter.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:UpdatePanel ID="uppolldetails" UpdateMode="Conditional" ClientIDMode="Static" runat="server">
      <ContentTemplate>
         <div class="main-section-inner">
            <div id="divDeletesucess" clientidmode="Static" runat="server" class="modal backgroundoverlay" style="display: none;">
             <div id="divDeleteConfirm" runat="server" class="modal-dialog modal-dialog-centered" clientidmode="Static">
              <div class="modal-content">
               <div class="modal-header">
                <h5 class="modal-title mb-0">
                Delete Confirmation 
                 <asp:Label ID="Label3" runat="server"></asp:Label>
                </h5>
<!--
                <button type="button" class="close" OnClick="lnkDeleteCancel_Click" aria-label="Close">
                <span aria-hidden="true">&times;</span></button>
-->
               </div>
               <div class="modal-body">
                <asp:Label ID="Label4" runat="server" Text="Do you want to delete ?"></asp:Label>
               </div>
               <div class="modal-footer text-right">
                <asp:LinkButton ID="lnkDeleteCancel" runat="server" ClientIDMode="Static" Text="Cancel" OnClick="lnkDeleteCancel_Click" class="btn btn-outline-primary m-r-15 add-scroller">
                </asp:LinkButton>
                <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" CssClass="btn btn-primary success-popup" OnClick="lnkDeleteConfirm_Click">
                </asp:LinkButton>
               </div>
              </div>
             </div>
            </div>
            <div class="panel-cover clearfix">
               <div class="center-panel">
                  <!--left box starts-->
                  <div class="custom-nav-con group-page-tab">
                     <GroupM:GroupDetailsM ID="grpDetailsM" runat="server" />
                     <ul class="custom-nav-control nav nav-tabs">
                        <li class="nav-item">
                           <asp:LinkButton class="nav-link " ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static" OnClick="lnkProfile_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item" id="DivHome" runat="server" style="display: block;">
                           <asp:LinkButton class="nav-link " ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item" id="DivForumTab" runat="server" clientidmode="Static" style="display: block">
                           <asp:LinkButton class="nav-link " ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static" OnClick="lnkForumTab_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item" id="DivUploadTab" runat="server" clientidmode="Static" style="display: block">
                           <asp:LinkButton class="nav-link " ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static" OnClick="lnkUploadTab_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item" id="DivPollTab" runat="server" clientidmode="Static">
                           <asp:LinkButton class="nav-link active" ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static" OnClick="lnkPollTab_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item" id="DivEventTab" runat="server" clientidmode="Static" style="display: block">
                           <asp:LinkButton class="nav-link " ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static" OnClick="lnkEventTab_Click"></asp:LinkButton>
                        </li>
                        <li class="nav-item" id="DivMemberTab" runat="server" clientidmode="Static" style="display: block">
                           <asp:LinkButton class="nav-link " ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static" OnClick="lnkEventMemberTab_Click"></asp:LinkButton>
                        </li>
                     </ul>
                  </div>
                  <div class="btn-title-con">
                   <div>
                    <h5 class="card-title ">Polls</h5>
                   </div>
                   <div>
                    <div class="dropdown d-inline-flex mr-2">
                     <span id="sortFilter" class="sortFilter" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> Sort by: &nbsp; 
                      <span id="spanSelectedDropDown" class="font-weight-bold" runat="server">Recent</span><%--Recent--%>
                      <span class="icon-caret-down"></span>
                     </span>
                     <div class="dropdown-menu recent-dropDown" aria-labelledby="dropdownMenuButton">
                      <asp:LinkButton ID="LinkButton1" runat="server" Text="Recent" ClientIDMode="Static" CssClass="dropdown-item recentPoll recentActive" 
                       OnClick="chkRecent_CheckedChanged" OnClientClick="showLoader1();"></asp:LinkButton>
                      <asp:LinkButton ID="LinkButton2" runat="server" Text="Active" ClientIDMode="Static" CssClass="dropdown-item recentPoll" 
                       OnClick="chkActive_CheckedChanged" OnClientClick="showLoader1();"></asp:LinkButton>
                     </div>
                    </div>
                    <asp:LinkButton ID="lnkcreatePoll" runat="server" Text="Create Poll" ClientIDMode="Static" OnClick="lnkcreatePoll_Click" 
                     CssClass="createForum btn btn-outline-primary"></asp:LinkButton>
                   </div>
                   <!-- Modal -->
                  </div>
                  <div id="divheight" runat="server">
                     <div id="divSuccessPolls" runat="server" class="modal backgroundoverlay" style="display: none" clientidmode="Static">
                      <div class="modal-dialog modal-dialog-centered">
                       <div class="modal-content">
                        <div class="modal-header">
                         <h5 class="modal-title">Success</h5>
                         <%--<button type="button" class="close" OnClick="lnkDeleteCancel_Click" aria-label="Close">
                         <span aria-hidden="true">&times;</span>
                         </button>--%>
                         
                        </div>
                        <div class="modal-body">
                         <asp:Label ID="lblSuccess" runat="server" Text="Successfully deleted."></asp:Label>
                        </div>
                        <div class="modal-footer">
                         <a href="#" clientidmode="Static" causesvalidation="false" class="btn btn-primary add-scroller" onclick="javascript:messageClosePoll();">Ok</a>
                        </div>
                       </div>
                      </div>
                     </div> 
                     <div id="divCancelPoll" runat="server" class="modal backgroundoverlay" style=" display: none;" clientidmode="Static">
                        <div class="modal-dialog modal-dialog-centered">
                           <div class="modal-content">
                            <div class="modal-header">
                                 <h5 class="modal-title">Confirmation</h5>
                                 <asp:Label ID="lbl" runat="server"></asp:Label>
                                 <button type="button" class="close" OnClick="lnkDeleteCancel_Click" aria-label="Close">
                                 <span aria-hidden="true">&times;</span>
                                 </button>
                                 
                              </div>
                              
                           
                           <div class="modal-body">
                             
                                 <asp:Label ID="lblConnDisconn" runat="server" Text="" Font-Size="Small"></asp:Label>
                             
                           </div>
                           <div class="modal-footer">
                             <a clientidmode="Static" causesvalidation="false" class="btn btn-outline-primary m-r-15 add-scroller" onclick="Cancel();">Cancel </a>
                              <asp:LinkButton ID="lnkConnDisconn" runat="server" ClientIDMode="Static" Text="Okay" CssClass="btn btn-primary add-scroller" OnClick="lnkConnDisconn_Click"></asp:LinkButton>
                             
                           </div>
                        </div>
                     </div>
                    </div>
                     <div class="searchGroup" style="display: none">
                        <asp:TextBox ID="txtsearch" AutoCompleteType="Disabled" AutoPostBack="true" runat="server" ClientIDMode="Static" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                        <ajax:TextBoxWatermarkExtender TargetControlID="txtsearch" ID="txtwatermarkz" runat="server" WatermarkText="Search title">
                        </ajax:TextBoxWatermarkExtender>
                     </div>
                     <asp:Label ID="Label1" runat="server"></asp:Label>
                     <div>
                        <asp:ListView ID="lstPollsDetails" runat="server" OnItemCommand="lstPollsDetails_ItemCommand" OnItemDataBound="lstPollsDetails_ItemDataBound">
                           <LayoutTemplate>
                              <div id="itemPlaceHolder" runat="server">
                              </div>
                           </LayoutTemplate>
                           <ItemTemplate>
                              <div  class="card card-list-con blog-list" id="Pollrow" runat="server">
                                 <div class="list-group-item top-list">
                                    <div class="post-con">
                                       <div class="post-body">
                                          <span class="more-btn float-right edit-btn-fix" id="spanEditDelete" visible="false" runat="server">
                                           <span class="dropdown">
                                            <a href="#" id="dropdownMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                             <img src="images/more.svg" alt="" class="more-btn"></a>
                                            <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start">
                                             <asp:LinkButton ID="lnkEdit" runat="server" Font-Underline="false" Visible="false" class="dropdown-item" ClientIDMode="Static"
                                              Text="Edit" ToolTip="Edit" CommandName="Edit Poll" CausesValidation="false">
                                              <span class="lnr lnr-pencil"></span> Edit 
                                             </asp:LinkButton>
                                             <asp:LinkButton ID="lnkDelete" runat="server" Font-Underline="false" Visible="false" class="dropdown-item hide-body-scroll"
                                              ClientIDMode="Static" OnClientClick="javascript:DeletedivOpen();"
                                              Text="Delete" ToolTip="Delete" CommandName="DeletePoll" CausesValidation="false">
                                              <span class="lnr lnr-trash"></span> Delete
                                             </asp:LinkButton>
                                            </span>
                                           </span>
                                          </span>
                                          <asp:HiddenField ID="hdnPollId" Value='<%# Eval("intPollId") %>' runat="server" />
                                          <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' runat="server" />
                                          <asp:HiddenField ID="hdnstrVoteType" Value='<%#Eval("strVoteType") %>' runat="server" />
                                          <asp:UpdatePanel ID="updels" runat="server">
                                           <ContentTemplate>
                                            <h3>
                                             <asp:LinkButton ClientIDMode="Static" ToolTip="View Poll Details" ID="lnkPollName" Text='<%# Eval("strPollName") %>'
                                              Font-Underline="false" CommandName="Get Poll" runat="server"></asp:LinkButton>
                                            </h3>
                                            <ul class="small-date">
                                             <li>
                                              by 
                                              <asp:LinkButton ID="lblPostlink" Text='<%# Eval("UserName") %>' class="" CommandName="Details" runat="server">
                                              </asp:LinkButton>
                                             </li>
                                             <li>
                                              <asp:Label ID="lblDate" Text='<%# Eval("dtAddedOn") %>' runat="server"></asp:Label>
                                             </li>
                                            </ul>
                                             <%--//Anurag debugged--%>
                                            <div id="trDescription" runat="server">
                                             <asp:Label ID="lblDescription" Text='<%# Eval("strDescription") %>' runat="server"></asp:Label>
                                            </div>
                                       
                                       <div class="row">
                                       <div class="post-footer">
                                       <ul class="list-inline">
                                       <li class="list-inline-item">
                                        <div class="active-toogle">
                                         <span class="like-btn"></span>
                                          <asp:Label ID="lblVoters" runat="server" Text='<%#string.IsNullOrEmpty(Eval("Votes").ToString()) ? "0 Votes" : Eval("Votes").ToString() + ((Eval("Votes").ToString() == "1") ? " Vote" : " Votes") %>'></asp:Label><%--Text='<%# Eval("Votes") %>' &nbsp;Votes--%>
                                        </div>
                                       </li>
                                       <li class="list-inline-item"><div class="active-toogle">
                                           <span class="calender-view"></span>
                                           <asp:Label ID="lblExpiredt" runat="server" Text='<%# Eval("dtPollExpireDate") %>'></asp:Label>&nbsp;
                                           <asp:Label ID="lblExpireTime" Text='<%# Eval("strPollExpireTime") %>' runat="server"></asp:Label>
                                        </div></li>
                                       </ul>
                                        </div>
                                           </div>
                                       </ContentTemplate>
                                       <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="lnkDelete" EventName="Click" />
                                       </Triggers>
                                       </asp:UpdatePanel>
                                       
                                       </div>
                                    </div>
                                 </div>
                              </div>
                           </ItemTemplate>
                        </asp:ListView>
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
                        <div style="display: none" class="innerContainer">
                         <div class="bgLine" id="Div1">
                          &nbsp;
                         </div>
                         <div class="cls">
                         </div>
                         <div id="dvPage" runat="server" class="pagination" clientidmode="Static">
                          <asp:LinkButton ID="lnkFirst" runat="server" CssClass="PagingFirst" OnClick="lnkFirst_Click"></asp:LinkButton>
                          <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">&lt;&lt;</asp:LinkButton>
                          <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand" OnItemDataBound="rptDvPage_ItemDataBound">
                           <ItemTemplate>
                            <asp:LinkButton ID="lnkPageLink" CssClass="Paging" runat="server" ClientIDMode="Static" CommandName="PageLink" Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                           </ItemTemplate>
                          </asp:Repeater>
                          <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">&gt;&gt;</asp:LinkButton>
                          <asp:LinkButton ID="lnkLast" runat="server" class="background_none" OnClick="lnkLast_Click"><img src="images/spacer.gif" class="last" /></asp:LinkButton>
                          <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                          <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                          <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                          <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                          <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                         </div>
                        </div>
                        <asp:HiddenField ID="HiddenField6" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="HiddenField7" ClientIDMode="Static" runat="server" />
                     </div>
                  </div>
               </div>
             
                  <!--groups top box starts-->
                  <asp:UpdatePanel ID="updatedoc" runat="server" UpdateMode="Conditional">
                     <ContentTemplate>
                        <Group:GroupDetails ID="GroupDetails1" runat="server" />
                     </ContentTemplate>
                  </asp:UpdatePanel>
                  <!--groups top box ends-->
              
               <!--left box ends-->
            </div>
         </div>
         <!--left verticle search list ends-->
         <script type="text/jscript">
            function Cancel() {
                document.getElementById("divCancelPoll").style.display = 'none';
                return false;
            }
         </script>
         <script type="text/javascript">
            function messageClosePoll() {
                document.getElementById("divSuccessPolls").style.display = 'none';
            }
         </script>
         <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
         <script type="text/javascript">
            $(document).ready(function() {
                var ID = "#" + $("#hdnLoader").val();
                $(ID).focus();
            });
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
            $(document).ready(function() {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function() {
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
         </script>
      </ContentTemplate>
      <Triggers>
         <asp:AsyncPostBackTrigger ControlID="LinkButton1" />
         <asp:AsyncPostBackTrigger ControlID="LinkButton2" />
         <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
         <asp:AsyncPostBackTrigger ControlID="lnkDeleteCancel" />
      </Triggers>
   </asp:UpdatePanel>
   <script type="text/javascript" language="javascript">
      $(document).ready(function() {
          $("#lblNoMoreRslt").css("display", "none");
          $("#imgLoadMore").css("display", "none");
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
      $(document).ready(function() {
          if ($("#lblNotifyCount").text() == '0') {
              document.getElementById("divNotification1").style.display = "none";
          }
          if ($("#lblInboxCount").text() == '0') {
              document.getElementById("divInbox").style.display = "none";
          }
      });
   </script>
   <script type="text/javascript">
      $(document).ready(function() {
          var showChar = 150;
          var ellipsestext = "...";
          var moretext = "More";
          var lesstext = "less";
          $('.more').each(function() {
              var content = $(this).html();
              if (content.length > showChar) {
                  var c = content.substr(0, showChar);
                  var h = content.substr(showChar - 1, content.length - showChar);
                  var html = c + '<span class="moreelipses">' + ellipsestext + '</span>&nbsp;<span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="morelink">' + moretext + '</a></span>';
                  $(this).html(html);
              }
          });
          $(".morelink").click(function() {
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
      });
   </script>
   <script type="text/javascript">
      $(document).ready(function() {
          $('#LinkButton2').on('click', function() {
              $('#LinkButton1').removeClass('recentActive');
              $(this).addClass('recentActive');
          });
      });
   </script>
   <script type="text/javascript">
      $(document).ready(function() {
          $('#lnkShare').click(function() {
              $('#divDeletesucess').css("display", "none")
              $('#divPopupMember').css("display", "none")
              $('#divSuccessAcceptMember').css("display", "none")
              $('#dvPopup').css("display", "none")
              $('#divSuccessMess').css("display", "none")
          });
      
          $('#divCancelPoll').center();
      });
      $(document).ready(function() {
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function() {
              $('#lnkShare').click(function() {
                  $('#divDeletesucess').css("display", "none")
                  $('#divPopupMember').css("display", "none")
                  $('#divSuccessAcceptMember').css("display", "none")
                  $('#dvPopup').css("display", "none")
                  $('#divSuccessMess').css("display", "none")
              });
      
              $('#divCancelPoll').center();
          });
          $(document).on('click', '.mobile_tab_icon', function() {
              $('ul.group_p').slideToggle('slow');
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
      
      function DeletedivOpen() {
          $('#divDeletesucess').css("display", "block");
      }
   </script>
   <script type="text/javascript">
      $(document).ready(function() {
          $("span.spEditPoll").click(function() {
              $(this).children('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $("span.spDeletePoll").click(function() {
              $(this).children('#lnkDelete').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $('#lnkDeleteConfirm').click(function(e) {
              $(this).css("box-shadow", "0px 0px 5px #00B7E5");
          });
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function() {
              $("span.spEditFolder").click(function() {
                  $(this).children('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spDeleteFolder").click(function() {
                  $(this).children('#lnkDelete').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $('#lnkDeleteConfirm').click(function(e) {
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
          });
      });
   </script>
</asp:Content>