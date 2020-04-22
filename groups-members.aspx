<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="groups-members.aspx.cs"
   Inherits="groups_members" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Src="~/UserControl/Share.ascx" TagName="ShareDetails" TagPrefix="Share" %>
<%@ Register Src="~/UserControl/Groups-m.ascx" TagName="GroupDetailsM" TagPrefix="GroupM" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
   <script src="<%=ResolveUrl("js/jquery.jcarousel.min.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:UpdatePanel ID="upmain" runat="server">
      <ContentTemplate>
         <div class="main-section-inner">
            <div class="panel-cover clearfix" >
               <!---Center panel Start-->
               <div class="center-panel">
                  <div class="custom-nav-con group-page-tab">
                    <GroupM:GroupDetailsM ID="grpDetailsM" runat="server" />
                     <ul class="custom-nav-control nav nav-tabs ">
                        <li class="nav-item">
                         <asp:LinkButton ID="lnkProfile" class="nav-link" runat="server" Text="Profile" ClientIDMode="Static"
                          OnClick="lnkProfile_Click"></asp:LinkButton>
                        </li>
                        <li id="DivHome" runat="server"  class="nav-item">
                         <asp:LinkButton ID="lnkHome" runat="server" Text="Wall" class="nav-link" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>                         
                        </li>
                        <li class="nav-item" id="DivForumTab" runat="server" clientidmode="Static" >
                         <asp:LinkButton ID="lnkForumTab" runat="server" class="nav-link" Text="Forums" ClientIDMode="Static"
                          OnClick ="lnkForumTab_Click"></asp:LinkButton>                          
                        </li>
                        <li class="nav-item" id="DivUploadTab" runat="server" clientidmode="Static" >
                         <asp:LinkButton ID="lnkUploadTab" runat="server" class="nav-link" Text="Uploads" ClientIDMode="Static"
                          OnClick="lnkUploadTab_Click"></asp:LinkButton>                         
                        </li>
                        <li class="nav-item" id="DivPollTab" runat="server" clientidmode="Static" >
                         <asp:LinkButton ID="lnkPollTab" runat="server" class="nav-link" Text="Polls" ClientIDMode="Static"
                          OnClick="lnkPollTab_Click"></asp:LinkButton>                          
                        </li>
                        <li class="nav-item" id="DivEventTab" runat="server" clientidmode="Static" >
                         <asp:LinkButton ID="lnkEventTab" class="nav-link" runat="server" Text="Events" ClientIDMode="Static"
                          OnClick="lnkEventTab_Click"></asp:LinkButton>                          
                        </li>
                        <li class="nav-item" id="DivMemberTab" runat="server" clientidmode="Static">
                         <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                          class="forumstabAcitve nav-link" OnClick="lnkEventMemberTab_Click"></asp:LinkButton>                          
                        </li>
                     </ul>
                  
                     <div class="tab-content m-t-15">
                        <div class="btn-title-con">
                         <div>
                          <h5 class="card-title float-left">Members</h5>
                         </div>
                        </div>
                        <div class="card-list-con blog-list">
                         <div class="">
                          <div class="post-con">
                           <div class="post-body p-b-15 mt-0 row">
                            <div class="serach-box col-8 col-sm-12 col-md-9 position-relative">
                             <asp:TextBox ID="txtsearch" runat="server" ClientIDMode="Static" AutoPostBack="true" placeholder="Search Members"
                              class="searchMembers form-control border-0 padding-right-25" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                             <input type="button" id="searchBtn" class="search-btn-main" value="">
                             
                             <%--<ajax:TextBoxWatermarkExtender TargetControlID="txtsearch" ID="txtwatermarkz" runat="server"
                              WatermarkText="Search Members">
                             </ajax:TextBoxWatermarkExtender>--%>
                            </div>
                            <div class="Member col-sm-12 col-4 col-md-3 members-count ">
                             <b class="" id="numberMembers">
                              <asp:Label ID="lblTotalMember" runat="server"></asp:Label>
                             </b>
                            </div>
                           </div>
                          </div>
                         </div>
                        </div>
                        <!---Listing of members-->
                        <div>
                           <asp:ListView ID="lstPostUpdates" runat="server" OnItemCommand="lstPostUpdates_ItemCommand"
                              OnItemDataBound="lstPostUpdates_ItemDataBound">
                              <LayoutTemplate>
                               <div class="row create-group-list">
                                <div id="itemPlaceHolder" runat="server">
                                </div>
                               </div>
                              </LayoutTemplate>
                              <ItemTemplate>
                                 <div id="tbl" runat="server" class="col-sm-4 position-relative">
                                  <asp:UpdatePanel ID="upeditdel" runat="server">
                                   <ContentTemplate>
         
                                    <div class="doc-card text-center">   
                                       <div class="searchListNew list_style w-100 connect-buttons">
                                        <!---Connected button-->
                                         <asp:LinkButton ID="lnkConnected" runat="server" Visible="false" title="Connected" CausesValidation="false"
                                            Text="Connected" CssClass="cssConn connected-user un-anchor" OnClientClick="return false;">
                                           <span class="add-icon"><img src="images/connected.svg" style="height:15px;"></span>
                                           <span class="text-with-icon"></span>
                                           </asp:LinkButton>
                                        <!---Remove Button-->
                                        <%--<span class="more-btn float-right">
                                           <span class="dropdown show">
                                             <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                              <img src="images/more.svg" alt="" class="more-btn">
                                             </a>
                                             <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start">
                                              <a class="dropdown-item" href="#"><span class="lnr lnr-pencil"></span> Edit</a>
                                              <a class="dropdown-item" href="#"><span class="lnr lnr-trash"></span> Delete</a>
                                             </span>
                                           </span>  
                                          </span>--%>   
                                        
                                        
                                            <span class="img-cover ">
                                             <%--<img id="imgprofile" class="user-img" runat="server"
                                              alt="" src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' />--%>
                                             <asp:ImageButton id="imgprofile" runat="server" class="user-img cursor-pointer" 
                                              ImageUrl='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' CommandName="Details" />
                                             <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />                                         
                                            </span>

                                            <h4 class="truncate">      
                                             <asp:LinkButton ID="lblPostlink" Text='<%# Eval("Name") %>' CommandName="Details" runat="server">
                                             </asp:LinkButton>
                                            </h4>                            
                                                                                                       
                                            <span class="spcssMsg">
                                             <asp:LinkButton ID="btnRecmnd" Visible="false" CommandName="Recommendation" 
                                              CausesValidation="false" runat="server" Text="Message" CssClass="btn hide-body-scroll btn-outline-primary mr-1"></asp:LinkButton>
                                            </span>
                                            <span class="spcssAcc">
                                             <asp:LinkButton ID="lnkAccept" Visible="false" CommandName="Accept"
                                              CausesValidation="false" runat="server" Text="Accept" CssClass="btn btn-outline-primary mr-1 hide-body-scroll "></asp:LinkButton>
                                            </span>
                                            <span class="spcssRej">
                                             <asp:LinkButton ID="lnkRejected" Visible="false" CommandName="Reject" Style="margin-right: -3px;"
                                              CausesValidation="false" runat="server" Text="Reject" CssClass="btn btn-outline-primary hide-body-scroll"></asp:LinkButton>
                                            </span>                                         
                                                 </div>
                                    </div> 
                                           </ContentTemplate>
                                           <Triggers>
                                              <asp:AsyncPostBackTrigger ControlID="lnkAccept" />
                                              <asp:AsyncPostBackTrigger ControlID="lnkRejected" />
                                              <asp:AsyncPostBackTrigger ControlID="btnRecmnd" />
                                             </Triggers>
                                          </asp:UpdatePanel>

                                       </div>      

                                        
                                    
                                    <div class="table-div" style="display: none;">
                                       <table width="100%">
                                          <tr>
                                           <td >
                                           </td>
                                          </tr>
                                          <tr>
                                           <td>
                                            <p class="hdnID">
                                               <asp:Label ID="lblName" Text='<%# Eval("vchrInstituteName") %>' runat="server"></asp:Label>
                                               <asp:HiddenField ID="hdnId" Value='<%# Eval("Id") %>' runat="server" />
                                               <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' runat="server" />
                                            </p>
                                           </td>
                                          </tr>
                                          <tr>
                                             <td>
                                                <p class="label">
                                                   <asp:Label ID="lblCityState" Text='<%# Eval("CityState") %>' runat="server"></asp:Label>
                                                </p>
                                             </td>
                                          </tr>
                                          <tr id="trRecommend" visible="false" runat="server">
                                             <td align="right" valign="top">
                                                <fieldset>
                                                 <table class="recommend">
                                                  <tbody>
                                                   <tr>
                                                      <td id="Footers" align="right" colspan="3">
                                                         <asp:LinkButton ClientIDMode="Static" CommandName="Close" CssClass="linkClass" ID="lnkClose"
                                                            runat="server" Text="X"></asp:LinkButton>
                                                      </td>
                                                   </tr>
                                                   <tr>
                                                    <td class="text_recommend">
                                                     Recommend
                                                    </td>
                                                    <td class="dot_added">
                                                     :
                                                    </td>
                                                    <td class="textRedDetails">
                                                     <textarea id="txtRecDetails" class="EmpTextAreaCss" clientidmode="Static" runat="server"
                                                      cols="28" rows="3"></textarea>
                                                    </td>
                                                   </tr>
                                                   <tr>
                                                    <td colspan="3">
                                                     <asp:RequiredFieldValidator ID="req" runat="server" ControlToValidate="txtRecDetails"
                                                      Display="Dynamic" ErrorMessage="Please enter text" ValidationGroup="btn"></asp:RequiredFieldValidator>
                                                    </td>
                                                   </tr>
                                                   <tr>
                                                    <td id="Footer" colspan="3" align="right">
                                                     <asp:LinkButton ID="btnSubmitRecmnd" CommandName="SubmitRecom" CausesValidation="true"
                                                      ValidationGroup="btn" runat="server" Text="Post"></asp:LinkButton>
                                                     &nbsp;
                                                     <asp:LinkButton ID="btnCancelRecmnd" CommandName="CancelRecom" CausesValidation="false"
                                                      runat="server" Text="Cancel"></asp:LinkButton>
                                                     <label class="TextCss textcss" id="lblErrorRemark">
                                                     Please Enter text.
                                                     </label>
                                                    </td>
                                                   </tr>
                                                  </tbody>
                                                 </table>
                                                </fieldset>
                                             </td>
                                          </tr>
                                       </table>
                                    </div>
                                  
                                    <div style="display: none" class="jury">
                                       <asp:Label ID="lblEmailId" Text='<%# Eval("vchrUserName") %>' runat="server"></asp:Label>
                                    </div>
                                
                                
                              </ItemTemplate>
                           </asp:ListView>
                       
                         
                           <div id="pLoadMore" runat="server" align="center" clientidmode="static">
                            <div id="imgLoadMore" class="lds-ellipsis"><%--style="display:none;"--%>
                             <div></div>
                             <div></div>
                             <div></div>
                             <div></div>
                            </div>
                            <asp:ImageButton ID="imgLoadMore2" runat="server" ClientIDMode="Static" OnClick="imgLoadMore_OnClick" style="display:none;" />
                           </div><%--ImageUrl="~/images/loadingIcon.gif"--%>
                           <p align="center" style="display:none">
                            <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server"
                             ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                           </p>
                           <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                           <div style="display: none" class="innerContainer">
                            <div class="cls">
                            </div>
                            <div id="dvPage" runat="server" class="pagination" clientidmode="Static">
                             <asp:LinkButton ID="lnkFirst" runat="server" CssClass="PagingFirst" OnClick="lnkFirst_Click"> </asp:LinkButton>
                             <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">&lt;&lt;</asp:LinkButton>
                             <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand" OnItemDataBound="rptDvPage_ItemDataBound">                                  
                              <ItemTemplate>
                               <asp:LinkButton ID="lnkPageLink" CssClass="Paging" runat="server" ClientIDMode="Static"
                                CommandName="PageLink" Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                              </ItemTemplate>
                             </asp:Repeater>
                             <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">&gt;&gt;</asp:LinkButton>
                             <asp:LinkButton ID="lnkLast" runat="server" Style="background: none" OnClick="lnkLast_Click"><img src="images/spacer.gif" class="last" alt="" /></asp:LinkButton>
                             <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                             <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                             <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                             <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                             <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                            </div>
                           </div>
                           <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
                           <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
                           <asp:HiddenField ID="hdnOwnerID" ClientIDMode="Static" runat="server" />
                        </div>
                     </div>   

                     <!---Members List Ended-->
                     <!---Popup start-->
                     <asp:Label ID="lblMessage" runat="server"></asp:Label>
                     <!---Message Popup-->   
                     <div id="dvPopup" class="modal backgroundoverlay" runat="server" style="display: none;"
                        clientidmode="Static">
                        <div class="modal-dialog modal-dialog-centered">
                           <div class="modal-content">
                              <div class="modal-header">
                               <h5 class="modal-title">Message</h5>
                              </div>
                              <div class="modal-body">
                              <div class="form-group">
                               <label for="Message">Subject</label>
                               <asp:TextBox ID="txtSubject" MaxLength="100" CssClass="MessageSubjectGroupMem form-control" placeholder="Subject"
                                runat="server"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject" Display="Dynamic" 
                                ValidationGroup="Mess" ErrorMessage="Please enter subject." ForeColor="Red"></asp:RequiredFieldValidator>
                              </div>
                              <div class="form-group">
                               <label for="Message">Message</label>
                               <textarea id="txtBody" runat="server" MaxLength="500" placeholder="Message" rows="0" cols="0" class="MessageBodyGroupMem form-control" />
                                <div class="message_enter">
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBody" Display="Dynamic"
                                  ValidationGroup="Mess" ErrorMessage="Please enter message" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                              </div>

                              </div>
                              <div class="modal-footer border-top-0 padding-top-0">                                 
                               <asp:LinkButton ID="btnCancel" CommandName="Join" CausesValidation="false" 
                                runat="server" Text="Cancel" class="btn add-scroller btn-outline-primary m-r-15" OnClick="btnCancel_Click"></asp:LinkButton>
                               <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Send" CssClass="btn btn-primary" 
                                ValidationGroup="Mess" OnClientClick="javascript:showLoader1(); OpenMessageGrpMember();" OnClick="lnkPopupOK_Click"></asp:LinkButton>
                              </div>
                           </div>
                        </div>
                     </div>
                     <!---Sucess Popup-->
                     <div id="divSuccessMess" class="modal backgroundoverlay" runat="server" style=" display: none;" clientidmode="Static">
                      <div class="modal-dialog modal-dialog-centered">
                         <div class="modal-header">
                           <h5 class="modal-title">Delete Confirmation
                          </h5>
                         </div>
                         <div class="modal-body">
                            <asp:Label ID="lblSuccess" runat="server" Text="" ></asp:Label>
                         </div>   
                         <div class="modal-footer border-top-0">
                            <a clientidmode="Static" class="add-scroller btn btn-outline-primary"  onclick="javascript:Cancel();">
                            Close </a>
                         </div>
                      </div>
                     </div>

                     <!---Reject Popup-->
                     <div id="divPopupMember" class="modal backgroundoverlay" clientidmode="Static" runat="server" style="
                      display:none;">
                      <div class="modal-dialog modal-dialog-centered">
                       <div class="modal-content">
                        <div class="modal-header">
                         <h5 class="modal-title"> <asp:Label ID="lblTitle" runat="server"></asp:Label></h5>                               
                        </div>
                        <div class="modal-body">
                         <b>
                          <asp:Label ID="lblTitleGroup" runat="server"></asp:Label>
                         </b>
                         <div id="tdddlRoles" class="div_popup_member" runat="server" style="display: none;">
                          <asp:DropDownList ID="ddlRoleDetails" CssClass="txtFieldNew" ClientIDMode="Static" runat="server">
                          </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="reqRole" runat="server" InitialValue="Select Role"
                           ControlToValidate="ddlRoleDetails" ErrorMessage="Select role from the list. "
                           ValidationGroup="Insert" Text="Select role from the list." ></asp:RequiredFieldValidator>
                         </div>
                        
                         <div class="reject_r">
                          <asp:Label ID="lblMessageacc" runat="server" ></asp:Label>
                          <textarea id="txtBodyacc" runat="server" cols="20" rows="2" value="Message" onblur="if(this.value=='') this.value='Message';"
                           onfocus="if(this.value=='Message') this.value='';" class="forumTitle" />
                         </div>
                         
                        </div>
                        <div class="modal-footer border-top-0">
                         <asp:LinkButton ID="lnkcose" runat="server" CommandName="Join" CausesValidation="false"
                          ClientIDMode="Static" class="add-scroller btn btn-outline-primary m-r-15" OnClick="lnkcose_Click">Close </asp:LinkButton>
                         <asp:LinkButton ID="LinkButton1" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="javascript:CallYes();showLoader1();"
                          CssClass="btn btn-primary hide-body-scroll" ValidationGroup="Insert" OnClick="lnkPopupacc_Click"></asp:LinkButton>
                        </div>
                       </div>
                      </div>
                     </div>
                     <!---Sucess Popup-->
                     <div id="divSuccessAcceptMember" class="modal backgroundoverlay"  runat="server" style=" display: none;" clientidmode="Static">
                      <div class="modal-dialog modal-dialog-centered">
                       <div class="modal-content">
                        <div class="modal-header">
                         <h5 class="modal-title"> Success</h5>   
                        </div>
                        <div class="modal-body">
                         <asp:Label ID="lblSuccessacc" runat="server" ></asp:Label>
                        </div>   
                        <div class="modal-footer border-top-0">
                         <asp:LinkButton ID="lnkSuccessFailure" class="btn btn-outline-primary add-scroller" runat="server" Text="Ok" ClientIDMode="Static"
                          CausesValidation="false"  OnClick="lnkSuccessFailure_Click"></asp:LinkButton>
                        </div>
                       </div>
                      </div>
                      <script type="text/javascript">
                       function JobMesClose() {
                           document.getElementById("divPopupMember").style.display = 'none';
                       }
                      </script>
                     </div>
                     <!---Popup ended-->
                 
                  </div>
               </div>
               
               <!---left box ends-->
               <!---Right Panel Start-->
           
                  <asp:UpdatePanel ID="updatepl" runat="server">
                     <ContentTemplate>
                        <Group:GroupDetails ID="grpDetails" runat="server" />
                     </ContentTemplate>
                  </asp:UpdatePanel>
              
            </div>
            <!--left verticle search list ends-->
   
         <Share:ShareDetails ID="ShareDetails" runat="server" />
         <script type="text/javascript">
             function hideScroll() {
                 $('body').removeClass('remove-scroller');
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
                    hideLoader1();
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
         <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
         <script type="text/javascript">
            $(document).ready(function () {
                var ID = "#" + $("#hdnLoader").val();
                $(ID).focus();
                   $(document).on('click', '.mobile_tab_icon', function() {
             $('ul.group_p').slideToggle('slow');
            });
         
            });
         </script>
      </ContentTemplate>
      <Triggers>
       <asp:AsyncPostBackTrigger ControlID="lnkPopupOK" EventName="Click" />
       <asp:AsyncPostBackTrigger ControlID="lnkcose" EventName="Click" />
       <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
       <asp:AsyncPostBackTrigger ControlID="lnkSuccessFailure" EventName="Click" />
      </Triggers>
   </asp:UpdatePanel>
   <script type="text/javascript" lang="javascript">
      $(document).ready(function () {
          $("#lblNoMoreRslt").css("display", "none");
      });
   </script>
   <script type="text/javascript">
       function showLoadMoreOnScroll() {
           document.getElementById('pLoadMore').style.display = 'none';
           $InProgress = false;
          $(document).scroll(function () {
              if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                  var v = $("#lblNoMoreRslt").text();
                  //setTimeout(1000);
                  var maxCount = $("#hdnMaxcount").val();
                  var totalItems = $("#hdnTotalItem").val();
                  if (maxCount > totalItems && !$InProgress) {
                      $InProgress = true;
                      document.getElementById('pLoadMore').style.display = 'block';
                      document.getElementById('<%= imgLoadMore2.ClientID %>').click();
                  } else {
                       //document.getElementById('pLoadMore').style.display = 'none';
                  }
              }
           });
       }
   </script>
   <script type="text/javascript">
      function OpenMessageGrpMember() {
          $('#lnkPopupOK').addClass('disabled');
          if ($('#txtSubject').text().trim() == '' || $('#txtBody').text().trim() == '') {
              setTimeout(
                  function () {
                      $('#lnkPopupOK').removeClass('disabled');
                  }, 1000);
            }
      }
      function Cancel() {
          document.getElementById("divSuccessMess").style.display = 'none';
          return false;
      }
      function CommonMessage() {
          document.getElementById("dvPopup").style.display = 'none';
            $('body').removeClass('remove-scroller');
          return false;
      }
   </script>
   <script type="text/javascript">
       $(document).ready(function () {
           showLoadMoreOnScroll();
          $("span.spcssMsg").click(function () {
              $(this).children('.cssMsg').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $("span.spcssAcc").click(function () {
              $(this).children('.cssAcc').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $("span.spcssRej").click(function () {
              $(this).children('.cssRej').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $("span.spcssConn").click(function () {
              $(this).children('.cssConn').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $('#ctl00_ContentPlaceHolder1_txtSubject').keypress(function (e) {
              if (e.keyCode == 13) {
                  return false;
              }
          });
          var prm = Sys.WebForms.PageRequestManager.getInstance();
           prm.add_endRequest(function () {
               showLoadMoreOnScroll();
              $("span.spcssMsg").click(function () {
                  $(this).children('.cssMsg').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spcssAcc").click(function () {
                  $(this).children('.cssAcc').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spcssRej").click(function () {
                  $(this).children('.cssRej').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spcssConn").click(function () {
                  $(this).children('.cssConn').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $('#ctl00_ContentPlaceHolder1_txtSubject').keypress(function (e) {
                  if (e.keyCode == 13) {
                      return false;
                  }
              });
          });
      });
      function CallYes() {
          $('#LinkButton1').css("box-shadow", "0px 0px 5px #00B7E5");
      }
   </script>
</asp:Content>
