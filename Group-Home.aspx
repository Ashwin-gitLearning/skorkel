<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Group-Home.aspx.cs" MasterPageFile="~/Main.master"
   Inherits="Group_Home" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Src="~/UserControl/Groups-m.ascx" TagName="GroupDetailsM" TagPrefix="GroupM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControl/Share.ascx" TagName="ShareDetails" TagPrefix="Share" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
   <script src="<%=ResolveUrl("js/jquery.custom-scrollbar.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


      <div class="main-section-inner">
         <div id="divDeletesucess" clientidmode="Static" runat="server" class="modal backgroundoverlay EditProfilepopupHome"
            style="display: none;">
            <div id="divDeleteConfirm" runat="server" class="confirmDeletes modal-dialog modal-dialog-centered" clientidmode="Static">
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

                     <asp:Label ID="Label4" runat="server" Text="Do you want to delete ?"></asp:Label>

                  </div>
                  <div class="modal-footer border-top-0">
                     <asp:LinkButton ID="lnkDeleteCancel" runat="server" class="add-scroller btn btn-outline-primary m-r-15" ClientIDMode="Static" Text="Cancel"
                        OnClientClick="javascript:divCancels();return false;"></asp:LinkButton>
                     <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes"
                        CssClass="btn btn-primary" OnClick="lnkDeleteConfirm_Click" OnClientClick="javascript:divCancels();"></asp:LinkButton>
                  </div>
               </div>   
            </div>
         </div>
   <!---Popup for Inserting Media Links-->
         <div class="modal fade" id="wallModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
            <div class="modal-dialog modal-dialog modal-dialog-centered" role="document">
               <div class="modal-content">
                     <div class="modal-header">
                          <h5 class="modal-title" id="exampleModalLabel">Add post</h5>
                          <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="$('#LinkButton1').click();">
                          <span aria-hidden="true">&times;</span>
                        </button>
                      </div>
                     <div class="modal-body text-left ">
                        <div class="createForumBox members Mem profileSection">
                        
                        <asp:Panel ID="pnlWhatsMind" runat="server" DefaultButton="lnkDummyPost">
                           <div class="iconsForm">
                              <div class="whtField">
                   
                                 <div class="display_block form-group" id="div1" runat="server" clientidmode="Static">
                                    <textarea id="txtPostUpdate" clientidmode="Static" runat="server" class="uploadDescriptionTxt profile form-control" rows="8" cols="0"
                                       placeholder="What's on your mind?" maxlength="5000"></textarea>
                                    <asp:RequiredFieldValidator  ClientIDMode="Static" ID="RequiredFieldValidator1"
                                       runat="server" ControlToValidate="txtPostUpdate" Display="Dynamic" ValidationGroup="post"
                                       ErrorMessage="Please write a post" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hdnvdeonme" ClientIDMode="Static"  runat="server" Value="" />
                                    <asp:HiddenField ID="hdnPhotoName" ClientIDMode="Static"  runat="server" Value="" />
                                     
                                 </div>
                                
                                 <div class="cls">
                                 </div>
                            
                                 
                              </div>
                              <div class="dropList form-group" id="ddlpost">
                                 <asp:DropDownList ID="ddlPostType" runat="server" CssClass="selectBox form-control" ClientIDMode="Static"
                                    Visible="true">
                                    <asp:ListItem Text="Public" Value="public"></asp:ListItem>
                                    <asp:ListItem Text="Private" Value="private"></asp:ListItem>
                                 </asp:DropDownList>
                              </div>
                              <!---Tabbing show and hide Radio buttons-->
                               <div id="divPostRadioBtn">
                               <ul class="list-inline">
                                    <li class="list-inline-item"><span class="custom-radio">
                                    <input type="radio" name="bn" id="1" value="1" checked>
                                    <label for="1">Plain Text</label>
                                </span> </li>
                                    <li class="list-inline-item"> <span class="custom-radio">
                                    <input type="radio" name="bn" id="2" value="2">
                                    <label for="2">Upload Media</label>
                                </span> </li>
                                    <li class="list-inline-item"><span class="custom-radio"> 
                                            <input type="radio" name="bn" id="4" value="4" >
                                    <label for="4">Add Link</label></span>
                                    </li>
                                </ul>
                                   </div>
                              <div class="form-group" style="display:none;">
                                 <ul class="list-inline">
                                    <li class="list-inline-item display-inline">
                                       <img id="icon1" src="images/icon-1.jpg" title="Update Post"  alt=""
                                          onclick="showImage('1');" />
                                    </li>
                                    <li class="list-inline-item display-inline">
                                       <img id="icon2" src="images/icon-2.jpg" title="Upload Photo"  alt=""
                                          onclick="showImage('2');" />
                                    </li>
                                   <%-- <li class="list-inline-item display-inline">
                                       <img id="icon3" src="images/icon-3.jpg" title="Upload Video"  alt=""
                                          onclick="showImage('3');" />
                                    </li>--%>
                                    <li class="list-inline-item display-inline">
                                       <img id="icon4" src="images/icon-4.jpg" title="Update Link"  alt=""
                                          onclick="showImage('4');" />
                                    </li>
                                 </ul>
                               </div>
                              <!---Tabbing show and hide Radio buttons Ended-->
                                 <!---Upload Photo-->
                                 <div class="div2 upload-con m-t-10" id="div2" runat="server" clientidmode="Static">
                                    <span id="spnChoseFileButton" class="btn btn-outline-primary flex-align-center inline-block">
                                        <span class="camera-icon">
                                         <span class="icon-camera"></span>
                                        </span>
                                        <span id="" class="upload-text"> Upload Media</span>
                                    
                                        <asp:FileUpload ID="PhotoUpload" class="form-control" ClientIDMode="Static" runat="server" />           
                                    </span>
                                       <span id="spnChosenFile" class="file-name-icon" style="display:none;">
                                            <span id="spnChosenFileName" class="truncate"></span>
                                            <a id="spnChosenFileDelete" onclick="$('#PhotoUpload').val('');$('#PhotoUpload').trigger('onchange');return false;"><span class="lnr lnr-trash"></span></a>
                                        </span>
                                     <p>   
                                     <asp:Label ID="lblErrorMess" ClientIDMode="Static" runat="server" Text="" ForeColor="Red"></asp:Label>
                                     </p>
                             
                             
                                 </div>
                                 <!---Upload Photo Ended-->
                                 <div class="div2 form-group m-t-10" id="div4" runat="server" clientidmode="Static">
                                    <asp:TextBox ID="txtPostLink" AutoCompleteType="Disabled" ClientIDMode="Static" CssClass="uploadTxtField testfield_up form-control"
                                       runat="server"></asp:TextBox>
                                    <ajax:TextBoxWatermarkExtender TargetControlID="txtPostLink" ID="TextBoxWatermarkExtender1"
                                       runat="server" WatermarkText="Link">
                                    </ajax:TextBoxWatermarkExtender>
                                    <br />
                                    <asp:Label ID="lblLink" ClientIDMode="Static" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <asp:HiddenField ID="hdnintStatusUpdateId" ClientIDMode="Static" Value="" runat="server" />
                                    <asp:Label ID="lblintCommentId" Style="display: none;" ClientIDMode="Static" Text=""
                                       runat="server"></asp:Label>
                                 </div>
                               <asp:Label ID="lbLinkPhotoText" style="display:none;" ClientIDMode="Static"  runat="server" />
                              <!---Post and Delete buttons-->
                              <div id="dvPostCancelSection" class="submit-con">
                                 <asp:LinkButton ID="lnkDummyPost" runat="server" CssClass="vote" ClientIDMode="Static"
                                    ValidationGroup="post" CausesValidation="true" Text="Post" Style="display: none;"
                                    OnClick="btnPostUpdate_Click"></asp:LinkButton>
                                 <asp:LinkButton ID="LinkButton1" runat="server" ClientIDMode="Static" CausesValidation="true"
                                 Text="Cancel" data-dismiss="modal" CssClass="btn add-scroller btn-outline-primary m-r-15" OnClientClick="ClearOrgText();$('#wallModal').hide();return false;"></asp:LinkButton>
                                 <a id="lnkPostUpdate" runat="server" clientidmode="Static" class="btn btn-primary hide-body-scroll" href="#"
                                    onclick="return validate();">Post</a>
                                  

                              </div>
                               <div id="divPostimage" class="" style="display:none;">
                                    <div class="loader-cover">
                                     <div class="lds-ellipsis">
                                      <div></div>
                                      <div></div>
                                      <div></div>
                                      <div></div>
                                     </div>
                                    </div>
                                   </div>
                           </div>
                        </asp:Panel>

                        </div>
                     </div>
               </div>
            </div>
         </div>

         <!--left box starts-->
         <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="panel-cover clearfix">
            <ContentTemplate>
                <asp:HiddenField ID="hdnCommentImageSrcId" runat="server" ClientIDMode="Static" Value='' />
               <div id="divSecondWall" runat="server" clientidmode="Static" class="width_h">
                  <!---Center panel Start-->
                  <div class="center-panel">
                     <div class="custom-nav-con group-page-tab">
                       <GroupM:GroupDetailsM ID="grpDetailsM" runat="server" />
                        <!---Tabs Name Start-->
                        <ul class="custom-nav-control nav nav-tabs">
                           <li class="nav-item">
                              <asp:LinkButton ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static"
                                 OnClick="lnkProfile_Click" class="nav-link"></asp:LinkButton>
                           </li>
                           <li  class="nav-item" id="DivHome" runat="server">
                                 <asp:LinkButton ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"
                                  class="forumstabAcitve nav-link"></asp:LinkButton>
                           </li>
                           <li  class="nav-item" id="DivForumTab" runat="server" clientidmode="Static" >
                                 <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                    OnClick="lnkForumTab_Click" class="nav-link"></asp:LinkButton>
                           </li>
                           <li  class="nav-item" id="DivUploadTab" runat="server" clientidmode="Static">

                                 <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                    OnClick="lnkUploadTab_Click" class="nav-link"></asp:LinkButton>

                           </li>
                           <li  class="nav-item" id="DivPollTab" runat="server" clientidmode="Static">
                                 <asp:LinkButton ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static"
                                    OnClick="lnkPollTab_Click" class="nav-link"></asp:LinkButton>
                           </li>
                           <li  class="nav-item" id="DivEventTab" runat="server" clientidmode="Static" >
                                 <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                    OnClick="lnkEventTab_Click" class="nav-link"></asp:LinkButton>
                           </li>
                           <li  class="nav-item" id="DivMemberTab" runat="server" clientidmode="Static">
                                 <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                    OnClick="lnkEventMemberTab_Click" class="nav-link"></asp:LinkButton>
                           </li> 
                        </ul>
                        <!---Tabs Name Ended-->
                        <!---Details Container-->
                        <div class="tab-content m-t-15">
                           <!---Heading-->
                           <div class="btn-title-con">
                            <div>
                             <h5 class="card-title float-left">Wall</h5>
                            </div>
                            <div>
                             <button class="btn btn-outline-primary float-right" data-toggle="modal" data-target="#wallModal" OnClick="showImage('1');return false;">Add Post</button>
                            </div>
                           </div><div class="clearfix"></div>
                           <div class="custom-nav-container tab-pane container active"> 
                              <div class="wallRightContainer">
                                 <!---Popup for Inserting Media Links-->
      
                                 <!--section starts-->
                                 <div class="lblmessage_group" style="display:none;">
                                    <asp:Label ID="lblMessage" runat="server" ClientIdMode="Static"></asp:Label>
                                 </div>
                                 <Share:ShareDetails ID="ShareDetails" runat="server" />
                                 <asp:ListView ID="lstPostUpdates" runat="server" OnItemCommand="lstPostUpdates_ItemCommand"
                                    OnItemDataBound="lstPostUpdates_ItemDataBound" OnItemCreated="lstPostUpdates_ItemCreated">
                                    <ItemTemplate>
                                       <asp:HiddenField ID="hdnPostUpdateId" Value='<%# Eval("Id") %>' runat="server" />
                                       <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' runat="server" />
                                       <asp:HiddenField ID="hdnVideoName" Value='<%# Eval("strVideoPath") %>' runat="server" />
                                       <asp:HiddenField ID="hdnPhotoPath" Value='<%# Eval("strPhotoPath") %>' runat="server" />
                                       <asp:HiddenField ID="hdnPostLink" Value='<%# Eval("strPostLink") %>' runat="server" />
                                       <asp:HiddenField ID="hdnintUserTypeId" runat="server" Value='<%#Eval("intUserTypeId") %>' />
                                       <asp:HiddenField ID="hdnIframe" Value='<%# "VideoFiles/"+Eval("strVideoPath")%>'
                                          ClientIDMode="Static" runat="server" />
                                       <asp:HiddenField ID="hdnShared" Value='<%# Eval("intShared") %>' runat="server" />
                                       <asp:HiddenField ID="hdnSharedId" Value='<%# Eval("intSharedId") %>' runat="server" />
                                       <asp:HiddenField ID="hdnSharedUserTypeId" Value='<%# Eval("intSharedUserTypeId") %>'
                                          runat="server" />
                                       <asp:HiddenField ID="hdnLikeUserPost" Value='<%# Eval("LikeUserPostId") %>' runat="server" />
                                       <div class="card post-con like-icon-style">
                                          <!---Post Header-->
                                          <div class="post-header">
                                             <span class="avatar-img">
                                                <img src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' id="imgprofile" runat="server" alt=""
                                                   class="rounded-circle" width="42" height="42" />
                                                <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                             </span> 
                                             <span class="user-name-date">
                                                <span class="user-name">
                                                 <asp:LinkButton ID="Label1"
                                                   Font-Underline="false" CommandName="Details" runat="server" Text='<%#Eval("UserName") %>'>
                                                 </asp:LinkButton>
                                                </span><span class="date">
                                                 <asp:Label ID="lblAddedOn" Text='<%# Eval("dtAddedOn") %>' runat="server">
                                                 </asp:Label>
                                                </span> 
                                             </span>
                                             <span class="more-btn float-right" id="spanEditDelete" visible="false" runat="server">
                                              <span class="dropdown">
                                               <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <img src="images/more.svg" alt="" class="more-btn">
                                               </a>

                                               <span class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                                <asp:LinkButton ID="lnkEditPost" Font-Underline="false" Visible="false" runat="server"
                                                 ToolTip="Edit" class="dropdown-item hide-body-scroll" CommandName="Edit Post" CausesValidation="false">
                                                 <span class="lnr lnr-pencil"></span> Edit                                    
                                                </asp:LinkButton>
                                                  <!--  <asp:Label ID="lblPipe" runat="server" Font-Size="Small" Visible="false" ForeColor="black"
                                                      Text="&nbsp;&nbsp;|&nbsp;&nbsp;" Font-Bold="false">
                                                         
                                                      </asp:Label> -->
                                                <asp:LinkButton ID="lnkDeleteComment" Font-Underline="false" Visible="false"
                                                 ToolTip="Delete"  class="dropdown-item hide-body-scroll" CausesValidation="false"
                                                 runat="server" CommandName="Delete Post" OnClientClick="docdelete();"><span class="lnr lnr-trash"></span> Delete                                       
                                                </asp:LinkButton>
                                               </span>
                                              </span>  
                                             </span>  
                                          </div>
                                          <!---Post body-->
                                          <div class="post-body">
                                             <p>
                                                 <asp:Label 
                                                   ID="lblPostDescription" Text='<%# Eval("strPostDescription") %>' runat="server"></asp:Label>
                                             </p>  
                                             <!---description ended-->
                                                   <asp:HyperLink ID="hplLinkUrl" ClientIDMode="Static" Target="_blank" ToolTip="Posted Link"
                                                      NavigateUrl='<%# Eval("strPostLink").ToString().Contains("http:")||Eval("strPostLink").ToString().Contains("https:")==true ? Eval("strPostLink") : "http://" + Eval("strPostLink") %>'                                                       
                                                      Text='<%#Eval("strPostLink") %>'
                                                      Font-Size="Small" runat="server"></asp:HyperLink>
                                               
                                                <div class="home_page_image">      
                                                   <img src='<%# "UploadedPhoto/"+Eval("strPhotoPath")%>' id="imgPhoto" runat="server" class="img-fluid" alt="" />
                                                </div>      
                                                <div id="dvVideo" runat="server" clientidmode="Static">
                                                   <embed id="frm1" src='<%# "VideoFiles/"+Eval("strVideoPath")%>' clientidmode="Static"
                                                      starttime="00:00" controls="true" autoplay="false" autostart="false" quality="high"
                                                      cache="true" correction="full" pluginurl="http://quicktime.en.softonic.com/download"
                                                      width="400" height="300" scale="aspect" pluginspage="http://quicktime.en.softonic.com/download" />
                                                   <a id="lbtnControlPanel" title="Play Video" runat="server"
                                                      clientidmode="Static" onclick="CPhit(this);"></a>
                                                </div>
                                                <div id="divChrome" runat="server" clientidmode="Static">
                                                   <div id='media-player'>
                                                      <video id='media-video' controls class="img-fluid">
                                                         <source src='<%# "VideoFiles/"+Eval("strVideoPath")%>' type='video/mp4'>
                                                         <source src='<%# "VideoFiles/"+Eval("strVideoPath")%>' type='video/mp3'>
                                                         <source src='<%# "VideoFiles/"+Eval("strVideoPath")%>' type='video/webm'>
                                                         <source src='<%# "VideoFiles/"+Eval("strVideoPath")%>' type='video/x-msvideo'>
                                                         <object type="application/x-shockwave-flash"
                                                         <param name="movie" value='<%# "VideoFiles/"+Eval("strVideoPath")%>' />
                                                         <param name="flashvars" value="controls=true&file=<%# "VideoFiles/"+Eval("strVideoPath")%>" />
                                                         </object>
                                                      </video>
                                                   </div>
                                                </div>         
                                          </div>  
                                          <!---Post Footer-->
                                          <div class="post-footer">
                                             <!---Like Comment and Share-->
                                             <ul class="list-inline">
                                                <li class="list-inline-item">
                                                   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                   <ContentTemplate>
                                                                                                    
                                                     <span class="d-flex align-items-center links-like-btn">
                                                      <asp:LinkButton class="active-toogle" Font-Underline="false" ID="lnkLike" runat="server" CommandName="Like Post">
                                                       <span class="like-btn"></span>
                                                          
                                                      </asp:LinkButton>
                                                         <asp:HiddenField ID="hdnlnkLike" Value="" runat="server" />
                                                   
                                                      <span class="d-flex">
                                                       <asp:Label ID="lnkLikePost" runat="server" Text='<%#Eval("Likes") %>' ToolTip="View Likes" ClientIDMode="Static">
                                                       </asp:Label>&nbsp;
                                                       <asp:Label runat="server" ClientIDMode="Static" ID="lblLikePostText" Text='<%# ((int)Eval("Likes") == 1) ? "Like" : "Likes" %>'></asp:Label>
                                                      </span>
                                                     </span>
                                                  
                                                   </ContentTemplate>
                                                   <Triggers>
                                                      <asp:AsyncPostBackTrigger ControlID="lnkLike" />
                                                   </Triggers>
                                                   </asp:UpdatePanel>
                                                </li>
                                                <li class="list-inline-item">
                                                     
                                                 <asp:LinkButton class="active-toogle" ID="lnkComment" Font-Underline="false" runat="server" CommandName="Comment">
                                                  <span class="comment-btn"></span>
                                                  <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Comments") %>' ClientIDMode="Static"></asp:Label>&nbsp;                                            
                                                  <asp:Label ID="lblCommentsCountText" runat="server" ClientIDMode="Static" Text='<%# (((int)Eval("Comments") == 1) ? "Comment" : "Comments") %>'></asp:Label>
                                                 </asp:LinkButton>
                                                </li>
                                               <%--<li id="dvSharelink" style="display: none;" runat="server" class="forumShare list-inline-item">
                                                 <asp:UpdatePanel ID="up1" runat="server">
                                                  <ContentTemplate>
                                                   <asp:LinkButton  class="active-toogle hide-body-scroll" ID="lnkShare" Font-Underline="false" 
                                                    runat="server" CommandName="Share" ToolTip="Share">   <span class="share-btn"></span><asp:Label ID="lblShare" runat="server" Text='<%#Eval("Share") %>'></asp:Label> Share</asp:LinkButton>
                                                  </ContentTemplate>
                                                  <Triggers>
                                                   <asp:AsyncPostBackTrigger ControlID="lnkShare" />
                                                  </Triggers>
                                                 </asp:UpdatePanel>
                                                </li>--%>
                                           </ul>   
                                          </div> 
                                          <div class="comments">
                                             
                                             <%--<div class="commentTxtNew">
                                                <span>
                                               
                                                   <div id="divshare" style="display: none" runat="server">
                                                      Shared&nbsp;
                                                      <asp:LinkButton ForeColor="#006699" ID="lnkSharedUserName" CommandName="SharedDetails"
                                                         runat="server" Font-Bold="false" Text='<%#Eval("SharedUserName")+ "&#39;s"%>' Font-Italic="true">
                                                      </asp:LinkButton>
                                                      &nbsp;
                                                            <asp:Label ID="lblSahreType" runat="server" Font-Size="Small" ForeColor="black" Font-Italic="true"
                                                         Font-Bold="false"></asp:Label>
                                                   </div>
                                                </span>
                                                &nbsp;&nbsp;
                                              
                                             </div>--%>
                  
                                             <!--comments section starts-->
                                             <div class="commentSection for_group_page comment-con">
                                                <asp:UpdatePanel ID="upcmnty" runat="server">
                                                   <ContentTemplate>
                                                      <div class="viewAllComments view-all-comments a_tag float-right">
                                                         <asp:LinkButton ID="lbchldlstHideShow" CommandName="Hide/ShowComments" runat="server"
                                                            ToolTip="View Comments" Text='<%# String.Concat("View all","  " , Eval("Comments")," ", ((int)Eval("Comments") == 1) ? "Comment" : "Comments")%>'                                                             
                                                            ClientIDMode="Static"></asp:LinkButton><%--Text='<%# ((int)Eval("Comments") == 1) ? "Comment" : "Comments" %>'--%>
                                                         <asp:ImageButton ID="imgUpDown" class="up-down-arrow" CommandName="Hide/ShowComments" runat="server" ImageUrl="images/chevron-up.svg" />
                                                      </div>
                                                      <div class="clearfix"></div>
                                                   </ContentTemplate>
                                                   <Triggers>
                                                      <asp:AsyncPostBackTrigger ControlID="lbchldlstHideShow" EventName="Command" />
                                                   </Triggers>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                   <ContentTemplate>
                                                      <asp:ListView ID="lstChild" runat="server" OnItemCommand="lstChild_ItemCommand" DataKeyNames="intID">
                                                         <ItemTemplate>
                                                            <asp:HiddenField ID="hdnCommentId" runat="server" Value='<%#Eval("intID") %>' />
                                                            <asp:HiddenField ID="hdnintUserTypeId" runat="server" Value='<%#Eval("intUserTypeId") %>' />
                                                            <asp:HiddenField ID="hdnRegistrationId" runat="server" Value='<%#Eval("intRegistrationId") %>' />
                                                            <asp:HiddenField ID="hdnCommentLikeId" runat="server" Value='<%#Eval("CommentLikeId") %>' />
                                                            <!--comment set start-->
                                                            <div class="commentSet">
                                                               <div class="post-header">
                                                                  <span class="avatar-img">
                                                                     <img src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' id="imgCommentpic" runat="server" alt=""
                                                                        class="commentPhoto rounded-circle"  />
                                                                     <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                                  </span>
                                                                  <span class="user-name-date">
                                                                     <span class="user-name">
                                                                        <asp:LinkButton class="un-anchor" ID="Label1"
                                                                           Font-Underline="false" CommandName="Post Comment Details" runat="server" Text='<%#Eval("UserName") %>'></asp:LinkButton>
                                                                     </span> 
                                                                     <span class="date">
                                                                          <asp:Label ID="lblPostedOn" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                                                     </span> 
                                                                  </span>
                                                                  <span class="more-btn float-right">
                                                                   <asp:UpdatePanel ID="upda" runat="server" Visible="false">
                                                                    <ContentTemplate>
                                                                     <span class="dropdown">
                                                                       <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                                                        <img src="images/more.svg" alt="" class="more-btn">
                                                                       </a>

                                                                       <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start">
                                                                        <%--<a class="dropdown-item" href="#"></a>
                                                                        <a class="dropdown-item" href="#"></a>--%>
                                                                        <asp:LinkButton ID="lnkEditComment" Font-Underline="false" Visible="false"
                                                                         ToolTip="Edit" Text="Edit" CommandName="Edit Comment"
                                                                         CausesValidation="false" CssClass="dropdown-item" runat="server"><span class="lnr lnr-pencil"></span> Edit                                       
                                                                        </asp:LinkButton>
                                                                         <%--<asp:Label ID="lblPipe" runat="server" Font-Size="Small" Visible="false" ForeColor="black"
                                                                            Text="&nbsp;&nbsp;|&nbsp;&nbsp;" Font-Bold="false"></asp:Label>--%>
                                                                        <asp:LinkButton ID="lnkDeleteComment" Font-Underline="false" Visible="false" CausesValidation="false" runat="server"
                                                                         ToolTip="Delete" OnClientClick="javascript:docdelete();" Text="Delete" class="dropdown-item hide-body-scroll" CommandName="Delete Comment">
                                                                         <span class="lnr lnr-trash"></span> Delete
                                                                        </asp:LinkButton>
                                                                       </span>
                                                                      </span> 
                                                                     </ContentTemplate>
                                                                     <Triggers>
                                                                      <asp:AsyncPostBackTrigger ControlID="lnkLikes" />
                                                                     </Triggers>
                                                                    </asp:UpdatePanel>


                                                                  </span>
                                                               </div>
                                                               <div class="post-body">
                                                                  <p>
                                                                     <asp:Label ID="lblstr" class="lblstar"
                                                                        runat="server" Text='<%#Eval("strComment") %>'></asp:Label>
                                                                  </p> 
                                                                  <div class="group-added-comments">
                                                                       

                                                                      <span class="d-flex align-items-center links-like-btn">
                                                                        <asp:LinkButton class="active-toogle" Font-Underline="false" ID="lnkLikes" runat="server" 
                                                                          Text="" CommandName="Like Comment" ToolTip="Like Comment">
                                                                       <span class="like-btn"></span>
                                                          
                                                                      </asp:LinkButton>
                                                                         <asp:HiddenField ID="hdnlnkLikes" Value="" runat="server" />
                                                   
                                                                      <span class="d-flex">
                                                                       <asp:Label ID="lnkLikeComment" runat="server" Text='<%#Eval("Likes") %>' ToolTip="View Likes" ClientIDMode="Static">
                                                                       </asp:Label>&nbsp;
                                                                       <asp:Label runat="server" ClientIDMode="Static" ID="lblLikeCommentText" Text='<%# ((int)Eval("Likes") == 1) ? "Like" : "Likes" %>'></asp:Label>
                                                                      </span>
                                                                     </span>
                                                                  </div>     
                                                               </div> 

                                                        
                                                            </div>
                                                            <!--comment set end-->
                                                         </ItemTemplate>
                                                      </asp:ListView>
                                                      <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" class="d-flex pt-3 mt-0">
                                                         <ContentTemplate>
                                                            <span class="avatar-img mr-3">
                                                             <img src="images/comment-profile.jpg" alt="" id="imgComment" runat="server" class="rounded-circle">
                                                             
                                                            </span>
                                                            <asp:Panel ID="pnlEnterComment" runat="server" ClientIDMode="Predictable" DefaultButton="lnkEnterComment" class="w-100 position-relative m-r-padding">
                                                               <asp:TextBox ID="txtComment" runat="server" CssClass="commentWrite form-control height-moz postcommentduplicate walll-comments" placeholder="Write a comment" 
                                                                autocomplete="off" MaxLength="500" TextMode="multiline" onkeydown="return postCommentNew.bind(this)(event)" oninput="postCommentInput.bind(this)(event)" rows="3">
                                                               </asp:TextBox>
                                                               <%--<ajax:TextBoxWatermarkExtender TargetControlID="txtComment" ID="TextBoxWatermarkExtender2"
                                                                  runat="server" WatermarkText="Write a comment">
                                                               </ajax:TextBoxWatermarkExtender>--%>
                                                               <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment"
                                                                  Display="Dynamic" ValidationGroup="comment" class="pleasewrite_g_h ml-3" ErrorMessage="Please write a comment"
                                                                  ForeColor="Red"></asp:RequiredFieldValidator>
                                                               <asp:Label Style="display: none;" ID="lblCommentError" runat="server"></asp:Label>
                                                               <asp:LinkButton ID="lnkEnterComment" class="display-none lnkEnterCommentCss" CommandArgument='<%# Eval("Id") %>'
                                                                  CommandName="EnterComment" runat="server" Text="Enter"></asp:LinkButton>
                                                            </asp:Panel>
                                                         </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                   </ContentTemplate>
                                                </asp:UpdatePanel>
                                             </div>
                                             <!--comments section ends-->
                                          </div>
                                       </div>
                                    </ItemTemplate>
                                 </asp:ListView>
                                 <div id="pLoadMore" runat="server" align="center" clientidmode="Static">

                                  <div id="imgLoadMore" runat="server" class="lds-ellipsis" ClientIDMode="Static">
                                   <div></div>
                                   <div></div>
                                   <div></div>
                                   <div></div>
                                  </div>

                                  <asp:Button ID="imgLoadMore2" runat="server" ClientIDMode="Static" Style="display: none;"
                                   OnClick="imgLoadMore_OnClick" />
                                  
                                 </div>
                                 <p align="center" Style="display: none;">
                                  <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server"
                                   ClientIDMode="Static" ForeColor="Red" Visible="false">
                                  </asp:Label>
                                 </p>
                                 <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                                 <div style="display: none" class="innerContainer">
                                  <div class="bgLine" id="pagination">
                                    &nbsp;
                                  </div>
                                  <div class="cls">
                                  </div>
                                  <div id="dvPage" runat="server" class="pagination" clientidmode="Static">
                                   <asp:LinkButton ID="lnkFirst" runat="server" CssClass="PagingFirst" OnClick="lnkFirst_Click"> </asp:LinkButton>
                                   <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">&lt;&lt;</asp:LinkButton>
                                   <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand" OnItemDataBound="rptDvPage_ItemDataBound">
                                    <ItemTemplate>
                                     <asp:LinkButton ID="lnkPageLink" CssClass="Paging" runat="server" ClientIDMode="Static"
                                      CommandName="PageLink" Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                   </asp:Repeater>
                                   <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">&gt;&gt;</asp:LinkButton>
                                   <asp:LinkButton ID="lnkLast" runat="server" Style="background: none" OnClick="lnkLast_Click"><img src="<%=ResolveUrl("images/spacer.gif")%>" class="last" alt="" /></asp:LinkButton>
                                   <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                   <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                                   <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                                   <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                                   <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                  </div>
                                 </div>
                                 <!--section ends-->
                              </div>
                              <!--wall right column starts-->
                              <asp:HiddenField ID="hdnTabId" runat="server" ClientIDMode="Static" />
                              <asp:HiddenField ID="hdnPostId" runat="server" ClientIDMode="Static" />
                              <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
                              <asp:HiddenField ID="hdncommentfocus" runat="server" ClientIDMode="Static"/>  
                           </div>   
                        </div>
                        <!---Details Container ended-->
                     </div>
                  </div>
                  <!---Center panel Ended-->
                  <!---Right Profile Panel-->
               
                   <asp:UpdatePanel ID="uppanel" runat="server">
                      <ContentTemplate>
                         <Group:GroupDetails ID="grpDetails" runat="server" />
                      </ContentTemplate>
                   </asp:UpdatePanel>
                   
               </div>
        
            </ContentTemplate>
            <Triggers>
               <asp:AsyncPostBackTrigger ControlID="lnkDummyPost" />
               <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
               <asp:AsyncPostBackTrigger ControlID="LinkButton1" />
            </Triggers>
         </asp:UpdatePanel>
         <asp:HiddenField ID="hdnPhoto" runat="server" ClientIDMode="Static" />
         <asp:HiddenField ID="hdnDocName" runat="server" ClientIDMode="Static" />
         <asp:HiddenField ID="hdnErrorMsg" runat="server" ClientIDMode="Static" />
         <asp:HiddenField ID="hdnImage" runat="server" ClientIDMode="Static" />
         <%--<asp:HiddenField ID="hdnVideo" runat="server" ClientIDMode="Static" />--%>
         <script type="text/javascript">
              $(document).ready(function() {
        $('.walll-comments').on('input propertychange', function() {
            CharLimit(this, 500);
        });
    });
                    function CharLimit(input, maxChar) {
                  var len = $(input).val().length;
                     
                  if (len > maxChar) {
                      $(input).val($(input).val().substring(0, maxChar));

                    
                  }
              }

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
                var ID = "#" + $("#hdnTabId").val();
                $(ID).focus();
            });
            
            $(document).ready(function () {
                var ID = "#" + $("#hdnPostId").val();
                $(ID).focus();
            });
            
            $(document).ready(function () {
                var ID = "#" + $("#hdnLoader").val();
                $(ID).focus();
            
            });
            
            $(document).ready(function () {
                var ID = "#" + $("#hdncommentfocus").val();
                $(ID).focus();
            
            });
            
            $(document).ready(function () {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                      $('.walll-comments').on('input propertychange', function() {
            CharLimit(this, 500);
        });
                            function CharLimit(input, maxChar) {
                  var len = $(input).val().length;
                     
                  if (len > maxChar) {
                      $(input).val($(input).val().substring(0, maxChar));

                    
                  }
              }
                    var ID = "#" + $("#hdnTabId").val();
                    $(ID).focus();
            
                    var ID1 = "#" + $("#hdnPostId").val();
                    $(ID1).focus();
            
                    var ID2 = "#" + $("#hdnLoader").val();
                    $(ID2).focus();
            
                    var ID3 = "#" + $("#hdncommentfocus").val();
                    $(ID3).focus();
                });
            });
         </script>
         <script type="text/javascript">
            $(".viewAllComments").click(function () {
                $("#lstChild").slideToggle("slow");
                $(".viewAllComments").toggle();
            });
         </script>
         <script type="text/jscript">
            function Cancel() {
                document.getElementById("divCancelPopup").style.display = 'none';
                return false;
            }
         </script>
 
   <script type="text/javascript">
         // tex area maximum lenght

      function showImage(image) {
          var src = "";
          if (image == 1) {
              src = $("#icon1").attr("src");
          }
          else if (image == 2) {
              src = $("#icon2").attr("src");
          }
          else if (image == 3) {
              src = $("#icon3").attr("src");
          }
          else if (image == 4) {
              src = $("#icon4").attr("src");
          }
      
          if (src == "images/icon-1.jpg") {
              document.getElementById("icon1").src = src;
              document.getElementById("icon2").src = "images/icon-2.jpg";
              //document.getElementById("icon3").src = "images/icon-3.jpg";
              document.getElementById("icon4").src = "images/icon-4.jpg";
              document.getElementById('div1').style.display = 'block';
              document.getElementById('div2').style.display = 'none';
              //document.getElementById('div3').style.display = 'none';
              document.getElementById('div4').style.display = 'none';
              $("#ddlPostType").css("margin-top", "3px");
              return;
          }
          else if (src == "images/icon-2.jpg") {
              document.getElementById("icon2").src = src;
              document.getElementById("icon1").src = "images/icon-1.jpg";
              //document.getElementById("icon3").src = "images/icon-3.jpg";
              document.getElementById("icon4").src = "images/icon-4.jpg";
              document.getElementById('div1').style.display = 'block';
              document.getElementById('div2').style.display = 'block';
              //document.getElementById('div3').style.display = 'none';
              document.getElementById('div4').style.display = 'none';
              //$("#ddlPostType").css("margin-top", "-40px");
          }
      
          //else if (src == "images/icon-3.jpg") {
          //    document.getElementById("icon3").src = src;
          //    document.getElementById("icon1").src = "images/icon-1.jpg";
          //    document.getElementById("icon2").src = "images/icon-2.jpg";
          //    document.getElementById("icon4").src = "images/icon-4.jpg";
          //    document.getElementById('div1').style.display = 'block';
          //    document.getElementById('div2').style.display = 'none';
          //    document.getElementById('div3').style.display = 'block';
          //    document.getElementById('div4').style.display = 'none';
          //    //$("#ddlPostType").css("margin-top", "-40px");
          //    return;
          //}
          else if (src == "images/icon-4.jpg") {
              document.getElementById("icon4").src = src;
              document.getElementById("icon1").src = "images/icon-1.jpg";
              document.getElementById("icon2").src = "images/icon-2.jpg";
              //document.getElementById("icon3").src = "images/icon-3.jpg";
              document.getElementById('div1').style.display = 'block';
              document.getElementById('div2').style.display = 'none';
              //document.getElementById('div3').style.display = 'none';
              document.getElementById('div4').style.display = 'block';
              //$("#ddlPostType").css("margin-top", "-60px");
              return;
          }
      }   
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          $(":input[data-watermark]").each(function () {
              $(this).val($(this).attr("data-watermark"));
              $(this).bind('focus', function () {
                  if ($(this).val() == $(this).attr("data-watermark")) $(this).val('');
              });
              $(this).bind('blur', function () {
                  if ($(this).val() == '') $(this).val($(this).attr("data-watermark"));
                  $(this).css('color', '#a8a8a8');
              });
          });
      });
      
      $(document).ready(function () {
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              $(":input[data-watermark]").each(function () {
                  $(this).val($(this).attr("data-watermark"));
                  $(this).bind('focus', function () {
                      if ($(this).val() == $(this).attr("data-watermark")) $(this).val('');
                  });
                  $(this).bind('blur', function () {
                      if ($(this).val() == '') $(this).val($(this).attr("data-watermark"));
                      $(this).css('color', '#a8a8a8');
                  });
              });
          });
      });
   </script>
   <script type="text/javascript">
      function ShowLoading(id) {
          location.href = '#' + id;
      }
   </script>
   <script type="text/javascript" language="javascript">
      $(document).ready(function () {
          //document.getElementById("hdnVideo").value = "";
          document.getElementById("hdnImage").value = "";
          document.getElementById("PhotoUpload").onchange = function () {
              document.getElementById("hdnImage").value = this.value;
              if (this.value && this.value != "") {
                  if (!validateUploadMedia()) {
                      return;
                  }
                  $('#spnChosenFile').show();
                  $('#spnChoseFileButton').hide();
                  $('#spnChosenFileName').text($("#PhotoUpload").get(0).files[0].name);
                   $('#lblErrorMess').text('');
              } else {
                  $('#spnChosenFile').hide();
                  $('#spnChoseFileButton').show();
              }
              //document.getElementById("hdnVideo").value = "";
          };
          //document.getElementById("VideoUpload").onchange = function () {
          //    document.getElementById("hdnVideo").value = this.value;
          //    document.getElementById("hdnImage").value = "";
          //};
      });
      $(document).ready(function () {
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              //document.getElementById("hdnVideo").value = "";
              document.getElementById("hdnImage").value = "";
              document.getElementById("PhotoUpload").onchange = function () {
                  document.getElementById("hdnImage").value = this.value;
                  //document.getElementById("hdnVideo").value = "";
                  if (this.value && this.value != "") {
                      if (!validateUploadMedia()) {
                          return;
                      }
                      $('#spnChosenFile').show();
                      $('#spnChoseFileButton').hide();
                      $('#spnChosenFileName').text($("#PhotoUpload").get(0).files[0].name);
                  } else {
                      $('#spnChosenFile').hide();
                      $('#spnChoseFileButton').show();
                  }
              };
              //document.getElementById("VideoUpload").onchange = function () {
              //    document.getElementById("hdnVideo").value = this.value;
              //    document.getElementById("hdnImage").value = "";
              //};
          });
      });
      function validate() {
          $('#lnkPostUpdate').addClass('disabled');
          $('#dvPostCancelSection').hide();
          $('#divPostimage').show();
          
          $("#hdnPhoto").val('');
          $("#hdnDocName").val('');
          $("#hdnErrorMsg").val('');
          if ($('#txtPostUpdate').val() == "" || $('#txtPostUpdate').val() == "What's on your mind?") {
              document.getElementById('RequiredFieldValidator1').style.display = "block";
              $('#lnkPostUpdate').removeClass('disabled');
              $('#divPostimage').hide();
              $('#dvPostCancelSection').show();
              return false;
          }
          if (document.getElementById('div2').style.display == 'block') {
              var fileUpload = $("#PhotoUpload").get(0);
              if (fileUpload.files.length > 0) {
                  SaveFile();
              }
              else {
                  document.getElementById('lblErrorMess').innerHTML = 'Please select a photo/video.';
                  $('#lnkPostUpdate').removeClass('disabled');
                  $('#divPostimage').hide();
                  $('#dvPostCancelSection').show();
                  return false;
              }
          }
          //else if (document.getElementById('div3').style.display == 'block') {
          //    var fileUpload = $("#VideoUpload").get(0);
          //    if (fileUpload.files.length > 0) {
          //        SaveFile();
          //    }
          //    else {
          //        document.getElementById('lblErrorvideo').innerHTML = 'Please select a video.';
          //        return false;
          //    }
          //}
          else if (document.getElementById('div4').style.display == 'block') {
              var PostLink = document.getElementById('txtPostLink').value;
              if (PostLink == "Link") {
                  document.getElementById('lblLink').innerHTML = 'Please paste a link.';
                  $('#lnkPostUpdate').removeClass('disabled');
                  $('#divPostimage').hide();
                  $('#dvPostCancelSection').show();
                  return false;
              }
              else {
                  document.getElementById('lnkDummyPost').click();
              }
      
          }
          else {
              document.getElementById('lnkDummyPost').click();
          }
      }  //End of function validate.
      function ClearOrgText(notClearStatusId) {
          if (!notClearStatusId) {
              $('#hdnintStatusUpdateId').val('');
          }
          $('#txtPostUpdate').val('');
          $('#ddlPostType').val($("#ddlPostType option:first").val());
          $('#txtPostLink').val('');
          $('#PhotoUpload').val('');

          $('#PhotoUpload').trigger('onchange');
          $('#lnkPostUpdate').removeClass('disabled');
          $('#divPostimage').hide();
          $('#dvPostCancelSection').show();
          $('#lblErrorMess').text('');
         
          $("input[name='bn'][value='1']").click();
          $('#divPostRadioBtn').show();
          $('#lbLinkPhotoText').text("");
          $('#lbLinkPhotoText').hide();
          $('#hdnvdeonme').val('');
          $('#hdnPhotoName').val('');
          //$('#VideoUpload').val('');
          return true;
      }
   </script>
   <script type="text/javascript">
      function MesProfileClose() {
          document.getElementById("divGroupSucces").style.display = 'none';
      }
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          document.getElementById('imgLoadMore').style.display = 'none';
      });
      
      $(document).ready(function () {
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              document.getElementById('imgLoadMore').style.display = 'none';
          });
      });
   </script>
   <script type="text/javascript">
      $(document).scroll(function () {
          if ($(document).height() <= $(window).scrollTop() + $(document).height()) {
              document.getElementById('imgLoadMore').style.display = 'block';
              var v = $("#lblNoMoreRslt").text();
              setTimeout(1500);
              var maxCount = $("#hdnMaxcount").val();
              if (maxCount <= 10) {
                  $("#lblNoMoreRslt").css("display", "none");
              } else {
      
                  if (v != "No more results available...") {
                      document.getElementById('<%= imgLoadMore2.ClientID %>').click();
                  }
              }
          }
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
      function SaveFile() {
          var fileUpload = $("#PhotoUpload").get(0);
          //var videoUpload = $("#VideoUpload").get(0);
          if (fileUpload.files.length > 0) {
              var files = fileUpload.files;
              var data = new FormData();
              for (var i = 0; i < files.length; i++) {
                  data.append(files[i].name, files[i]);
              }
          }


          //else if (videoUpload.files.length > 0) {
          //    var files = videoUpload.files;
          //    var data = new FormData();
          //    for (var i = 0; i < files.length; i++) {
          //        data.append(files[i].name, files[i]);
          //    }
          //}
          //debugger;
          $.ajax({
              type: "POST",
              url: "handler/FileUploadHandlerHome.ashx",
              contentType: false,
              processData: false,
              data: data,
              success: function (result) {
                  if (result == 'File format not supported.' || result == 'File size should be less than or equal to 3MB.' || result == 'Please choose a video file less than 12MB.') {
                      $("#hdnErrorMsg").val(result)
                  }
                  else {
                      var v = result.split(":");
                      $("#hdnPhoto").val(v[0]);
                      $("#hdnDocName").val(v[1]);
                  }
                  document.getElementById("lnkDummyPost").click();
              },
              error: function () {
                  alert("There was error uploading files!");
              }
          });
      }
      
   </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("input[name$='bn']").click(function() {
                var radio_value = $(this).val();
                $('#icon' + radio_value).click();
                if (radio_value != 4) {
                    $('#txtPostLink').val('');
                }

            });
            $('[name="bn"]:checked').trigger('click');
        });

        function ShowEditPost(strPostId, strPostType, strPostDescription, strPostLink, strVideoPath , strPhotoPath) {
            
            $('#wallModal').modal('show');
            ClearOrgText(true);

            $('#hdnintStatusUpdateId').val(strPostId);
           
            $('#txtPostUpdate').val(strPostDescription);
            if (strPostType && strPostType != '') {
                $('#ddlPostType').val(strPostType);
            }
            if (strPostLink && strPostLink != '') {
                $('#txtPostLink').val(strPostLink);
                $("input[name='bn'][value='4']").click();   
              
            }
            if (strVideoPath && strVideoPath != '') {
                $('#hdnvdeonme').val(strVideoPath);
                $('#divPostRadioBtn').hide();
                $('#lbLinkPhotoText').text(strVideoPath);
                $('#lbLinkPhotoText').show();
            }
            if (strPhotoPath && strPhotoPath != '') {
                $('#hdnPhotoName').val(strPhotoPath);
                $('#divPostRadioBtn').hide();
                $('#lbLinkPhotoText').text(strPhotoPath);
                $('#lbLinkPhotoText').show();

                
            }
        }

        function showUploadMediaError(error) {
            $('#lblErrorMess').text(error);
            $('#PhotoUpload').val('');
            $('#PhotoUpload').trigger('onchange');
            $('#lnkPostUpdate').removeClass('disabled');
            $('#divPostimage').hide();
            $('#dvPostCancelSection').show();
        }

        function validateUploadMedia() {
              var files = $("#PhotoUpload").get(0).files;
              var size = files[0].size;
              var imgFilesTypes = ['gif', 'png', 'jpg', 'jpeg', 'bmp', 'org'];
              var videoFileTypes = ['wmv','mp4', 'mpg', 'mpeg'];
              var ext = files[0].name.split('.').pop().toLowerCase();
              
              if ($.inArray(ext, imgFilesTypes) == -1) {
                  // Not an image check for video
                  if ($.inArray(ext, videoFileTypes) == -1) {
                      showUploadMediaError( "You can only upload valid image and video files.");
                      return false;

                  }
                  // This is a video - check for size
                  if (size > 1024 * 1024 * 12) {
                      showUploadMediaError( "You can only upload videos upto 12 MB.");
                      return false;
                  }

              } else {
                  // This is image
                  if (size > 1024 * 1024 * 3) {
                      showUploadMediaError("You can only upload images upto 3 MB.");
                      return false;
                  }
            }
            return true;
        }

        function showGrpHomeSuccesPopup() {
            showSuccessPopup('Success', $('#lblMessage').text());
        }

        function postCommentNew(e) {
           if (e.keyCode == 13 && $(this).val()) {
               window.location.href = $(this).parent().children('.lnkEnterCommentCss').attr('href');
               return false;
           } else if (e.keyCode == 13) {
               return false;
           }
           
        }

        function postCommentInput(e) {
           var msg = $(this).val().replace(/\n/g, "");
           if (msg != $(this).val()) { // Enter was pressed
               $(this).val(msg);
               window.location.href = $(this).parent().children('.lnkEnterCommentCss').attr('href');
               showLoader1();
           }
       }

    </script>
</asp:Content>
