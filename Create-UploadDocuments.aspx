<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="Create-UploadDocuments.aspx.cs"
   Inherits="Create_UploadDocuments" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UserControl/Groups-m.ascx" TagName="GroupDetailsM" TagPrefix="GroupM" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register TagPrefix="usc" TagName="UserControl_DragNDrop" Src="~/UserControl/DragNDrop.ascx" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
   <script src="<%=ResolveUrl("js/jquery.filedrop.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

      <div class="main-section-inner">
         <div class="center-panel">
            <div class="custom-nav-con group-page-tab">
               <GroupM:GroupDetailsM ID="grpDetailsM" runat="server" />
            <!---Tabs panel Start-->
               <ul class="custom-nav-control nav nav-tabs">
                  <li class="nav-item">
                     <asp:LinkButton ID="lnkProfile" CssClass="nav-link" runat="server" Text="Profile" ClientIDMode="Static"
                        OnClick="lnkProfile_Click"></asp:LinkButton>
                  </li>
                  <li id="DivHome" runat="server"  class="nav-item">
                
                        <asp:LinkButton ID="lnkHome" class="nav-link" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                   
                  </li>
                  <li id="DivForumTab" runat="server" clientidmode="Static"  class="nav-item">
                   
                        <asp:LinkButton ID="lnkForumTab" class="nav-link" runat="server" Text="Forums" ClientIDMode="Static"
                           OnClick="lnkForumTab_Click"></asp:LinkButton>
                  
                  </li>
                  <li id="DivUploadTab" runat="server" clientidmode="Static" class="nav-item">
                    
                        <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                           class="forumstabAcitve nav-link" OnClick="lnkUploadTab_Click"></asp:LinkButton>
                    
                  </li>
                  <li id="DivPollTab" runat="server" clientidmode="Static" class="nav-item">
                    
                        <asp:LinkButton ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static"
                           OnClick="lnkPollTab_Click" class="nav-link"></asp:LinkButton>
                   
                  </li>
                  <li id="DivEventTab" runat="server" clientidmode="Static" class="nav-item">
                   
                        <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                           OnClick="lnkEventTab_Click" class="nav-link"></asp:LinkButton>
                   
                  </li>
                  <li id="DivMemberTab" runat="server" clientidmode="Static" class="nav-item">
                     
                        <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                           OnClick="lnkEventMemberTab_Click" class="nav-link"></asp:LinkButton>
                    
                  </li>
               </ul>
            <!---Tabs panel Ended-->
            <!---List Section Start-->
               <div class="btn-title-con">
                  <div class="back-link-cover mb-0 mt-0"> 
                      <asp:LinkButton ID="lnkAllUpload" runat="server" class="backButton back-link" OnClick="lnkAllUpload_Click" ClientIDMode="Static" Text="Back">
                      <span class="lnr lnr-arrow-left"></span>
                      <span>Back</span>
                     </asp:LinkButton>
                  </div>
               </div>  
               <div class="forumContainerCreate margin_top_z card" >
                  <div class="card-body">                
                   <div id="divDocsSuccess" runat="server" class="modal backgroundoverlay" clientidmode="Static" style="display:none;">
                      <div class="modal-dialog modal-dialog-centered">
                         <div class="modal-content"> 
                           <div class="modal-header">
                              <h5 class="modal-title">Success
                            </h5>
                           </div>
                           <div class="modal-body">
                            <asp:Label ID="lblSuccess" runat="server" Text="Document uploaded successfully."
                                    ></asp:Label>
                           </div>         
                           <div class="modal-footer border-top-0">
                              <a clientidmode="Static" href="javascript:void()" causesvalidation="false" 
                                 onclick="javascript:MesCloseDocs();" class="btn btn-outline-primary add-scroller" >Close </a>
                           </div>
                         </div>
                      </div>
                   </div>
                  
                    
                      <div class="u_div">
                         <asp:Label ID="lnkFirst" runat="server" ForeColor="#A7A8AA"></asp:Label>
                         <asp:Label ID="lblFirst" runat="server" Visible="false" Text="&nbsp;>"></asp:Label>
                         <asp:Label ID="lnkSecond" runat="server" ForeColor="#A7A8AA"></asp:Label>
                         <asp:Label ID="lblSecond" runat="server" Visible="false" Text="&nbsp;>>"></asp:Label>
                         <asp:Label ID="lnkThird" runat="server" ForeColor="#A7A8AA"></asp:Label>
                         <asp:Label ID="lblThird" runat="server" Visible="false" Text="&nbsp;>>"></asp:Label>
                      </div>
                     
                      <div class="form-group ">
                         <label for="Name of Document">
                         <asp:Label ID="lblm" runat="server" ClientIDMode="Static">Upload Document</asp:Label></label>
                         <div class="">

                             <usc:UserControl_DragNDrop ID="ddUploader1" ClientIDMode="Static" runat="server" />

                         </div>
                          <div id="uploadPopopError" runat="server" class="errorMsg error-msz" clientidmode="static" style="display:none;">Test </div>
                           <div  class="errorMsg " id="Div1">
                      <asp:Label ID="lblMessage" runat="server"></asp:Label>
                   </div>
                       </div>
                       <div class="form-group">
                           <label for="textbox">Name of Document</label>
                           <asp:TextBox ID="txtDocTitle" autocomplete="off" runat="server" MaxLength="50" ClientIDMode="Static" CssClass="form-control"
                               ValidationGroup="docUpload" placeholder="Enter Document Name"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                         CssClass="uploadDescription border_none"  ControlToValidate="txtDocTitle"
                         Display="Dynamic" ValidationGroup="t" ErrorMessage="Please select Title"
                         ForeColor="Red"></asp:RequiredFieldValidator>
                       </div>
                  
           
                       <div class="form-group">
                           <label for="textarea">Description</label>
                           <textarea id="txtAuthors" rows="5" runat="server" maxlength="500" placeholder="Description" class="form-control"></textarea>
                       </div>
                  <div class="form-group radio-itmes">
                     <label for="classify" class="mb-2">Classify Document</label>
                     <br />
                      <asp:CheckBoxList ID="ddlDocumentType" runat="server" data-toggle="radio" RepeatDirection="Horizontal"
                         Style="display: inline; display: none;">
                      </asp:CheckBoxList>
                      <div class="form-check form-check-inline">
                        <div class="custom-checkbox">
                          <asp:CheckBox ID="ChkCases" class="align-center-f" Text="Cases" GroupName="Who can Vote" ClientIDMode="Static"
                         runat="server" />
                       </div>
                      </div>  
                      <div class="form-check form-check-inline">
                        <div class="custom-checkbox"> 
                          <asp:CheckBox ID="ChkArticles" class="align-center-f" Text="Articles" GroupName="Who can Vote" ClientIDMode="Static"
                         runat="server" />
                         </div>
                      </div>
                      <div class="form-check form-check-inline">
                        <div class="custom-checkbox">    
                          <asp:CheckBox ID="ChkNotes" class="align-center-f" Text="Notes" GroupName="Who can Vote" ClientIDMode="Static"
                         runat="server" />
                        </div>
                      </div>   
                      <div class="form-check form-check-inline">
                        <div class="custom-checkbox">
                          <asp:CheckBox ID="ChkOthers" class="align-center-f" Text="Others" GroupName="Who can Vote" ClientIDMode="Static"
                         runat="server" />
                        </div>
                      </div>   
                   
                      </div>
                   <!--category box starts-->
                       <div class="form-group remove-flex">
                           <label for="textbox">Context</label>
                           <uc:UserControl_MultiSelect class="form-control" ID="lstSubjCategory" ClientIDMode="Static" runat="server" />
                       </div>
                  <div class="form-group radio-itmes-two">     
                     <label for="Mark as">Mark as</label><br />
                      <div class="form-check form-check-inline">
                        <div class="custom-checkbox mark-as-input">
                           <asp:CheckBoxList ID="ddlintDocsSee" runat="server" RepeatDirection="Horizontal"
                           >
                           <asp:ListItem Text="Private Document" Value="Private"></asp:ListItem>
                          </asp:CheckBoxList>
                        </div>  
                     </div>
                  </div> 
                   
                   <div class="float-right">
                      <asp:LinkButton ID="btnCancelExperience" CommandName="Join" CausesValidation="false" class="btn btn-outline-primary m-r-15" runat="server" Text="Cancel" OnClick="btnCancel_Click"></asp:LinkButton>
                      <asp:LinkButton ID="LinkButton1" CssClass="btn  btn-primary" runat="server" Text="Upload" ClientIDMode="Static"
                         ValidationGroup="t" OnClick="btnSave_Click" OnClientClick="CallUploaddoc(); " ></asp:LinkButton>
                   </div>
                  <!--  <div class="error_message">
                      <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                         ValidationGroup="t" ForeColor="Red"  />
                   </div> -->
                  </div> 
               </div>
            <!---List Section Ended-->   
            </div>
         </div>
         <!--Center Panel Ended-->
         <!---Right Panel Start-->
 
            <Group:GroupDetails ID="grpDetails" runat="server" />
      
      </div>
      <!--left verticle search list ends-->

   <script type="text/javascript">
          
      $(document).ready(function () {
         // $('#lnkShare').css("margin-top", "30px");
             $(document).on('click', '.mobile_tab_icon', function() {
             $('ul.group_p').slideToggle('slow');
            });

      });
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          $(".categoryTxt ul li").click(function () {
              $(this).toggleClass("gray");
          });
          $(".viewAll a").click(function () {
              $(this).parent().parent().toggleClass("categoryBoxToggle");
              var abc = this.text;
              if (abc == "Close") {
                  $(this).text("View all");
              }
              if (abc == "View all") {
                  $(this).text("Close");
              }
          });
      });
   </script>
   <script type="text/javascript">
      function MutExChkList(chk) {
          var chkList = chk.parentNode.parentNode.parentNode;
          var chks = chkList.getElementsByTagName("input");
          for (var i = 0; i < chks.length; i++) {
              if (chks[i] != chk && chk.checked) {
                  chks[i].checked = false;
              }
          }
      }
   </script>
   <script type="text/javascript">
       $(document).ready(function () {
           createChosen();
          if ($("#lblNotifyCount").text() == '0') {
              document.getElementById("divNotification1").style.display = "none";
          } 
          if ($("#lblInboxCount").text() == '0') {
              document.getElementById("divInbox").style.display = "none";
          } 
          $("#uploadTrigger").click(function () {
              $("#uploadDoc").click();
          });
      });
      $(document).ready(function () {
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
      createChosen();
              if ($("#lblNotifyCount").text() == '0') {
                  document.getElementById("divNotification1").style.display = "none";
              } 
              if ($("#lblInboxCount").text() == '0') {
                  document.getElementById("divInbox").style.display = "none";
              } 
              $("#uploadTrigger").click(function () {
                  $("#uploadDoc").click();
              });
          });
      });
   </script>
   <script type="text/javascript">
      //$(document).ready(function () {
      //    var box;
      //    box = document.getElementById("dvDest");
      //    box.addEventListener("dragenter", OnDragEnter, false);
      //    box.addEventListener("dragover", OnDragOver, false);
      //    box.addEventListener("drop", OnDrop, false);
      //});
      //function OnDragEnter(e) {
      //    e.stopPropagation();
      //    e.preventDefault();
      //}
      //function OnDragOver(e) {
      //    e.stopPropagation();
      //    e.preventDefault();
      //}
      //function OnDrop(e) {
      //    e.stopPropagation();
      //    e.preventDefault();
      //    selectedFiles = e.dataTransfer.files;
      //    $('#lblfilenamee').show();
      //    $('#lblfilenamee').text(selectedFiles[0].name);
      //    $('#hdnOrgFileName').val(selectedFiles[0].name);
      //    $('#ctl00_ContentPlaceHolder1_upload').hide();
      //    var data = new FormData();
      //    for (var i = 0; i < selectedFiles.length; i++) {
      //        data.append(selectedFiles[i].name, selectedFiles[i]);
      //    }
      //    $.ajax({
      //        type: "POST",
      //        url: "handler/UploadDocument.ashx",
      //        contentType: false,
      //        processData: false,
      //        data: data,
      //        success: function (result) {
      //            $('#hdnUploadFile').val(result);
      //            $('#lnkDelete').show();
      //        },
      //        error: function () {
      //            alert("There was error uploading files!");
      //        }
      //    });
      //}
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          $('#ChkCases').click(function () {
              $('#ChkArticles').prop('checked', false);
              $('#ChkNotes').prop('checked', false);
              $('#ChkOthers').prop('checked', false);
          });
          $('#ChkArticles').click(function () {
              $('#ChkCases').prop('checked', false);
              $('#ChkNotes').prop('checked', false);
              $('#ChkOthers').prop('checked', false);
          });
          $('#ChkNotes').click(function () {
              $('#ChkCases').prop('checked', false);
              $('#ChkArticles').prop('checked', false);
              $('#ChkOthers').prop('checked', false);
          });
          $('#ChkOthers').click(function () {
              $('#ChkCases').prop('checked', false);
              $('#ChkArticles').prop('checked', false);
              $('#ChkNotes').prop('checked', false);
          });
      });
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              var config = {
                  '.chosen-select': {},
                  '.chosen-select-deselect': { allow_single_deselect: true },
                  '.chosen-select-no-single': { disable_search_threshold: 10 },
                  '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                  '.chosen-select-width': { width: "95%" }
              }
              for (var selector in config) {
                  $(selector).chosen(config[selector]);
              }
          });
      });
   </script>
   <script type="text/javascript">
      function MesCloseDocs() {
          document.getElementById("divDocsSuccess").style.display = 'none';
      }
      function CallUploaddoc() {
          $('#LinkButton1').addClass('hide-body-scroll');
          if ($('#txtDocTitle').val() == '') {

              setTimeout(
                  function () {
                      $('#LinkButton1').removeClass('hide-body-scroll');
                         $('body').removeClass('remove-scroller');
                  }, 1000);
          } else {
              showLoader1();

          }
      }
           function OverlayBody() {
            $('#bodyelement').addClass("remove-scroller");
        }
      $(document).ready(function () {
          $('#txtDocTitle').keypress(function (e) {
              if (e.keyCode == 13) {
                  return false;
              }
          });
      });
   </script>
</asp:Content>
