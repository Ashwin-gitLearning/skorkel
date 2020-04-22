<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="my-saved-searches.aspx.cs" Inherits="my_saved_searches" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server"></asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <div class="main-section-inner">
            <div class="panel-cover clearfix ">
                <div class="full-width-con">
                    <div> <h5 class="card-title float-left">My Searches</h5></div><div class="clearfix"></div>
                    <asp:UpdatePanel ID="updatetag" runat="server">
                        <ContentTemplate>
                            <div id="divSavedSearch" runat="server">
                                <!--searches starts-->
                                <asp:ListView ID="lstMySavedSearches" runat="server" OnItemCommand="lstMySavedSearches_ItemCommand">
                                    <LayoutTemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr id="itemPlaceHolder" runat="server">
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnCaseID" ClientIDMode="Static" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnintMySearchId" ClientIDMode="Static" runat="server" Value='<%#Eval("intMySearchId") %>' />
                                        <asp:HiddenField ID="hdnAmongst" ClientIDMode="Static" runat="server" Value='<%#Eval("strPreSearchedIn") %>' />
                                        <asp:HiddenField ID="hdnContextId" ClientIDMode="Static" runat="server" Value='<%#Eval("strContextId") %>' />
                                        <asp:HiddenField ID="hdnDocumenttype" ClientIDMode="Static" runat="server" Value='<%#Eval("strDocumentType") %>' />
                                        <asp:HiddenField ID="hdnstrSearchQuery" ClientIDMode="Static" runat="server" Value='<%#Eval("strSearchQuery") %>' />
                                        <div class="card card-list-con tags-card">
                                            <div class="list-group-item top-list">
                                                <div class="post-con">
                                                    <div class="post-header">
                                                        <span class="question-icon">
                                                            <span class="icon"> <img src="images/tags-small.svg"></span>
                                                        </span>
                                                        <ul class="que-con">
                                                            <li>
                                                               Search
                                                            </li>        
                                                            <li>     
                                                                    <asp:Label ID="lblstrSearchFor" runat="server" Text='<%#Eval("strSearchFor") %>'></asp:Label>
                                                                    
                                                                
                                                            </li>
                                                        </ul>    
                                                    </div>   
                                                    <div class="post-body"> 
                                                        <div class="bookmarkDtl list_style margin_bottom">
                                                            <div class="searchQuery bld breakallsmyserch">
                                                                <h3 class="align-h3">
                                                                <asp:LinkButton ID="lnksaveTitle" CommandName="SaveSearch" runat="server" Text='<%#Eval("strSavedMyTitle") %>' class="remove-anchor cursor-pointer"></asp:LinkButton>
                                                                </h3>
                                                            </div>
                                                        
                                                            <p>
                                                                <strong>Results:</strong>&nbsp;
                                                                <asp:Label ID="lblcount" runat="server" Text='<%#Eval("intSearchResultCount") %>'></asp:Label>
                                                            </p>
                                                            <p class="small-date display-inline m-t-10">
                                                                <asp:Label ID="lblTime" runat="server" Text='<%#Eval("dtTimes") %>'></asp:Label>
                                                                on
                                                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("dtAddedOn") %>'></asp:Label>
                                                            </p>
                                                           
                                                        </div>
                                                       <!--  <div class="bookmarkArrow">
                                                            <asp:ImageButton ID="imgbutton" runat="server" ImageUrl="images/view_more_page.png" CommandName="SaveSearch" />
                                                        </div> -->
                                                    </div>    
                                                </div>    
                                            </div>    
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div id="pLoadMore" runat="server" align="center">
                                    <div id="imgLoadMore" class="lds-ellipsis" style="display:none;">
                                        <div></div>
                                        <div></div>
                                        <div></div>
                                        <div></div>
                                    </div>
                                    <asp:ImageButton ID="imgLoadMore2" runat="server" ClientIDMode="Static" ImageUrl="~/images/loadingIcon.gif" OnClick="imgLoadMore_OnClick" style="display:none;" />
                                </div>
                                <p align="center">
                                    <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server" ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                                </p>
                                <!--searches ends-->
                                <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                                <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
                                <div id="divEmptyResult" class="blank-page-message card card-list-con" clientidmode="static" runat="server">
                                    <p>You have not saved any case searches yet - Please visit the <a href="Research_SearchResult_S.aspx">Research</a> section to save search.</p>
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
        <div style="display: none" class="clsFooter">
        </div>

        <script type="text/javascript">
            function redirecttoresearch(Url) {
                window.location = Url;
            }
            $(document).on('click', '.mobile_tab_icon', function() {
                $('.mobile_m_skorkel').slideToggle('slow');
            });
        </script>
        <script type="text/javascript">
            $(document).ready(function() {
                $(window).scroll(function() {
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
                prm.add_endRequest(function() {
                    $(window).scroll(function() {
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