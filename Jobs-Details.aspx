<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Jobs-Details.aspx.cs" Inherits="Jobs_Details" %>
<%@ Register TagPrefix="usc" TagName="UserControl_DragNDrop" Src="~/UserControl/DragNDrop.ascx" %>
<%--<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="main-section-inner">
  <div class="back-link-cover news-details-back-m">
   <asp:LinkButton ID="lnkBack" runat="server" CssClass="back-link" Font-Underline="false"
    PostBackUrl="~/AllJobsListing.aspx"><span class="lnr lnr-arrow-left"></span>Back to Jobs</asp:LinkButton>           
  </div>
  <div class="panel-cover clearfix">
   <div class="full-width-con common-user">
    <asp:UpdatePanel ID="pnlParent" runat="server">
     <ContentTemplate>
      <div class="card card-list-con jobs-con">

      <div class="post-con">
       <div class="post-header">      
       
       <asp:HiddenField ID="hdnPostUpdateId" runat="server" ClientIDMode="Static" Value='<%#Eval("ID")%>' />
       <span class="float-right small-date">
        <asp:Label ID="dtAddedOn" runat="server" Text='<%#Eval("Created_timestamp") %>' />
       </span>
       <h3>            
        <asp:Label ID="lblTitle" runat="server" Font-Underline="false" CssClass="commentQA"
         Text='<%#Eval("Title") %>'></asp:Label>
        <span id="span_status" runat="server" class="badge alignments-btn-details badge-success">
         <%--<asp:Label ID="lblCounter" runat="server" Text="32" />--%>
         <asp:Label ID="lblStatus" runat="server" Text="Applied" />
        </span>
       </h3>
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

       <div class="post-body clearfix">
        <p class="mb-0">
         <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>' />
        </p>
       
        <a href="#" id="lnkApply" runat="server" class="btn hide-button-ios btn-primary float-right m-t-10 journal-comp" visible="false" data-toggle="modal" data-target="#jobModal"
         onclick="$('#lblErrorMsg').text('');">
         Apply
        </a>
        <div id="divuploadIOS" runat="server" visible="false" >
           <a href="#" id="lnkuploadIOS" class="btn hide-burron-ios btn-primary float-right m-t-10 journal-comp">
         Apply
        </a>
         </div>
       
        <div id="jobModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
         <div class="modal-dialog modal-dialog-centered" role="document">
          <div class="modal-content">
           <div class="modal-header">
            <h5 id="exampleModalLabel" class="modal-title">Submit Resume</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="callClose();">
             <span aria-hidden="true">×</span>
            </button>
           </div>
           <div class="modal-body text-left">
            <form action="" method="">
             <div class="form-group">
              <label>Upload Resume</label>
              <div class="form-group">
               <usc:UserControl_DragNDrop ID="ddUploader1" runat="server" ClientIDMode="Static" OnDelete="upldrFile_delete" />
               <%--<asp:HiddenField ID="hdnUploader1" runat="server" ClientIDMode="Static" Value='' />--%>
              </div>              
              <span class="grey-text font-size-12">Note: Only PDF or DOC/DOCX file Support, Max File Size 5MB</span>
             </div>
             <p>
              <asp:Label ID="lblErrorMsg" runat="server" CssClass="RedErrormsg" Visible="false" ForeColor="Red" Text="" ClientIDMode="Static"></asp:Label>
             </p>
             <div class="submit-con">
              <%--<button class="btn btn-outline-primary m-r-15">Cancel</button>--%>
              <asp:Button ID="btnCancel" runat="server" type="Reset" Text="Cancel" class="btn btn-outline-primary m-r-15" data-dismiss="modal" ClientIDMode="Static"
               OnClientClick="javascript:messageClose();" OnClick="btnCancel_Click"><%--OnClientClick="javascript:callClose();" --%>
              </asp:Button>
              <%--<button class="btn btn-primary">Submit</button>--%>
              <asp:LinkButton ID="lnkSubmit" runat="server" Text="Submit" CssClass="btn hide-body-scroll btn-primary" ClientIDMode="Static" 
               OnClick="lnkSubmit_Click" ValidationGroup="vldJobs" />
             </div>
            </form>
           </div>
          </div>
         </div>
        </div>
         <!-- Modal ended --> 
       </div>
      </div>
       <!-- post-con ended -->     
      </div>
       <!-- full-width-con ended -->
     </ContentTemplate>
     <%--<Triggers>
      <asp:AsyncPostBackTrigger ControlID="lnkSubmit" />
     </Triggers>--%>
    </asp:UpdatePanel>
   </div>
  </div>
    <!-- panel-cover ended -->
  </div>
  <!-- main-section inner ended -->
 <script type="text/javascript">
   function callClose() {
    debugger;
       $('#lblErrorMsg').val('');
       $('#jobModal').modal('hide');  
       document.getElementById('btnCancel').click();
       //document.getElementById('jobModal').style.display = 'none';
       //$('#jobModal').modal('toggle');
   }

   function messageClose() {
       debugger;
       //if ($('#lblErrorMsg').val() != "") {
       //    $('#jobModal').modal('show');
       //}
       //else {
       //    $('#jobModal').modal('hide');
       //}
       $('#lblErrorMsg').val('');
       $('#jobModal').modal('hide'); 
   }

   function OverlayBody() {
        $('#bodyelement').addClass("remove-scroller");
   }
 </script>
</asp:Content>

