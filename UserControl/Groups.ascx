<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Groups.ascx.cs" Inherits="UserControl_Groups" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%--<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>--%>
<!--groups top box starts-->

        <asp:UpdatePanel ID="updatess" runat="server">
           <ContentTemplate>
            
                 <div class="right-panel-back-layer"></div>
                    <div class="right-panel">
                      <span class="m-view back">Back <span class="lnr lnr-arrow-right"></span></span>
              <div class="aside-bar">
                  <div class="card padding-15">
              <asp:Label ID="lblMessage" runat="server"></asp:Label>
              <!---Group Profile Details-->
              <div class="">
                  <!---Edit and Delete Buttons-->
                  <div class="more-btn text-right edit-btn-fix w-100">
                   <span class="dropdown" id="spandropdownMenuLink" runat="server" visible="false">
                    <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                     <img src="images/more.svg" alt="" class="more-btn">
                    </a>

                    <span class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                      
                      <asp:LinkButton ID="lnkEdit" class="dropdown-item" runat="server" ClientIDMode="Static" OnClick="lnkEdit_Click" Visible="false" OnClientClick="CallEditGroup();"><span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>
                      <!-- <a class="dropdown-item" href="#"><span class="lnr lnr-trash"></span> Delete</a> -->
                      <asp:LinkButton ID="lnkAddMember" runat="server" ClientIDMode="Static" Text="Add Member"
                       OnClick="lnkAddMember_Click" Visible="false" OnClientClick="CallAddMemGroup();" class="dropdown-item"><span class="lnr lnr-users"></span> Add Member</asp:LinkButton>

                     <asp:LinkButton id="lnkLeave" Visible="false" runat="server" class="dropdown-item d-flex align-items-center" clientidmode="Static"
                      OnClientClick="OpenConnection(); return false;"><img class="mr-2" src="images/exit.svg">  Leave Group</asp:LinkButton>
                    </span>
                   </span>  
                  </div>
                 <!---User image-->
                 
                 <img id="imgGrp" runat="server" class="user-img"/>
                  
                 <div class="groupDetailsbox">
                   <!---User name-->
                   <asp:Label ID="lblGroupName" runat="server" class="user-name"></asp:Label>
                   <!---Description summury-->

                   <!--  <p>
                      <asp:Label ID="lblGroupSummary"  runat="server"></asp:Label>
                    </p>  -->                  

                 
                    <!---Created and members details-->
                    <div class="row p-t-b-8">
                       <div class="col-sm-6 right-panel-grid d-flex">
                        <div class="r-icon-con">
                         <img src="images/r-calendar.svg" alt="">
                        </div>
                        <div class="text-con">
                         <span class="create">Created On</span>
                         <span class="date"><asp:Label ID="lblCreatedOn" runat="server"></asp:Label></span>
                        </div>
                       </div>
                       <div class="col-sm-6 right-panel-grid d-flex">

                          <span class="r-icon-con">
                            <img src="images/r-target.svg" alt="">
                          </span>
                          <div class="text-con">
                              <span class="create"> Owner</span>
                              <span class="date"> <asp:Label ID="lblOwner" runat="server" class="truncate owner-name-t"></asp:Label></span>
                          </div>                           

                       </div>                       
                       <div class="col-sm-6 right-panel-grid d-flex">
                          <span class="r-icon-con">
                            <img src="images/r-target.svg" alt="">
                          </span>
                          <div class="text-con">
                              <span class="create"> Members</span>
                              <span class="date"><asp:Label ID="lblMembers" runat="server"></asp:Label></span>
                          </div>                          
                       </div>
                       <div class="col-sm-6 right-panel-grid d-flex">
                          <span class="r-icon-con">
                            <img src="images/r-contract.svg" alt="">
                          </span>
                          <div class="text-con">
                              <span class="create"> Type</span>
                              <span class="date"><asp:Label ID="lblGroupType" runat="server" class="truncate owner-name-t"></asp:Label></span>
                          </div>                           
                       </div>
                    </div>
                     <!---Created and members details Ended-->
                    <div class="row share-box">
                       <asp:LinkButton ID="lnkShare"  runat="server" ClientIDMode="Static" Text="Share" OnClick="lnkShare_Click" OnClientClick="CallShareGroup();" class="btn btn-outline-primary hide-body-scroll"><span class="btntext">Share</span></asp:LinkButton>
                       <a href="javascript: void(0)" id="lnkJoin" runat="server" class="btn btn-outline-primary hide-body-scroll" clientidmode="Static"
                          onclick="OpenConnection();">Join</a> 
                       <a href="javascript: void(0)" id="lnkMessage" runat="server" class="btn btn-outline-primary hide-body-scroll"
                          clientidmode="Static" onclick="OpenMessage();">Message</a>
                    </div>
                 </div>
              </div>
              <!---Group Profile Details Ended-->
              <!--groups top box ends-->
                 </div>
              </div> 
              </div> 
               <!---Share Popup Start-->
              <div clientidmode="Static" id="PopUpShare" class="modal popup-share-group backgroundoverlay" runat="server">
             
                 <div class="modal-dialog modal-dialog-centered" >
                  <div class="modal-content">
                    <div class="modal-header">   

                       <h5 class="modal-title"> <asp:Label ID="lblTitle" Text="Share Group" runat="server"></asp:Label></h5> 
                       <button  type="button" id="close-popup" class="close" >
                         <span aria-hidden="true">×</span>
                       </button>  
                    </div>
                    <b>
                       <asp:Label ID="lblTitleGroup" runat="server"></asp:Label>
                    </b>
                    <div class="modal-body group_text ">
                       <div class="body_text share-profile-popup">
                       
                        <div class="form-group position-relative">
                           <label for="member">To</label>
                           <select data-placeholder="Enter members name here" class="chosen-select form-control" id="txtInvitee"
                              onchange="getMultipleValuesForGroup(this.id)" runat="server" clientidmode="Static"
                              multiple tabindex="4">
                           </select>
                           <%--<uc:UserControl_MultiSelect class="form-control" ID="txtInvitee" ClientIDMode="Static" runat="server" />--%>
                           <%--<asp:HiddenField ID="hdnInvId" ClientIDMode="Static" runat="server" />--%>
                           <asp:HiddenField ID="hdnDepartmentIds" ClientIDMode="Static"  runat="server" />
                           <div >
                            <asp:Label ID="lblMess" ForeColor="Red"  runat="server"
                             Text=""></asp:Label>
                            </div>
                        </div>   

                        <div class="media mb-3">
                          <div>    
                           <img id="imgGrp1" runat="server" class="align-self-center user-img" alt=""/>
                          </div> 
                          <div class="media-body ml-3">
                            <h5 class="mt-0 mb-0"><asp:Label ID="lblGroupName1" runat="server"></asp:Label></h5>
                            <p class="mb-0"><asp:Label ID="lblGroupSummary1" runat="server"></asp:Label></p>
                          </div>
                        </div>
                        <div class="form-group">
                          <label for="Message">Message</label>
                          <textarea id="txtBody" maxlength="500" runat="server" cols="20" rows="2" placeholder="Message" class="forumTitle form-control"></textarea>
                        </div>
                        <div class="form-group">
                          <label for="member">Link</label>
                          <asp:TextBox ID="txtLink" runat="server" value="Paste link" Enabled="false" onblur="if(this.value=='') this.value='Paste link';"
                           onfocus="if(this.value=='Paste link') this.value='';" class="forumTitlenew form-control"></asp:TextBox>
                        </div>  
                       </div>                   

                     <p>
                        <strong>
                           <asp:Label ID="lblMessAccept" runat="server"></asp:Label>
                           <asp:Label ID="lblMessReject" runat="server"></asp:Label>
                        </strong>
                     </p>
                      </div>
                      <div class="modal-footer border-top-0 padding-top-0">              
                         <asp:LinkButton ID="aCancel" runat="server" CausesValidation="false" OnClick="lnkCancels_Click"
                             OnClientClick="javascript:Cancel();" class="btn add-scroller btn-outline-primary m-r-15">
                             Cancel
                          </asp:LinkButton>
                          <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Share"
                             CssClass="btn btn-primary" OnClick="lnkPopupOK_Click" OnClientClick="CallShare();" ></asp:LinkButton>
                      </div>
                    </div>
                </div> 
              </div>
              <!---Share Popup Ended-->
              <!---Grpoup Join popup-->
              <div id="divConnDisPopup" class="modal backgroundoverlay" runat="server" clientidmode="Static">
                 <div class="modal-dialog modal-dialog-centered">
                  <div class="modal-content">
                    <b>
                       <asp:Label ID="lbl" runat="server"></asp:Label>
                    </b>
                    <div class="modal-header">
                      <h5 class="modal-title"> Confirmation </h5>
                    
                    </div>
                    <div class="modal-body">
                       <asp:Label ID="lblConnDisconn" ClientIDMode="Static" runat="server" Text="" ></asp:Label>
                    </div>  
                    <div class="modal-footer border-top-0">
                       <a href="#" clientidmode="Static" class="btn add-scroller btn-outline-primary m-r-15" CausesValidation="false"
                          onclick="CloseGroupMessage();return false;">Cancel </a>
                        <asp:LinkButton ID="lnkConnDisconn" runat="server" ClientIDMode="Static" Text="Yes"
                          CssClass="btn btn-primary add-scroller" OnClick="lnkConnDisconn_Click" OnClientClick="CloseGroupMessage(); showLoader1();"></asp:LinkButton>
                    </div>
                   </div>
                 </div>
              </div>
              <!---Grpoup Join popup Ended-->

              <!---Share Popup sucess msz-->
              <div id="divSuccess"  runat="server" class="modal backgroundoverlay" clientidmode="Static" style="display: none;">
                 <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                      <div class="modal-header">
                       <h5 class="modal-title">
                         Success
                       </h5>
                      </div>
                      <div class="modal-body">
                        <p> <asp:Label ID="lblSuccess" style="color: #464a4b !important" runat="server" Text=""></asp:Label></p>
                      </div>  
                      <div class="modal-footer border-top-0">
                         <asp:LinkButton ID="LinkButton2" runat="server" ClientIDMode="Static" Text="Close"
                            CssClass="joinBtn default_btn btn btn-outline-primary add-scroller" OnClick="brokerSearchButton_Click" OnClientClick="javascript:CallClose();hideLoader1();" ></asp:LinkButton>
                      </div>
                    </div>
                 </div>
              </div>
              <!---Share Popup sucess msz Ended-->
              <!---Message Popup Start-->
              <div id="divGroupMessPopup" runat="server" class="modal backgroundoverlay" clientidmode="Static">
                 <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                      <div class="modal-header ">
                         <h5 class="modal-title">Message</h5>
                      </div>
                      <div class="modal-body">
                        <div class="form-group">
                         <label for="Message">Subject</label>
                         <asp:TextBox ID="txtSubject" maxlength="100" runat="server" ClientIDMode="Static" placeholder="Subject"
                          class="forumTitle form-control">
                         </asp:TextBox>
                        
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject"
                          ErrorMessage="Please enter subject." Display="Dynamic" ValidationGroup="GrpMessage">
                         </asp:RequiredFieldValidator>
                        </div>      
                        
                       <div class="form-group">
                        <label for="Message">Message</label>
                        <textarea id="txtBodyMessage" maxlength="500" runat="server" clientidmode="Static" cols="20" rows="2"
                         placeholder="Message" class="MsgBodygroups form-control"></textarea>                    
                       
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                         runat="server" ControlToValidate="txtBodyMessage" Display="Dynamic"
                         ValidationGroup="GrpMessage" ErrorMessage="Please enter message." ForeColor="Red">
                        </asp:RequiredFieldValidator>
                       </div>     
                      </div>   
                      <div class="modal-footer border-top-0 padding-top-0">
                       <a href="#" ClientIDMode="Static" CausesValidation="false" class="btn add-scroller btn-outline-primary m-r-15" onclick="GroupMessageClose();">
                        Cancel 
                       </a>
                       <asp:LinkButton ID="lnkSendMess" runat="server" ClientIDMode="Static" Text="Send"
                        ValidationGroup="GrpMessage" CssClass="btn btn-primary" OnClick="lnkSendMess_Click" OnClientClick="checkDoubleMessage();">
                       </asp:LinkButton>
                      </div>
                    </div>
                 </div>
              </div> 
              <script type="text/javascript">
                 function Cancel1() {
                     document.getElementById("divSuccess").style.display = 'none';
                     document.getElementById("divConnDisPopup").style.display = 'none';
                     return false;
                 }
              </script>
           </ContentTemplate>
           <Triggers>
              <asp:AsyncPostBackTrigger ControlID="lnkShare" />
              <asp:AsyncPostBackTrigger ControlID="lnkPopupOK" />
              <asp:AsyncPostBackTrigger ControlID="LinkButton2" />
              <asp:AsyncPostBackTrigger ControlID="lnkConnDisconn" />
              <asp:AsyncPostBackTrigger ControlID="lnkSendMess" />
           </Triggers>
        </asp:UpdatePanel>
 



<script type="text/javascript">
   $(document).ready(function () {
       $("#lnkEdit").click(function () {
           $('#PopUpShare').css("display", "none")
           $('#divDeletesucess').css("display", "none")
       });
       $('#lnkShare').click(function () {
           $('#divDeletesucess').css("display", "none")
           $('#divPopupMember').css("display", "none")
           $('#divSuccessAcceptMember').css("display", "none")
           $('#dvPopup').css("display", "none")
           $('#divSuccessMess').css("display", "none")
           $('#divSuccessEvent').css("display", "none")
       });
   });
   $(document).ready(function () {
       var prm = Sys.WebForms.PageRequestManager.getInstance();
       prm.add_endRequest(function () {
           $("#lnkEdit").click(function () {
               $('#PopUpShare').css("display", "none")
               $('#divDeletesucess').css("display", "none")
           });
           $('#lnkShare').click(function () {
               $('#divDeletesucess').css("display", "none")
               $('#divPopupMember').css("display", "none")
               $('#divSuccessAcceptMember').css("display", "none")
               $('#dvPopup').css("display", "none")
               $('#divSuccessMess').css("display", "none")
               $('#divSuccessEvent').css("display", "none")
           });
   
       });
   });
   
</script>
<script type="text/javascript">
    function ShareCancel() {

       $('#divPopupMember').css("display", "none")
       $('#divSuccessAcceptMember').css("display", "none")
       $('#dvPopup').css("display", "none")
       $('#divSuccessMess').css("display", "none")
       $('#divDeletesucess').css("display", "none")
       document.getElementById("eventpopup").css("display", "none")
       //return false;
   }
   function Cancel() {
       document.getElementById("PopUpShare").style.display = 'none';
       document.getElementById("divConnDisPopup").style.display = 'none';
       document.getElementById("divSuccess").style.display = 'none';
       document.getElementById("divGroupMessPopup").style.display = 'none';
       //document.getElementById("eventpopup").style.display = 'none';
       $('#divDeletesucess').css("display", "none")
       $('#hdnDepartmentIds').val('');
       return false;
   }
   function GroupMessageClose() {
       $('#txtSubject').val('');
       $('#txtBodyMessage').val('');
       document.getElementById("PopUpShare").style.display = 'none';
       document.getElementById("divConnDisPopup").style.display = 'none';
       document.getElementById("divSuccess").style.display = 'none';
       document.getElementById("divGroupMessPopup").style.display = 'none';
       $('#divDeletesucess').css("display", "none")
       return false;
   }
   function getMultipleValuesForGroup(ctrlId) {
       $('#tdDepartment').find('label.error').remove();
       var control = document.getElementById(ctrlId);
       var strSelText = '';
       var cnt = 0;
       for (var i = 0; i < control.length; i++) {
           if (control.options[i].selected) {
               if (cnt == 0) {
                   strSelText += control.options[i].value;
               }
               else {
                   strSelText += ',' + control.options[i].value;
               }
               cnt++;
           }
       }
       $('#hdnDepartmentIds').val(strSelText);
   }
</script>
<script type="text/javascript">
   function CloseGroupMessage() {
       document.getElementById("divConnDisPopup").style.display = 'none';
       document.getElementById("divGroupMessPopup").style.display = 'none';
   }
   function OpenConnection() {
       $('#lnkJoin').css("box-shadow", "0px 0px 5px #00B7E5");
       if ($("#lnkJoin").text() == 'Join')
           $('#lblConnDisconn').text("Do you want to Join ?");
       else if ($("#lnkJoin").text() == 'Leave')
           $('#lblConnDisconn').text("Do you want to Leave ?");
   
       $('#divSuccess').hide();
       $('#PopUpShare').hide();
       $('#divConnDisPopup').show();
       $('#divGroupMessPopup').hide();
   }
   
   function OpenMessage() {
       $('#lnkMessage').css("box-shadow", "0px 0px 5px #00B7E5");
       $('#divSuccess').hide();
       $('#PopUpShare').hide();
       $('#divConnDisPopup').hide();
       $('#divGroupMessPopup').show();
   }
   
</script>
<script type="text/javascript">
   $(document).ready(function () {
       $('#divSuccess').center();
       $('#divGroupMessPopup').center();
       $('#lnkConnDisconn').click(function (e) {
           $(this).css("box-shadow", "0px 0px 5px #00B7E5");
       });
   });
   
   $(document).ready(function () {
       var prm = Sys.WebForms.PageRequestManager.getInstance();
       prm.add_endRequest(function () {
           $('#divSuccess').center();
           $('#divGroupMessPopup').center();
           $('#lnkConnDisconn').click(function (e) {
               $(this).css("box-shadow", "0px 0px 5px #00B7E5");
           });
       });
   });
   function CallEditGroup() {
       $('#lnkEdit').css("box-shadow", "0px 0px 5px #00B7E5");
   }
   function CallShareGroup() {
       $('#lnkShare').css("box-shadow", "0px 0px 5px #00B7E5");
   }
   function CallAddMemGroup() {
       $('#lnkAddMember').css("box-shadow", "0px 0px 5px #00B7E5");
   }
    function CallShare() {
       $('#lnkPopupOK').addClass('disabled');
       $('#lnkPopupOK').css("box-shadow", "0px 0px 5px #00B7E5");
   }
   function CallClose() {
       $('#LinkButton2').css("box-shadow", "0px 0px 5px #00B7E5");
    }

    function checkDoubleMessage() {
        if ($('#txtSubject').val() != '' && $('#txtBodyMessage').val() != '' ) {
            $('#lnkSendMess').addClass('disabled');
        }
    }
</script>