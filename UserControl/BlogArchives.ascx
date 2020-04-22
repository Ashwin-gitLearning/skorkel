<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogArchives.ascx.cs"
    Inherits="UserControl_BlogArchives" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="divTree">
            <asp:TreeView ID="TreeArchives" runat="server">
            </asp:TreeView>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
