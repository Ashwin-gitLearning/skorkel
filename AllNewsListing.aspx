<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AllNewsListing.aspx.cs" Inherits="AllNewsListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-section-inner">

        <div class="panel-cover clearfix">
            <div class="full-width-con common-user">

                <div class="form-inline">
                    <div class="search-filter-con w-100">
                        <asp:UpdatePanel ID="pnlSearch" runat="server" UpdateMode="Conditional" class="search-bar col-lg-4 clearfix margin-bottom-m p-0 col-sm-12 col-12">
                            <ContentTemplate>
                                <div class="w-100 search-cover">
                                    <asp:TextBox ID="txtNewsSearch" runat="server" autocomplete="off" placeholder="Search News" ClientIDMode="Static" class="form-control p-r-0"
                                        onkeydown="return postSearchNews.bind(this)(event)">
                                    </asp:TextBox>
                                    <input type="button" id="searchBtn" class="search-btn-2" value="">
                                </div>
                                <asp:Button ID="btnNewsSearch" runat="server" ClientIDMode="Static" OnClick="btnNewsSearch_Click" OnClientClick="javascript:showLoader1();"
                                    Width="50px" Height="30px" Text="Search" Style="display: none" />
                                <asp:HiddenField ID="hdnSearch" runat="server" Value='' ClientIDMode="Static" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="card-list-con">
                    <asp:UpdatePanel ID="pnlParent" runat="server">
                        <ContentTemplate>
                            <asp:ListView ID="lstParentCrdDetails" runat="server" OnItemDataBound="lstParentCrdDetails_ItemDataBound" OnItemCommand="lstParentCrdDetails_ItemCommand">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnPostID" runat="server" Value='<%# Eval("ID") %>' ClientIDMode="Static" />
                                    <div class="card top-list">
                                        <div class="post-con">
                                            <div class="post-body p-b-15">
                                                <%--<h3><asp:Label ID="lblHeading" runat="server" Text='<%#Eval("Title") %>' /></h3>--%>
                                                <h3>
                                                    <asp:LinkButton ID="lblHeading" runat="server" Font-Underline="false" CommandName="NewsDetails" CssClass="commentQA"
                                                        Text='<%#Eval("Title") %>'></asp:LinkButton>
                                                </h3>
                                                <%--Text="Two militants blamed for J-K highway shooting killed, third trapped"--%>
                                                <ul class="small-date">
                                                    <%--<li>By <asp:Label ID="lblUserName" runat="server" /></li>--%>
                                                    <li>Source:
                                                        <asp:Label ID="lblSource" class="hover-same-color" runat="server" /></li>
                                                    <%--Text='<%#Eval("Type") %>'--%>
                                                    <li>
                                                        <asp:Label ID="dtAddedOn" runat="server" Text='<%#Eval("Created_timestamp") %>' /></li>
                                                </ul>
                                                <p class="m-t-5">
                                                    <asp:Label ID="lblDtlNews" runat="server" CssClass="commentQA remove-anchor moreQA" /><%--Text='<%#Eval("Content") %>'--%>
                                                </p>
                                                <%--Text="Two militants who had opened fire at a check post on the Jammu-Srinagar-highway on Wednesday were killed in a shootout with security 
              forces after being surrounded near a village in Jammu and Kashmir’s Reasi district on Thursday and the third was trapped, officials said."--%>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- card ended -->
                                </ItemTemplate>
                            </asp:ListView>

                            <!---Pagination Start-->
                            <div class="pagination_main_div">
                                <nav aria-label="Page navigation example">
                                    <ul id="dvPage" runat="server" class="pagination" clientidmode="Static">
                                        <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click" ClientIDMode="Static" class="page-link">                              
           <span class="lnr lnr-chevron-left">
                                        </asp:LinkButton>

                                        <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand" OnItemDataBound="rptDvPage_ItemDataBound">
                                            <ItemTemplate>
                                                <li class="page-item">
                                                    <asp:LinkButton ID="lnkPageLink" runat="server" ClientIDMode="Static" CommandName="PageLink" class="page-link" Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:LinkButton ID="lnkNext" runat="server" class="page-link" OnClick="lnkNext_Click" ClientIDMode="Static"><span class="lnr lnr-chevron-right"></span></asp:LinkButton>

                                        <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                        <asp:HiddenField ID="hdnEndPage" runat="server" ClientIDMode="Static" />
                                    </ul>
                                </nav>
                            </div>

                            <div id="divEmptyResult" class="" clientidmode="static" runat="server">
                                <p class="searh-page-result" id="pMessageForEmpty" runat="server" clientidmode="static"></p>
                            </div>
                            <!---Pagination End-->
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnkNext" />
                            <asp:AsyncPostBackTrigger ControlID="lnkPrevious" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
                <!-- card-list-con ended -->
            </div>
            <!-- full-width-con ended -->
        </div>
        <!-- panel-cover ended -->
    </div>
    <!-- main-section inner ended -->
    <script type="text/javascript">
        $(document).ready(function () {

            var showChar = 300;
            var ellipsestext = "...";
            var moretext = "Read more";
            var lesstext = "Less";
            $('.moreQA').each(function () {
                var content = $(this).html();
                if (content.length > showChar) {
                    var c = content.substr(0, showChar);
                    var h = content.substr(showChar - 1, content.length - showChar);
                    var html = c + '<span class="moreelipses">' + ellipsestext + '</span>';
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

            if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                //$('#lnkNextshow').css("display", "block");
                $('#lnkNext').css("display", "none");
            }

            $("#dvPage a").on('click', function (event) { showLoader1(); });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                    //$('#lnkNextshow').css("display", "block");
                    $('#lnkNext').css("display", "none");
                }

                $("#dvPage a").on('click', function (event) { showLoader1(); });

                var showChar = 300;
                var ellipsestext = "...";
                var moretext = "Read more";
                var lesstext = "Less";
                $('.moreQA').each(function () {
                    var content = $(this).html();
                    if (content.length > showChar) {
                        var c = content.substr(0, showChar);
                        var h = content.substr(showChar - 1, content.length - showChar);
                        var html = c + '<span class="moreelipses">' + ellipsestext + '</span>';
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

        function postSearchNews(e) {
            if (e.keyCode == 13 && $(this).val()) {
                $('#btnNewsSearch').click();
                return false;
            } else if (e.keyCode == 13) {
                return false;
            }
        }
    </script>
</asp:Content>

