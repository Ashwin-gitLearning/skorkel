<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
   CodeFile="Notifications_Details_2.aspx.cs" Inherits="Notifications_Details_2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--inner container ends-->    
    
    <div class="main-section-inner">
     <div class="panel-cover clearfix">
      <div class="full-width-con">      
               
     <div class="innerContainer notification_page">
      <!--middle container starts-->
      <div class="NmiddleContainer width_h">
       <asp:UpdatePanel ID="upnotification" runat="server">
        <ContentTemplate>            
                
               <div id="divSuccess"  class="modal backgroundoverlay" runat="server" style="
                  display: none; " clientidmode="Static">
                  <div class="modal-dialog modal-dialog-centered">
                     <div class="modal-content">   
                       <div class="modal-header">
                          <h5 class="modal-title">Success</h5>
                       </div>
                       <div class="modal-body">
                         <asp:Label ID="lblSuccess" runat="server" Text="Do you want to Decline?"></asp:Label>
                       </div> 

                       <div class="modal-footer border-top-0">
                           <a href="#" clientidmode="Static" CausesValidation="false" 
                             onclick="javascript:Cancel();return false;" class="btn add-scroller btn-outline-primary m-r-15" >Close </a>
                          <asp:LinkButton ID="lnkCancels" CssClass="btn btn-primary add-scroller" CausesValidation="false" runat="server"
                             Text="Ok" OnClick="lnkCancels_Click" OnClientClick="showLoader1();"></asp:LinkButton>
                         
                       </div>
                     </div>
                  </div>
               </div>
                
               <div id="divheight" runat="server" class="height_auto">
                    
                  <asp:Label ID="lblMessage" runat="server"></asp:Label>
                  <asp:ListView ID="lstMainMyTag" runat="server" OnItemDataBound="lstMainMyTag_ItemDataBound"
                     OnItemCreated="lstMainMyTag_ItemCreated">
                     <ItemTemplate>
                        <div class="list_style position_relative">
                          <div class="activity-cover notification-con">
                              <asp:panel id ="pnlDateAdded" Visible="false" runat="server">
                            <span  class="date-clip" runat="server"><span><asp:Label ID="lblAddedOn" Text='<%#Eval("dtAddedOn") %>' runat="server" visible="true"></asp:Label></span>
                            </span></asp:panel>

                              <asp:ListView ID="lstChildMyTag" runat="server" OnItemCommand="lstChildMyTag_ItemCommand"
                              OnItemDeleting="lstChildMyTag_ItemDeleting" DataKeyNames="Id">
                              <ItemTemplate>
                                  
                               <div class="card card-list-con padding-15">       
                                  <div class="list-group-item top-list border-0">
                                     <div class="post-con">
                                                   
                                 <asp:HiddenField ID="hdnPkId" Value='<%# Eval("Id") %>' runat="server" />
                                 <asp:HiddenField ID="hdnRegID" Value='<%# Eval("intRegistrationId") %>' runat="server" />
                                 <asp:HiddenField ID="hdnintUserTypeId" runat="server" Value='<%#Eval("intUserTypeId") %>' />
                                 <asp:HiddenField ID="hdnrequserid" Value='<%# Eval("intInvitedUserId") %>' runat="server" />
                                 <asp:HiddenField ID="hdnTableName" Value='<%# Eval("strTableName") %>' runat="server" />
                                 <asp:HiddenField ID="hdnShareInvitee" Value='<%# Eval("strInvitee") %>' runat="server" />
                                 <asp:HiddenField ID="hdnStrRecommendation" Value='<%# Eval("StrRecommendation") %>' runat="server" />
                                 <asp:HiddenField ID="hdnIsAccept" Value='<%# Eval("IsAccept") %>' runat="server" />
                                 <asp:HiddenField ID="hdnintIsAccept" Value='<%# Eval("intIsAccept") %>' runat="server" />
                                 <div id="SearchRept" runat="server">
                                  
                                    <asp:UpdatePanel ID="updele" runat="server">
                                    <ContentTemplate>
                                    
                                    <div  class="breakallwords width_bw display-none">
                                    
                                    <asp:Label ID="lblComment" Text="" Font-Bold="true" Visible="false" runat="server"></asp:Label>
                                    <asp:LinkButton ID="lnkShareDetail" CommandName="Details" class="linkshareDe_no" runat="server"></asp:LinkButton>
                                    &nbsp;
                                  
                                    </div>
                                        
                                        <div class="media">
                                          <img id="imgIcon" runat="server" class="mr-3" src='<%# Eval("vchrPhotoPath") %>' alt="Generic placeholder image">
                                          <div class="media-body">
                                            <span class="float-right"><asp:Label ID="lblTime" Text= '<%# Eval("AddedTime") %>' class="lblTime" runat="server"></asp:Label></span>
                                            <h5 class="mt-0 no-margin">
                                                <asp:LinkButton ID="lnkName" Text='<%# Eval("Name") %>' Font-Bold="true" CommandName="Details" runat="server">
                                                </asp:LinkButton>
                                            </h5>
                                              <asp:Label ID="lblnotificationname" runat="server"></asp:Label>
                                              <p><asp:Label ID="lblstrMessage" Text='<%# Eval("strMessage") %>' CommandName="Details"
                                          runat="server"></asp:Label></p>
                                              
                                                <asp:LinkButton ID="lnkCancel" CssClass="message btn btn-outline-primary mr-1 m-t-5" CommandName="Delete" CausesValidation="false" runat="server" Text="Decline" OnClientClick="javascript:OpenPopup(); "></asp:LinkButton>
                                          <asp:LinkButton ID="lnkConfirm" CssClass="message message_noti btn btn-primary m-t-5" OnClientClick="showLoader1();" CommandName="Confirm" CausesValidation="false" runat="server" Text="Accept"></asp:LinkButton>
                                          <strong>
                                              <asp:LinkButton Visible="false" ID="lnkConnected" OnClientClick="return false;" CausesValidation="false"
                                       class="linkconnected_n m-t-5 un-anchor" runat="server"> <span class="add-icon"><img src="images/connected.svg" style="height:15px;"></span> Accepted</asp:LinkButton>
                                          </strong>
                                              
                                          <asp:LinkButton ID="lnkstrLink" Text='<%# Eval("strLink") %>' CommandArgument='<%# Eval("strLink") %>' CommandName="Details"
                                          class="details_inkstrlink pt-1 inline-block  hover-a-tag"
                                          runat="server"></asp:LinkButton>
                                            
                                          </div>
                                        </div><!-- media ended -->
                                 
                                    </ContentTemplate>
                                           
                                    <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lnkCancel" />
                                    <asp:AsyncPostBackTrigger ControlID="lnkConfirm" />
                                    </Triggers>      
                                    </asp:UpdatePanel>
                                    
                                    </div>        
                            </div><!-- post-con ended -->
                        </div><!-- top-list ended -->  
                    </div><!-- card ended -->      
    
                                 <div class="display-none">
                                    <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("vchrUserName") %>'></asp:Label>
                                    <asp:Label ID="lblUserType" runat="server" Text='<%#Eval("intUserTypeID") %>'></asp:Label>
                                    <asp:Label ID="lblGroupName" Font-Italic="true" runat="server" Text='<%#Eval("strGroupName") %>'></asp:Label>
                                 </div>
                                 <!--comments box ends-->
                                 <!--tag ends-->
                                  
                                  
                              </ItemTemplate>
                           </asp:ListView>
                            
                            
                </div><!-- activity-cover ended -->
                      
                           <div class="cls"></div>
                        </div>
                        <!--<div class="bgLine">
                           <img src="images/spacer.gif" height="2" width="500" /></div> -->
                     </ItemTemplate>
                  </asp:ListView>
                  
                  <div id="pLoadMore" runat="server" align="center" ClientIDMode="Static">

                  <div id="imgLoadMore" runat="server" class="lds-ellipsis" ClientIDMode="Static" Style="display: none;">
                   <div></div>
                   <div></div>
                   <div></div>
                   <div></div>
                  </div>
                 
                 <asp:ImageButton ID="imgLoadMore2" runat="server" ClientIDMode="Static" OnClick="imgLoadMore_OnClick" Style="display: none;" />
                 </div>
                  <p align="center" Style="display: none;">
                   <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server"
                    ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                  </p>
               </div>
             
         <!--       <asp:UpdateProgress id="updateProgress" runat="server">
                <ProgressTemplate>
                 <div class="loader_maine" Style="display: none;">
                  <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/Loadgif.gif" AlternateText="Loading ..." ToolTip="Loading ..." 
                   class="divProgress loader_inner" />
                 </div>
                 <div id="pLoadMore3" runat="server" align="center" ClientIDMode="Static">

                  <div id="imgLoadMore3" runat="server" class="lds-ellipsis" ClientIDMode="Static">
                   <div></div>
                   <div></div>
                   <div></div>
                   <div></div>
                  </div>
                 
                 <asp:ImageButton ID="imgLoadMore4" runat="server" ClientIDMode="Static" Style="display: none;" />
                 </div>
                </ProgressTemplate>
               </asp:UpdateProgress> -->
             
            <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
           <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
           <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
           <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
           <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
            </ContentTemplate>
            <Triggers>
               <asp:AsyncPostBackTrigger ControlID="lnkCancels" />
            </Triggers>

         </asp:UpdatePanel>
      </div>
      <div class="cls"></div>
      <!--middle container ends-->
   </div>
   <!--pagination starts-->
   <!--pagination ends-->
   
   <div class="cls">
   </div>
   <script type="text/javascript">
      $(document).ready(function () {
          $('#divSuccess').center();
      });
   </script>
   <script type="text/jscript">
      function Cancel() {
          document.getElementById("divSuccess").style.display = 'none';
          return true;
      }
      function Submit() {
          document.getElementById("divSuccess").style.display = 'none';
          return true;
      }
      function OpenPopup() {
          return true;
      }
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          $(document).scroll(function () {

              if ($(document).height() <= $(window).scrollTop() + $(document).height()) {
                  $("#imgLoadMore").css("display", "block");
                  $(".divProgress").css("display", "none");
                  var v = $("#lblNoMoreRslt").text();
                  var maxCount = $("#hdnMaxcount").val();
                  if (parseInt(maxCount, 10) <= parseInt($("#hdnTotalItem").val(), 10) ) {
                      $("#lblNoMoreRslt").css("display", "none");
                      $("#imgLoadMore").css("display", "none");
                  } else {
                      document.getElementById('<%= imgLoadMore2.ClientID %>').click();
                  }
              }
          });
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              hideLoader1();
              $(document).scroll(function () {
                     if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
                      $("#imgLoadMore").css("display", "block");
                      $(".divProgress").css("display", "none");
                      var v = $("#lblNoMoreRslt").text();
                      var maxCount = $("#hdnMaxcount").val();
                      if (parseInt(maxCount, 10) <= parseInt($("#hdnTotalItem").val(), 10) )  {
                          $("#lblNoMoreRslt").css("display", "none");
                          $("#imgLoadMore").css("display", "none");
                      } else {
                          document.getElementById('<%= imgLoadMore2.ClientID %>').click();
                      }
                  }
              });
          });
      });
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
            
               </div><!-- full-width-con ended -->
        </div><!-- panel-cover ended -->
    </div><!-- main-section-inner ended -->
               
</asp:Content>
