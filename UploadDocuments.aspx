<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="UploadDocuments.aspx.cs"
    Inherits="UploadDocuments" %>

<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="js/jquery.custom-scrollbar.js" type="text/javascript"></script>
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
                    }
                    else {
                        strSelText += ',' + control.options[i].value;
                    }
                    cnt++;
                }
            }
            $('#hdnDepartmentIds').val(strSelText);

        }
    
    
    </script>
    <meta charset="utf-8">
    <link rel="stylesheet" href="docsupport/style.css">
    <link rel="stylesheet" href="docsupport/prism.css">
    <link href="docsupport/chosen.css" rel="stylesheet" type="text/css" />
    <%-- <link rel="stylesheet" href="chosen.css">--%>
    <style type="text/css" media="all">
        /* fix rtl for demo */.chosen-rtl .chosen-drop
        {
            left: -9000px;
        }
    </style>
    <script src="docsupport/chosen.jquery.js" type="text/javascript"></script>
    <script src="docsupport/prism.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="cls">
        </div>
        <div class="innerDocumentContainer">
            <div class="innerContainer">
                <Group:GroupDetails ID="GroupDetails1" runat="server" />
                <div class="clsFooter" style="border: 0">
                </div>
                <div class="innerGroupBox">
                    <!--left verticle icons starts-->
                    <!--left verticle icons ends-->
                    <div class="innerContainerLeft">
                        <div class="tagContainer">
                            <div class="tagsTitle">
                                <div class="tag">
                                    <asp:LinkButton ID="lnkHome" runat="server" Text="Home" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                                </div>
                                <div class="tag">
                                    <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                        OnClick="lnkForumTab_Click"></asp:LinkButton>
                                </div>
                                <div class="tag">
                                    <a href="#" id="tagSelected">Uploads</a>
                                </div>
                                <%-- <div class="tag" id="tagSelected">
                                    <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                        OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                </div>--%>
                                <div class="tag">
                                    <asp:LinkButton ID="lnkPollTab" runat="server" Text="Poll" ClientIDMode="Static"
                                        OnClick="lnkPollTab_Click"></asp:LinkButton>
                                </div>
                                <div class="tag">
                                    <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                        OnClick="lnkEventTab_Click"></asp:LinkButton>
                                </div>
                                <div class="tag">
                                    <asp:LinkButton ID="lnkJobTab" runat="server" Text="Jobs" ClientIDMode="Static" OnClick="lnkJobTab_Click"></asp:LinkButton>
                                </div>
                                <div class="tag">
                                    <asp:LinkButton ID="lnMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                        OnClick="lnkEventMemberTab_Click"></asp:LinkButton></div>
                            </div>
                        </div>
                        <div class="cls">
                            <br />
                        </div>
                        <div class="top">
                        </div>
                        <div class="center">
                            <div class="documentHeading">
                                Upload Documents</div>
                            <div id="lnkUpdateDetaila" runat="server" style="padding-right: 30px; text-align: right;
                                display: none;" class="cls">
                                <%-- <asp:LinkButton ID="lnkEdit" PostBackUrl="~/CreateProfile-Documents.aspx?updateid=1"
                                    Text="Update" runat="server"></asp:LinkButton>--%>
                                <asp:LinkButton ID="btnSave" CssClass="createFormBtn" runat="server" Text="Upload Document"
                                    ClientIDMode="Static" OnClick="lnkEdit_Click"></asp:LinkButton>
                                <%--  <asp:LinkButton ID="lnkEdit" Text="Upload Documents" runat="server" OnClick="lnkEdit_Click"></asp:LinkButton>--%>
                            </div>
                            <div class="cls">
                            </div>
                            <div class="cls" style="height: 10px">
                            </div>
                            <div id="vertical-horizontal-scrollbar-demo" class="default-skin demo">
                                <asp:ListView ID="LstDocument" runat="server" GroupItemCount="500" GroupPlaceholderID="groupPlaceHolder1"
                                    ItemPlaceholderID="itemPlaceHolder1" OnItemDataBound="LstDocument_ItemDataBound">
                                    <LayoutTemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                        </table>
                                    </LayoutTemplate>
                                    <GroupTemplate>
                                        <tr>
                                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                        </tr>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <td>
                                            <div class="cls">
                                            </div>
                                            <div class="itemDocument">
                                                <div class="rightTag">
                                                    <p class="achievementTitle">
                                                        <asp:HiddenField ID="hdnDocId" Value='<%#Eval("intGroupDocId") %>' runat="server" />
                                                        <%--<a href="#">Document Name.pdf</a>--%>
                                                        <asp:HyperLink ID="hrp_DocPath" ClientIDMode="Static" Target="_blank" ToolTip="Download file"
                                                            NavigateUrl='<%# "UploadDocument/"+Eval("strFilePath")%>' Text='<%#Eval("strDocName") %>'
                                                            Font-Size="Small" runat="server"></asp:HyperLink>
                                                    </p>
                                                    <p>
                                                        <asp:Label ID="lblDocument" runat="server" Text='<%#Eval("strDocTitle") %>'></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="cls">
                                                </div>
                                                <br />
                                                <asp:Panel ID="pnlSale" runat="server">
                                                    <div class="sale" id="divSale" runat="server">
                                                        <img src="images/sale.png" /></div>
                                                </asp:Panel>
                                            </div>
                                            <div class="cls">
                                            </div>
                                        </td>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            <div class="cls" style="height: 10px">
                            </div>
                        </div>
                        <div class="bottom">
                        </div>
                    </div>
                </div>
                <%-- <div class="clsFooter">
                </div>--%>
            </div>
            <!--left verticle search list ends-->
        </div>
    </div>
    <script type="text/javascript"> $(window).load(function () { $(".demo").customScrollbar();
    $("#scroll-button1").click(function () { $("#vertical-scrollbar-demo").customScrollbar("scrollTo",
    $("#vertical-scrollbar-demo p")); }); $("#scroll-button2").click(function () { $("#horizontal-scrollbar-demo").customScrollbar("scrollTo",
    $("#horizontal-scrollbar-demo #no-lena")); }); $("#scroll-button3").click(function
    () { $("#vertical-horizontal-scrollbar-demo").customScrollbar("scrollTo", $("#vertical-horizontal-scrollbar-demo
    strong")); }); }); </script>
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
    </script>
</asp:Content>
