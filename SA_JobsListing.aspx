<%@ Page Title="" Language="C#" MasterPageFile="~/Main_Super.master" AutoEventWireup="true" CodeFile="SA_JobsListing.aspx.cs" Inherits="SA_JobsListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <div class="main-section-inner">
                    
  <div class="panel-cover clearfix">
   <div class="full-width-con super-admin">
    <asp:UpdatePanel ID="pnlParent" runat="server">
     <ContentTemplate>
     <asp:HiddenField ID="hdnIsFiltered" runat="server" Value="false" />
     <%--<asp:HiddenField ID="hdnintJobsDelete" runat="server" ClientIDMode="Static" Value='' />--%>
     <div class="btn-title-con m-t-15-minus">
      <div>
       <h5 class="card-title">Jobs</h5> 
      </div>
      <div class="d-flex align-items-center">
       <asp:UpdatePanel ID="pnlUpperDropdown" runat="server" class="">
       <ContentTemplate>
       <div class="dropdown">
        <span id="sortFilter" class="sortFilter d-flex align-items-center" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> Filter by: &nbsp;
         <strong id="txtFilter" runat="server"> Active Jobs</strong><span class="icon-caret-down align-items-center"></span></span>
         <%--<span class="icon-caret-down"></span>
        </span>--%>
        <div class="dropdown-menu right-dropdown" aria-labelledby="dropdownMenuButton">
         <asp:LinkButton id="LnkActive" runat="server" class="dropdown-item" Text="Active" OnClick="LnkActiveClick"></asp:LinkButton>
         <asp:LinkButton id="LnkInActive" runat="server" class="dropdown-item" Text="Inactive" OnClick="LnkInActiveClick"></asp:LinkButton>
         <%--<a class="dropdown-item" href="#">Most Recent</a>
         <a class="dropdown-item" href="#">Most Popular</a>--%>
        </div>
       </div>
       </ContentTemplate>
       </asp:UpdatePanel>
       <asp:UpdatePanel ID="PnlJobsCreate" runat="server" UpdateMode="Conditional">
       <ContentTemplate>
       <a href="#" class="btn btn-primary float-right m-l-5 super-admin-comp" data-toggle="modal" data-target="#jobModal" onclick="$('#lnkCreate').text('Create');">Create</a>
                                            
       <div id="jobModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
       <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
         <div class="modal-header">
          <h5 id="exampleModalLabel" class="modal-title">Create Job</h5>
          <button type="button" class="close" data-dismiss="modal" onclick="javascript:callClose();" aria-label="Close">
           <span aria-hidden="true">×</span>
          </button>
         </div>
         <div class="modal-body text-left">
          <form action="" method="">
           <div class="form-group">
            <label for="textbox">Job Title</label>
            <asp:TextBox id="txtJobTitle" runat="server" class="form-control" maxlength="100" aria-describedby="emailHelp" placeholder="Job Title" 
             ClientIDMode="Static" autocomplete="off" />
            <asp:RequiredFieldValidator ID="rfdtxtJobTitle" runat="server" ControlToValidate="txtJobTitle" Display="Dynamic" ValidationGroup="vldJobs" 
             ErrorMessage="Please enter job title" ForeColor="Red" ClientIDMode="Static"></asp:RequiredFieldValidator>
           </div>

           <div class="form-group">
            <label for="">Job Description</label>
            <textarea id="txtJobDescription" rows="3" runat="server" class="form-control textarea-editor" clientidmode="Static" maxlength="2000" placeholder="Job Description" />
            <asp:RequiredFieldValidator ID="rfdtxtJobDescription" runat="server" ControlToValidate="txtJobDescription" Display="Dynamic" ValidationGroup="vldJobs" 
             ErrorMessage="Please enter job description" ForeColor="Red" ClientIDMode="Static"></asp:RequiredFieldValidator>
           </div>

           <div class="form-group">
            <label for="">Salary Range</label>
            <div class="row">
            <div class="col-6">
             <asp:TextBox ID="SalaryFrom" runat="server" class="form-control" maxlength="20" placeholder="Salary From" ClientIDMode="Static" />
            </div>
            <div class="col-6">
             <asp:TextBox ID="SalaryTo" runat="server" class="form-control" maxlength="20" placeholder="Salary To" ClientIDMode="Static" />
            </div>
            </div>
           </div>
           
           <div class="form-group">
            <label for="textbox">Location</label>
            <asp:TextBox id="txtLocation" runat="server" class="form-control" maxlength="50" aria-describedby="emailHelp" placeholder="Location" 
             ClientIDMode="Static" autocomplete="off" />
           </div>
           
           <div class="form-group">
            <label for="textbox">Type of Job</label>
             <asp:DropDownList ID="drpJobType" runat="server" ClientIDMode="Static" CssClass="form-control">
              <asp:ListItem Text="Internship" Value="Internship"></asp:ListItem>
              <asp:ListItem Text="Full Time" Value="Full Time"></asp:ListItem>
             </asp:DropDownList>
             <asp:RequiredFieldValidator ID="rfddrpJobType" runat="server" ControlToValidate="drpJobType" Display="Dynamic" ValidationGroup="vldJobs" 
              ErrorMessage="Please enter job type" ForeColor="Red" InitialValue="-1" ClientIDMode="Static"></asp:RequiredFieldValidator>
             <%--<select class="form-control" id="JobType">
              <option>Internship</option>
              <option>Full Time</option>
             </select>--%>
           </div>
           
           <div class="form-group">
            <label for="">Duration</label>
            <div class="row">
             <div class="col-6">
              <asp:TextBox ID="DurationFrom" runat="server" class="form-control" maxlength="20" ClientIDMode="Static" placeholder="Duration From" />
             </div>

             <div class="col-6">
              <asp:TextBox ID="DurationTo" runat="server" class="form-control" maxlength="20" ClientIDMode="Static" placeholder="Duration To" />
             </div>
            </div>
            <div id="dvsourceError" class="error_message"></div>
           </div>

           <div class="submit-con">
            <asp:HiddenField ID="hdnintStatusUpdateId" ClientIDMode="Static" Value="" runat="server" />
            <asp:Button ID="btnCancel" runat="server" type="Reset" Text="Cancel" class="btn btn-outline-primary m-r-15" data-dismiss="modal" 
             OnClientClick="javascript:callClose();">
            </asp:Button>
            <asp:LinkButton ID="lnkCreate" runat="server" Text="Create" CssClass="btn hide-body-scroll btn-primary" ClientIDMode="Static" 
             OnClientClick="javascript:messageClose(); CallPostNews();" OnClick="lnkCreate_Click" ValidationGroup="vldJobs" />
            <%--<button class="btn btn-outline-primary m-r-15">Cancel</button>
            <button class="btn btn-primary">Create</button>--%>
           </div>
          </form>
         </div>
        </div>
       </div>
      </div>
        <!-- Modal ended -->
       </ContentTemplate>
       <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkCreate" />
       </Triggers>
      </asp:UpdatePanel>
                                     
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
          <div class="post-header d-flex justify-content-between">
          <%--<span class="more-btn float-right super-admin-comp">
           <span class="dropdown">
            <a id="dropdownMenuLink" href="#" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
             <img src="images/more.svg" alt="" class="more-btn">
            </a>
          
            <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start" onclick="setJobsId.bind(this)()">             
             <asp:HiddenField ID="hdnJobsId" runat="server" ClientIDMode="Static" Value='<%#Eval("ID")%>' />
             <asp:LinkButton ID="lnkJobsEdit" runat="server" CommandName="Edit Jobs" Font-Underline="false" CssClass="dropdown-item">
              <span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>             
             <asp:LinkButton ID="lnkJobsdelete" runat="server" CommandName="Delete Jobs" Font-Underline="false" CssClass="dropdown-item hide-body-scroll"
              OnClientClick="javascript:docdeleteJobs();">
              <span class="lnr lnr-trash"></span> Delete</asp:LinkButton>
            </span>
          </span>
          </span>--%>

          <div class="job-title-listing">
           <h3>
            <asp:LinkButton ID="lblTitle" runat="server" Font-Underline="false" CommandName="JobsDetail" CssClass="commentQA title-truncate"
             Text='<%#Eval("Title") %>'></asp:LinkButton> &nbsp;
            <%--<asp:Label ID="lblTitle" runat="server" Font-Underline="false" CssClass="commentQA"
             Text='<%#Eval("Title") %>'></asp:Label> &nbsp;--%>
            <span id="span_status" runat="server" class="badge badge-success alignments-btn">
             <asp:Label ID="lblCounter" runat="server" />
             <asp:Label ID="lblStatus" runat="server" Text=" Applied" />
            </span>

            &nbsp;<asp:LinkButton ID="lnkJobsEdit" runat="server" CommandName="Edit Jobs" Font-Underline="false">
            <span class="lnr lnr-pencil font-size-16"></span></asp:LinkButton> 
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
          <div class="d-flex justify-content-between jobs-listing"> 
            <span class="small-date">
             <asp:Label ID="dtAddedOn" runat="server" Text='<%#Eval("Created_timestamp") %>' />
            </span>  &nbsp;  &nbsp;
            <asp:HiddenField ID="hdnJobsIdToggle" runat="server" Value='<%#Eval("ID")%>' />
            <div id="ToggleBtn" runat="server" class="material-toggle hide-body-scroll" visible="true">                
             <asp:CheckBox ID="chkToggle" runat="server" AutoPostBack="true" Text="&nbsp;"
              Checked="true" OnCheckedChanged="Check_Click" />
            
             <%--<asp:HiddenField ID="hf_reminderID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ReminderID") %>'/>--%>                 
            </div>
             <!-- more-btn ended -->            
              <%--<span class="lnr lnr-pencil "></span>--%>
          </div>      
        </div>
          
          <div class="post-body">
           <p>
            <asp:Label ID="lblDescription" runat="server" class="moreQA"  Text='<%#Eval("Description") %>' /> 
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
 <%--<div id="divDeletesuccessJobs" runat="server" class="modal backgroundoverlay" clientidmode="Static" style="display: none;">  
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="modal-dialog modal-dialog-centered">
   <ContentTemplate>
    <div id="div2" runat="server" class="w-100" clientidmode="Static">
     <div class="modal-content">
      <div>
       <b>
        <asp:Label ID="lbl3" runat="server"></asp:Label>
       </b>
      </div>
      <div class="modal-header">
       <h5 class="modal-title"> Delete confirmation
       </h5>
      </div>
      <div class="modal-body">
       <asp:Label ID="lbl4" runat="server" Text="Do you want to delete?"></asp:Label>
      </div>
      <div class="modal-footer border-top-0">
       <asp:Button ID="lnkDeleteJobsCancel" runat="server" ClientIDMode="Static" class="add-scroller btn btn-outline-primary m-r-15" Text="Cancel"
        OnClientClick="javascript:divJobsCancel();return false;"></asp:Button>
       <asp:Button ID="lnkDeleteJobsConfirm" runat="server" ClientIDMode="Static" CssClass="btn btn-primary add-scroller" Text="Yes"
        OnClientClick="javascript:divdeletesJobs();" OnClick="lnkDeleteJobsConfirm_Click"></asp:Button>
      </div>

     </div>
    </div>
   </ContentTemplate>
  </asp:UpdatePanel>
 </div>--%>
 <script type="text/javascript">
   function callClose() {
        debugger;
    $('#txtJobTitle').val('');
    $('#txtJobDescription').val('');
    $('#txtLocation').val('');
    $('#drpJobType').val($("#drpJobType option:first").val()).change();    
    $('#SalaryFrom').val('');
    $('#SalaryTo').val('');   
    $('#DurationFrom').val('');
    $('#DurationTo').val('');      
    $('#hdnintStatusUpdateId').val('');
    //$('#dvSourceLinkErrorText').text('');
    $('#jobModal').modal('hide');      
   }

   function messageClose() { 
      if ($('#txtJobTitle').val() != "" && $('#txtJobDescription').val() != "") { // && $('#drpJobType').val() != "-1"
          $('#jobModal').modal('hide');
      }
   }
   //function divdeletesJobs() {
   //   // $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 5px #00B7E5");         
   //   $('#divDeletesuccessJobs').css("display", "none");
   //}
   //function divJobsCancel() {
   //   document.getElementById('jobModal').style.display = 'none';
   //   $('#divDeletesuccessJobs').css("display", "none");
   //   //$('#rssModal').modal('toggle');
   //   //$('#divDeletesucessRss').hide();
   //}
   //function docdeleteJobs() {
   //   $('#divDeletesuccessJobs').css("display", "block");
   //}
   //function setJobsId() {
   //   $('#hdnintJobsDelete').val($(this).children('#hdnJobsId').val());
   //}
   function ShowEditPost(strPostId,Title,Description,Location,JobType,StartingSalary,EndingSalary,StartDuration,EndDuration) {
           //$('#dvSourceLinkErrorText').text('');
           $('#jobModal').modal('show');
     
           $('#hdnintStatusUpdateId').val(strPostId);     
           $('#txtJobTitle').val(Title);
           $('#txtJobDescription').val(Description);
           $('#txtLocation').val(Location);;
           $('#drpJobType').val(JobType).change();
           //$('#drpJobType option:selected').text(JobType);
           $('#SalaryFrom').val(StartingSalary);
           $('#SalaryTo').val(EndingSalary);
           $('#DurationFrom').val(StartDuration);
           $('#DurationTo').val(EndDuration);
           //$('#jobModal').modal('show');
     
           document.getElementById('jobModal').style.display = 'none';
           $('#jobModal').modal('toggle');
   }

   function CallPostNews() {
         //debugger;
         if ($('#txtJobTitle').val() != "" || $('#txtJobDescription').val() != "") {
             if ($('#txtJobTitle').val() == '' || $('#txtJobDescription').val() == "") {
                 $('#dvsourceError').text('Please enter both job Title and job description.');
                 //alert('Must enter both');
                 return false;
             }      
         }
         $('#dvsourceError').text('');
          //tinyMCE.triggerSave(true,true);

          if ($('#txtJobTitle').val() == '' || $('#txtJobDescription').val() == '') {
              $('#divSaveimage').css("display", "none");

              setTimeout(
                  function () {
                      $('#divSaveimage').css("display", "none");                          
                  }, 1000);

          } else {
              showLoader1();
          }             
     } 
 </script>

 <script type="text/javascript">
    $(document).ready(function () {

        var showChar = 500;
        var ellipsestext = "...";
        var moretext = "";
        var lesstext = "";
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
    
        //if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
        //    //$('#lnkNextshow').css("display", "block");
        //    $('#lnkNext').css("display", "none");
        //}
        if ($('#hdnCurrentPageJobs').val() == $('#hdnEndPageJobs').val()) {
            //$('#lnkNextshow').css("display", "block");
            $('#lnkNextJobs').css("display", "none");
        }

        //$('#searchBtn').on('click', function () {
        //  document.getElementById("btnSave").click();
        //})

        //createChosen();
        // $('#selectControl').on('change', function (evt, params) {
        //    $('#selectControl').trigger('chosen:close');
        //    $('.chosen-drop').css('display','none');
        //    document.getElementById("btnSave1").click();
   
        //  });
        $("#dvPageJobs a:enabled").on('click', function(event){showLoader1();});

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
         
            if ($('#hdnCurrentPageJobs').val() == $('#hdnEndPageJobs').val()) {
                //$('#lnkNextshow').css("display", "block");
                $('#lnkNextJobs').css("display", "none");
            }

            $('.moreQA').each(function () {
                var content = $(this).html();
                if (content.length > showChar) {
                    var c = content.substr(0, showChar);
                    var h = content.substr(showChar - 1, content.length - showChar);
                    var html = c + '<span class="moreelipses">' + ellipsestext + '</span>&nbsp;<span class="morecontent"><span span style="display:none;">' + h + '</span>&nbsp;&nbsp;<a href="" class="morelinkQA">' + moretext + '</a></span>';
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

            //$('#searchBtn').on('click', function () {
            //      document.getElementById("btnSave").click();

            //});

            //createChosen();
            // $('#selectControl').on('change', function (evt, params) {
            //      $('#selectControl').trigger('chosen:close');
            //      $('.chosen-drop').css('display','none');
            //      document.getElementById("btnSave1").click();

            //});

            $("#dvPageJobs a:enabled").on('click', function(event){showLoader1();});
    
        });
    });
 </script>
</asp:Content>

