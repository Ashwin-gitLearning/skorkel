<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="my-bookmarks.aspx.cs" Inherits="my_bookmarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="">
        <!--inner container ends-->
        <div class="main-section-inner">
            <div class="panel-cover clearfix ">
                <div class="full-width-con">
                    <div>
                        <h5 class="card-title float-left">My Bookmarks</h5>
                    </div>
                    <div class="clearfix"></div>
                    <!--bookmark starts-->
                    <!--bookmark ends-->

                    <div id="divCancelPopup" runat="server" class="modal backgroundoverlay" style="display: none;" clientidmode="Static">
                        <div class="modal-dialog  modal-dialog-centered">
                            <div class="modal-content">
                                <div>
                                    <b>
                                        <asp:Label ID="lbl" runat="server"></asp:Label>
                                    </b>
                                </div>
                                <div class="modal-header">
                                    <h5 class="modal-title">Demo text  here
                                    </h5>

                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="lblConnDisconn" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="modal-footer border-top-0">
                                    <a href="#" clientidmode="Static" class="btn btn-outline-primary m-r-15" causesvalidation="false" onclick="Cancel();">Cancel </a>
                                    <asp:LinkButton ID="lnkConnDisconn" runat="server" ClientIDMode="Static" Text="Yes" CssClass="btn btn-primary" OnClick="lnkConnDisconn_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divSuccess" class="modal_bg" runat="server" style="display: none;" clientidmode="Static">
                        <div class="modal-dialog">
                            <div>
                                <b>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </b>
                            </div>
                            <div class="modal-header">
                                <strong>&nbsp;&nbsp;
                              <asp:Label ID="Label2" runat="server" Text="Book mark removed successfully."></asp:Label>
                                </strong>
                            </div>
                            <div class="modal-footer">
                                <a href="#" clientidmode="Static" causesvalidation="false" class="cancel_btn margin_right_l add-scroller" onclick="Cancel();">Close </a>
                            </div>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="updatetag" runat="server">
                        <ContentTemplate>
                            <div id="divbookmark" runat="server">
                                <asp:HiddenField ID="hdnContentTypeID" runat="server" Value="1" ClientIDMode="Static" />
                                <asp:ListView ID="lstBookMark" runat="server" OnItemCommand="lstBookMark_ItemCommand">
                                    <LayoutTemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr id="itemPlaceHolder" runat="server">
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnCaseID" ClientIDMode="Static" runat="server" Value='<%#Eval("intCaseId") %>' />
                                        <asp:HiddenField ID="hdnstrJudgeNames" ClientIDMode="Static" runat="server" Value='<%#Eval("strJudgeNames") %>' />
                                        <div class="card card-list-con tags-card">
                                            <div class="list-group-item top-list">
                                                <div class="post-con">
                                                    <!--   <div class="post-header">
                                                            <span class="question-icon">
                                                            <span class="icon"> <img src="images/tags-small.svg"></span>
                                                            </span>
                                                            <ul class="que-con">
                                                                <li>Bookmarks</li>
                                                        
                                                            </ul>
                                                        </div> -->
                                                    <div class="post-body">
                                                        <h3 class="align-h3">
                                                            <asp:LinkButton class="remove-anchor cursor-pointer" ID="lnkTitle" runat="server" CommandName="Title">
                                                                <asp:Label ID="lblstrPartyNames" runat="server" Text='<%#Eval("strCaseTitle") %>'></asp:Label>
                                                                <asp:Label ID="lblEq_Citations" runat="server" Text='<%#Eval("Eq_Citations") %>'></asp:Label>
                                                            </asp:LinkButton>
                                                        </h3>
                                                        <p>
                                                            <asp:Label ID="lblstrJurisdiction" runat="server" Text='<%#Eval("strJurisdiction") %>'></asp:Label>
                                                            <asp:Label ID="lblintYear" runat="server" Text='<%#Eval("intYear") %>'></asp:Label>
                                                        </p>

                                                        <div class="bookmarkArrow">
                                                            <asp:LinkButton Visible="false" ID="lnkTitle1" runat="server" CommandName="Title"><img src="images/view_more_page.png" /> </asp:LinkButton>
                                                        </div>
                                                        <div class="close" style="display: none;">
                                                            <asp:LinkButton ID="lnkDeleteDoc" ToolTip="Delete bookmark" runat="server" CommandName="Delete Bookmark"><img src="images/gray-close.png" />
                                                            </asp:LinkButton>
                                                        </div>
                                                        <span class="lastEditedDate" style="display: none;">Last edited :
                                                                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("AddedOn") %>'></asp:Label>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div id="pLoadMore" runat="server" align="center">
                                    <div id="imgLoadMore" class="lds-ellipsis" style="display: none;">
                                        <div></div>
                                        <div></div>
                                        <div></div>
                                        <div></div>
                                    </div>
                                    <asp:ImageButton ID="imgLoadMore2" runat="server" ClientIDMode="Static" ImageUrl="~/images/loadingIcon.gif" OnClick="imgLoadMore_OnClick" Style="display: none;" />
                                </div>
                                <p align="center">
                                    <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server" ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                                </p>
                                <!--searches ends-->
                                <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                                <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />

                                 <div id="divEmptyResult" class="blank-page-message  card card-list-con" clientidmode="static" runat="server">
                                    <p>You have not bookmarked any cases yet - Please visit the <a href="Research_SearchResult_S.aspx">Research</a> section to bookmark.</p>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="cls">
                    </div>
                </div>
                <!--left box ends-->
            </div>
            <!--left verticle search list ends-->
        </div>
        <div class="cls">
        </div>
    </div>
    <script type="text/jscript">
        function Cancel() {
            document.getElementById("divCancelPopup").style.display = 'none';
            document.getElementById("divSuccess").style.display = 'none';
            return false;
        }
    </script>
    <script type="text/javascript">
        $(document).on('click', '.mobile_tab_icon', function () {
            $('.mobile_m_skorkel').slideToggle('slow');
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(window).scroll(function () {
                if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                    $("#imgLoadMore").css("display", "block");
                    var v = $("#lblNoMoreRslt").text();
                    var maxCount = $("#hdnMaxcount").val();
                    if (maxCount <= 10) {
                        $("#lblNoMoreRslt").css("display", "block");
                        $("#imgLoadMore").css("display", "none");
                    } else {
                        if (v != "No more results available...") {
                            document.getElementById('<%= imgLoadMore2.ClientID %>').click();
                            }
                        }
                    }
                });
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    $(window).scroll(function () {
                        if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                            $("#imgLoadMore").css("display", "block");
                            var v = $("#lblNoMoreRslt").text();
                            var maxCount = $("#hdnMaxcount").val();
                            if (maxCount <= 10) {
                                $("#lblNoMoreRslt").css("display", "none");
                                $("#imgLoadMore").css("display", "none");
                            } else {
                                if (v != "No more results available...") {
                                    document.getElementById('<%= imgLoadMore2.ClientID %>').click();
                                }
                            }
                        }
                    });
                });
            });
    </script>
</asp:Content>
