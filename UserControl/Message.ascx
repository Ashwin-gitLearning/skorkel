<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Message.ascx.cs" Inherits="UserControl_Message" %>
<script type="text/javascript">
    function Cancel() {
        document.getElementById("divSuccessMess").style.display = 'none';
        return false;
    }

    function CommonMessage() {
        document.getElementById("dvPopup").style.display = 'none';
        return false;
    }

</script>
<script type="text/javascript">
    $(document).ready(function () {
//        $('#dvPopup').center();
    });
</script>
<style>
    .MessageSubject
    {
        font-family: Arial;
        border: 1px solid #c9cbcd;
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        -khtml-border-radius: 3px;
        -ms-border-radius: 3px;
        border-radius: 3px;
        padding: 4px 2px;
        margin: 5px 50px;
        width: 372px;
    }
    .MessageBody
    {
        font-family: Arial;
        font-size: 13px;
        width: 372px;
        border: 1px solid #c9cbcd;
        -moz-border-radius: 3px;
        color: #9c9c9c;
        font-size: 15px;
        margin-top: -16px;
        -webkit-border-radius: 3px;
        -khtml-border-radius: 3px;
        -ms-border-radius: 3px;
        border-radius: 3px;
        padding: 4px 2px;
        margin-left: 50px;
        resize: vertical;
    }
</style>
<asp:UpdatePanel ID="updatesmsg" runat="server"><ContentTemplate> 
<div id="dvPopup" runat="server" style="border: 20px solid rgba(0,0,0,0.5); float: left;
    width: 500px; position: fixed; margin: -295px 0 0 11%; z-index: 100; display: block" clientidmode="Static">
<%--<div id="dvPopup" runat="server" style="border: 20px solid rgba(0,0,0,0.5); width: 500px;
    z-index: 100; display: block; margin-left: 9%;" clientidmode="Static">--%>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
        <tr>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="popHeading">
                           <span style="color:#9c9c9c;"> Message</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="popBgLineGray">
            </td>
        </tr>
        <tr>
            <td>
                <p>
                    <asp:TextBox ID="txtSubject" Value="Subject" CssClass="MessageSubject" onblur="if(this.value=='') this.value='Subject';"
                        onfocus="if(this.value=='Subject') this.value='';" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject"
                        Style="margin-left: 50px;" InitialValue="Subject" ErrorMessage="Please enter subject."
                        ValidationGroup="Mess"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <textarea id="txtBody" runat="server" value="Message" onblur="if(this.value=='') this.value='Message';"
                        onfocus="if(this.value=='Message') this.value='';" class="MessageBody"></textarea>
                </p>
                <div style="padding-left: 50px;margin-top: -13px;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="Message" style="font-size:15px;"
                        ControlToValidate="txtBody" Display="Dynamic" ValidationGroup="Mess" ErrorMessage="Please enter message"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </td>
        </tr>
        <tr>
            <td class="popBgLineGray">
            </td>
        </tr>
        <tr>
            <td align="right" style="padding-right: 40px; padding-bottom: 5px;">
                <table width="100" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Send" style="width: 83px;"
                                ValidationGroup="Mess" CssClass="joinBtn" OnClick="lnkPopupOK_Click"></asp:LinkButton>
                            <%--<asp:LinkButton ID="btnCancel" CommandName="Join" CausesValidation="false" Style="float: left;
                                text-align: center; text-decoration: none; width: 82px; color: #000; padding-bottom:5px;" runat="server"
                                Text="Cancel" OnClick="btnCancel_Click"></asp:LinkButton>--%>
                        </td>
                        <td>
                          <%--  <a clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                text-decoration: none; width: 82px; padding-top: 0px; color: #000 !important;
                                cursor: pointer" onclick="javascript:CommonMessage();">Cancel </a>--%>
                                 <asp:LinkButton ID="btnCancel" CommandName="Join" CausesValidation="false" Style="float: right;
                                                text-align: center; text-decoration: none; width: 82px; padding-top: 7px; color: #000;"
                                                runat="server" Text="Cancel" OnClick="btnCancel_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div id="divSuccessMess" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
    float: left; width: 500px; padding-top: 0px; position: fixed; margin: -210px 0 0 11%;
    z-index: 100; display: none;" clientidmode="Static">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
        <tr>
            <td>
                <strong>&nbsp;&nbsp;
                    <asp:Label ID="lblSuccess" runat="server" Text="" Font-Size="Small"></asp:Label>
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
                            <a clientidmode="Static" causesvalidation="false" style="float: left; text-align: center;
                                text-decoration: none; width: 82px; padding-top: 1px; color: #000; cursor: pointer"
                                onclick="Cancel();">Close </a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
</ContentTemplate> </asp:UpdatePanel>