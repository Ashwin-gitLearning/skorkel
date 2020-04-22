<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="ViewActivity.aspx.cs" Inherits="ViewActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
   <div class="main-section-inner">
    <div class="panel-cover">
        <div class="full-width-con">
            
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <asp:ListView ID="lstViewActivity" runat="server" OnItemDataBound="lstViewActivity_ItemDataBound"
                OnItemCreated="lstViewActivity_ItemCreated">
                <ItemTemplate>
                
                   <div class="activity-cover">
                   <span class="date-clip"> <asp:Label ID="lblAddedOn" Text='<%#Eval("dtAddedOn") %>' runat="server"></asp:Label></span>                    
                  
                    <asp:ListView ID="lstChildViewActivity" runat="server" OnItemCommand="lstChildViewActivity_ItemCommand"
                        OnItemDeleting="lstChildViewActivity_ItemDeleting" DataKeyNames="Id">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnPkId" Value='<%# Eval("Id") %>' runat="server" />
                            <asp:HiddenField ID="hdnRegID" Value='<%# Eval("intAddedBy") %>' runat="server" />
                            <asp:HiddenField ID="hdnTableName" Value='<%# Eval("TableName") %>' runat="server" />
                            <asp:HiddenField ID="hdnstrActivity" Value='<%# Eval("Title") %>' runat="server" />
                              <div class="card card-list-con">
                                 <div class="top-list activity-card">
                                 <div class="post-con">
                                   <div class="post-header">
                                    <span class="question-icon">
                                     <span class="icon">
                                      <asp:Image id="iconImage" runat="server" class="img-fluid rounded-circle" />
                                      <asp:Label ID="txtIcon" runat="server" Visible="false"></asp:Label>
                                     </span><%--src="images/groupPhoto.jpg"--%>
                                    </span>
                                     <%--<asp:Image ID="Image1" runat="server" /><asp:ImageButton ID="ImageButton1" runat="server" />--%>
                                   <ul class="que-con">
<!--                                      <li>Question</li>-->
                                   <%--<li><asp:Label ID="lblCommentUser" Text="" Visible="false" runat="server"></asp:Label></li>--%>
                                   <li>
                                    <asp:Label ID="lblCommentUser" Visible="false" runat="server"  Style="display:none"></asp:Label>
                                    <asp:Label ID="lblnotificationname" runat="server" class="remove-after-comma"></asp:Label>
                                     <strong>
                                      <asp:LinkButton ID="lnkName" Text='<%# Eval("Addedby") %>' CommandName="Details"
                                       ToolTip="View Profile" class="lnkName" runat="server">
                                      </asp:LinkButton>
                                     </strong>
                                   </li>
                                   <li><asp:Label ID="lblTime" Text='<%# Eval("AddedTime") %>' class="lblTime" runat="server"></asp:Label></li>
                                   </ul>
                                   </div><!-- post-header ended -->
                                   
                                    <div class="post-body" id="divPostBody" runat="server" style="display:none;">
                                     <p><asp:Label ID="lblComment" Text="" Visible="false" runat="server"></asp:Label></p>
                                    </div><!-- post-body ended -->
                                   
                                    </div><!-- post-con ended -->
                                  </div><!-- top-list ended -->
                            </div><!-- card ended -->  
                            
                            <asp:LinkButton ID="lnkShareDetail" CommandName="ShareDetails" runat="server"></asp:LinkButton>             
                                    
                     </ItemTemplate>
                    </asp:ListView>
                    
                    </div><!-- activity-cover ended -->
                </ItemTemplate>
            </asp:ListView>
        
        
           <div id="dvHeight" runat="server" class="adv">
           </div>
        
           </div><!-- full-width-con ended -->
         </div><!-- panel-cover ended -->
       </div><!-- main-section-inner ended -->
   
    <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
    <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
    <div class="cls">
    </div>
</asp:Content>
