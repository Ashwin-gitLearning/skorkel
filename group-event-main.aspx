<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="group-event-main.aspx.cs" Inherits="group_event_main" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Src="~/UserControl/Groups-m.ascx" TagName="GroupDetailsM" TagPrefix="GroupM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <%--<script src="<%=ResolveUrl("js/jquery.datepick.js")%>" type="text/javascript"></script>--%>
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("js/bootstrap-datepicker.min.js")%>" type="text/javascript">></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upmains" runat="server">
        <ContentTemplate>
            <div class="main-section-inner">
                <div class="panel-cover clearfix">
                    <!---Delete event popup-->
                    <div id="divDeletesucess" clientidmode="Static" runat="server" class="modal backgroundoverlay"
                        style="display: none;">
                        <div id="divDeleteConfirm" runat="server" class="modal-dialog modal-dialog-centered" clientidmode="Static">
                            <div class="modal-content">
                                <div>
                                    <b>
                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                    </b>
                                </div>
                                <div class="modal-header">
                                    <h5 class="modal-title">Delete Confirmation
                                    </h5>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="Label4" runat="server" Text="Do you want to delete ?"></asp:Label>
                                </div>
                                <div class="modal-footer border-top-0">
                                    <asp:LinkButton ID="lnkDeleteCancel" runat="server" class="add-scroller btn btn-outline-primary m-r-15" ClientIDMode="Static" Text="Cancel"
                                        OnClientClick="javascript:divCancels();return false;"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes"
                                        CssClass="btn btn-primary success-popup" OnClick="lnkDeleteConfirm_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!---Delete event popup Ended-->
                    <div class="center-panel">
                        <div class="custom-nav-con group-page-tab">
                            <!--left box starts-->
                            <div class="innerGroupBoxnew margin_top_fifteen">
                                <div class="width_h">
                                  <GroupM:GroupDetailsM ID="grpDetailsM" runat="server" />
                                    <ul class="custom-nav-control nav nav-tabs">
                                        <li class="nav-item">
                                            <asp:LinkButton ID="lnkProfile" runat="server" class="nav-link" Text="Profile" ClientIDMode="Static"
                                                OnClick="lnkProfile_Click"></asp:LinkButton>
                                        </li>
                                        <li id="DivHome" runat="server" class="nav-item">
                                            <div>
                                                <asp:LinkButton ID="lnkHome" runat="server" class="nav-link" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                                            </div>
                                        </li>
                                        <li id="DivForumTab" runat="server" clientidmode="Static" class="nav-item">
                                            <div>
                                                <asp:LinkButton ID="lnkForumTab" runat="server" class="nav-link" Text="Forums" ClientIDMode="Static"
                                                    OnClick="lnkForumTab_Click"></asp:LinkButton>
                                            </div>
                                        </li>
                                        <li id="DivUploadTab" runat="server" clientidmode="Static" class="nav-item">
                                            <div>
                                                <asp:LinkButton ID="lnkUploadTab" runat="server" class="nav-link" Text="Uploads" ClientIDMode="Static"
                                                    OnClick="lnkUploadTab_Click"></asp:LinkButton>
                                            </div>
                                        </li>
                                        <li id="DivPollTab" runat="server" clientidmode="Static" class="nav-item">
                                            <div>
                                                <asp:LinkButton ID="lnkPollTab" runat="server" class="nav-link" Text="Polls" ClientIDMode="Static"
                                                    OnClick="lnkPollTab_Click"></asp:LinkButton>
                                            </div>
                                        </li>
                                        <li id="DivEventTab" runat="server" clientidmode="Static" class="nav-item">
                                            <div>
                                                <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                                    class="forumstabAcitve nav-link" OnClick="lnkEventTab_Click"></asp:LinkButton>
                                            </div>
                                        </li>
                                        <li id="DivMemberTab" runat="server" clientidmode="Static" class="nav-item">
                                            <div>
                                                <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                                    OnClick="lnkEventMemberTab_Click" class="nav-link"></asp:LinkButton>
                                            </div>
                                        </li>
                                    </ul>
                                    <div id="divSuccessEvents" runat="server" class="modal backgroundoverlay" style="display: none;" clientidmode="Static">
                                        <div class="modal-dialog modal-dialog-centered">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title">Success</h5>
                                                </div>
                                                <div class="modal-body">
                                                    <asp:Label ID="Label2" runat="server" Text="Event Deleted Successfully." ForeColor="Green"></asp:Label>
                                                </div>
                                                <div class="modal-footer border-top-0">
                                                    <a href="#" clientidmode="Static" class="add-scroller btn btn-outline-primary m-r-15" causesvalidation="false" onclick="javascript:messageCloseEvent();return false;">Close </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divCancelEvent" runat="server" class="modal_bg" style="display: none;" clientidmode="Static">
                                        <div class="modal-dialog">
                                            <div>
                                                <b>
                                                    <asp:Label ID="lbl" runat="server"></asp:Label>
                                                </b>
                                            </div>
                                            <div class="modal-header">
                                                <strong>
                                                    <asp:Label ID="lblConnDisconn" runat="server" Text=""></asp:Label>
                                                </strong>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:LinkButton ID="lnkConnDisconn" runat="server" ClientIDMode="Static" Text="Yes"
                                                    CssClass="joinBtn default_btn" OnClick="lnkConnDisconn_Click"></asp:LinkButton>
                                                <a href="#" clientidmode="Static" class="cancel_btn" causesvalidation="false"
                                                    onclick="Cancel();">Cancel </a>
                                            </div>
                                        </div>
                                    </div>
                                    <!--calendar starts-->
                                    <!---Event Success popup-->
                                    <div id="divSuccessEvent" runat="server" class="modal backgroundoverlay" clientidmode="Static" style="display: none">
                                        <div class="modal-dialog modal-dialog-centered">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title">Success
                                                    </h5>
                                                </div>
                                                <div class="modal-body">
                                                    <asp:Label ID="lblSuccess" runat="server" Text="Event created successfully."></asp:Label>
                                                </div>
                                                <div class="modal-footer border-top-0">
                                                    <asp:LinkButton ID="lnksucessClose" runat="server" ClientIDMode="Static" class="btn btn-outline-primary add-scroller" CausesValidation="false"
                                                        OnClick="lnksucessClose_Click">
                                         Close 
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!---Event sucess popup ended-->

                        <div class="tab-content m-t-15">  
                         <div class="card top-list"> 
                           <!---Calendar Start-->
                           <div class="padding-15 event-main-calendar">
                           <div class="rightCreateEvents">
                              <asp:LinkButton ID="lnkCreate" runat="server" OnClick="lnkCreate_Click" CssClass="btn hide-body-scroll btn-outline-primary crate-event-btn crate-event-btn-main float-right" Text="Create event"></asp:LinkButton>
                               <asp:UpdatePanel ID="updateEvents" class="update-events-main" runat="server">
                                   <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="CalendarEvent" />
                                   </Triggers>
                                   <ContentTemplate>
                                       <asp:Calendar ID="CalendarEvent" runat="server" CellPadding="1" DayNameFormat="Short" Font-Names="Quicksand" CssClass="myCalendar"  Width="100%" FirstDayOfWeek="Monday" Height="90%" OnDayRender="CalendarEvent_DayRender" OnSelectionChanged="CalendarEvent_SelectionChanged" NextPrevStyle-BorderStyle="None">
                                           <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                           <SelectorStyle BackColor="#00b6bc
                                                                     " ForeColor="#fff" />
                                           <WeekendDayStyle BackColor="#ffffff" />
                                           <OtherMonthDayStyle ForeColor="#999999" />
                                           <TodayDayStyle BackColor="#00b6bc
                           " ForeColor="#ffffff" />
                                           <NextPrevStyle Font-Underline="false" Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" CssClass="myCalendarNextPrev" />
                                           <DayHeaderStyle BackColor="#00b6bc
                                                                     " ForeColor="#ffffff" Font-Bold="true" BorderWidth="1px" VerticalAlign="Middle" Height="25px" HorizontalAlign="Center" BorderColor="#e7e7e7" />
                                           <TitleStyle  CssClass="myCalendarTitle"  VerticalAlign="Middle" HorizontalAlign="Center" />
                                           <DayStyle Height="40px" VerticalAlign="Top" HorizontalAlign="Right" BorderColor="#e7e7e7" BorderWidth="1px" />
                                       </asp:Calendar>
                                       <div id="dvPopup" class="modal_bg" runat="server" clientidmode="Static" style="
                                                                  display: none">
                                           <asp:Panel ID="ShowTitle" runat="server" Visible="false">
                                               <div class="modal-dialog">
                                                   <table width="100%" border="0" cellspacing="0" cellpadding="0" class="popTable">
                                                       <tr>
                                                           <td>
                                                               <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                   <asp:ListView ID="lstViewEvents" runat="server" OnItemCommand="lstViewEvents_ItemCommand">
                                                                       <LayoutTemplate>
                                                                           <table cellpadding="0" cellspacing="0">
                                                                               <tr>
                                                                                   <p class="center">
                                                                                       <strong>&nbsp; Select Option</strong>
                                                                                   </p>
                                                                               </tr>
                                                                               <tr id="itemPlaceHolder" runat="server">
                                                                               </tr>
                                                                               <tr>
                                                                                   &nbsp;&nbsp; <a clientidmode="Static" style="cancel_event_popup" causesvalidation="false" onclick="javascript:CloseCalPopup();">Cancel </a>
                                                                               </tr>
                                                                               <tr>
                                                                                   <asp:Label ID="textLine" runat="server" CssClass="popBgLineGray"></asp:Label>
                                                                               </tr>
                                                                           </table>
                                                                       </LayoutTemplate>
                                                                       <ItemTemplate>
                                                                           &nbsp;
                                                                           <asp:LinkButton ID="lnkEvent" runat="server" ClientIDMode="Static" CssClass="SrNo" CommandName="EventDetails" Text='<%# "Event " + Eval("RowNumber") +"&nbsp;&nbsp;&nbsp;"%>'></asp:LinkButton>
                                                                                                <asp:HiddenField ID="hdnEventId" Value='<%# Eval("intGrpEventtId") %>' runat="server" ClientIDMode="Static" />
                                                                                            </ItemTemplate>
                                                                                        </asp:ListView>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="ShowDetails" runat="server" Visible="false">
                                                                    <div id="tdPopupColor" runat="server" class="tdpopupcolor">
                                                                        &nbsp;
                                                                    </div>
                                                                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                                    <div class="lbldescription_events">
                                                                        &nbsp;
                                                   <asp:Label ID="lblDescription" runat="server"></asp:Label>
                                                                        </p>
                                                   <p>
                                                       &nbsp; <strong>Date:</strong>
                                                       <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                   </p>
                                                                        &nbsp; <strong>Venue:</strong>
                                                                        <asp:Label ID="lblVenue" runat="server"></asp:Label>
                                                                        </p>
                                                   <p>
                                                       &nbsp; <strong>Contact:</strong>
                                                       <asp:Label ID="lblContactPerson" runat="server"></asp:Label>
                                                       - +91-
                                                       <asp:Label ID="lblContactNumber" runat="server"></asp:Label>
                                                   </p>
                                                                        <a clientidmode="Static" causesvalidation="false" class="conect_button" onclick="javascript:CloseCalPopup();">Ok </a>
                                                                </asp:Panel>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <!---Calendar end-->
                                            <!---Create Event popup sart-->
                                            <div class="modal backgroundoverlay" id="eventpopup" runat="server" clientidmode="Static">
                                                <div class="modal-dialog modal-dialog-centered">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title">Create Event </h5>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="form-group">
                                                                <label for="Event Title">Event Title</label>
                                                                <asp:TextBox ID="txtTitle" MaxLength="50" autocomplete="off" ClientIDMode="Static" runat="server" placeholder="Title"
                                                                    class="eventTitleField form-control" name="txtUsername"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                                                    Display="Dynamic" ValidationGroup="Events" ErrorMessage="Please enter title"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="Description">Description</label>
                                                                <textarea id="txtDescription" maxlength="500" autocomplete="false" clientidmode="Static" runat="server" placeholder="Description"
                                                                    class="ventDescriptionField form-control"></textarea>
                                                            </div>
                                                            <!---Date Selection-->
                                                            <div class="row">
                                                                <div class="createEventFrom form-group col-sm-4">
                                                                    <label for="Date from">Date from</label>
                                                                    <div class="position-relative">
                                                                        <asp:TextBox ID="txtFromDate" autocomplete="off" runat="server" ClientIDMode="Static" placeholder="From Date" CssClass="form-control eventTitleField" onkeypress="event.returnValue = false;" onkeydown="event.returnValue = false;"></asp:TextBox>
                                                                        <span onclick="fromDateClicked();" class="lnr lnr-calendar-full event-calendar"></span>
                                                                    </div>
                                                           
                                                                        <asp:Label ID="lblDateMessage1" ClientIDMode="Static" ForeColor="Red" runat="server" Text=""></asp:Label>
                                                                                                                                      
                                                                </div>
                                                                <div class="createEventFrom form-group to col-sm-4">
                                                                    <label for="Date to">Date to</label>
                                                                    <div class="position-relative">
                                                                        <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" CssClass="form-control eventTitleField" placeholder="To Date" autocomplete="off"></asp:TextBox>
                                                                        <span onclick="toDateClicked();" class="lnr lnr-calendar-full event-calendar"></span>
                                                                    </div>
                                                                </div>

                                                                <div class="select-event form-group col-sm-4">
                                                                    <label for="Date to">Select Priority</label>

                                                                    <asp:DropDownList  ID="ddlPriorityType" ClientIDMode="Static" runat="server" CssClass="eventType form-control after-down-arrow">
                                                                        <asp:ListItem Text="Select Priority" Value="S"></asp:ListItem>
                                                                        <asp:ListItem Text="Normal" Value="N"></asp:ListItem>
                                                                        <asp:ListItem Text="Important" Value="I"></asp:ListItem>
                                                                        <asp:ListItem Text="Very Important" Value="V"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblDateMessage" ClientIDMode="Static" Style="display: none;" ForeColor="Red" runat="server" Text=""></asp:Label>
                                                                </div>
                                                                
                                                            </div>
                                                            <!---Date Selection Ended-->


                                                            <div class="calEvnt" id="select-event-color">
                                                                <div class="color-picker">
                                                                    <div class="color1" onclick="getSelText1();" clientidmode="Static" class="cursor_pointer">

                                                                        <asp:TextBox ID="txtFiColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#ff3131"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color2" onclick="getSelText2();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txtSeColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#68bee1"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color3" onclick="getSelText3();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txtthColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#fdaf18"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color4" onclick="getSelText4();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txtFoColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#c66905"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color5" onclick="getSelText5();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txtFivColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#23ec02"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color6" onclick="getSelText6();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txtSiColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#021dec"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color7" onclick="getSelText7();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txteSeColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#9a9a9d"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color8" onclick="getSelText8();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txtEigColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#10132a"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color9" onclick="getSelText9();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txtNiColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#70759a"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color10" onclick="getSelText10();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txtTenColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#e77cf4"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color11" onclick="getSelText11();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txtEleColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#8ff1fa"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color12" onclick="getSelText12();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txtTweColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#27950d"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color13" onclick="getSelText13();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txtThiColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#723e14"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <div class="color14" onclick="getSelText14();" class="cursor_pointer">
                                                                        <asp:TextBox ID="txtFouColor" autocomplete="off" ClientIDMode="Static" class="display_none" Text="#f93583"
                                                                            runat="server"></asp:TextBox>
                                                                    </div>
                                                                    <asp:TextBox ID="txtColorCode" Style="display: none" ClientIDMode="Static" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="calEvntTxt">
                                                                <asp:Label ID="Label1" class="font-weight-bold mb-1 inline-block" runat="server" Text="Choose Colour"></asp:Label>
                                                                <br />
                                                                <div class="output-color" onclick="showColor()">
                                                                    <div class="colorPicker">
                                                                        <asp:TextBox Enabled="false" ID="TextBox1" autocomplete="off" class="text_box_Events" ClientIDMode="Static" runat="server"></asp:TextBox>


                                                                    </div>
                                                                    <div class="arrow-color-select"></div>
                                                                </div>
                                                                <div class="lbevent_color_event">
                                                                    <asp:Label ID="lbleventColor" ClientIDMode="Static" class="lbevents_color" ForeColor="Red" runat="server" Text=""></asp:Label>
                                                                </div>
                                                            </div>


                                                        </div>
                                                        <div class="modal-footer border-top-0 padding-top-0">
                                                            <div class="causevalidation">
                                                                <a href="javascript: void(0)" clientidmode="Static" causesvalidation="false" id="close-popup" class="causevalidation_a btn btn-outline-primary m-r-15" onclick="javascript:Clear();">Cancel </a>
                                                            </div>
                                                            <div class="calEvnt linksave_Events" align="left">
                                                                <asp:LinkButton ID="lnkSave" runat="server" Text="Create Event" OnClientClick="CallSaveEvent.bind(this)();" CssClass="btn btn-primary" ValidationGroup="Events" OnClick="lnkSave_Click"></asp:LinkButton>

                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <!---Create Event popup end-->

                                            <div class="leftCreateEvents display_none">
                                                <br />
                                                <asp:TextBox ID="txtVenue" ClientIDMode="Static" runat="server" onfocus="if(this.value=='Venue') this.value='';" Visible="false" onblur="if(this.value=='') this.value='Venue';" value="Venue" class="eventTitleField text_box_venue" name="txtVenue"></asp:TextBox>
                                                <div>
                                                </div>
                                                <asp:TextBox ID="txtContactPerson" ClientIDMode="Static" runat="server" onfocus="if(this.value=='Contact person') this.value='';" onblur="if(this.value=='') this.value='Contact person';" value="Contact person" Visible="false" class="eventTitleField text_box_venue"></asp:TextBox>
                                                <asp:TextBox ID="txtContactPerNumber" ClientIDMode="Static" runat="server" onkeypress="return isNumber(event)" Visible="false" onfocus="if(this.value=='Contact person number') this.value='';" onblur="if(this.value=='') this.value='Contact person number';" value="Contact person number" MaxLength="10" class="PollTitlegrup text_box_venue" name="txtContactPerNumber"></asp:TextBox>
                                            </div>
                                            <div class="cls">
                                            </div>


                                        </div>

                                        <!--left box ends-->
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="myEventsList border_none padding-top-0">
                            <div class="myevents" id="divmyevents" runat="server" style="display: none;">
                                <h4>Group Events</h4>
                            </div>
                            <asp:UpdatePanel ID="upevent" runat="server" class="row">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdnDeletePostQuestionID" Value="" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hdnstrQuestionDescription" ClientIDMode="Static" runat="server" />
                                    <asp:Repeater ID="RptCreatedEvent" OnItemDataBound="RptCreatedEvent_ItemDataBound" runat="server" OnItemCommand="RptCreatedEvent_ItemCommand">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnColor" Value='<%# Eval("strColor") %>' runat="server" />
                                            <asp:HiddenField ID="hdnEventId" Value='<%# Eval("intGrpEventtId") %>' runat="server" />
                                            <asp:UpdatePanel ID="upev" runat="server" class="col-sm-12 col-md-6 m-box">
                                                <ContentTemplate>
                                                    <div>
                                                        <div class="events-box doc-card position-relative">
                                                            <!---Edit and delete Buttons-->
                                                            <span class="more-btn float-right">

                                                                <span class="dropdown">
                                                                    <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                                                        <img src="images/more.svg" alt="" class="more-btn">
                                                                    </a>

                                                                    <span class="dropdown-menu editDeleteMorev" onclick="delSet.bind(this)();" aria-labelledby="dropdownMenuLink" x-placement="bottom-start">

                                                                        <asp:HiddenField ID="hdnintPostQuestionIdelet" Value='<%# Eval("intGrpEventtId") %>' ClientIDMode="Static" runat="server" />
                                                                        <asp:HiddenField ID="lnkstrQuestionDescription" runat="server" Value='<%#Eval("strTitle") %>' ClientIDMode="Static"></asp:HiddenField>

                                                                        <asp:LinkButton ID="lnkEditEvent" Font-Underline="false" class="dropdown-item" ClientIDMode="Static"
                                                                            ToolTip="Edit" Text="Edit" CommandName="Edit" CausesValidation="false" runat="server"> <span class="lnr lnr-pencil"></span> Edit
                                                                        </asp:LinkButton>


                                                                        <asp:LinkButton ID="lnkDeleteEvent" Font-Underline="false" class="dropdown-item hide-body-scroll" ClientIDMode="Static"
                                                                            ToolTip="Delete" Text="Delete" CommandName="Delete" CausesValidation="false"
                                                                            OnClientClick="javascript:docdelete()" runat="server">
                                                                                            <span class="lnr lnr-trash"></span> Delete
                                                                        </asp:LinkButton>

                                                                    </span>
                                                                </span>
                                                            </span>
                                                            <div id="dvcolor" class="event-color" runat="server">
                                                                <asp:Label ID="lblColor" runat="server"></asp:Label>
                                                            </div>
                                                            <asp:Label ID="lblTitles" runat="server" Text='<%# Eval("strTitle") %>' class="lbtitles_s truncate"></asp:Label>
                                                            </span>

                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lnkEditEvent" />
                                                    <asp:AsyncPostBackTrigger ControlID="lnkDeleteEvent" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="RptCreatedEvent" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <!--Event Mouse Over Popup Start Hear-->
                       <%-- <div class="eventDetailsPop event-tooltip arrow_box card" id="eventPop" clientidmode="Static">
                        </div>--%>
                    </div>

                    <!--Center Panel Ended-->
                    <!---Right Panel Start-->
               
                        <asp:UpdatePanel ID="upgrdetails" runat="server">
                            <ContentTemplate>
                                <Group:GroupDetails ID="grpDetails" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                   
                    <!---Right Panel Ended-->
                </div>
                <asp:HiddenField ID="hdnEventPopupRefresh" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnUpdateEventId" runat="server" Value="" ClientIDMode="Static" />
                <script type="text/javascript">
                    function fromDateClicked() {
                        document.getElementById("txtFromDate").focus();
                        //$('#txtFromDate').click();
                    }
                    function toDateClicked() {
                        document.getElementById("txtToDate").focus();
                        //$('#txtFromDate').click();
                    }
                    function showColor() {

                        $("#select-event-color").addClass('display-blockk');
                    }

                    $(document).ready(function () {
                        var ID = "#" + $("#hdnEventPopupRefresh").val();
                        $(ID).focus();
                    });
                    $(document).ready(function () {
                        var prm = Sys.WebForms.PageRequestManager.getInstance();
                        prm.add_endRequest(function () {
                            $('#ctl00_ContentPlaceHolder1_lnkSave').removeClass('disabled');
                            var ID = "#" + $("#hdnEventPopupRefresh").val();
                            $(ID).focus();
                        });
                    });
                </script>
                <script type="text/javascript">
                    function CloseCalPopup() {
                        $('#dvPopup').hide();
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
                </script>
                <%--<script type="text/javascript">
               function DateControl() {
                   $(function () {
                       $('#txtFromDate').datepick({ showTrigger: '#imgFromdate' });
                       $('#txtToDate').datepick({ showTrigger: '#imgTodate' });
                   });
               }
               DateControl();
               $(document).ready(function () {
                   var prm = Sys.WebForms.PageRequestManager.getInstance();
                   prm.add_endRequest(function () {
                       $('#txtFromDate').datepick({ showTrigger: '#imgFromdate' });
                       $('#txtToDate').datepick({ showTrigger: '#imgTodate' });
                   });
               });
            </script>--%>
                <script type="text/jscript">
                    function Cancel() {
                        document.getElementById("divCancelEvent").style.display = 'none';
                        return false;
                    }
                </script>
                <script type="text/jscript">
                    function messageCloseEvent() {
                        document.getElementById("divSuccessEvents").style.display = 'none';
                        return false;
                    }
                </script>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
            <asp:AsyncPostBackTrigger ControlID="lnksucessClose" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function CloseMessage() {
            document.getElementById('divSuccessEvent').style.display = 'none';
        }
    </script>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function Close() {
            document.getElementById('dvPopup').style.display = "none";
        }
        function getSelText1() {
            document.getElementById("txtColorCode").value = document.getElementById("txtFiColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtFiColor").value;
            $("#select-event-color").removeClass('display-blockk');
            // $('.arrow-color-select').addClass('display-none');
        }

        function getSelText2() {

            document.getElementById("txtColorCode").value = document.getElementById("txtSeColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtSeColor").value;
            $("#select-event-color").removeClass('display-blockk');
            // $('.arrow-color-select').addClass('display-none');
        }
        function getSelText3() {

            var a = document.getElementById("txtthColor").value;;
            document.getElementById("txtColorCode").value = document.getElementById("txtthColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtthColor").value;
            $("#select-event-color").removeClass('display-blockk');
            // $('.arrow-color-select').addClass('display-none');
        }
        function getSelText4() {

            document.getElementById("txtColorCode").value = document.getElementById("txtFoColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtFoColor").value;
            $("#select-event-color").removeClass('display-blockk');
            //$('.arrow-color-select').addClass('display-none');
        }
        function getSelText4() {

            document.getElementById("txtColorCode").value = document.getElementById("txtFoColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtFoColor").value;
            $("#select-event-color").removeClass('display-blockk');
            //$('.arrow-color-select').addClass('display-none');
        }
        function getSelText5() {

            document.getElementById("txtColorCode").value = document.getElementById("txtFivColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtFivColor").value;
            $("#select-event-color").removeClass('display-blockk');
            // $('.arrow-color-select').addClass('display-none');
        }
        function getSelText6() {

            document.getElementById("txtColorCode").value = document.getElementById("txtSiColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtSiColor").value;
            $("#select-event-color").removeClass('display-blockk');
            //$('.arrow-color-select').addClass('display-none');
        }
        function getSelText7() {

            document.getElementById("txtColorCode").value = document.getElementById("txteSeColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txteSeColor").value;
            $("#select-event-color").removeClass('display-blockk');
            // $('.arrow-color-select').addClass('display-none');
        }
        function getSelText8() {

            document.getElementById("txtColorCode").value = document.getElementById("txtEigColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtEigColor").value;
            $("#select-event-color").removeClass('display-blockk');
            //$('.arrow-color-select').addClass('display-none');
        }
        function getSelText9() {

            document.getElementById("txtColorCode").value = document.getElementById("txtNiColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtNiColor").value;
            $("#select-event-color").removeClass('display-blockk');
            // $('.arrow-color-select').addClass('display-none');
        }
        function getSelText10() {

            document.getElementById("txtColorCode").value = document.getElementById("txtTenColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtTenColor").value;
            $("#select-event-color").removeClass('display-blockk');
            //$('.arrow-color-select').addClass('display-none');
        }
        function getSelText11() {

            document.getElementById("txtColorCode").value = document.getElementById("txtEleColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtEleColor").value;
            $("#select-event-color").removeClass('display-blockk');
            //$('.arrow-color-select').addClass('display-none');
        }
        function getSelText12() {

            document.getElementById("txtColorCode").value = document.getElementById("txtTweColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtTweColor").value;
            $("#select-event-color").removeClass('display-blockk');
            //$('.arrow-color-select').addClass('display-none');
        }
        function getSelText13() {

            document.getElementById("txtColorCode").value = document.getElementById("txtThiColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtThiColor").value;
            $("#select-event-color").removeClass('display-blockk');
            //$('.arrow-color-select').addClass('display-none');
        }
        function getSelText14() {

            document.getElementById("txtColorCode").value = document.getElementById("txtFouColor").value;
            document.getElementById("TextBox1").style.backgroundColor = document.getElementById("txtFouColor").value;
            $("#select-event-color").removeClass('display-blockk');
            //$('.arrow-color-select').addClass('display-none');
        }

    </script>
    <script type="text/javascript">
        function Clear() {
            $('#txtTitle').val("");
            $('#txtDescription').val("");
            $('#txtVenue').val("Venue");
            $('#txtContactPerson').val("Contact person");
            $('#txtContactPerNumber').val("Contact person number");
            $('#txtFromDate').val("From Date");
          //  $('#txtToDate').val("To Date");
            $('#txtColorCode').val("");
            $('#ddlPriorityType').val(0);
            $("#TextBox1").css("background-color", "");
            $("#TextBox1").val("");
            $('#hdnUpdateEventId').val("");
            $('#lblDateMessage').val("");
            $('#lblDateMessage1').val("");
            document.getElementById('lblDateMessage').style.display = "none";
            document.getElementById('lblDateMessage1').style.display = "none";
        }
        $(document).on('click', '.mobile_tab_icon', function () {
            $('ul.group_p').slideToggle('slow');
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtTitle').keypress(function () {
                $('#txtTitle').css('font-weight', 'normal');
            });
            $('#txtDescription').keypress(function () {
                $('#txtDescription').css('font-weight', 'normal');
            });
            $('#txtVenue').keypress(function () {
                $('#txtVenue').css('font-weight', 'normal');
            });
            $('#txtContactPerson').keypress(function () {
                $('#txtContactPerson').css('font-weight', 'normal');
            });
            $('#txtContactPerNumber').keypress(function () {
                $('#txtContactPerNumber').css('font-weight', 'normal');
            });
            $('#txtFromDate').keypress(function () {
                $('#txtFromDate').css('font-weight', 'normal');
            });
            $('#txtToDate').keypress(function () {
                $('#txtToDate').css('font-weight', 'normal');
            });
        });

        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#txtTitle').keypress(function () {
                    $('#txtTitle').css('font-weight', 'normal');
                });
                $('#txtDescription').keypress(function () {
                    $('#txtDescription').css('font-weight', 'normal');
                });
                $('#txtVenue').keypress(function () {
                    $('#txtVenue').css('font-weight', 'normal');
                });
                $('#txtContactPerson').keypress(function () {
                    $('#txtContactPerson').css('font-weight', 'normal');
                });
                $('#txtContactPerNumber').keypress(function () {
                    $('#txtContactPerNumber').css('font-weight', 'normal');
                });
                $('#txtFromDate').keypress(function () {
                    $('#txtFromDate').css('font-weight', 'normal');
                });
                $('#txtToDate').keypress(function () {
                    $('#txtToDate').css('font-weight', 'normal');
                });

            });
        });
    </script>
    <script type="text/javascript">
        // function showpopevnt(str, GrpEventId, intGroupId) {
        //     $.ajax({
        //         type: "POST",
        //         url: "group-event-main.aspx/getData",
        //         data: "{frmDate:'" + str + "',EventId:'" + GrpEventId + "',intGroupId:'" + intGroupId + "'}",
        //         contentType: "application/json; charset=utf-8",
        //         datatype: "jsondata",
        //         async: "true",
        //         success: function (response) {
        //             var msg = eval('(' + response.d + ')');
        //             var table = '<div id="" class="eventpoparroww"></div>';
        //             //<img src="images/eventpoparrow.png" />
        //             for (var i = 0; i <= (msg.length); i++) {
        //                 table += '<div class="eventPopDetails"><div id="dvcolor" class="color1Pop" style="background-color:' + msg[i].strColor + '"> </div> <div class="headingPop heading-tooltip"> <label id="lblTitle">' + msg[i].strTitle + '</label></div>  <div class="eventDtl event-tooltip-des"><label id="strDescription">' + msg[i].strDescription + '</label></div><div class="eventTiming event-tooltip-timing"><label id="dtFromDates" >' + msg[i].dtFromDate + ' - ' + msg[i].dtTodate + '</label></div></div><div class="cls"></div>';
        //                 $('#eventPop').html(table);
        //             }
        //         }
        //     });

        //     document.getElementById('eventPop').style.display = "block";
        // }
        function hidepopevnt() {
            document.getElementById('eventPop').style.display = "none";
        }
        window.onmousemove = function (e) {
            $("#eventPop").offset({ right: e.pageX, top: e.pageY + 30 });
            $("#eventpoparrows").offset({ left: e.pageX, top: e.pageY + 15 });
        }
    </script>
    <script type="text/javascript">
        function delSet() {
                $('#hdnDeletePostQuestionID').val($(this).children('#hdnintPostQuestionIdelet').val());
                $('#hdnstrQuestionDescription').val($(this).children('#lnkstrQuestionDescription').val());
            }

        $(document).ready(function () {
            $('#divCancelEvent').center();
            $('#divSuccessEvents').center();
            //$('.overout').mouseout(function () {
            //    document.getElementById('eventPop').style.display = "none";
            //});

            
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                //$('.overout').mouseout(function () {
                //    document.getElementById('eventPop').style.display = "none";
                //});
                //function delSet() {
                //    $('#hdnDeletePostQuestionID').val($(this).children('#hdnintPostQuestionIdelet').val());
                //    $('#hdnstrQuestionDescription').val($(this).children('#lnkstrQuestionDescription').val());
                //}
            })
        });
    </script>
    <script type="text/javascript">
        function docdelete() {
            $('#divDeletesucess').css("display", "block");
        }
        function divCancels() {
            $('#divDeletesucess').css("display", "none");
        }
        function CallSaveEvent() {
            var that = this;
            $(this).addClass('disabled');
            $('#ctl00_ContentPlaceHolder1_lnkSave').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtTitle').text() == '' || $('#txtFromDate').text() == '' || $('#txtToDate').text() == '') {
                setTimeout(
                    function () {
                        $(that).removeClass('disabled');
                    }, 1000);
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("span.spEditPoll").click(function () {
                $(this).children('#lnkEditEvent').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $("span.spDeletePoll").click(function () {
                $(this).children('#lnkDeleteEvent').css("box-shadow", "0px 0px 5px #00B7E5");
            });
            $('#txtTitle').keypress(function (e) {
                if (e.keyCode == 13) {
                    return false;
                }
            });
            $('#lnkDeleteConfirm').click(function (e) {
                $(this).css("box-shadow", "0px 0px 5px #00B7E5");
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("span.spEditFolder").click(function () {
                    $(this).children('#lnkEditEvent').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $("span.spDeleteFolder").click(function () {
                    $(this).children('#lnkDeleteEvent').css("box-shadow", "0px 0px 5px #00B7E5");
                });
                $('#txtTitle').keypress(function (e) {
                    if (e.keyCode == 13) {
                        return false;
                    }
                });
                $('#lnkDeleteConfirm').click(function (e) {
                    $(this).css("box-shadow", "0px 0px 5px #00B7E5");
                });
            });
        });
    </script>
    <script type="text/javascript">
        function DateControlInit() {
         //$.fn.datepicker.defaults.format = "mm-M-yyyy";
         <%--if ($('#rdVotingNeverEnds').is(':checked')) {
             document.getElementById('<%= txtFromDate.ClientID %>').disabled = true;
         }--%>
            $("#txtFromDate, #txtToDate").datepicker({
                //debugger;
                autoclose: true,
                format: 'dd-M-yyyy',
                todayHighlight: true
            })
        }
        $(document).ready(function () {
            DateControlInit();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                DateControlInit();
            });
        })

    </script>
</asp:Content>
