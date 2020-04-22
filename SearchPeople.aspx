<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
   CodeFile="SearchPeople.aspx.cs" Inherits="SearchPeople" %>
<%@ Register Src="~/UserControl/AcceptMember.ascx" TagName="AcceptMember" TagPrefix="Accept" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:UpdatePanel ID="upmain" runat="server">
      <ContentTemplate>
         <asp:HiddenField ID="hdnTempUserId" runat="server" Value="" ClientIDMode="Static" />
         <!--heading starts-->
         <!--heading ends-->

         <div class="main-section-inner">
            <div class="leftFilterBox" style="display: none;">
               <div>
                  <p class="heading">
                     Filter further:
                  </p>
                  <p class="groups">
                     People:
                  </p>
                  <br />
                  <div>
                     <asp:CheckBox CssClass="checkboxFive" ID="ChkStudents" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Students"
                        runat="server" AutoPostBack="true" OnCheckedChanged="Students_CheckedChange" />
                     <label for="ChkStudents" class="float_left">
                     </label>
                  </div>
                  <br />
                  <div>
                     <asp:CheckBox CssClass="checkboxFive" ID="ChkProfessors" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Professors"
                        runat="server" AutoPostBack="true" OnCheckedChanged="Lawyers_CheckedChange" />
                     <label for="ChkProfessors">
                     </label>
                  </div>
                  <br />
                  <div align="left">
                     <asp:CheckBox CssClass="checkboxFive" ID="ChkLawyers" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Lawyers"
                        runat="server" AutoPostBack="true" OnCheckedChanged="Lawyers_CheckedChange" />
                     <label for="ChkLawyers">
                     </label>
                  </div>
                  <br />
                  <div align="left">
                     <asp:CheckBox CssClass="checkboxFive" ID="ChkJudges" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Judges"
                        runat="server" AutoPostBack="true" OnCheckedChanged="Lawyers_CheckedChange" />
                     <label for="ChkJudges">
                     </label>
                  </div>
                  <br />
                  <div align="left">
                     <asp:CheckBox CssClass="checkboxFive" ID="ChkOthers" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Others"
                        runat="server" AutoPostBack="true" OnCheckedChanged="Others_CheckedChange" />
                     <label for="ChkOthers">
                     </label>
                  </div>
                  <br />
                  <div class="bgLine">
                  </div>
                  <asp:ImageButton ID="imgReset" runat="server" OnClick="imgReset_Click" ImageUrl="images/spacer.gif"
                     class="reset" />
               </div>
            </div>
            <div id="divConnDisPopup" runat="server" class="modal backgroundoverlay" clientidmode="Static">
               <div class="modal-dialog modal-dialog-centered">
                  <div class="modal-content">
                     <div>
                        <b>
                           <asp:Label ID="lbl" runat="server"></asp:Label>
                        </b>
                     </div>
                     <div class="modal-header">
                        <h5 class="modal-title">Confirmation
                               </h5>
                     </div>
                     <div class="modal-body">
                         <asp:Label ID="lblConnDisconn" runat="server" Text="" ></asp:Label>
                     </div>   
                     <div class="modal-footer border-top-0">
                        <a clientidmode="Static" href="javascript: void(0)" causesvalidation="false" 
                         class="cancel_btn add-scroller btn btn-outline-primary m-r-15" onclick="Cancel();">Cancel </a>
                        
                        <asp:LinkButton ID="lnkConnDisconn" runat="server" ClientIDMode="Static" Text="Yes"
                           CssClass="btn btn-primary" OnClick="lnkConnDisconn_Click" OnClientClick="CallConnDissCon(); showLoader1();"></asp:LinkButton>

                        
                     </div>
                  </div>
               </div>
            </div>
            <div id="divMessPopup" class="modal backgroundoverlay" runat="server"  clientidmode="Static">
               <div class="modal-dialog modal-dialog-centered">
                  <div class="modal-content">
                     <div class="modal-header">
                       <h5 class="modal-title">Message</h5>
                     </div>
                     <div class="modal-body">
                      <div class="form-group">
                       <label for="Message">Subject</label>   
                       <asp:TextBox ID="txtSubject" maxlength="100" runat="server" ClientIDMode="Static" placeholder="Subject"
                        class="forumTitle Msgsubjectsp form-control"></asp:TextBox>
                      
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject" Display="Dynamic" ClientIDMode="Static" 
                        ErrorMessage="Please enter subject." ValidationGroup="PeopleMessage" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>
                      <div class="form-group">
                       <label for="Message">Message</label>   
                       <textarea id="txtBody" runat="server" maxlength="500" cols="20" rows="2" placeholder="Message" clientidmode="Static"
                       class="MsgserachBodey form-control" />                       
                      
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" 
                        ControlToValidate="txtBody" Display="Dynamic" ValidationGroup="PeopleMessage"
                        ErrorMessage="Please enter a message" ForeColor="Red"></asp:RequiredFieldValidator>
                      </div>   
                     </div>
        
                     <div class="modal-footer border-top-0 padding-top-0">
                     <%--<a href="javascript: void(0)" class="btn add-scroller btn-outline-primary m-r-15" onclick="Cancel();">Cancel </a>--%>  
                      <asp:LinkButton ID="btnCancel" CommandName="Join" CausesValidation="false" 
                       runat="server" Text="Cancel" class="cancel_btn add-scroller btn btn-outline-primary m-r-15" OnClick="btnCancel_Click"></asp:LinkButton>
                      <%--OnClick="lnkCancel_Click"--%>
                      <asp:LinkButton ID="lnkSendMess" runat="server" ValidationGroup="PeopleMessage" ClientIDMode="Static" Text="Send"
                       CssClass="btn btn-primary" OnClientClick="javascript:CallSendMessage();" OnClick="lnkSendMess_Click">
                      </asp:LinkButton>                       
                     </div>
                  </div>   
               </div>
            </div>
            <div id="divSuccess" runat="server" class="modal backgroundoverlay" clientidmode="Static" style="display: none;">
             <div class="modal-dialog modal-dialog-centered">
              <div class="modal-content">
               <div class="modal-header">
                <h5 class="modal-title">Success  
                </h5>
               </div>
               <div class="modal-body">
                <asp:Label ID="lblSuccess" runat="server" Text=""> </asp:Label>
               </div>   
               <div class="modal-footer border-top-0">
                <a ClientIDMode="Static" CausesValidation="false" href="javascript: void(0)" class="btn add-scroller btn-outline-primary" 
                 onclick="Cancel();">Close </a>
               </div>
              </div>
             </div>
            </div>
            <div class="panel-cover clearfix">
               <div class="full-width-con">
                <div class="form-inline">
                  <div class="search-filter-con w-100">
                    <asp:UpdatePanel ID="pnlSearch" class="search-bar col-lg-4 clearfix margin-bottom-m p-0 col-sm-12 col-12" runat="server" UpdateMode="Conditional">
                     <ContentTemplate>
                     
                      <div class="w-100 search-cover">
                         <asp:TextBox ID="txtPplSearch" runat="server" autocomplete="off" placeholder="Search People" ClientIDMode="Static" class="form-control p-r-0"
                          onkeydown="return postSearchPpl.bind(this)(event)">
                         </asp:TextBox>
                         <input type="button" id="searchBtn" class="search-btn-2" value="">
                      </div>   
                    
        
                      <asp:Button ID="btnPplSearch" runat="server" ClientIDMode="Static" OnClick="btnPplSearch_Click" Width="50px" Height="30px" Text="Search" Style="display: none" />
                      <asp:HiddenField ID="hdnSearch" runat="server" Value='' ClientIDMode="Static" />
                     </ContentTemplate>
                    </asp:UpdatePanel>
                  </div>  
                </div>
                <!--search result list starts-->
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                <asp:ListView ID="lstPostUpdates" runat="server" OnItemCommand="lstPostUpdates_ItemCommand"
                     OnItemDataBound="lstPostUpdates_ItemDataBound">
                     <LayoutTemplate>
                        <div class="row create-group-list">
                           <span id="itemPlaceHolder" runat="server">
                           </span>
                        </div>
                     </LayoutTemplate>
                     <ItemTemplate>
                        <div class="col-sm-3">
                           <div class="doc-card text-center">
                           <div class="searchListNew list_style">
                              <div class="connected-user" id="connected_user_icon" runat="server" visible="false"><span class="add-icon"><img src="images/connected.svg" style="height:15px;"></span></div>
                              <span class="img-cover">
                               <asp:ImageButton id="imgprofile" runat="server" class="user-img cursor-pointer" ImageUrl='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' 
                                CommandName="Details" />
                               <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                              </span>
                           
                              
                              <h4>
                                 <asp:LinkButton ID="lblPostlink" class="un-anchor truncate cursor-pointer" Text='<%# Eval("strPostDescription") %>'
                                    CommandName="Details" runat="server"></asp:LinkButton>
                              </h4>
             
                           <p>
                                 <!-- <asp:Label ID="lblName" Text='<%# Eval("UserName") %>' runat="server"></asp:Label> -->
                                  <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' runat="server" />
                                 <asp:HiddenField ID="hdnintUserTypeID" Value='<%#Eval("intUserTypeID") %>' runat="server" />
                                 <asp:HiddenField ID="hdnstrOthers" Value='<%#Eval("strOthers") %>' runat="server" /> 
                              </p>
                  
                              <!--<p class="label">
                                 <asp:Label ID="lblCityState" Text='<%# Eval("CityState") %>' runat="server"></asp:Label>
                              </p>-->
                
                              <span class="member-group">
                               <span class="member-icon"><img src="images/member.svg" alt=""></span>
                               <asp:Label ID="lblFriends" runat="server" class="mr-1"></asp:Label><%-- friends--%>
                              </span>
                                   
                                    <tr id="trRecommend" visible="false" style="display: none" runat="server">
                                       <td align="right" valign="top">
                                          <fieldset>
                                             <table class="recommend">
                                                <tbody>
                                                   <tr>
                                                      <td id="Footer" align="right" colspan="3">
                                                         <asp:LinkButton ClientIDMode="Static" CommandName="Close" CssClass="linkClass" ID="lnkClose"
                                                            runat="server" Text="X"></asp:LinkButton>
                                                      </td>
                                                   </tr>
                                                   <tr>
                                                      <td class="ext_recommend">
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
                                                         <label class="TextCss textcss"  id="lblErrorRemark">
                                                         Please Enter text.
                                                         </label>
                                                      </td>
                                                   </tr>
                                                </tbody>
                                             </table>
                                          </fieldset>
                                       </td>
                                    </tr>      
                              
                         
                              <div class="connect-buttons">
                                 <asp:LinkButton ID="btnsendreq" CssClass="connectedSearchFriend btn btn-outline-primary hide-body-scroll" CommandName="Connect" OnClientClick="showLoader1();"
                                    CausesValidation="false" runat="server" Text="Connect"></asp:LinkButton>
                                 <asp:LinkButton ID="lnkDisConnect" CssClass="connectedSearchFriend btn btn-outline-primary hide-body-scroll mr-2" CommandName="DisConnect"
                                    CausesValidation="false" runat="server" Text="Disconnect"></asp:LinkButton>
                                 <asp:LinkButton ID="btnRecmnd" CssClass="connectedSearchFriend btn btn-outline-primary hide-body-scroll" CommandName="Recommendation"
                                    CausesValidation="false" runat="server" Text="Message"></asp:LinkButton>
                              </div>
                           </div>
                           <div style="display: none" class="jury">
                              <asp:Label ID="lblEmailId" Text='<%# Eval("vchrUserName") %>' runat="server"></asp:Label>
                           </div>
                     
                           </div>
                        </div>
                     </ItemTemplate>
                  </asp:ListView>
                <!--search result list ends-->
                <div class="pagination_main_div">
                     <ul id="dvPage" runat="server" class="pagination" clientidmode="Static"
                        >
                        <asp:LinkButton ID="lnkPrevious" class="page-link" ClientIDMode="Static" runat="server" OnClick="lnkPrevious_Click">
                           <span class="lnr lnr-chevron-left">
                        </asp:LinkButton>
                        <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand"
                           OnItemDataBound="rptDvPage_ItemDataBound">
                           <ItemTemplate>
                                <li class="page-item">
                              <asp:LinkButton ID="lnkPageLink" runat="server" ClientIDMode="Static" CommandName="PageLink"  class="page-link"
                                 Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                                    </li>
                           </ItemTemplate>
                        </asp:Repeater>
                        <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click" ClientIDMode="Static" class="page-link"><span class="lnr lnr-chevron-right"></span> </asp:LinkButton>
                        <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                        <asp:HiddenField ID="hdnEndPage" runat="server" ClientIDMode="Static" />
                     </ul>
                  </div>
               </div>   
            </div>
            <div class="adv" style="display: none;">
               <!--right side banner starts-->
               <img src="images/adv1.jpg" /><br />
               <br />
               <img src="images/adv2.jpg" />
               <!--right side banner ends-->
            </div>
         </div>
         <!--pagination starts-->
         <div class="innerContainer">
         </div>
         <!--pagination ends-->
         <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
         <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
         <asp:Button ID="Button1" runat="server" Style="display: none" Text="Button" />
        
     
      </ContentTemplate>
      <Triggers>
         <asp:AsyncPostBackTrigger ControlID="lnkConnDisconn" />
         <asp:AsyncPostBackTrigger ControlID="lnkSendMess" />
         <asp:AsyncPostBackTrigger ControlID="lnkNext" />
      </Triggers>
   </asp:UpdatePanel>
   <script type="text/javascript">
      function CheckBoxCheck(ctl) {
          var myCars = new Array();
          myCars[1] = ctl;
          $.ajax({
              type: "POST",
              url: '<%= ResolveUrl("SearchPeople.aspx/SaveComment1") %>',
              data: JSON.stringify({ array: myCars }),
              contentType: "application/json; charset=utf-8",
              dataType: "json",
      
              success: function (data) {
                  GetComments();
              },
              error: function (XMLHttpRequest, textStatus, errorThrown) {
                  alert('Error has occured for getting timezone');
              }
          });
      }
   </script>
   <script type="text/javascript">
      $(function check() {
          if ($("#myCheckbox").is(':checked')) {
              $(".checkedBox").prop("checked", true);
      
          } else {
              $(".checkedBox").prop("checked", false);
          }
       });
       function postSearchPpl(e) {
           if (e.keyCode == 13 && $(this).val()) {
               $('#btnPplSearch').click();
               showLoader1();
               return false;
           } else if (e.keyCode == 13) {
               return false;
           }
       }
   </script>
   <script type="text/javascript">
      function EnterEvent(ctl) {
          __doPostBack('PostBackFromOnclick_btnOpen', ctl);
      }   
   </script>
   <script type="text/jscript">
       function Cancel() {
          document.getElementById("divConnDisPopup").style.display = 'none';
          document.getElementById("divMessPopup").style.display = 'none';
          document.getElementById("divSuccess").style.display = 'none';
          hideLoader1();
          return false;
      }
       $(document).ready(function () {
          hideLoader1();
          $('.connectedSearchFriend').click(function (e) {
              //$(this).css("background", "#19A0AA");
              //$(this).css("box-shadow", "0px 0px 5px #00B7E5");
          });
          if ($('#hdnCurrentPage').val() == '1') {
               $('#lnkPrevious').hide();
              //$('#imgPaging').css("display", "none");
             // $('#lnkprev').css("display", "block");
          } else {
             // $('#imgPaging').css("display", "block");
              //$('#lnkprev').css("display", "none");
              $('#lnkPrevious').show();
          }
      
          if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
              //$('#lnkNextshow').css("display", "block");
              $('#lnkNext').hide();
          } else {
              $('#lnkNext').show();
          }
          $("#dvPage a").on('click', function(event){showLoader1();});
      
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              hideLoader1();
              $('.connectedSearchFriend').click(function (e) {
                  $(this).css("background", "#19A0AA");
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
              if ($('#hdnCurrentPage').val() == '1') {
                   $('#lnkPrevious').hide();
                  //$('#imgPaging').css("display", "none");
                 // $('#lnkprev').css("display", "block");
              } else {
                 // $('#imgPaging').css("display", "block");
                  //$('#lnkprev').css("display", "none");
                  $('#lnkPrevious').show();
              }
      
              if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                  //$('#lnkNextshow').css("display", "block");
                  $('#lnkNext').hide();
              } else {
                  $('#lnkNext').show();
              }

              $("#dvPage a").on('click', function(event){showLoader1();});
          });
       });

       function CallSendMessage() {
          $('#lnkSendMess').css("box-shadow", "0px 0px 5px #00B7E5");
          //$('#lnkSendMess').addClass('disabled');

          if ($('#txtSubject').val() != '' && $('#txtBody').val() != '') {
               showLoader1();
           }

          if ($('#txtSubject').text() == '' || $('#txtBody').text() == '') {
              setTimeout(
                  function () {
                      //debugger;
                  //$('#lnkSendMess').removeClass('disabled');
                  $('#lnkSendMess').css("box-shadow", "0px 0px 0px #00B7E5");
              }, 1000);
          }
      }
      function CallConnDissCon() {
          $('#lnkConnDisconn').css("box-shadow", "0px 0px 5px #00B7E5");
      }
   </script>
   <script type="text/javascript">

        function showLoader1() {
            $("#globalLoader").show();
        }

        function hideLoader1() {
            $("#globalLoader").hide();
        }

        function postSearch(e) {
           if (e.keyCode == 13 && $(this).val()) {
               $('#btnSearchHeader').click();
               return false;
           } else if (e.keyCode == 13) {
               return false;
           }           
       }

    </script>
</asp:Content>
