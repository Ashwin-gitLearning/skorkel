<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="create-poll.aspx.cs" Inherits="create_poll" %>
   
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Src="~/UserControl/Groups-m.ascx" TagName="GroupDetailsM" TagPrefix="GroupM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
  <%--<script src="<%=ResolveUrl("js/jquery.datepick.js")%>" type="text/javascript"></script>--%>
  <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
  <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
  <script src="<%=ResolveUrl("js/bootstrap-datepicker.min.js")%>" type="text/javascript">></script>     
 </asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                <!--inner container ends-->
                <div class="main-section-inner">
                    <div class="panel-cover clearfix">
                        <!--left box starts-->
                        <div class="center-panel">
                            <div class="custom-nav-con group-page-tab">
                               <GroupM:GroupDetailsM ID="grpDetailsM" runat="server" />
                                <ul class="custom-nav-control nav nav-tabs">
                                    <li class="nav-item">
                                        <asp:LinkButton class="nav-link" ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static" OnClick="lnkProfile_Click"></asp:LinkButton>
                                    </li>
                                    <li class="nav-item" id="DivHome" runat="server">
                                        <asp:LinkButton class="nav-link" ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                                    </li>
                                    <li class="nav-item" id="DivForumTab" runat="server" clientidmode="Static">
                                        <asp:LinkButton class="nav-link" ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static" OnClick="lnkForumTab_Click"></asp:LinkButton>
                                    </li>
                                    <li class="nav-item" id="DivUploadTab" runat="server" clientidmode="Static">
                                        <asp:LinkButton ID="lnkUploadTab" class="nav-link" runat="server" Text="Uploads" ClientIDMode="Static" OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                    </li>
                                    <li class="nav-item" id="DivPollTab" runat="server" clientidmode="Static">
                                        <asp:LinkButton ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static" class="nav-link active" OnClick="lnkPollTab_Click"></asp:LinkButton>
                                    </li>
                                    <li class="nav-item" id="DivEventTab" runat="server" clientidmode="Static">
                                        <asp:LinkButton class="nav-link" ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static" OnClick="lnkEventTab_Click"></asp:LinkButton>
                                    </li>
                                    <li class="nav-item" id="DivMemberTab" runat="server" clientidmode="Static">
                                        <div>
                                            <asp:LinkButton class="nav-link" ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static" OnClick="lnkEventMemberTab_Click"></asp:LinkButton>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="back-link-cover mt-2">
                                <asp:LinkButton ID="lnkBack" runat="server" class="back-arrow-height back-link" OnClick="lnkBack_Click" Text="Back">
                                   <span class="lnr lnr-arrow-left"></span> Back
                                </asp:LinkButton>
                            </div>
                            <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div id="divSuccessPolls" class="modal backgroundoverlay" runat="server" style=" display: block;" clientidmode="Static">
                                            <div class=" modal-dialog modal-dialog-centered">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h5 class="modal-title">Success</h5>
                                                        <!-- <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                            <span aria-hidden="true">&times;</span>
                                                        </button> -->
                                                    </div>
                                                    <div class="modal-body">
                                                        <asp:Label ID="lblSuccess" runat="server" Text="Poll created successfully."></asp:Label>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <asp:LinkButton ID="lnkSuccessPoll" runat="server" Text="Ok" ClientIDMode="Static" CausesValidation="false" class="btn btn-primary" OnClick="lnkSuccessPoll_Click"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card">
                                            <div class="card-body votecardcreate">
                                                <div class="form-group">
                                                 <label for="txtQuestion">Write your question</label>
                                                 <asp:TextBox ID="txtQuestion" autocomplete="off" runat="server" class="form-control" placeholder="Type question here" ClientIDMode="Static" MaxLength="500"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuestion" Display="Dynamic" ValidationGroup="Polls" 
                                                  ErrorMessage="Please enter Question" ForeColor="Red">
                                                 </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                 <label for="txtDescription">Description</label>
                                                 <textarea Id="txtDescription" runat="server" class="form-control" rows="5" ClientIDMode="Static" placeholder="Description" MaxLength="500"/>
                                                </div>
                                                <div class="form-group">
                                                 <label for="exampleFormControlTextarea1">Options</label>
                                                 <div id="div1" runat="server" clientidmode="Static" class="row align-items-center mb-3 clearfix">
                                                  <div class="col-10 col-lg-11 col-md-10 float-left">
                                                   <asp:TextBox ID="TextBox1" runat="server" ClientIDMode="Static" class="form-control" autocomplete="off" value="Option 1" MaxLength="200"
                                                    onblur="if(this.value=='') this.value='Option 1';" onfocus="if(this.value=='Option 1') this.value='';"></asp:TextBox>
                                                  </div>
                                                  <div class="col-2 col-lg-1 col-md-2 float-right">
                                                   <a href="#?" onclick="Removeattach('1');" id="deleteimage1">
                                                    <asp:Image ID="crossimg" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" class="cimg" />
                                                   </a>
                                                  </div>
                                                 </div>
                                                 <div id="div2" class="row align-items-center mb-3 clearfix" runat="server" clientidmode="Static">
                                                  <div class="col-10 col-lg-11 col-md-10 float-left">
                                                   <asp:TextBox autocomplete="off" ID="TextBox2" ClientIDMode="Static" runat="server" class="form-control" name="txtUsername" value="Option 2" MaxLength="200"
                                                    onblur="if(this.value=='') this.value='Option 2';" onfocus="if(this.value=='Option 2') this.value='';"></asp:TextBox>
                                                  </div>
                                                  <div class="col-2 col-lg-1 col-md-2 float-right">
                                                   <a href="#?" onclick="Removeattach('2');" id="deleteimage2">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" class="cimg" />
                                                   </a>
                                                  </div>
                                                 </div>
                                                 <div id="div3" class="row align-items-center mb-3 clearfix" runat="server" clientidmode="Static">
                                                  <div class="col-10 col-lg-11 col-md-10 float-left">
                                                   <asp:TextBox ID="TextBox3" autocomplete="off" ClientIDMode="Static" runat="server" class="form-control" name="txtUsername" value="Option 3" MaxLength="200"
                                                    onblur="if(this.value=='') this.value='Option 3';" onfocus="if(this.value=='Option 3') this.value='';"></asp:TextBox>
                                                  </div>
                                                  <div class="col-2 col-lg-1 col-md-2 float-right">
                                                   <a href="#?" onclick="Removeattach('3');" id="deleteimage3">
                                                    <asp:Image ID="Image2" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" class="cimg" />
                                                   </a>
                                                  </div>
                                                 </div>
                                                 <div id="div4" class="row align-items-center mb-3 clearfix" runat="server" clientidmode="Static">
                                                  <div class="col-10 col-lg-11 col-md-10 float-left">
                                                   <asp:TextBox autocomplete="off" ID="TextBox4" ClientIDMode="Static" runat="server" class="form-control" name="txtUsername" value="Option 4" MaxLength="200"
                                                    onblur="if(this.value=='') this.value='Option 4';" onfocus="if(this.value=='Option 4') this.value='';"></asp:TextBox>
                                                  </div>
                                                  <div class="col-2 col-lg-1 col-md-2 float-right">
                                                   <a href="#?" onclick="Removeattach('4');" id="deleteimage4">
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" class="cimg" />
                                                   </a>
                                                  </div>
                                                 </div>
                                                 <div id="div5" class="row align-items-center mb-3 clearfix" runat="server" clientidmode="Static">
                                                  <div class="col-10 col-lg-11 col-md-10 float-left">
                                                   <asp:TextBox autocomplete="off" ID="TextBox5" ClientIDMode="Static" runat="server" class="form-control" name="txtUsername" value="Option 5" MaxLength="200" 
                                                    onblur="if(this.value=='') this.value='Option 5';" onfocus="if(this.value=='Option 5') this.value='';"></asp:TextBox>
                                                  </div>
                                                  <div class="col-2 col-lg-1 col-md-2 float-right">
                                                   <a href="#?" onclick="Removeattach('5');" id="deleteimage5">
                                                    <asp:Image ID="Image4" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" class="cimg" />
                                                   </a>
                                                  </div>
                                                 </div>
                                                 <div id="div6" class="row align-items-center mb-3 clearfix" runat="server" clientidmode="Static">
                                                  <div class="col-10 col-lg-11 col-md-10 float-left">
                                                   <asp:TextBox autocomplete="off" ID="TextBox6" ClientIDMode="Static" runat="server" class="form-control" name="txtUsername" value="Option 6" MaxLength="200"
                                                    onblur="if(this.value=='') this.value='Option 6';" onfocus="if(this.value=='Option 6') this.value='';"></asp:TextBox>
                                                  </div>
                                                  <div class="col-2 col-lg-1 col-md-2 float-right">
                                                   <a href="#?" onclick="Removeattach('6');" id="deleteimage6">
                                                    <asp:Image ID="Image5" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" class="cimg" />
                                                   </a>
                                                  </div>
                                                 </div>
                                                 <div id="div7" class="row align-items-center mb-3 clearfix" runat="server" clientidmode="Static">
                                                  <div class="col-10 col-lg-11 col-md-10 float-left">
                                                   <asp:TextBox autocomplete="off" ID="TextBox7" ClientIDMode="Static" runat="server" class="form-control" name="txtUsername" value="Option 7" MaxLength="200" 
                                                    onblur="if(this.value=='') this.value='Option 7';" onfocus="if(this.value=='Option 7') this.value='';"></asp:TextBox>
                                                  </div>
                                                  <div class="col-2 col-lg-1 col-md-2 float-right">
                                                   <a href="#?" onclick="Removeattach('7');" id="deleteimage7">
                                                    <asp:Image ID="Image6" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" class="cimg" />
                                                   </a>
                                                  </div>
                                                 </div>
                                                 <div id="div8" class="row align-items-center mb-3 clearfix" runat="server" clientidmode="Static">
                                                  <div class="col-10 col-lg-11 col-md-10 float-left">
                                                   <asp:TextBox autocomplete="off" ID="TextBox8" ClientIDMode="Static" runat="server" class="form-control" name="txtUsername" value="Option 8" MaxLength="200"
                                                    onblur="if(this.value=='') this.value='Option 8';" onfocus="if(this.value=='Option 8') this.value='';"></asp:TextBox>
                                                  </div>
                                                  <div class="col-2 col-lg-1 col-md-2 float-right">
                                                   <a href="#?" onclick="Removeattach('8');" id="deleteimage8">
                                                    <asp:Image ID="Image7" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" class="cimg" />
                                                   </a>
                                                  </div>
                                                 </div>
                                                 <div id="div9" class="row align-items-center mb-3 clearfix" runat="server" clientidmode="Static">
                                                  <div class="col-10 col-lg-11 col-md-10 float-left">
                                                   <asp:TextBox autocomplete="off" ID="TextBox9" ClientIDMode="Static" runat="server" class="form-control" name="txtUsername" value="Option 9" MaxLength="200"
                                                    onblur="if(this.value=='') this.value='Option 9';" onfocus="if(this.value=='Option 9') this.value='';"></asp:TextBox>
                                                  </div>
                                                  <div class="col-2 col-lg-1 col-md-2 float-right">
                                                   <a href="#?" onclick="Removeattach('9');" id="deleteimage9">
                                                    <asp:Image ID="Image8" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" class="cimg" />
                                                   </a>
                                                  </div>
                                                 </div>
                                                 <div id="div10" class="row align-items-center mb-3 clearfix" runat="server" clientidmode="Static">
                                                  <div class="col-10 col-lg-11 col-md-10 float-left">
                                                   <asp:TextBox autocomplete="off" ID="TextBox10" ClientIDMode="Static" runat="server" class="form-control" name="txtUsername" value="Option 10" MaxLength="200"
                                                    onblur="if(this.value=='') this.value='Option 10';" onfocus="if(this.value=='Option 10') this.value='';"></asp:TextBox>
                                                  </div>
                                                  <div class="col-2 col-lg-1 col-md-2 float-right">
                                                   <a href="#?" onclick="Removeattach('10');" id="deleteimage10">
                                                    <asp:Image ID="Image9" runat="server" ImageUrl="images/Delete.gif" ToolTip="Remove" class="cimg" />
                                                   </a>
                                                  </div>
                                                 </div>
                                                </div>
                                                <div class="add_caption">
                                                 <asp:LinkButton ID="Button2" ClientIDMode="Static" runat="server" class=" btn btn-primary w-150" Text="Add Option" 
                                                  OnClientClick="javascript:showfile1();return false;">
                                                 </asp:LinkButton>
                                                </div>
                                                
                                                <p><asp:Label ID="lblMessage" ForeColor="Red" runat="server" class="text-right"></asp:Label></p>
                                                
                                                <div class="form-group mt-3">
                                                 <label for="textarea">Voting Pattern</label>
                                                 <br>
                                                 <div class="form-check form-check-inline">
                                                  <div class="custom-checkbox">
                                                   <asp:CheckBox ID="rdbSinglePattern" runat="server" Text="Allow only one option selection" ClientIDMode="Static" GroupName="Voting Pattern" />
                                                  </div>
                                                 </div>
                                                 <div class="form-check form-check-inline">
                                                  <div class="custom-checkbox">
                                                   <asp:CheckBox ID="rdbMultiplePattern" runat="server" Text="Allow multiple option selection" ClientIDMode="Static" GroupName="Voting Pattern" />
                                                  </div>
                                                 </div>
                                                </div>
                                                   
                                             <div class="form-group">
    <label for="textarea">Voting Ends</label>
    <br>
    <div class="row align-items-center">
        <div class="col-sm-2">
            <div class="custom-checkbox">
                <asp:CheckBox ID="rdVotingNeverEnds" runat="server" Text="Never" GroupName="Voting Ends" ClientIDMode="Static" onclick="rdoChanged(this);" /> </div>
        </div>
        
        <div class="col-sm-10 d-flex align-items-center">
        <div class="d-inline-flex">
            <div class="custom-checkbox">
                <asp:CheckBox ID="rdVotingEnds" runat="server" Text="&nbsp;" GroupName="Voting Ends" ClientIDMode="Static" onclick="rdoChanged(this);" /> </div>
        </div>
        <div class="form-group mb-0 d-inline-flex">
            <asp:DropDownList Enabled="false" ID="ddlTime" runat="server" CssClass="votingTime form-control" ClientIDMode="Static">
                <asp:ListItem Text="1 AM" Value="1:00 AM"></asp:ListItem>
                <asp:ListItem Text="2 AM" Value="2:00 AM"></asp:ListItem>
                <asp:ListItem Text="3 AM" Value="3:00 AM"></asp:ListItem>
                <asp:ListItem Text="4 AM" Value="4:00 AM"></asp:ListItem>
                <asp:ListItem Text="5 AM" Value="5:00 AM"></asp:ListItem>
                <asp:ListItem Text="6 AM" Value="6:00 AM"></asp:ListItem>
                <asp:ListItem Text="7 AM" Value="7:00 AM"></asp:ListItem>
                <asp:ListItem Text="8 AM" Value="8:00 AM"></asp:ListItem>
                <asp:ListItem Text="9 AM" Value="9:00 AM"></asp:ListItem>
                <asp:ListItem Text="10 AM" Value="10:00 AM"></asp:ListItem>
                <asp:ListItem Text="11 AM" Value="11:00 AM"></asp:ListItem>
                <asp:ListItem Text="12 AM" Value="12:00 AM"></asp:ListItem>
                <asp:ListItem Text="1 PM" Value="1:00 PM"></asp:ListItem>
                <asp:ListItem Text="2 PM" Value="2:00 PM"></asp:ListItem>
                <asp:ListItem Text="3 PM" Value="3:00 PM"></asp:ListItem>
                <asp:ListItem Text="4 PM" Value="4:00 PM"></asp:ListItem>
                <asp:ListItem Text="5 PM" Value="5:00 PM"></asp:ListItem>
                <asp:ListItem Text="6 PM" Value="6:00 PM"></asp:ListItem>
                <asp:ListItem Text="7 PM" Value="7:00 PM"></asp:ListItem>
                <asp:ListItem Text="8 PM" Value="8:00 PM"></asp:ListItem>
                <asp:ListItem Text="9 PM" Value="9:00 PM"></asp:ListItem>
                <asp:ListItem Text="10 PM" Value="10:00 PM"></asp:ListItem>
                <asp:ListItem Text="11 PM" Value="11:00 PM"></asp:ListItem>
                <asp:ListItem Text="12 PM" Value="12:00 PM"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-7 col-lg-4 col-md-5 col-sm-5">
            <div class="form-group mb-0">
                <%--<asp:Calendar ID="dtp" runat="server" Visible="false"></asp:Calendar>--%>
                    <asp:TextBox ID="txtExpireDate" runat="server" ClientIDMode="Static" placeholder="Select Date" class="form-control" onkeypress="event.returnValue = false;" onkeydown="event.returnValue = false;" MaxLength="50"></asp:TextBox>
                    <%--<i class="fa fa-calendar cal_icon" aria-hidden="true"></i>--%> <i class="lnr lnr-calendar-full position-absolute calc" onclick="$('#txtExpireDate').focus();"></i>
                        <%--<asp:LinkButton ID="lnkpickdate" runat="server" onclick="lnkpickdate_click" Text="GetDate"/>--%>
            </div>
        </div>
         </div>

    </div>
    <asp:Label ID="lblDateMessage" runat="server" ForeColor="Red" Text="" ClientIDMode="Static" Style="display: none;"></asp:Label>
</div>
                       <!--     <div class="votingBox">
                        <h4>Voting Ends</h4>
                        <div>
                            
                       
                            <div class="margin_top_twenty">
                                <div class="inline w25">
                                    
                                   
                                </div>
                                <div class="inline half">
                                   
                                    <i class="fa fa-calendar cal_icon" aria-hidden="true"></i>
                                </div>
                                <%-- <div id="divCal" class="GroupPollCal">
                           <img src="images/calendar1.png" align="absmiddle" class="calendarImg" id="img2" />
                           </div>--%>
                            </div>
                        </div>
                        </div> -->
                                                <div style=" margin-left: 22px;">
                                                </div>
                                                <div class="votingBox polls" style="display: none;">
                                                 <strong>Who can Vote</strong>
                                                 <br />
                                                 <asp:CheckBox ID="rdVoteTypePublic" Text="&nbsp; Public" GroupName="Who can Vote" ClientIDMode="Static" runat="server" /> &nbsp;&nbsp;&nbsp;
                                                 <asp:CheckBox ID="rdVoteTypeMember" Text="&nbsp; Group members only" GroupName="Who can Vote" ClientIDMode="Static" runat="server" />
                                                </div>
                                            </div>
                                            
                                            <div class="card-footer text-right">


                                             <asp:LinkButton ID="lnkCancelPoll" Text="Cancel" runat="server" class="btn btn-outline-primary m-r-15" CausesValidation="false" OnClick="lnkCancelPoll_Click">
                                             </asp:LinkButton>
                                             <asp:LinkButton ID="lnkSavePoll" Text="Create Poll" runat="server" CssClass="btn btn-primary" ClientIDMode="Static" OnClientClick="CallSavePoll();" 
                                              OnClick="lnkSavePoll_Click" ValidationGroup="Polls">
                                             </asp:LinkButton>
                                            </div>
                                        </div>
                                </ContentTemplate>
                                <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="lnkSavePoll" />
                                 <asp:AsyncPostBackTrigger ControlID="Button2" />
                                 <asp:AsyncPostBackTrigger ControlID="lnkSuccessPoll" />
                                </Triggers>
                            </asp:UpdatePanel>
                            </div>
                            <!--left box ends-->
                            <!--left verticle search list ends-->
                        </div>
                     
                         <!--groups top box starts-->
                         <Group:GroupDetails ID="grpDetails" runat="server" />
                         <!--groups top box ends-->
                     
                      </div>
                        <%--<script type="text/javascript">
                        function DateControl() {
                            $(function() {
                                $('#txtExpireDate').datepick({ showTrigger: '#imgInterview' });
                            });
                        }
                        $(document).ready(function() {
                            DateControl();
                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                            prm.add_endRequest(function() {
                                $('#txtExpireDate').datepick({ showTrigger: '#imgInterview' });
                            });
                        });
                        </script>--%>
                        <script type="text/javascript" language="javascript">
                        function goBack() {
                            window.history.back()
                        }

                        function showfile1() {
                            $('#Button2').css("box-shadow", "0px 0px 0px #00B7E5");
                            if (document.getElementById('div1').style.display == 'none') {
                                document.getElementById('div1').style.display = 'flex';
                                document.getElementById('Button2').value = 'Add More Option';
                                return;
                            }
                            if (document.getElementById('div2').style.display == 'none') {
                                document.getElementById('div2').style.display = 'flex';
                                document.getElementById('Button2').value = 'Add More Option';
                                return;
                            }
                            if (document.getElementById('div3').style.display == 'none') {
                                document.getElementById('div3').style.display = 'flex';
                                document.getElementById('Button2').value = 'Add More Option';
                                return;
                            }
                            if (document.getElementById('div4').style.display == 'none') {
                                document.getElementById('div4').style.display = 'flex';
                                document.getElementById('Button2').value = 'Add More Option';
                                return;
                            }
                            if (document.getElementById('div5').style.display == 'none') {
                                document.getElementById('div5').style.display = 'flex';
                                document.getElementById('Button2').value = 'Add More Option';
                                return;
                            }
                            if (document.getElementById('div6').style.display == 'none') {
                                document.getElementById('div6').style.display = 'flex';
                                document.getElementById('Button2').value = 'Add More Option';
                                return;
                            }

                            if (document.getElementById('div7').style.display == 'none') {
                                document.getElementById('div7').style.display = 'flex';
                                document.getElementById('Button2').value = 'Add More Option';
                                return;
                            }
                            if (document.getElementById('div8').style.display == 'none') {
                                document.getElementById('div8').style.display = 'flex';
                                document.getElementById('Button2').value = 'Add More Option';
                                return;
                            }
                            if (document.getElementById('div9').style.display == 'none') {
                                document.getElementById('div9').style.display = 'flex';
                                document.getElementById('Button2').value = 'Add More Option';
                                return;
                            }
                            if (document.getElementById('div10').style.display == 'none') {
                                document.getElementById('div10').style.display = 'flex';
                                document.getElementById('Button2').style.display = 'none';
                                return;
                            }
                        }

                        function showDiv(id) {

                            var id1 = 'Div' + id;
                            document.getElementById(id1).value = '';
                            document.getElementById(id1).style.display = 'flex';
                        }

                        function Removeattach(id) {
                            var id2 = '';
                            id2 = 'TextBox' + id;
                            document.getElementById(id2).value = 'Option ' + id;
                            var who = document.getElementById(id2);
                            var who2 = who.cloneNode(false);
                            who2.onchange = who.onchange;
                            who.parentNode.replaceChild(who2, who);
                            id2 = 'div' + id;
                            if (id2 == 'div1') {

                            } else if (id2 == 'div2') {

                            } else {
                                document.getElementById(id2).style.display = 'none';
                            }
                            document.getElementById('Button2').style.display = 'block';
                            var i = 1;
                            var dispval = 0;
                            for (i = 1; i <= 10; i = i + 1) {
                                id2 = 'div' + i;

                                if (document.getElementById(id2).style.display == 'block')
                                    dispval = 1;
                            }

                            if (dispval == 1) {
                                document.getElementById('Button2').value = 'Add More Option';
                            } else {
                                document.getElementById('Button2').value = 'Add Option';
                            }
                        }
                        </script>
                        <script language="javascript" type="text/javascript">
                        function rdoChanged(rdo) {
                            if (rdo.id == '<%= rdVotingNeverEnds.ClientID %>') {
                                document.getElementById('<%= ddlTime.ClientID %>').disabled = true;
                                document.getElementById('<%= txtExpireDate.ClientID %>').disabled = true;
                                $('#lblDateMessage').text('');

                            } else if (rdo.id == '<%= rdVotingEnds.ClientID %>') {
                                document.getElementById('<%= ddlTime.ClientID %>').disabled = false;
                                document.getElementById('<%= txtExpireDate.ClientID %>').disabled = false;
                            }
                        }
                        </script>
                        <script type="text/javascript">
                        $(document).ready(function() {
                            $('#txtQuestion').keypress(function() {
                                $('#txtQuestion').css('font-weight', 'normal');
                            });

                            $('#txtDescription').keypress(function() {
                                $('#txtDescription').css('font-weight', 'normal');
                            });
                            $('#TextBox1').keypress(function() {
                                $('#TextBox1').css('font-weight', 'normal');
                            });
                            $('#TextBox2').keypress(function() {
                                $('#TextBox2').css('font-weight', 'normal');
                            });
                            $('#TextBox3').keypress(function() {
                                $('#TextBox3').css('font-weight', 'normal');
                            });
                            $('#TextBox4').keypress(function() {
                                $('#TextBox4').css('font-weight', 'normal');
                            });
                            $('#TextBox5').keypress(function() {
                                $('#TextBox5').css('font-weight', 'normal');
                            });
                            $('#TextBox6').keypress(function() {
                                $('#TextBox6').css('font-weight', 'normal');
                            });
                            $('#TextBox7').keypress(function() {
                                $('#TextBox7').css('font-weight', 'normal');
                            });
                            $('#TextBox8').keypress(function() {
                                $('#TextBox8').css('font-weight', 'normal');
                            });
                            $('#TextBox9').keypress(function() {
                                $('#TextBox9').css('font-weight', 'normal');
                            });
                            $('#TextBox10').keypress(function() {
                                $('#TextBox10').css('font-weight', 'normal');
                            });

                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                            prm.add_endRequest(function() {
                                $('#txtQuestion').keypress(function() {
                                    $('#txtQuestion').css('font-weight', 'normal');
                                });

                                $('#txtDescription').keypress(function() {
                                    $('#txtDescription').css('font-weight', 'normal');
                                });

                                $('#TextBox1').keypress(function() {
                                    $('#TextBox1').css('font-weight', 'normal');
                                });
                                $('#TextBox2').keypress(function() {
                                    $('#TextBox2').css('font-weight', 'normal');
                                });
                                $('#TextBox3').keypress(function() {
                                    $('#TextBox3').css('font-weight', 'normal');
                                });
                                $('#TextBox4').keypress(function() {
                                    $('#TextBox4').css('font-weight', 'normal');
                                });
                                $('#TextBox5').keypress(function() {
                                    $('#TextBox5').css('font-weight', 'normal');
                                });
                                $('#TextBox6').keypress(function() {
                                    $('#TextBox6').css('font-weight', 'normal');
                                });
                                $('#TextBox7').keypress(function() {
                                    $('#TextBox7').css('font-weight', 'normal');
                                });
                                $('#TextBox8').keypress(function() {
                                    $('#TextBox8').css('font-weight', 'normal');
                                });
                                $('#TextBox9').keypress(function() {
                                    $('#TextBox9').css('font-weight', 'normal');
                                });
                                $('#TextBox10').keypress(function() {
                                    $('#TextBox10').css('font-weight', 'normal');
                                });
                            });
                            if (navigator.userAgent.indexOf('Mozilla') != -1 && navigator.userAgent.indexOf('Chrome') == -1) {
                                $('#divCal').css('margin-left', '74%');
                            }

                        });
                        </script>
                        <script type="text/javascript">
                        $(document).ready(function() {
                            $('#rdbSinglePattern').prop('checked', true);
                            $('#rdVotingNeverEnds').prop('checked', true);
                            $('#rdVoteTypePublic').prop('checked', true);

                            if ($('#rdVotingEnds').is(':checked') == true) {
                                $('#rdVotingNeverEnds').prop('checked', false);
                                document.getElementById('<%= ddlTime.ClientID %>').disabled = false;
                                document.getElementById('<%= txtExpireDate.ClientID %>').disabled = false;
                            }

                            if ($('#rdbMultiplePattern').is(':checked') == true) {
                                $('#rdbSinglePattern').prop('checked', false);
                            }

                            $('#rdbSinglePattern').click(function() {
                                $('#rdbSinglePattern').prop('checked', true);
                                $('#rdbMultiplePattern').prop('checked', false);

                            });

                            $('#rdbMultiplePattern').click(function() {
                                $('#rdbSinglePattern').prop('checked', false);
                                $('#rdbMultiplePattern').prop('checked', true);

                            });
                            $('#rdVotingNeverEnds').click(function() {
                                $('#rdVotingNeverEnds').prop('checked', true);
                                $('#rdVotingEnds').prop('checked', false);

                            });

                            $('#rdVotingEnds').click(function() {
                                $('#rdVotingEnds').prop('checked', true);
                                $('#rdVotingNeverEnds').prop('checked', false);

                            });
                            $('#rdVoteTypePublic').click(function() {
                                $('#rdVoteTypePublic').prop('checked', true);
                                $('#rdVoteTypeMember').prop('checked', false);

                            });

                            $('#rdVoteTypeMember').click(function() {
                                $('#rdVoteTypeMember').prop('checked', true);
                                $('#rdVoteTypePublic').prop('checked', false);

                            });
                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                            prm.add_endRequest(function() {
                                $('#rdbSinglePattern').prop('checked', true);
                                $('#rdVotingNeverEnds').prop('checked', true);
                                $('#rdVoteTypePublic').prop('checked', true);

                                if ($('#rdVotingEnds').is(':checked') == true) {
                                    $('#rdVotingNeverEnds').prop('checked', false);
                                    document.getElementById('<%= ddlTime.ClientID %>').disabled = false;
                                    document.getElementById('<%= txtExpireDate.ClientID %>').disabled = false;
                                }

                                if ($('#rdbMultiplePattern').is(':checked') == true) {
                                    $('#rdbSinglePattern').prop('checked', false);
                                }
                                $('#rdbSinglePattern').click(function() {
                                    $('#rdbSinglePattern').prop('checked', true);
                                    $('#rdbMultiplePattern').prop('checked', false);

                                });

                                $('#rdbMultiplePattern').click(function() {
                                    $('#rdbSinglePattern').prop('checked', false);
                                    $('#rdbMultiplePattern').prop('checked', true);

                                });

                                $('#rdVotingNeverEnds').click(function() {
                                    $('#rdVotingNeverEnds').prop('checked', true);
                                    $('#rdVotingEnds').prop('checked', false);

                                });

                                $('#rdVotingEnds').click(function() {
                                    $('#rdVotingEnds').prop('checked', true);
                                    $('#rdVotingNeverEnds').prop('checked', false);

                                });
                                $('#rdVoteTypePublic').click(function() {
                                    $('#rdVoteTypePublic').prop('checked', true);
                                    $('#rdVoteTypeMember').prop('checked', false);

                                });
                                $('#rdVoteTypeMember').click(function() {
                                    $('#rdVoteTypeMember').prop('checked', true);
                                    $('#rdVoteTypePublic').prop('checked', false);

                                });
                            });

                        });

                        function calldivSucess() {
                            $('#divSuccessPolls').css('display', 'block');
                        }
                        </script>
                        <script type="text/javascript">
                        $(document).ready(function() {
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

                        function CallSavePoll() {
                            $('#lnkSavePoll').css("box-shadow", "0px 0px 5px #00B7E5");
                            if ($('#txtQuestion').val() == '') {
                                setTimeout(
                                    function () {
                                        $('#lnkSavePoll').css("box-shadow", "0px 0px 0px #00B7E5");
                                    }, 1000);
                            } else {
                                $('#lnkSavePoll').addClass('disabled');
                                showLoader1();
                            }
                        }
                        $(document).ready(function() {
                            $('#txtQuestion').keypress(function(e) {
                                if (e.keyCode == 13) {
                                    return false;
                                }
                            });
                        });
                        </script>
                        <script>
                            function DateControlInit() {
                                //$.fn.datepicker.defaults.format = "mm-M-yyyy";
                                if ($('#rdVotingNeverEnds').is(':checked')) {
                                    document.getElementById('<%= txtExpireDate.ClientID %>').disabled = true;
                                }
                                $("#txtExpireDate").datepicker({
                                    autoclose: true,
                                    format: 'dd-M-yyyy',
                                    todayHighlight: true
                                })
                            }
                            $(document).ready(function () {
                                DateControlInit();
                                var prm = Sys.WebForms.PageRequestManager.getInstance();
                                prm.add_endRequest(function() {
                                    DateControlInit();
                                });
                            })

                        </script>

            </asp:Content>