<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Groups-m.ascx.cs" Inherits="UserControl_GroupsM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!--groups top box starts-->
<!-- <span class="m-view back">Back <span class="lnr lnr-arrow-right"></span></span> -->
<div class="m-aside-trigger mt-0">
    <span class="lnr lnr-arrow-left"></span>

        <asp:UpdatePanel ID="updatess" runat="server" class="d-flex align-items-center">
           <ContentTemplate>
              <asp:Label ID="lblMessage" runat="server"></asp:Label>
             
                 <span class="avatar-img">
                    <img id="imgGrp" runat="server" class="header-avatar rounded-circle"/>
                 </span>
               <asp:Label ID="lblGroupName" runat="server" class="avatar-text truncate-group-name-m top-0"></asp:Label>
                  
             
           
             
 
           </ContentTemplate>

        </asp:UpdatePanel>
    
</div>    


