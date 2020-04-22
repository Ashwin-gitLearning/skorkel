<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="News-Details.aspx.cs" Inherits="News_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" Runat="Server">
    <script type="text/javascript">
        function SetTarget() {
         debugger;
         document.forms[0].target = "_blank";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="main-section-inner">
  <div class="back-link-cover news-details-back-m">
   <asp:LinkButton ID="lnkBack" runat="server" CssClass="back-link" Font-Underline="false"
    OnClick="lnkBack_click"><span class="lnr lnr-arrow-left"></span>Back to News</asp:LinkButton>           
  </div>
  <div class="panel-cover clearfix">
   <div class="full-width-con common-user">
    <div class="card-list-con">
     <div class="card top-list">
      <div class="post-con">
       <div class="post-body p-b-15 multi-para news-detailss">
        <asp:UpdatePanel ID="pnlParent" class="RSS-feed" runat="server">
         <ContentTemplate>
          
            <h3>
             <asp:Label ID="lblNewsHeading" runat="server" Text='<%#Eval("Title") %>'>
             </asp:Label>
            </h3>
            <ul class="small-date">
             <%--<li>By <asp:Label ID="lblUserName" runat="server" /></li>--%>
             <li>Source: <asp:LinkButton ID="lblSource" runat="server" /></li><%--Text='<%#Eval("Type") %>' OnClientClick="SetTarget();" OnClick="lblSource_click"--%>
             <li><asp:Label ID="dtAddedOn" runat="server" Text='<%#Eval("Created_timestamp") %>' /></li>
            </ul>
            <p class="m-t-5">
             <asp:Label ID="lblNewsDetails" runat="server" Text='<%#Eval("Content") %>'>
             </asp:Label>
            </p>
           
         </ContentTemplate>
        </asp:UpdatePanel>
        <%--<h3>Lorem ipsum dolor sit amet, consectetur adipiscing elit</h3>
        
        <p class="m-t-5">Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab 
         illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo natus error sit voluptatem accusantium doloremque laudantium, 
         totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo natus error sit voluptatem accusantium 
         doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo natus error sit 
         voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.
        </p>

        <p> Perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis 
         et quasi architecto beatae vitae dicta sunt explicabo natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo 
         inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, 
         eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo natus error sit voluptatem accusantium doloremque laudantium, 
         totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. </p>

        <p> Perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et 
         quasi architecto beatae vitae dicta sunt explicabo natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo 
         inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, 
         eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo natus error sit voluptatem accusantium doloremque laudantium, 
         totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. </p>--%>
        
       </div>
      </div>
     </div><!-- card ended -->       
    </div> <!-- card-list-con ended -->
    
   </div><!-- full-width-con ended -->      
  </div><!-- panel-cover ended -->
 </div>
</asp:Content>

