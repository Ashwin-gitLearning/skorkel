<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ArticleDetails.aspx.cs" Inherits="ArticleDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlMsg" runat="server">
        <ContentTemplate>
            <div class="main-section-inner">
                <span class="m-aside-trigger mt-0 mb-3 mr-0" onclick="showRight();">
                    <span class="lnr lnr-arrow-left"></span>
                    <span class="avatar-text">Comments</span>
                </span>
                <div class="panel-cover clearfix">
                    <div class="full-width-con">
                        <div class="card card-list-con">
                            <div class="list-group-item top-list">
                                <div class="post-con">
                                    <div class="post-header justify-content-between">
                                        <div>
                                            <span class="question-icon">
                                                <span class="icon">
                                                    <img src="images/file.svg"></span>
                                            </span>
                                            <ul class="que-con">
                                                <li>
                                                    <asp:Label ID="lblJournal" runat="server" Text="Jounal"></asp:Label></li>
                                            </ul>
                                        </div>
                                        <div class="position-relative">
                                            <nav class="navbar navbar-expand-lg p-0">
                                                <a class="navbar-brand" href="#"><span class="sr-only">(current)</span></a>
                                                <span class="research-mobile" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                                                    <span class="icon-more  hide-ios"></span>
                                                </span>
                                              <%--  <asp:UpdatePanel id="pnlLinks" runat="server"><ContentTemplate>--%>
                                                <div class="collapse navbar-collapse research-mobile-drop" id="navbarSupportedContent">
                                                    <ul class="navbar-nav mr-auto">
                                                        
                                                        <li class="nav-item">
                                                            <asp:LinkButton OnClick="lnkDownload_Click" runat="server" id="lnkDownLoad" class="nav-link d-flex align-items-center justify-content-lg-center justify-content-sm-start" ><span class="iconblock"><i class="icon-down-arrow"></i></span><span>Download</span></asp:LinkButton>
                                                        </li>
                                                    </ul>
                                                </div>
                                                 <%--   </ContentTemplate></asp:UpdatePanel>--%>
                                            </nav>
                                        </div>
                                    </div>
                                    <div class="post-body p-b-15">
                                        <h3>
                                            <asp:Label ID="lnkArticle" runat="server" href="#">5 Reasons Why People Like Civil Law.</asp:Label></h3>
                                        <p class="small-date light-black-text">by
                                            <asp:LinkButton ID="lnkUser" runat="server" class="remove-a-color" ></asp:LinkButton></p>
                                            <div class="li-flex-align">
                                                <ul class="list-inline li-flex-align">
                                                    <li class="list-inline-item d-inline-flex align-items-center links-like-btn">
                                                   <asp:LinkButton class="active-toogle" OnClick="lnkLike_Click" runat="server" id="lnkLike">
                                                        <span class="like-btn"></span>
                                                            
                                                    </asp:LinkButton>
                                                    <span class="d-flex">
                                                    <asp:Label ID="lblLikes" runat="server">10 Likes</asp:Label>
                                                    </span>
                                                        </li>
                                                    <li class="list-inline-item d-inline-flex align-items-center">
                                                       <span class="iconblock"><i class="comment-btn"></i></span>
                                                            <asp:Label ID="lblComments" runat="server">43 Comments</asp:Label>
                                                    </li>                                                    
                                                </ul>    
                                            </div>    
                                    </div>
                                    <!-- post-body ended -->

                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- full-width-con ended -->
                </div>
                <!-- panel-cover ended -->
                <div class="panel-cover clearfix">
                    <div class="center-panel">
                        <div class="card" id="mainPDFContainer">
                            <asp:HiddenField ID="hdnArticlePath" runat="server" ClientIDMode="Static"/>
                        </div>

                       
                        <!-- card ended -->

                    </div>
                    <!-- center-panel ended -->
                    <div class="right-panel-back-layer"></div>
                    <div class="right-panel" >
                        <span class="m-view back" onclick="hideRight();">Back <span class="lnr lnr-arrow-right"></span></span>

                        <div class="aside-bar">

                            <h4>Comments</h4>

                            <div class="card post-con article-card">
                                <div class="comment-con">
                                    <div class="post-footer">
                                        <span class="avatar-img">
                                            <img id="imgCurrentUser" runat="server"  alt="" class="rounded-circle">
                                        </span>
                                        <asp:TextBox onKeyDown="if(event.keyCode==13) return false;" ID="txtComment" AutoPostBack="false" runat="server" maxlength="500" placeholder="Please Enter Comment" class="form-control"></asp:TextBox>
                                    </div>
                                     <asp:Label runat="server" Visible="false" ForeColor="Red" Text="Please enter comment." CssClass="RedErrormsg error-comments" ID="lblErrorComment"></asp:Label>
                                    <asp:LinkButton ID="lnkPostComments" OnClick="lnkPostComments_Click" runat="server" Text="Post" class="btn btn-primary float-right" />
                                    <asp:HiddenField ID="hdnCommentId" runat="server" />
                                </div>
                            </div>
                            <!-- card ended-->
                            <asp:ListView ID="lstComments" OnItemCommand="lstComments_ItemCommand" OnItemDataBound="lstComments_ItemDataBound" runat="server">
                                <ItemTemplate>
                                    <div class="card post-con article-card">
                                        <div class="post-header">
                                            <span class="avatar-img">
                                                <img id="imgUser" runat="server" src="images/avatar2.jpg" alt="" class="rounded-circle">
                                            </span>
                                            <span class="user-name-date mt-0 truncate">
                                                <a class="user-name text-left font-size-14-m truncate" href='<%# "Profile2.aspx?RegId="+Eval("AddedByID") %>'><%# Eval("AddedBy") %></a>
                                                <span class="date mt-0"><%# Eval("AddedOn") %></span>
                                            </span>
                                            <asp:HiddenField ID="hdnCommentIdInner" Value='<%# Eval("CommentID") %>' runat="server" />
                                            <asp:HiddenField ID="hdnComment" Value='<%# Eval("Comment") %>' runat="server" />
                                            <span class="more-btn float-right">
                                                <span class="dropdown" style="display:none;" id="lnkmani" runat="server">
                                                    <a  runat="server" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                        <img src="images/more.svg" alt="" class="more-btn" />
                                                    </a>
                                                    <span class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="dropdown-item" CommandName="EditComment"><span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>
                                                        <asp:LinkButton  ID="lnkDelete" runat="server" CssClass="dropdown-item hide-body-scroll" CommandName="DeleteComment" OnClientClick="javascript:showDelete();" ><span class="lnr lnr-trash"></span> Delete</asp:LinkButton>
                                                    </span>
                                                </span>
                                            </span>
                                        </div>
                                        <div class="post-body">
                                            <p class="moreQA"><%# Eval("Comment") %></p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                            <!-- card ended -->
                        </div>
                        <!-- aside-bar ended -->

                    </div>
                    <!-- right-panal ended -->
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
         function OverlayBody() {
            $('#bodyelement').addClass("remove-scroller");
        }
        function hideRight() {
             $(".right-panel").removeClass("openRightPanel");
        $(".right-panel-back-layer").removeClass("active");
        $("body").removeClass("overflowHidden");
        }
        function showRight() {
                    $(".right-panel").addClass("openRightPanel");
        $(".right-panel-back-layer").addClass("active");
        $("body").addClass("overflowHidden");
        }
              var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            //$('a.embed').gdocsViewer({ width: "100%" });
            //$('.ndfHFb-c4YZDc-Wrql6b').remove();
        });
             //$('a.embed').gdocsViewer({ width: "100%" });
        //$('.ndfHFb-c4YZDc-Bz112c').remove();
        function showDelete() {
            OverlayBody();
            $('#divDeletesucess').css("display", "block");
        }
        function hideDelete() {
            $('#divDeletesucess').css("display", "none");
        }
        function OverlayBody() {
            $('#bodyelement').addClass("remove-scroller");
        }
        function RemoveOverlay() {
            $('#bodyelement').removeClass("remove-scroller");
        }
        function ShowAddJournal() {
            $('#docModal1').show();
        }
        function ViewPDF() {
            
        }
            $(".m-aside-trigger").click(function() {
        $(".right-panel").addClass("openRightPanel");
        $(".right-panel-back-layer").toggleClass("active");
        $("body").toggleClass("overflowHidden");
    });
             $(".right-panel-back-layer").click(function() {
        $(".right-panel").removeClass("openRightPanel");
        $(".right-panel-back-layer").removeClass("active");
        $("body").removeClass("overflowHidden");
    });

        $('.selectedOption').click(function () {
            $(this).toggleClass('down-arrow');
        });

        $(document).ready(function () {
            var viewerHeight = window.innerHeight * 0.90;

            $('#mainPDFContainer').append('<iframe src="/pdfviewer/web/viewer.html?file='+$('#hdnArticlePath').val() +'" width="100%" height="'+ viewerHeight +'"></iframe>')
            var showChar = 100;
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

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                var viewerHeight = window.innerHeight * 0.90;

                $('#mainPDFContainer').append('<iframe src="/pdfviewer/web/viewer.html?file='+$('#hdnArticlePath').val() +'" width="100%" height="'+ viewerHeight +'"></iframe>')
                $('.moreQA').each(function () {
                  var content = $(this).html();
                  if (content.length > showChar) {
                      var c = content.substr(0, showChar);
                      var h = content.substr(showChar - 1, content.length - showChar);
                      var html = c + '<span class="moreelipses">' + ellipsestext + '</span>&nbsp;<span class="morecontent"><span span style="display:none;">' + h + '</span>&nbsp;&nbsp;<a href="" class="morelinkQA">' + moretext + '</a></span>';
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
            });
        });
    </script>
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
                             OnClientClick="javascript:hideDelete();"   OnClick="lnkDeleteCancel_Click"></asp:Button>
                            <asp:Button ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="javascript:hideDelete();"
                                      OnClick="lnkDeleteConfirm_Click" CssClass="btn btn-primary add-scroller" ></asp:Button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
