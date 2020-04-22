<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
   CodeFile="Polls-VoteComment.aspx.cs" Inherits="Polls_VoteComment" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Src="~/UserControl/Groups-m.ascx" TagName="GroupDetailsM" TagPrefix="GroupM" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
   Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
  <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
  <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <asp:UpdatePanel ID="updateEvents" runat="server" ChildrenAsTriggers="true">
    <Triggers>
      <asp:AsyncPostBackTrigger ControlID="lnkVote" />
      <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
      <asp:AsyncPostBackTrigger ControlID="lnkDeleteCancel" />
    </Triggers>
    <ContentTemplate>
      <div class="">
        <div id="divDeletesucess" clientidmode="Static" runat="server" class="modal backgroundoverlay" style="display:none;">
          <div id="divDeleteConfirm" runat="server" class="modal-dialog modal-dialog-centered" clientidmode="Static">
           <div class="modal-content ">
            <div class="modal-header">
             <h5 class="modal-title">Delete Confirmation</h5>
             <asp:Label ID="Label3" runat="server"></asp:Label>
            </div>
            <div class="modal-body">
             <asp:Label ID="Label4" runat="server" Text="Do you want to delete ?"></asp:Label>
            </div>
            <div class="modal-footer test-right">
             <asp:LinkButton ID="lnkDeleteCancel" runat="server" ClientIDMode="Static" class="btn btn-outline-primary m-r-15 add-scroller" Text="Cancel" OnClientClick="javascript:divCancels();return false;"></asp:LinkButton>
             <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" CssClass="btn btn-primary success-popup" OnClick="lnkDeleteConfirm_Click"></asp:LinkButton>
            </div>
          </div>
         </div>
        </div>
        <div class="main-section-inner">
          <div class="panel-cover clearfix">
            <div class="center-panel"> 
              <!--groups top box starts--> 
              <!--groups top box ends--> 
              <!--left box starts-->
              <asp:UpdatePanel ID="update" runat="server">
              <ContentTemplate>
              <div class="custom-nav-con group-page-tab">
                 <GroupM:GroupDetailsM ID="grpDetailsM" runat="server" />
                <ul class="custom-nav-control nav nav-tabs">
                  <li class="nav-item">
                    <asp:LinkButton ID="lnkProfile" runat="server" class="nav-link" Text="Profile" ClientIDMode="Static" OnClick="lnkProfile_Click"></asp:LinkButton>
                  </li>
                  <li class="nav-item" id="DivHome" runat="server">
                    <asp:LinkButton class="nav-link" ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                  </li>
                  <li class="nav-item" id="DivForumTab" runat="server" clientidmode="Static">
                    <asp:LinkButton class="nav-link" ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static" OnClick="lnkForumTab_Click"></asp:LinkButton>
                  </li>
                  <li class="nav-item" id="DivUploadTab" runat="server" clientidmode="Static">
                    <asp:LinkButton class="nav-link" ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static" OnClick="lnkUploadTab_Click"></asp:LinkButton>
                  </li>
                  <li class="nav-item" id="DivPollTab" runat="server" clientidmode="Static">
                    <asp:LinkButton class="nav-link active" ID="lnkPollTab" runat="server" Text="Poll" ClientIDMode="Static"  OnClick="lnkPollTab_Click"></asp:LinkButton>
                  </li>
                  <li class="nav-item" id="DivEventTab" runat="server" clientidmode="Static">
                    <asp:LinkButton  class="nav-link" ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static" OnClick="lnkEventTab_Click"></asp:LinkButton>
                  </li>
                  <li class="nav-item" id="DivMemberTab" runat="server" clientidmode="Static">
                    <asp:LinkButton  class="nav-link" ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static" OnClick="lnkEventMemberTab_Click"></asp:LinkButton>
                  </li>
                </ul>
              </div>
              <div class="back-link-cover mt-2 mb-3">
              <asp:LinkButton ID="lnkBack" runat="server" class="back-link back-arrow-height" OnClick="lnkBack_Click" Text="Back"> <span class="lnr lnr-arrow-left "></span> Back </asp:LinkButton>
           </div>
               <asp:ListView ID="lstPoll" runat="server" OnItemDataBound="lstPoll_ItemDataBound" OnItemCommand="lstPoll_ItemCommand">
                <LayoutTemplate>
                 <div id="itemPlaceHolder" runat="server"> </div>
                </LayoutTemplate>
                <ItemTemplate>
                 <div  class="card card-list-con polls-vote-comment">
                  <div class="list-group-item top-list">
                   <div class="post-con">
                    <asp:HiddenField ID="hdnRegistrationId" Value='<%# Eval("intRegistrationId") %>' runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdnVotingPattern" runat="server" Value='<%# Eval("strVotingPattern") %>' ClientIDMode="Static" />
                    <div>
                    <div class="thumbnail" style="display: none;"> 
                     <img id="imgprofile" runat="server" src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' />
                    </div>
                    <div class="post-body">
                     <span class="more-btn float-right edit-btn-fix" ID="spanEditDelete" Visible="false" runat="server">
                      <span class="dropdown">
                       <a href="#" id="dropdownMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <img src="images/more.svg" alt="" class="more-btn"></a>
                        <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start">
                        <asp:LinkButton ID="lnkEdit1" runat="server" Font-Underline="false" Visible="false" class="dropdown-item" ToolTip="Edit" Text="Edit" 
                         CommandName="Edit Poll" CausesValidation="false" ClientIDMode="Static"><i class="lnr lnr-pencil" aria-hidden="true"></i> Edit
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete1" runat="server" Font-Underline="false" Visible="false" class="dropdown-item hide-body-scroll" ToolTip="Delete" Text="Delete" 
                         CommandName="DeletePoll" CausesValidation="false" ClientIDMode="Static"><i class="lnr lnr-trash" aria-hidden="true"></i> Delete
                        </asp:LinkButton>
                       </span>
                      </span>
                     </span>
                     <h3>
                      <asp:Label ID="lnkPollName" Text='<%# Eval("strPollName") %>' runat="server"></asp:Label>
                     </h3>
                     <asp:UpdatePanel ID="updele" runat="server">
                      <ContentTemplate>
                       <ul class="small-date">
                        <li>by <asp:LinkButton ID="lblPostlink" runat="server" ToolTip="View profile" Text='<%# Eval("UserName") %>' CommandName="Details"></asp:LinkButton></li>
                        <li><asp:Label ID="lblDate" runat="server" Text='<%# Eval("dtAddedOn") %>'></asp:Label></li>
                       </ul>
                       <asp:Label ID="lblDescription" Text='<%# Eval("strDescription") %>' class="m-t-5 d-inline-block" runat="server"></asp:Label>
                       <div class="row">
                        <div class="post-footer">
                         <ul class="list-inline">
                          <li class="list-inline-item"><div class="active-toogle">
                           <span class="like-btn"></span>
                           <asp:Label ID="lblVoters" runat="server" Text='<%#string.IsNullOrEmpty(Eval("Votes").ToString()) ? "0 Votes" : Eval("Votes").ToString() + ((Eval("Votes").ToString() == "1") ? " Vote" : " Votes") %>'></asp:Label></div></li>
                          <li class="list-inline-item">
                           <div class="active-toogle">
                            <span class="calender-view"></span>
                            <asp:Label ID="lblExpiredt" runat="server" Text='<%# Eval("dtPollExpireDate") %>'></asp:Label>&nbsp;
                            <asp:Label ID="lblExpireTime" runat="server" Text='<%# Eval("strPollExpireTime") %>'></asp:Label>
                           </div>
                          </li>
                         </ul>
                        </div>
                       </div>
                      </ContentTemplate>
                      
                      <Triggers>
                       <asp:AsyncPostBackTrigger ControlID="lnkDelete1" />
                      </Triggers>
                     </asp:UpdatePanel>
                         <asp:Panel ID="pnlRadio" runat="server" Visible="false" ClientIDMode="Static">
                         <ul class="list-group-flush list-group votinglist">
                            <li class="list-group-item"  id="tr1" runat="server">
                             <div class="questionbox">
                              <asp:RadioButton ID="rdbQ1" runat="server" Text='<%#Eval("stroption1") %>' CssClass="radioTitle" ClientIDMode="Static" title='<%#Eval("stroption1") %>' Value='<%#Eval("stroption1") %>' GroupName="grppoll" />
                             </div>
                            </li>
                            <li class="list-group-item"  id="tr2" runat="server">
                             <div class="questionbox">
                              <asp:RadioButton ID="rdbQ2" runat="server" title='<%#Eval("stroption2") %>' Text='<%#Eval("stroption2") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption2") %>' GroupName="grppoll"></asp:RadioButton></div>
                              <li class="list-group-item"  id="tr3" runat="server">
                                 <div class="questionbox"><asp:RadioButton ID="rdbQ3" runat="server" Text='<%#Eval("stroption3") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption3") %>' GroupName="grppoll"></asp:RadioButton></div>
                              </li>
                              <li class="list-group-item"  id="tr4" runat="server">
                                 <div class="questionbox"><asp:RadioButton ID="rdbQ4" runat="server" Text='<%#Eval("stroption4") %>' title='<%#Eval("stroption4") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption4") %>' GroupName="grppoll"></asp:RadioButton></div>
                              </li>
                              <li class="list-group-item"  id="tr5" runat="server">
                                 <div class="questionbox"><asp:RadioButton ID="rdbQ5" runat="server" Text='<%#Eval("stroption5") %>' CssClass="radioTitle" title='<%#Eval("stroption5") %>' ClientIDMode="Static" Value='<%#Eval("stroption5") %>' GroupName="grppoll"></asp:RadioButton></div>
                              </li>
                              <li class="list-group-item"  id="tr6" runat="server">
                                 <div class="questionbox"><asp:RadioButton ID="rdbQ6" runat="server" Text='<%#Eval("stroption6") %>' title='<%#Eval("stroption6") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption6") %>' GroupName="grppoll"></asp:RadioButton></div>
                              </li>
                              <li class="list-group-item"  id="tr7" runat="server">
                                 <div class="questionbox"><asp:RadioButton ID="rdbQ7" runat="server" Text='<%#Eval("stroption7") %>' title='<%#Eval("stroption6") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption7") %>' GroupName="grppoll"></asp:RadioButton></div>
                              </li>
                              <li class="list-group-item"  id="tr8" runat="server">
                                 <div class="questionbox"><asp:RadioButton ID="rdbQ8" runat="server" Text='<%#Eval("stroption8") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption8") %>' title='<%#Eval("stroption8") %>' GroupName="grppoll"></asp:RadioButton></div>
                              </li>
                              <li class="list-group-item"  id="tr9" runat="server">
                                 <div class="questionbox"><asp:RadioButton ID="rdbQ9" runat="server" Text='<%#Eval("stroption9") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption9") %>' title='<%#Eval("stroption9") %>' GroupName="grppoll"></asp:RadioButton></div>
                              </li>
                              <li class="list-group-item"  id="tr10" runat="server">
                                 <div class="questionbox"><asp:RadioButton ID="rdbQ10" runat="server" Text='<%#Eval("stroption10") %>' title='<%#Eval("stroption10") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption10") %>' GroupName="grppoll"></asp:RadioButton></div>
                              </li>
                            </li>
                         </ul>
                         
                       </asp:Panel>
                      <asp:Panel ID="pnlCheckBox" runat="server" Visible="false"  ClientIDMode="Static">
                       <ul class="list-group-flush list-group votinglist">
                          <li class="list-group-item" id="tr11" runat="server">
                             <div class="questionbox"><asp:CheckBox ID="chkQ1" runat="server" Text='<%#Eval("stroption1") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption1") %>' GroupName="grppoll" /></div>
                          </li>
                          <li class="list-group-item" id="tr12" runat="server">
                             <div class="questionbox"><asp:CheckBox ID="chkQ2" runat="server" Text='<%#Eval("stroption2") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption2") %>' GroupName="grppoll"></asp:CheckBox></div>
                          </li>
                          <li class="list-group-item" id="tr13" runat="server">
                             <div class="questionbox"><asp:CheckBox ID="chkQ3" runat="server" Text='<%#Eval("stroption3") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption3") %>' GroupName="grppoll"></asp:CheckBox></div>
                          </li>
                          <li class="list-group-item" id="tr14" runat="server">
                             <div class="questionbox"><asp:CheckBox ID="chkQ4" runat="server" Text='<%#Eval("stroption4") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption4") %>' GroupName="grppoll"></asp:CheckBox></div>
                          </li>
                          <li class="list-group-item" id="tr15" runat="server">
                             <div class="questionbox"><asp:CheckBox ID="chkQ5" runat="server" Text='<%#Eval("stroption5") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption5") %>' GroupName="grppoll"></asp:CheckBox></div>
                          </li>
                          <li class="list-group-item" id="tr16" runat="server">
                             <div class="questionbox"><asp:CheckBox ID="chkQ6" runat="server" Text='<%#Eval("stroption6") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption6") %>' GroupName="grppoll"></asp:CheckBox></div>
                          </li>
                          <li class="list-group-item" id="tr17" runat="server">
                             <div class="questionbox"><asp:CheckBox ID="chkQ7" runat="server" Text='<%#Eval("stroption7") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption7") %>' GroupName="grppoll"></asp:CheckBox></div>
                          </li>
                          <li class="list-group-item" id="tr18" runat="server">
                             <div class="questionbox"><asp:CheckBox ID="chkQ8" runat="server" Text='<%#Eval("stroption8") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption8") %>' GroupName="grppoll"></asp:CheckBox></div>
                          </li>
                          <li class="list-group-item" id="tr19" runat="server">
                             <div class="questionbox"><asp:CheckBox ID="chkQ9" runat="server" Text='<%#Eval("stroption9") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption9") %>' GroupName="grppoll"></asp:CheckBox></div>
                          </li>
                          <li class="list-group-item" id="tr20" runat="server">
                             <div class="questionbox"><asp:CheckBox ID="chkQ10" runat="server" Text='<%#Eval("stroption10") %>' CssClass="radioTitle" ClientIDMode="Static" Value='<%#Eval("stroption10") %>' GroupName="grppoll"></asp:CheckBox></div>
                          </li>
                       </ul>
                        
                      </asp:Panel>
                 </ItemTemplate>
              </asp:ListView>
                  <asp:HiddenField ID="hdnBarOptions" runat="server" ClientIDMode="Static" />
                  <asp:HiddenField ID="hdnBarValues" runat="server" ClientIDMode="Static" />
                <div style="display:none" id="dvNewBarChart" class="votingresult">
                    
                </div>
                  <div id="barChartWrapper" style="display:none;">
              <div style="display: none;" clientidmode="Static" id="divBarChart" runat="server">
                <asp:Panel ID="pnlBarChart" runat="server" Visible="false" Width="100%">
                  
                </asp:Panel>
              </div>
              </div>
              <asp:Label ID="lblMessageVote" runat="server" ClientIDMode="Static" Visible="false" ></asp:Label>
              <div class="margin_top_polls">
                <asp:UpdatePanel ID="update1" runat="server">
                  <ContentTemplate>
                    <div class="text-right mb-4 mt-2">
                    <asp:LinkButton ID="lnkVote" Text="Vote"  runat="server" CssClass="btn btn-primary" ClientIDMode="Static" OnClick="lnkVote_Click" OnClientClick="$('#lnkVote').addClass('disabled');"></asp:LinkButton>
                    </div>
                  </ContentTemplate>
                </asp:UpdatePanel>
              </div>
              <span>
              </span>
              <asp:HiddenField ID="divcommrnt" runat="server" ClientIDMode="Static" />
            </div>
          </div>
       </div>

       <div id="dvShowComments" runat="server" ClientIDMode="Static" class="post-form text-right">          
        <textarea id="txtComment" runat="server" ClientIDMode="Static" placeholder="Comment" validationgroup="PollComment" onfocus="javascript:PostEnable.bind(this)();" 
         onblur="javascript:PostDisable.bind(this)();" class="form-control question-area h-34" maxlength="500" />
         <%--eventTitleField commentA form-control question-area--%>
    
        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtComment" Display="Dynamic" ValidationGroup="PollComment" 
         ErrorMessage="Please enter comment" ForeColor="Red"></asp:RequiredFieldValidator>--%>
        <asp:Label ID="lblMessage" runat="server" ClientIDMode="Static" Visible="false"></asp:Label>
        <asp:Button ID="postbtn" runat="server" ClientIDMode="Static" Text="Post" CausesValidation="true" ValidationGroup="PollComment" OnClick="lnkPost_Click" 
         OnClientClick="createPollCheck()" CssClass="btn btn-primary post-btn m-t-10 hide-body-scroll"></asp:Button> <%--btn-primary margin_top_p_c btn btn-primary--%>
       </div>
       
       <span id="totalComments" runat="server" class="answer-counter"></span>
        
      </div>
        
          <asp:UpdatePanel ID="upcomment" class="upcomment" runat="server">
            <ContentTemplate>
              <asp:HiddenField ID="hdnDeletePostQuestionID" Value="" ClientIDMode="Static" runat="server" />
              <asp:HiddenField ID="hdnstrQuestionDescription" ClientIDMode="Static" runat="server" />
              <asp:ListView ID="lstPollsComment" OnItemDataBound="lstPollsComment_ItemDataBound" OnItemCommand="lstPollsComment_ItemCommand" runat="server">
                <LayoutTemplate>
                 <table cellpadding="0" cellspacing="0" width="100%">
                  <tr id="itemPlaceHolder" runat="server"></tr>
                 </table>
                </LayoutTemplate>

                <ItemTemplate>
                  <div class="list-group-item">
                  <div class="list_style_table_row polls-comments-page post-con"><asp:HiddenField ID="hdnRegistrationId" Value='<%# Eval("intRegistrationId") %>' runat="server" ClientIDMode="Static" />
                   <asp:HiddenField ID="hdnintCommentId" Value='<%# Eval("intCommentId") %>' runat="server" ClientIDMode="Static" />
                    
                   <div class="post-header">
                    <span class="avatar-img">
                     <img id="imgprofile" runat="server" class="rounded-circle" src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' />
                     <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                    </span>
                    <span class="user-name-date">
                     <span class="user-name">
                      <asp:LinkButton class="un-anchor" ID="lblPostlink" Text='<%# Eval("UserName") %>'
                       CommandName="Details" runat="server">
                      </asp:LinkButton>
                     </span> 
                     <span class="date"><asp:Label ID="lblDate" Text='<%# Eval("dtAddedOn") %>' runat="server"></asp:Label></span> 
                    </span>
                    <span ID="divEditDelete" runat="server" class="more-btn float-right" Visible="false">
                     <span class="dropdown show">
                     <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                      <img src="images/more.svg" alt="" class="more-btn">
                     </a>
                     <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start">
                                   
                     <asp:UpdatePanel ID="updele" runat="server">
                      <ContentTemplate>
                       <div class="editDeleteMore">
                       <asp:HiddenField ID="hdnintPostQuestionIdelet" Value='<%# Eval("intCommentId") %>' ClientIDMode="Static" runat="server" />
                       <asp:HiddenField ID="lnkstrQuestionDescription" runat="server" Value='<%#Eval("strComment") %>' ClientIDMode="Static"></asp:HiddenField>
                       <span class="spEditForum">
                        <asp:LinkButton ID="lnkEdits" runat="server" class="dropdown-item" Font-Underline="false" Visible="false"
                         ClientIDMode="Static" ToolTip="Edit" Text="Edit" CommandName="Edit Poll" CausesValidation="false">
                         <span class="lnr lnr-pencil"></span> Edit
                        </asp:LinkButton>
                       </span>
                      <span class="spDeleteForum">
                       <asp:LinkButton ID="lnkDeletes" runat="server" Text="Delete" Font-Underline="false" Visible="false" class="dropdown-item hide hide-body-scroll" ToolTip="Delete"
                        ClientIDMode="Static" OnClientClick="javascript:docdelete();" CommandName="DeletePoll" CausesValidation="false">                         
                        <span class="lnr lnr-trash"></span> Delete 
                       </asp:LinkButton>
                       </span>
                       </div>
                      </ContentTemplate>
                      <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkDeletes" />
                        <asp:AsyncPostBackTrigger ControlID="lnkEdits" />
                      </Triggers>
                     </asp:UpdatePanel>
                     </span>
                     </span>
                    </span>
                   </div>  
                   <div class="post-body">
                    <p class="breakallwords">
                     <asp:Label ID="lblName" Text='<%# Eval("strComment") %>' runat="server" CssClass="comment moreQA"></asp:Label>
                    </p>
                   </div>                       
                   </div>
                   </div>   
                   
                  </div>
                  </div>                 
                </ItemTemplate>
              
               </asp:ListView>
             </ContentTemplate>
           </asp:UpdatePanel>
          </div>
        </div>
        <div class="">
         <div class="cls"> </div>
         <div id="dvPage" runat="server" class="pagination" clientidmode="Static">
          <asp:LinkButton ID="lnkFirst" runat="server" CssClass="PagingFirst" OnClick="lnkFirst_Click"> </asp:LinkButton>
          <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">&lt;&lt;</asp:LinkButton>
          <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand" OnItemDataBound="rptDvPage_ItemDataBound">
           <ItemTemplate>
            <asp:LinkButton ID="lnkPageLink" CssClass="Paging" runat="server" ClientIDMode="Static" CommandName="PageLink" Text='<%#Eval("intPageNo") %>'>
            </asp:LinkButton>
           </ItemTemplate>
          </asp:Repeater>
          <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">&gt;&gt;</asp:LinkButton>
          <asp:LinkButton ID="lnkLast" runat="server" Style="background: none" OnClick="lnkLast_Click"><img src="images/spacer.gif" class="last" /></asp:LinkButton>
          <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
         </div>
        </div>
    
      </ContentTemplate>
      </asp:UpdatePanel>
      <!--left box ends--> 
      
      <!--left verticle search list ends-->
     
      <div class="right-panel-back-layer"></div>
      <div class="right-panel">
       <asp:UpdatePanel ID="uppal" runat="server">
        <ContentTemplate>
         <Group:GroupDetails ID="GroupDetails1" runat="server" />
        </ContentTemplate>
       </asp:UpdatePanel>
      </div>
      </div>
      </div>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
  <script type="text/javascript">
     
      function showNewBarChart() {
          var colors = ['purple', 'steelblue', 'aqua', 'navy', 'green', 'blue', 'red'];
          if ($('#hdnBarOptions').val() == "" || $('#divBarChart').css('display') == 'none') {
              return; // nothing to do.
          }

          var barOptions = $('#hdnBarOptions').val().split('%@%');
          var barValues = $('#hdnBarValues').val().split('%@%');

          //barOptions = barValues = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

          var total = 0;
          for (var i = 0; i < barOptions.length; i++) {
              barValues[i] = parseInt(barValues[i]);
              total += barValues[i];
          }
   
          if (total == 0) {
              return;
          }
          $('#pnlRadio').hide();
          $('#pnlCheckBox').hide();
          var color_style = '';
          var finalToAddHTML = '<div class="d-table w-100">';
          for (var i = 0; i < barOptions.length; i++) {
             color_style = '';
              var percentage = Math.round((barValues[i] * 10000) / total)/100;
              var color = colors[i % colors.length];
              if (percentage != 0) {
                color_style = 'color:white;';
              }
              finalToAddHTML += '<div class="d-table-row">' +
                                    '<div class="d-table-cell text-left max-20 pr-2" title="'+barOptions[i]+'" >'+barOptions[i]+'</div>' +
                                    '<div class="d-table-cell w-100 pb-3">' +
                                        '<div class="progress" style="height:25px;">' +
                                            '<div class="progress-bar"  role="progressbar" style="'+ color_style+'width: '+percentage+'%;background:'+ color +';" aria-valuenow="'+percentage+'" aria-valuemin="0" aria-valuemax="100">'+percentage+'%</div>'+
                                        '</div>' +
                                    '</div>' +
                                    
                                '</div>';
                          
          }
          finalToAddHTML += '</div>';
          $('#dvNewBarChart').show();
          $('#dvNewBarChart').html(finalToAddHTML);
      }
      $(document).ready(function () {
          showNewBarChart();

          $('#txtQuestion').keypress(function() {
              $('#txtQuestion').css('font-weight', 'normal');
          });
      
          $('#txtDescription').keypress(function() {
              $('#txtDescription').css('font-weight', 'normal');
          });
      
          if ($('#divcommrnt').val() == '1') {
              $('#dvShowComments').css("margin-top", "1%");
          }
          $('#lnkDelete1').click(function(e) {
              $(this).css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $('#lnkEdit1').click(function(e) {
              $(this).css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $("span.spEditForum").click(function() {
              $(this).children('#lnkEdits').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $("span.spDeleteForum").click(function() {
              $(this).children('#lnkDeletes').css("box-shadow", "0px 0px 5px #00B7E5");
              setTimeout(
                  function() {
                      $(this).children('#lnkDeletes').css("box-shadow", "0px 0px 0px #00B7E5");
                  }, 1000);
          });
      });
      $(document).ready(function() {
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              showNewBarChart();
              $('#txtQuestion').keypress(function() {
                  $('#txtQuestion').css('font-weight', 'normal');
              });
      
              $('#txtDescription').keypress(function() {
                  $('#txtDescription').css('font-weight', 'normal');
              });
              if ($('#divcommrnt').val() == '1') {
                  $('#dvShowComments').css("margin-top", "1%");
              }
              $('#lnkDelete1').click(function(e) {
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $('#lnkEdit1').click(function(e) {
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spEditForum").click(function() {
                  $(this).children('#lnkEdits').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spDeleteForum").click(function() {
                  $(this).children('#lnkDeletes').css("box-shadow", "0px 0px 5px #00B7E5");
                  setTimeout(
                      function() {
                          $(this).children('#lnkDeletes').css("box-shadow", "0px 0px 0px #00B7E5");
                      }, 1000);
              });
          });
      });
   </script> 
  <script type="text/javascript">

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
      $(document).ready(function() {
          showMoreLinks();
      });
      
      $(document).ready(function() {
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function() {
              showMoreLinks();
          });
      });
   </script> 
  <script type="text/javascript">
      $(document).ready(function() {
          $("#lnkVote").click(function() {
              $("#divBarChart").slideDown();
              $("#divBarChart").slideDown();
          });
          $("div.editDeleteMore").click(function() {
              $('#hdnDeletePostQuestionID').val($(this).children('#hdnintPostQuestionIdelet').val());
              $('#hdnstrQuestionDescription').val($(this).children('#lnkstrQuestionDescription').val());
          });
          $('#lnkDeleteConfirm').click(function(e) {
              $(this).css("box-shadow", "0px 0px 5px #00B7E5");
          });
      });
      
      $(document).ready(function() {
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function() {
              $("#lnkVote").click(function() {
                  $("#divBarChart").slideDown();
                  $("#divBarChart").slideDown();
              });
      
              $("div.editDeleteMore").click(function() {
                  $('#hdnDeletePostQuestionID').val($(this).children('#hdnintPostQuestionIdelet').val());
                  $('#hdnstrQuestionDescription').val($(this).children('#lnkstrQuestionDescription').val());
              });
              $('#lnkDeleteConfirm').click(function(e) {
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
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

      var clicked = false;
      function disablebtn() {
         if (clicked)
             return false;
         clicked = true;
         $(this).addClass("disabled");
         setTimeout(function () { clicked = false; }, 3000);
         setTimeout(function () { return }, 400);
        }


      function createPollCheck() {
          if ($('#txtComment').val() != "") {
              showLoader1();
          }
      }
      function PostEnable() {
            $(this).removeClass("h-34");
            $(this).height(60);
          $('#postbtn').show();
          $('#lblMessage').hide();
      }
      function PostDisable() {
          if (this.value.trim() == "") {
              $(this).addClass("h-34");
              $('#postbtn').hide();
          }             
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
      function checkQuestionToggle() {
          $('.questionbox input:checked').parent().addClass("active");
        //If another radio button is clicked, add the select class, and remove it from the previously selected radio
        $('.questionbox input').click(function () {

          $('.questionbox input:not(:checked)').parent().removeClass("active");
          $('.questionbox input:checked').parent().addClass("active");
          //$(this).parent().addClass("active");
        });

      }
      $(document).ready(function() {
          checkQuestionToggle();
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function() {
              checkQuestionToggle();
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
    //   $(".question-area").focusin(function() {
    //    $(this).removeClass("h-34");
    //    $(this).height(60);
    //   $(".postbtn").show();    
    //});

    //$(".question-area").focusout(function() {
    //   $(this).addClass("h-34");
    //   $(".postbtn").hide();    
    //});
      function openPollSuccessPopup(title, msg) {
          showSuccessPopup(title, msg);
      }

   </script> 
</asp:Content>
