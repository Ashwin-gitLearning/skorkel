<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControl/CropImg.ascx" TagName="CropImage" TagPrefix="crp1" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="headMain" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!-- Chrome, Firefox OS and Opera -->
    <meta name="theme-color" content="#00b6bc">
    <!-- Windows Phone -->
    <meta name="msapplication-navbutton-color" content="#00b6bc">
    <!-- iOS Safari -->
    <meta name="apple-mobile-web-app-status-bar-style" content="#00b6bc">
    <!--      <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE11" /> -->
    <%--<script src="<%=ResolveUrl("js/jquery-1.8.2.min.js")%>" type="text/javascript" lang="javascript"></script>
      <script src="<%=ResolveUrl("js/jquery.autocomplete.js")%>" type="text/javascript"></script>--%>
    <script src="<%=ResolveUrl("js/ddsmoothmenu.js")%>" type="text/javascript"></script>
    <%--<script src="<%=ResolveUrl("js/Blob.js")%>" type="text/javascript"></script>--%>
    <%--<script src="js/jquery.1.12.4.min.js"></script>
      <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
      <script src="js/bootstrap.min.js"></script>
      <script src="js/custom.js"></script>--%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <asp:HiddenField ID="hdnRegId" runat="server" ClientIDMode="Static" Value="" />
    <label runat="server" id="lblmaster" visible="false"></label>
    <asp:Label ID="lblNotifyCount" Visible="false" Style="margin-top: 1px;" runat="server" ClientIDMode="Static" class="notification"></asp:Label>


    <div class="main-section-inner">
        <span class="m-aside-trigger mt-0 justify-content-between score-card-home">
            <span class="newCard card" runat="server" id="endorCover" clientidmode="Static">
                <span class="endor-cover">
                    <span class="icon">
                        <img src="images/shape-2.svg"></span>
                    <span class="endors-text">
                        <h1>
                            <span id='spnMessCount' runat="server" clientidmode="static" class="text_decoration_color">0</span>
                        </h1>
                        <span class="font-size-10">Skorkel Score</span>
                    </span>
                </span>
            </span>
            <span>
                <span class="lnr lnr-arrow-left"></span>
                <span class="avatar-img">
                    <%--<img src="images/avatar.jpg" alt="" class="rounded-circle">--%>
                    <img id="imgUserMobile" runat="server" src="profile-photo.png" class="header-avatar rounded-circle" />
                </span>
                <span class="avatar-text">
                    <label runat="server" id="lblUserName" class="mb-0 truncate"></label>
                </span>
            </span>
        </span>
        <div class="panel-cover clearfix">
            <div id="divdisplayWall" runat="server" class="display_block">
                <asp:UpdatePanel ID="pnlNews" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
                    <ContentTemplate>
                        <div class="news-feeds border-radius-16 card">
                            <div class="breakingNews bn-green" id="bn4">
                                <div class="bn-title">
                                    <h2><a href="AllNewsListing.aspx">News</a></h2>
                                    <span></span>
                                </div>
                                <ul>
                                    <asp:ListView ID="lstPostNews" runat="server" OnItemCommand="lstPostNews_ItemCommand"
                                        OnItemDataBound="lstPostNews_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnPostID" runat="server" Value='<%# Eval("ID") %>' ClientIDMode="Static" />
                                            <li>
                                                <asp:LinkButton ID="lblHeading" runat="server" Font-Underline="false" CommandName="NewsDetails" CssClass="commentQA moreQA cursor-pointer"
                                                    ClientIDMode="Static" Text='<%#Eval("Title") %>'>
                                                </asp:LinkButton>
                                            </li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </ul>
                                <div class="bn-navi">
                                    <span></span>
                                    <span></span>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <%--<Triggers>
                  <asp:AsyncPostBackTrigger ControlID="lblHeading"/>
                  </Triggers>--%>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="center_panel" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
                    <ContentTemplate>
                        <div class="center-panel">
                            <asp:UpdatePanel ID="uppostupdate" runat="server">
                                <ContentTemplate>
                                    <%--<marquee>
                              <asp:LinkButton id="lblHeading" runat="server" Font-Underline="false" OnCommand="NewsDetails_Command" CssClass="commentQA moreQA cursor-pointer" 
                               clientidmode="Static">
                              </asp:LinkButton>
                              </marquee>--%>
                                    <!---Notication and job panel-->
                                    <div class="jobs-notification">
                                        <div class="card">
                                            <!--Notification starts-->
                                            <div id="dvNotification"
                                                runat="server" clientidmode="Static" class="notification-home">
                                                <h4>
                                                    <asp:Label ID="notificationlabel" Text="" runat="server"></asp:Label>
                                                </h4>
                                                <asp:UpdatePanel ClientIDMode="Static" ID="upsss" runat="server">
                                                    <ContentTemplate>
                                                        <div class="notification-scroll">
                                                            <asp:ListView ID="lstNotification" OnItemDataBound="lstNotification_ItemDataBound"
                                                                OnItemCommand="lstNotification_ItemCommand" runat="server">
                                                                <%--OnItemDeleting="lstNotification_ItemDeleting"--%>
                                                                <LayoutTemplate>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr id="itemPlaceHolder" runat="server"></tr>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnPkId" Value='<%# Eval("Id") %>' runat="server" />
                                                                    <asp:HiddenField ID="hdnRegID" Value='<%# Eval("intRegistrationId") %>' runat="server" />
                                                                    <asp:HiddenField ID="hdnintUserTypeId" Value='<%#Eval("intUserTypeId")%>' runat="server" />
                                                                    <asp:HiddenField ID="hdnrequserid" Value='<%# Eval("intInvitedUserId") %>' runat="server" />
                                                                    <asp:HiddenField ID="hdnTableName" Value='<%# Eval("strTableName") %>' runat="server" />
                                                                    <asp:HiddenField ID="hdnShareInvitee" Value='<%# Eval("strInvitee") %>' runat="server" />
                                                                    <asp:HiddenField ID="hdnStrRecommendation" Value='<%# Eval("StrRecommendation") %>' runat="server" />
                                                                    <asp:HiddenField ID="hdnIsAccept" Value='<%# Eval("IsAccept") %>' runat="server" />
                                                                    <asp:HiddenField ID="hdnintIsAccept" Value='<%# Eval("intIsAccept") %>' runat="server" />
                                                                    <asp:HiddenField ID="hdnNotificationCount" Value="0" runat="server" />
                                                                    <!--result starts-->
                                                                    <div class="newnotification" id="SearchRept" runat="server">
                                                                        <div class="media">
                                                                            <div class="media-left mr-2">
                                                                                <img id="imgprofile" runat="server" class="img_progile_edit" src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' />
                                                                            </div>
                                                                            <div class="media-body">
                                                                                <h6 class="mb-0 mt-0">
                                                                                    <asp:LinkButton PostBackUrl="Notifications_Details_2.aspx" CommandName="Details" runat="server" ID="hyp" class="font-size-14px" ToolTip="View Notification"
                                                                                        Text='<%#Eval("Name") %>'>                                                                                                                                   
                                                                                    </asp:LinkButton>
                                                                                </h6>
                                                                                <asp:UpdatePanel ID="upmains" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <p class="mb-0">
                                                                                            <asp:Label ID="lbDesignation" Text='<%#Eval("Designation") %>' runat="server"></asp:Label>
                                                                                        </p>
                                                                                        <p class="mb-0">
                                                                                            <asp:Label ID="lblnotificationname" runat="server"></asp:Label>
                                                                                        </p>
                                                                                        <div class="d-flex pt-1">
                                                                                            <asp:LinkButton ID="lnkShareDetail" CommandName="ShareDetails" class="hover-a-tag command_name" runat="server"></asp:LinkButton>
                                                                                            <asp:LinkButton ID="lnkQAshare" CommandName="QAShareDetails" class="hover-a-tag command_name" runat="server"></asp:LinkButton>
                                                                                            <asp:LinkButton ID="lnkBlogshare" CommandName="BlogShareDetails" class="hover-a-tag command_name" runat="server"></asp:LinkButton>
                                                                                            <asp:Label ID="lblComment" Text="" Visible="false" runat="server"></asp:Label>
                                                                                            <asp:Label ID="lblSANotification" Text="" Visible="false" runat="server"></asp:Label>
                                                                                            <asp:LinkButton ID="lnkCancel" Visible="false" CssClass="message btn btn-outline-primary mr-2" Text="Decline" CommandName="RemoveNotification" OnClientClick="showLoader1();" CausesValidation="false" Style=""
                                                                                                runat="server"></asp:LinkButton>
                                                                                            <asp:LinkButton ID="lnkConfirm" Visible="false" CssClass="message lnkConfirm btn btn-primary" Text="Accept" CommandName="Confirm" OnClientClick="showLoader1();" CausesValidation="false" ClientIDMode="Static"
                                                                                                runat="server"></asp:LinkButton>
                                                                                            <asp:LinkButton ID="lnkConnected" runat="server" Visible="false" CausesValidation="false" class="causes_validation un-anchor">
                                                                        <span class="add-icon"><img src="images/connected.svg" style="height:15px;"></span>
                                                                        <strong>Accepted</strong>
                                                                                            </asp:LinkButton>
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                    <Triggers>
                                                                                        <asp:AsyncPostBackTrigger ControlID="lnkConfirm" />
                                                                                        <asp:AsyncPostBackTrigger ControlID="lnkCancel" />
                                                                                    </Triggers>
                                                                                </asp:UpdatePanel>
                                                                                <div style="display: none;">
                                                                                    <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("vchrUserName") %>'></asp:Label>
                                                                                    <asp:Label ID="lblUserType" runat="server" Text='<%#Eval("intUserTypeID") %>'></asp:Label>
                                                                                    <asp:Label ID="lblGroupName" runat="server" Text='<%#Eval("strGroupName") %>' Font-Italic="true"></asp:Label>
                                                                                    <asp:Label ID="lblQAPath" runat="server" Text='<%#Eval("StrRecommendation") %>' Font-Italic="true"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <!--result ends-->
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </div>
                                                        <p class="noti-empty-m" id="notificationEmptyMsg" style="display: none" clientidmode="Static" runat="server">No Notification to show.</p>

                                                        <br />
                                                        <div id="dvAllNotifi" runat="server" class="dvAllNotifi" style="display: none" clientidmode="Static">
                                                            <a href="Notifications_Details_2.aspx" id="aViewNotifications" runat="server" class="aViewNotifications">View All</a>
                                                        </div>

                                                        <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <!--Notification ends-->
                                        </div>
                                        <div class="card">
                                            <asp:UpdatePanel ID="pnlParent" class="jobs-panel" runat="server">
                                                <ContentTemplate>
                                                    <%--<asp:HiddenField ID="hdnintJobsDelete" runat="server" ClientIDMode="Static" Value='' />--%>
                                                    <div>
                                                        <h4 class="mb-0">Jobs</h4>
                                                    </div>
                                                    <asp:UpdatePanel ID="pnlJobsListing" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ListView ID="lstJobsListing" runat="server" OnItemDataBound="lstJobs_ItemDataBound" OnItemCommand="lstJobs_ItemCommand">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnPostUpdateId" runat="server" ClientIDMode="Static" Value='<%#Eval("ID")%>' />
                                                                    <div class="card-list-con jobs-con">
                                                                        <div class="post-con">
                                                                            <div class="post-header">

                                                                                <!--   <span class="float-right small-date">
                                                        <asp:Label ID="dtAddedOn" runat="server" Text='<%#Eval("Created_timestamp") %>' />
                                                    </span> -->
                                                                                <h6 class="d-flex align-items-md-center font-size-14px align-items-lg-center mb-0 align-items-end">
                                                                                    <asp:LinkButton ID="lblTitle" runat="server" Font-Underline="false" CommandName="JobsDetail" CssClass="commentQA title-truncate"
                                                                                        Text='<%#Eval("Title") %>'></asp:LinkButton>
                                                                                    <!--       <span id="span_status" runat="server" class="badge ml-2 badge-success" visible="false">
                                                            <%--<asp:Label ID="lblCounter" runat="server" Text="32" />--%>
                                                            <asp:Label ID="lblStatus" runat="server" Text="Applied" />
                                                        </span> -->
                                                                                </h6>

                                                                                <%--<asp:LinkButton ID="lnkJobsEdit" runat="server" CommandName="Edit Jobs" Font-Underline="false" CssClass="dropdown-item">
           <span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>--%>
                                                                                <ul class="small-date">
                                                                                    <li id="li_salary" runat="server" visible="false">
                                                                                        <strong>
                                                                                            <asp:Label ID="lblStartingSalary" runat="server" Text='<%#Eval("StartingSalary") %>' />
                                                                                            -
                                                                <asp:Label ID="lblEndingSalary" runat="server" Text='<%#Eval("EndingSalary") %>' />
                                                                                        </strong>
                                                                                    </li>
                                                                                    <li id="li_location" runat="server" visible="false">
                                                                                        <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location") %>' />
                                                                                    </li>
                                                                                    <li id="li_jobtype" runat="server" visible="false">
                                                                                        <asp:Label ID="lblJobType" runat="server" Text='<%#Eval("JobType") %>' />
                                                                                    </li>
                                                                                    <li id="li_duration" runat="server" visible="false">
                                                                                        <asp:Label ID="lblStartDuration" runat="server" Text='<%#Eval("StartDuration") %>' />
                                                                                        to 
            <asp:Label ID="lblEndDuration" runat="server" Text='<%#Eval("EndDuration") %>' />
                                                                                    </li>
                                                                                </ul>
                                                                            </div>

                                                                            <!--   <div class="post-body">
                                                    <p class="mb-0">
                                                        <asp:Label ID="lblDescription" runat="server" class="moreQA" Text='<%#Eval("Description") %>' />
                                                    </p>
                                                </div> -->
                                                                        </div>
                                                                        <!-- post-con ended -->
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <p class="job-empty-m" id="emptyJobMessage" style="display: none" clientidmode="Static" runat="server">No Jobs to apply.</p>

                                            <br />
                                            <span class="view-more-jobs" runat="server" id="spanJobViewAll" clientidmode="Static" style="display: none">
                                                <a href="AllJobsListing.aspx">View All</a>
                                            </span>

                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdnCommentImageSrcId" runat="server" ClientIDMode="Static" Value='' />
                                    <%--<div class="upper_div">--%>
                                    <div class="card">
                                        <div class="share-group">
                                            <form action="" method="">
                                                <%--<div class="rightSection">
                                       <div class="upper_div">--%>
                                                <textarea id="txtPostUpdate" runat="server" class="form-control" placeholder="Share an update..." clientidmode="Static" maxlength="5000" autocomplete="off"></textarea>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPostUpdate"
                                                    Display="Dynamic" ValidationGroup="posts" ErrorMessage="Please write a post." ForeColor="Red" ClientIDMode="Static">
                                                </asp:RequiredFieldValidator>
                                                <%----%>
                                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                       runat="server" Display="dynamic" 
                                       ControlToValidate="txtPostUpdate" 
                                       ValidationExpression="^([\S\s]{0,5000})$" 
                                       ErrorMessage="Only 5000 characters are allowed.">
                                       </asp:RegularExpressionValidator>--%>
                                                <asp:Label ID="lblPostMsg" Text="Please write a post." Style="display: none; color: Red;" runat="server" ClientIDMode="Static"></asp:Label>
                                                <asp:HiddenField ID="hdnintStatusUpdateId" runat="server" ClientIDMode="Static" Value="" />
                                                <asp:HiddenField ID="hdneditdeletepost" runat="server" ClientIDMode="Static" Value="" />
                                                <asp:Label ID="lblintCommentId" Style="display: none;" Text="" runat="server" ClientIDMode="Static"></asp:Label>
                                                <div id="postUpdateWrapper" class="submit-con">
                                                    <div class="upload-con">
                                                        <span class="btn btn-outline-primary flex-align-center">
                                                            <span class="camera-icon">
                                                                <span class="icon-camera"></span>
                                                            </span>
                                                            <span class="upload-text">Upload Media</span>
                                                            <asp:FileUpload ID="FileUplogo" ClientIDMode="Static" runat="server" class="form-control" name="file_1" Style="display: block;" />
                                                        </span>
                                                    </div>
                                                    <a id="lnkPostUpdate" runat="server" class="btn btn-primary post-home float-right" clientidmode="Static" href="#" onclick="return false;">Post</a>
                                                    <asp:Button ID="lnkDummyPost" class="btn btn-primary float-right " runat="server" OnClick="lnkPostUpdate_Click" ClientIDMode="Static" ValidationGroup="posts" CausesValidation="true" Style="display: none;" Text=""></asp:Button>
                                                </div>
                                                <span class="grey-text mt-1 font-size-12 float-right d-none">Note: Image (max 3 MB) or Video (max 12 MB).</span>
                                                <!--  <asp:Label ID="lblfilename" ClientIDMode="Static" class="display-inline w-100 text-right d-none" Text="." runat="server" ForeColor="White"></asp:Label> -->
                                                <div id="divPostimage" class="" style="display: none;">
                                                    <div class="loader-cover">
                                                        <div class="lds-ellipsis">
                                                            <div></div>
                                                            <div></div>
                                                            <div></div>
                                                            <div></div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--<div id="divPostimage" class="divPostimage">
                                       <img id="imgPostSave" src="images/Loadgif.gif" />
                                       </div>--%>
                                                <%--<div>
                                       </div>--%>
                                                <div class="home_page_image m-t-10">
                                                    <img id="imgselect" src="" class="img-fluid" style="display: none;" />
                                                </div>
                                                <video id="Mediavideo" src="" controls="controls" class="img-fluid" autostart="false" style="display: none;" quality="high"></video>
                                                <asp:Label ID="lblVideomsg" runat="server" ClientIDMode="Static" Text="Missing Plug-in" Style="display: none; margin-left: 30px;">
                                                </asp:Label>
                                                <%--<div class="rightSection">--%>
                                                <%--<div class="upper_div">--%>
                                                <%--<div class="tabContainer">--%>
                                            </form>
                                        </div>
                                    </div>
                                    <!-- card ended -->
                                    <%--<div class="new_style position_relative">--%>
                                    <!-- Post Start -->
                                    <asp:HiddenField ID="hdnintPostId" Value="" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hdnstrPostDescriptiondele" Value="" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hdnintPostceIdelet" Value="" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hdnstrPostDescriptioncedel" Value="" ClientIDMode="Static" runat="server" />
                                    <asp:ListView ID="lstPostUpdates" runat="server" OnItemCommand="lstPostUpdates_ItemCommand"
                                        OnItemDataBound="lstPostUpdates_ItemDataBound" OnItemCreated="lstPostUpdates_ItemCreated">
                                        <ItemTemplate>
                                            <div class="card post-con">
                                                <div class="post-header">
                                                    <asp:HiddenField ID="hdnVideoName" Value='<%# Eval("strVideoPath") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnPhotoPath" Value='<%# Eval("strPhotoPath") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnPostLink" Value='<%# Eval("strPostLink") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnintUserTypeId" runat="server" Value='<%#Eval("intUserTypeId") %>' />
                                                    <asp:HiddenField ID="hdnIframe" Value='<%# "VideoFiles/"+Eval("strVideoPath")%>' ClientIDMode="Static" runat="server" />
                                                    <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intAddedBy") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnShared" Value='<%# Eval("intShared") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnSharedId" Value='<%# Eval("intSharedId") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnSharedUserTypeId" Value='<%# Eval("intSharedUserTypeId") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnstrPostDescription" runat="server" Value='<%# Eval("strPostDescription") %>' />
                                                    <asp:UpdatePanel ID="UP22" runat="server">
                                                        <ContentTemplate>
                                                            <span class="avatar-img">
                                                                <img src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' id="imgprofile" runat="server"
                                                                    alt="" class="rounded-circle" />
                                                            </span>
                                                            <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                            <span class="user-name-date">
                                                                <span class="user-name">
                                                                    <asp:LinkButton class="linkbutton_home_page" ID="Label1" runat="server" Font-Underline="false" CommandName="Details"
                                                                        Text='<%#Eval("UserName") %>'>
                                                                    </asp:LinkButton>
                                                                </span>
                                                                <span class="date">
                                                                    <asp:Label ID="lblAddedOn" Text='<%# Eval("dtAddedOn") %>' runat="server"></asp:Label>
                                                                </span>
                                                            </span>
                                                            <%--<div class="cls">
                                                </div>--%>
                                                            <!--edit starts-->
                                                            <span class="more-btn float-right">
                                                                <%--<span class="dropdown">--%>
                                                                <div class="linkbutton_home_page" id="editUser" runat="server">
                                                                    <span class="dropdownlblMessCount">
                                                                        <%--<div id="smoothmenu1" class="ddsmoothmenu">
                                                         <ul class="iconUl">
                                                             <li><a href="#" onclick="return false">
                                                                 <img src="images/edit.png" /></a>
                                                                 <ul>
                                                                     <li style="border-bottom: 1px solid #cdced0;">--%>
                                                                        <asp:PlaceHolder ID="plcDeletePost" runat="server">
                                                                            <a href="#" id="dropdownMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                <img src="images/more.svg" alt="" class="more-btn" />
                                                                            </a>
                                                                            <span id="spdeletepost" onclick="setPostId.bind(this)()" runat="server" class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                                                                <asp:HiddenField ID="hdnintPostIdelet" Value='<%# Eval("Id") %>' ClientIDMode="Static" runat="server" />
                                                                                <asp:HiddenField ID="hdnstrPostDescriptiondel" Value='<%# Eval("strPostDescription") %>' ClientIDMode="Static" runat="server" />
                                                                                <asp:LinkButton ID="lnkEditPost" runat="server" Font-Underline="false" Visible="true" ToolTip="Edit"
                                                                                    Text="" class="dropdown-item" CommandName="Edit Post" CausesValidation="false">  
                                                               <span class="lnr lnr-pencil"></span> Edit
                                                                                </asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkDeletePost" runat="server" Font-Underline="false" Visible="true" ClientIDMode="Static"
                                                                                    ToolTip="Delete Post" class="dropdown-item" Text="" CommandName="Delete Post" CausesValidation="false"
                                                                                    OnClientClick="javascript:Commentsdelete();return false;">
                                                               <span class="lnr lnr-trash"></span> Delete
                                                                                </asp:LinkButton>
                                                                            </span>
                                                                        </asp:PlaceHolder>
                                                                        <%--<asp:LinkButton ID="lnkEditPost" Font-Underline="false" Visible="true" ToolTip="Edit"
                                                         Text="" class="new_edit home_edit" CommandName="Edit Post" CausesValidation="false" runat="server">                                                                                   
                                                         </asp:LinkButton>--%>
                                                                        <%-- </li>
                                                         <li>--%>
                                                                        <%--<div class="cls"></div>             
                                                         <span class="ediDel">--%>
                                                                    </span>
                                                                </div>
                                                            </span>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnkDeletePost" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="post-body">
                                                    <%--<img src="images/133313561.jpg" alt="" class="img-fluid" />--%>
                                                    <asp:Label ID="lblPostDescription" runat="server"></asp:Label>
                                                    <div class="home_page_image m-t-10">
                                                        <img src='<%# "UploadedPhoto/"+Eval("strPhotoPath")%>' id="imgPhoto" class="img-fluid" runat="server" />
                                                    </div>
                                                    <asp:HyperLink ID="hplLinkUrl" ClientIDMode="Static" Target="_blank" runat="server" Text='<%#"http://"+Eval("strPostLink") %>'
                                                        NavigateUrl='<%#"http://"+Eval("strPostLink") %>'>
                                                    </asp:HyperLink>
                                                    <div id="dvVideo" runat="server" clientidmode="Static">
                                                        <embed id="frm1" src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>'
                                                            clientidmode="Static" starttime="00:00" controls="true" autoplay="false" autostart="false"
                                                            quality="high" cache="true" correction="full" pluginurl="http://quicktime.en.softonic.com/download"
                                                            pluginspage="http://quicktime.en.softonic.com/download" width="100%" height="100%;"
                                                            scale="aspect" pluginspage="http://quicktime.en.softonic.com/download" />
                                                        <a id="lbtnControlPanel" title="Play Video" runat="server" clientidmode="Static" onclick="CPhit(this);"></a>
                                                    </div>
                                                    <div id="divChrome" runat="server" clientidmode="Static">
                                                        <div id='media-player'>
                                                            <video id='media-video' controls class="img-fluid">
                                                                <source src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' type='video/mp4'>
                                                                <source src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' type='video/mp3'>
                                                                <source src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' type='video/webm'>
                                                                <source src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' type='video/x-msvideo'>
                                                                <object type="application/x-shockwave-flash">
                                                                    <param name="movie" value='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' />
                                                                    <param name="flashvars" value="controls=true&file=<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>" />
                                                                </object>
                                                            </video>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="post-footer">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnkLike" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <div id="smoothmenu1" class="ddsmoothmenu">
                                                                <ul class="list-inline">
                                                                    <li class="list-inline-item">
                                                                        <span class="d-flex align-items-center links-like-btn">
                                                                            <asp:LinkButton ID="lnkLike" class="active-toogle" runat="server" Font-Underline="false" CommandName="Like Post">
                                                            <span class="like-btn"></span>               
                                                                            </asp:LinkButton>
                                                                            <span class="d-flex">
                                                                                <asp:Label ID="lnkLikePost" runat="server" Text='<%#Eval("Likes") %>' ToolTip="View Likes"
                                                                                    ClientIDMode="Static"></asp:Label>
                                                                                &nbsp;
                                                            <asp:Label runat="server" ClientIDMode="Static" ID="lblLikePostText" Text='<%# ((int)Eval("Likes") == 1) ? "Like" : "Likes" %>'></asp:Label>
                                                                            </span>
                                                                        </span>
                                                                        <%--<asp:Label ID="lbllike" runat="server" ClientIDMode="Static" />--%>
                                                                    </li>
                                                                    <li class="list-inline-item">
                                                                        <asp:LinkButton ID="lnkCommentsCount" class="active-toogle un-anchor" runat="server" Font-Underline="false">
                                                                            <%--CommandName="Like Post"--%>
                                                                            <span class="comment-btn"></span>
                                                                            <asp:Label ID="lblCommentsCount" runat="server" ClientIDMode="Static" Text='<%# Eval("Comments") %>'></asp:Label>
                                                                            &nbsp;
                                                         <asp:Label ID="lblCommentsCountText" runat="server" ClientIDMode="Static" Text='<%# ((int)Eval("Comments") == 1) ? "Comment" : "Comments" %>'></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </li>
                                                                    <%--<li class="list-inline-item">
                                                      <a href="#" class="active-toogle">
                                                      <span class="comment-btn"></span> 
                                                       43 Comments
                                                      </a>
                                                      </li>--%>
                                                                </ul>
                                                            </div>
                                                            <asp:HiddenField ID="hdnPostUpdateId" Value='<%# Eval("Id") %>' ClientIDMode="Static" runat="server" />
                                                            <asp:HiddenField ID="hdnPostLikeUserId" Value='<%# Eval("PostLikeUserId") %>' ClientIDMode="Static" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <!--comments starts-->
                                                    <div class="comment-con">
                                                        <%--<p class="grayComments">
                                             <!--Comments-->
                                             </p>--%>
                                                        <asp:ListView ID="lstChild" runat="server" DataKeyNames="intID" ClientIDMode="Static">
                                                            <%--OnItemCommand="lstChild_ItemCommand"--%>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnintUserTypeId" runat="server" Value='<%#Eval("intUserTypeId") %>' />
                                                                <asp:HiddenField ID="hdnRegistrationId" runat="server" Value='<%#Eval("intRegistrationId") %>' />
                                                                <asp:HiddenField ID="intUserId" runat="server" Value='<%#Eval("intUserId") %>' />
                                                                <div class="tabCmtBox position_relative">
                                                                    <%--<div class="tabImg">--%>
                                                                    <div class="post-header">
                                                                        <span class="avatar-img">
                                                                            <img src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' id="imgCommentpic" runat="server" class="rounded-circle" />
                                                                            <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                                        </span>
                                                                        <span class="user-name-date">
                                                                            <asp:LinkButton class="user-name post_comment_d" ID="Label1" Font-Underline="false" CommandName="Post Comment Details" runat="server"
                                                                                Text='<%#Eval("UserName") %>'>
                                                                            </asp:LinkButton>
                                                                            <asp:Label ID="lblPostedOn" class="date" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                                                        </span>
                                                                        <asp:UpdatePanel ID="upchild" Visible="true" runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="editIcon edit_new_home_C" id="Div1" runat="server">
                                                                                    <%--style="display: none;"--%>
                                                                                    <span class="more-btn float-right">
                                                                                        <span class="dropdown">
                                                                                            <a href="#" id="dropdownMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                                <img src="images/more.svg" alt="" class="more-btn" />
                                                                                            </a>
                                                                                            <%--<span class="dropdown-menu" aria-labelledby="dropdownMenuLink">--%>
                                                                                            <span id="spdeletepost1" onclick="setPostCommentId.bind(this)()" runat="server" class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                                                                                <asp:HiddenField ID="hdnintPostcIdelet" Value='<%# Eval("intID") %>' ClientIDMode="Static" runat="server" />
                                                                                                <asp:HiddenField ID="hdnstrPostDescriptioncdel" Value='<%# Eval("strComment") %>' ClientIDMode="Static" runat="server" />
                                                                                                <asp:LinkButton ID="lnkEditComment" Font-Underline="false" Visible="true" ToolTip="Edit" runat="server"
                                                                                                    Text="" class="dropdown-item" CommandName="Edit Comment" CausesValidation="false">
                                                                           <span class="lnr lnr-pencil"></span> Edit
                                                                                                </asp:LinkButton>
                                                                                                <asp:LinkButton ID="lnkDeleteComment" Visible="true" OnClientClick="javascript:Commentsdelete1();return false;"
                                                                                                    ToolTip="Delete Comments" class="dropdown-item" Text="" CommandName="Delete Comment" CausesValidation="false"
                                                                                                    runat="server"><span class="lnr lnr-trash"></span> Delete
                                                                                                </asp:LinkButton>
                                                                                            </span>
                                                                                            <%--OnClientClick="javascript:docdelete();return false;"--%>
                                                                                        </span>
                                                                                    </span>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="lnkEditComment" />
                                                                                <asp:AsyncPostBackTrigger ControlID="lnkDeleteComment" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                        <%--</div>--%>
                                                                    </div>
                                                                    <%--<div class="tabTxtInfo">--%>
                                                                    <div class="post-body">
                                                                        <asp:Label ID="lblstr" runat="server" Text='<%#Eval("strComment") %>'></asp:Label>
                                                                        <asp:UpdatePanel ID="upda" class="wall-post" runat="server">
                                                                            <ContentTemplate>
                                                                                <ul class="list-inline mt-2">
                                                                                    <li class="list-inline-item">
                                                                                        <span class="d-flex align-items-center links-like-btn">
                                                                                            <asp:LinkButton ID="lnkLikes" runat="server" class="active-toogle" Font-Underline="false" CommandName="Like Comment" ToolTip="Like Comments">
                                                                        <span class="like-btn"></span> 
                                                                                            </asp:LinkButton>
                                                                                            <span class="d-flex">
                                                                                                <asp:Label ID="lnkLikeComment" runat="server" Text='<%#Eval("Likes") %>' ClientIDMode="Static" ToolTip="View Likes">
                                                                                                </asp:Label>
                                                                                                &nbsp; 
                                                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblLikeCommentText" Text='<%# ((int)Eval("Likes") == 1) ? "Like" : "Likes" %>'></asp:Label>
                                                                                            </span>
                                                                                        </span>
                                                                                    </li>
                                                                                </ul>
                                                                                <asp:HiddenField ID="hdnCommentId" runat="server" ClientIDMode="Static" Value='<%#Eval("intID") %>' />
                                                                                <asp:HiddenField ID="hdnCommentLikeUserId" Value='<%# Eval("CommentLikeUserId") %>' ClientIDMode="Static" runat="server" />
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="lnkLikes" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                    <!-- post body ended -->
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                        <asp:Panel ID="pnlEnterComment" runat="server" ClientIDMode="Predictable" DefaultButton="lnkEnterComment" class="position-relative m-r-padding">
                                                            <%--<span class="spCommentsss">--%>
                                                            <asp:HiddenField ID="hdnPostIDs" Value='<%# Eval("Id") %>' ClientIDMode="Static" runat="server" />
                                                            <div class="post-footer">
                                                                <span class="avatar-img mr-3">
                                                                    <img id="imgComment" runat="server" alt="" class="rounded-circle" />
                                                                </span>
                                                                <asp:TextBox TextMode="multiline" ID="txtComment" runat="server" MaxLength="2000" Class="form-control postcommentduplicate height-moz"
                                                                    placeholder="Type your comment" onkeydown="return postCommentNew.bind(this)(event)" oninput="postCommentInput.bind(this)(event)"
                                                                    autocomplete="off" Rows="1"></asp:TextBox>
                                                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                                   runat="server" Display="dynamic" 
                                                   ControlToValidate="txtComment" 
                                                   ValidationExpression="^([\S\s]{0,2000})$" 
                                                   ErrorMessage="Only 2000 characters are allowed.">
                                                   </asp:RegularExpressionValidator>--%>
                                                            </div>
                                                            <%--</span>--%>
                                                            <%--<ajax:TextBoxWatermarkExtender TargetControlID="txtComment" ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="Add your comment...">
                                                </ajax:TextBoxWatermarkExtender>--%>
                                                            <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment" ForeColor="Red"
                                                                Display="Dynamic" ValidationGroup="comment" class="margin-left-group-w" ErrorMessage="Please write a comment">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:Label Style="display: none;" ID="lblCommentError" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="lnkEnterComment" CommandArgument='<%# Eval("Id") %>'
                                                                CssClass="lnkEnterCommentcss display-none" CommandName="EnterComment" runat="server" Text="Enter">
                                                            </asp:LinkButton>
                                                            <%-- <asp:Button ID="lnkEnterComment" Style="display: none" CommandArgument='<%# Eval("Id") %>' ClientIDMode="Static"
                                                CommandName="EnterComment" runat="server" Text="Enter" />--%>
                                                        </asp:Panel>
                                                        <%--</ContentTemplate>
                                             </asp:UpdatePanel>--%>
                                                        <!--comments ends-->
                                                    </div>
                                                </div>
                                            </div>
                                            <%--div card post-con--%>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <%--div post footer--%>
                                    <%--<p class="noMar nomar_lable">
                              <asp:Label ID="lblPostDescription" runat="server"></asp:Label>
                              </p>--%>
                                    <%--<div class="">
                              <p>
                              <asp:HyperLink ID="hplLinkUrl" ClientIDMode="Static" Target="_blank" ToolTip="Posted Link"
                                 NavigateUrl='<%#"http://"+Eval("strPostLink") %>' Text='<%#"http://"+Eval("strPostLink") %>'
                                 Font-Size="Small" runat="server"></asp:HyperLink>
                              <div class="home_page_image"> <img src='<%# "UploadedPhoto/"+Eval("strPhotoPath")%>' id="imgPhoto" class="imgPhoto" runat="server" height="150" width="150"
                                 /></div>
                              <div id="dvVideo" runat="server" clientidmode="Static">
                              <embed id="frm1" src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>'
                                 clientidmode="Static" starttime="00:00" controls="true" autoplay="false" autostart="false"
                                 quality="high" cache="true" correction="full" pluginurl="http://quicktime.en.softonic.com/download"
                                 pluginspage="http://quicktime.en.softonic.com/download" width="100%" height="100%;"
                                 scale="aspect" pluginspage="http://quicktime.en.softonic.com/download" />
                              <a id="lbtnControlPanel" title="Play Video"  runat="server"
                                 clientidmode="Static" onclick="CPhit(this);"></a>
                              </div>
                              <div id="divChrome" runat="server" clientidmode="Static">
                              <div id='media-player'>
                              <video id='media-video' controls>
                              <source src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' type='video/mp4'>
                              <source src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' type='video/mp3'>
                              <source src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' type='video/webm'>
                              <source src='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' type='video/x-msvideo'>
                              <object type="application/x-shockwave-flash" >
                              <param name="movie" value='<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>' />
                              <param name="flashvars" value="controls=true&file=<%= ResolveUrl("VideoFiles/") %><%#Eval("strVideoPath")%>" />
                              </object>
                              </video>
                              </div>
                              </div>
                              </p>
                              </div>--%>
                                    <%--<div class="divtabLikes_home divtabLikespost">
                              <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                              <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="lnkLike" />
                              </Triggers>
                              <ContentTemplate>
                              <asp:LinkButton Font-Underline="false" ID="LinkButton3" runat="server" CommandName="Like Post"> 
                              <i class="fa fa-thumbs-o-up link_style" aria-hidden="true"></i></asp:LinkButton>
                              <asp:Label ID="Label3" runat="server" Text='<%#Eval("Likes") %>' ToolTip="View Likes"
                                ClientIDMode="Static" class="linkedit_posT"></asp:Label>
                              <asp:HiddenField ID="HiddenField1" Value='<%# Eval("Id") %>' ClientIDMode="Static"
                                runat="server" />
                              <asp:HiddenField ID="HiddenField2" Value='<%# Eval("PostLikeUserId") %>' ClientIDMode="Static"
                                runat="server" />
                              </ContentTemplate>
                              </asp:UpdatePanel>
                              </div>--%>
                                    <%--</div>--%>
                                    <div id="pLoadMore" runat="server" align="center" clientidmode="Static" style="display: none;">
                                        <!--
                                 <asp:ImageButton ID="imgLoadMore" runat="server" ClientIDMode="Static" ImageUrl="~/images/loadingIcon.gif"
                                  CssClass="imageLoadmoreHome" OnClick="imgLoadMore_OnClick" Height="100px" Width="100px" />
                                 -->
                                        <div id="imgLoadMore2" runat="server" class="lds-ellipsis">
                                            <div></div>
                                            <div></div>
                                            <div></div>
                                            <div></div>
                                        </div>
                                        <asp:Button ID="imgLoadMore1" runat="server" ClientIDMode="Static" Style="display: none;"
                                            CssClass="imageLoadmoreHome" OnClick="imgLoadMore_OnClick" Height="100px" Width="100px" />
                                    </div>
                                    <div style="display: none;">
                                        <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server" ClientIDMode="Static" ForeColor="Red" Visible="false">
                                        </asp:Label>
                                    </div>
                                    <%--</div>--%>
                                    <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                                    <%--<div class="innerContainer display_none">
                              <div class="bgLine" id="pagination">
                               &nbsp;
                              </div>
                              <div class="cls">
                              </div>--%>
                                    <div id="dvPage" runat="server" class="pagination" clientidmode="Static" visible="false">
                                        <asp:LinkButton ID="lnkFirst" runat="server" CssClass="PagingFirst" OnClick="lnkFirst_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">&lt;&lt;</asp:LinkButton>
                                        <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand" OnItemDataBound="rptDvPage_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPageLink" CssClass="Paging" runat="server" ClientIDMode="Static" CommandName="PageLink" Text='<%#Eval("intPageNo") %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">&gt;&gt;</asp:LinkButton>
                                        <asp:LinkButton ID="lnkLast" runat="server" OnClick="lnkLast_Click"><img src="<%=ResolveUrl("images/spacer.gif")%>" class="last" />
                                 <img src="<%=ResolveUrl("images/spacer.gif")%>" class="last" />
                                        </asp:LinkButton>
                                        <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                    </div>
                                    <%--</div>--%>
                                    <!-- Post Ends -->
                                    <%--</div>--%>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lnkDummyPost" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- center-panel ended -->
            <div class="right-panel-back-layer"></div>
            <div class="right-panel">
                <span class="m-view back">Back <span class="lnr lnr-arrow-right"></span></span>
                <div class="aside-bar">
                    <div class="card user-details">
                        <!--top profile starts-->
                        <asp:HiddenField ID="hdnConnCommandName" runat="server" ClientIDMode="Static" Value="" />
                        <asp:HiddenField ID="hdnloginIds" runat="server" ClientIDMode="Static" Value="" />
                        <asp:HiddenField ID="hdnUserID" runat="server" ClientIDMode="Static" Value="" />
                        <%--<div class="display_none">--%>
                        <asp:Button ID="btnAccept" runat="server" ClientIDMode="Static" OnClick="ClickAccept" Visible="false" />
                        <%--</div>--%>
                        <asp:UpdatePanel ID="upProfileMain" runat="server" class="text-center">
                            <ContentTemplate>
                                <%--<div class="tabProfileBox">--%>
                                <img id="imgUser" runat="server" src="profile-photo.png" alt="" class="user-img" clientidmode="Static" />
                                <span class="user-name">
                                    <asp:Label ID="lblUserProfName" runat="server" Text=""></asp:Label>
                                </span>
                                <span class="view-all font-size-12 pt-1 m-b-10" runat="server" id="spanlnkAddFriend">
                                    <asp:LinkButton ID="lnkAddFriend" runat="server" role="button" class="addFriend add-frnd-btn" ClientIDMode="Static" OnClick="lnkAddFriend_Click" Text="Add Friend" OnClientClick="javascript:showLoader1();"></asp:LinkButton>
                                    <%--OnClientClick="javascript:ShowAddUserProcess();"--%>
                                </span>
                                <div id="profileUserC" runat="server">
                                    <span class="view-all font-size-12 pt-1 m-b-10"><a href="Profile2.aspx?RegId=<%=hdnRegId.Value %>">View Profile</a></span>
                                </div>
                                <asp:LinkButton ID="btnSndMsg" runat="server" CssClass="btn btn-outline-primary font-size-12 mb-3" OnClientClick="javascript:MsgClick(); return false;"
                                    CausesValidation="false" Text="Message" Visible="false"></asp:LinkButton>
                                <ul class="list-inline">
                                    <li class="list-inline-item d-none">
                                        <span class="endor-cover">
                                            <span class="icon">
                                                <img src="images/thumb-color.svg" alt=""></span>
                                            <span class="endors-text un-anchor">
                                                <strong>
                                                    <asp:LinkButton ID="lblEndorseCount" runat="server" class="text_decoration_color un-anchor " OnClick="lblEndorseCount_click" OnClientClick="">
                                                    </asp:LinkButton>
                                                </strong>
                                                Endorsements
                                            </span>
                                        </span>
                                    </li>
                                    <li class="list-inline-item w-100" id="liscore" runat="server">
                                        <span class="endor-cover">
                                            <span class="icon">
                                                <img src="images/shape-2.svg"></span>
                                            <span class="endors-text">
                                                <h1>
                                                    <asp:LinkButton ID="lblMessCount" runat="server" class="text_decoration_color un-anchor" PostBackUrl='Profile2.aspx'></asp:LinkButton>
                                                    <%--<asp:Label ID="lblMessCount" runat="server" Text=""></asp:Label>--%>
                                                </h1>
                                                Score
                                            </span>
                                        </span>
                                    </li>
                                </ul>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkAddFriend" />
                                <%--<asp:AsyncPostBackTrigger ControlID="btnSndMsg" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                        <%--<asp:UpdatePanel ID="upProfileUserC" runat="server" Visible="false">
                     <ContentTemplate>
                      
                       <img id="img1" runat="server" src="avatar.jpg" alt="" class="user-img" clientidmode="Static" />
                        <span class="user-name"><asp:Label ID="Label3" runat="server" Text="">User C</asp:Label></span>
                        <span class="view-all font-size-12 m-b-10"><a href="Home.aspx">View Profile</a></span>
                        <div class="padding-15">
                        <asp:LinkButton ID="lnkAddFriend" runat="server" ClientIDMode="Static" class="addFriend addfriend_inline"
                         Text="Add Friend" OnClientClick="javascript:ShowAddUserProcess();" OnClick="lnkAddFriend_Click">
                        </asp:LinkButton>
                     
                        <asp:Button ID="lnkConnectedUser" runat="server" class="ConnectedUser" Text="Connected" OnClientClick="return false;">
                        </asp:Button>
                        <div class="addbuttonblock">
                         <a id="aAddConnection" runat="server" href="~/SearchPeople.aspx" class="btn btn-primary" clientidmode="Static" onclick="CallAddFriends();"> Add Friend</a>
                        </div>
                      </div>
                     </ContentTemplate>
                     <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="lnkAddFriend" />
                     </Triggers>
                     </asp:UpdatePanel>--%>
                        <!--top profile ends-->
                        <%--<img src="images/avatar-main.jpg" alt="" class="user-img" /><span class="user-name">Rajat Jain</span>--%>
                        <%--<ul class="list-inline">--%>
                        <%--<li class="list-inline-item"> 
                     <span class="endor-cover">
                     <span class="icon"><img src="images/thumb-color.svg" alt="" ></span>
                     <span class="endors-text">
                      <strong>100</strong>
                       Endorsements
                     </span>
                     </span>
                     </li>--%>
                        <%--<li class="list-inline-item">
                     <span class="endor-cover">
                     <span class="icon"><img src="images/shape-2.svg"></span>
                     <span class="endors-text">
                      <strong>100</strong>
                       Score
                     </span>
                     </span>
                     </li>--%>
                        <%--</ul>--%>
                    </div>
                    <!-- card ended-->
                    <div id="divDisplayFrnd" runat="server" class="display_block">
                        <div class="card">
                            <div class="add-friend-con">
                                <asp:UpdatePanel ID="PnlHome" runat="server" Visible="false" UpdateMode="Conditional" ClientIDMode="Static">
                                    <ContentTemplate>
                                        <!--left section starts-->
                                        <!-- Nav tabs -->
                                        <ul class="custom-nav-control nav nav-tabs">
                                            <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#home">Friends</a></li>
                                            <li class="nav-item"><a class="nav-link " data-toggle="tab" href="#menu1" id="GroupSection" runat="server" clientidmode="Static">Groups</a></li>
                                        </ul>
                                        <%--<div id="divAddUser"  class="divAddUser" >
                                 <img src="images/Loadgif.gif" />
                                 </div>--%>
                                        <!-- Tab panes -->
                                        <div class="tab-content">
                                            <div class="custom-nav-container tab-pane container active" id="home">
                                                <div class="padding-15">
                                                    <%--<asp:LinkButton ID="lnkAddFriend" runat="server" ClientIDMode="Static" class="addFriend addfriend_inline" Visible="false"
                                          Text="Add Friend" OnClientClick="javascript:ShowAddUserProcess();" OnClick="lnkAddFriend_Click">
                                          </asp:LinkButton>--%>
                                                    <a id="lnkConnectedUser" runat="server" class="ConnectedUser un-anchor text-right" text="Connected" onclientclick="return false;" style="display: none;">
                                                        <span class="add-icon">
                                                            <img src="images/connected.svg" style="height: 15px;"></span>
                                                        <span class="text-with-icon"><strong>Connected</strong></span>
                                                    </a>
                                                    <%--<asp:label ID="lnkConnectedUser" runat="server" class="ConnectedUser" Text="Connected" OnClientClick="return false;" Style="display: none;">
                                          </asp:label>--%>
                                                    <%--<div class="addbuttonblock">--%>
                                                    <a id="aAddConnection" runat="server" href="~/SearchPeople.aspx" class="" clientidmode="Static" onclick="CallAddFriends();">
                                                        <span class="add-icon">
                                                            <img src="images/add-user.svg"></span>
                                                        <span class="text-with-icon">Add Friend</span>
                                                    </a>
                                                    <%--onclick="CallAddFriends();"--%>
                                                </div>
                                                <!--Friend section starts-->
                                                <%--<div class="groupSection add_frnd" id="divFriendSection">--%>
                                                <%--</div>--%>
                                                <%--<asp:HiddenField ID="hdnUserIDlstFrnds" runat="server" ClientIDMode="Static" Value='' />--%>
                                                <asp:ListView ID="lstFrnds" runat="server" OnItemDataBound="lstFrnds_ItemDataBound" OnItemCommand="lstFrnds_ItemCommand" CellPadding="9">
                                                    <ItemTemplate>
                                                        <ul class="m-truncate">
                                                            <li>
                                                                <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                                                <a class="view_frnd_d d-flex align-items-center" title="View Friend Details" href='<%= ResolveUrl("Home.aspx") %>?RegId=<%#Eval("intInvitedUserId")%>'>
                                                                    <asp:Image ID="imgfrnd" runat="server" ImageUrl='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' ToolTip="View Friend Details" class="img-thumbnail avatar-img"
                                                                        alt="" Height="37px" Width="37px"></asp:Image>
                                                                    <span class="line-height">
                                                                        <asp:Label runat="server" class="name-title inline-block text-left max-width-165px truncate" ID="lblName" Text='<%#Eval("Name")%>'></asp:Label>
                                                                    </span>
                                                                </a>
                                                                <asp:LinkButton ID="aAddConnectionListing" runat="server" class="p-0 m-3 line-height" ClientIDMode="Static" Visible="false" CommandArgument='<%# Eval("intInvitedUserId") %>'
                                                                    CommandName="Connect"><%--onclick="CallAddFriends();" OnClientClick="javascript:AddfrndClick(); return false;"--%>
                                                   <span class="add-icon right-icon-alignments hide-body-scroll"><img src="images/add-user.svg"></span>
                                                   <%--<span class="text-with-icon">Add Friend</span>--%>
                                                                </asp:LinkButton>
                                                                <%--<a href="">
                                                   <span class="lnr lnr-envelope font-size-30px"></span>
                                                   </a>--%>
                                                                <asp:LinkButton ID="btnSndMsgFrnd" runat="server" CssClass="line-height text-right m-3 p-0 font-size-20px" OnClientClick="javascript:MsgClick();"
                                                                    Visible="false" CommandArgument='<%# Eval("intInvitedUserId") %>'
                                                                    CausesValidation="false" CommandName="SndMessage"><span class="lnr lnr-envelope"></span></asp:LinkButton>
                                                            </li>
                                                            <%--OnClientClick="if (!validatePage()MsgClick();) {return false;} else{MsgClick();}"--%>
                                                        </ul>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <li class="text-center border-top-1px p-2 view-all" id="ViewAllFrnds" runat="server" visible="false">
                                                    <asp:LinkButton ID="lnkViewAllFriend" runat="server" Font-Underline="false" Text="View all" OnClientClick="showLoader1();"
                                                        ClientIDMode="Static" CssClass="" OnClick="lnkViewAllFriend_Click" />
                                                    <%--OnClientClick="CallNewGroups();"--%>
                                                </li>
                                                <%--<div class="adding-btn-main">           
                                       </div>--%>
                                                <%--<div id="divAddUser" class="divAddUser" >
                                       <img src="images/Loadgif.gif" />
                                       </div>--%>
                                                <%--<div class="groupSection add_frnd" id="divFriendSection">--%>
                                                <%--<span class="add-icon">--%>
                                                <%--</span>--%>
                                                <%--</div>--%>
                                                <%--</div>--%>
                                            </div>
                                            <!-- custom-nav-container ended -->
                                            <!--group section starts-->
                                            <div class="custom-nav-container tab-pane container fade " id="menu1">
                                                <div class="padding-15" id="dvNewGroupsHeader" runat="server" clientidmode="Static">
                                                    <asp:LinkButton ID="lnkNewGroups" runat="server" Font-Underline="false" Text="Add Group"
                                                        ClientIDMode="Static" CssClass="" OnClick="lnkAddNewGrp_Click" OnClientClick="CallNewGroups();">
                                          <span class="add-icon"><img src="images/add-group.svg"></span>
                                          <span class="text-with-icon">Add Group</span>
                                                    </asp:LinkButton>
                                                </div>
                                                <asp:Repeater ID="rptGroup" runat="server" OnItemDataBound="rptGroup_ItemDataBound">
                                                    <ItemTemplate>
                                                        <ul>
                                                            <li>
                                                                <asp:HiddenField ID="hdnGroupId" runat="server" ClientIDMode="Static" Value='<%#Eval("inGroupId")%>' />
                                                                <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("strLogoPath") %>' />
                                                                <%--<div class="photoHomeProfile">--%>
                                                                <%--</div>--%>
                                                                <%--<div class="viewerName" id="viewerName">
                                                   <p class="viewerCommentTxt b_a_tag breakallwords">--%>
                                                                <%--  <span class="add-icon">--%>
                                                                <a title="View Group Details" class="d-flex align-items-center" href='<%# "Group-Profile.aspx?GrpId="+ Eval("inGroupId") %>'>
                                                                    <asp:Image ID="img_myGrp" ToolTip="View Group Details" runat="server" ImageUrl='<%# "CroppedPhoto/"+Eval("strLogoPath")%>'
                                                                        class="img-thumbnail avatar-img" Height="37px" Width="37px"></asp:Image>
                                                                    <span class="name-title truncate">
                                                                        <asp:Label runat="server" ID="lblName" Text='<%#Eval("strGroupName")%>'></asp:Label>
                                                                    </span>
                                                                </a>
                                                                <%-- </span>--%>
                                                                <%--</p>
                                                   </div>--%>
                                                            </li>
                                                        </ul>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <li class="text-center border-top-1px p-2 view-all" id="ViewAllGrps" runat="server" visible="false">
                                                    <asp:LinkButton ID="lnkViewAllGroup" runat="server" Font-Underline="false" Text="View all" OnClientClick="showLoader1();"
                                                        ClientIDMode="Static" CssClass="" OnClick="lnkViewAllGroup_Click" />
                                                    <%--OnClientClick="CallNewGroups();"--%>
                                                </li>
                                                <%--<ul>
                                       <li><a href="#"><img src="images/avatar.jpg" class="img-thumbnail avatar-img"><span class="name-title">Atlogys Group</span><span class="add-icon"><img src="images/add-user.svg"></span></a></li>            
                                       </ul>--%>
                                            </div>
                                            <!-- custom-nav-container ended -->
                                            <!--group section ends-->
                                        </div>
                                        <!-- tab-content ended -->
                                    </ContentTemplate>
                                    <%--<Triggers>
                              <asp:AsyncPostBackTrigger ControlID="aAddConnectionListing" />
                              </Triggers>--%>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- right-panal ended -->
        </div>
        <!-- panel-cover ended -->
    </div>
    <%--From this div need to be commented old code--%>
    <%--<div class="innerDocumentContainerSpc home_page_main">
      <div class="innerContainer">
         <div class="innerGroupBox for_home_page_only">--%>
    <%--<div class="innerSignUpTabs">--%>
    <!--top profile starts-->
    <%--<asp:HiddenField ID="hdnConnCommandName" runat="server" ClientIDMode="Static" Value="" />
      <asp:HiddenField ID="hdnloginIds" runat="server" ClientIDMode="Static" Value="" />
      <asp:HiddenField ID="hdnUserID" runat="server" ClientIDMode="Static" Value="" />
      <div class="display_none">
         <asp:Button ID="btnAccept" runat="server" ClientIDMode="Static" OnClick="ClickAccept" />
      </div>
      <asp:UpdatePanel ID="upProfileMain" runat="server">
         <ContentTemplate>
            <div class="tabProfileBox">--%>
    <%--<div class="photoIcon">--%>
    <%--<img id="imgUser" runat="server" height="160" width="160" clientidmode="Static" />--%>
    <%--<asp:LinkButton ID="lnkChangeImage" CssClass="camera " runat="server" ToolTip="Change Profile Image"
      ClientIDMode="Static" OnClick="lnkChangeImage_Click" OnClientClick="javascript:CallCameraload();">
      <img id="imgCamera" src="images/camera-icon.png" style="display: none;"/>
      </asp:LinkButton>
      <div id="divProilePic" style="display: none;" class="home_loader" >
      <img id="imgCameraLoad" src="images/Loadgif.gif" class="display_block" />
      </div>
      </div>--%>
    <%--<div class="CropImagepopupProfile  modal_bg" id="PopUpCropImage" clientidmode="Static" runat="server" style="display: none;"
      >
      <div class="popUp1  modal-dialog" id="PopUpCropImage1" clientidmode="Static" runat="server">
         <div class="popUp" id="popUp" >
            <div  class="modal-header" >
               &nbsp;  &nbsp; <b>Change Image</b>
            </div>
            <div class="home_profile_image">
               <crp1:CropImage ID="crp1" runat="server" />
               <br />
               <asp:LinkButton ID="lnkCancelProfilediv" runat="server" Text="Cancel" CssClass="cancelcropdivHome home_pr_image"
                  ClientIDMode="Static" > </asp:LinkButton>
            </div>
         </div>
      </div>
      </div>--%>
    <%--<div class="photoIconDtls">--%>
    <%--<div class="profile_dit_name">
      <p class="photoIconDtlsName">--%>
    <%--<asp:Label ID="lblUserProfName" runat="server" Text=""></asp:Label>--%>
    <!--edit starts-->
    <%--<asp:panel runat ="server">
      <div class="editIcon editName no_border edit_style" id="divEditUserProfile" runat="server" visible="false">
         <div id="smoothmenu1" class="ddsmoothmenu">
            <ul class=" header_edit">
               <li>
                  <a href="#">
                  <img src="images/edit.png" /></a>
                  <ul>
                     <li class="border_bottom">
                        <asp:LinkButton ID="lnkeditProfile" runat="server" Text="Edit" OnClientClick="javascript:CallUserJSON();javascript:callBodyDisable();return false;"></asp:LinkButton>
                     </li>
                  </ul>
               </li>
            </ul>
         </div>
      </div>
            </asp:panel>--%>
    <!--edit starts-->
    <%--<p>
      </p>
      <p>
      </p>
      <p>
      </p>
      <p>
      </p>
      <p>
      </p>
      <p>
      </p>
      <p>
      </p>
      <p>
      </p>
      <p>
      </p>
      <p>
      </p>
      <p>
      </p>--%>
    <%--</p>
      </div>--%>
    <%--<div class="endMsg">
      <div class="endorsements">
         <p class="txtT">
            <%--<asp:LinkButton ID="lblEndorseCount" runat="server" Text="" class="text_decoration_color" OnClick="lblEndorseCount_click" OnClientClick="divdeletes();" ></asp:LinkButton>--%>
    <%--</p>
      <p class="subTxtT">
         Endorsements
      </p>
      </div>
      <div class="messages">
      <p class="txtT">--%>
    <%--<asp:Label ID="lblMessCount" runat="server" Text=""></asp:Label>--%>
    <%--</p>
      <p class="subTxtT">
         Score
      </p>
      </div><div class="cls"></div>
      </div>
      </div>--%>
    <!--edit starts-->
    <%--<div class="EditProfilepopupHome  modal_bg" id="divEditProfile" clientidmode="Static" runat="server" style="display: none;"
      visible="false">
      <div class="editNamePop " id="divEditProfiles" >--%>
    <%--<div class="modal-header" ><b class="Profile_edit">Profile Edit</b></div>--%>
    <%--<div class="edit_popup">
      <p>
         <asp:TextBox ID="txtName" runat="server" placeholder="Name" ClientIDMode="Static"></asp:TextBox>
      </p>
      <p>
         <asp:TextBox ID="txtGender" runat="server" placeholder="Gender" ClientIDMode="Static"></asp:TextBox>
      </p>
      <p>
         <asp:TextBox ID="txtLanguage" runat="server" placeholder="Languages Known" ClientIDMode="Static"></asp:TextBox>
      </p>
      <asp:TextBox ID="txtEmailId" runat="server" placeholder="Email Id" Enabled="false"
         ClientIDMode="Static"></asp:TextBox>
      <p>
         <asp:TextBox ID="txtPhone" runat="server" ClientIDMode="Static" MaxLength="11" placeholder="Phone Number"></asp:TextBox>
      </p>
      <p class="error_msg_home">
         <asp:Label ID="lblProfilemsg" runat="server" ForeColor="Red"></asp:Label>
      </p>
      </div>--%>
    <%--<div class="cls"></div>
      <div class="cancel_btn_home">
         <asp:LinkButton ID="lnkProfileSave" CssClass="vote lnkProfileSaves" runat="server"
            Text="Save" OnClick="lnkProfileSave_click" OnClientClick="javascript:callBodyEnable();"></asp:LinkButton>
         <asp:LinkButton ID="lnkCancelProfile" CssClass="editNameCancel" runat="server" Text="Cancel"
            ClientIDMode="Static" OnClientClick="javascript:callBodyEnable();return false;"></asp:LinkButton>
      </div>--%>
    <%--</div>
      </div>--%>
    <%--</div>
      </ContentTemplate>
      <Triggers>
         <asp:AsyncPostBackTrigger ControlID="lnkChangeImage" />
      </Triggers>
      </asp:UpdatePanel>--%>
    <!--top profile ends-->
    <%--<div id="divDisp" class="cls home_external" runat="server">
      </div>--%>
    <%--<div class="tab_name innerTabHome">Home</div>--%>
    <!--tabs starts-->
    <%--<div id="divdisplayWall" runat="server" class="display_block" Visible="false">--%>
    <%--<asp:UpdatePanel ID="UpdatePanelTab" runat="server" UpdateMode="Always">
      <ContentTemplate>--%>
    <%--<div class="innerTabBox">--%>
    <%-- <div class="mobile_tab_icon">
      <div class="tab_icon">
       <i class="fa fa-ellipsis-v" aria-hidden="true"></i>
      </div>
      </div>--%>
    <%--<ul id="homeId" class="tabsProfile hide_m">
      <li>
         <asp:LinkButton runat="server" ID="lnkHome" class="innerTabHome" Text="Home" OnClick="lnkHome_Click"></asp:LinkButton>
      </li>
      <li>
         <asp:LinkButton runat="server" ID="lnkDocuments" class="innerTabDoc" Text="Documents"
            OnClick="lnkDocuments_Click"></asp:LinkButton>
      </li>--%>
    <%--<li >
      <asp:LinkButton runat="server" ID="lnkWorkex" class="innerWrkex innerWrk_width" Text="Work Experience"
         OnClick="lnkWorkex_Click"></asp:LinkButton>
      </li>--%>
    <%--<li>
      <asp:LinkButton runat="server" ID="lnkEducation" class="innerEdu" Text="Education"
         OnClick="lnkEducation_Click"></asp:LinkButton>
      </li>--%>
    <%--<li class="border_none">
      <asp:LinkButton runat="server" ID="lnkAchievements" class="innerAch" Text="Achievements"
         OnClick="lnkAchievements_Click"></asp:LinkButton>
      </li>
      </ul>
      </div>--%>
    <!--tabs ends-->
    <!--HOME START-->
    <!--left section ends-->
    <!--right section starts-->
    <%--</div>--%>
    <!--HOME ends-->
    <!--DOCUMENT START-->
    <%--<div id="PnlDocuments1">
      <asp:UpdatePanel ID="PnlDocuments" runat="server" Visible="false" UpdateMode="Conditional"
         ClientIDMode="Static">
         <ContentTemplate>--%>
    <!--main section starts-->
    <%--<asp:UpdatePanel ID="upFileUpl" runat="server" UpdateMode="Conditional">
      <ContentTemplate>--%>
    <%--<div class="cls"></div>--%>
    <%--<div class="work_exper margin_bottom_thirty">--%>
    <%--<div class="cls">
      </div>
      <p class="newGroupHeading innerTabDoc">
         Existing Documents
      </p>--%>
    <!--document starts-->
    <asp:HiddenField ID="hdnintdocIdelete" Value="" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnstrdocDescriptiondele" Value="" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnfilestrFilePathe" Value="" ClientIDMode="Static" runat="server" />
    <%--<asp:ListView ID="LstDocument" runat="server" GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder1"
      OnItemCommand="LstDocument_ItemCommand" OnItemDataBound="LstDocument_ItemDataBound"
      ItemPlaceholderID="itemPlaceHolder1">--%>
    <%--<LayoutTemplate>
      <table id="tblDoc" cellpadding="0" cellspacing="0" class="position_static">
         <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
      </table>
      </LayoutTemplate>
      <GroupTemplate>
      <tr>
         <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
      </tr>
      </GroupTemplate>
      <ItemTemplate>--%>
    <asp:HiddenField ID="hdnDocId" Value='<%#Eval("intDocId") %>' runat="server" />
    <asp:HiddenField ID="hdnDocsSee" Value='<%#Eval("intDocsSee") %>' runat="server" />
    <%--<asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
      <asp:HiddenField ID="hdnMaxcount" Value='<%#Eval("Maxcount") %>' runat="server" ClientIDMode="Static" />--%>
    <asp:HiddenField ID="hdnstrFilePath" Value='<%#Eval("strFilePath") %>' runat="server"
        ClientIDMode="Static" />
    <asp:HiddenField ID="hdnstrDocTitle" Value='<%#Eval("strDocTitle") %>' runat="server"
        ClientIDMode="Static" />
    <asp:HiddenField ID="hdnIsDocsDownload" Value='<%#Eval("IsDocsDownload") %>' runat="server"
        ClientIDMode="Static" />
    <%--<td class="home_code_refine">--%>
    <%--<div class="documentDts">
      <img src="images/snapshot.jpg" width="155" />
      <p class="docName">
         <asp:Label ID="lblDocument" runat="server" Text='<%#Eval("strDocTitle") %>' Style="display: none;"></asp:Label>
         <asp:HyperLink ID="lblDocument1" ClientIDMode="Static" class="client_mode" Target="_blank" ToolTip="Download file" NavigateUrl='<%# "~/UploadDocument/"+Eval("strFilePath")%>'
            Text='<%#Eval("strDocTitle") %>' Font-Size="Small" runat="server"></asp:HyperLink>
      </p>--%>
    <!--edit starts-->
    <%--<div class="editIcon editName" id="editUserComment" runat="server">--%>
    <%--<div id="smoothmenu1" class="ddsmoothmenu">--%>
    <%--<div id="divposition" class="position position_div">
      <asp:LinkButton ID="lnkEditDoc" Font-Underline="false" Visible="true" ToolTip="Edit"
         CssClass="scroll" Text="Edit" CommandName="EditDocs" CausesValidation="false"
         runat="server">                             
      </asp:LinkButton>
      <span class="ediDeldoc">
         <asp:HiddenField ID="hdnintdocIdelet" Value='<%# Eval("intDocId") %>' ClientIDMode="Static"
            runat="server" />
         <asp:HiddenField ID="hdnstrdocDescriptiondel" Value='<%# Eval("strDocTitle") %>'
            ClientIDMode="Static" runat="server" />
         <asp:HiddenField ID="hdnfilestrFilePath" Value='<%# Eval("strFilePath") %>' ClientIDMode="Static"
            runat="server" />
         <asp:LinkButton ID="lnkDeleteDoc" Visible="true" OnClientClick="javascript:docdelete();return false;"
            ToolTip="Delete" Text="Delete" CommandName="DeleteDocs" CausesValidation="false" 
            runat="server">                                       
         </asp:LinkButton>
      </span>
      </div>--%>
    <%--<div id="smoothmenu1" class="ddsmoothmenu ">
      <ul class="iconUl  position">
          <li><a href="#" onclick="return false">
              <img src="images/edit.png" /></a>
              <ul>
                  <li style="border-bottom: 1px solid #cdced0;">--%>
    <%--<div id ="EditDocument">     --%>
    <%--</div>--%>
    <%--</li>
      <li>--%>
    <%--</li>
      </ul>
      </li>
      </ul>--%>
    <%--<br style="clear: left" />--%>
    <%--</div>--%>
    <%--</div>--%>
    <!--edit end-->
    <%--</div>--%>
    <%--</td>--%>
    <%--</ItemTemplate>
      </asp:ListView>--%>
    <%--<div class="uploadDC" id="divDocumentUplaod" clientidmode="Static" runat="server"
      style="display: none;">--%>
    <%--<div class="document_div">--%>
    <%--<a name="docssection" id="aDivDoc"></a>
      <p class="imgVer">
         <asp:TextBox ID="txtDocTitle" runat="server" ClientIDMode="Static" class="newGroupInput skilset"
            ValidationGroup="docUpload"  placeholder="Name of the Document"></asp:TextBox>
      </p>
      <div class="cls"></div>--%>
    <%--<div class="makethiprv">
      <tabel class="width_h">
         <tr>
           <span class="align_mobile"> 
            <td>
               <asp:HiddenField ID="hdnimgPrivate" runat="server" ClientIDMode="Static" Value="0" />
               <img id="imgPrivate" clientidmode="Static" runat="server" src="images/unchk1.png" onclick="fncsave();"  />
            </td>
            <td>
              <span class="vertical-align"> Make this private</span>
            </td>
            </span>
            <span class="align_mobile">
            <td>
               <asp:HiddenField ID="hdnimgDownload" runat="server" ClientIDMode="Static" Value="1" />
               <img id="imgDownload" clientidmode="Static" class="secImg" runat="server" src="images/chk1.png" onclick="fncsavedow();" />
            </td>
            <td > 
              <span class="vertical-align"> Downloadable<
            </td>
            </span>
         </tr>
      </tabel>--%>
    <%--<asp:Button ID="savebtn" runat="server" OnClick="imgPrivate_Click" Style="display: none" />
      <asp:Button ID="savebtndow" runat="server" OnClick="imgDownload_Click" Style="display: none" />
      </div>--%>
    <%--<p>
      <textarea id="txtDescrition" runat="server" placeholder="Description" class="newGroupDescp"></textarea>
      </p>
      <p class="addcontect">
      Context:
      </p>
      <div class="cls">
      </div>
      <div class="hdnsubject_p">--%>
    <asp:HiddenField ID="hdnSubject" ClientIDMode="Static" runat="server" />
    <%--<ul class="context">--%>
    <%--<asp:ListView ID="lstSubjCategory" runat="server" OnItemDataBound="LstSubjCategory_ItemDataBound"
      GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">--%>
    <%--<LayoutTemplate>
      <table width="90%">
         <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
      </table>
      </LayoutTemplate>--%>
    <%--<GroupTemplate>
      <tr>
         <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
      </tr>
      </GroupTemplate>--%>
    <%--<ItemTemplate>
      <li id="SubLi" runat="server" >--%>
    <asp:HiddenField ID="hdnSubCatId" ClientIDMode="Static" runat="server" Value='<%#Eval("intCategoryId")%>' />
    <asp:HiddenField ID="hdnCountSub" ClientIDMode="Static" runat="server" Value='<%#Eval("CountSub")%>' />
    <%--<asp:Label ID="lnkCatName" ClientIDMode="Static" CssClass="unselectedtagnameGroup"
      runat="server" Text='<%#Eval("strCategoryName")%>' CommandName="Subject Category"></asp:Label>
      </li>
      </ItemTemplate>--%>
    <%--</asp:ListView>--%>
    <%--</ul>
      </div>--%>
    <%--<div class="uploadBox fileUploadHome uploadDocsHomeHome" id="upload" runat="server"
      clientidmode="Static">
      <span>Upload Document</span>
      <asp:FileUpload ID="uploadDoc" ClientIDMode="Static" runat="server" CssClass="upload display_block"
         />
      </div>
      <div class="cls">
      </div>
      <p>--%>
    <%--<asp:Label ID="lblfilenamee" runat="server" ClientIDMode="Static">
      </asp:Label>--%>
    <asp:HiddenField ID="hdnUploadFile" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnUploadFile1" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnFilePath" runat="server" ClientIDMode="Static" />
    <%--<br />
      <asp:LinkButton ID="lnkDeleteDoc123" ClientIDMode="Static" class="conform_home" runat="server" OnClientClick="javascript:return confirm('DO YOU WANT TO DELETE?');"
         OnClick="lnkDelete_Click">Delete</asp:LinkButton>--%>
    <%--</p>
      <p>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDocTitle"
            Display="Dynamic" ValidationGroup="docUpload" ErrorMessage="Please enter Title"
            ForeColor="Red"></asp:RequiredFieldValidator>
      </p>--%>
    <%--<asp:LinkButton ID="LinkSave" CssClass="createGroup LinkSave" runat="server" Text="Save"
      ValidationGroup="docUpload" OnClick="btnSave_Click" ClientIDMode="Static" OnClientClick="javascript:callDocSave();"></asp:LinkButton>
      <asp:LinkButton ID="lnkCancelDoc" CssClass="cancelGroup" runat="server" Text="Cancel"
      ClientIDMode="Static" OnClick="lnkCancelDoc_Click" OnClientClick="javascript:callDoccancel();"></asp:LinkButton>--%>
    <%--</div>--%>
    <%--</div>--%>
    <%--<div class="cls"></div>
      <div class="doc_bruuton_style home_documents">
         <asp:LinkButton class="addWorkEx" ID="lnkuploadDoc" runat="server" ClientIDMode="Static" OnClientClick="CallUploadDoc();" 
            Text="Upload Document" OnClick="lnkuploadDoc_click"></asp:LinkButton>
      </div>--%>
    <%--</div>--%>
    <%--</span>--%>
    <%--</ContentTemplate>
      </asp:UpdatePanel>--%>
    <!--main section ends-->
    <%--</ContentTemplate>
      </asp:UpdatePanel>
      </div>--%>
    <!--DOCUMENT END-->
    <!--Work Exp START-->
    <%--<div id="PnlWorkex1">
      <asp:UpdatePanel ID="PnlWorkex" runat="server" Visible="false" UpdateMode="Conditional"
         ClientIDMode="Static">
         <ContentTemplate>--%>
    <!--main section starts-->
    <%--<div class="cls"></div>--%>
    <%--<div class=" remover_l_r_padding">--%>
    <%--<div class="work_exper">--%>
    <%--<p class="newGroupHeading innerWrkex">
      Work Experience
      </p>--%>
    <asp:HiddenField ID="hdnintworkeIdelet" Value="" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnstrworkeDescriptiondel" Value="" ClientIDMode="Static" runat="server" />
    <%--<asp:ListView runat="server" ID="lstWorkExperience" OnItemDataBound="lstWorkExperience_ItemDataBound"
      OnItemCommand="lstWorkExperience_ItemCommand" CellPadding="9">
      <ItemTemplate>--%>
    <asp:HiddenField ID="hdnintExperienceId" Value='<%#Eval("intExperienceId") %>' runat="server" />
    <%--<asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />--%>
    <%--<p class="newGroupContext ex">
      <asp:Label ID="lblCompanyName" runat="server" Text='<%#Eval("strCompanyName") %>' />
      </p>
      <p class="workExDesignation">
      <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("strDesignation") %>' />
      </p>
      <p class="exDuration">
      <asp:Label ID="lblinFromtMonth" runat="server" Text='<%#Eval("inFromtMonth") %>' />
      <asp:Label ID="lblFromYear" runat="server" Text='<%#Eval("intFromYear") %>' />
      -
      <asp:Label ID="lblintToMonth" runat="server" Text='<%#Eval("intToMonth") %>' />
      <asp:Label ID="lblToYear" runat="server" Text='<%#Eval("intToYear") %>' />
      </p>
      <div class="cls">
      </div>--%>
    <!--edit starts-->
    <%--<div class="editIcon exBox edit_code_refine" id="divUserexperenceED" style=""
      runat="server">--%>
    <%--<div id="smoothmenu1" class="ddsmoothmenu">
      <ul class="iconUl ">
          <li><a href="#" onclick="return false">
              <img src="images/edit.png" /></a>
              <ul>
                  <li style="border-bottom: 1px solid #cdced0;">--%>
    <%--<div id ="EditUserExp">
      <asp:LinkButton ID="lnkEditDoc" Font-Underline="false" Visible="true" ToolTip="Edit"
         Text=""  class="new_edit edit" CommandName="EditExp" CausesValidation="false" runat="server"> 
      </asp:LinkButton>
      </div>--%>
    <%--</li>
      <li>--%>
    <%--<div id ="DeleteUserExp">
      <span class="ediDelwork">--%>
    <asp:HiddenField ID="hdnintworkIdelet" Value='<%# Eval("intExperienceId") %>' ClientIDMode="Static"
        runat="server" />
    <asp:HiddenField ID="hdnstrworkDescriptiondel" Value='<%# Eval("strCompanyName") %>'
        ClientIDMode="Static" runat="server" />
    <%--<asp:LinkButton ID="lnkDeleteDoc" Font-Underline="false" Visible="true" ToolTip="Delete"
      Text="" class="delete_new_c" CommandName="DeleteExp" CausesValidation="false" runat="server"
      OnClientClick="javascript:docdelete();return false;"> <img src="images/Delete_icon.png">
      </asp:LinkButton>--%>
    <%--</span>
      </div>--%>
    <%-- </li>
      </ul>
      </li>
      </ul>--%>
    <%--<br style="clear: left" />--%>
    <%--</div>
      </div>--%>
    <!--edit starts-->
    <%--<div class="cls">
      </div>
      <p class="description_text">
         <asp:Label ID="Label2" runat="server" Text='<%#Eval("strDescription") %>' />
      </p>--%>
    <%--</ItemTemplate>
      </asp:ListView>--%>
    <%--</div>--%>
    <%--<div class="cls">
      </div>--%>
    <!--add exp starts-->
    <%--<div class="uploadBg" id="AddWorkExp" runat="server" style="display: none;" clientidmode="Static">
      <div  class="upload_refine">--%>
    <%--<p>
      <asp:TextBox ID="txtInstituteName" runat="server" placeholder="Company name" class="newGroupInput"
         ClientIDMode="Static" ValidationGroup="workExp"></asp:TextBox>
      </p>
      <p>
      <asp:TextBox ID="txtDegree" ClientIDMode="Static" runat="server" placeholder="Position"
         class="newGroupInput"></asp:TextBox>
      </p>
      <asp:UpdatePanel ID="upda" runat="server">
      <ContentTemplate>
         <p class="i_pad_Respnsive">
            <span class="timeframe">Timeframe</span>
            <select name="select" id="fromMonth" runat="server">
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
            <asp:DropDownList ID="txtFromYear" runat="server" CssClass="signInputY" ClientIDMode="Static">
            </asp:DropDownList>
            <span class="spanDash"> to </span>
            <asp:DropDownList ID="toMonth" runat="server" ClientIDMode="Static">
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
            <asp:DropDownList ID="txtToYear" runat="server" CssClass="signInputY" ClientIDMode="Static">
            </asp:DropDownList>
            <asp:CheckBox ID="chkAtPresent" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkAtPresent_CheckedChanged"
               runat="server" />
            Currently employer
         </p>
      </ContentTemplate>
      <Triggers>
         <asp:AsyncPostBackTrigger ControlID="chkAtPresent" />
      </Triggers>
      </asp:UpdatePanel>--%>
    <%--<p>
      <textarea class="newGroupDescp" id="txtDescription" runat="server" placeholder="Description"></textarea>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInstituteName"
         ValidationGroup="workExp" ErrorMessage="Please enter company name." EnableClientScript="true"
         ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDegree"
         Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please enter position."
         ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="fromMonth"
         InitialValue="Month" Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please select from month."
         ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromYear"
         Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please enter from year."
         InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="toMonth"
         InitialValue="Month" Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please select to month."
         ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtToYear"
         Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please enter to year."
         InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator>
      </p>--%>
    <%--<asp:LinkButton runat="server" ID="lnlSaveExp" Text="Save" CssClass="createGroup"
      ClientIDMode="Static" ValidationGroup="workExp" OnClick="lnlSaveExp_Click" OnClientClick="javascript:callSaveExp();"></asp:LinkButton>
      <asp:LinkButton runat="server" ID="LinkButton1" Text="Cancel" CssClass="cancelGroup"
      OnClick="lnlCancel_Click" ClientIDMode="Static" Visible="true" OnClientClick="javascript:callCancelExp();"></asp:LinkButton>--%>
    <%--</div>
      </div>--%>
    <!--add exp ends-->
    <%--<a name="captchaAnchor"></a>
      <div class="cls"></div>
                   <div class="button_style for_work_E">
         <asp:LinkButton class="addWorkEx" ID="aAddworkExp" runat="server" ClientIDMode="Static" OnClientClick="CallaAddworkExp();"
            Text="+  Add Work Experience" OnClick="aAddworkExp_click"></asp:LinkButton>
      </div>
      </div>--%>
    <%--<div class="style_skillset">--%>
    <%--<p class="newGroupHeading sk skillset_image">
      Skillset
      </p>
      <div class="cls">
      </div>
      <a name="SkillsetSection"></a>--%>
    <!--edit starts-->
    <%-- <div class="editIcon skBox remove_top_space" id="divSkillEditdelete" runat="server">
      <div id="smoothmenu1" class="ddsmoothmenu">
         <ul class="iconUl">
            <li>
               <a href="#" onclick="return false">
                  <asp:LinkButton ID="lnkEditSkill"   
                     CausesValidation="false" runat="server" OnClick="lnkEditSkill_Click"> <img src="images/edit.png"  />  </asp:LinkButton>
               </a>
               <!--      <ul>
                  <li style="border-bottom: 1px solid #cdced0;">
                      <%--<a href="#">Edit</a>--%>
    <%--</li>
      </ul> -->
      </li>
      </ul>
      
      </div>
      </div>--%>
    <!--edit starts-->
    <%--<div class="cls">
      </div>
      <asp:UpdatePanel ID="upskillsets" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
         <ContentTemplate>
            <ul class="skillsetList" runat="server" id="skillsets" clientidmode="Static">
               <asp:ListView ID="lstAreaExpert" runat="server" OnItemCommand="lstAreaExpert_ItemCommand"
                  OnItemDataBound="lstAreaExpert_ItemDataBound">
                  <ItemTemplate>
                     <asp:HiddenField ID="hdnintUserSkillId" runat="server" Value='<%#Eval("intUserSkillId")%>' />
                     <li class="endronsecssli" >
                        <asp:UpdatePanel ID="upskillend" runat="server" ClientIDMode="Static">
                           <ContentTemplate>
                              <asp:Label ID="lblEndronseCount" runat="server" class="sscount" Text="0"></asp:Label>
                              <asp:Label ID="lblstrSkillName" runat="server" class="ssField" Text='<%#Eval("strSkillName")%>'></asp:Label>
                              <img id="imgPlus" runat="server" src="images/plus.png" clientidmode="Static" visible="false"
                                 class="endronsecssimg" />
                              <asp:LinkButton ID="lnkEndrose" ClientIDMode="Static" class="endronsecss" runat="server"
                                 CommandName="EndronseSkill" Text="Endorse" Style="display: none;"></asp:LinkButton>
                           </ContentTemplate>
                           <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="lnkEndrose" />
                           </Triggers>
                        </asp:UpdatePanel>
                     </li>
                  </ItemTemplate>
               </asp:ListView>
            </ul>
         </ContentTemplate>
         <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lstAreaExpert" />
         </Triggers>
      </asp:UpdatePanel>--%>
    <%--<div class="cls">
      <p>
         &nbsp;
      </p>
      </div>--%>
    <!--add exp starts-->
    <%--<div class="uploadBg" id="divAddskill" runat="server" style="display: none;" clientidmode="Static">
      <asp:UpdatePanel ID="updateskills" runat="server">
         <ContentTemplate>
            <div class="set_skill">
               <p>
                  <asp:TextBox ID="txtAreaExpert" runat="server" placeholder="Add your area of expertise"
                     ValidationGroup="skills" class="newGroupInput skilset" ClientIDMode="Static"></asp:TextBox>
                  <asp:LinkButton ID="btnAreaExpSave" runat="server" Text="Add" ClientIDMode="Static"
                     OnClientClick="javascript:CallAddSkill();" ValidationGroup="skills" CssClass="createGroup addsk"
                     OnClick="btnAreaExpSave_Click"></asp:LinkButton>
               <div style="display:none;">
                  <asp:Button ID="btnAreaExpSave1" runat="server" Text="Add" ClientIDMode="Static"
                     ValidationGroup="skills" CssClass="createGroup addsk" OnClick="btnAreaExpSave_Click" />
               </div>
               </p>--%>
    <%--<ul class="skillsetList editss new_home_edit">
      <asp:ListView ID="lstAreaSkill" runat="server" OnItemCommand="lstAreaSkill_ItemCommand"
         GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
         <GroupTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
         </GroupTemplate>
         <ItemTemplate>
            <asp:HiddenField ID="hdnintUserSkillId" runat="server" Value='<%#Eval("intUserSkillId")%>' />
            <li class="skills_add_style">
               <span class="sscount">0</span> 
               <span class="ssField">
                  <%#Eval("strSkillName")%>
                  <asp:LinkButton ID="lnkdDelete" runat="server" CommandName="DeleteExp">
                     <img src="images/close.png" />
                  </asp:LinkButton>
               </span>
            </li>
         </ItemTemplate>
      </asp:ListView>
      </ul>
      <p>
      <asp:Label ID="lblareaskill" ClientIDMode="Static" runat="server" ForeColor="Red"></asp:Label>
      </p>
      <div class="cls"></div>--%>
    <%--<div class="spacing">
      <asp:LinkButton ID="lnkSaveSkill" runat="server" Text="Save" ClientIDMode="Static"
         CssClass="createGroup" OnClick="lnkSaveSkill_Click" OnClientClick="javascript:callSaveSkill();"></asp:LinkButton>
      <asp:LinkButton ID="lnkCancelSkill" runat="server" Text="Cancel" ClientIDMode="Static"
         CssClass="cancelGroup" OnClick="lnkCancelSkill_Click" OnClientClick="javascript:callCancelSkill();"></asp:LinkButton>
      </div>
      </div>
      </ContentTemplate>
      <Triggers>
      <asp:AsyncPostBackTrigger ControlID="lnkCancelSkill" />
      <asp:AsyncPostBackTrigger ControlID="btnAreaExpSave" />
      <asp:AsyncPostBackTrigger ControlID="btnAreaExpSave1" />
      </Triggers>
      </asp:UpdatePanel>
      </div>--%>
    <!--add exp ends-->
    <%--<div class="cls"></div>
      <div class="button_style2">
         <asp:LinkButton ID="lnkAddskill" runat="server" OnClick="lnkAddSkill_Click" CssClass="addWorkEx"
            Text="+ Add Skill" OnClientClick="CalllnkAddskill();" ></asp:LinkButton>
      </div>
      </div>--%>
    <!--main section ends-->
    <%--</ContentTemplate>
      <Triggers>
         <asp:AsyncPostBackTrigger ControlID="lnlSaveExp" />
         <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
         <asp:AsyncPostBackTrigger ControlID="lnkSaveSkill" />
         <asp:AsyncPostBackTrigger ControlID="lnkCancelSkill" />--%>
    <%--<asp:AsyncPostBackTrigger ControlID="LinkButton1" />
      <asp:AsyncPostBackTrigger ControlID="aAddworkExp" />
      <asp:AsyncPostBackTrigger ControlID="lnkAddskill" />
      </Triggers>
      </asp:UpdatePanel>
      </div>--%>
    <!--Work Exp End-->
    <!--Education START-->
    <%--<asp:UpdatePanel ID="PnlEduction" runat="server" Visible="false" UpdateMode="Conditional"
      ClientIDMode="Static">
      <ContentTemplate>--%>
    <!--main section starts-->
    <%--<div class="cls"></div>
      <div class="work_exper margin_bottom_thirty">--%>
    <%--<div class="cls">
      </div>
      <p class="newGroupHeading innerEdu">
         Education
      </p>
      <div class="cls">
      </div>--%>
    <asp:HiddenField ID="hdnintedueIdelet" Value="" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnstredueDescriptiondel" Value="" ClientIDMode="Static" runat="server" />
    <%--<asp:ListView runat="server" ID="lstEducation" OnItemDataBound="lstEducation_ItemDataBound"
      OnItemCommand="lstEducation_ItemCommand" CellPadding="9">
      <ItemTemplate>--%>
    <asp:HiddenField ID="hdnintEducationId" Value='<%#Eval("intEducationId") %>' runat="server" />
    <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
    <asp:HiddenField ID="hdnToMonth" Value='<%#Eval("ToMonth") %>' runat="server" />
    <%--<div class="text_align">
      <p class="newGroupContext ex">
         <asp:Label ID="lblInstituteName" runat="server" Text='<%#Eval("strInstituteName") %>' />
      </p>
      <p class="workExDesignation">
         <asp:Label ID="lblEducationAchievement" runat="server" Text='<%#Eval("strDegree") %>' />
      </p>
      <p class="exDuration">
         <asp:Label ID="lblFromMonth" runat="server" Text='<%#Eval("intMonth") %>' />
         <asp:Label ID="lblFromYear" runat="server" Text='<%#Eval("intYear") %>' />
         -
         <asp:Label ID="lblintToMonth" runat="server" Text='<%#Eval("intToMonth") %>' />
         <asp:Label ID="lblintToYear" runat="server" Text='<%#Eval("intToYear") %>' />
         <asp:Label ID="lblPresentDay" runat="server" />
      </p>
      </div>
      <div class="cls">
      </div>
      <div class="cls">
      </div>--%>
    <!--edit starts-->
    <%--<div class="editIcon exBox exbox_refine" id="divEducationED" runat="server" 
      clientidmode="Static">
      <div id="smoothmenu1" class="ddsmoothmenu">
         <ul class="iconUl">
             <li><a href="#" onclick="return false">
                 <img src="images/edit.png" /></a>
                 <ul>
                     <li >--%>
    <%--<div id =" editEducation">
      <asp:LinkButton ID="lnkEditDoc" Font-Underline="false" Visible="true" ToolTip="Edit"
         Text="" CommandName="EditEdu" class="new_edit" CausesValidation="false" runat="server">               
      </asp:LinkButton>
      </div>--%>
    <%--      </li>
      <li>
    --%>
    <%--<div id ="DeleteEducation">
      <span class="ediDeledu">
         <asp:HiddenField ID="hdninteduIdelet" Value='<%# Eval("intEducationId") %>' ClientIDMode="Static"
            runat="server" />
         <asp:HiddenField ID="hdnstreduDescriptiondel" Value='<%# Eval("strInstituteName") %>'
            ClientIDMode="Static" runat="server" />
         <asp:LinkButton ID="lnkDeleteDoc" Visible="true" OnClientClick="javascript:docdelete();return false;"
            ToolTip="Delete" Text=""  class="delete_new_c" CommandName="DeleteEdu" CausesValidation="false"
            runat="server">    <img src="images/Delete_icon.png">                              
         </asp:LinkButton>
      </span>
      </div>--%>
    <%--</li>
      </ul>
      </li>
      </ul>
      <br style="clear: left" />--%>
    <%--</div>
      </div>--%>
    <!--edit starts-->
    <%--<div class="cls">
      </div>
      <div class="text_align">
         <p >
            <asp:Label ID="Label2" runat="server" Text='<%#Eval("strSpclLibrary") %>' />
         </p>
      </div>
      </ItemTemplate>
      </asp:ListView>
      <div class="cls">
      </div>--%>
    <!--add exp starts-->
    <%--<div class="uploadBg education_div" id="divEducation" runat="server" style="display: none;" clientidmode="Static">
      <div>--%>
    <%--<p>
      <asp:TextBox ID="txtInstitute" runat="server" ClientIDMode="Static" placeholder="Institute Name"
         class="newGroupInput"></asp:TextBox>
      <ajax:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
         CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtInstitute"
         ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
      </ajax:AutoCompleteExtender>
      </p>
      <p>
      <asp:TextBox ID="txtTitle" runat="server" ClientIDMode="Static" placeholder="Degree"
         class="newGroupInput"></asp:TextBox>
      <ajax:AutoCompleteExtender ServiceMethod="GetDegreeList" MinimumPrefixLength="1"
         CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtTitle"
         ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false">
      </ajax:AutoCompleteExtender>
      </p>--%>
    <%--<asp:UpdatePanel ID="updates" runat="server">
      <ContentTemplate>
         <p>
            <span class="timeframe">Timeframe</span>
            <select name="select" id="ddlFromMonth" runat="server" clientidmode="Static">
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
            <asp:DropDownList ID="txtEducationFromdt" runat="server" CssClass="signInputY" ClientIDMode="Static">
            </asp:DropDownList>
            <span class="spanDash"> to </span>--%>
    <%--<asp:DropDownList ID="ddlToMonth" runat="server" ClientIDMode="Static">
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
      </asp:DropDownList>--%>
    <%--<asp:DropDownList ID="txtEducationTodt" runat="server" CssClass="signInputY" ClientIDMode="Static">
      </asp:DropDownList>--%>
    <%--<asp:CheckBox ID="chkEducation" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkAtPresent_CheckedChanged1"
      runat="server" />
      Currently studying
      </p>
      </ContentTemplate>
      <Triggers>
      <asp:AsyncPostBackTrigger ControlID="chkEducation" />
      </Triggers>
      </asp:UpdatePanel>--%>
    <%--<p>
      <textarea class="newGroupDescp" id="txtEduDescription" runat="server" placeholder="Description"
         clientidmode="Static"></textarea>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtInstitute"
         Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please enter Institute / University name"
         ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtTitle"
         Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please enter Degree"
         ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlFromMonth"
         Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select from month."
         InitialValue="Month" ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtEducationFromdt"
         Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select from year."
         InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlToMonth"
         Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select to month."
         InitialValue="Month" ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtEducationTodt"
         Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select to year."
         InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
      </p>
      <asp:LinkButton runat="server" ID="lnkSaveEducation" Text="Save" CssClass="createGroup"
      ClientIDMode="Static" ValidationGroup="validationEdu" OnClick="lnkSaveEducation_Click"
      OnClientClick="javascript:callSaveEdu();"></asp:LinkButton>
      <asp:LinkButton runat="server" ID="lnkCancelEducation" Text="Cancel" CssClass="cancelGroup"
      ClientIDMode="Static" OnClick="lnkCancelEducation_Click" OnClientClick="javascript:callCancelEdu();"></asp:LinkButton>--%>
    <%--</div>
      </div>
      <!--add exp ends-->
      <div class="cls">
         <p>
            &nbsp;
         </p>
      </div>
      <asp:LinkButton runat="server" ID="lnlAddEducation" Text="Add Education" CssClass="addWorkEx"
         OnClick="lnlAddEducation_Click" OnClientClick="CalllnlAddEducation();"></asp:LinkButton>
      <div class="cls">
      </div>
      </div>--%>
    <!--add exp ends-->
    <%--</div>
      <!--main section ends-->
      </ContentTemplate>
      <Triggers>--%>
    <%--<asp:AsyncPostBackTrigger ControlID="lnkSaveEducation" />
      <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
      <asp:AsyncPostBackTrigger ControlID="lnkCancelEducation" />
      </Triggers>
      </asp:UpdatePanel>--%>
    <!--Education End-->
    <!--Achivement START-->
    <%--<asp:UpdatePanel ID="PnlAchivement" runat="server" Visible="false" UpdateMode="Conditional"
      ClientIDMode="Static">
      <ContentTemplate>
      <!--main section starts-->
      <div class="cls"></div>
      <div class="work_exper margin_bottom_thirty">
      <div class="cls">
      </div>
      <p class="newGroupHeading innerAch">
      Achievements</p>
    --%>
    <%--<asp:ListView runat="server" ID="lstAchivement" OnItemDataBound="lstAchivement_ItemDataBound"
      OnItemCommand="lstAchivement_ItemCommand" CellPadding="9">
      <ItemTemplate>
      <asp:HiddenField ID="hdnintAchivmentId" Value='<%#Eval("intAchivmentId") %>' runat="server"
      ClientIDMode="Static" />
      <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />--%>
    <%--<div class="text_align">
      <p class="newGroupContext ex">
      <asp:Label ID="lblstrCompititionName" runat="server" Text='<%#Eval("strCompititionName") %>' /></p>
      <p class="workExDesignation">
      <asp:Label ID="lblstrPosition" runat="server" Text='<%#Eval("strPosition") %>' /></p>
      </div>--%>
    <!--edit starts-->
    <%--<div class="editIcon exBox achivement_edit" id="divAchivementED" 
      runat="server">
      <div id="smoothmenu1" class="ddsmoothmenu">
      <ul class="iconUl" style="margin-top: 56px;">
          <li><a href="#" onclick="return false">
              <img src="images/edit.png" /></a>
              <ul>
                  <li style="border-bottom: 1px solid #cdced0;">--%>
    <%--<div id ="editAchivement">
      <asp:LinkButton ID="lnkEditDoc" Font-Underline="false" Visible="true" ToolTip="Edit"
         Text="" class="new_edit" CommandName="EditAch" CausesValidation="false" runat="server">
      </asp:LinkButton>
      </div>--%>
    <%--</li>
      <li>--%>
    <%--<div id ="deleteAchivement">
      <span class="ediDelach">
      <asp:HiddenField ID="hdnintachIdelet" Value='<%# Eval("intAchivmentId") %>' ClientIDMode="Static"
         runat="server" />
      <asp:HiddenField ID="hdnstrachDescriptiondel" Value='<%# Eval("strCompititionName") %>'
         ClientIDMode="Static" runat="server" />
      <asp:LinkButton ID="lnkDeleteDoc" Visible="true" OnClientClick="javascript:docdelete();return false;"
         ToolTip="Delete" Text=""  class="delete_new_c" CommandName="DeleteAch" CausesValidation="false"
         runat="server">   <img src="images/Delete_icon.png">                                 
      </asp:LinkButton>
      </span>
      </div>--%>
    <%--</li>
      </ul>
      </li>
      </ul>--%>
    <%--</div>
      </div>--%>
    <!--edit starts-->
    <%--<div class="text_align home_text_align">
      <p >
      <asp:Label ID="Label2" runat="server" Text='<%#Eval("strDescription") %>' /></p>
      </div>--%>
    <%--</ItemTemplate>
      </asp:ListView>--%>
    <!--add exp starts-->
    <%--<div class="uploadBg education_div" id="divAchivement" runat="server" style="display: none;" clientidmode="Static">
      <div class="achivements_div">--%>
    <%--<p>
      <asp:DropDownList ID="ddlMilestone" runat="server" ClientIDMode="Static" class="consellist"
         ValidationGroup="validationAchiv">
      </asp:DropDownList>
      </p>
      <p>
      <asp:DropDownList ID="ddlPosition" runat="server" ClientIDMode="Static" class="consellist"
         ValidationGroup="validationAchiv">
      </asp:DropDownList>
      </p>--%>
    <%--<p>
      <asp:TextBox ID="txtCompitition" runat="server" placeholder="Name of Competition"
         ClientIDMode="Static" class="newGroupInput"></asp:TextBox>
      <ajax:AutoCompleteExtender ServiceMethod="GetCompititionLists" MinimumPrefixLength="1"
         CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtCompitition"
         ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false">
      </ajax:AutoCompleteExtender>
      </p>
      <p>
      <asp:TextBox ID="txtAdditionalAwrd" runat="server" placeholder="Additional Award"
         class="newGroupInput"></asp:TextBox>
      </p>
      <p>
      <textarea id="txtAchivDescription" runat="server" placeholder="Description" class="newGroupDescp"></textarea></p>--%>
    <%--<p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlMilestone"
         Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please select milestone."
         InitialValue="Type of Milestone" ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtCompitition"
         Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please enter competition"
         ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlPosition"
         Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please select position."
         InitialValue="Position" ForeColor="Red"></asp:RequiredFieldValidator>
      </p>
      <p>
      <asp:Label ID="lblAchivmentmsg" runat="server" ForeColor="Red" ClientIDMode="Static"></asp:Label>
      </p>--%>
    <%-- <asp:LinkButton ID="lnkSaveAchivemnt" runat="server" ClientIDMode="Static" CssClass="createGroup"
      ValidationGroup="validationAchiv" Text="Save" OnClick="lnkSaveAchivemnt_Click"
      OnClientClick="javascript:callSaveAch();"></asp:LinkButton>
      <asp:LinkButton ID="LinkButton4" runat="server" ClientIDMode="Static" CssClass="cancelGroup"
      Text="Cancel" OnClick="lnkCancelAchivemnt_Click" OnClientClick="javascript:callCancelAch();"></asp:LinkButton>
      </div>
      </div>--%>
    <!--add exp ends-->
    <%--<asp:LinkButton ID="lnkAddachive" runat="server" class="addWorkEx" Style="margin-top: 25px;"
      Text="Add Achievements and Milestones" OnClientClick="CalllnkAddachive();" OnClick="lnkAddachive_Click"></asp:LinkButton>
      </div>--%>
    <!--main section ends-->
    <%--</ContentTemplate>
      <Triggers>--%>
    <%--<asp:AsyncPostBackTrigger ControlID="lnkSaveAchivemnt" />--%>
    <%--<asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />--%>
    <%--<asp:AsyncPostBackTrigger ControlID="LinkButton4" />--%>
    <%--<asp:AsyncPostBackTrigger ControlID="lnkAddachive" />
      </Triggers>
      </asp:UpdatePanel>--%>
    <!--Achivement End  background-color: #000000;-->
    <%--<div class="display_none">
      <asp:Button ID="btnPostCommentSave" runat="server" ClientIDMode="Static" OnClick="btnPostCommentSave_Click" />
      </div>
      <asp:UpdateProgress id="updateProgress" runat="server">
      <ProgressTemplate>
      <div class="loader_home_gif">
      <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/loadingImage.gif" AlternateText="Loading ..." ToolTip="Loading ..."  class="divProgress divprogress_home" />
      </div>
      </ProgressTemplate>
      </asp:UpdateProgress>--%>
    <%--</ContentTemplate>
      <Triggers>--%>
    <%--<asp:AsyncPostBackTrigger ControlID="LinkSave" />
      <asp:AsyncPostBackTrigger ControlID="lnkuploadDoc" />--%>
    <%--<asp:AsyncPostBackTrigger ControlID="lnkCancelDoc" />--%>
    <%--<asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />--%>
    <%--<asp:AsyncPostBackTrigger ControlID="btnPostCommentSave" />--%>
    <%--</Triggers>
      </asp:UpdatePanel>--%>
    <%--</div>--%>
    <%--</div>--%>
    <%--<div class="cls">--%>
    <asp:HiddenField ID="hdnintacheIdelet" Value="" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnstracheDescriptiondel" Value="" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnPostDelete" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="hdnAchivmentId" runat="server" ClientIDMode="Static" />
    <%--</div>--%>
    <!--left verticle search list ends-->
    <%--</div>
      </div>
      </div>--%>
    <script type="text/javascript">
        function showRight() {
            setTimeout(function () { $('.m-aside-trigger').click(); }, 500);
        }
        function CallProfilename() {
            $('#ctl00_lblUserName').text($('#ctl00_ContentPlaceHolder1_lblUserProfName').text());
        }
        function setPostId() {
            $('#hdnintPostId').val($(this).children('#hdnintPostIdelet').val());
            $('#hdnstrPostDescriptiondele').val($(this).children('#hdnstrPostDescriptiondel').val());
        }

        function postCommentNew(e) {
            if (e.keyCode == 13 && $(this).val()) {
                window.location.href = $(this).parent().parent().children('.lnkEnterCommentcss').attr('href');
                return false;
            } else if (e.keyCode == 13) {
                return false;
            }

        }

        function postCommentInput(e) {
            var msg = $(this).val().replace(/\n/g, "");
            if (msg != $(this).val()) { // Enter was pressed
                $(this).val(msg);
                window.location.href = $(this).parent().parent().children('.lnkEnterCommentcss').attr('href');
                showLoader1();
            }
        }

        function setPostCommentId() {
            $('#hdnintPostceIdelet').val($(this).children('#hdnintPostcIdelet').val());
            $('#hdnstrPostDescriptioncedel').val($(this).children('#hdnstrPostDescriptioncdel').val());
        }
        function CallHomeMiddle() {
            $("html, body").animate({ scrollTop: 1000 }, 1);
        }
        function CallDocMiddle() {
            var offset = $("#txtDocTitle").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $(window).scrollTop();
            var txt = document.getElementById("txtDocTitle");
            var p = GetScreenCordinates(txt);
            $("html, body").animate({ scrollTop: $(window).height() }, 10);
        }
        function CallWorkMiddle() {
            var offset = $("#aAddworkExp").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $("#aAddworkExp").position().top - 500;
            $("html, body").animate({ scrollTop: y }, 10);
        }
        function CallSkillMiddle() {
            var offset = $("#divAddskill").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $("#divAddskill").position().top;
            $("html, body").animate({ scrollTop: $(window).height() + 2000 }, 10);
        }
        function CallEducationMiddle() {
            var offset = $("#txtEduDescription").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $(window).scrollTop();
            $("html, body").animate({ scrollTop: $(window).height() + 2000 }, 10);
        }
        function CallAchiveMiddle() {
            var offset = $("#divAchivement").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $("#divAchivement").position().top + 800;
            $("html, body").animate({ scrollTop: $(window).height() + 2000 }, 10);
        }
        function GetScreenCordinates(obj) {
            var p = {};
            p.x = obj.offsetLeft;
            p.y = obj.offsetTop;
            while (obj.offsetParent) {
                p.x = p.x + obj.offsetParent.offsetLeft;
                p.y = p.y + obj.offsetParent.offsetTop;
                if (obj == document.getElementsByTagName("body")[0]) {
                    break;
                }
                else {
                    obj = obj.offsetParent;
                }
            }
            return p;
        }
      //function callBodyDisable() {
      //    $("body").css("position", "fixed");
      //    $("body").css("overflow-y", "scroll");
      //}
      ////function callBodyEnable() {
      ////    $('#PopUpCropImage').css('display', 'none');
      ////    $('#lnkCancelProfile').css("box-shadow", "0px 0px 5px #BCBDCE");
      ////    $('.lnkProfileSaves').css("box-shadow", "0px 0px 5px #00B7E5");
      ////    $('#divEditProfile').css('display', 'none');
      ////    $("body").css("position", "static");
      ////    $("body").css("overflow-y", "auto");
      ////}
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var ir = 0;
            var RegId = '<%= Request.QueryString["RegId"]%>';
            if (RegId == '') {
                $(".endronsecssli").toggleClass('endronsecssli', 'endronsecsslit');
            } else {
                $(".endronsecsslit").toggleClass('endronsecsslit', 'endronsecssli');
            }
            $("ul.skillsetList").children("li").each(function () {
                $(this).children("div").addClass('divends');
                $(".endronsecssli").mouseover(function () {
                    $(this).children('.divends').children("a.endronsecss").css('display', 'block');
                });
                $(".endronsecssli").mouseout(function () {
                    $(this).children('.divends').children("a.endronsecss").css('display', 'none');
                });
            });
            $(document).on('click', '.mobile_tab_icon', function () {
                $('#homeId').slideToggle('slow');
            });





            $(document).on('click', '.tabsProfile li', function () {
                var textValue = ($(this).children("a").text());
                var image = ($(this).children("a").attr('class'));

                $('.tab_name').addClass(image);
                $('.tab_name').text(textValue);




            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                var RegId = '<%= Request.QueryString["RegId"]%>';
                if (RegId == '') {
                    $(".endronsecssli").toggleClass('endronsecssli', 'endronsecsslit');
                } else {
                    $(".endronsecsslit").toggleClass('endronsecsslit', 'endronsecssli');
                }

                $("ul.skillsetList").children("li").each(function () {
                    $(this).children("div").addClass('divends');
                    $(".endronsecssli").mouseover(function () {
                        $(this).children('.divends').children("a.endronsecss").css('display', 'block');
                    });
                    $(".endronsecssli").mouseout(function () {
                        $(this).children('.divends').children("a.endronsecss").css('display', 'none');
                    });
                });
            });
        });
    </script>
    <script type="text/javascript">
        var strSelTexts = '';
        $(document).ready(function () {
            $('ul.context li').click(function () {
                $(this).toggleClass('selectedcreateGroup graycreateGroup');
                if ($(this).hasClass("selectedcreateGroup")) {
                    $(this).children("#lnkCatName").removeAttr('style');
                    $(this).children(".unselectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
                } else {
                    $(this).children("#lnkCatName").removeAttr('style');
                    $(this).children(".selectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
                }
                AddSubjectCall($(this).children("#hdnSubCatId").val());
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('ul.context li').click(function () {
                    $(this).toggleClass('selectedcreateGroup graycreateGroup');
                    if ($(this).hasClass("selectedcreateGroup")) {
                        $(this).children("#lnkCatName").removeAttr('style');
                        $(this).children(".unselectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
                    } else {
                        $(this).children("#lnkCatName").removeAttr('style');
                        $(this).children(".selectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
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
        function CloseMsg() {
            $('#divSuccess').css("display", "none");
        }

        function MsgClick() {
            $('#divMessPopup').modal('show');
        }

        function AddfrndClick() {
            $('#divConnDisPopup').modal('show');
        }

        function Commentsdelete() {
            $('#divDeletesucess').modal('show');
        }

        function Commentsdelete1() {
            $('#divDeletesucess1').modal('show');
        }

        function docdelete() {
            //alert('indocdeletefunction');

            $('#divDeletesucess').css("display", "block");
            $('#AddWorkExp').css("display", "none");
        }
        function divCancels() {
            $('#hdnintPostId').val('');
            $('#hdnintPostceIdelet').val('');
            $('#hdnintacheIdelet').val('');
            $('#hdnintedueIdelet').val('');
            $('#hdnintdocIdelete').val('');
            $('#divDeletesucess').css("display", "none");
            $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 0px #00B7E5");
            $('#divDeletesucess1').css("display", "none");
            $('#lnkDeleteConfirm1').css("box-shadow", "0px 0px 0px #00B7E5");
        }
        function CallUserJSON() {
            $.ajax({
                url: 'Home.aspx/GetUserJSONdata',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (data) {
                    $('#divEditProfile').css('display', 'block');
                    var parsed = $.parseJSON(data.d);
                    $.each(parsed, function (i, jsondata) {
                        $('#txtName').val(jsondata.NAME);
                        $('#txtGender').val(jsondata.chrSex);
                        $('#txtLanguage').val(jsondata.vchrLanguages);
                        $('#txtEmailId').val(jsondata.vchrUserName);
                        $('#txtPhone').val(jsondata.intMobile);
                        //if (val(jsondata.intMobile) != null && val(jsondata.intMobile) != '' && val(jsondata.intMobile) != 0) {
                        //    // do something
                        //    $('#txtPhone').val(jsondata.intMobile);
                        //}
                        //else
                        //{
                        //    $('#txtPhone').val('');
                        //}
                    });
                }
            });
        }
        function getIEVersion() {
            var agent = navigator.userAgent;
            var reg = /MSIE\s?(\d+)(?:\.(\d+))?/i;
            var matches = agent.match(reg);
            if (matches != null) {
                return { major: matches[1], minor: matches[2] };
            }
            return { major: "-1", minor: "-1" };
        }
        function callposts() {
            var ie_version = getIEVersion();
            var is_ie10 = ie_version.major == 10;
            if (is_ie10 != false) {
            }
        }
        var ie_version = getIEVersion();
        var is_ie10 = ie_version.major == 10;
        if (is_ie10 != false) {
            $(document).on("change", "#FileUplogo", function (event) {
                var tmppath = URL.createObjectURL(event.target.files[0]);
                var ext = $('#FileUplogo').val().split('.').pop().toLowerCase();
                if (ext != '') {
                    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp']) == -1) {
                        if (ext == 'pdf' || ext == 'xlsx' || ext == 'txt' || ext == 'doc' || ext == 'docx' || ext == 'xls' || ext == 'odt' || ext == 'ods') {
                            alert('Please select image or video.');
                        } else {
                            $("#Mediavideo").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
                            $("#imgselect").css("display", "none");
                            $("#imgBtnDelete").css("display", "block");
                        }
                    } else {
                        $("#imgselect").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
                        $("#Mediavideo").css("display", "none");
                        $("#imgBtnDelete").css("display", "block");
                    }
                } else {
                    $("#imgselect").css("display", "none");
                    $("#Mediavideo").css("display", "none");
                    $("#imgBtnDelete").css("display", "none");
                }
            });
            //---------------------------------------------------------
            $('#lnkPostUpdate').on("click", function (event) {
                if ('#divPostimage'.css('display') == 'block') {

                }
                if (document.getElementById("txtPostUpdate").value == "" || document.getElementById("txtPostUpdate").value == "Share an update...") {
                    document.getElementById('lblPostMsg').style.display = 'block';
                }
                else {

                    var fileUpload = $("#FileUplogo").get(0);
                    var files = fileUpload.files;
                    var data = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        data.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        type: "POST",
                        url: "handler/FileUploadHandlerHome.ashx",
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                            if (result == 'File format not supported.' || result == 'File size should be less than or equal to 3 MB.' || result == 'Please choose a video file less than 12 MB.' || result == 'please') {
                                if (result != "please") {
                                    $("#hdnErrorMsg").val(result)
                                }

                            }
                            else {
                                var v = result.split(":");
                                document.getElementById("hdnPhoto").value = v[0];
                                document.getElementById("hdnDocName").value = v[1];
                            }
                            $("#FileUplogo").val('');
                            document.getElementById("lnkDummyPost").click();
                        },
                        error: function () {
                            alert("There was error uploading files!");
                        }
                    });
                }
            });
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("span.ediDel").click(function () {
                $('#hdnintPostId').val($(this).children('#hdnintPostIdelet').val());
                $('#hdnstrPostDescriptiondele').val($(this).children('#hdnstrPostDescriptiondel').val());
            });

            $("#testDelete").click(function () {
                $('#hdnintPostceIdelet').val($(this).children('#hdnintPostcIdelet').val());
                $('#hdnstrPostDescriptioncedel').val($(this).children('#hdnstrPostDescriptioncdel').val());
            });

            $("span.ediDeldoc").click(function () {
                $('#hdnintdocIdelete').val($(this).children('#hdnintdocIdelet').val());
                $('#hdnstrdocDescriptiondele').val($(this).children('#hdnstrdocDescriptiondel').val());
                $('#hdnfilestrFilePathe').val($(this).children('#hdnfilestrFilePath').val());
            });

            $("span.ediDelwork").click(function () {
                $('#hdnintworkeIdelet').val($(this).children('#hdnintworkIdelet').val());
                $('#hdnstrworkeDescriptiondel').val($(this).children('#hdnstrworkDescriptiondel').val());
            });

            $("span.ediDeledu").click(function () {
                $('#hdnintedueIdelet').val($(this).children('#hdninteduIdelet').val());
                $('#hdnstredueDescriptiondel').val($(this).children('#hdnstreduDescriptiondel').val());
            });

            $("span.ediDelach").click(function () {
                $('#hdnintacheIdelet').val($(this).children('#hdnintachIdelet').val());
                $('#hdnstracheDescriptiondel').val($(this).children('#hdnstrachDescriptiondel').val());
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("span.ediDel").click(function () {
                    $('#hdnintPostId').val($(this).children('#hdnintPostIdelet').val());
                    $('#hdnstrPostDescriptiondele').val($(this).children('#hdnstrPostDescriptiondel').val());
                });

                $("#testDelete").click(function () {
                    $('#hdnintPostceIdelet').val($(this).children('#hdnintPostcIdelet').val());
                    $('#hdnstrPostDescriptioncedel').val($(this).children('#hdnstrPostDescriptioncdel').val());
                });

                $("span.ediDeldoc").click(function () {
                    $('#hdnintdocIdelete').val($(this).children('#hdnintdocIdelet').val());
                    $('#hdnstrdocDescriptiondele').val($(this).children('#hdnstrdocDescriptiondel').val());
                    $('#hdnfilestrFilePathe').val($(this).children('#hdnfilestrFilePath').val());
                });

                $("span.ediDelwork").click(function () {
                    $('#hdnintworkeIdelet').val($(this).children('#hdnintworkIdelet').val());
                    $('#hdnstrworkeDescriptiondel').val($(this).children('#hdnstrworkDescriptiondel').val());
                });

                $("span.ediDeledu").click(function () {
                    $('#hdnintedueIdelet').val($(this).children('#hdninteduIdelet').val());
                    $('#hdnstredueDescriptiondel').val($(this).children('#hdnstreduDescriptiondel').val());
                });

                $("span.ediDelach").click(function () {
                    $('#hdnintacheIdelet').val($(this).children('#hdnintachIdelet').val());
                    $('#hdnstracheDescriptiondel').val($(this).children('#hdnstrachDescriptiondel').val());
                });
            });
        });
    </script>
    <script type="text/javascript">
        ddsmoothmenu.init({
            mainmenuid: "smoothmenu1", //menu DIV id
            orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
            className: 'ddsmoothmenu', //class added to menu's outer DIV
            contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
        })
        $(document).ready(function () {
            $("#txtPhone").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $(".photoIcon").mouseover(function () {
                $("#imgCamera").css('display', 'block');
            });
            $(".photoIcon").mouseout(function () {
                $("#imgCamera").css('display', 'none');
            });
            // we changed here live to on function
            //.on(eventType, selector, function)"#FileUplogo"
            $(document).on("change", "#FileUplogo", function (event) {
                //alert("Working");
                $("#lblVideomsg").css("display", "none");
                $(".divProgress").css("display", "none");
                var URL = window.URL || window.webkitURL || window.mozURL || window.msURL || window.oURL;
                var tmppath = URL.createObjectURL(event.target.files[0]);
                var size = event.target.files[0].size;
                var imgFilesTypes = ['gif', 'png', 'jpg', 'jpeg', 'bmp', 'org'];
                var videoFileTypes = ['wmv', 'mp4', 'mpg', 'mpeg'];
                var ext = $('#FileUplogo').val().split('.').pop().toLowerCase();
                if (ext != '') {
                    if ($.inArray(ext, imgFilesTypes) == -1) {
                        // Not an image check for video
                        if ($.inArray(ext, videoFileTypes) == -1) {
                            showSuccessPopup("Error", "You can only upload valid image and video files.");
                            $('#FileUplogo').val('');
                        } else {
                            // This is a video - check for size
                            if (size > 1024 * 1024 * 12) {
                                showSuccessPopup("Error", "You can only upload videos upto 12 MB.");
                                $('#FileUplogo').val('');
                                return;
                            }
                            if (navigator.userAgent.indexOf('Safari') != -1 && navigator.userAgent.indexOf('Chrome') == -1) {
                                var pathsimg;
                                //alert('Its Safari');
                                var fileUpload = $("#FileUplogo").get(0);
                                var files = fileUpload.files;
                                var data = new FormData();
                                for (var i = 0; i < files.length; i++) {
                                    data.append(files[i].name, files[i]);
                                }
                                $.ajax({
                                    type: "POST",
                                    url: "handler/FileUploadHandlerHome.ashx",
                                    contentType: false,
                                    processData: false,
                                    data: data,
                                    success: function (result) {
                                        if (result == 'File format not supported.' || result == 'File size should be less than or equal to 3 MB.' || result == 'Please choose a video file less than 12 MB.' || result == 'please') {
                                        }
                                        else {
                                            var v = result.split(":");
                                            pathsimg = "VideoFiles/" + v[0];
                                            $("#Mediavideo").fadeIn("fast").attr('src', pathsimg);
                                        }
                                    },
                                    error: function () {
                                        alert("There was error uploading files!");
                                    }
                                });

                                $("#Mediavideo").fadeIn("fast").attr('src', pathsimg);
                                $("#imgselect").css("display", "none");
                                $("#imgBtnDelete").css("display", "block");
                                $("#lblVideomsg").css("display", "block");
                            } else {
                                $("#Mediavideo").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
                                $("#imgselect").css("display", "none");
                                $("#imgBtnDelete").css("display", "block");
                            }
                        }
                    } else {
                        if (size > 1024 * 1024 * 3) {
                            showSuccessPopup("Error", "You can only upload images upto 3 MB.");
                            $('#FileUplogo').val('');
                            return;
                        }
                        if (navigator.userAgent.indexOf('Safari') != -1 && navigator.userAgent.indexOf('Chrome') == -1) {
                            var pathsimg;
                            var fileUpload = $("#FileUplogo").get(0);
                            var files = fileUpload.files;
                            var data = new FormData();
                            for (var i = 0; i < files.length; i++) {
                                data.append(files[i].name, files[i]);
                            }
                            $.ajax({
                                type: "POST",
                                url: "handler/FileUploadHandlerHome.ashx",
                                contentType: false,
                                processData: false,
                                data: data,
                                success: function (result) {
                                    if (result == 'File format not supported.' || result == 'File size should be less than or equal to 3 MB.' || result == 'Please choose a video file less than 12 MB.' || result == 'please') {
                                    }
                                    else {
                                        var v = result.split(":");
                                        pathsimg = "UploadedPhoto/" + v[0];
                                        $("#imgselect").attr('src', pathsimg);
                                    }
                                },
                                error: function () {
                                    alert("There was error uploading files!");
                                }
                            });

                            $("#imgselect").fadeIn("fast").attr('src', pathsimg);
                            $("#Mediavideo").css("display", "none");
                            $("#imgBtnDelete").css("display", "block");
                        } else {
                            $("#imgselect").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
                            $("#Mediavideo").css("display", "none");
                            $("#imgBtnDelete").css("display", "block");
                        }
                    }
                } else {
                    $("#imgselect").css("display", "none");
                    $("#Mediavideo").css("display", "none");
                    $("#imgBtnDelete").css("display", "none");
                }
            });
            $("#uploadDoc").change(function (event) {
                var tmppath = URL.createObjectURL(event.target.files[0]);
                $("#lblfilenamee").text($("#uploadDoc").val().split('\\').pop());
                $("#lblfilenamee").removeClass("RedErrormsg")
            });

            $(".showMouseOver").mouseover(function () {
                $(this).parent().children(".imageRolloverBg").css('display', 'block');
            });

            $(".showMouseOver").mouseout(function () {
                $(this).parent().children(".imageRolloverBg").css('display', 'none');
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#lnkPostUpdate').click(function () {
                    $(".divProgress").css("display", "none");
                    $('#lnkPostUpdate').css("background", "#19A0AA");
                    $('#lnkPostUpdate').css("box-shadow", "0px 0px 5px #00B7E5");
                    if ($('#txtPostUpdate').val().trim() == "" || $('#txtPostUpdate').val().trim() == "Share an update...") {
                        document.getElementById('RequiredFieldValidator2').style.display = "block";
                        setTimeout(
                            function () {
                                $('#lnkPostUpdate').css("background", "#0096a1");
                                $('#lnkPostUpdate').css("box-shadow", "0px 0px 0px #00B7E5");
                            }, 1000);
                    }
                    else {
                        $('#divPostimage').css("display", "block");
                        $('#lnkPostUpdate').addClass("disabled");
                        $('#imgBtnDelete').css("margin", "-41px -2px -2px 1px");
                        $('.UploadFilesHomeHome').css("cursor", "progress");
                        $('#lnkPostUpdate').css("cursor", "progress");
                        var fileUpload = $("#FileUplogo").get(0);
                        var files = fileUpload.files;
                        var data = new FormData();
                        for (var i = 0; i < files.length; i++) {
                            data.append(files[i].name, files[i]);
                        }
                        if ($("#FileUplogo").val() == "") {
                            data = null;
                        }
                        $.ajax({
                            type: "POST",
                            url: "handler/FileUploadHandlerHome.ashx",
                            contentType: false,
                            processData: false,
                            data: data,
                            success: function (result) {
                                if (result == 'File format not supported.' || result == 'File size should be less than or equal to 3 MB.' || result == 'Please choose a video file less than 12 MB.' || result == 'please') {
                                    if (result != "please") {
                                        $("#hdnErrorMsg").val(result)
                                    }
                                    $("#hdnDocName").val('');
                                    $("#hdnPhoto").val('');
                                }
                                else {
                                    var v = result.split(":");
                                    $("#hdnPhoto").val(v[0]);
                                    $("#hdnDocName").val(v[1]);
                                }
                                $("#FileUplogo").val('');
                                document.getElementById('lnkDummyPost').click();
                            },
                            error: function () {
                                alert("There was error uploading files!");
                                document.getElementById('lnkDummyPost').click();
                            }
                        });
                    }
                });
            });
        })
        $(document).ready(function () {
            $('#lnkPostUpdate').click(function () {
                $(".divProgress").css("display", "none");
                $('#lnkPostUpdate').css("background", "#19A0AA");
                $('#lnkPostUpdate').css("box-shadow", "0px 0px 5px #00B7E5");
                if ($('#txtPostUpdate').val().trim() == "" || $('#txtPostUpdate').val().trim() == "Share an update...") {
                    document.getElementById('RequiredFieldValidator2').style.display = "block";
                    setTimeout(
                        function () {
                            $('#lnkPostUpdate').css("background", "#0096a1");
                            $('#lnkPostUpdate').css("box-shadow", "0px 0px 0px #00B7E5");
                        }, 1000);
                }
                else {
                    $('#divPostimage').css("display", "block");
                    $('#lnkPostUpdate').addClass("disabled");
                    $('#imgBtnDelete').css("margin", "-41px -2px -2px 1px");
                    $('.UploadFilesHomeHome').css("cursor", "progress");
                    $('#lnkPostUpdate').css("cursor", "progress");
                    var fileUpload = $("#FileUplogo").get(0);
                    var files = fileUpload.files;
                    var data = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        data.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        type: "POST",
                        url: "handler/FileUploadHandlerHome.ashx",
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                            if (result == 'File format not supported.' || result == 'File size should be less than or equal to 3 MB.' || result == 'Please choose a video file less than 12 MB.' || result == 'please') {
                                if (result != "please") {
                                    $("#hdnErrorMsg").val(result)
                                }
                                $("#hdnDocName").val('');
                                $("#hdnPhoto").val('');
                            }
                            else {
                                var v = result.split(":");
                                $("#hdnPhoto").val(v[0]);
                                $("#hdnDocName").val(v[1]);
                            }
                            $("#FileUplogo").val('');
                            document.getElementById("lnkDummyPost").click();
                        },
                        error: function () {
                            alert("There was error uploading files!");
                            document.getElementById('lnkDummyPost').click();
                        }
                    });
                }
            });
        })
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById('pLoadMore').style.display = 'none';
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                ddsmoothmenu.init({
                    mainmenuid: "smoothmenu1", //menu DIV id
                    orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
                    className: 'ddsmoothmenu', //class added to menu's outer DIV
                    contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
                })
                var ir = 0;
                $(".endronsecssimg").mouseover(function () {
                    $(this).parent().children(".endronsecss").css('display', 'block');
                });

                $(".endronsecss").mouseout(function () {
                    $(this).parent().children(".endronsecss").css('display', 'none');
                });

                $(".endronsecssimg").mouseout(function () {
                    $(this).parent().children(".endronsecss").css('display', 'none');
                });
                $(".scroll").click(function (event) {
                    $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 500);
                });
                $(".showMouseOver").mouseover(function () {
                    $(this).parent().children(".imageRolloverBg").css('display', 'block');
                });
                $(".showMouseOver").mouseout(function () {
                    $(this).parent().children(".imageRolloverBg").css('display', 'none');
                });

                $(".photoIcon").mouseover(function () {
                    $("#imgCamera").css('display', 'block');
                });
                $(".photoIcon").mouseout(function () {
                    $("#imgCamera").css('display', 'none');
                });
                $("#uploadDoc").change(function (event) {
                    var tmppath = URL.createObjectURL(event.target.files[0]);
                    $("#lblfilenamee").text($("#uploadDoc").val().split('\\').pop());
                    var fileUpload = $("#uploadDoc").get(0);
                    var files = fileUpload.files;
                    var data = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        data.append(files[i].name, files[i]);
                    }
                    $.ajax({
                        type: "POST",
                        url: "handler/FileUploadHandler.ashx",
                        contentType: false,
                        processData: false,
                        data: data,
                        success: function (result) {
                            if (result == "File format not supported." || result == "File size should be less than or equal to 3 MB.") {
                                $("#hdnErrorMsg").val(result)
                            }
                            else {
                                var v = result.split(":");
                                $("#hdnFilePath").val(v[0]);
                                $("#hdnDocName").val(v[1]);
                            }
                        },
                        error: function () {
                            alert("There was error uploading files!");
                        }
                    });
                });
                $("#imgGroup").click(function () {
                    $("#divfrdgrp").removeClass("fGroupBox frd").addClass("fGroupBox grp");
                    $("#divgroupSection").show();
                    $("#divFriendSection").hide();
                    $('#imgFriend').addClass('active');
                    return false;
                });
                $("#imgFriend").click(function () {
                    $("#divfrdgrp").removeClass("fGroupBox grp").addClass("fGroupBox frd");
                    $("#divFriendSection").show();
                    $("#divgroupSection").hide();
                    $('#imgFriend').removeClass('active');
                    return false;
                });
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById('pLoadMore').style.display = 'none';
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).scroll(function () {
                    if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                        $(".divProgress").css("display", "none");
                        //document.getElementById('pLoadMore').style.display = 'block';
                        if ($("#hdnTabIds").val() == 0) {
                            var v = $("#lblNoMoreRslt").text();
                            var maxCount = $("#hdnMaxcount").val();
                            if (maxCount <= 10) {
                                $("#lblNoMoreRslt").css("display", "none");
                            } else {
                                if (v != "No more results available...") {
                                    var elm = '#imgLoadMore1';
                                    $(elm).click();
                                } else {
                                    document.getElementById('pLoadMore').style.display = 'none';
                                }
                            }
                        }
                    }
                });
                if ($("#lblNotifyCount").text() == '0') {
                    document.getElementById("divNotification1").style.display = "none";
                }
                if ($("#lblInboxCount").text() == '0') {
                    document.getElementById("divInbox").style.display = "none";
                }
                $("#txtPhone").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
                $("#txtFromYear").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
                $("#txtToYear").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
                $("#txtEducationFromdt").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
                $("#txtEducationTodt").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        return false;
                    }
                });
            });
            $('#lnkUpload').click(function (event) {
                $('#FileUplogo').click();
            });
            $('#FileUplogo').change(function (click) {
                $('#lblfilename').val(this.value);
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).scroll(function () {
                if ($(document).height() <= $(window).scrollTop() + $(document).height()) {
                    $(".divProgress").css("display", "none");
                    document.getElementById('pLoadMore').style.display = 'block';
                    if ($("#hdnTabIds").val() == 0) {
                        var v = $("#lblNoMoreRslt").text();
                        var maxCount = $("#hdnMaxcount").val();
                        if (maxCount <= 10) {
                            document.getElementById('pLoadMore').style.display = 'none';
                            $("#lblNoMoreRslt").css("display", "none");
                        } else {
                            if (v != "No more results available...") {
                                document.getElementById('imgLoadMore1').click();
                            } else {
                                document.getElementById('pLoadMore').style.display = 'none';
                            }
                        }
                    }
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#lblNotifyCount").text() == '0') {
                document.getElementById("divNotification1").style.display = "none";
            }
            if ($("#lblInboxCount").text() == '0') {
                document.getElementById("divInbox").style.display = "none";
            }
            $("#imgGroup").click(function () {
                $("#divfrdgrp").removeClass("fGroupBox frd").addClass("fGroupBox grp");
                $("#divgroupSection").show();
                $("#divFriendSection").hide();
                return false;
            });
            $("#imgFriend").click(function () {
                $("#divfrdgrp").removeClass("fGroupBox grp").addClass("fGroupBox frd");
                $("#divFriendSection").show();
                $("#divgroupSection").hide();
                return false;
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#rdDownloadYes').click(function () {
                $('#rdDownloadYes').prop('checked', true);
                $('#rdDownloadNo').prop('checked', false);
            });
            $('#rdDownloadNo').click(function () {
                $('#rdDownloadYes').prop('checked', false);
                $('#rdDownloadNo').prop('checked', true);
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //called when key is pressed in textbox
            $("#txtFromYear").keypress(function (e) {
                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtToYear").keypress(function (e) {
                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            //called when key is pressed in textbox
            $("#txtEducationFromdt").keypress(function (e) {
                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtEducationTodt").keypress(function (e) {
                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        });
    </script>
    <script type="text/javascript">
        function CallExpNumaric() {
            //called when key is pressed in textbox
            $("#txtFromYear").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtToYear").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        }
    </script>
    <script type="text/javascript">
        function callAcivemet() {
            $("#divAchivement").show();
            CallAchiveMiddle();
        }
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("#LinkButton1").click(function () {
                    $("#AddWorkExp").hide();
                    ClearExpControls();
                });
                $("#lnkAddskill").click(function () {
                    $("#divAddskill").show();
                    CallSkillMiddle();
                });
                $("#lnkCancelEducation").click(function () {
                    $("#divEducation").hide();
                    ClearEducation();
                });
                $("#LinkButton4").click(function () {
                    $("#divAchivement").hide();
                    ClearAchieveMents();
                });
            });
        });
        function ClearExpControls() {
            $("#txtInstituteName").val('');
            $("#txtDegree").val('');
            $("#fromMonth").val('');
            $("#txtFromYear").val('');
            $("#toMonth").val('');
            $("#txtToYear").val('');
            $("#ctl00_ContentPlaceHolder1_txtDescription").val('');
            $("#chkAtPresent").attr('checked', false);
        }
        function ClearEducation() {
            $("#txtInstitute").val('');
            $("#txtTitle").val('');
            $("#ddlFromMonth").val('');
            $("#txtEducationFromdt").val('');
            $("#ddlToMonth").val('');
            $("#txtEducationTodt").val('');
            $("#txtEduDescription").val('');
            $("#chkEducation").attr('checked', false);
        }
        function ClearAchieveMents() {
            $("#txtCompitition").val('');
            $("#ctl00_ContentPlaceHolder1_txtAdditionalAwrd").val('');
            $("#ctl00_ContentPlaceHolder1_txtAchivDescription").val('');
            $("#ddlMilestone").val('0');
            $("#ddlPosition").val('0');
        }
    </script>
    <script type="text/javascript">
        function removeimages() {
            $("#Mediavideo").fadeIn("fast").attr('src', '');
            $("#Mediavideo").css("display", "none");
            $("#imgselect").fadeIn("fast").attr('src', '');
            $("#imgselect").css("display", "none");
            $('#FileUplogo').val('');
            $("#imgBtnDelete").css("display", "none");
            $("#lblVideomsg").css("display", "none");
        }
        function fncsave() {
            if ($("#imgPrivate").attr('src') == "images/unchk1.png") {
                $("#imgPrivate").attr('src', 'images/chk1.png')

                if ($("#hdnimgPrivate").val() == 0) {
                    $("#hdnimgPrivate").val('1');
                }

            } else {
                if ($("#hdnimgPrivate").val() == 1) {
                    $("#hdnimgPrivate").val('0');
                }

                $("#imgPrivate").attr('src', 'images/unchk1.png')
            }
        }
        function fncsavedow() {
            if ($("#imgDownload").attr('src') == "images/unchk1.png") {
                $("#imgDownload").attr('src', 'images/chk1.png')
                if ($("#hdnimgDownload").val() == 0) {
                    $("#hdnimgDownload").val('1');
                }

            } else {
                $("#imgDownload").attr('src', 'images/unchk1.png')

                if ($("#hdnimgDownload").val() == 1) {
                    $("#hdnimgDownload").val('0');
                }
            }

        }
        function groupConnChange() {
            $("#imgGroup").click(function () {
                $("#divfrdgrp").removeClass("fGroupBox frd").addClass("fGroupBox grp");
                $("#divgroupSection").show();
                $("#divFriendSection").hide();

                return false;
            });
            $("#imgFriend").click(function () {
                $("#divfrdgrp").removeClass("fGroupBox grp").addClass("fGroupBox frd");
                $("#divFriendSection").show();
                $("#divgroupSection").hide();
                return false;
            });
            if ($("#lblNotifyCount").text() == '0') {
                document.getElementById("divNotification1").style.display = "none";
            }
            if ($("#lblInboxCount").text() == '0') {
                document.getElementById("divInbox").style.display = "none";
            }
        }
        function callDocUpload() {
            $("#uploadDoc").change(function (event) {
                var tmppath = URL.createObjectURL(event.target.files[0]);
                $("#lblfilenamee").text($("#uploadDoc").val().split('\\').pop());
            });
        }
    </script>
    <asp:HiddenField ID="hdnTabIds" Value="0" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnPhoto" Value="" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnDocName" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnErrorMsg" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnImageProfile" runat="server" ClientIDMode="Static" />
    <script type="text/javascript">
        $(".viewAllComments").click(function () {
            $("#lstChild").slideToggle("slow");
            $(".viewAllComments").toggle();
        });
        function CancelFrndPopUp() {
            document.getElementById("divConnDisPopup").style.display = 'none';
            //document.getElementById("divMessPopup").style.display = 'none';
            document.getElementById("divSuccess").style.display = 'none';
            //hideLoader1();
            return false;
        }
        function CallConnDissCon() {
            $('#lnkConnDisconn').css("box-shadow", "0px 0px 5px #00B7E5");
        }
    </script>
    <script type="text/jscript">
        function Cancel() {
            document.getElementById("PopUpCropImage").style.display = 'none';
            document.getElementById("divCancelPopup").style.display = 'none';
            return false;
        }
        function CallMessageNotification() {
            if ($("#lblNotifyCount").text() == '0') {
                document.getElementById("divNotification1").style.display = "none";
            }
            if ($("#lblInboxCount").text() == '0') {
                document.getElementById("divInbox").style.display = "none";
            }
        }
    </script>
    <script type="text/javascript">
        function ToogleComment(ctl) {
            $(ctl).find("#commentTxt1").slideToggle("slow");
            $(ctl).find("#commnetBtn1 img").toggle();
        }
    </script>
    <script type="text/javascript">
        function ShowLoading(id) {
            location.href = '#' + id;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.innerTabHome').click(function () {
                //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
                $("#imgUser").attr("src", $("#hdnImageProfile").val());
                $("#PopUpCropImage").css("display", "none");
                $('#hdnTabIds').val(0);
            });
            $('.innerTabDoc').click(function () {
                //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
                $("#imgUser").attr("src", $("#hdnImageProfile").val());
                $("#PopUpCropImage").css("display", "none");
                $('#hdnTabIds').val(1);
                $('#divDocumentUplaod').css('display', 'none');
            });
            $('.innerWrkex').click(function () {
                //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
                $("#imgUser").attr("src", $("#hdnImageProfile").val());
                $("#PopUpCropImage").css("display", "none");
                $('#hdnTabIds').val(1);
            });
            $('.innerEdu').click(function () {
                //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
                $("#imgUser").attr("src", $("#hdnImageProfile").val());
                $("#PopUpCropImage").css("display", "none");
                $('#hdnTabIds').val(1);
            });
            $('.innerAch').click(function () {
                //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
                $("#imgUser").attr("src", $("#hdnImageProfile").val());
                $("#PopUpCropImage").css("display", "none");
                $('#hdnTabIds').val(1);
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.innerTabHome').click(function () {
                    //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
                    $("#imgUser").attr("src", $("#hdnImageProfile").val());
                    $("#PopUpCropImage").css("display", "none");
                    $('#hdnTabIds').val(0);
                });
                $('.innerTabDoc').click(function () {
                    //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
                    $("#imgUser").attr("src", $("#hdnImageProfile").val());
                    $("#PopUpCropImage").css("display", "none");
                    $('#hdnTabIds').val(1);
                    $('#divDocumentUplaod').css('display', 'none');
                });
                $('.innerWrkex').click(function () {
                    //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
                    $("#imgUser").attr("src", $("#hdnImageProfile").val());
                    $("#PopUpCropImage").css("display", "none");
                    $('#hdnTabIds').val(1);
                });
                $('.innerEdu').click(function () {
                    //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
                    $("#imgUser").attr("src", $("#hdnImageProfile").val());
                    $("#PopUpCropImage").css("display", "none");
                    $('#hdnTabIds').val(1);
                });
                $('.innerAch').click(function () {
                    //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
                    $("#imgUser").attr("src", $("#hdnImageProfile").val());
                    $("#PopUpCropImage").css("display", "none");
                    $('#hdnTabIds').val(1);
                });
            });
        });
        function callinnerGroupBox() {
            $('.innerGroupBox').css('height', 'auto');
        }
        $(document).ready(function () {
            $("#homeId li:first").addClass('active');
        });
        function CallDocLI() {
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
            //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
        }
        function CallWorkLI() {
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
            //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
        }
        function CallEduLI() {
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
            //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
            $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 0px #00B7E5");
        }
        function CallAchLI() {
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
            //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
        }
        function CallHomeLI() {
            $('.UploadFilesHomeHome').css("cursor", "pointer");
            $('#lnkPostUpdate').css("cursor", "pointer");
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
            //$("#imgUser1").attr("src", $("#hdnImageProfile").val());
        }
        function OpenDocentry() {
            $('#divDocumentUplaod').css('display', 'block');
        }
        function ShowAddUserProcess() {
            //debugger;
            $('#divAddUser').css('display', 'block');
            $('#lnkAddFriend').css('cursor', 'wait');
        }
        function HideAddUserProcess() {
            $('#divAddUser').css('display', 'none');
            $('#lnkAddFriend').css('cursor', 'pointer');
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.divtabLikespost').click(function (e) {
                e.preventDefault();
                var LikepostCount = $(this).find('#lnkLikePost').text();
                if ($(this).find('#hdnPostLikeUserId').val() != $('#hdnUserID').val()) {
                    var d = parseInt(LikepostCount) + parseInt(1);
                    $(this).find('#lnkLikePost').text(d);
                    $(this).find('#hdnPostLikeUserId').val($('#hdnUserID').val())
                }
                var UserId = $('#hdnUserID').val();
                var hdnPostUpdateId = $(this).find('#hdnPostUpdateId').val();
                $.ajax({
                    url: 'Home.aspx/UpdateUserStatusLike',
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{UserId:'" + UserId + "',hdnPostUpdateId:'" + hdnPostUpdateId + "'}",
                    async: true,
                    success: function (data) {
                    }
                });
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.divtabLikespost').click(function (e) {
                    e.preventDefault();
                    var LikepostCount = $(this).find('#lnkLikePost').text();
                    if ($(this).find('#hdnPostLikeUserId').val() != $('#hdnUserID').val()) {
                        var d = parseInt(LikepostCount) + parseInt(1);
                        $(this).find('#lnkLikePost').text(d);
                        $(this).find('#hdnPostLikeUserId').val($('#hdnUserID').val())
                    }
                    var UserId = $('#hdnUserID').val()
                    var hdnPostUpdateId = $(this).find('#hdnPostUpdateId').val();
                    $.ajax({
                        url: 'Home.aspx/UpdateUserStatusLike',
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: "{UserId:'" + UserId + "',hdnPostUpdateId:'" + hdnPostUpdateId + "'}",
                        async: true,
                        success: function (data) {
                        }
                    });
                });
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.divtabLikespostcomm').click(function (e) {
                e.preventDefault();
                var LikepostCount = $(this).find('#lnkLikeComment').text();
                if ($(this).find('#hdnCommentLikeUserId').val() != $('#hdnUserID').val()) {
                    var d = parseInt(LikepostCount) + parseInt(1);
                    $(this).find('#lnkLikeComment').text(d)
                    $(this).find('#hdnCommentLikeUserId').val($('#hdnUserID').val())
                }
                var UserId = $('#hdnUserID').val()
                var hdnCommentId = $(this).find('#hdnCommentId').val();
                $.ajax({
                    url: 'Home.aspx/UpdateUserStatusCommLike',
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{UserId:'" + UserId + "',hdnCommentId:'" + hdnCommentId + "'}",
                    async: true,
                    success: function (data) {
                        //                        var msg = eval('(' + data.d + ')');
                        //                        if (msg[0].Action != 0) {
                        //                        }
                    }
                });
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.divtabLikespostcomm').click(function (e) {
                    e.preventDefault();
                    var LikepostCount = $(this).find('#lnkLikeComment').text();
                    if ($(this).find('#hdnCommentLikeUserId').val() != $('#hdnUserID').val()) {
                        var d = parseInt(LikepostCount) + parseInt(1);
                        $(this).find('#lnkLikeComment').text(d)
                        $(this).find('#hdnCommentLikeUserId').val($('#hdnUserID').val())
                    }
                    var UserId = $('#hdnUserID').val()
                    var hdnCommentId = $(this).find('#hdnCommentId').val();
                    $.ajax({
                        url: 'Home.aspx/UpdateUserStatusCommLike',
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: "{UserId:'" + UserId + "',hdnCommentId:'" + hdnCommentId + "'}",
                        async: true,
                        success: function (data) {
                        }
                    });
                });
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#lnkCancelProfilediv').click(function (e) {
                e.preventDefault();
                // $('#lnkCancelProfilediv').css("background", "#0096a1");
                // $('#lnkCancelProfilediv').css("box-shadow", "0px 0px 0px #00B7E5");
                var UserId = $('#hdnUserID').val()
                $.ajax({
                    url: 'Home.aspx/LoadProfileImage',
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{UserId:'" + UserId + "'}",
                    async: true,
                    success: function (data) {
                        var msg = eval('(' + data.d + ')');
                        $("#PopUpCropImage").css("display", "none");
                        $("#imgUser").attr("src", msg[0].path);
                        //$("#imgUser1").attr("src", msg[0].path);
                        $("#imgProfilePic").attr("src", msg[0].path);
                        $("#hdnImageProfile").val(msg[0].path);
                    }
                });
            });
            $('.UploadFilesHomeHome').click(function (e) {
                $('.UploadFilesHomeHome').css("box-shadow", "0px 0px 5px #BCBDCE");
            });
        });
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#lnkCancelProfilediv').click(function (e) {
                    e.preventDefault();
                    $('#lnkCancelProfilediv').css("background", "#0096a1");
                    $('#lnkCancelProfilediv').css("box-shadow", "0px 0px 0px #00B7E5");
                    var UserId = $('#hdnUserID').val()
                    $.ajax({
                        url: 'Home.aspx/LoadProfileImage',
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: "{UserId:'" + UserId + "'}",
                        async: true,
                        success: function (data) {
                            $("#PopUpCropImage").css("display", "none");
                            var msg = eval('(' + data.d + ')');
                            $("#imgUser").attr("src", msg[0].path);
                            //$("#imgUser1").attr("src", msg[0].path);
                            $("#imgProfilePic").attr("src", msg[0].path);
                            $("#hdnImageProfile").val(msg[0].path);
                        }
                    });
                });
            });
        });
        function CallCameraload() {
            $(".divProgress").css("display", "none");
            $("#imgCamera").css("display", "none");
            $("#divProilePic").css("display", "block");
        }
    </script>
    <script type="text/javascript">
        function callDocSave() {
            $('#LinkSave').css("box-shadow", "0px 0px 5px #00B7E5");
            $('#LinkSave').css("background", "#00A5AA");
            if ($('#txtDocTitle').text() == '') {
                setTimeout(
                    function () {
                        // $('#LinkSave').css("background", "#0096a1");
                        // $('#LinkSave').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
            if ($('#lblfilenamee').text() == 'Please select file to upload.') {
                $('#LinkSave').css("box-shadow", "0px 0px 5px #00B7E5");
                $('#LinkSave').css("background", "#00A5AA");
                setTimeout(
                    function () {
                        // $('#LinkSave').css("background", "#0096a1");
                        // $('#LinkSave').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
        }
        function callDoccancel() {
            $('#lnkCancelDoc').css("background", "#D0D0D0");
            $('#lnkCancelDoc').css("border", "2px solid #BCBDCE");
        }
        function callSaveExp() {
            // $('#lnlSaveExp').css("background", "#00A5AA");
            // $('#lnlSaveExp').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtInstituteName').text() == '' || $('#txtDegree').text() == '' || $('#fromMonth').text() == 'Month'
                || $('#txtFromYear').text() == 'Year' || $('#toMonth').text() == 'Month' || $('#txtToYear').text() == 'Year') {
                setTimeout(
                    function () {
                        // $('#lnlSaveExp').css("background", "#0096a1");
                        // $('#lnlSaveExp').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
        }
        function CallAddSkill() {
            $("#PopUpCropImage").css("display", "none");
            // $('#btnAreaExpSave').css("background", "#00A5AA");
            // $('#btnAreaExpSave').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#lblareaskill').text() == 'Please enter area of expertise.') {
                setTimeout(
                    function () {
                        // $('#btnAreaExpSave').css("background", "#0096a1");
                        // $('#btnAreaExpSave').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
        }
        function callCancelExp() {
            $('#LinkButton1').css("background", "#D0D0D0");
            $('#LinkButton1').css("border", "2px solid #BCBDCE");
        }
        function callSaveSkill() {
            // $('#lnkSaveSkill').css("background", "#00A5AA");
            // $('#lnkSaveSkill').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#lblareaskill').text() == 'Please add skill.') {
                setTimeout(
                    function () {
                        // $('#lnkSaveSkill').css("background", "#0096a1");
                        // $('#lnkSaveSkill').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
        }
        function callCancelSkill() {
            $('#lnkCancelSkill').css("background", "#D0D0D0");
            $('#lnkCancelSkill').css("border", "2px solid #BCBDCE");
        }
        function callSaveEdu() {
            $('#lnkSaveEducation').css("background", "#00A5AA");
            $('#lnkSaveEducation').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtInstitute').text() == '' || $('#txtTitle').text() == '' || $('#ddlFromMonth').text() == 'Month'
                || $('#txtEducationFromdt').text() == 'Year' || $('#ddlToMonth').text() == 'Month' || $('#txtEducationTodt').text() == 'Year') {
                setTimeout(
                    function () {
                        // $('#lnkSaveEducation').css("background", "#0096a1");
                        // $('#lnkSaveEducation').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
        }
        function callCancelEdu() {
            $('#lnkCancelEducation').css("background", "#D0D0D0");
            $('#lnkCancelEducation').css("border", "2px solid #BCBDCE");
        }
        function callSaveAch() {
            // $('#lnkSaveAchivemnt').css("background", "#00A5AA");
            // $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtCompitition').text() == '' || $('#ddlMilestone').text() == 'Type of Milestone' || $('#ddlPosition').text() == 'Position') {
                setTimeout(
                    function () {
                        $('#lnkSaveAchivemnt').css("background", "#0096a1");
                        $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
            if ($('#lblAchivmentmsg').text() == 'Please select milestone.' || $('#lblAchivmentmsg').text() == 'Please select position.') {
                setTimeout(
                    function () {
                        // $('#lnkSaveAchivemnt').css("background", "#0096a1");
                        // $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
        }
        function callCancelAch() {
            $('#LinkButton4').css("background", "#D0D0D0");
            $('#LinkButton4').css("border", "1px solid #BCBDCE");
        }
        function CallAddFriends() {
            $(".divProgress").css("display", "none");
            // $('#aAddConnection').css("background", "#D0D0D0");
            // $('#aAddConnection').css("box-shadow", "0px 0px 5px #00B7E5");
        }
        function CallNewGroups() {
            $(".divProgress").css("display", "none");
            // $('#lnkNewGroups').css("background", "#D0D0D0");
            // $('#lnkNewGroups').css("box-shadow", "0px 0px 5px #00B7E5");
        }
        function divdeletes() {
            $(".divProgress").css("display", "none");
            $('#divDeletesucess').css("display", "none");
        }
        function CallSendMessage() {
            $('#lnkSendMess').css("box-shadow", "0px 0px 5px #00B7E5");
            //$('#lnkSendMess').addClass('disabled');
            if ($('#txtSubject').val() != '' && $('#txtBody').val() != '') {
                showLoader1();
            }

            if ($('#txtSubject').text() == '' || $('#txtBody').text() == '') {
                setTimeout(
                    function () {
                        //debugger;
                        //$('#lnkSendMess').removeClass('disabled');
                        $('#lnkSendMess').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);

            }
        }
        function ScrollToEdit(id) {
            //debugger;
            newid = id.split("$").join('_');
            $('#' + newid).focus();
        };
      //function abc(txtBoxID)
      //{
      //    debugger;
      //    var linkEdit = $("#lnkEditComment");
      //    linkEdit.removeAttr('href');
      //    linkEdit.attr('href', '#'+txtBoxID);
      //    //document.getElementById("lnkEditComment").href=""; 
      //    //document.getElementById("lnkEditComment").href="#"+txtBoxID; 
      //}
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#txtblogsearch, #searchBtn, #txtSearchQuestion").focusin(function () {
                $(this).parent(".search-cover").addClass("box-outline")
            });

            $("#bn4").breakingNews({
                effect: "slide-v",
                autoplay: true,
                timer: 3000,
                color: 'green',
                border: true
            });

            $("#txtDocTitle").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#txtInstituteName").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#txtDegree").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#txtInstitute").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#txtTitle").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#txtCompitition").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#ctl00_ContentPlaceHolder1_txtAdditionalAwrd").keydown(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $("#txtAreaExpert").keydown(function (e) {
                if (e.keyCode == 13) {
                    $("#btnAreaExpSave1").click();
                    return false;
                }
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                $("#txtblogsearch, #searchBtn, #txtSearchQuestion").focusin(function () {
                    $(this).parent(".search-cover").addClass("box-outline")
                });

                //$("#bn4").breakingNews({
                //    effect: "slide-v",
                //    autoplay: true,
                //    timer: 3000,
                //    color: 'green',
                //    border: true
                //});

                $("#txtDocTitle").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#txtInstituteName").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#txtDegree").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#txtInstitute").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#txtTitle").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#txtCompitition").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#ctl00_ContentPlaceHolder1_txtAdditionalAwrd").keydown(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $("#txtAreaExpert").keydown(function (e) {
                    if (e.keyCode == 13) {
                        $("#btnAreaExpSave1").click();
                        return false;
                    }
                });


            });
            // $('input').keydown(function(e){
            //       if(event.keyCode == 13) {
            //          $('#lnkEnterComment').trigger('click');

            //       }
            //   });
        });
    </script>
    <div id="divConnDisPopup" runat="server" class="modal backgroundoverlay" clientidmode="Static">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div>
                    <b>
                        <asp:Label ID="lbl" runat="server"></asp:Label>
                    </b>
                </div>
                <div class="modal-header">
                    <h5 class="modal-title">Confirmation
                    </h5>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblConn" runat="server"></asp:Label>
                </div>
                <div class="modal-footer border-top-0">
                    <a clientidmode="Static" href="javascript: void(0)" causesvalidation="false"
                        class="cancel_btn add-scroller btn btn-outline-primary m-r-15" onclick="CancelFrndPopUp();">Cancel </a>
                    <asp:LinkButton ID="lnkConnDisconn" runat="server" ClientIDMode="Static" Text="Yes"
                        CssClass="btn btn-primary" OnClick="lnkConnDisconn_Click" OnClientClick="CallConnDissCon(); showLoader1();">
                    </asp:LinkButton>
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
                    <asp:Label ID="lblSuccess" runat="server" Text="Friend added successfully."> </asp:Label>
                </div>
                <div class="modal-footer border-top-0">
                    <asp:Button ClientIDMode="Static" class="btn btn-primary add-scroller" ID="btnOk" runat="server" Text="Ok" OnClientClick="javascript:CloseMsg(); return false;"></asp:Button>
                    <%--OnClick="btnOk_click"--%>
                </div>
            </div>
        </div>
    </div>
    <div id="divFirstLogin" runat="server" class="modal backgroundoverlay" clientidmode="Static" style="display: none;">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Welcome to Skorkel  
                    </h5>
                </div>
                <div class="modal-body">
                    Welcome to Skorkel - A one-stop platform for law students to connect, research, contribute content, catch up on legal news & developments, get academic help/guidance from their peers and most importantly - obtain hundreds of jobs and internships being offered right here. Skorkel will give your career the headstart it deserves!
                </div>
                <div class="modal-footer border-top-0">
                    <asp:Button ClientIDMode="Static" class="btn btn-primary add-scroller" ID="youtube" runat="server" Text="Ok" OnClientClick="$('#divFirstLogin').css('display', 'none'); return false;"></asp:Button>
                    <%--OnClick="btnOk_click"--%>
                </div>
            </div>
        </div>
    </div>
    <div id="divDeletesucess" class="modal hide" role="dialog" aria-labelledby="confirmationTitle" aria-hidden="true">
        <div id="divDeleteConfirm" runat="server" class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="confirmationTitle">Delete Confirmation</h5>
                    <!--        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                  </button> -->
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblConnDisconn" runat="server" Text="Do you want to delete?"></asp:Label>
                </div>
                <div class="modal-footer border-top-0">
                    <asp:LinkButton ID="lnkDeleteCancel" runat="server" class="btn add-scroller btn-outline-primary m-r-15" data-dismiss="modal" ClientIDMode="Static" Text="Cancel"
                        OnClientClick="javascript:divCancels();return false;">
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" class="btn btn-primary" OnClientClick="divdeletes();"
                        OnClick="lnkDeleteConfirm_Click">
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div id="divDeletesucess1" class="modal hide" role="dialog" aria-labelledby="confirmationTitle" aria-hidden="true">
        <div id="divDeleteConfirm1" runat="server" class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="confirmationTitle1">Delete Confirmation</h5>
                    <!--      <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                  </button> -->
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblConnDisconn1" runat="server" Text="Do you want to delete?"></asp:Label>
                </div>
                <div class="modal-footer border-top-0">
                    <asp:LinkButton ID="lnkDeleteCancel1" runat="server" ClientIDMode="Static" Text="Cancel" class="btn btn-outline-primary m-r-15 add-scroller" data-dismiss="modal"
                        OnClientClick="javascript:divCancels();return false;">
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkDeleteConfirm1" runat="server" ClientIDMode="Static" Text="Yes" class="btn btn-primary" OnClientClick="javascript:showLoader1(); divdeletes();"
                        OnClick="lnkDeleteConfirm_Click">
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div id="divMessPopup" class="modal backgroundoverlay" runat="server" clientidmode="Static">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Message</h5>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label for="Message">Subject</label>
                        <asp:TextBox ID="txtSubject" MaxLength="100" autocomplete="off" runat="server" ClientIDMode="Static" placeholder="Subject"
                            class="forumTitle Msgsubjectsp form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubject" Display="Dynamic" ClientIDMode="Static"
                            ErrorMessage="Please enter subject." ValidationGroup="PeopleMessage" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label for="Message">Message</label>
                        <textarea id="txtBody" runat="server" maxlength="500" cols="20" rows="2" placeholder="Message" clientidmode="Static"
                            class="MsgserachBodey form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ClientIDMode="Static"
                            ControlToValidate="txtBody" Display="Dynamic" ValidationGroup="PeopleMessage"
                            ErrorMessage="Please enter a message" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <%--<asp:UpdatePanel ID="upPnlLnkButton" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>
                <div class="modal-footer border-top-0 padding-top-0">
                    <%--<a href="javascript: void(0)" class="btn add-scroller btn-outline-primary m-r-15" onclick="Cancel();">Cancel </a>--%>
                    <asp:LinkButton ID="btnCancel" CommandName="Join" CausesValidation="false"
                        runat="server" Text="Cancel" class="cancel_btn add-scroller btn btn-outline-primary m-r-15" OnClick="btnCancel_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lnkSendMess" runat="server" ValidationGroup="PeopleMessage" ClientIDMode="Static" Text="Send"
                        CssClass="btn btn-primary" OnClick="lnkSendMess_Click" OnClientClick="CallSendMessage();">
                    </asp:LinkButton>
                </div>
                <%--</ContentTemplate>
               </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
    <!--end of modal-->
    <!--end of Confirmation-->
    <%--   <div id="divDeletesucess" clientidmode="Static" runat="server" class="EditProfilepopupHome modal_bg" style="display: none;">
      <div id="divDeleteConfirm" runat="server" class="confirmDeletes modal-dialog" clientidmode="Static">
       <div>
        <b><asp:Label ID="lbl" runat="server"></asp:Label></b>
       </div>       
       <div class="modal-header">
        <strong>
         &nbsp;&nbsp;<asp:Label ID="lblConnDisconn" runat="server" Text="Do you want to Delete......... ?"></asp:Label>
        </strong>
       </div>      
      
       <tr>
       <td align="right">
       <div class="modal-footer">     
        <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="divdeletes();" 
         CssClass="joinBtn default_btn" OnClick="lnkDeleteConfirm_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkDeleteCancel" runat="server" class="cancel_btn" ClientIDMode="Static" Text="Cancel"
         OnClientClick="javascript:divCancels();return false;"></asp:LinkButton>      
       </div>
       </td>
       </tr>
      </div>
      </div>--%>
    <!--    popup end -->
    <div class="modal backgroundoverlay disclaimer-popup" id="divDisclaimerPopup" clientidmode="Static" runat="server">
        <div class="modal-dialog modal-dialog-centered">
            <div class="w-100">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Disclaimer
                        </h5>
                        <div class="float-left close-page-popup add-scroller" data-dismiss="modal">x</div>
                    </div>
                    <div class="modal-body">
                        Please note that you will not be eligible to obtain jobs and internships on Skorkel until you fill out your details and 
                  generate your own Skorkel Score. We urge you to do so at the earliest.
                    </div>
                    <div class="submit-con m-b-15">
                        <%--<div class="modal-close-login btn btn-outline-primary hide-body-scroll m-r-15" data-dismiss="modal">Ok</div>--%>
                        <button id="btnOkForClose" type="button" class="modal-close-login btn btn-outline-primary hide-body-scroll m-r-15">Ok</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $('#youtube').click(function () {
            $('.you-tube').addClass('display-blockk');
        });
        $(document).on('click', '.close-page-popup', function () {
            $('.disclaimer-popup').hide();
        });
        $(document).on('click', '#btnOkForClose', function () {
            $('.disclaimer-popup').hide();
        });
    </script>
</asp:Content>
