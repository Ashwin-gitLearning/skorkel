<%@ Page Title="" Language="C#" MasterPageFile="~/Main_Super.master" AutoEventWireup="true" CodeFile="CSVFileUploader.aspx.cs" Inherits="CSVFileUploader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="main-section-inner">
  <div class="panel-cover clearfix">
   <div class="full-width-con super-admin">
    <div style="color: darkslateblue;"> 
     <div class="super-admin-comp">   
      <h5>    
       CSV File Uploader<br />
      </h5>  
     </div> 
     <div class="card card-list-con"> 
      <div class="list-group-item top-list">
       <div class="post-con">
        <div class="post-body pb-3">
         Select File:
         <asp:FileUpload ID="fileUpload" runat="server" accept=".csv"/>     
         <asp:LinkButton class="btn btn-outline-primary ml-2 mr-2" runat="server" Text="Download CSV" OnClick="btnDownload_Click"/>  
         <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" Text="Upload" OnClick="btnUpload_Click" OnClientClick="showLoader1();" />  
         <p><asp:Label ID="lblError" runat="server" CssClass="error_message hide-outside-click" Visible="false" /></p>
        </div>
       </div>
      </div>
     </div>
     <br /><br />       
    </div>  
   </div>
  </div>
 </div>  
</asp:Content>

