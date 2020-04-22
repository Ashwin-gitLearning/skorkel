<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AcceptMember.ascx.cs"
    Inherits="UserControl_AcceptMember" %>
<asp:UpdatePanel ID="updates" runat="server">
    <ContentTemplate>
        <div id="divPopupMember" clientidmode="Static" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
            float: left; width: 500px; position: fixed; margin: -19% 0 0 12%; z-index: 100;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                <tr>
                    <td class="popHeading">
                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <asp:Label ID="lblTitleGroup" runat="server"></asp:Label>
                        </b>
                    </td>
                </tr>
                <tr>
                    <td class="popBgLineGray">
                    </td>
                </tr>
                <tr id="tdddlRoles" runat="server" style="display: none;">
                    <td>
                        &nbsp;&nbsp;<asp:DropDownList ID="ddlRoleDetails" CssClass="txtFieldNew" ClientIDMode="Static"
                            Style="width: 170px;" runat="server">
                        </asp:DropDownList>
                        <br />
                        &nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="reqRole" runat="server" InitialValue="Select Role"
                            ControlToValidate="ddlRoleDetails" ErrorMessage="Select role from the list. "
                            ValidationGroup="Insert" Text="Select role from the list." Font-Size="Small"></asp:RequiredFieldValidator>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>&nbsp;&nbsp;
                            <asp:Label ID="lblMessage" runat="server" Font-Size="Small"></asp:Label>
                            <textarea id="txtBody" runat="server" cols="20" rows="2" value="Message" onblur="if(this.value=='') this.value='Message';"
                                onfocus="if(this.value=='Message') this.value='';" class="forumTitle" style="width: 400px;
                                font-size: small; margin-left: 21px; margin-right: 50px; margin-top: 5px"></textarea>
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <table width="100" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Yes" CssClass="joinBtn"
                                        ValidationGroup="Insert" OnClick="lnkPopupOK_Click"></asp:LinkButton>
                                </td>
                                <td style="padding-right: 20px;">
                                    <asp:LinkButton ID="lnkcose" runat="server" CommandName="Join" CausesValidation="false"
                                        Style="float: left; text-align: center; text-decoration: none; width: 82px; color: #000;
                                        padding-bottom: 11px;" ClientIDMode="Static" OnClick="lnkcose_Click">Close </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divSuccessAcceptMember" runat="server" style="border: 20px solid rgba(0,0,0,0.5);
            float: left; width: 500px; padding-top: 0px; position: fixed; margin: -200px 0 0 11%;
            z-index: 100; display: none;" clientidmode="Static">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                <tr>
                    <td>
                        <strong>&nbsp;&nbsp;
                            <asp:Label ID="lblSuccess" runat="server" Font-Size="Small"></asp:Label>
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
                                    <asp:LinkButton ID="lnkSuccessFailure" runat="server" Text="Ok" ClientIDMode="Static"
                                        CausesValidation="false" Style="float: left; text-align: center; text-decoration: none;
                                        width: 82px; padding-top: 5px; color: #000;" OnClick="lnkSuccessFailure_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
                function JobMesClose() {
                    document.getElementById("divPopupMember").style.display = 'none';
                }
            </script>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkcose" />
        <asp:AsyncPostBackTrigger ControlID="lnkPopupOK" />
    </Triggers>
</asp:UpdatePanel>
