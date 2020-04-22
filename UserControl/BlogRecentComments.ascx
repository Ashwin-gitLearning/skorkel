<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogRecentComments.ascx.cs"
    Inherits="UserControl_BlogRecentComments" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="divRepeater">
            <div class="listHeading">
                <asp:Repeater ID="RepComments" runat="server" OnItemCommand="RepComments_ItemCommand">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td align="center">
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="left" style="width: 80%; vertical-align: top">
                                <img id="img3" src="images/reply-gray.png" />
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("strComment")%>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                              <%--  <div>
                                    ........................................</div>--%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="viewAll" style="text-align: right;">
                <asp:LinkButton ID="lnlView" PostBackUrl="~/AllBlogs.aspx" runat="server" Text="View All"></asp:LinkButton>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
