<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Scrl_CropImage.ascx.cs"
    Inherits="UserControl_Scrl_CropImage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<script src="../js/jquery.min.js" type="text/javascript"></script>--%>
<%--<script src="../js/jquery.Jcrop.min.js" type="text/javascript"></script>--%>
<%--<script src="../js/jquery.min.js" type="text/javascript"></script>--%>
<link rel="stylesheet" href="../Styles/jquery.Jcrop.css" type="text/css" />
<script src="../js/jquery.Jcrop.js" type="text/javascript"></script>
<script language="Javascript" type="text/javascript">

    jQuery(document).ready(function () {

        jQuery('#cropbox').Jcrop({
            onSelect: updateCoords
        });
    });

    jQuery(document).ready(function () {

        jQuery('#newimg').Jcrop({
            onSelect: updateCoords
        });
    });

    function updateCoords(c) {
        jQuery('#X').val(c.x);
        jQuery('#Y').val(c.y);
        jQuery('#W').val(c.w);
        jQuery('#H').val(c.h);
    };
</script>
<%--<asp:UpdatePanel ID="xx" runat="server">
    <ContentTemplate>--%>
<div>
    <%-- <asp:Image ID="cropedImage"  runat="server" Visible="False" />--%>
    <asp:FileUpload ID="uploaderImg" runat="server" Style="display: none;" />
    <asp:Button ID="btnUploadImage" runat="server" Text="Upload" OnClick="btnUploadImage_Click"
        Style="display: none;" />
    <%--Start for normal crop image     --%>
    <br />
    <img scr="" clientidmode="Static" id="newimg" runat="server" /><asp:Image ID="cropedImage"
        runat="server" Visible="False" Height="250px" Width="250px" ClientIDMode="Static" />
    <br />
    <asp:FileUpload ID="fileup1" runat="server" />
    <%--  <asp:FileUpload ID="fileup1" runat="server" />--%><br />
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ClientIDMode="Static" UpdateMode="Conditional" ID="videoup" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="uploadImage" runat="server" ToolTip="Click to upload new image/photo"
                            Text="Upload" ClientIDMode="Static" Style="display: block;" OnClick="uploadImage_Click" />
                        <asp:Button ID="SaveImg" runat="server" ToolTip="Click here & take mouse over image & select part of the image then click on Add button."
                            Text="Crop Photo" ClientIDMode="Static" Style="display: none;" OnClick="SaveImg_Click" />
                        <asp:Label ID="lblerror" Font-Bold="false" runat="server" Visible="false"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <%-- <asp:AsyncPostBackTrigger ControlID="uploadImage" />--%>
                        <asp:PostBackTrigger ControlID="uploadImage" />
                        <%-- <asp:PostBackTrigger ControlID="SaveImg" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <br />
    <%--End for normal crop image     --%>
    <asp:UpdatePanel ID="xxj" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none;" CommandName="Receive"></asp:LinkButton>
            <cc1:ModalPopupExtender BackgroundCssClass="modalBackground" runat="server" TargetControlID="LinkButton2"
                PopupControlID="Panel2" ID="ModalCropImage" CancelControlID="btnMClose" OkControlID="btnMFinish"
                ClientIDMode="Static" />
            <asp:Button ID="btnMFinish" runat="server" Style="visibility: hidden" />
            <asp:Button ID="btnMClose" runat="server" Style="visibility: hidden" />
            <asp:Panel ID="Panel2" Width="19%" Height="40%" runat="server" CssClass="modalPopup"
                ClientIDMode="Static">
                <div class="ModalDiv">
                    <table style="width: 100%">
                        <tr>
                            <td valign="top" style="width: 50%;">
                                Upload Photo
                            </td>
                            <td align="right">
                                <asp:LinkButton ID="lnkClose" runat="server" OnClick="lnkClose_Click">
                      <%--  <img src="~/images/Close1.jpg" border="0" style="padding-top:2px;" />--%>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
        <ContentTemplate>--%>
                <table id="Table2" cellspacing="1" cellpadding="1" class="pnl" align="center" bgcolor="Black"
                    width="250">
                    <tbody>
                        <tr>
                            <td>
                                <img src="" id="cropbox" clientidmode="Static" runat="server" height="150" width="150" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Submit" runat="server" Text="Crop Image" OnClick="Submit_Click" ClientIDMode="Static" />
                                <%-- <asp:Button ID="btnOk" runat="server" Width="68px" Text="Save" OnClick="btnOk_Click" Height="27px" />--%>
                                &nbsp;<asp:Button ID="btnClose" runat="server" Text="Cancel" Width="68px" OnClick="btnClose_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
                <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
            </asp:Panel>
            <asp:HiddenField ID="X" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="Y" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="W" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="H" ClientIDMode="Static" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<%-- </ContentTemplate>
</asp:UpdatePanel>
--%>