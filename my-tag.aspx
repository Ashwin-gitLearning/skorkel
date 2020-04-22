<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="my-tag.aspx.cs" Inherits="my_tag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="">
        <div class="cls">
        </div>
        <div class="main-section-inner">
            <div class="panel-cover clearfix">
                <div class="full-width-con">
                    <div>
                        <h5 class="card-title float-left">My Tags</h5>
                    </div>
                    <div class="clearfix"></div>
                    <asp:UpdatePanel ID="updatetag" runat="server">
                        <ContentTemplate>
                            <div id="divtag" runat="server">
                                <asp:HiddenField ID="hdnContentTypeID" runat="server" Value="1" ClientIDMode="Static" />
                                <asp:ListView ID="lstMainMyTag" runat="server" OnItemDataBound="lstMainMyTag_ItemDataBound" OnItemCommand="lstMainMyTag_ItemCommand">
                                    <ItemTemplate>
                                        <!--tag box starts-->
                                        <div class="tagDtl">
                                            <asp:HiddenField ID="hdnCaseId" Value='<%#Eval("intCaseId") %>' runat="server" />
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("intCaseId") %>' />
                                            <div class="card card-list-con tags-card">
                                                <div class="list-group-item top-list">
                                                    <div class="post-con">
                                                        <div class="post-header">
                                                            <span class="question-icon">
                                                                <span class="icon">
                                                                    <img src="images/tags-small.svg">
                                                                </span>
                                                            </span>
                                                            <ul class="que-con">
                                                                <li>
                                                                    <asp:Label ID="lblTagName" Text='<%#Eval("TagName") %>' runat="server"></asp:Label>
                                                                </li>
                                                                <li>
                                                                    <asp:LinkButton ID="lnkTitle" ToolTip="View document" runat="server" CommandName="Title" Text='<%#Eval("CaseTitle") %>'></asp:LinkButton>
                                                                </li>
                                                            </ul>

                                                        </div>
                                                        <div class="post-body">
                                                            <p>
                                                                <asp:Label ID="lblContent" runat="server" Text='<%#Eval("strSelectedContent") %>'></asp:Label>
                                                            </p>
                                                            <asp:Label ID="Label1" class="small-date display-inline m-t-10" runat="server" Text='<%#Eval("dtaddedOn") %>'></asp:Label>
                                                            <asp:Label ID="Label2" class="small-date display-inline m-t-10" runat="server" Text='<%#Eval("Times") %>'></asp:Label>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--tag box ends-->
                                    </ItemTemplate>
                                </asp:ListView>
                                <p id="pLoadMore" runat="server" align="center">
                                    <div id="imgLoadMore" class="lds-ellipsis" style="display: none;">
                                        <div></div>
                                        <div></div>
                                        <div></div>
                                        <div></div>
                                    </div>
                                    <asp:ImageButton ID="imgLoadMore2" runat="server" ClientIDMode="Static" ImageUrl="~/images/loadingIcon.gif" OnClick="imgLoadMore_OnClick" Style="display: none;" />
                                </p>
                                <p align="center" class="display-none">
                                    <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server" ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                                </p>
                                <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                                <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
                                <div id="divEmptyResult" class="blank-page-message  card card-list-con" clientidmode="static" runat="server">
                                    <p>You have not tagged any cases yet - Please visit the <a href="Research_SearchResult_S.aspx">Research</a> section to tag.</p>
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
    </div>
    <script type="text/javascript">
        function ToogleComment(ctl) {
            $(ctl).find("#commentTxt1").slideToggle("slow");
            $(ctl).find("#commnetBtn1 img").toggle();
        }
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
                        $("#lblNoMoreRslt").css("display", "none");
                        $("#imgLoadMore").css("display", "none");
                    } else {
                        if (v != "No more results available...") {
                            document.getElementById('<%= imgLoadMore2.ClientID %>').click();
                        } else {
                            $("#imgLoadMore").css("display", "none");
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
                            } else {
                                $("#imgLoadMore").css("display", "none");
                            }
                        }
                    }
                });
            });
        });
    </script>

</asp:Content>
