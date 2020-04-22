<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AllJobsListing.aspx.cs" Inherits="AllJobsListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="main-section-inner">
                    
  <div class="panel-cover clearfix">
   <div class="full-width-con super-admin">
    <asp:UpdatePanel ID="pnlParent" runat="server">
     <ContentTemplate>
     <%--<asp:HiddenField ID="hdnintJobsDelete" runat="server" ClientIDMode="Static" Value='' />--%>
     <div class="btn-title-con m-t-15-minus">
      <div>
       <h5 class="card-title">Jobs</h5> 
      </div>
      
     </div>
       <!-- btn-title-con ended  -->
    <asp:UpdatePanel ID="pnlJobsListing" runat="server">
     <ContentTemplate>
      <asp:ListView ID="lstJobsListing" runat="server" OnItemDataBound="lstJobs_ItemDataBound" OnItemCommand="lstJobs_ItemCommand">
       <ItemTemplate>
        <asp:HiddenField ID="hdnPostUpdateId" runat="server" ClientIDMode="Static" Value='<%#Eval("ID")%>' />
        <div class="card card-list-con jobs-con">
         <div class="post-con">
          <div class="post-header">
                
          <span class="float-right small-date">
           <asp:Label ID="dtAddedOn" runat="server" Text='<%#Eval("Created_timestamp") %>' />
          </span>
          <h3 class="d-flex align-items-md-center align-items-lg-center align-items-end">            
           <asp:LinkButton ID="lblTitle" runat="server" Font-Underline="false" CommandName="JobsDetail" CssClass="commentQA title-truncate"
            Text='<%#Eval("Title") %>'></asp:LinkButton> 
           <span id="span_status" runat="server" class="badge ml-2 badge-success" visible="false">
            <%--<asp:Label ID="lblCounter" runat="server" Text="32" />--%>
            <asp:Label ID="lblStatus" runat="server" Text="Applied" />
           </span>
          </h3>
          
          <%--<asp:LinkButton ID="lnkJobsEdit" runat="server" CommandName="Edit Jobs" Font-Underline="false" CssClass="dropdown-item">
           <span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>--%>
          <ul class="small-date">
           <li id="li_salary" runat="server" visible="false">
            <strong>
             <asp:Label ID="lblStartingSalary" runat="server" Text='<%#Eval("StartingSalary") %>' /> - <asp:Label ID="lblEndingSalary" runat="server" Text='<%#Eval("EndingSalary") %>' />
            </strong>
           </li>
           <li id="li_location" runat="server" visible="false">
            <asp:Label ID="lblLocation" runat="server" Text='<%#Eval("Location") %>' />
           </li>
           <li id="li_jobtype" runat="server" visible="false">
            <asp:Label ID="lblJobType" runat="server" Text='<%#Eval("JobType") %>' />
           </li>
           <li id="li_duration" runat="server" visible="false">
            <asp:Label ID="lblStartDuration" runat="server" Text='<%#Eval("StartDuration") %>' /> to 
            <asp:Label ID="lblEndDuration" runat="server" Text='<%#Eval("EndDuration") %>' />
           </li>
          </ul>
          </div>
          
          <div class="post-body">
           <p class="mb-0">
            <asp:Label ID="lblDescription" runat="server" class="moreQA" Text='<%#Eval("Description") %>' /> 
           </p>
          </div>
         </div>
          <!-- post-con ended -->
        </div>
       </ItemTemplate>
      </asp:ListView>
     </ContentTemplate>
    </asp:UpdatePanel>    
     <!---Pagination Start-->
       <div class="pagination_main_div">
        <nav aria-label="Page navigation example">
         <ul id="dvPageJobs" runat="server" class="pagination" clientidmode="Static" visible="false">
          <asp:LinkButton ID="lnkPreviousJobs" runat="server" OnClick="lnkPreviousJobs_Click" ClientIDMode="Static" class="page-link">                              
           <span class="lnr lnr-chevron-left">
          </asp:LinkButton>
                             
          <asp:Repeater ID="rptDvPageJobs" runat="server" OnItemCommand="rptDvPageJobs_ItemCommand" OnItemDataBound="rptDvPageJobs_ItemDataBound">
           <ItemTemplate>
            <li class="page-item">
             <asp:LinkButton ID="lnkPageLinkJobs" runat="server" ClientIDMode="Static" CommandName="PageLink" class="page-link" Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
            </li>
           </ItemTemplate>
          </asp:Repeater>
          <asp:LinkButton ID="lnkNextJobs" runat="server" class="page-link" OnClick="lnkNextJobs_Click" ClientIDMode="Static">
           <span class="lnr lnr-chevron-right"></span></asp:LinkButton>
          
          <asp:HiddenField ID="hdnTotalItemJobs" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnNextPageJobs" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnLastPageJobs" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnPreviousPageJobs" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnCurrentPageJobs" runat="server" ClientIDMode="Static" Value="1" />
          <asp:HiddenField ID="hdnEndPageJobs" runat="server" ClientIDMode="Static" />
         </ul>
        </nav>  
       </div>
      <!---Pagination End-->
     </ContentTemplate>
    </asp:UpdatePanel>

   </div>
    <!-- full-width-con ended -->
  </div>
   <!-- panel-cover ended -->
                  
 </div>
  <script type="text/javascript">
     $(document).ready(function () {

        var showChar = 500;
        var ellipsestext = "...";
        var moretext = "Read more";
        var lesstext = "Less";
        $('.moreQA').each(function () {
            var content = $(this).html();
            if (content.length > showChar) {
                var c = content.substr(0, showChar);
                var h = content.substr(showChar - 1, content.length - showChar);
                var html = c + '<span class="moreelipses">' + ellipsestext + '</span>';
                $(this).html(html);
            }
        });
        $(".morelinkQA").click(function () {
            if ($(this).hasClass("less")) {
                $(this).removeClass("less");
                $(this).html(moretext);
            } else {
                $(this).addClass("less");
                $(this).html(lesstext);
            }
            $(this).parent().prev().toggle();
            $(this).prev().toggle();
            return false;
         });
          if ($('#hdnCurrentPageJobs').val() == $('#hdnEndPageJobs').val()) {
            $('#lnkNextJobs').css("display", "none");
        }

         $("#dvPageJobs a:enabled").on('click', function (event) { showLoader1(); });

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {

         var showChar = 500;
         var ellipsestext = "...";
         var moretext = "Read more";
         var lesstext = "Less";
         $('.moreQA').each(function () {
             var content = $(this).html();
             if (content.length > showChar) {
                 var c = content.substr(0, showChar);
                 var h = content.substr(showChar - 1, content.length - showChar);
                 var html = c + '<span class="moreelipses">' + ellipsestext + '</span>';
                 $(this).html(html);
             }
         });
         $(".morelinkQA").click(function () {
             if ($(this).hasClass("less")) {
                 $(this).removeClass("less");
                 $(this).html(moretext);
             } else {
                 $(this).addClass("less");
                 $(this).html(lesstext);
             }
             $(this).parent().prev().toggle();
             $(this).prev().toggle();
             return false;
          });
          
             if ($('#hdnCurrentPageJobs').val() == $('#hdnEndPageJobs').val()) {
                 $('#lnkNextJobs').css("display", "none");
              }
              $("#dvPageJobs a:enabled").on('click', function(event){showLoader1();});
    
        });
      });
   </script>
</asp:Content>


