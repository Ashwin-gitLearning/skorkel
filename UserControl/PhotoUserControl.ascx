<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PhotoUserControl.ascx.cs"
    Inherits="UserControl_PhotoUserControl" %>
<script src="<%=ResolveUrl("../js/jquery.Jcrop.min.js") %>" type="text/javascript"></script>
<link href="<%=ResolveUrl("../Styles/jquery.Jcrop.css") %>" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    function ValidatePhotoUpload() {
        if (document.all('fupPhoto').value == "") {
            alert("Kindly Select Photo.");
            document.getElementById("<%=fupPhoto.ClientID %>").focus();
            return false;
        }
        needToConfirm = false;
        return true;
    }

    $(function ($) {
        $('#ImFullImage').Jcrop({
            onSelect: updateCoords,
            aspectRatio: 1,
            minSize: [50, 50]
        });
    });
    function updateCoords(c) {
        $('#hfX').val(c.x);
        $('#hfY').val(c.y);
        $('#hfHeight').val(c.h);
        $('#hfWidth').val(c.w);
    }
    function hidePopUpShare() {
        debugger;
        window.parent.$find('PopUpShare').hide();
    }
</script>
<asp:HiddenField ID="hfX" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hfY" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hfHeight" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hfWidth" runat="server" ClientIDMode="Static" />
<br />
<br />
<div>
    <asp:FileUpload BackColor="white" ForeColor="Black" ID="fupPhoto" runat="server"
        ClientIDMode="Static" Width="250px"/> &nbsp;
    <asp:Button ID="btnUploadPhoto" runat="server" Text="Upload" OnClick="btnUploadPhoto_Click"
        OnClientClick="return ValidatePhotoUpload();" Height="28px" Width="75px" /><br />
</div>
<br />
<div id="divCropImage" runat="server">
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Image ID="ImFullImage" ImageUrl="~/images/profile-photo.png" runat="server" ClientIDMode="Static" />
                <asp:Image ID="imCropped" runat="server" ClientIDMode="Static" Visible="false" />
            </td>
            <td>
                <asp:Button ID="btnCrop" runat="server" Text="Crop Image" OnClick="btnCrop_Click"
                    OnClientClick="Disable();"  Visible="false"/>
                &nbsp; &nbsp;
                <asp:Button CssClass="submitButton" ID="btnSave" runat="server" Text="Save Picture"
                    ClientIDMode="Static" Visible="false" OnClick="btnSave_Click" OnClientClick="hidePopUpShare()"/>
            </td>
        </tr>
    </table>
</div>
