<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
   CodeFile="post-new-question.aspx.cs" Inherits="post_new_question" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
     <script src="docsupport/chosen.jquery.js" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
    <script src="docsupport/chosen.jquery.js" type="text/javascript"></script>
    <script src="docsupport/prism.js" type="text/javascript"></script>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <title>Skorkel</title>

    <link rel="stylesheet" href="https://cdn.linearicons.com/free/1.0.0/icon-font.min.css">
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/bootstrap-reboot.css" rel="stylesheet">
    <link href="css/bootstrap-grid.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="css/font-icon.css" rel="stylesheet">
    <link href="css/font-icon.css" rel="stylesheet">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
  <div class="innerContainer">

         <%-- <span class="m-aside-trigger">
                 <span class="lnr lnr-arrow-left"></span>
                        <span class="avatar-text">Related Questions</span>
          </span>--%>
            <div class="main-section-inner">
                        <span class="m-aside-trigger mt-0 mb-0">
                            <span class="lnr lnr-arrow-left"></span>
                            <span class="avatar-text">Related Questions</span>
                        </span>
                       <div class="back-link-cover">
                        <a href="AllQuestionDetails.aspx" class="back-link"><span class="lnr lnr-arrow-left"></span> Back to Q & A</a>
                       </div>
                 <div class="panel-cover clearfix">
                                <div class="center-panel">
                                         <asp:UpdatePanel ID="upmain" runat="server">
                                                        <ContentTemplate>
                                  <div class="card">
                                                                <div class="card-body">
                                                                    <div class="form-group">
                                                                        <label for="exampleFormControlTextarea1">Write Your Question</label>
                                                                
                                                                        <div class="form-group">
                                                                        <textarea maxlength="470" rows="5" class="form-control" clientidmode="Static" id="CKDescription" onchange="ClearMsg();"  placeholder="Type your question here..." runat="server"></textarea>
                                                                        <asp:Label ID="lblSuMess" runat="server" Text=""></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CKDescription" Display="Dynamic" ValidationGroup="Blog" ErrorMessage="Please add question" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                       </div>
                                                               
                                                                    </div>
                                                                <div class="form-group remove-flex position-relative">
                                                                    <label>Classify Your Question </label>
                                                                  <%--  <select data-placeholder="Type to add a tag" class="chosen-select multipleSubjects form-control" id="txtSubjectList" onchange="getAllSubjectValue(this.id)" runat="server" clientidmode="Static" multiple tabindex="4">
                                                                    </select>--%>
                                                                    <uc:UserControl_MultiSelect class="form-control" ID="txtSubjectList" ClientIDMode="Static" runat="server"  />
                                                                    <asp:HiddenField ID="hdnsubject" ClientIDMode="Static" runat="server" />
                                                                 </div>
                                                              </div>
                                                              <div class="card-footer text-right">
                                                                <asp:LinkButton ID="LinkButton1" runat="server"  class="btn btn-outline-primary"  OnClick="btnClose_Click" OnClientClick="javascript:callClose();">Cancel</asp:LinkButton>
                                                    
                                                                <asp:LinkButton ID="lnkcreateForum" Text="Post" runat="server" ClientIDMode="Static"
                                                                 OnClientClick="javascript:CallPostques();" CssClass="menuPublish btn btn-primary"  ValidationGroup="Qstn" OnClick="btnSave_Click"></asp:LinkButton>

                                                              </div>

                                                            </div>
                                                        </ContentTemplate>
                                                
                                                
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnkcreateForum" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                </div>

                                    <div class="right-panel-back-layer"></div>
                                    <div class="right-panel"> <span class="m-view back">Back <span class="lnr lnr-arrow-right"></span></span>
                                            <div class="aside-bar">
                                                <!-- card ended-->                 
                                                <asp:UpdatePanel ID="up1" class="card releated-section related-question" runat="server">
                                                        <ContentTemplate>
                                                                    <div class="card-body">
                                                                    <h4>Related Questions</h4>
                                                                        <ul class="list-unstyled">
                                                                        <asp:ListView ID="lstRelQuestions" runat="server"  class="list-unstyled" OnItemCommand="lstRelQuestions_ItemCommand"
                                                                                        GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                                                                                        <LayoutTemplate>
                                                                                                <table width="90%">
                                                                                                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                                                                                </table>
                                                                                        </LayoutTemplate>
                                                                                        <GroupTemplate>
                                                                                                <tr>
                                                                                                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                                                                </tr>
                                                                                        </GroupTemplate>
                                                                                        <ItemTemplate>
                                                                                                    <li>
                                                                                                        <asp:HiddenField ID="hdnPostQuestionId" runat="server" Value='<%#Eval("intPostQuestionId")%>' />
                                                                                                        <p class="breakallwords commentquestionsrply">
                                                                                                        <asp:LinkButton  ID="lnkQueAnsDesc" Font-Underline="false"  runat="server" Text='<%#Eval ("strQuestionDescription") %>'
                                                                                                            CommandName="OpenQ"></asp:LinkButton>
                                                                                                        </p>
                                                                                                    </li>
                                                                                        </ItemTemplate>
                                                                        </asp:ListView>
                                                                            </ul>
                                                                        <div class="text-center show-more-seprator remove-body-fixed-class">
                                                                             <a class="readmore new" href="AllQuestionDetails.aspx">Show More</a>
                                                                        </div>     
                                                                    </div>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>          
                                            </div>
                                       </div>
                    </div>
        </div>

                                             
     </div>



    <div id="divSuccess" runat="server" style=" display: none;" class="modal backgroundoverlay"  clientidmode="Static">
        <div class=" modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                   <h5 class="modal-title">Success</h5>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblSuccess"  runat="server" Text="Question created successfully."
                    ></asp:Label>
                </div>     
                <div class="modal-footer border-top-0">
                    <a onclick="javascript:messageClose();" class="add-scroller a-tag-colcor btn btn-outline-primary">Close</a>
                </div>
            </div>
        </div>
    </div>




     <div style="display: none;">
                              <asp:Label ID="lblMessage" runat="server"></asp:Label>
                              <p class="post_attach">
                                 <b>Attach File :</b>
                                 <asp:FileUpload ID="uploadDoc" class="upload_doC" runat="server" />
                              </p>
     </div>



<script type="text/javascript">
    function ClearMsg() {
        $('#ctl00_ContentPlaceHolder1_lblSuMess').text('')
    }
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
         function callClose() {
             $('#txtSubjectList').val('').trigger('chosen:updated');
             
         }
      </script>
      <script type="text/javascript">
         function getAllMemberValue(ctrlId) {
             $('#divFreeSkorlTargetSearch').find('label.error').remove();
             var control = document.getElementById(ctrlId);
             var strSelTexts = '';
             var cnt = 0;
             for (var i = 0; i < control.length; i++) {
                 if (control.options[i].selected) {
                     if (cnt == 0) {
                         strSelTexts += control.options[i].value;
                     }
                     else {
                         strSelTexts += ',' + control.options[i].value;
                     }
                     cnt++;
                 }
             }
             $('#hdnMembId').val(strSelTexts);
         }
         function getAllSubjectValue(ctrlId) {
             $('#MicroTag').find('label.error').remove();
             var control = document.getElementById(ctrlId);
             var strSelTexts = '';
             var cnt = 0;
             for (var i = 0; i < control.length; i++) {
                 if (control.options[i].selected) {
                     if (cnt == 0) {
                         strSelTexts += control.options[i].value;
                     }
                     else {
                         strSelTexts += ',' + control.options[i].value;
                     }
                     cnt++;
                 }
             }
             $('#hdnsubject').val(strSelTexts);
         }
      </script>
      <script type="text/javascript">
         $(document).ready(function () {
             var ID = "#" + $("#hdnTabId").val();
             $(ID).focus();
         });
         function ShowLoading(id) {
             location.href = '#' + id;
         }
      </script>
      <script type="text/javascript">
         function messageClose() {
             $('#txtSubjectList').val('').trigger('chosen:updated');
             $('#divSuccess').modal('hide');
             //document.getElementById('divSuccess').style.display = 'none';
         }
         function getAllSubjectValue(ctrlId) {
             var control = document.getElementById(ctrlId);
             var strSelTexts = '';
             var cnt = 0;
             for (var i = 0; i < control.length; i++) {
                 if (control.options[i].selected) {
                     if (cnt == 0) {
                         strSelTexts += control.options[i].value;
                     }
                     else {
                         strSelTexts += ',' + control.options[i].value;
                     }
                     cnt++;
                 }
             }
             $('#hdnsubject').val(strSelTexts);
         }
      </script>
      <script type="text/javascript">
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
      </script>
      <script type="text/javascript">

         $(document).ready(function () {
           
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_endRequest(function () {
               
                 hideLoader1();
               
             });

         });
          function CallPostques() {
              
             // $('#divSaveimage').css("display", "block");
             // $('#lnkcreateForum').css("text-shadow", "0px 0px 1px #00B7E5");
             // $('#lnkcreateForum').css("color", "#00A5AA");
              
              if ($('#CKDescription').val() == '') {
                  $('#divSaveimage').css("display", "none");

                  setTimeout(
                      function () {
                          $('#divSaveimage').css("display", "none");
                          // $('#lnkcreateForum').css("color", "#0096a1");
                          // $('#lnkcreateForum').css("text-shadow", "0px 0px 0px #00B7E5");
                      }, 1000);

              } else {
                  showLoader1();
              }
             
         }

          function CallOnRequestSucess() {
                  $('#divSuccess').modal({ backdrop: "static" });
         }
         function CallPostimg() {
             $('#divSaveimage').css("display", "block");
         }
         function CallPostimgs() {
             $('#divSaveimage').css("display", "none");
         }
      </script>

</asp:Content>
