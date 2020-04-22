<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Inbox.aspx.cs" Inherits="Inbox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlMsg" runat="server">
        <ContentTemplate>
            <div class="main-section-inner">
                <div class="panel-cover clearfix">
                    <div class="full-width-con">
                        <div class="">
                            <div class="custom-nav-con messaging-tab">
                                <!-- Nav tabs -->
                                <ul class="custom-nav-control nav nav-tabs ">
                                    <li class="nav-item">
                                        <asp:LinkButton ID="lnkInbox" runat="server" class="nav-link active font-weight-bold" OnClick="lnkInbox_Click">Inbox</asp:LinkButton></li>
                                    <li class="nav-item">
                                        <asp:LinkButton ID="lnkOutBox" runat="server" OnClick="lnkOutBox_Click" class="nav-link font-weight-bold">Sent</asp:LinkButton>
                                    </li>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content">
                                    <div class="custom-nav-container tab-pane container active" id="sent">
                                        <div class="inbox_style card">
                                            <asp:UpdatePanel ID="upOut" runat="server">
                                                <ContentTemplate>
                                                    <asp:ListView ID="lstInbox" runat="server" OnItemDataBound="lstInbox_ItemDataBound" OnItemCommand="lstInbox_ItemCommand">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnGrpId" runat="server" Value='<%#Eval("intGroupId") %>' />
                                                            <asp:HiddenField ID="hdnInviUserID" runat="server" Value='<%#Eval("striInvitedUserId") %>' />
                                                            <asp:HiddenField ID="hdnAddedBy" runat="server" Value='<%#Eval("intAddedBy") %>' />
                                                            <asp:HiddenField ID="hdnMaxCount" runat="server" Value='<%#Eval("Maxcount") %>' />
                                                            <asp:HiddenField ID="hdnMessId" runat="server" Value='<%#Eval("intMessageId") %>' />
                                                            <asp:HiddenField ID="hdnIsRead" runat="server" Value='<%#Eval("IsRead") %>' />
                                                            <asp:HiddenField ID="hdnIsReadSent" runat="server" Value='<%#Eval("IsReadSent") %>' />
                                                            <asp:HiddenField ID="hdnSubjectmsg" runat="server" Value='<%#Eval("Subjectmsg") %>' />
                                                            <asp:HiddenField ID="hdnstrTotalGrpMemberID" runat="server" Value='<%#Eval("strTotalGrpMemberID") %>' />
                                                            <asp:UpdatePanel ID="upinbox" runat="server">
                                                                <ContentTemplate>
                                                                    <div id="divCard" runat="server" class="msg-card expand-card d-flex">
                                                                        <div class="un-read"></div>
                                                                        <div class="w-100">
                                                                            <div class="d-flex align-items-center justify-content-between header-con">
                                                                                <div class="d-flex algn-center logo-with-text">
                                                                                    <img src="images/profile-photo.png" alt="" class="rounded-circle message-icon" />
                                                                                    <span class="text-con">
                                                                                        <asp:Label class="un-anchor" ID="lblSenderName" Style="text-decoration: none;" runat="server" ClientIDMode="Static" Text='<%#Eval("NAME") %>'></asp:Label>
                                                                                    </span>
                                                                                </div>
                                                                                <div class="date">
                                                                                    <asp:LinkButton class="un-anchor" ID="lblDate" Style="text-decoration: none;" runat="server" ClientIDMode="Static" CommandName="GetMessageSetails" Text='<%#Eval("dtAddedOn") %>'></asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                            <!-- d-flex ended -->
                                                                            <h4>
                                                                                <asp:LinkButton class="un-anchor cursor-pointer display-block hover-a-tag" ID="lnksubject" runat="server" ClientIDMode="Static" CommandName="GetMessageSetails" Text='<%#Eval("strSubject") %>'></asp:LinkButton>
                                                                            </h4>
                                                                            <div id="dvRecom" style="display: none;" runat="server">
                                                                                <p><%# Eval("StrRecommendation") %></p>
                                                                            </div>
                                                                            <asp:LinkButton ID="btnReplyMess" Visible="false" clinetidmode="Static" runat="server" Text="Reply"  CommandName="DoReply"  CssClass="connectLink default_btn hide-body-scroll margin_right_l"></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <!-- card ended -->
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="lnksubject" />
                                                                    <asp:AsyncPostBackTrigger ControlID="lblDate" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="lblMessage" ForeColor="Red" Text="No message found." class="padding-15 inline-block" runat="server"></asp:Label>
                                                        </EmptyDataTemplate>
                                                    </asp:ListView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <!-- custom-nav-container ended -->
                                </div>
                                <!-- tab-content ended -->
                            </div>
                        </div>
                        <div class="innerDocumentContainer list_style" id="dvInBox" runat="server">
                            <asp:UpdatePanel ID="upmains" runat="server">
                                <ContentTemplate>
                                    <div id="divSuccessMess" runat="server" class=" modal_bg" style="display: none;" clientidmode="Static">
                                        <div class="modal-dialog">
                                            <div class="modal-header">
                                                <strong>
                                                    <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
                                                </strong>
                                            </div>
                                            <div class="modal-footer">
                                                <a clientidmode="Static" class="cancel_btn margin_right_l" causesvalidation="false" onclick="PopClose();">Close </a>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="dvPage" runat="server" class="pagination_main_div" align="center" clientidmode="Static">
                                        <nav class="Page navigation">  
                                            <ul class="pagination">  
                                              
                                                <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click" ClientIDMode="Static" class="">     
                                                <span id="imgPaging" runat="server" clientidmode="Static" class="lnr lnr-chevron-left page-link"></span>
                                                    
                                                </asp:LinkButton>
                                               
                                                <li class="page-item display-none">
                                                <asp:LinkButton ID="lnkprev" runat="server" OnClientClick="return false;" ClientIDMode="Static" Style="display: none;">
                                                    <img id="img2" runat="server" src="images/backpaging.jpg" clientidmode="Static" class="opt" />
                                                </asp:LinkButton>
                                                </li>
                                                <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand" OnItemDataBound="rptDvPage_ItemDataBound">
                                                    <ItemTemplate>
                                                        <li class="page-item">
                                                            <asp:LinkButton ID="lnkPageLink" class="page-link" runat="server" ClientIDMode="Static" CommandName="PageLink" Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <li class="page-item">
                                                <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click" class="page-link" ClientIDMode="Static">
                                                    <span class="lnr lnr-chevron-right"></span>
                                                </asp:LinkButton>
                                                </li>
                                                <li class="page-item display-none">
                                                <asp:LinkButton ID="lnkNextshow" runat="server" OnClientClick="return false;" Style="display: none;" ClientIDMode="Static"><img class="opt" src="images/nextpaging.jpg" />
                                                </asp:LinkButton>
                                                </li>
                                                <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" ClientIDMode="Static" Value="1" />
                                                <asp:HiddenField ID="hdnEndPage" runat="server" ClientIDMode="Static" />
                                            </ul>
                                        </nav>    
                                    </div>
                                    <div style="display:none;">
                                    <asp:UpdateProgress ID="updateProgress" runat="server">
                                        <ProgressTemplate>
                                            <div class="loader_home_gif">
                                                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/Loadgif.gif" AlternateText="Loading ..." ToolTip="Loading ..." class="divProgress loader_loading_p" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                            <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
                        </div>
                        <div class="inbox_msz">
                            <div class="NmiddleContainer width_h">
                            </div>
                            <!--middle container ends-->
                            <div class="adv" style="display: none">
                                <!--right side banner starts-->
                                <img src="images/adv1.jpg" />
                                <br />
                                <br />
                                <img src="images/adv2.jpg" />
                                <!--right side banner ends-->
                            </div>
                        </div>
                    </div>
                    <!-- fullwidth-con ended -->
                </div>
                <!-- panel-cover ended -->
            </div>
            <!-- main-section-inner ended -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(".msg-card").click(function () {
            $(this).addClass("expand-card");
        });
        $(document).ready(function () {
            if ($('#hdnCurrentPage').val() == '1') {
                $('#imgPaging').css("display", "none");
                $('#lnkprev').css("display", "block");
            } else {
                $('#imgPaging').css("display", "block");
                $('#lnkprev').css("display", "none");
            }
            if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                $('#lnkNextshow').css("display", "block");
                $('#lnkNext').css("display", "none");
            }
            $('#lnkPageLink').click(function () {
                $(this).removeClass('Paging');
                $(this).addClass('ahover');
            });
            $("#dvPage a").on('click', function(event){showLoader1();});
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $("#dvPage a").on('click', function(event){showLoader1();});
                hideLoader1();
                if ($('#hdnCurrentPage').val() == '1') {
                    $('#imgPaging').css("display", "none");
                    $('#lnkprev').css("display", "block");
                } else {
                    $('#imgPaging').css("display", "block");
                    $('#lnkprev').css("display", "none");
                }
                if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                    $('#lnkNextshow').css("display", "block");
                    $('#lnkNext').css("display", "none");
                }
                $('#lnkPageLink').click(function () {
                    $(this).removeClass('Paging');
                    $(this).addClass('ahover');
                });
            });
        });
    </script>
    <script type="text/javascript">
        function PopClose() {
            //document.getElementById("dvPopup").style.display = 'none';
            document.getElementById("divSuccessMess").style.display = 'none';
        }
        function CallSendMessage() {
          $('#lnkSendMess').css("box-shadow", "0px 0px 5px #00B7E5");
          //$('#lnkSendMess').addClass('disabled');

          if ($('#txtSubject').val() != '' && $('#txtMessage').val() != '') {
               showLoader1();
           }

          $('#lnkSendMess').css("box-shadow", "0px 0px 5px #00B7E5");
          //$('#lnkSendMess').addClass('disabled');
            if ($('#txtSubject').text() == '' || $('#txtMessage').text() == '') {
                setTimeout(
                    function () {
                        //$('#lnkSendMess').removeClass('disabled');
                        $('#lnkSendMess').css("box-shadow", "0px 0px 0px #00B7E5");

                    }, 1000);
            }
        }
        function divCancels() {
          //debugger;
          $('#dvPopup').css("display", "none");
          $('#divDeleteConfirm').css("display", "none");
          $('#txtSubject').val('');
          $('#txtMessage').val('');
        }
    </script>
    <asp:UpdatePanel ID="h" runat="server">
        <ContentTemplate>
            <div id="dvPopup" class="modal backgroundoverlay" clientidmode="Static" runat="server"
                style="display: none;">
                <asp:UpdatePanel ID="upasss" runat="server" class="modal-dialog modal-dialog-centered">
                    <ContentTemplate>
                        <div id="divDeleteConfirm" runat="server" class="w-100" clientidmode="Static">
                            <div class="modal-content">
                                <div>
                                    <b>
                                        <asp:Label ID="lbl" runat="server"></asp:Label>
                                    </b>
                                </div>
                                <div class="modal-header">
                                    <h5 class="modal-title">Message
                                    </h5>
                                </div>
                                <div class="modal-body">
                                    <div class="inbox_popup">
                                        <p id="MessTo" runat="server" class="text-left" clientidmode="Static">
                                            <strong>To:</strong>
                                            <asp:Label ID="lblTo" class="lblto" runat="server"></asp:Label>
                                        </p>
                                        <p id="MessFrom" runat="server" style="display: none;" clientidmode="Static">
                                            <strong>From:</strong>
                                            <asp:Label ID="lblFrom" class="lblfrm" runat="server"></asp:Label>
                                        </p>
                                        <p id="pMessageSubBox" clientidmode="Static">
                                            <asp:TextBox ID="txtSubject" MaxLength="100" autocomplete="off" ClientIDMode="Static" CssClass="MessageSubjectInbox form-control" placeholder="Subject" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" class="lbl_error_me" ControlToValidate="txtSubject" ErrorMessage="Please enter subject." ValidationGroup="Mess"></asp:RequiredFieldValidator>
                                        </p>
                                        <p id="pMessageBox" clientidmode="Static">
                                            <textarea id="txtMessage" maxlength="500" runat="server" clientidmode="Static" cols="20" rows="2" placeholder="Message" class="MessageBodyInbox form-control" style=""></textarea>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="margin-left: 0px;" ControlToValidate="txtMessage" Display="Dynamic" ValidationGroup="Mess" ErrorMessage="Please enter message." ForeColor="Red"></asp:RequiredFieldValidator>
                                            <%-- </strong>--%>
                                        </p>
                                    </div>
                                </div>
                                <div class="modal-footer border-top-0">
                                    <asp:Button ID="lnkDeleteCancel" runat="server" class="add-scroller btn btn-outline-primary m-r-15" ClientIDMode="Static" Text="Cancel"
                                        OnClientClick="javascript:divCancels();"></asp:Button>
                                    <asp:Button ID="lnkDeleteConfirm" ValidationGroup="Mess" runat="server" ClientIDMode="Static" Text="Send" OnClick="lnkPopupOK_Click"
                                        OnClientClick="javascript:CallSendMessage();" CssClass="btn btn-primary"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
