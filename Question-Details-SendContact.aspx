<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Question-Details-SendContact.aspx.cs" Inherits="Question_Details_SendContact" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-section-inner">
         <span class="m-aside-trigger mt-0 mb-0">
                            <span class="lnr lnr-arrow-left"></span>
                            <span class="avatar-text">Related Questions</span>
                        </span>
        <div class="back-link-cover">
         <asp:LinkButton ID="lnkBack" Font-Underline="false" runat="server" CssClass="back-link back-arrow-height"
          OnClick="lnkBack_click"><span class="lnr lnr-arrow-left"></span>Back to Q &amp; A</asp:LinkButton>
           
        </div>
      <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="modal-dialog modal-dialog-centered">
            <ContentTemplate>--%>
        <div class="panel-cover clearfix">
            <div class="center-panel">
                <div class="card card-list-con">
                    <asp:UpdatePanel ID="pnlParent" runat="server">
                    <ContentTemplate>
                    <%--New Hidden-fields Added--%>
                    <asp:HiddenField ID="hdnDeletePostQuestionID" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdnstrQuestionDescription" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdnintCommentDelete" runat="server" ClientIDMode="Static" Value='' />
                    <asp:HiddenField ID="hdnCommentEdit" runat="server" ClientIDMode="Static" Value='' />
                    <asp:HiddenField ID="hdnCommentImageSrcId" runat="server" ClientIDMode="Static" Value='' />
                    <div class="list-group list-group-flush">
                     <asp:ListView ID="lstParentQADetails" runat="server" OnItemDataBound="lstParentQADetails_ItemDataBound"
                      OnItemCommand="lstParentQADetails_ItemCommand">
                      <ItemTemplate>
                       <div class="list-group-item top-list question-details-page">
                         <div class="post-con">
                          <div class="post-header">
                                     <span class="question-icon">
                                      <span class="icon">?</span>
                                     </span>
                                    <asp:HiddenField ID="hdnPostQuestionID" Value='<%# Eval("intPostQuestionId") %>'
                                     runat="server" />
                                    <asp:HiddenField ID="hdnintAddedBy" Value='<%# Eval("intAddedBy") %>' runat="server" />
                                    <ul class="que-con">
                                     <li>Question</li>
                                     <li id="liSubCategory" runat="server">   
                                      <asp:ListView ID="lstSubjCategory" runat="server">
                                       <ItemTemplate>
                                        <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                        <asp:Label ID="lnkCatName" Style="text-decoration: none !important;"
                                         Font-Underline="false" runat="server" Text='<%#Eval("strCategoryName")%>'></asp:Label>                                             
                                       </ItemTemplate>
                                      </asp:ListView>
                                     </li>                                                                                  
                                    </ul>
                                   </div>                                      

                                   <div class="post-body mt-2">
                                   <div id="divEditDeleteList" Visible="false" runat="server" class="more-btn edit-btn-fix float-right">
                                    <span class="dropdown">
                                     <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                      <img src="images/more.svg" alt="" class="more-btn">
                                     </a>

                                     <span class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                      <asp:HiddenField ID="hdnintPostQuestionIdelet" Value='<%# Eval("intPostQuestionId") %>' ClientIDMode="Static"
                                       runat="server"  />
                                      <asp:HiddenField ID="lnkstrQuestionDescription" runat="server" Value='<%#Eval("strQuestionDescription") %>' ClientIDMode="Static"></asp:HiddenField>
                                      <asp:LinkButton ID="lnkEdit" Font-Underline="false" Visible="false"
                                       ClientIDMode="Static" ToolTip="Edit" Text="Edit"
                                       class="edit dropdown-item" CommandName="Edit QA" CausesValidation="false" runat="server">
                                       <span class="lnr lnr-pencil"> </span> Edit
                                      </asp:LinkButton>
                                      <asp:LinkButton ID="lnkDeleteQues" Font-Underline="false" Visible="false"
                                       ClientIDMode="Static" ToolTip="Delete" 
                                       class="edit dropdown-item hide-body-scroll" CommandName="Delete Quest" CausesValidation="false" runat="server" Text="Delete">
                                       <span class="lnr lnr-trash"></span> Delete
                                      </asp:LinkButton><%--OnClientClick="javascript:docdelete();return false;"--%>
                                     </span>
                                    </span>
                                   </div>
                                   <h4><asp:Label ID="Label1" CommandName="QADetails" runat="server"
                                    Text='<%#Eval("strQuestionDescription") %>'></asp:Label></h4>
                                   <ul class="small-date question-details-date">
                                    <li>by <asp:LinkButton ID="Label2" Font-Underline="false" runat="server" Text='<%#Eval("AuthorName") %>' CommandName="GoToProfile"></asp:LinkButton></li>
                                    <li ><%#Eval("dtAddedOn") %></li>
                                   </ul>
                                   <asp:HiddenField ID="hdnAuthorId" runat="server" ClientIDMode="Static" Value='<%#Eval("intAddedBy")%>' />
                                  </div>

                                  <div class="post-footer pb-1">
                                    <ul class="list-inline">
                                     <li class="list-inline-item">
                                      <span class="d-flex align-items-center links-like-btn">
                                       <asp:LinkButton runat="server" ID="btnLike" CommandName="LikeForum" class="active-toogle">
                                        <span class="like-btn"></span>
                                       </asp:LinkButton>
                                       <span class="d-flex">
                                       <asp:Label ID="lblTotallike" runat="server" Text=""></asp:Label>
                                       </span>
                                      </span>
                                     </li>
                                     <li class="list-inline-item">
                                      <span class="d-flex align-items-center share-button">
                                       <asp:LinkButton runat="server" ID="btnShare" CommandName="ShareForum" class="hide-body-scroll active-toogle"><span class="share-btn"></span></asp:LinkButton>
                                      <span class="d-flex">
                                       <asp:Label ID="lblShare" runat="server" Text=""></asp:Label>
                                      </span> 
                                      </span></li>
                                     <li class="list-inline-item">
                                      <span class="d-flex align-items-center">
                                       <asp:LinkButton runat="server" ID="btnReply" CommandName="ReplyPost" class="active-toogle cursor-initial"><span class="edit-comment-btn"></span>  </asp:LinkButton><asp:Label ID="lblreply" runat="server" Text=""></asp:Label>
                                      </span>
                                     </li>
                                    </ul>
                                   </div>
                         </div>
                       </div>
                      </ItemTemplate>
                     </asp:ListView>
                     <div class="list-group-item top-list pt-0">
                      <div class="post-form text-right mt-0">
                       <textarea placeholder="Type your answer" runat="server" clientidmode="static" id="CKEditorControl" maxlength="5000" runat="server" onfocus="javascript:PostEnable.bind(this)();" onblur="javascript:PostDisable.bind(this)();" class="form-control question-area h-34"></textarea>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="CKEditorControl"
                         Display="Dynamic" ClientIDMode="Static" ValidationGroup="t" ErrorMessage="Please enter your answer."
                         ForeColor="Red" class="margin_left_seven"></asp:RequiredFieldValidator>
                       <asp:Button ID="btnPost" runat="server" CssClass="btn btn-primary post-btn m-t-10" Text="Post" OnClientClick="disablebtn.bind(this)();showLoader1();" OnClick="btnSave_Click" ClientIDMode="Static"></asp:Button>
                      </div>
                      <span id="totalAwnsers" runat="server" class="answer-counter"></span>
                     </div>
                    </div>
                       
                    <ul class="list-group list-group-flush">
                       <asp:ListView ID="lstAllReplies" runat="server" OnItemCommand="lstAllReplies_ItemCommand"
                        OnItemDataBound="lstAllReplies_ItemDataBound" OnItemCreated="lstAllReplies_ItemCreated">
                        <ItemTemplate>
                        <li class="list-group-item">
                            <div class="post-con">
                                <div class="post-header">
                                    <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hdnQAReplyLikeShareId" Value='<%#Eval("intQAReplyLikeShareId") %>'
                                     ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hdnintPrivateMessage" Value='<%#Eval("intPrivateMessage") %>'
                                     ClientIDMode="Static" runat="server" />
                                    <span class="avatar-img">
                                    <img id="imgprofile" runat="server" class="rounded-circle" src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' />
                                    <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                    </span><span class="user-name-date">
                                    <asp:LinkButton Font-Underline="false" CssClass="user-name" CommandName="Details" ID="Label1"  runat="server" Text='<%#Eval("Name") %>'></asp:LinkButton>
                                    <asp:Label ID="lblDate" CssClass="date" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label> </span>
                                     <span class="more-btn float-right">
                                       <span id="editDelLink" runat="server" class="dropdown">
                                        <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                         <img src="images/more.svg" alt="" class="more-btn" />
                                        </a>
                                        <span class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                         <asp:LinkButton Font-Underline="false" CssClass="dropdown-item" CommandName="Edit Ans" ID="lnkEdit"
                                          runat="server"><span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>
                                         <asp:LinkButton Font-Underline="false" CssClass="dropdown-item hide-body-scroll" CommandName="Delete Ans" OnClientClick="javascript:docdelete();" ID="lnkdelete"
                                          runat="server"><span class="lnr lnr-trash"></span> Delete</asp:LinkButton>
                                        </span>
                                       </span>
                                     </span>
                                </div>
                             
                                <asp:Label ID="lblReplyComment" CssClass="post-body" runat="server" Text='<%#((string)Eval("strComment")).Replace("\n", "<br>") %>'></asp:Label>
                                <div class="post-footer qanda-border">
                                  <hr />
                                <ul class="list-inline">
                                 <li class="list-inline-item">
                                  <span class="d-flex align-items-center links-like-btn">
                                   <asp:LinkButton ID="btnLikeAns" runat="server" CommandName="LikeAns" class="active-toogle"><%--CommandName="LikeAns"--%>
                                    <span class="like-btn"></span>
                                   </asp:LinkButton>
                                    <span class="d-flex">
                                   <asp:Label ID="lblTotallikeAns" runat="server" Text=""></asp:Label>
                                   </span>
                                  </span>
                                 </li>
                                 
                                 <li class="list-inline-item">
                                  <span class="d-flex align-items-center">
                                   <asp:LinkButton ID="btnReplyAns" runat="server" CommandName="ReplyPostAns" class="active-toogle cursor-initial">
                                    <span class="edit-comment-btn"></span>
                                   </asp:LinkButton>
                                   <asp:Label ID="lblreplyAns" runat="server" Text=""></asp:Label>
                                  </span>
                                 </li>
                                </ul>  
                                </div>  
                                
                                <div class="comment-con">                                

                                 <asp:ListView ID="lstAllComments" runat="server" OnItemDataBound="lstAllComments_ItemDataBound" OnItemCommand="lstAllComments_ItemCommand">                                  
                                  <ItemTemplate>
                                   
                                   <div class="ans-comments">
                                    <div class="post-header"> 
                                     <span class="avatar-img">
                                      <img id="imgCommentpic" src='<%# GetStatusImage(Eval("PhotoPath").ToString()) %>' runat="server" alt="" class="rounded-circle">
                                       <%--src='<%# "CroppedPhoto/"+Eval("PhotoPath")%>'--%>
                                     </span> 
                                     <span class="user-name-date">
                                      <span class="user-name"><asp:Label ID="lblUsername" runat="server" Text='<%#Eval("UserName")%>'/></span>
                                      <span class="date"><asp:Label ID="lblDateAddedOn" runat="server" Text='<%#Eval("dtAddedOn")%>' /></span> 
                                     </span>
                                     <span class="more-btn float-right">
                                      <span class="dropdown" id="editDelCmnts" runat="server">
                                       <a href="#" role="button" id="dropdownMenuLinkCmnts" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        <img src="images/more.svg" alt="" class="more-btn" />
                                       </a>
                                       <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" onclick="setAnsCommentId.bind(this)()">
                                        <asp:HiddenField ID="hdnChildtblId" runat="server" Value='<%#Eval("ID")%>' ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnComment" runat="server" Value='<%#Eval("strComment")%>' ClientIDMode="Static" />
                                        <asp:LinkButton Font-Underline="false" CssClass="dropdown-item" CommandName="Edit Comments" ID="lnkEditCmnts"
                                         runat="server"><span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>
                                        <asp:LinkButton Font-Underline="false" CssClass="dropdown-item hide-body-scroll" CommandName="Delete Comments" ID="lnkdeleteCmnts"
                                         runat="server" OnClientClick="javascript:docdeleteCmnts();"><span class="lnr lnr-trash"></span> Delete</asp:LinkButton>
                                       </span>
                                      </span>  
                                     </span>

                                    </div>
                                    <div class="post-body border-0">
                                     <asp:Label ID="lblDescriptionAns" runat="server" Text='<%#Eval("strComment")%>'/>
                                    </div> 
                                   </div>
                                 
                                  </ItemTemplate>
                                 </asp:ListView>
                                 <asp:Panel ID="pnlEnterComment" runat="server" ClientIDMode="Predictable" DefaultButton="btnSaveCmnt">
                                  <div class="post-footer margin-m-ans-comt">
                                   <span class="avatar-img mr-3">
                                    <img ID="imgUser" runat="server" alt="" class="rounded-circle"/>
                                   </span>
                                   
                                    <asp:HiddenField ID="hdnCommentEditId" runat="server" Value=""/>
                                    <%--<TextArea ID="TextArea1" runat="server" maxlength="2000" autocomplete="off" placeholder="Type your comment" class="form-control" />--%>
                                    <asp:TextBox TextMode="multiline" ID="CmntAns" runat="server" maxlength="2000" autocomplete="off" placeholder="Type your comment" class="form-control postcommentduplicate height-moz" onkeydown="return postCommentNew.bind(this)(event)" oninput="postCommentInput.bind(this)(event)" rows="1"/>
                                    <asp:LinkButton ID="btnSaveCmnt" runat="server" CommandName="btnSaveCmnt" Text="Submit" Style="display: none" CssClass="btnSaveCmtCss"/>
                                     <%--OnClick="lnkPostForum_Click" CommandArgument='<%# Eval("ID") %>'--%>
                                  </div> 
                                 </asp:Panel>                                 
                                </div>
                            </div>
                        </li>
                        </ItemTemplate>
                        </asp:ListView>
                    </ul>
                        <div class="modal backgroundoverlay sharepopup" style="display: none;" id="PopUpShare" runat="server">
                            <div class="modal-dialog modal-dialog-centered" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                     <h4 class="modal-title" id="sharemodalTitle">Share Q & A</h4>
                                     <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                      <span aria-hidden="true" onclick="$('.sharepopup').hide();">&times;</span>
                                     </button>
                                    </div>
                                    <div class="modal-body">
                                     <form>
                                      <div class="form-group">
                                       <label for="member">To</label>
                                       <uc:UserControl_MultiSelect class="form-control" ID="txtInviteMembers" ClientIDMode="Static" runat="server" />
                                        <asp:HiddenField ID="hdnInvId" ClientIDMode="Static" runat="server" />
                                        <asp:Label ID="lblmsg" runat="server" Text="" class="lbmsg_one" ForeColor="Red"
                                         ClientIDMode="Static"></asp:Label>
                                      </div>

                                      <div class="form-group">
                                       <label for="Message">Message</label>
                                       <textarea id="txtBody" runat="server" maxlength="500" cols="20" rows="2" placeholder="Message" class="form-control"></textarea>
                                      </div>
                                      <div class="form-group">
                                       <label for="member">Link</label>
                                       <asp:TextBox ID="txtLink" runat="server" value="Paste link" onblur="if(this.value=='') this.value='Paste link';"
                                        Enabled="false" onfocus="if(this.value=='Paste link') this.value='';" class="form-control">
                                       </asp:TextBox>

                                      </div>
                                     </form>
                                    </div>
                                    <div class="modal-footer border-top-0 padding-top-0">
                                     <button type="button" class="btn btn-outline-primary m-r-15 add-scroller" onclick="$('.sharepopup').hide();" 
                                      data-dismiss="modal">Cancel</button>
                                     <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Share"
                                      ValidationGroup="Comment" CausesValidation="true" CssClass="btn btn-primary" OnClick="lnkPopupOK_Click">
                                     </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                         </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
            <!-- center-panel ended -->

            <div class="right-panel-back-layer"></div>
            <div class="right-panel">
                <span class="m-view back">Back <span class="lnr lnr-arrow-right"></span></span>
                <div class="aside-bar">
                    <div class="card releated-section">
                        <div class="card-body">
                            <h4>Related Questions</h4>
                            <ul class="list-unstyled">
                                <asp:ListView ID="lstRelQuestions" runat="server" OnItemCommand="lstRelQuestions_ItemCommand"
                                    GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                                    <ItemTemplate>
                                        <li>
                                            <asp:LinkButton ID="lnkQueAnsDesc" Font-Underline="false" runat="server" Text='<%#Eval("strQuestionDescription")%>'
                                                CommandName="OpenQ"></asp:LinkButton>
                                        </li>
                                        <asp:HiddenField ID="hdnPostQuestionId" runat="server" Value='<%#Eval("intPostQuestionId")%>' />
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                            <div class="text-center show-more-seprator remove-body-fixed-class">
                                <a class="readmore new" href="AllQuestionDetails.aspx">Show More</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- right-panal ended -->

        </div>
        <!-- panel-cover ended -->
               <%-- </ContentTemplate>
             </asp:UpdatePanel>--%>
    </div>
    
    <div id="divDeletesucessQuest" class="modal backgroundoverlay" clientidmode="Static" runat="server"
        style="display: none;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="modal-dialog modal-dialog-centered">
            <ContentTemplate>
                <div id="div3" runat="server" class="w-100" clientidmode="Static">
                    <div class="modal-content">
                        <div>
                            <b>
                                <asp:Label ID="Label3" runat="server"></asp:Label>
                            </b>
                        </div>
                        <div class="modal-header">
                            <h5 class="modal-title">Delete Confirmation
                            </h5>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="Label4" runat="server" Text="Do you want to delete?"></asp:Label>
                        </div>
                        <div class="modal-footer border-top-0">
                         <asp:Button ID="lnkDeleteCancelQuest" runat="server" class="add-scroller btn btn-outline-primary m-r-15" ClientIDMode="Static" Text="Cancel"
                             OnClientClick="javascript:divCancelQuest();return false;"></asp:Button>
                         <asp:LinkButton ID="lnkDeleteQuest" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="javascript:showLoader1();"
                             CssClass="btn btn-primary success-popup" OnClick="lnkDeleteQuest_Click"></asp:LinkButton>
                        </div>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="divDeletesucess" class="modal backgroundoverlay" clientidmode="Static" runat="server"
        style="display: none;">
        <asp:UpdatePanel ID="upasss" runat="server" class="modal-dialog modal-dialog-centered">
            <ContentTemplate>
                <div id="divDeleteConfirm" runat="server" class="w-100" clientidmode="Static">
                    <div class="modal-content">
                        <div>
                            <b>
                                <asp:Label ID="lbl" runat="server"></asp:Label>
                            </b>
                        </div>
                        <div class="modal-header">
                            <h5 class="modal-title">Delete Confirmation
                            </h5>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblConnDisconn" runat="server" Text="Do you want to delete?"></asp:Label>
                        </div>
                        <div class="modal-footer border-top-0">
                            <asp:Button ID="lnkDeleteCancel" runat="server" class="add-scroller btn btn-outline-primary m-r-15" ClientIDMode="Static" Text="Cancel"
                                OnClientClick="javascript:divCancels();return false;"></asp:Button>
                            <asp:Button ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="javascript:divdeletes();"
                                CssClass="btn btn-primary success-popup" OnClick="lnkDeleteConfirm_Click"></asp:Button>
                        </div>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="divDeletesucessCmnts" class="modal backgroundoverlay" clientidmode="Static" runat="server"
     style="display: none;">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="modal-dialog modal-dialog-centered">
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
          <asp:Button ID="lnkCmntDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="javascript:divdeletesCmnts();"
           CssClass="btn btn-primary success-popup" OnClick="lnkDeleteCmntConfirm_Click"></asp:Button>
         </div>

        </div>
       </div>
      </ContentTemplate>
     </asp:UpdatePanel>
    </div>
   
    <script type="text/javascript">
        $(document).ready(function () {
          $('#lnkDeleteQues').click(function () {
              $('#hdnDeletePostQuestionID').val($(this).parent().children('#hdnintPostQuestionIdelet').val());
              $('#hdnstrQuestionDescription').val($(this).parent().children('#lnkstrQuestionDescription').val());
              $('#divDeletesucessQuest').css("display", "block");
          });
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              $('#lnkDeleteQues').click(function () {
              $('#hdnDeletePostQuestionID').val($(this).parent().children('#hdnintPostQuestionIdelet').val());
              $('#hdnstrQuestionDescription').val($(this).parent().children('#lnkstrQuestionDescription').val());
              $('#divDeletesucessQuest').css("display", "block");
              });
            });
        });
        function hideScroll() {
            $('body').removeClass('remove-scroller');
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
        $(document).ready(function () {
            hideLoader1();
            <%--debugger;
            var btn = document.getElementById('<%=btnSaveCmnt1.ClientID%>');
                btn.click();--%>
               $('#btnPost').click(function () {
                if ($('#CKEditorControl').val() != '') {
                    $('#btnPost').addClass("disabled");
                    showLoader1();
                }
            });
            $("#CmntAns").keypress(function (e) {
                debugger;
                if (e.keyCode == 13) {
                    $('btnSaveCmnt').click();
                    //$('#btnSaveCmnt').click();
                  return false; 
              }
            });
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_endRequest(function () {
                 createChosen();
                 hideLoader1();
                 $('#btnPost').click(function () {
                    if ($('#CKEditorControl').val() != '') {
                        $('#btnPost').addClass("disabled");
                        showLoader1();
                    }
                 });
                 $("#CmntAns").keypress(function (e) {
                     debugger;
                     if (e.keyCode == 13) {
                         $('#btnSaveCmnt').click();
                         return false;
                     }
                 });
             });
         });
         function callClose() {
            $('#txtSubjectList').val('').trigger('chosen:updated');
             
         }
        function divCancels() {
            $('#divDeletesucess').hide();
        }
        function divCancelQuest(){
            $('#divDeletesucessQuest').hide();
        }
        function divCancelsCmnts() {
            $('#divDeletesucessCmnts').hide();
        }
        function divdeletes() {
            // $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 5px #00B7E5");         
            $('#divDeletesucess').css("display", "none");
        }
        function divdeletesCmnts() {
            // $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 5px #00B7E5");         
            $('#divDeletesucessCmnts').css("display", "none");
        }
        function docdelete() {
            $('#divDeletesucess').css("display", "block");
        }
        function docdeleteCmnts() {
            $('#divDeletesucessCmnts').css("display", "block");
        }
        function setAnsCommentId() {
            $('#hdnintCommentDelete').val($(this).children('#hdnChildtblId').val());
            //$('#hdnCommentEditId').val($(this).children('#hdnChildtblId').val());
            $('#hdnCommentEdit').val($(this).children('#hdnComment').val());
        }
        function PostEnable() {
            $(this).removeClass("h-34");
            $(this).height(60);
            $('#btnPost').show();
        }
        function PostDisable() {
            if (this.value.trim() == "") {
                $(this).addClass("h-34");
                $('#btnPost').hide();
            }
               
        }

        function postCommentNew(e) {
           if (e.keyCode == 13 && $(this).val()) {
               window.location.href = $(this).parent().children('.btnSaveCmtCss').attr('href');
               return false;
           } else if (e.keyCode == 13) {
               return false;
           }
           
        }
        function postCommentInput(e) {
           var msg = $(this).val().replace(/\n/g, "");
           if (msg != $(this).val()) { // Enter was pressed
               $(this).val(msg);
               window.location.href = $(this).parent().children('.btnSaveCmtCss').attr('href');
               showLoader1();
           }
       }

    </script>
</asp:Content>
