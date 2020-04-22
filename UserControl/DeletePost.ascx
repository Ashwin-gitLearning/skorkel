<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DeletePost.ascx.cs" Inherits="UserControl_DeletePost" %>
<script type="text/javascript">
    function MessageClose() {
   
        document.getElementById("divMessSucces").style.display = 'none';
        document.getElementById("divDeletePopup").style.display = 'none';
    }
           
</script>
<div id="divDeletePopup" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
    float: left; width: 500px; padding-top: 0px; position: relative; margin: -200px 0 0 50px;
    z-index: 100; display: none" clientidmode="Static">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
        <tr>
            <td>
                <b>
                    <asp:Label ID="lbl" runat="server"></asp:Label>
                </b>
            </td>
        </tr>
        <tr>
            <td>
                <strong>&nbsp;&nbsp;
                    <asp:Label ID="lblConnDisconn" runat="server" Text="Do you want to remove post?"
                        Font-Size="Small"></asp:Label>
                </strong>
            </td>
        </tr>
        <tr>
            <td class="popBgLineGray">
            </td>
        </tr>
        <tr>
            <td align="right">
                <table width="100" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <a href="#" clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                text-decoration: none; width: 82px; padding-top: 0px; color: #000;" onclick="MessageClose();">
                                Cancel </a>
                        </td>
                        <td style="padding-right: 20px;">
                            <asp:LinkButton ID="lnkConnDisconn" runat="server" ClientIDMode="Static" Text="Yes"
                                CssClass="joinBtn" OnClick="lnkConnDisconn_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div id="divMessSucces" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
    float: left; width: 500px; padding-top: 20px; position: relative; margin: -400px 0 0 50px;
    z-index: 100; display: none;" clientidmode="Static">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
        <tr>
            <td class="popHeading">
                <asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="Label3" runat="server"></asp:Label>
                </b>
            </td>
        </tr>
        <tr>
            <td>
                <strong>&nbsp;&nbsp;
                    <asp:Label ID="Label4" runat="server" Text="Post removed successfully." Font-Size="Small"></asp:Label>
                </strong>
            </td>
        </tr>
        <tr>
            <td class="popBgLineGray">
            </td>
        </tr>
        <tr>
            <td align="right">
                <table width="100" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <a href="#" clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                text-decoration: none; width: 82px; padding-top: 5px; color: #000;" onclick="MessageClose();">
                                Close </a>
                        </td>
                        <td style="padding-right: 20px;">
                            <%-- <asp:LinkButton ID="LinkButton1"  runat="server" ClientIDMode="Static" Text="ok" CssClass="joinBtn"
                                                             OnClientClick="MessageClose();"  ></asp:LinkButton>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
