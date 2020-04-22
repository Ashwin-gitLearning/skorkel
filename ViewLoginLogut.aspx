<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ViewLoginLogut.aspx.cs" Inherits="ViewLoginLogut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <!--heading ends-->
    <div class="cls">
    </div>
    <!--inner container starts-->
    <div class="cls">
    </div>
    <!--inner container ends-->
    <div class="innerContainer" style="background: #fff; float: left">
        <!--middle container starts-->
        <div class="NmiddleContainer">
            <!--search result list starts-->
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <asp:ListView ID="lstViewLog" runat="server" >
                <ItemTemplate>
                          <p>                                
                                <asp:Label ID="lblLogin" runat="server" Text="Last Login Date :"></asp:Label>
                                <asp:Label ID="lnkLoginDate" Text='<%# Eval("LoginDate") %>' 
                                    ToolTip="Login Date" Style="color: #00B6BD; font-size: 12px; text-decoration: none;margin-left: 86px;"
                                    runat="server"></asp:Label>&nbsp;
                                <asp:Label ID="lblLoginTime" Text='<%# Eval("LoginTime") %>' Style="color: #00B6BD; font-size: 11px;
                                    text-decoration: none;" runat="server"></asp:Label>
                                    <br /><br />
                                    <asp:Label ID="lbllogout" runat="server" Text="Last Logout Date :"></asp:Label>
                                <asp:Label ID="lnklogoutdt" Text='<%# Eval("LogOutDate") %>'
                                    ToolTip="Login Date" Style="color: #00B6BD; font-size: 12px; text-decoration: none;margin-left: 77px;"
                                    runat="server"></asp:Label>&nbsp;
                                <asp:Label ID="lbllogouttime" Text='<%# Eval("LogOutTime") %>' Style="color: #00B6BD; font-size: 11px;
                                    text-decoration: none;" runat="server"></asp:Label>
                                    <br /><br />
                                    <asp:Label ID="lblpass" runat="server" Text="Last Password Change Date :"></asp:Label>&nbsp;
                                <asp:Label ID="lnkpassdate" Text='<%# Eval("PasswordDate") %>'
                                    ToolTip="Login Date" Style="color: #00B6BD; font-size: 12px; text-decoration: none;margin-left: 0%;"
                                    runat="server"></asp:Label>&nbsp;
                                <asp:Label ID="lblpasstime" Text='<%# Eval("PasswordTime") %>' Style="color: #00B6BD; font-size: 11px;
                                    text-decoration: none;" runat="server"></asp:Label>
                            </p>
                    <div class="bgLine" style="width: 920px;">
                        <img src="images/spacer.gif" height="2" width="920" /></div>
                </ItemTemplate>
            </asp:ListView>
            <!--search result list ends-->
        </div>
        <!--middle container ends-->
        <div id="dvHeight" runat="server" style="height: 350px;" class="adv">
        </div>
    </div>
    <!--pagination starts-->
    <!--pagination ends-->
    <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
    <div class="cls">
    </div>

</asp:Content>

