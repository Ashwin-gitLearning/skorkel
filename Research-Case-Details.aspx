<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Research-Case-Details.aspx.cs" Inherits="Research_Case_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("js/jquery.custom-scrollbar.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("js/jquery.mCustomScrollbar.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-section-inner">

        <div class="container-popupreserchdetails" id="divPopp" style="display: none;">
            <%--<div class="popupreserch pops_l center">
                        <img id="imgSave" src="images/loadingImage.gif" />
                    </div>--%>
        </div>
        <!--left box starts-->
        <span class="m-aside-trigger m-aside-trigger mb-0 mt-0">
            <span class="lnr lnr-arrow-left"></span>
            <span class="avatar-text">User highlights</span>
        </span>
        <div class="back-link-cover">
            <asp:HiddenField ID="hdnBack" runat="server" Value="0" ClientIDMode="Static" />
            <a onclick="historyBack();" href="#?" class="back-link">
                <span class="lnr lnr-arrow-left"></span>Back to Results
            </a>
        </div>
        <asp:UpdatePanel ID="upDesk" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="full-width-con">
                    <!--party starts-->
                    <div class="card card-list-con">
                        <div class="list-group-item top-list">
                            <div class="post-con">
                                <!--             <div class="post-header justify-content-between">
                                            <div>
                                                <span class="question-icon">
                              <span class="icon"><img src="images/file.svg"></span>
                                                </span>
                                                <ul class="que-con">
                                                    <li>
                                                        Case
                                                        <asp:Label ID="lblCitedBy" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
                                            </div>
                                          
                                        </div> -->
                                <div class="post-body">

                                    <h3 class="mb-1">
                                        <asp:Label ID="lblpartyname" class="partyNameLabel" runat="server" Text="Party Name"></asp:Label>
                                    </h3>

                                    <p class="date pt-2 mb-2 text-left">
                                        <asp:Label ID="lblcourt" runat="server" Text="Supreme Court"></asp:Label>
                                        <asp:Label ID="lblyear" runat="server" Text="1984"></asp:Label>
                                        <br />
                                        <span class="pt-2 display-inline">
                                            <span>
                                                <strong>Case No:</strong> <span id="lblCaseNo" runat="server" class="mr-2 display-inline"></span>

                                            </span>
                                            <span id="spnCitation" runat="server" clientidmode="Static">
                                                <strong>Citation:</strong>
                                                <asp:Label ID="lblcitation" class="partyNameLabelTxt" runat="server" Text="AIR 1984 SC571"></asp:Label>
                                            </span>
                                        </span>
                                    </p>
                                    <ul class="list-inline judgeslist">
                                        <li class="list-inline-item"><strong>Judges:</strong></li>
                                        <asp:ListView ID="lstJudgeName" runat="server">
                                            <ItemTemplate>
                                                <li class="list-inline-item">
                                                    <asp:Label ID="lblJudgeName" runat="server" Text='<%#Eval("JudgeName")%>'></asp:Label>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ul>
                                    <div class="post-footer pb-1 pl-0 pt-3 r-m-icons">
                                        <ul class="list-inline">
                                            <li class="list-inline-item">
                                                <asp:UpdatePanel ID="upresearchp" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton CssClass="d-flex align-items-center active-toogle" ID="lnkFvrtImage" runat="server" ToolTip="Bookmark" OnClick="lnkFvrtImage_Click" ClientIDMode="Static" OnClientClick="javascript:callFvrtimg();">
                                                            <span class="iconblock"><i runat="server" id="icnBook" class="icon-tags"></i></span><span id="txtBookMark" runat="server" class="hide-text-d">Bookmark</span>
                                                        </asp:LinkButton>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lnkFvrtImage" EventName="Command" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </li>
                                            <li class="list-inline-item">
                                                <asp:LinkButton ID="lnkShare" runat="server" ClientIDMode="Static" CssClass="d-flex align-items-center hide-body-scroll active-toogle" OnClick="lnkShare_Click" OnClientClick="showLoader1();">
                                                                    <span class="iconblock"><i class="icon-chat"></i></span><span class="hide-text-d">Share</span>
                                                </asp:LinkButton>
                                            </li>
                                            <li class="list-inline-item">

                                                <asp:LinkButton ID="lnkDownload" runat="server" ClientIDMode="Static" class="d-flex align-items-center active-toogle" OnClick="lnkDownload_Click" OnClientClick="return DownLoadClicks();">
                                                                    <span class="iconblock"><i class="icon-down-arrow"></i></span><span class="hide-text-d">Download</span>
                                                </asp:LinkButton>
                                            </li>
                                            <li class="list-inline-item">
                                                <asp:LinkButton ID="gmailshare" runat="server" OnClick="gmailshare_Click" ClientIDMode="Static" class="d-flex align-items-center active-toogle" OnClientClick="return callLoaderAndDownload();">
                                                            <span class="iconblock"><i class="far fa-envelope"></i></span><span class="hide-text-d">Mail</span>
                                                </asp:LinkButton>
                                            </li>
                                            <%-- <li class="list-inline-item wts-app-mob">--%>
                                            <li class="list-inline-item wts-app-mob">
                                                <asp:LinkButton ID="whatsappShare" runat="server" OnClick="whatsappShare_Click" ClientIDMode="Static" class="d-flex align-items-center active-toogle" OnClientClick="return callLoaderAndDownload();">
                                                           <span class="iconblock"><i class="fab fa-whatsapp"></i></span><span class="hide-text-d">WhatsApp</span>
                                                </asp:LinkButton>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <!---Gmail POPUP-->
                                <div id="PopUpGmailShare" clientidmode="Static" runat="server" class="remove-inline modal backgroundoverlay">
                                    <div class="modal-dialog modal-dialog-centered">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title">
                                                    <span>Share Document Over Mail</span>
                                                </h5>


                                                <button type="button" class="close add-scroller" onclick="Cancel();" aria-label="Close">
                                                    <span aria-hidden="true">×</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="form-group position-relative">
                                                    <label>To </label>
                                                    <%--<input type="text" class="form-control" value="Enter members name here" class="default" autocomplete="off" tabindex="4">--%>
                                                    <asp:TextBox runat="server" ID="txtEmailId" ClientIDMode="Static" class="form-control  default" placeholder="Enter email here." autocomplete="off" />

                                                    <div class="error_message">
                                                        <asp:Label ID="lblEmailValidation" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="media align-items-center mb-3">
                                                    <div class="media-object case-outline mr-2">
                                                        <img src="images/file.svg" alt="Case">
                                                    </div>
                                                    <div class="media-body">
                                                        <h6 class="media-heading mb-0"><span></span>
                                                            <span id="docText" runat="server" clientidmode="Static"></span>
                                                        </h6>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Message </label>
                                                    <textarea name="" id="txtBodyGmail" runat="server" clientidmode="Static" cols="20" rows="2" placeholder="Message" class="form-control"></textarea>
                                                    <span class="message-on-mail"><strong>Note:</strong> You can add multiple emails separated with a comma.</span>
                                                </div>
                                                <%-- <div class="form-group">
                                                    <label>Link </label>
                                                    <input name="" id="" type="text" readonly="readonly" placeholder="Paste link" class="form-control">
                                                </div>--%>
                                            </div>
                                            <div class="modal-footer border-top-0 padding-top-0">
                                                <%--<a href="#" class="btn btn-outline-primary add-scroller m-r-15">Cancel </a>--%>
                                                <%--<a href="#" class="joinBtn btn btn-primary">Share</a>--%>
                                                <a clientidmode="Static" href="#?" class="btn btn-outline-primary add-scroller m-r-15" onclick="Cancel();">Cancel </a>
                                                <asp:LinkButton ID="lnkShareOnGmail" runat="server" ClientIDMode="Static" Text="Share" CssClass="joinBtn btn btn-primary" OnClientClick="showLoader1()" OnClick="lnkShareOnGmail_Click"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!--Whats POPUP-->
                                <div id="popupWhatsappShare" clientidmode="Static" runat="server" class="remove-inline modal backgroundoverlay">
                                    <div class="modal-dialog modal-dialog-centered">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title">
                                                    <span>Share Docnumet Over WhatsApp</span>
                                                </h5>
                                                <button type="button" class="close add-scroller" onclick="Cancel();" aria-label="Close">
                                                    <span aria-hidden="true">×</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="form-group position-relative">
                                                    <label>To </label>
                                                    <%--<input type="text" class="form-control" value="Enter members name here" class="default" autocomplete="off" tabindex="4">--%>
                                                    <asp:TextBox runat="server" onkeyup="if (/\D/g.test(this.value)) this.value = this.value.replace(/\D/g,'')" ID="txtMobileNo" MaxLength="12" ClientIDMode="Static" class="form-control  default" placeholder="Enter mobile no. here." autocomplete="off" />

                                                    <div class="error_message">
                                                        <asp:Label ID="lblMobileErrorMsg" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="media align-items-center mb-3">
                                                    <div class="media-object case-outline mr-2">
                                                        <img src="images/file.svg" alt="Case">
                                                    </div>
                                                    <div class="media-body">
                                                        <h6 class="media-heading mb-0"><span></span>
                                                            <span id="spamDoc" runat="server" clientidmode="Static"></span>
                                                        </h6>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Message </label>
                                                    <textarea name="" id="txtWhatsappMessage" runat="server" clientidmode="Static" cols="20" rows="2" placeholder="Message" class="form-control"></textarea>
                                                </div>
                                            </div>
                                            <div class="modal-footer border-top-0 padding-top-0">
                                                <a clientidmode="Static" href="#?" class="btn btn-outline-primary add-scroller m-r-15" onclick="Cancel();">Cancel </a>
                                                <asp:LinkButton ID="lnkShareDocWhatsapp" runat="server" ClientIDMode="Static" Text="Share" CssClass="joinBtn btn btn-primary" OnClientClick="showLoader1()"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br>
                                <!--     <div class="post-footer">
                                        <ul class="list-inline">
                                            <li class="list-inline-item">
                                                <%--<asp:LinkButton ID="lnkComments" runat="server" ClientIDMode="Static" CssClass="citedCited"
                                                                        Text="Comment" OnClick="lnkComments_Click" PostBackUrl="#Commentsection"></asp:LinkButton>--%>
                                                    
                                            </li>
                                            <li class="list-inline-item"><a href="#" class="active-toogle"><span class="share-btn"></span> 5 Share</a></li>
                                            <li class="list-inline-item"><a href="#" class="active-toogle"><span class="download-view"></span> 5 Downloads</a></li>
                                        </ul>
                                    </div> -->
                            </div>
                        </div>
                    </div>
                    <!--tab ends-->
                    <div class="panel-cover clearfix">
                        <div class="center-panel">
                            <div class="card card-list-con">
                                <asp:UpdatePanel ID="updates" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="intCommentAddedFors" runat="server" ClientIDMode="Static" />
                                        <%--<div class="orgTxt" style="background-size: 170px 20px;">
                                 <asp:Label ID="lblOriginalTxt" runat="server" ClientIDMode="Static" ></asp:Label>
                                 </div>--%>
                                        <!--Summary Write starts-->
                                        <div id="DivSummary" clientidmode="Static" class="modal backgroundoverlay" style="display: none;">
                                            <div class="modal-dialog modal-dialog-centered">
                                                <div class="modal-content">
                                                    <input id="hdnSummaryTextPost" type="hidden" runat="server" />
                                                    <div class="modal-header">
                                                        <h5 class="modal-title">Summary</h5>
                                                        <button type="button" class="close add-scroller" onclick="Cancel(); return false;" aria-label="Close">
                                                            <span aria-hidden="true">&times;</span>
                                                        </button>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="form-group">
                                                            <textarea id="txtSummary" maxlength="8000" placeholder="Write your summary here..." clientidmode="Static" runat="server" class="tabSummary form-control" rows="8"></textarea>
                                                            <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="Summary" ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSummary" class="summary_erroe_mssz error_message" ErrorMessage="Please enter Summary"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class=" border-top-0 modal-footer padding-top-0">
                                                        <a href="#" onclick="Cancel(); return false;" class="cancel_btn cancel_r_c btn btn-outline-primary add-scroller m-r-15">Cancel</a>
                                                        <asp:LinkButton ID="BtnSaveSummary" CssClass="btn btn-primary add-scroller" CausesValidation="true" OnClientClick="CallSaveSummaery();" ClientIDMode="Static" ValidationGroup="Summary" runat="server" Text="Submit" OnClick="BtnSaveSummary_Click"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--Summary Write ends-->
                                        <div id="PopUpShare" runat="server" clientidmode="Static" class="modal backgroundoverlay" style="display: none;">
                                            <div class="modal-dialog modal-dialog-centered">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h5 class="modal-title">
                                                            <asp:Label ID="lblTitles" ClientIDMode="Static" Text="Share Document" runat="server"></asp:Label>
                                                        </h5>
                                                        <asp:Label ID="lblTitleGroup" ClientIDMode="Static" runat="server"></asp:Label>
                                                        <button type="button" class="close add-scroller" onclick="Cancel();" aria-label="Close">
                                                            <span aria-hidden="true">&times;</span>
                                                        </button>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="form-group position-relative">
                                                            <label>To </label>
                                                            <select data-placeholder="Enter members name here" class="share_one chosen-select form-control" id="txtInviteMembers" onchange="getMultipleValues(this.id)" runat="server" clientidmode="Static" multiple tabindex="4">
                                                            </select>
                                                            <asp:HiddenField ID="hdnInvId" ClientIDMode="Static" runat="server" />
                                                            <div class="error_message">
                                                                <asp:Label ID="lblMess" runat="server" Text=""></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="media align-items-center mb-3">
                                                            <div class="media-object case-outline mr-2">
                                                                <img id="imgGrp1" runat="server" alt="Case" />
                                                            </div>
                                                            <div class="media-body">
                                                                <h6 class="media-heading mb-0">
                                                                    <asp:Label ID="lblDocTitle" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblGroupSummary1" runat="server"></asp:Label>
                                                                </h6>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Message </label>
                                                            <textarea id="txtBody" clientidmode="Static" runat="server" cols="20" rows="2" placeholder="Message" class="form-control"></textarea>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Link </label>
                                                            <asp:TextBox ID="txtLink" runat="server" placeholder="Paste link" ReadOnly="true" class="form-control"></asp:TextBox>
                                                        </div>
                                                        <p>
                                                            <strong>
                                                                <asp:Label ID="lblMessAccept" runat="server"></asp:Label>
                                                                <asp:Label ID="lblMessReject" runat="server"></asp:Label>
                                                            </strong>
                                                        </p>
                                                    </div>
                                                    <div class="modal-footer border-top-0 padding-top-0">
                                                        <a clientidmode="Static" href="#?" class="btn btn-outline-primary add-scroller m-r-15" onclick="Cancel();">Cancel </a>
                                                        <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Share" CssClass="joinBtn btn btn-primary" OnClientClick="showLoader1()" OnClick="lnkPopupShare_Click"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkShare" EventName="Click" />
                                        <%-- <asp:AsyncPostBackTrigger ControlID="lnkComments" EventName="Click" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="list-group-item top-list case-text">
                                    <div class="post-con">
                                        <div class="post-header d-flex align-items-center justify-content-between">
                                            <div class="heighligt-user d-flex align-items-center">
                                                <div runat="server" id="divusrPosted" class="avtar-block mr-1">
                                                    <asp:Image runat="server" ID="imgUserPosted" />
                                                </div>
                                                <div class="avtar-title ml-2">
                                                    <h4 class="mb-0">
                                                        <asp:Label ID="lblHighBy" runat="server" Text="Highlighted by " ClientIDMode="Static"></asp:Label><asp:Label ID="lblOriginalTxt" runat="server" ClientIDMode="Static"></asp:Label></h4>
                                                </div>
                                            </div>
                                            <div runat="server" id="divUserStart" class="userstats mt-2">
                                                <ul class="list-inline">
                                                    <li class="list-inline-item">
                                                        <div class="d-flex align-items-center links-like-btn">
                                                            <div runat="server" id="divLike" class="">
                                                                <asp:LinkButton ID="lnkLike" OnClick="lnkLike_Click" runat="server" class="like-btn"></asp:LinkButton>
                                                            </div>
                                                            <span class="d-flex">
                                                                <asp:Label runat="server" ID="lblTotalLikes" Visible="true"></asp:Label>
                                                            </span>
                                                        </div>
                                                    </li>
                                                    <li class="list-inline-item">
                                                        <div class="d-flex align-items-center">
                                                            <div class="eye-view"></div>
                                                            <asp:Label runat="server" ID="lblTotalViews" Visible="true"></asp:Label>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="post-body">
                                            <h4 class="text-center">
                                                <asp:Label ID="lblCaseTitle" runat="server" ClientIDMode="Static" Text="Title"></asp:Label>
                                            </h4>
                                            <div runat="server" id="DivDesc">
                                                <!--     <p>
                                       <asp:HiddenField runat="server" ID="hdndivvalue" ClientIDMode="Static" />
                                       </p> -->
                                                <div>
                                                    <p>
                                                        <%--<div id="divdisp" name="divdisp"  onmouseup="javascript:highlightSelection('#FF0000');" runat="server"
                                             clientidmode="Static" onkeypress="javascript:return false;" onkeydown="javascript:return false;">--%>
                                                        <div id="divdisp" name="divdisp" runat="server" clientidmode="Static" onkeypress="javascript:return false;" onkeydown="javascript:return false;">
                                                        </div>
                                                        <p>
                                                        </p>
                                                        <div id="divGuest" runat="server" clientidmode="Static" style="">
                                                        </div>
                                                        <div class="blankdivheight">
                                                        </div>
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
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                    </p>
                                                </div>
                                            </div>
                                            <!--mark as box starts-->
                                            <!--mark as box ends-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="DivMenuBar" class="markAsBox" style="display: none;">
                                <h4>Mark As</h4>
                                <ul>
                                    <li>
                                        <asp:LinkButton ID="lnkFactTab" ClientIDMode="Static" runat="server" OnClick="lnkFactTab_Click" ToolTip="Fact"><span>Fact</span></asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="lnkIssueTab" ClientIDMode="Static" runat="server" OnClick="lnkIssueTab_Click" ToolTip="Issue"><span>Issue</span></asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="lnkImparagrph" ClientIDMode="Static" runat="server" OnClick="lnkImportantPara_Click" ToolTip="Important Paragraph"><span>Important Paragraph</span></asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="lnkPrecedent" ClientIDMode="Static" runat="server" OnClick="lnkPrecedent_Click" ToolTip="Precedent"><span>Precedent</span></asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="lnkDecidendit" ClientIDMode="Static" runat="server" OnClick="lnkDecidendit_Click" ToolTip="Radio Decidendi"><span>Ratio Decidendi</span></asp:LinkButton>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="lnkOrbite" ClientIDMode="Static" runat="server" OnClick="lnkOrbite_Click" ToolTip="Orbiter Dictum"><span>Orbiter Dictum</span></asp:LinkButton>
                                    </li>
                                    <li style="display: none;">
                                        <asp:LinkButton ID="lnkHighlight" ClientIDMode="Static" runat="server" OnClick="lnkHighlight_Click" ToolTip="Highlight"><span>Highlight</span></asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                            <a name="Commentsection"></a>
                            <div id="CommentSections" class="CommentSections"></div>
                        </div>
                        <!--right box starts-->
                        <div class="right-panel-back-layer"></div>
                        <div class="right-panel">
                            <span class="m-view back">Back <span class="lnr lnr-arrow-right"></span></span>
                            <div class="aside-bar">
                                <div class="card">
                                    <div class="card-body">
                                        <div class="d-flex justify-content-between align-items-center">
                                            <h4 class="font-size-14-m">User Highlighting</h4>
                                            <div>
                                                <asp:LinkButton ID="lnkShowOrgtxt" runat="server" ClientIDMode="Static" CssClass="showOriText" Text="Show Original" OnClick="lnkShowOrgtxt_Click"></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="btn-group w-100">
                                            <div class="userdropdown dropdown-toggle w-100 d-flex align-items-center justify-content-between" data-toggle="dropdown">
                                                <div id="userSelected" clientidmode="Static" runat="server">Select User</div>
                                                <div class="icon-caret-down"></div>
                                            </div>
                                            <ul class="dropdown-menu userdropdown-menu">
                                                <li runat="server" id="noUser" class="dropdown-item remove-body-fixed-class">No User</li>
                                                <asp:ListView ID="lstUserActivityCase" runat="server" OnItemCommand="lstUserActivityCase_ItemCommand" OnItemDataBound="lstUserActivityCase_ItemDataBound">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnintCommentAddedFor" runat="server" ClientIDMode="Static" Value='<%# Eval("intAddedby") %>' />
                                                        <asp:HiddenField ID="hdnintContentId" runat="server" ClientIDMode="Static" Value='<%# Eval("intCaseId") %>' />
                                                        <!--user comment starts-->
                                                        <li>
                                                            <asp:LinkButton ID="lnk" CssClass="dropdown-item remove-body-fixed-class" CommandName="UserName" runat="server">
                                                                <div class="media">


                                                                    <img id="imgprofile" runat="server" class="rounded-circle mr-2" src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' width="40" height="40" alt="" />
                                                                    <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />

                                                                    <div class="media-body">
                                                                        <h5 class="mt-0 mb-0 usertitle truncate">
                                                                            <asp:Label Font-Underline="false" ID="lnkName" CssClass="usrnm" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                        </h5>


                                                                        <%-- <asp:LinkButton Font-Underline="false" CommandName="Title" ID="lnkTitle" CssClass="usrBrief" runat="server" Text='<%#Eval("Title") %>'></asp:LinkButton>--%>

                                                                        <%-- <p class="usrBriefTxt">
                                                        <asp:Label ID="lblAttachDocs" runat="server" Text='<%#Eval("Descriptions") %>' CssClass="usrBriefTxt"></asp:Label>
                                                    </p>--%>
                                                                        <ul class="list-inline">
                                                                            <li class="list-inline-item">
                                                                                <div class="d-flex align-items-center">
                                                                                    <div class="like-btn "></div>
                                                                                    <div>
                                                                                        <asp:Label ID="lblLikes" runat="server" Text="0 Likes"></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </li>
                                                                            <li class="list-inline-item">
                                                                                <div class="d-flex align-items-center">
                                                                                    <div class="eye-view "></div>
                                                                                    <div>
                                                                                        <asp:Label ID="lblViews" runat="server" Text=""></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </li>
                                                                        </ul>
                                                                    </div>

                                                                </div>
                                                            </asp:LinkButton>
                                                        </li>
                                                        <!--user comment ends-->
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <!-- card ended-->
                                <div class="card">
                                    <div class="card-body">
                                        <h4>Legend Mark</h4>
                                        <ul class="list-group legand">
                                            <li class="list-inline-item">
                                                <span class="fact"></span><span>Fact</span>
                                            </li>
                                            <li class="list-inline-item">
                                                <span class="issue"></span><span>Issue</span>
                                            </li>
                                            <li class="list-inline-item">
                                                <span class="important"></span><span>Important Paragraph</span>
                                            </li>

                                            <li class="list-inline-item">
                                                <span class="prec"></span><span>Precedent</span>
                                            </li>
                                            <li class="list-inline-item">
                                                <span class="ratio"></span><span>Ratio Decidendi</span>
                                            </li>
                                            <li class="list-inline-item">
                                                <span class="orbit"></span><span>Orbiter Dictum</span>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <center>
                                    <div>
                                    </div>
                                </center>


                    </div>
                </div>
                <asp:HiddenField ID="hdnCommentAddedFor" runat="server" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdncolour" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnMarkText" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnPasteCode" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnX" ClientIDMode="Static" />
                <asp:HiddenField runat="server" ID="hdnY" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnhtmlSelectedText" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnAttemptselection" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnPostBackCheck" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnPostDescTxt" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnSelectionCount" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnSlectedText" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnTabId" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnTagtypeId" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnLoginId" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnRatioTabId" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnMainSelectedText" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnComments" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnStartIdx" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hdnEndIdx" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hdnMarkMaxCount" Value="0" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hdnDivContent" runat="server" ClientIDMode="Static" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkFactTab" />
                <asp:AsyncPostBackTrigger ControlID="lnkIssueTab" />
                <asp:AsyncPostBackTrigger ControlID="lnkImparagrph" />
                <asp:AsyncPostBackTrigger ControlID="lnkPrecedent" />
                <asp:AsyncPostBackTrigger ControlID="lnkDecidendit" />
                <asp:AsyncPostBackTrigger ControlID="lnkOrbite" />
            </Triggers>
        </asp:UpdatePanel>
        <!--right box ends-->
        <div class="cls">
        </div>
    </div>
    <div class="overlaylight"></div>
    <div class="summaryblock">
        <div class="arrowblock">
            <span class="left-border"></span><a class="arroblock" onclick="sumClick();" href="#?">
                <h4 class="mb-0 d-inline-block">Summary</h4>
                <i class="arrow d-inline-block">
                    <img src="images/double-arrow.svg" alt="arrow" /></i></a><span class="right-border"></span>
        </div>
        <div class="sumt-scroll">


            <!--  <div class="first-pargraph">
                       <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.</p>
                   </div> -->
            <div class="sumlist">
                <div class="text-center">
                    <asp:UpdatePanel ID="updatee" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnkWriteButton" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:LinkButton ID="lnkWriteButton" runat="server" ClientIDMode="Static" CssClass="WriteSummerys btn btn-primary w-150 hide-body-scroll" Text="Add Summary" OnClientClick="CallWriteSummary();" OnClick="lnkWriteButton_Onclick">
                            </asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:UpdatePanel ID="pnlSum" runat="server">
                    <ContentTemplate>
                        <div class="card" runat="server" id="divSumByyou" style="display: none;">

                            <asp:HiddenField ID="hdnSummryID" runat="server" />
                            <div class="card-header tranb border-bottom-0 d-flex align-items-center justify-content-between">
                                <h4 class="mb-0">Summary by you</h4>
                                <span class="more-btn edit-btn-fix">
                                    <span class="dropdown">
                                        <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <img src="images/more.svg" alt="" class="more-btn" />
                                        </a>
                                        <span class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                            <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkWriteButton_Onclick" CssClass="dropdown-item"><span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>
                                        </span>
                                    </span>
                                </span>
                            </div>
                            <div class="card-body pt-0">
                                <p runat="server" id="sumDetails" class="pargraph-pre-wrap"></p>
                                <span class="d-flex align-items-center links-like-btn">
                                    <asp:LinkButton ID="lnkOwnLike" OnClick="lnkOwnLike_Click" runat="server" CssClass="active-toogle"><span class="like-btn"></span></asp:LinkButton>
                                    <span class="d-flex">
                                        <asp:Label ID="lblOwnSumlike" ToolTip="View Likes" runat="server" Text="">
                                        </asp:Label>
                                    </span>
                                </span>
                            </div>


                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="upList" runat="server">
                    <ContentTemplate>
                        <h4 id="lblSumOtherss" runat="server" class="mb-3">Summary by other members</h4>
                        <div class="card card-list-con">
                            <asp:ListView ID="lstSumOthrs" OnItemCommand="lstSumOthrs_ItemCommand" OnItemDataBound="lstSumOthrs_ItemDataBound" runat="server">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnSumId" Value="" runat="server" />
                                    <div class="list-group-item border-top-0">
                                        <div class="post-con">
                                            <div class="post-header">
                                                <span class="avatar-img">
                                                    <img src='<%#Eval("vchrPhotoPath") %>' alt="" class="rounded-circle">
                                                </span><span class="user-name-date">&nbsp;<span class="user-name"><%#Eval("strAddedBy") %></span><span class="date"><%#Eval("dtAddedDate") %></span></span>
                                            </div>
                                            <div class="post-body">
                                                <p><%#Eval("strSummaryText") %></p>
                                            </div>
                                            <div class="post-footer">
                                                <ul class="list-inline">
                                                    <li class="list-inline-item">
                                                        <span class="d-flex align-items-center links-like-btn">
                                                            <asp:LinkButton ID="lnkSumLike" CommandName="Like" runat="server" CssClass="active-toogle"><span class="like-btn"></span></asp:LinkButton>
                                                            <span class="d-flex">
                                                                <asp:Label ID="lblTotalSumlike" ToolTip="View Likes" runat="server"
                                                                    Text='<%#Eval("TotalLikes") + (((int)Eval("TotalLikes")==1)? " Like": " Likes")%>'>
                                                                </asp:Label>
                                                            </span>
                                                        </span>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <hr class="event-seprator">
                                    </div>

                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <!-- <h4 class="mb-3">Summary by other members</h4>
                        <div class="card card-list-con">
                            <div class="list-group-item border-top-0">
                                <div class="post-con">
                                    <div class="post-header">
                                        <span class="avatar-img">
                                                              <img src="images/avatar2.jpg" alt="" class="rounded-circle">
                                                             </span> <span class="user-name-date">
                                                                <span class="user-name">Sumit Mehta</span> <span class="date">30 Sep 2018</span> </span>
                                    </div>
                                    <div class="post-body">
                                        <p> Would you like to pop in a CD and have a better quality of life, and even self improvement? There are three ways you can use music to accomplish this.</p>
                                        <p>Music For Motivation</p>
                                        <p>Put on energetic music, and even doing housework seems less like work. Using music to motivate yourself or change your mood is an area where you can trust your experience and experimentation. When you find the music that energizes you, relaxes you, or makes you happy, keep it ready for when you need it. </p>
                                    </div>
                                    <div class="post-footer">
                                        <ul class="list-inline">
                                            <li class="list-inline-item"><a href="#" class="active-toogle"><span class="like-btn active"></span> 10 Likes</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="list-group-item ">
                                <div class="post-con">
                                    <div class="post-header">
                                        <span class="avatar-img">
                                                              <img src="images/avatar2.jpg" alt="" class="rounded-circle">
                                                             </span> <span class="user-name-date">
                                                                <span class="user-name">Sumit Mehta</span> <span class="date">30 Sep 2018</span> </span>
                                    </div>
                                    <div class="post-body">
                                        <p> Would you like to pop in a CD and have a better quality of life, and even self improvement? There are three ways you can use music to accomplish this.</p>
                                        <p>Music For Motivation</p>
                                        <p>Put on energetic music, and even doing housework seems less like work. Using music to motivate yourself or change your mood is an area where you can trust your experience and experimentation. When you find the music that energizes you, relaxes you, or makes you happy, keep it ready for when you need it. </p>
                                    </div>
                                    <div class="post-footer">
                                        <ul class="list-inline">
                                            <li class="list-inline-item"><a href="#" class="active-toogle"><span class="like-btn active"></span> 10 Likes</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="list-group-item">
                                <div class="post-con">
                                    <div class="post-header">
                                        <span class="avatar-img">
                                                              <img src="images/avatar2.jpg" alt="" class="rounded-circle">
                                                             </span> <span class="user-name-date">
                                                                <span class="user-name">Sumit Mehta</span> <span class="date">30 Sep 2018</span> </span>
                                    </div>
                                    <div class="post-body">
                                        <p> Would you like to pop in a CD and have a better quality of life, and even self improvement? There are three ways you can use music to accomplish this.</p>
                                        <p>Music For Motivation</p>
                                        <p>Put on energetic music, and even doing housework seems less like work. Using music to motivate yourself or change your mood is an area where you can trust your experience and experimentation. When you find the music that energizes you, relaxes you, or makes you happy, keep it ready for when you need it. </p>
                                    </div>
                                    <div class="post-footer">
                                        <ul class="list-inline">
                                            <li class="list-inline-item"><a href="#" class="active-toogle"><span class="like-btn active"></span> 10 Likes</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div> -->
            </div>
        </div>
        <asp:HiddenField ID="hdnIpAddress" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnIsCalledWhatsAppMethod" runat="server" ClientIDMode="Static" />
    </div>
    <script type="text/javascript">
        function sumClick() {
            //     if ($('#txtShortSum').text().trim() != "") {
            $('.summaryblock').toggleClass('parent-height');
            $('body').toggleClass('over');
            // $('#collapseSum').toggle();
            $('.overlaylight').toggleClass('show');
            var div = $("div").height();
            var win = $(window).height();


            //         }
        }
        $(document).ready(function () {
            var flagno = "1";
            var Groupvalues = "";
            var CommentUserId = '';
            CommentUserId = $('#intCommentAddedFors').val();
            if (CommentUserId != '') {
                var body = $("#divdisp").html();
                body = body.replace(/<span4 class="preced">/g, "");
                body = body.replace(/<\/span4>/g, "");
                body = body.replace(/<span5 class="rediod">/g, "");
                body = body.replace(/<\/span5>/g, "");
                body = body.replace(/<span2 class="issuss">/g, "");
                body = body.replace(/<\/span2>/g, "");
                body = body.replace(/<span1 class="factss">/g, "");
                body = body.replace(/<\/span1>/g, "");
                body = body.replace(/<span7 class="highls">/g, "");
                body = body.replace(/<\/span7>/g, "");
                body = body.replace(/<span6 class="orbits">/g, "");
                body = body.replace(/<\/span6>/g, "");
                body = body.replace(/<span3 class="ImpPss">/g, "");
                body = body.replace(/<\/span3>/g, "");
                body = body.replace(/&amp;/g, '&');
                var caseId = '<%= Request.QueryString["cId"]%>';
                var tagTypeIds = ',';
                $("#DivCommentContent").css('display', 'block');
                tagTypeIds += "1,2,3,4,5,6,7";
                $('#divdisp').unbind('mouseup');
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: '{"CaseId":"' + caseId + '","TagTypeIds":"' + tagTypeIds + '","FlagNo":"' + flagno + '","GroupId":"' + Groupvalues + '","CommentUserId":"' + CommentUserId + '"}',
                    url: 'DocumentWebService.asmx/GetDocumentIndexes',
                    async: false,
                    dataType: "json",
                    success: function (data) {
                        body = $.trim(body).replace(/\s(?=\s)/g, '');
                        $(data.d).each(function () {
                            body = SetBackgroundColor(body, this.start, this.end, this.imgtype, this.extralength, this.rownum, this.name, this.NoSpan, this.MaxCount);
                        });

                        $("#divdisp").html(body);
                    }
                });
                return false;
            }
            $('.mobile-alert').click(function () {
                if (/iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream) {
                    alert("This feature isn't supported on this device.");
                }
            });


            //To be checked valid email address

            $('#lnkShareOnGmail').click(function () {
                //var email = $('#txtEmailId').val();
                //$('#lblEmailValidation').text('');

                //if (email == '') {
                //    $('#lblEmailValidation').text('Enter member email id!');
                //    return false;
                //}
                //if (IsEmail(email) == false) {
                //    $('#lblEmailValidation').text('Invalid email id!');
                //    return false;
                //}

                //Test
                //var emails = email.split(",");
                ////alert(emails);
                //emails.forEach(function (email) {
                //    alert(email.trim());
                //    if (IsEmail(email) == false) {
                //        $('#lblEmailValidation').text('Invalid email id!');
                //        return false;
                //    }
                //});
                //return false;
                //showLoader1();
                $('#lblEmailValidation').text('');

            });
            function IsEmail(email) {
                var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (!regex.test(email)) {
                    return false;
                } else {
                    return true;
                }
            }
        });
    </script>
    <script type="text/javascript">
        var flagno = "1";
        var Groupvalues = "";
        function ShoWAddSum() {
            document.getElementById("DivSummary").style.display = 'block';
        }
        function onlinkclick() {
            // document.getElementById("DivSummary").style.display = 'block';
            var body = $("#divdisp").html();
            body = body.replace(/<span4 class="preced">/g, "");
            body = body.replace(/<\/span4>/g, "");
            body = body.replace(/<span5 class="rediod">/g, "");
            body = body.replace(/<\/span5>/g, "");
            body = body.replace(/<span2 class="issuss">/g, "");
            body = body.replace(/<\/span2>/g, "");
            body = body.replace(/<span1 class="factss">/g, "");
            body = body.replace(/<\/span1>/g, "");
            body = body.replace(/<span7 class="highls">/g, "");
            body = body.replace(/<\/span7>/g, "");
            body = body.replace(/<span6 class="orbits">/g, "");
            body = body.replace(/<\/span6>/g, "");
            body = body.replace(/<span3 class="ImpPss">/g, "");
            body = body.replace(/<\/span3>/g, "");
            body = body.replace(/&amp;/g, '&');
            var caseId = '<%= Request.QueryString["cId"]%>';
            var tagTypeIds = ',';
            tagTypeIds += "1,2,3,4,5,6,7";
            var intcommentId = $('#intCommentAddedFors').val();
            flagno = "1";
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: '{"CaseId":"' + caseId + '","TagTypeIds":"' + tagTypeIds + '","FlagNo":"' + flagno + '","GroupId":"' + Groupvalues + '","CommentUserId":"' + intcommentId + '"}',
                url: 'DocumentWebService.asmx/GetDocumentIndexes',
                async: false,
                dataType: "json",
                success: function (data) {
                    body = $.trim(body).replace(/\s(?=\s)/g, '').replace(/&nbsp;/g, '');
                    $(data.d).each(function () {
                        body = SetBackgroundColor(body, this.start, this.end, this.imgtype, this.extralength, this.rownum, this.name, this.NoSpan, this.MaxCount);
                        document.getElementById('divPopp').style.display = 'none';
                    });
                    $("#divdisp").html(body);
                }
            });
            return false;
        }

    </script>
    <script type="text/javascript">
        var flagno = "";
        var Groupvalues = "";
        var EndText = "";
        var imagelength = 0;
        var laststart;
        var isdisplayed = "0";
        var startIndexs = 0;

        function SetBackgroundColor(body, start, end, type, extralength, rownum, name, NoSpan, MaxCount) {
            var part1 = "";
            var part2 = "";
            $('#hdnMarkMaxCount').val(MaxCount);
            $('#lblOriginalTxt').text(name);
            var bodyDetails1 = body;
            var bodyDetails2 = body;
            var Index1 = start;
            var Index2 = end;
            var length = bodyDetails1.length;
            for (var i = 0; i < length; i++) {
                var ind1 = bodyDetails1.indexOf('<');
                var ind2 = bodyDetails1.indexOf('>');
                var len = parseInt(ind2) - parseInt(ind1);
                if ((ind1 <= Index1 || ind2 <= Index2) && ind1 == i) {
                    if (ind1 <= Index1) {
                        Index1 = parseInt(Index1) + len + 1;
                    }
                    Index2 = parseInt(Index2) + len + 1;
                    bodyDetails1 = bodyDetails1.replace('<', '$');
                    bodyDetails1 = bodyDetails1.replace('>', '$');
                }
            }
            //debugger;
            var in1 = "000000" + Index1;
            var in2 = "000000" + Index2;
            in1 = in1.substring(in1.length - 6);
            in2 = in2.substring(in2.length - 6);
            extralength = "0000" + extralength;
            extralength = extralength.substring(extralength.length - 4);
            var imgtype = type;
            imgtype = "000" + imgtype;
            imgtype = imgtype.substring(imgtype.length - 3);
            var oldstart = start;
            oldstart = "000000" + oldstart;
            oldstart = oldstart.substring(oldstart.length - 6);
            img = "";
            part1 = bodyDetails2.substring(0, Index1);
            part2 = bodyDetails2.substring(Index1, Index2);
            part3 = bodyDetails2.substring(Index2, bodyDetails2.length);
            body = part1 + img + part2 + part3;
            if ((laststart != in1) || (laststart == in1 && isdisplayed == "0")) {
                isdisplayed = "1";
                var start = in1;
                var end = in2;
                var extralength = extralength;
                start = parseInt(start, 10);
                //end = parseInt(end, 10) + ((parseInt(extralength, 10)));
                end = parseInt(end, 10) + ((parseInt(0, 10)));
                var part1 = body.substring(0, end);
                var part2 = body.substring(end, body.length);
                body = part1 + '</span' + parseInt(NoSpan, 10) + '>' + part2;
                var part1 = body.substr(0, start);
                var part2 = body.substr(start, body.length);
                if (imgtype == 4) {
                    body = part1 + "<span" + NoSpan + " class='preced'>" + part2;
                }
                if (imgtype == 5) {
                    body = part1 + "<span" + NoSpan + " class='rediod'>" + part2;
                }
                if (imgtype == 2) {
                    body = part1 + "<span" + NoSpan + " class='issuss'>" + part2;
                }
                if (imgtype == 1) {
                    body = part1 + "<span" + NoSpan + " class='factss'>" + part2;
                }
                if (imgtype == 7) {
                    body = part1 + "<span" + NoSpan + " class='highls'>" + part2;
                }
                if (imgtype == 6) {
                    body = part1 + "<span" + NoSpan + " class='orbits'>" + part2;
                }
                if (imgtype == 3) {
                    body = part1 + "<span" + NoSpan + " class='ImpPss'>" + part2;
                }
            } else {
                isdisplayed = "0";
            }
            isdisplayed = "0";
            laststart = start;
            for (var i = 1; i <= MaxCount; i++) {
                var StarSpanlength = 22;
                var EndSpanlength = 8;
                if (i > 9) {
                    if (i < 100) {
                        StarSpanlength = StarSpanlength + 1;
                        EndSpanlength = EndSpanlength + 1;
                    } else {
                        StarSpanlength = StarSpanlength + 2;
                        EndSpanlength = EndSpanlength + 2;
                    }
                }
                var SI = '<span' + i;
                var EI = '</span' + i + '>';
                var SI1 = '<span';
                var EI1 = '</span';
                var FindSI = body.indexOf(SI);
                var FindEI = body.indexOf(EI);
                if (FindSI != 0 && FindSI != -1 && FindEI != 0 && FindEI != -1) {
                    var data = body.substring(FindSI + StarSpanlength, FindEI);
                    var SIcount = occurrences(data, SI1);
                    var EIcount = occurrences(data, EI1);
                    if (SIcount != 0 && EIcount == 0) {
                        //alert('1');
                        var mSI = data.indexOf(SI1);
                        var ReplaceData = data.substring(mSI, mSI + StarSpanlength);
                        if (i < 4) {
                            var MiddleSI = body.substring(0, FindEI).indexOf(ReplaceData);
                            var part1 = body.substring(0, MiddleSI);
                            var part2 = EI;
                            var part3 = body.substring(MiddleSI + StarSpanlength, body.length).replace(EI, '');
                            var data2 = body.substring(MiddleSI + StarSpanlength, body.length)
                            var SIcount = occurrences(data2, SI1);
                            var EIcount = occurrences(data2, EI1);
                            if (SIcount == 1 && EIcount > 1) {
                                var meSI = data2.indexOf('<s');
                                var ReplaceData2 = data2.substring(meSI, meSI + StarSpanlength + EndSpanlength);
                                if ((ReplaceData2.indexOf('</')) != 0 && (ReplaceData2.indexOf('</')) != -1) {
                                    //                               var inde = ReplaceData2.indexOf('</');
                                    //                               var rep = ReplaceData2.substring(inde, inde+EndSpanlength);
                                    //                               ReplaceData2 = ReplaceData2.replace(rep, '');
                                    //                               alert(ReplaceData2);
                                    //                               var par1 = data2.substring(0, meSI);
                                    //                               var par2 = rep + ReplaceData2;
                                    //                               var par3 = data2.substring(meSI + StarSpanlength + EndSpanlength, data2.length);
                                    //                               part3 = par1 + par2 + par3;
                                } // end if indexOf('</')

                            } // end if SIcount == 1 && EIcount > 1

                        } else { // end if (i < 4)
                            //alert(data);
                            if (ReplaceData.length == StarSpanlength) {
                                part1 = body.substring(0, FindEI + EndSpanlength).replace(ReplaceData, '');
                                part3 = body.substring(FindEI + EndSpanlength, body.length);
                                part2 = "";
                            } else {
                                //                               var ReplaceDataLen = ReplaceData.length;
                                //                               var RemainLen = 0;
                                //                               //debugger;
                                //                               if (ReplaceDataLen < StarSpanlength) {
                                //                                   RemainLen = StarSpanlength - ReplaceDataLen;
                                //                               }
                                ////                               alert(RemainLen);
                                //                               part1 = body.replace(ReplaceData, '');
                                //                               var data = part1.substring(FindSI + StarSpanlength, FindEI);
                                //                               alert(data);
                                //                               part1 = part1.substring(0, FindEI + EndSpanlength);
                                //                               part3 = body.substring(FindEI + EndSpanlength, body.length);

                                part1 = body.substring(0, FindEI + EndSpanlength).replace(ReplaceData, '');
                                part3 = body.substring(FindEI + EndSpanlength, body.length);
                                part2 = "";
                            }
                        }
                        body = part1 + part2 + ReplaceData + part3;
                    } //end if SIcount != 0 && EIcount == 0
                    else if (SIcount == 0 && EIcount != 0) {
                        //                       alert('2');
                        var part1;
                        var part2;
                        var part3;
                        if (i < 4) {
                            var mSI = data.indexOf(EI1);
                            var ReplaceData = data.substring(mSI, mSI + EndSpanlength);
                            var MiddleSI = body.substring(0, FindEI).indexOf(ReplaceData);
                            part1 = body.substring(0, MiddleSI + EndSpanlength);
                            var Opentag = EI.replace('/', '').replace('>', '');
                            var FSI = part1.indexOf(Opentag);
                            var Replacestr = part1.substring(FSI, FSI + StarSpanlength);
                            part1 = part1.replace(Replacestr, '');
                            part2 = Replacestr;
                            part3 = body.substring(MiddleSI + EndSpanlength, body.length);
                        } else { // end if (i <4)
                            //alert(data);
                            var mSI = data.indexOf(EI1);
                            var ReplaceData = data.substring(mSI, mSI + EndSpanlength);
                            part1 = body.substring(0, FindEI + EndSpanlength).replace(ReplaceData, '');
                            part2 = ReplaceData;
                            part3 = body.substring(FindEI + EndSpanlength, body.length);
                        }
                        body = part1 + part2 + part3;
                    } else if (SIcount == 1 && EIcount == 2) {
                        //alert('3');
                        //alert(data);
                        var mEI = data.indexOf(EI1);
                        var mSI = data.indexOf(SI1);
                        if (mSI > mEI) {
                            //                           var ReplaceStr = data.substring(mEI, mEI + EndSpanlength);
                            //                           var part1 = body.substring(0, FindSI);
                            //                           var part2 = body.substring(FindSI, body.length).replace(ReplaceStr, '');
                            //                           body = part1 + ReplaceStr + part2;

                            var ReplaceStr = data.substring(mEI, mEI + EndSpanlength);
                            var meEI = body.indexOf(ReplaceStr);
                            var part1 = body.substring(0, meEI + EndSpanlength);
                            var meSI = part1.indexOf(SI);
                            var ReplaceSpan = part1.substring(meSI, meSI + StarSpanlength);
                            var part1 = part1.replace(ReplaceSpan, '');
                            var part2 = body.substring(meEI + EndSpanlength, body.length);
                            body = part1 + ReplaceSpan + part2;

                        } // end if mSI > mEI
                        else if (mSI < mEI) {
                            //alert(data);
                            var Str = data.substring(mEI + EndSpanlength, FindEI);
                            var StrEI = Str.indexOf(EI1)
                            var ReplaceStr = Str.substring(StrEI, StrEI + EndSpanlength);
                            var part1 = body.substring(0, FindSI);
                            var part2 = body.substring(FindSI, FindEI).replace(ReplaceStr, '');
                            var part3 = body.substring(FindEI, body.length).replace(ReplaceStr, '');
                            body = part1 + ReplaceStr + part2 + part3;
                            //                           var Str = data.substring(mEI + EndSpanlength, FindEI);
                            //                           var StrEI = Str.indexOf(EI1)
                            //                           var ReplaceStr = Str.substring(StrEI, StrEI + EndSpanlength);
                            //                           var part1 = body.substring(0, FindSI);
                            //                           var part2 = body.substring(FindSI, FindEI);//.replace(ReplaceStr, '');
                            //                           var part3 = body.substring(FindEI, body.length);//.replace(ReplaceStr, '');
                            //                           body = part1 + part2 + part3;
                        }
                    } // end else if (SIcount == 1 && EIcount == 2)
                    else if (SIcount > EIcount) {
                        //alert('4');
                        var OpenTag = body.substring(FindEI, FindEI - 1);
                        if (OpenTag == '<') {
                            var part1 = body.substring(0, FindEI);
                            var part2 = body.substring(FindEI, body.length);
                            var ReplaceStr = part2.substring(0, EndSpanlength);
                            part2 = part2.replace(ReplaceStr, '');
                            var CloseTag = part2.indexOf('>');
                            var datas = insert(part2, CloseTag + 1, ReplaceStr);
                            body = part1 + datas;
                        }
                    } else if (SIcount == 1 && EIcount == 1) {
                        //alert('5');
                        var OpenTag = body.substring(FindEI, FindEI - 1);
                        if (OpenTag == '<') {
                            var part1 = body.substring(0, FindEI);
                            var part2 = body.substring(FindEI, body.length);
                            var ReplaceStr = part2.substring(0, EndSpanlength);
                            part2 = part2.replace(ReplaceStr, '');
                            var CloseTag = part2.indexOf('>');
                            var datas = insert(part2, CloseTag + 1, ReplaceStr);
                            body = part1 + datas;
                        }
                    } else if (SIcount == 0 && EIcount == 0) {
                        var In = data.indexOf('<');
                        if (In != 0 && In != -1) {
                            //alert('6');
                            if ((data.substring(In, In + 4)) != '<br>') {
                                var Str = data.substring(In, In + 6);
                                data = data.replace(Str, '');
                                var part1 = body.substring(0, FindSI + StarSpanlength);
                                var part2 = body.substring(FindEI, FindEI + EndSpanlength);
                                var part3 = body.substring(FindEI + EndSpanlength, body.length);
                                body = part1 + data + part2 + Str + part3;
                            } // end if of <br> not equal.
                        } // end if In != 0 && In != -1
                    } // end if SIcount == 0 && EIcount == 0
                } // end if
            } // end for

            $("#divdisp").html(body);
            imagelength = 2;
            imgtype = parseInt(imgtype);
            return body;
        }

        function occurrences(string, subString, allowOverlapping) {
            string += "";
            subString += "";
            if (subString.length <= 0) return (string.length + 1);
            var n = 0,
                pos = 0,
                step = allowOverlapping ? 1 : subString.length;

            while (true) {
                pos = string.indexOf(subString, pos);
                if (pos >= 0) {
                    ++n;
                    pos += step;
                } else break;
            }
            return n;
        }

        function insert(str, index, value) {
            return str.substr(0, index) + value + str.substr(index);
        }
    </script>
    <script type="text/javascript" language="JavaScript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

        var browserName = navigator.appName;

        function ShowSummaryPost(ContentId, SummaryText) {
            var ContentId = '<%= Request.QueryString["cId"]%>';
            var OtherUID = $('#intCommentAddedFors').val();
            var AddedBy = document.getElementById('hdnLoginId').value;
            //if (OtherUID != '') {
            //    AddedBy = OtherUID;
            //} else {}

            document.getElementById("hdnTagtypeId").value = "8";
            //  document.getElementById("DivSummary").style.display = 'block';
            var file_type = 'Summary';
            //   document.getElementById("DivSummary").style.display = 'none';
            $.ajax({
                contentType: "text/html; charset=utf-8",
                data: "{}",
                url: "handler/GetSummaryDetails.ashx?ContentId=" + ContentId + "&TagType=" + file_type + "&AddedBy=" + AddedBy,
                dataType: "html",
                success: function (data) {
                    if (data.length > 0) {
                        $('#lnkWriteButton').hide();
                        //  $('#txtShortSum').text(data); 
                        $('#sumDetails').text(data);
                        $('#lnkWriteButton').hide();
                        $('#divSumByyou').show();
                        $('#txtSummary').text(data);
                    } else {
                        $('#lnkWriteButton').show();
                    }
                }
            });
            var sid = ContentId;
            var add = AddedBy;
            document.getElementById('<%=hdnSummaryTextPost.ClientID%>').value = SummaryText;
        }

        function ShowSummaryPostLoad(ContentId, SummaryText) {
            var ContentId = '<%= Request.QueryString["cId"]%>';
            var AddedBy = document.getElementById('hdnLoginId').value;
            document.getElementById("hdnTagtypeId").value = "8";
            var file_type = 'Summary';
            $.ajax({
                contentType: "text/html; charset=utf-8",
                data: "{}",
                url: "handler/GetSummaryDetails.ashx?ContentId=" + ContentId + "&TagType=" + file_type + "&AddedBy=" + AddedBy,
                dataType: "html",
                success: function (data) {
                    if (data.length > 0) {
                        $('#lnkWriteButton').hide();
                        //  $('#txtShortSum').text(data);
                        $('#txtSummary').text(data);
                        $('#sumDetails').text(data);
                        $('#lnkWriteButton').hide();
                        $('#divSumByyou').show();
                            //var div = document.getElementById('<%=txtSummary.ClientID%>');
                        //div.innerHTML = data;
                    } else {
                        $('#lnkWriteButton').show();
                        //   $('#txtSummary').attr('placeholder', 'Write your summary here...');
                    }
                }
            });
            var sid = ContentId;
            var add = AddedBy;
            document.getElementById('<%=hdnSummaryTextPost.ClientID%>').value = SummaryText;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#divdisp').focusout(function () {
                $(this).attr('spellcheck', false);
                forceSpellcheck($(this));
            });
            var canCheck = true;

            function forceSpellcheck($textarea) {
                if (canCheck) {
                    canCheck = false;
                    $textarea.focus();
                    $textarea.attr('spellcheck', false);
                    var characterCount = $textarea.val().length;

                    var selection = window.getSelection();
                    for (var i = 0; i < characterCount; i++) {
                        selection.modify("move", "backward", "character");
                    }
                }
            }
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.getElementById("divdisp").onmouseup = function () {
                getSelectionCharacterOffsetsWithin(this);
            };
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                //     ShowSummaryPost();
                $("body").removeClass("overflowHidden");
                document.getElementById("divdisp").onmouseup = function () {
                    getSelectionCharacterOffsetsWithin(this);
                };
            });
        });

        function getSelectionCharacterOffsetsWithin(element) {
            var txt = '';
            var htmlText = '';
            var startOffset = 0,
                endOffset = 0;
            document.getElementById('DivMenuBar').style.display = 'none';
            if (typeof window.getSelection != "undefined") {
                var range = window.getSelection().getRangeAt(0);
                var preCaretRange = range.cloneRange();
                preCaretRange.selectNodeContents(element);
                preCaretRange.setEnd(range.startContainer, range.startOffset);
                startOffset = preCaretRange.toString().length;
                endOffset = startOffset + range.toString().length;
                if (range != '') {
                    getSelctedtexts(startOffset, endOffset, range);
                }
            } else if (typeof document.selection != "undefined" &&
                document.selection.type != "Control") {
                var textRange = document.selection.createRange();
                var preCaretTextRange = document.body.createTextRange();
                preCaretTextRange.moveToElementText(element);
                preCaretTextRange.setEndPoint("EndToStart", textRange);
                startOffset = preCaretTextRange.text.length;
                endOffset = startOffset + textRange.text.length;
                if (range != '') {
                    getSelctedtexts(startOffset, endOffset, textRange);
                }
            }
        } // End function getSelectionCharacterOffsetsWithin(element)

        function getSelctedtexts(startOffset, endOffset, textRange) {
            var txt = '';
            var htmlText = '';
            var range;
            var temp = 1;
            var browserName = navigator.appName;
            htmlText = textRange;
            document.getElementById('hdnSlectedText').value = htmlText;
            document.getElementById('hdnMainSelectedText').value = htmlText;
            if (htmlText != "") {
                document.getElementById("DivSummary").style.display = 'none';
            } else {
                document.getElementById('DivMenuBar').style.display = 'none';
            }
            document.getElementById("hdnSelectionCount").value = 1;
            if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1) {
                document.getElementById("hdnhtmlSelectedText").value = htmlText;
                document.getElementById("hdnMarkText").value = textRange;
            }

            if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1) { document.getElementById("hdnStartIdx").value = startOffset; } // -1; //newH TMLSel.toLowerCase();//  LcDivBodyText.indexOf(newHTMLSel.toLowerCase());
            var intstart = 0;
            try {
                intstart = parseInt(document.getElementById("hdnStartIdx").value);
            } catch (err) { }
            if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1) { document.getElementById("hdnEndIdx").value = endOffset; }

            if (document.getElementById("hdnStartIdx").value >= 0 && document.getElementById("hdnEndIdx").value >= 0)
                document.getElementById("hdnSelectionCount").value = 0;

            colour = document.getElementById("hdncolour").value;
            ///---------For selection check
            if (txt != "" && htmlText != "") {
                document.getElementById("hdnAttemptselection").value = '2';
            } else {
                document.getElementById("hdnAttemptselection").value = '0';
            }

            if (document.selection) {
                if (document.selection.type == "Text") {
                    document.execCommand("BackColor", false, colour);
                    document.selection.empty();
                    temp = 1;
                }
            } else if (window.getSelection) {
                var sel = window.getSelection();
                temp = 1;
                if (!sel.isCollapsed) {
                    range = sel.getRangeAt(0);
                    document.designMode = "on";
                    //sel.removeAllRanges();
                    sel.addRange(range);
                    if (txt != "")
                        document.execCommand("HiliteColor", false, colour);
                    document.designMode = "off";
                    //sel.removeAllRanges();
                }
            }


            var widthnew = getBrowserWidth();
            var a = parseInt(document.body.offsetHeight);
            var x = parseInt(document.getElementById("hdnX").value) + 460;
            var NewX = parseInt(document.getElementById("hdnX").value);
            if (widthnew < x) {
                NewX = widthnew - 480;
            }
            if (temp == "1") //document.getElementById("hdncolour").value == "#C6DEFF"
            {
                document.getElementById('DivMenuBar').style.position = "fixed";
                document.getElementById('DivMenuBar').style.zIndex = "100000";
                document.getElementById('DivMenuBar').style.display = 'block';
                $("#DivMenuBar").css({
                    position: "fixed",
                    top: document.getElementById("hdnY").value + "px",
                    //left: -635 + "px",
                    top: 850 + "px"
                }).show();
            }
            if (htmlText != "") {
                var AddedBy = document.getElementById('hdnLoginId').value;
                var CommentAddedFor = document.getElementById('hdnCommentAddedFor').value;
                if (CommentAddedFor != '') {
                    if (AddedBy != CommentAddedFor) {
                        document.getElementById('DivMenuBar').style.display = 'none';
                    } else {
                        document.getElementById('DivMenuBar').style.display = 'block';
                    }
                } else {
                    document.getElementById('DivMenuBar').style.display = 'block';
                }
            } else {
                document.getElementById('DivMenuBar').style.display = 'none';
            }

        } // End function getSelctedtexts()


        // OLD SELECTION CONTENT CODE  6 Nov 2015
        //        function highlightSelection(colour) {
        //            var txt = '';
        //            var htmlText = '';
        //            var range;
        //            var temp = 0;
        //            var browserName = navigator.appName;
        //            $(".h2").removeAttr("title");
        //            if (window.getSelection) {
        //                txt = window.getSelection();
        //                if (txt.rangeCount > 0) {
        //                    range = txt.getRangeAt(0);
        //                    var clonedSelection = range.cloneContents();
        //                    var div = document.createElement('div');
        //                    div.appendChild(clonedSelection);
        //                    htmlText = $(div).text(); //div.innerHTML;
        //                    document.getElementById('hdnSlectedText').value = htmlText;
        //                    document.getElementById('hdnMainSelectedText').value = htmlText;
        //                    if (htmlText != "") {
        //                        document.getElementById("DivSummary").style.display = 'none';
        //                    }
        //                    else {
        //                        document.getElementById('DivMenuBar').style.display = 'none';
        //                    }
        //                }
        //            }
        //            else if (document.getSelection) {
        //                txt = document.getSelection();
        //                range = document.selection.createRange();
        //                htmlText = range.text; //range.htmlText;
        //                document.getElementById('hdnSlectedText').value = htmlText;
        //                document.getElementById('txtIssue').value = document.getElementById('hdnSlectedText').value;

        //            }
        //            else if (document.selection) {
        //                txt = document.selection.createRange().Text;
        //                range = document.selection.createRange();
        //                htmlText = range.text; //range.htmlText;
        //                txt = range.text; //range.htmlText;
        //                var SlectedText = range.text;
        //                document.getElementById('hdnSlectedText').value = SlectedText;
        //                document.getElementById('txtIssue').value = document.getElementById('hdnSlectedText').value;
        //            }
        //            document.getElementById("hdnSelectionCount").value = 1;
        //            if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1) {
        //                document.getElementById("hdnhtmlSelectedText").value = htmlText;
        //                document.getElementById("hdnMarkText").value = txt;
        //            }
        //            var DivBodyText = $("#divdisp").text();
        //            var LcDivBodyText = DivBodyText.toLowerCase();
        //            //LcDivBodyText = LcDivBodyText.replace(/(^\s*)|(\s*$)/gi, "");
        //            LcDivBodyText = LcDivBodyText.replace(/[ ]{2,}/gi, " ");
        //            LcDivBodyText = LcDivBodyText.replace(/\n /, "\n");
        //            if (browserName == 'Microsoft Internet Explorer') {
        //                var IdIndex = -1, ClassIndex = -1;
        //                var ActualId = '', BodyForID = '', ClasName = '';
        //                BodyForID = LcDivBodyText;
        //                while (BodyForID.indexOf("id=") > 0) {
        //                    BodyForID = BodyForID.substring(BodyForID.indexOf("id="));
        //                    ActualId = BodyForID.substring(BodyForID.indexOf("id=") + 3, BodyForID.indexOf(" class="));

        //                    while (LcDivBodyText.indexOf("id=" + ActualId) > 0 && IdIndex < LcDivBodyText.indexOf("id=" + ActualId) && !ActualId.startsWith("\"")) {
        //                        LcDivBodyText = LcDivBodyText.replace("id=" + ActualId, "id=\"" + ActualId + "\"");
        //                    }
        //                    BodyForID = BodyForID.substring(BodyForID.indexOf("id=") + 3);
        //                }
        //                BodyForID = LcDivBodyText;

        //                while (BodyForID.indexOf("class=") > 0) {
        //                    BodyForID = BodyForID.substring(BodyForID.indexOf("class="));
        //                    var classdetails = BodyForID.substring(BodyForID.indexOf("class=") + 6, BodyForID.indexOf(">"));
        //                    ClasName = BodyForID.substring(BodyForID.indexOf("class=") + 6, BodyForID.indexOf(">"));
        //                    ClasName = classdetails.substring(0, 2);
        //                    while (LcDivBodyText.indexOf(" class=" + ClasName) > 0 && !ClasName.startsWith("\"")) {
        //                        LcDivBodyText = LcDivBodyText.replace(" class=" + ClasName, " class=\"" + ClasName + "\"");
        //                    }
        //                    if (!ClasName.startsWith("\""))
        //                        BodyForID = BodyForID.substring(BodyForID.indexOf("class=") + 7);
        //                }
        //                BodyForID = LcDivBodyText;
        //                while (BodyForID.indexOf("onclick=") > 0) {
        //                    BodyForID = BodyForID.substring(BodyForID.indexOf("onclick="));
        //                    var Clickdetails = BodyForID.substring(BodyForID.indexOf("onclick=") + 8, BodyForID.indexOf(">"));
        //                    ClasName = Clickdetails.substring(0, Clickdetails.indexOf(");") + 2);
        //                    while (LcDivBodyText.indexOf("onclick=" + ClasName) > 0 && !ClasName.startsWith("\"")) {
        //                        LcDivBodyText = LcDivBodyText.replace("onclick=" + ClasName, "onclick=\"" + ClasName + "\"");
        //                    }
        //                    BodyForID = BodyForID.substring(BodyForID.indexOf("onclick=") + 9);
        //                }
        //            }
        //            var BodyHtmlRepl = '';
        //            BodyHtmlRepl = htmlText;
        //            while (BodyHtmlRepl.indexOf("class=") > 0) {
        //                BodyHtmlRepl = BodyHtmlRepl.substring(BodyHtmlRepl.indexOf("class="));
        //                var classdetails = BodyHtmlRepl.substring(BodyHtmlRepl.indexOf("class=") + 6, BodyHtmlRepl.indexOf(">"));
        //                ClasName = BodyHtmlRepl.substring(BodyHtmlRepl.indexOf("class=") + 6, BodyHtmlRepl.indexOf(">"));
        //                ClasName = classdetails.substring(0, 2);
        //                while (htmlText.indexOf("class=" + ClasName) > 0 && !ClasName.startsWith("\"")) {
        //                    htmlText = htmlText.replace("class=" + ClasName, "class=\"" + ClasName + "\"");
        //                }
        //                if (!ClasName.startsWith("\""))
        //                    BodyHtmlRepl = BodyHtmlRepl.substring(BodyForID.indexOf("class=") + 7);
        //            }
        //            var lchtmlText = htmlText.toLowerCase();
        //            var newHTMLSel = "";
        //            newHTMLSel = htmlText;
        //            var browserName = navigator.appName;
        //            var lcNewHTML = newHTMLSel.toLowerCase().trim();
        //            //lcNewHTML = lcNewHTML.replace(/(^\s*)|(\s*$)/gi, "");
        //            lcNewHTML = lcNewHTML.replace(/[ ]{2,}/gi, " ");
        //            lcNewHTML = lcNewHTML.replace("\n", "");
        //            lcNewHTML = lcNewHTML.replace("\r\n", "");
        //            lcNewHTML = lcNewHTML.replace("\r", "").replace(/\n/g, "").replace(/\r/g, "");
        //            while (LcDivBodyText.indexOf("<br> ") > 0) {
        //                LcDivBodyText = LcDivBodyText.replace("<br> ", "<br>");
        //            }
        //            while (LcDivBodyText.indexOf(" <br>") > 0) {
        //                LcDivBodyText = LcDivBodyText.replace(" <br>", "<br>");
        //            }
        //            while (LcDivBodyText.indexOf("\r\n") > 0) {
        //                LcDivBodyText = LcDivBodyText.replace("\r\n", "");
        //            }
        //            if (lchtmlText.indexOf("</span>") >= 0 || lchtmlText.indexOf("<span ") >= 0) {
        //                var index = -1;
        //                var SelLength = lchtmlText.length;

        //                if (lchtmlText.substr(SelLength - 8, SelLength).indexOf("</span>") >= 0 && lchtmlText.substr(0, 7).indexOf("<span ") >= 0) {

        //                    index = htmlText.indexOf(">");
        //                    newHTMLSel = htmlText.substr(index + 1);
        //                    if (browserName != 'Microsoft Internet Explorer') {
        //                        while (newHTMLSel.substr(0, 7).indexOf("<span ") >= 0) {
        //                            index = newHTMLSel.indexOf(">");
        //                            newHTMLSel = newHTMLSel.substr(index + 1);
        //                        }
        //                    }
        //                    else {
        //                        while (newHTMLSel.substr(0, 7).indexOf("<SPAN ") >= 0) {
        //                            index = newHTMLSel.indexOf(">");
        //                            newHTMLSel = newHTMLSel.substr(index + 1);
        //                        }
        //                    }
        //                    var SelLen = newHTMLSel.length;
        //                    index = lchtmlText.indexOf("</s");
        //                    newHTMLSel = newHTMLSel.substr(0, SelLen - 7);
        //                    if (browserName != 'Microsoft Internet Explorer') {
        //                        while (newHTMLSel.substr(SelLen - 8, SelLen).indexOf("</span>") >= 0) {
        //                            index = newHTMLSel.indexOf("</s");
        //                            newHTMLSel = newHTMLSel.substr(0, SelLen - 7);
        //                        }
        //                    }
        //                    else {
        //                        while (newHTMLSel.substr(SelLen - 8, SelLen).indexOf("</SPAN>") >= 0) {
        //                            index = newHTMLSel.indexOf("</S");
        //                            newHTMLSel = newHTMLSel.substr(0, SelLen - 7);
        //                        }
        //                    }
        //                } else if (lchtmlText.substr(0, 7).indexOf("<span ") >= 0) {
        //                    index = htmlText.indexOf(">");
        //                    newHTMLSel = htmlText.substr(index + 1);
        //                    if (browserName != 'Microsoft Internet Explorer') {
        //                        while (newHTMLSel.substr(0, 7).indexOf("<span ") >= 0) {
        //                            index = newHTMLSel.indexOf(">");
        //                            newHTMLSel = newHTMLSel.substr(index + 1);
        //                        }
        //                    }
        //                    else {
        //                        while (newHTMLSel.substr(0, 7).indexOf("<SPAN ") >= 0) {
        //                            index = newHTMLSel.indexOf(">");
        //                            newHTMLSel = newHTMLSel.substr(index + 1);
        //                        }
        //                    }
        //                } else if (lchtmlText.substr(SelLength - 8, SelLength).indexOf("</span>") >= 0) {
        //                    index = lchtmlText.indexOf("</s");
        //                    newHTMLSel = htmlText.substr(0, index);
        //                    if (browserName != 'Microsoft Internet Explorer') {
        //                        while (newHTMLSel.substr(SelLength - 8, SelLength).indexOf("</span>") >= 0) {
        //                            index = newHTMLSel.indexOf("</s");
        //                            newHTMLSel = newHTMLSel.substr(0, index);
        //                        }
        //                    }
        //                    else {
        //                        while (newHTMLSel.substr(SelLength - 8, SelLength).indexOf("</SPAN>") >= 0) {
        //                            index = newHTMLSel.indexOf("</S");
        //                            newHTMLSel = newHTMLSel.substr(0, index);
        //                        }
        //                    }
        //                }
        //                else {
        //                    newHTMLSel = htmlText;
        //                }
        //                var LCFinal = newHTMLSel.toLowerCase();
        //                LCFinal = LCFinal.replace(/(^\s*)|(\s*$)/gi, "");
        //                LCFinal = LCFinal.replace(/[ ]{2,}/gi, " ");
        //                LCFinal = LCFinal.replace(/\n /, "\n");
        //                while (LCFinal.indexOf("\r\n") > 0) {
        //                    LCFinal = LCFinal.replace("\r\n", "");
        //                }
        //                LCFinal = LCFinal.replace(/(\r\n|\n|\r)/gm, " ");
        //                while (LCFinal.indexOf("<br> ") > 0) {
        //                    LCFinal = LCFinal.replace("<br> ", "<br>");
        //                }
        //                while (LCFinal.indexOf(" <br>") > 0) {
        //                    LCFinal = LCFinal.replace(" <br>", "<br>");
        //                }
        //                if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1)
        //                { document.getElementById("hdnStartIdx").value = LcDivBodyText.indexOf(LCFinal); } // -1; //newH TMLSel.toLowerCase();//  LcDivBodyText.indexOf(newHTMLSel.toLowerCase());
        //                var intstart = 0;
        //                try {
        //                    intstart = parseInt(document.getElementById("hdnStartIdx").value);
        //                }
        //                catch (err) {
        //                }
        //                if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1)
        //                { document.getElementById("hdnEndIdx").value = intstart + (LCFinal.length); }
        //            }
        //            else {
        //                while (lcNewHTML.indexOf("\r\n") > 0) {
        //                    lcNewHTML = lcNewHTML.replace("\r\n", "");
        //                }

        //                while (lcNewHTML.indexOf("<br> ") > 0) {
        //                    lcNewHTML = lcNewHTML.replace("<br> ", "<br>");
        //                }
        //                while (lcNewHTML.indexOf(" <br>") > 0) {
        //                    lcNewHTML = lcNewHTML.replace(" <br>", "<br>");
        //                }
        //                if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1)
        //                { document.getElementById("hdnStartIdx").value = LcDivBodyText.indexOf(lcNewHTML); }
        //                var intstart = 0;
        //                try {
        //                    intstart = parseInt(document.getElementById("hdnStartIdx").value);
        //                }
        //                catch (err) {
        //                }
        //                if (document.getElementById("hdnSelectionCount").value != null && document.getElementById("hdnSelectionCount").value == 1)
        //                { document.getElementById("hdnEndIdx").value = intstart + lcNewHTML.length; }
        //            }
        //            if (document.getElementById("hdnStartIdx").value >= 0 && document.getElementById("hdnEndIdx").value >= 0)
        //                document.getElementById("hdnSelectionCount").value = 0;

        //            colour = document.getElementById("hdncolour").value;
        //            ///---------For selection check
        //            if (txt != "" && htmlText != "") {
        //                document.getElementById("hdnAttemptselection").value = '2';
        //            }
        //            else {
        //                document.getElementById("hdnAttemptselection").value = '0';
        //            }
        //            //////End code Attempt selection 
        //            if (document.selection) {
        //                // IE case 
        //                if (document.selection.type == "Text") {
        //                    document.execCommand("BackColor", false, colour);
        //                    document.selection.empty();
        //                    temp = 1;
        //                }
        //            } else if (window.getSelection) {
        //                var sel = window.getSelection();
        //                temp = 1;
        //                if (!sel.isCollapsed) {
        //                    range = sel.getRangeAt(0);
        //                    document.designMode = "on";
        //                    sel.removeAllRanges();
        //                    sel.addRange(range);
        //                    if (txt != "")
        //                        document.execCommand("HiliteColor", false, colour);
        //                    document.designMode = "off";
        //                    sel.removeAllRanges();
        //                }
        //            }
        //            var widthnew = getBrowserWidth();
        //            var a = parseInt(document.body.offsetHeight);
        //            var x = parseInt(document.getElementById("hdnX").value) + 460;
        //            var NewX = parseInt(document.getElementById("hdnX").value);
        //            if (widthnew < x) {
        //                NewX = widthnew - 480;
        //            }
        //            if (temp == "1")  //document.getElementById("hdncolour").value == "#C6DEFF"
        //            {
        //                document.getElementById('DivMenuBar').style.position = "fixed";
        //                document.getElementById('DivMenuBar').style.zIndex = "100000";
        //                document.getElementById('DivMenuBar').style.display = 'block';
        //                $("#DivMenuBar").css({
        //                    position: "fixed",
        //                    top: document.getElementById("hdnY").value + "px",
        //                    //left: -635 + "px",
        //                    top: 850 + "px"
        //                }).show();
        //            }
        //            if (htmlText != "") {

        //                var AddedBy = document.getElementById('hdnLoginId').value;
        //                var CommentAddedFor = document.getElementById('hdnCommentAddedFor').value;
        //                if (CommentAddedFor != '') {
        //                    if (AddedBy != CommentAddedFor) {
        //                        document.getElementById('DivMenuBar').style.display = 'none';
        //                    } else {
        //                        document.getElementById('DivMenuBar').style.display = 'block';
        //                    }
        //                } else {
        //                    document.getElementById('DivMenuBar').style.display = 'block';
        //                }
        //            }
        //            else {
        //                document.getElementById('DivMenuBar').style.display = 'none';
        //            }
        //        }

        function getBrowserWidth() {
            var myWidth = 0;
            if (typeof (window.innerWidth) == 'number') {
                myWidth = window.innerWidth; //Non-IE
            } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                myWidth = document.documentElement.clientWidth; //IE 6+ in 'standards compliant mode'
            } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                myWidth = document.body.clientWidth; //IE 4 compatible
            }
            return myWidth;
        }
    </script>
    <script type="text/javascript">
        if ($(window).width() <= 767) {


            $('#DivMenuBar').addClass('postion_fixed');


        }
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            ShowSummaryPostLoad();
        });
    </script>
    <script type="text/javascript" language="javascript">
        function Cancel() {
            $('#txtSummary').val($('#ctl00_ContentPlaceHolder1_sumDetails').text());
            //document.getElementById("DivMenuBar").style.display = 'none';
            document.getElementById("DivSummary").style.display = 'none';
            document.getElementById("PopUpShare").style.display = 'none';
            document.getElementById("PopUpGmailShare").style.display = 'none';
            document.getElementById("popupWhatsappShare").style.display = 'none';
            //$('#txtBody').text('');
            //$('#txtLink').text('');
            // return false;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var ID = "#" + $("#hdnTabId").val();
            $(ID).focus();
        });
    </script>
    <script type="text/javascript">
        function getMultipleValues(ctrlId) {
            $('#tdDepartment').find('label.error').remove();
            var control = document.getElementById(ctrlId);
            var strSelText = '';
            var cnt = 0;
            for (var i = 0; i < control.length; i++) {
                if (control.options[i].selected) {
                    if (cnt == 0) {
                        strSelText += control.options[i].value;
                    } else {
                        strSelText += ',' + control.options[i].value;
                    }
                    cnt++;
                }
            }
            $('#hdnInvId').val(strSelText);
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#DivSummary').center();
            //  var placeholder = document.getElementById("txtSummary").getAttribute("placeholder");
            var nAgt = navigator.userAgent;
            if ((verOffset = nAgt.indexOf("MSIE")) != -1) {
                var browserName = "Microsoft Internet Explorer";
                var fullVersion = nAgt.substring(verOffset + 5);
            }
        });



        function writeSummary() {
            $("#DivSummary").css('display', 'block');
        }
    </script>
    <script type="text/javascript">
        function historyBack() {
            var b = $('#hdnBack').val();
            event.preventDefault();
            history.go(-b);
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

        function callchosencss() {
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
        }
    </script>
    <script type="text/javascript">
        function getMultipleValues(ctrlId) {
            $('#tdDepartments').find('label.error').remove();
            var control = document.getElementById(ctrlId);
            var strSelTexts = '';
            var cnt = 0;
            for (var i = 0; i < control.length; i++) {
                if (control.options[i].selected) {
                    if (cnt == 0) {
                        strSelTexts += control.options[i].value;
                    } else {
                        strSelTexts += ',' + control.options[i].value;
                    }
                    cnt++;
                }
            }
            $('#hdnInvId').val(strSelTexts);
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var ID = "#" + $("#hdnTabId").val();
            $(ID).focus();
        });

        function ShowLoading(id) {
            location.href = '#' + id;
        }

        function DownLoadClicks() {
            if (/iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream) {
                alert("This feature isn't supported on this device.");
                return false;
            }
            var data = $("#divdisp").html();
            data = data.replace(/colspan="&quot;&quot;"/gm, "").replace(/colspan="&quot;0&quot;"/gm, "");
            data = data.replace(/rowspan="&quot;&quot;"/gm, "").replace(/rowspan="&quot;0&quot;"/gm, "");
            $("#hdnDivContent").val(data);

        }
        function shareOverWhatsAppEmailClicks() {

            var data = $("#divdisp").html();
            data = data.replace(/colspan="&quot;&quot;"/gm, "").replace(/colspan="&quot;0&quot;"/gm, "");
            data = data.replace(/rowspan="&quot;&quot;"/gm, "").replace(/rowspan="&quot;0&quot;"/gm, "");
            $("#hdnDivContent").val(data);

        }

        function callDivHandler() {
            var caseId = '<%= Request.QueryString["cId"]%>';
            $.ajax({
                url: "handler/PDFDownload.ashx?ContentId=" + caseId,
                dataType: "html",
                success: function (data) { return true },
                error: function (data) { return true }
            });
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#lnkFactTab").one('click', function (event) {
                document.getElementById('DivMenuBar').style.display = 'none';
                document.getElementById('divPopp').style.display = 'block';
            });
            $("#lnkIssueTab").one('click', function (event) {
                document.getElementById('DivMenuBar').style.display = 'none';
                document.getElementById('divPopp').style.display = 'block';
            });
            $("#lnkImparagrph").one('click', function (event) {
                document.getElementById('DivMenuBar').style.display = 'none';
                document.getElementById('divPopp').style.display = 'block';
            });
            $("#lnkPrecedent").one('click', function (event) {
                document.getElementById('DivMenuBar').style.display = 'none';
                document.getElementById('divPopp').style.display = 'block';
            });
            $("#lnkDecidendit").one('click', function (event) {
                document.getElementById('DivMenuBar').style.display = 'none';
                document.getElementById('divPopp').style.display = 'block';
            });
            $("#lnkOrbite").one('click', function (event) {
                document.getElementById('DivMenuBar').style.display = 'none';
                document.getElementById('divPopp').style.display = 'block';
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("#lnkFactTab").one('click', function (event) {
                    document.getElementById('DivMenuBar').style.display = 'none';
                    document.getElementById('divPopp').style.display = 'block';
                });
                $("#lnkIssueTab").one('click', function (event) {
                    document.getElementById('DivMenuBar').style.display = 'none';
                    document.getElementById('divPopp').style.display = 'block';
                });
                $("#lnkImparagrph").one('click', function (event) {
                    document.getElementById('DivMenuBar').style.display = 'none';
                    document.getElementById('divPopp').style.display = 'block';
                });
                $("#lnkPrecedent").one('click', function (event) {
                    document.getElementById('DivMenuBar').style.display = 'none';
                    document.getElementById('divPopp').style.display = 'block';
                });
                $("#lnkDecidendit").one('click', function (event) {
                    document.getElementById('DivMenuBar').style.display = 'none';
                    document.getElementById('divPopp').style.display = 'block';
                });
                $("#lnkOrbite").one('click', function (event) {
                    document.getElementById('DivMenuBar').style.display = 'none';
                    document.getElementById('divPopp').style.display = 'block';
                });
            });



            var _fileName = $('#hdnIsCalledWhatsAppMethod').val();
            if (_fileName != '' && _fileName != '0') {

                if (_fileName == '') {
                    alert('Session Expired');
                    window.location.href = window.location.href;
                    return false;
                }
                var _UserName = '<%= Convert.ToString(Session["LoginName"]) %>';
                var _lblCaseTitle = $('#lblCaseTitle').html();

                var number = Math.floor(Math.random() * 90000) + 10000;

                var _path = window.location.host + '/SharingDoc/' + _fileName + '?_docfile=' + number + '';
                var message = _UserName + " has shared a case with you, titled " + _lblCaseTitle + "   " + _path;
                $('#hdnIsCalledWhatsAppMethod').val('');
                window.location.href = "whatsapp://send?text=" + message + "";

            }
            else {
                $('#hdnIsCalledWhatsAppMethod').val('');
            }
        });
        function OverlayBody() {
            $('#bodyelement').addClass("remove-scroller");
        }
        function CallWriteSummary() {
            //$('#lnkWriteButton').css("box-shadow", "0px 0px 5px #00B7E5");
            //  OverlayBody();
        }

        function CallSaveSummaery() {
            $('#BtnSaveSummary').css("box-shadow", "0px 0px 5px #00B7E5");
        }

        function CallSaveComment() {
            document.getElementById('DivMenuBar').style.display = 'none';
            document.getElementById('divPopp').style.display = 'block';
            $('#ctl00_ContentPlaceHolder1_BtnSaveComment').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtComment').text() == '') {
                document.getElementById('divPopp').style.display = 'none';
                setTimeout(
                    function () {
                        $('#ctl00_ContentPlaceHolder1_BtnSaveComment').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
        }

        function CallLoadImg() {
            document.getElementById('divPopp').style.display = 'none';
        }

        function callFvrtimg() {
            if ($('#imgFvrt').attr("src") == 'images/gray-tag.png') {
                $("#imgFvrt").attr("src", 'images/bookmark_newone.png');
            } else {
                $("#imgFvrt").attr("src", 'images/gray-tag.png');
            }
        }

        function callCommentDiv() {
            document.getElementById('DivCommentContent').style.display = 'block';
            var divPosition = $('.CommentSections').offset();
            $('html, body').animate({ scrollTop: divPosition.top }, "slow");
        }
        <%--function shareUrlOverWhatsapp() {
            //alert('WhatsApp oh ho!!');
            DownLoadClicks();
            var _UserId = '<%= Convert.ToString(Session["ExternalUserId"]) %>';
            var _UserName = '<%= Convert.ToString(Session["LoginName"]) %>';
            var _ContentID = '<%= Request.QueryString["cId"].Trim() %>';
            var _hdnDivContent = '';//$('#hdnDivContent').val();
            var _hdnMarkMaxCount = $('#hdnMarkMaxCount').val();
            var _lblCaseTitle = $('#lblCaseTitle').html();
            var ip = $('#hdnIpAddress').val();
            //$("#btnTest").trigger('click');
            if (_UserId == '') {
                window.location.href = window.location.href;
                return false;
            }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify('{"UserId":"' + _UserId + '","ContentID":"' + _ContentID + '","hdnDivContent":"' + _hdnDivContent + '","ip":"' + ip + '","hdnMarkMaxCount":"' + _hdnMarkMaxCount + '","lblCaseTitle":"' + _lblCaseTitle + '"}'),
                url: 'DocumentWebService.asmx/GetPdfPathForWhatsApp',
                async: false,
                dataType: "json",
                beforeSend: function () {
                    //showLoader1();
                },
                success: function (data) {
                    //hideLoader1();
                    var _path = window.location.host + '/SharingDoc/' + data.d.toLowerCase();
                    var message = _UserName + " has shared a case with you, titled " + _lblCaseTitle + "   " + _path;
                    window.location.href = "whatsapp://send?text=" + message + "";
                    _UserId = _UserName = _ContentID = _hdnDivContent = _hdnMarkMaxCount = ip = '';
                },
                error: function (err) {
                    alert(JSON.stringify(err));
                    _UserId = _UserName = _ContentID = _hdnDivContent = _hdnMarkMaxCount = ip = '';
                }
            });
        }--%>

        function callLoaderAndDownload() {
            shareOverWhatsAppEmailClicks();
            showLoader1();
        }

        function shareUrlOverWhatsapp() {

            //alert('WhatsApp oh ho!!');
            shareOverWhatsAppEmailClicks();

            var _UserId = '<%= Session["ExternalUserId"] %>';
            var _UserName = '<%= Convert.ToString(Session["LoginName"]) %>';
            var _ContentID = '<%= Convert.ToInt32(Request.QueryString["cId"].Trim()) %>';
            var _hdnDivContent = '';//$('#hdnDivContent').val();
            var _hdnMarkMaxCount = $('#hdnMarkMaxCount').val();
            var _lblCaseTitle = $('#lblCaseTitle').html();
            var ip = $('#hdnIpAddress').val();

            if (_UserId == '') {
                window.location.href = window.location.href;
                return false;
            }

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: '{"UserId":"' + _UserId + '","ContentID":"' + _ContentID + '","hdnDivContent":"' + _hdnDivContent + '","ip":"' + ip + '","hdnMarkMaxCount":"' + _hdnMarkMaxCount + '","lblCaseTitle":"' + _lblCaseTitle + '"}',
                url: 'DocumentWebService.asmx/GetPdfPathForWhatsApp',
                async: false,
                dataType: "json",
                beforeSend: function () {
                    //showLoader1();
                },
                success: function (data) {
                    //hideLoader1();
                    var _path = window.location.host + '/SharingDoc/' + data.d.toLowerCase();
                    var message = _UserName + " has shared a case with you, titled " + _lblCaseTitle + "   " + _path;
                    window.location.href = "whatsapp://send?text=" + message + "";
                    _UserId = _UserName = _ContentID = _hdnDivContent = _hdnMarkMaxCount = ip = '';
                },
                error: function (err) {
                    _UserId = _UserName = _ContentID = _hdnDivContent = _hdnMarkMaxCount = ip = '';
                }
            });
        }
        function redirectToWhatsApp(fileName) {
            if (fileName == '') {
                alert('Session Expired');
                window.location.href = window.location.href;
                return false;
            }
            var _UserName = '<%= Convert.ToString(Session["LoginName"]) %>';
            var _lblCaseTitle = $('#lblCaseTitle').html();

            var number = Math.floor(Math.random() * 900000) + 10000;

            var _path = window.location.host + '/SharingDoc/' + fileName + '?_docfile=' + number + '';
            var message = _UserName + " has shared a case with you, titled " + _lblCaseTitle + "   " + _path;
            window.location.href = "whatsapp://send?text=" + message + "";

            fileName = '';
        }
    </script>
</asp:Content>
