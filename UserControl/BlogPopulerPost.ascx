<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlogPopulerPost.ascx.cs"
    Inherits="UserControl_BlogPopulerPost" %>
<div class="popularPost">
    <ul class="list-unstyled">
        <asp:Repeater ID="RepPopPost" runat="server" OnItemCommand="RepPopPost_ItemCommand">
            <ItemTemplate>
                <asp:HiddenField ID="hdnintBlogId" runat="server" Value='<%#Eval("intBlogId")%>' />
                <li>
                    <asp:LinkButton ID="Label1" runat="server" CommandName="Linkhead" Text='<%#Eval("strBlogHeading")%>'></asp:LinkButton>   
                    <asp:Label ID="lbladddedBy" runat="server" CssClass="author-name"
                        CommandName="Linkhead" Text=' <%# "by " + Eval("strAddedBy")%>'></asp:Label>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<div class="text-center show-more-seprator remove-body-fixed-class" id="divShomore" runat="server">
 <asp:LinkButton ID="lnlView" runat="server" Text="Show More" OnClick="lnkview_Click">
 </asp:LinkButton>
</div>    
<script type="text/javascript">
    function hideRight() {
        $(".right-panel").removeClass("openRightPanel");
        $(".right-panel-back-layer").removeClass("active");
        $("body").removeClass("overflowHidden");
    }
    function showRight() {
        $(".right-panel").addClass("openRightPanel");
        //setTimeout(function () {
        //    $(".right-panel-back-layer").addClass("active");
        //}, 100);
        $("body").addClass("overflowHidden");
    }
    function setActive() {
         $(".right-panel-back-layer").addClass("active");
    }
</script>
 
