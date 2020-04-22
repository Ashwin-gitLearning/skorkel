<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="BlogsComments.aspx.cs" Inherits="BlogsComments" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
<%@ Register Src="~/UserControl/BlogPopulerPost.ascx" TagName="BlogPopulerPost" TagPrefix="BlogPopulerPost" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ MasterType TypeName="Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <%--<script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>--%>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <!--pop up starts-->
            <!---Sucess Popup  on delete post-->
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
            <div class="modal backgroundoverlay" id="PopUpShare" clientidmode="Static" runat="server" style="display:none;">
                <div class="modal-dialog  modal-dialog-centered" id="popUp">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Share Blog</h5>
                            <button onclick="CancelPopup();" type="button" id="close-popup" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body text-left">
                            <div class="popHeading" style="display: none;">
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </div>
                            <div>
                                <b>
                                    <asp:Label ID="lblTitleGroup" runat="server"></asp:Label>
                                </b>
                            </div>
                            <div class="form-group position-relative">
                                <div id="tdDepartment">
                                    <label for="member">To</label>
                                      <uc:UserControl_MultiSelect class="form-control" ID="txtInviteMembers" ClientIDMode="Static" runat="server"  />
                                   <%-- <select data-placeholder="Select member" class="chosen-select form-control" id="txtInviteMembers"
                                        onchange="getMultipleValues(this.id)" runat="server" multiple
                                        tabindex="4">
                                    </select>--%>
                                    <asp:HiddenField ID="hdnDepartmentIds" ClientIDMode="Static" runat="server" />
                                </div>
                                <div class="share_error">
                                    <asp:Label ID="lblMess" ForeColor="Red" runat="server" Text=""></asp:Label>
                                </div>     
                            </div>

                            <div class="form-group">
                                <label for="member">Message</label>
                                <textarea id="txtBody" runat="server" placeholder="Message" class="forumTitle share_popup_a form-control" maxlength="500"></textarea>
                            </div>
                            <div class="form-group">
                                <label for="member">Link</label>
                                <asp:TextBox ID="txtLink" runat="server" value="Paste link" data-watermark="Paste link"
                                    Enabled="false" class="forumTitlenew share_text_box form-control" ></asp:TextBox>
                            </div>
                            <p>
                                <strong>
                                    <asp:Label ID="lblMessAccept" runat="server"></asp:Label>
                                    <asp:Label ID="lblMessReject" runat="server"></asp:Label>
                                </strong>
                            </p>

                        </div>
                        <div class="modal-footer border-top-0 padding-top-0">           
                            <a onclick="CancelPopup();" causesvalidation="false" href="" class="btn btn-outline-primary m-r-15 add-scroller">Cancel</a>
                            <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Share"
                                class="btn btn-primary" OnClick="lnkPopupOK_Click"></asp:LinkButton>     
                        </div>
                    </div>
                </div>
            </div>
            <!--pop up ends-->
            <!--heading ends-->
            <div class="cls">
            </div>
            <!--Sucess pop up starts-->
            <div id="divSuccess" class="modal backgroundoverlay" runat="server" style="
                display: none;" clientidmode="Static">
                <div class="modal-dialog  modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">
                               Success
                            </h5>
                        </div>
                        <div class="modal-body">
                            
                            <asp:Label ID="lblSuccess" runat="server" ></asp:Label>
                            
                        </div>    
                        <div class="modal-footer border-top-0">
                            <a href="#" clientidmode="Static" causesvalidation="false"  class="btn btn-outline-primary add-scroller" onclick="javascript:messageClose();return false;">
                            Close </a>
                        </div>
                    </div>
                </div>
            </div>
             <!--Sucess pop up Ended-->
            <div class="cls">
            </div>
            <!--Content Container Start-->
            <div class="main-section-inner">
                      <span class="m-aside-trigger mt-0 mb-0">
                        <span class="lnr lnr-arrow-left"></span>
                        <span onclick="showRight();" class="avatar-text">Popular Posts</span>
                      </span>
                <div class="back-link-cover">
                 <a href="AllBlogs.aspx" class="back-link back-arrow-height"><span class="lnr lnr-arrow-left"></span> Back to Blogs</a>
                </div>
                <div class="panel-cover clearfix">
                    <!---Delete Sucess Popup-->
                    <div id="divDeletesucess" class="modal backgroundoverlay" clientidmode="Static" runat="server" 
                        style="display: none;">
                        <div id="divDeleteConfirm" runat="server" class="confirmDeletes modal-dialog modal-dialog-centered" clientidmode="Static">
                            <div class="modal-content">
                                <b>
                                    <asp:Label ID="lbl" runat="server"></asp:Label>
                                </b>
                                <div class="modal-header">
                                    <h5 class="modal-title">Delete Confirmation </h5>
                                    
                                        
                                    
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="lblConnDisconn" runat="server" Text="Do you want to delete ?"></asp:Label>
                                </div>    
                                <div class="modal-footer border-top-0">
                                    <asp:LinkButton ID="lnkDeleteCancel" class="btn add-scroller btn-outline-primary m-r-15" runat="server" ClientIDMode="Static" Text="Cancel"
                                        OnClientClick="javascript:divCancels();return false;"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes"
                                        class="btn btn-primary" OnClick="lnkDeleteConfirm_Click"></asp:LinkButton>
                                </div>
                            </div>    
                        </div>
                    </div>
                    <!---Delete Sucess Popup Ended-->
                    <div class="center-panel">    
                        <!--search box starts-->
                        <div class="form-inline display-none">
                            <asp:UpdatePanel ID="upmains" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdnSubject" ClientIDMode="Static" runat="server" />
                                    <div class="w-100">
                                        <div class="search-filter-con blog-list-filter">
                                            <div class="search-cover">
                                            <asp:UpdatePanel ID="er" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtblogsearch" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtblogsearch" ClientIDMode="Static" CssClass="searchBlogs" runat="server"
                                                        CausesValidation="true"></asp:TextBox>
                                                    <input type="submit" class="search-btn-2" value="">
                                                    <ajax:TextBoxWatermarkExtender TargetControlID="txtblogsearch" ID="txtwatermarkz"
                                                        runat="server" WatermarkText="Search Blogs">
                                                    </ajax:TextBoxWatermarkExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtblogsearch"
                                                        Display="Dynamic" ValidationGroup="blogg" ErrorMessage="Please enter keyword"
                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <span class="dropdown_m"> Search Using Tags Only  </span>
                                            <div class="hide_show_m">
                                            <ul class="ulblogcontaxt ">
                                                <asp:ListView ID="lstSerchSubjCategory" runat="server" OnItemDataBound="lstSerchSubjCategory_ItemDataBound">
                                                    <ItemTemplate>
                                                        <li id="SubLi" runat="server" >
                                                            <asp:HiddenField ID="hdnSubCatId" ClientIDMode="Static" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                            <asp:LinkButton ID="lnkCatName"  Font-Underline="false"
                                                                ClientIDMode="Static" OnClientClick="return false;" runat="server" Text='<%#Eval("strCategoryName")%>'
                                                                CommandName="Subject Category"></asp:LinkButton>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </ul>

                                            <div>
                                            <br />
                                            <asp:LinkButton ID="btnSave" CssClass="searchBlog" CausesValidation="true" runat="server" OnClientClick="javascript:CallWritebolgs();"
                                                ClientIDMode="Static" Text="SEARCH" ValidationGroup="blogg" OnClick="btnSave_Click"></asp:LinkButton>
                                            <div style="display: none;">
                                                <asp:Button ID="btnSave1" ClientIDMode="Static" runat="server" OnClick="btnSave_Click" />
                                            </div>
                                             </div>
                                            </div>
                                      
                                        <!--   <div class="viewAll" style="float: right; margin-bottom: 5px; margin-right: 2px;">
                                            <asp:LinkButton ID="lnkViewSubj1" Text="View all" Font-Underline="false" runat="server"
                                                OnClick="lnkViewSubj1_Click" Style="padding-right: 5px;"></asp:LinkButton></div> -->
                                            </div> 
                                            <div class="fliter-con-view">
                                                 <div class="dropdown"> <span class="sortFilter d-flex align-items-center" id="sortFilter" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> Sort by: &nbsp;<strong> Most Recent</strong> <span class="icon-caret-down"></span></span>
                                                     <div class="dropdown-menu" aria-labelledby="dropdownMenuButton" x-placement="bottom-start"> <a class="dropdown-item" href="#">Most Recent</a> <a class="dropdown-item" href="#">Most Popular</a> </div>
                                                 </div>
                                            </div>   
                                        </div>        
                                    </div>
                                  
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                    <asp:AsyncPostBackTrigger ControlID="lnkViewSubj1" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <!---Post Section Start-->    
                   
                        <div class="subtitle display-none">
                            <div class="recentBlogs">
                                <asp:LinkButton ID="lnkBacklink" runat="server" ClientIDMode="Static" Text="My Blogs"
                                    CssClass="recentBlogTxt" Font-Underline="false" OnClick="lnkBacklink_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        <div class="card card-list-con list-comments-border" >
                            <div class="list-group-item top-list">
                               
                                <div class="post-con">
                                     <asp:ListView ID="lstAllBlogs" runat="server" OnItemCommand="lstAllBlogs_ItemCommand"
                                      OnItemDataBound="lstAllBlogs_ItemDataBound" GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1"
                                      ItemPlaceholderID="itemPlaceHolder1">
                                      <GroupTemplate>
                                       
                                              <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                          
                                      </GroupTemplate>
                                      <ItemTemplate>
                                    <!---Details Header-->
                                    <div class="post-header"> 
                                     <span class="question-icon">
                                     <span class="icon"><img src="images/file.svg"></span>
                                     </span>
                                     <ul class="que-con">
                                      <li>Blog</li>
                                      <li id="SubList" runat="server" >
                                       <asp:ListView ID="lstSubjCategory" runat="server">
                                        <ItemTemplate>
                                            
                                         <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                         <asp:Label ID="btnBlogShare" CssClass="tagsBlog smtxt" Text='<%#Eval("strCategoryName")%>'
                                             runat="server"></asp:Label>
                                         
                                        </ItemTemplate>
                                       </asp:ListView>
                                      </li>
                                      <%--<li>Banking Laws, Civil Laws</li>--%>
                                     </ul>
                                    </div>
                                    <!---Details Header Ended-->
                                    <!---Details Body-->
                                    <div class="post-body">
                                     <!---Post Delete and Edit buton-->
                                         <div id="dvEditDeletePostDots" class="more-btn float-right" Visible="false" runat="server">
                                          <span class="dropdown">
                                           <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><img src="images/more.svg" alt="" class="more-btn">
                                           </a>
                                           <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="top-start">
                                            <asp:LinkButton Font-Underline="false" CssClass="dropdown-item" CommandName="Edit Blog" ID="lnkEdit"
                                             runat="server" Visible="false" Text="Edit" OnClientClick="showLoader1();"><span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>
                                            <asp:LinkButton Font-Underline="false" CssClass="hide-body-scroll dropdown-item" CommandName="Delete Blog" ID="lnkdelete"
                                             runat="server" Visible="false" Text="Delete"><span class="lnr lnr-trash"></span> Delete</asp:LinkButton>
                                            <%--<a class="dropdown-item" href="#"><span class="lnr lnr-pencil"></span> Edit</a>
                                            <a class="dropdown-item" href="#"><span class="lnr lnr-trash"></span> Delete</a>--%>
                                               </span>
                                              </span>
                                             </div>
                                         
                                              <asp:HiddenField ID="hdnBlogLikeUserId" runat="server" Value='<%#Eval("BlogLikeUserId") %>' />
                                              <asp:HiddenField ID="hdnintAddedBy" runat="server" Value='<%#Eval("intAddedBy") %>' />
                                         
                                          
                                              <h3>    
                                                  <asp:Label ID="Label1" runat="server" Text='<%#Eval("strBlogHeading")%>' CssClass="BlogHeadingFont"></asp:Label>
                                              </h3>
                                             
                                              <ul class="small-date">
                                                  
                                                  <li>by <asp:Label ID="lnkAddBy"  runat="server" Text='<%#Eval("strAddedBy")%>'></asp:Label></li>

                                                  <li><asp:Label ID="lblDate" Font-Bold="false" runat="server" 
                                                      Text='<%#Eval("dtAddedOn")%>'></asp:Label></li>
                                              </ul>
                                              
    <%--                                          <div class="postReview" >
                                                  <div class="commentsGiven">
                                                      <img src="images/blog-comments.png" align="absmiddle" />
                                                      <asp:Label ID="lnkComments" Font-Bold="false" ForeColor="#9c9c9c" 
                                                          Font-Underline="false" runat="server" Text='<%#Eval("CommentCount")%>'></asp:Label>
                                                  </div>
                                           
                                              </div>
                                              <br />
                                              <div class="categoryBoxBlog">
                                                  <div class="ansTagsBlog" >
                                                      <ul class="law_btn">
                                                         
                                                      </ul>
                                                  </div>
                                              </div>
                                              --%>
                                              <!--post area ends-->
                                              <!--post txt starts-->
                                              <p class="m-t-20">    
                                                  <asp:Label ID="Label2" runat="server" Text='<%#Eval("strBlogTitle").ToString().Replace(Environment.NewLine,"<br />")%>'></asp:Label>
                                              </p>   

                                             <%-- <!---Edit Delete buttons-->    
                                              <div class="qscreenBox margin_right_r">
                                                        <asp:LinkButton Font-Underline="false" CssClass="edit" CommandName="Edit Blog" ID="lnkEdit"
                                                            runat="server" Visible="false" Text="Edit"></asp:LinkButton>
                                                        <asp:LinkButton Font-Underline="false" CssClass="edit" CommandName="Delete Blog"
                                                            ID="lnkdelete" Visible="false" runat="server" Text="Delete"></asp:LinkButton>
                                                    </div>
                                            <!---Edit Delete buttons Ended--> --%>

                                      </div>            
                                                     
                                      <div class="post-footer pb-1 pt-3">
                                          <ul class="list-inline">
                                              <li class="list-inline-item">
                                                <asp:UpdatePanel ID="UpdateBlogPanel" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>                                                            
                                            <span class="d-flex align-items-center links-like-btn">
                                                 <asp:LinkButton class="active-toogle" ID="btnBlogLike" runat="server" CommandName="LikeBlog" ToolTip="Like">
                                                  <span class="like-btn"></span>
                                                   
                                                 </asp:LinkButton>
                                                 <span class="d-flex">
                                                  <asp:Label ID="lblTotalBloglike" ToolTip="View Likes" runat="server"
                                                    Text='<%#Eval("TotalLike") + (((int)Eval("TotalLike")==1)? " Like": " Likes")%>'>
                                                   </asp:Label> 
                                               </span>
                                                   </span>
                                                    </ContentTemplate>
                                               </asp:UpdatePanel>
                                              </li>
                                              <li class="list-inline-item">                                                      
                                               <asp:LinkButton class="active-toogle hide-body-scroll" ID="btnBlogShare" Text="Share" runat="server" CommandName="ShareBlog" ToolTip="Share">                                                            
                                                <span class="share-btn"></span>
                                                <asp:Label ID="lblBlogShare" class="text_d_c" runat="server" Text='<%#Eval("TotalShare") + (((int)Eval("TotalShare")==1)? " Share": " Shares")%>'></asp:Label>
                                               </asp:LinkButton>
                                              </li>
                                             <li class="list-inline-item"> 

                                              <a class="active-toogle"> <span class="eye-view"></span>   
                                                  <asp:Label ID="Label5" runat="server" Text='<%#Eval("ViewCount") + ((Eval("ViewCount").ToString()=="1")? " View": " Views") %>'></asp:Label>
                                              </a>
                                          
                                             </li>                                         
                                            
                                          </ul>
                                      </div>
                                      
                                   </ItemTemplate>
                                     </asp:ListView>
                                    <!---Details Body with comments-->
                                    <!---Add  new Comments section-->
                                    <div class="post-form text-right mt-0">
                                     <div id="divCommentDisplay" clientidmode="Static" class="w-100">
                                      <div class="w-100">
                                       <asp:Label ID="Label3" runat="server"></asp:Label>
                                       
                                        <textarea id="CKDescription" maxlength="2000" clientidmode="Static" runat="server" cols="20" rows="10" placeholder="Type your Comment"
                                         class="form-control question-area h-34 w-100" ></textarea>
                                      
                                        <%--<input type="submit" id="PostComment" class="btn btn-primary post-btn m-t-10" value="Post">--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                         class="margin_left_rt" ControlToValidate="CKDescription" Display="Dynamic"
                                         ValidationGroup="blog" ErrorMessage="Please enter comment." ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                       
                                       <div class="cls"></div>
                                   
                                        <asp:Button ID="lnkPostComent" runat="server" ClientIDMode="Static" CssClass="btn btn-primary post-btn m-t-10 hide-body-scroll" OnClick="lnkPostComent_Click"
                                         Text="Post" ValidationGroup="blog">
                                        </asp:Button>
                                        <%--<asp:Button ID="btnPostCmmnt" ClientIDMode="Static" Style="display: none;" runat="server" OnClick="lnkPostComent_Click"/>--%>
                                     
                                        <%--CssClass="vote margin_right_r" OnClick="lnkPostComent_Click"--%>   
  <%--<asp:LinkButton ID="lnkSignOut" OnClientClick="javascript:lnkSignOut_Click();return false;" runat="server">Logout</asp:LinkButton>
  <asp:Button ID="btnSignOut" OnClick="lnkSignOut_Click" ClientIDMode="Static" Style="display: none;" runat="server" />--%>
                                       </div>
                                      </div>        
                                     </div>
                                  

                                                            
                                          <span id="totalComments" class="answer-counter" runat="server"></span>
                                    <%--<span class="answer-counter">5 Comments</span>--%>
                                    <!---Add  new Comments section-->

                                </div>
                            </div>
                             <!---Comments section start-->
                                  
                                <asp:Repeater ID="RepPopPost" runat="server" OnItemCommand="RepPopPost_ItemCommand"
                                    OnItemDataBound="RepPopPost_ItemDataBound">

                                <ItemTemplate>
                                    <div class="list-group-item" >
                                        <div class="post-con">
                                            <div class="post-header">
                                                <span class="avatar-img" >
                                                    <img id="img1" runat="server" src='<%# Eval("vchrPhotoPath")%>' class="rounded-circle" />
                                                    <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>'  /> <%--Value='<%# Eval("vchrPhotoPath") %>'--%>
                                                    <asp:HiddenField ID="hdnBlogCommentUser" runat="server" Value='<%#Eval("BlogCommentUser")%>' />
                                                    <asp:HiddenField ID="hdnCommentID" runat="server" Value='<%#Eval("intCommentId")%>' />
                                                    <asp:HiddenField ID="hdnintAddedBy" runat="server" Value='<%#Eval("intAddedBy")%>' />
                                                </span>
                                         
                                                <span class="user-name-date">
                                                    <span class="user-name">
                                                        <asp:LinkButton  class="un-anchor" ID="lnkAddedby" ToolTip="View profile" CommandName="View profile"
                                                            runat="server" Text='<%#Eval("strAddedBy")%>'></asp:LinkButton>
                                                    </span>
                                                    <span class="date">
                                                        <asp:Label ID="lblPostedOn" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                                    </span>
                                                    
                                                </span>
                                                <!---Dropdown Edit and Delete-->
                                                <div id="dvEditDeleteComment" Visible="false" runat="server" class="more-btn float-right">
                                                    <span class="dropdown">
                                                      <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                                       <img src="images/more.svg" alt="" class="more-btn">
                                                      </a>

                                                      <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start">
                                                        <asp:LinkButton Font-Underline="false" class="dropdown-item" CommandName="Edit Blog" ID="lnkEdit"
                                                         runat="server" Visible="false" Text="Edit" OnClientClick="showLoader1();"><span class="lnr lnr-pencil"></span> Edit </asp:LinkButton>
                                                        <asp:LinkButton Font-Underline="false" class="dropdown-item hide-body-scroll" CommandName="Delete Blog"
                                                         ID="lnkdelete" Visible="false" runat="server" Text="Delete"><span class="lnr lnr-trash"></span> Delete</asp:LinkButton><%--CommandName="Delete Blog"--%>
                                                      </span>
                                                    </span>
                                                </div>


                                            </div>
                                            <div class="post-body">
                                              <p class="breakwordsbc">
                                                    <asp:Label ID="lblComment" Visible="false" runat="server" Text='<%#Eval("strComment")%>'></asp:Label>
                                                    <asp:Label ID="lblCommentAll" runat="server" Text='<%#((string)Eval("strCommentAll")).Replace("\n", "<br>")%>'></asp:Label>
                                                </p>    
                                            </div>        
                                             
                                            <asp:UpdatePanel ID="upRep1" UpdateMode="Conditional" runat="server" Visible="false">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkViewSubj" Text="view more" runat="server" OnClick="lnkViewSubj_Click"
                                                         CommandName="ViewMore" class="view_more_m"></asp:LinkButton>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>                                   
                                       
                                        </div>
                                    </div>
                                </ItemTemplate>
                             
                                </asp:Repeater>
                           
                             <!---Comments section End-->   

                        </div>
                        <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
                    </div>
                    <!---Right Section Start-->   
                    <div class="right-panel-back-layer"> </div>
                    <div class="right-panel">
                        <span onclick="hideRight();" class="m-view back">Back <span class="lnr lnr-arrow-right"></span></span>
                        <div class="aside-bar">
                            <div class="card releated-section">
                                <div class="card-body">
                                    <h4>Popular Posts</h4>
                                    <div class="popularPost">
                                        <div class="popular_cat">
                                            <asp:UpdatePanel ID="update" runat="server">
                                                <ContentTemplate>
                                                    <BlogPopulerPost:BlogPopulerPost ID="ucBlogPopulerPost" runat="server" AllowDirectUpdate="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>    
                            </div>    
                        </div>    
                    </div>    
                </div>
            </div> 
            
            <div class="clearfix"></div>
       
         
          

                
            <!---Content Container Ended-->  
              

    <script>

            //Focus Increasing div height
        
     
           $(document).on('click', '.dropdown_m', function() {
               $(".hide_show_m").slideToggle('slow');
              $(".popular_cat").hide('slow');
            });
    
            $(document).on('click', '.popularHeading', function() {
              $(".popular_cat").slideToggle('slow');
             $(".hide_show_m").hide('slow');
            });
        
             

       </script>
    <script type="text/javascript">
                    $(document).ready(function () {
                        $("#lnkPostComent").hide();

                        $(".question-area").focusin(function() {
                            $(this).removeClass("h-34");
                            $(this).height(60);
                           $("#lnkPostComent").show();    
                        });

                        $(".question-area").focusout(function () {
                            if ($("#CKDescription").val() == "") {
                                $(this).addClass("h-34");
                                $("#lnkPostComent").hide();

                            }
                        });
                                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                        prm.add_endRequest(function () {
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
                            createChosen();

                            $("#lnkPostComent").hide();

                            $(".question-area").focusin(function() {
                                $(this).removeClass("h-34");
                                $(this).height(60);
                               $("#lnkPostComent").show();    
                            });

                            $(".question-area").focusout(function() {
                               if ($("#CKDescription").val() == "") {
                                    $(this).addClass("h-34");
                                    $("#lnkPostComent").hide();
                                }    
                            });
                        });
                    });
                </script>
            
            <!--inner container ends-->
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkPostComent" />
            <%--<asp:AsyncPostBackTrigger ControlID="btnPostCmmnt" />   --%>         
            <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
            <asp:AsyncPostBackTrigger ControlID="lnkPopupOK" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".categoryTxt ul li").click(function () {
                $(this).toggleClass("gray");
            });
        });
        //function getMultipleValues(ctrlId) {
        //    $('#tdDepartment').find('label.error').remove();
        //    var control = document.getElementById(ctrlId);
        //    var strSelText = '';
        //    var cnt = 0;
        //    for (var i = 0; i < control.length; i++) {
        //        if (control.options[i].selected) {
        //            if (cnt == 0) {
        //                strSelText += control.options[i].value;
        //            }
        //            else {
        //                strSelText += ',' + control.options[i].value;
        //            }
        //            cnt++;
        //        }
        //    }
        //    $('#hdnDepartmentIds').val(strSelText);
        //}
        function CancelPopup() {
            document.getElementById("PopUpShare").style.display = 'none';
            document.getElementById("divSuccess").style.display = 'none';
            return false;
        }
    </script>
    <script type="text/javascript">
        function messageClose() {
            document.getElementById('divSuccess').style.display = 'none';
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#lnkPostComent').click(function () {
                if ($('#CKDescription').val() != '') {
                    $('#lnkPostComent').addClass("disabled");
                    showLoader1();
                }
            });
            $("#lnkComment").click(function () {
                $("#CKDescription").focus();
            });
            $("#txtblogsearch").keydown(function (e) {
                if (e.keyCode == 13) {
                    if ($("#txtblogsearch").val() != '') {
                        e.returnValue = true;
                        $('#btnSave1').click();
                        return false;
                    }
                }
            });

            $('#lnkPopupOK').click(function () {
                if ($('#tdDepartment select').val() != null) {
                    $('#lnkPopupOK').addClass("disabled");
                }
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                hideLoader1();
                $('#lnkPostComent').click(function () {
                    if ($('#CKDescription').val() != '') {
                        $('#lnkPostComent').addClass("disabled");
                        showLoader1();
                    }
                });
                $("#lnkComment").click(function () {
                    $("#CKDescription").focus();
                });
                $("#txtblogsearch").keydown(function (e) {
                    if (e.keyCode == 13) {
                        if ($("#txtblogsearch").val() != '') {
                            e.returnValue = true;
                            $('#btnSave1').click();
                            return false;
                        }
                    }
                });

                 $('#lnkPopupOK').click(function () {
                    if ($('#tdDepartment select').val() != null) {
                        $('#lnkPopupOK').addClass("disabled");
                    }
                });
        
            });
        });
    </script>
    <script type="text/javascript">
        var strSelTexts = '';
        $(document).ready(function () {
            $('ul.ulblogcontaxt li').click(function () {
                $(this).toggleClass('selectBlogLi unselectBlogLi');
                if ($(this).hasClass("selectBlogLi")) {
                    $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                } else {
                    $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                }
                AddSubjectCall($(this).children("#hdnSubCatId").val());
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('ul.ulblogcontaxt li').click(function () {
                    $(this).toggleClass('selectBlogLi unselectBlogLi');
                    if ($(this).hasClass("selectBlogLi")) {
                        $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                    } else {
                        $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                    }
                    AddSubjectCall($(this).children("#hdnSubCatId").val());
                });
        
            });
        });
        function AddSubjectCall(ids) {
            var subVal = $("#hdnSubject").val();
            if (subVal == '') {
                $("#hdnSubject").val(ids);
            } else {
                strSelTexts = $("#hdnSubject").val();
                strSelTexts += ',' + ids;
                $("#hdnSubject").val(strSelTexts);
                strSelTexts = '';
            }
        }
    </script>
    <script type="text/javascript">
        function docdelete() {
            $('#divDeletesucess').css("display", "block");
        }
        function divCancels() {
            $('#divDeletesucess').css("display", "none");
        }
        function CallWritebolgs() {
            $('#btnSave').css("box-shadow", "0px 0px 5px #00B7E5");
            $('#btnSave').css("background", "#00A5AA");
            if ($('#txtblogsearch').text() == '') {
                setTimeout(
                function () {
                    $('#btnSave').css("background", "#0096a1");
                    $('#btnSave').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
        function lnkPostComent_Click() {
         document.getElementById("btnPostCmmnt").click();
        }
    </script>
</asp:Content>