<%@ Page Title="" Language="C#" MasterPageFile="~/Main_Super.master" AutoEventWireup="true" CodeFile="SA_News-Details.aspx.cs" Inherits="SA_News_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="main-section-inner">
  <div class="panel-cover clearfix">
   <div class="full-width-con super-admin">
    <div class="card-list-con">
     <div class="card top-list">
      <div class="post-con">
       <div class="post-body p-b-15 multi-para">
        <asp:UpdatePanel ID="pnlParent" runat="server">
         <ContentTemplate>
          
            <h3>
             <asp:Label ID="lblNewsHeading" runat="server" Text='<%#Eval("Title") %>'>
             </asp:Label>
            </h3>
            <p class="m-t-5">
             <asp:Label ID="lblNewsDetails" runat="server" Text='<%#Eval("Content") %>'>
             </asp:Label>
            </p>
           
         </ContentTemplate>
        </asp:UpdatePanel>
        
       </div>
      </div>
     </div><!-- card ended -->       
    </div> <!-- card-list-con ended -->
    
   </div><!-- full-width-con ended -->      
  </div><!-- panel-cover ended -->
 </div>
</asp:Content>

