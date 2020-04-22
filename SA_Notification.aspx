<%@ Page Title="" Language="C#" MasterPageFile="~/Main_Super.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="SA_Notification.aspx.cs" Inherits="SA_Notification" %>
<%@ MasterType TypeName="SuperMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlMsg" runat="server">
        <ContentTemplate>
              <asp:HiddenField ID="hdnNotificationIDGlo" runat="server" Value="false" />
            <div class="main-section-inner">
                    
                    <div class="panel-cover clearfix">
                        <div class="full-width-con super-admin">   
                            <div class="super-admin-comp">
                             <div class="btn-title-con s-journal-heading m-t-15-minus">
                            <div><h5 class="card-title ">Notifications</h5></div>
                            <div class="">
                                <ul class="list-inline">
                                    <li class="list-inline-item">
                                          <asp:LinkButton ID="lnkAddNotification" OnClick="lnkAddNotification_Click" runat="server" CssClass="btn btn-outline-primary"> Send Notification</asp:LinkButton>
                                    </li>
                               </ul>
                                
                                <div class="modal fade show  backgroundoverlay" runat="server" id="docModal1" >
                                    <div class="modal-dialog modal-dialog-centered" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                             <h5 class="modal-title" id="exampleModalLabel">Send Notification</h5>
                                             <asp:LinkButton CssClass="close" ID="btnCross" runat="server" OnClick="btnCancel_Click" OnClientClick="javascript:callClose();">
                                              <span aria-hidden="true">×</span>
                                             </asp:LinkButton>
                                            </div>
                                            <div class="modal-body text-left ">
                                               
                                                <asp:UpdatePanel ID="upAddNotifaction" runat="server">
                                                    <ContentTemplate>
                                                    <div class="form-group">

                                                    

                                                     <label for="textbox">Notification</label>
                                                    <%-- <asp:TextBox ID="txtNotification" autocomplete="off" MaxLength="500" runat="server" CssClass="form-control" ValidationGroup="SANotification"  placeholder="Enter Notification"></asp:TextBox>--%>
                                                        <textarea maxlength="500" rows="5" class="form-control" lenght="500"  clientidmode="Static" id="txtNotification" onchange="ClearMsg();"  placeholder="Enter Notification here..." runat="server"></textarea>
                                                                     
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNotification"
                                                      Display="Dynamic" ValidationGroup="SANotification" ErrorMessage="Please enter Notification." ForeColor="Red">
                                                     </asp:RequiredFieldValidator>

                                                    </div>

                                                   
                                                    <div class="submit-con">
                                                        <asp:LinkButton ID="btnCancel"  OnClick="btnCancel_Click" runat="server" class="btn btn-outline-primary m-r-15">Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="btnSave" ValidationGroup="SANotification" OnClick="btnSave_Click" ClientIDMode="Static"  OnClientClick="javascript:CallPostques();" runat="server" class="btn btn-primary">Send</asp:LinkButton>
                                                    </div>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                              <%--  </form>--%>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <!-- Modal ended -->
                                
                            </div>
                        </div><!-- btn-title-con ended -->
                            </div><!-- super-admin-comp ended -->
                            <asp:UpdatePanel ID="upNotifications" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdnNotificationId" Value="" ClientIDMode="Static" runat="server" />
                                        
                                    <asp:ListView ID="lstNotification" OnItemDataBound="lstNotification_ItemDataBound"  OnItemCommand="lstNotification_ItemCommand" runat="server">
                                        <ItemTemplate>
                                            <div class="card card-list-con">
                                                <div class="list-group-item top-list">
                                                    <div class="post-con">
                                                        <div class="post-header justify-content-between">
                                                            <div>
                                                                <span class="question-icon">
                                                                    <span class="icon">
                                                                        <span class="lnr lnr-alarm"></span>
                                                                      </span>
                                                                </span>
                                                                <ul class="que-con">
                                                                    <li>Notification</li>
                                                                </ul>
                                                            </div>
                                                            <div class="dropdown material-toggle more-btn super-admin-comp">

                            
                                                                  
                                            <asp:placeholder id="plcDeletePost" runat="server">          
                                                   <a href="#" id="dropdownMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <img src="images/more.svg" alt="" class="more-btn" />
                                                   </a>
                                                   <span id="spdeletepost" onclick="setPostId.bind(this)()" runat="server" class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                                        <asp:HiddenField ID="hdnintNotificationID" Value='<%# Eval("Notification_ID") %>' ClientIDMode="Static" runat="server" />
                                                        <asp:HiddenField ID="hdnstrNotificationDetail" Value='<%# Eval("Notification_Detail") %>' ClientIDMode="Static" runat="server" />
                                                      <asp:LinkButton ID="lnkDeletePost" runat="server" Font-Underline="false" Visible="true" ClientIDMode="Static"
                                                         ToolTip="Delete Post" class="dropdown-item" Text="" CommandName="Delete Post" CausesValidation="false"
                                                         OnClientClick="javascript:Commentsdelete();return false;">
                                                         <span class="lnr lnr-trash"></span> Delete
                                                     </asp:LinkButton>
                                                 </span>
                                        </asp:placeholder>


                                                           
                                                            </div>
                                                        </div>
                                                        <div class="post-body pb-3">
                                                            <p class="pb-1">
                                                              <asp:label runat="server" Font-Underline="false" CssClass="commentQA remove-anchor moreQA" Id="lnkNotification" CommandName="Details" ><%#Eval("Notification_Detail") %></asp:label>
                                                             </p>   
                                                               <asp:Label ID="lblDate" runat="server" Text='<%#Eval("dateaddedon") %>' class="small-date"></asp:Label>
                                                            
                                                        </div>
                                                        <!-- post-body ended -->

                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <!-- card ended -->
                                     <div class="clearfix"></div>
                                            <div class="pagination_main_div">
                                             <nav aria-label="Page navigation example">
                                              <ul id="dvPage" runat="server" class="pagination" clientidmode="Static">
                                               <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click" ClientIDMode="Static" class="page-link">
                              <%--<img id="imgPaging" runat="server" src="images/backpaging.jpg" clientidmode="Static"
                                  alt="" class="opt" style="display: none;" />--%>
                                                <span class="lnr lnr-chevron-left">

                                               </asp:LinkButton>
                                                    
                                                    <%--<asp:LinkButton ID="lnkprev" runat="server" class="page-link" OnClientClick="return false;" ClientIDMode="Static">
                                                        <span class="lnr lnr-chevron-left"></span>
                                                    </asp:LinkButton>--%>
                                                    
                                                    <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand" OnItemDataBound="rptDvPage_ItemDataBound">
                                                        <ItemTemplate>
                                                            <li class="page-item">
                                                            <asp:LinkButton ID="lnkPageLink" runat="server" ClientIDMode="Static" CommandName="PageLink" class="page-link" Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                                                          </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                      <asp:LinkButton ID="lnkNext" runat="server" class="page-link" OnClick="lnkNext_Click" ClientIDMode="Static"> <span class="lnr lnr-chevron-right"></span> </asp:LinkButton>
                                                    <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                                                    <asp:HiddenField ID="hdnEndPage" runat="server" ClientIDMode="Static" />
                                                </ul>
                                              </nav>  
                                            </div>
                                            <!--pagination end-->
                                             </div>  
                                </ContentTemplate>
                            </asp:UpdatePanel>   
                        </div>
                        <!-- full-width-con ended -->
                    </div>
                    <!-- panel-cover ended -->
                  
                </div><!-- main-section inner ended -->
                
            <!-- main-section-inner ended -->

             <div id="divSuccess" runat="server" style=" display: none;" class="modal backgroundoverlay"  clientidmode="Static">
                <div class=" modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                    <div class="modal-header">
                            <h5 class="modal-title">Success </h5>
                        </div>
                        <div class="modal-body">
                                          
                            <asp:Label ID="lblSuccess"  runat="server" Text="Notification sent successfully."></asp:Label>
                                          
                            </div>
                    <div class="modal-footer border-top-0">
                        <a href="SA_Notification.aspx" class="add-scroller a-tag-colcor btn btn-outline-primary">Close</a>
                    </div>
                            </div>
                </div>
             </div>
             <div id="divSuccessNoti" runat="server" class="modal backgroundoverlay" clientidmode="Static" style="display: none;">
              <div class="modal-dialog modal-dialog-centered">
               <div class="modal-content">
                <div class="modal-header">
                 <h5 class="modal-title">Success  
                 </h5>
                </div>
                <div class="modal-body">
                 <asp:Label ID="Label1" runat="server" Text="Successfully deleted."> </asp:Label>
                </div>   
                <div class="modal-footer border-top-0">
                 <asp:Button ClientIDMode="Static" class="btn btn-primary add-scroller" id="btnOk" runat="server" Text="Ok" OnClientClick="javascript:CloseMsg(); return false;"></asp:Button><%--OnClick="btnOk_click"--%>
                </div>
               </div>
              </div>
             </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" >
        $(document).ready(function () {
        var showChar = 150;
        var ellipsestext = "...";
        var moretext = "Read more";
        var lesstext = "Less";
            $('.moreQA').each(function () {
                var content = $(this).html();
                if (content.length > showChar) {
                    var c = content.substr(0, showChar);
                    var h = content.substr(showChar - 1, content.length - showChar);
                    var html = c + '<span class="moreelipses">' + ellipsestext + '</span>&nbsp;<span class="morecontent"><span style="display:none;">' + h + '</span>&nbsp;&nbsp;<a href="" class="morelinkQA">' + moretext + '</a></span>';
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
         });

        function ClearMsg() {
        $('#ctl00_ContentPlaceHolder1_lblSuMess').text('')
        }


          function CallPostques() {
              
             // $('#divSaveimage').css("display", "block");
             // $('#lnkcreateForum').css("text-shadow", "0px 0px 1px #00B7E5");
             // $('#lnkcreateForum').css("color", "#00A5AA");
              
              if ($('#txtNotification').val() == '') {
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

        function divdeletes() {
          $(".divProgress").css("display", "none");
          $('#divDeletesucess').css("display", "none");
        }

          function setPostId() {
             $('#hdnNotificationId').val($(this).children('#hdnintNotificationID').val());
               
       }

           function OverlayBody() {
            $('#bodyelement').addClass("remove-scroller");
        }
        function Commentsdelete() { 
        $('#divDeletesucess').modal('show'); 
       }
          function callClose() {
             $('#txtNotification').val('').trigger('chosen:updated');
             
         }
        function RemoveOverlay() {
            $('#bodyelement').removeClass("remove-scroller");
        }
        function ShowAddJournal() {
            $('#docModal1').show();
        }

        $('.selectedOption').click(function(){
              $(this).toggleClass('down-arrow'); 
        });
          if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                //$('#lnkNextshow').css("display", "block");
                $('#lnkNext').hide();
        }

        $("#dvPage a").on('click', function(event){showLoader1();});
           var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $("#dvPage a").on('click', function(event){showLoader1();});
            if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                //$('#lnkNextshow').css("display", "block");
                $('#lnkNext').hide();
            }
        });


    </script>

    <div id ="divDeletesucess" class="modal hide" role="dialog" aria-labelledby="confirmationTitle" aria-hidden="true" >
    <div id="divDeleteConfirm" runat="server" class ="modal-dialog modal-dialog-centered" role="document">
     <div class="modal-content">

      <div class="modal-header">
       <h5 class="modal-title" id="confirmationTitle">Delete Confirmation</h5>
<!--        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
        <span aria-hidden="true">&times;</span>
       </button> -->
      </div>
      <div class="modal-body">
       <asp:Label ID="lblConnDisconn" runat="server" Text="Do you want to delete?"></asp:Label>     
      </div>
      <div class="modal-footer border-top-0">
       
      <asp:LinkButton ID="lnkDeleteCancel" runat="server" class="btn add-scroller btn-outline-primary m-r-15" data-dismiss="modal" ClientIDMode="Static" Text="Cancel"
       OnClientClick="javascript:divCancels();return false;">
      </asp:LinkButton> 
      <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" class="btn btn-primary" OnClientClick="divdeletes();" 
       OnClick="lnkDeleteConfirm_Click">
      </asp:LinkButton>       
     </div>
    </div> 
    </div>
   </div>
   <script type="text/javascript">
    function divCancels() {         
          $('#divDeletesucess').css("display", "none");          
       }
    function CloseMsg() {
          $('#divSuccessNoti').css("display", "none");
      }
  </script>

</asp:Content>
