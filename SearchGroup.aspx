<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
   CodeFile="SearchGroup.aspx.cs" Inherits="SearchGroup" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
<%@ Register Src="~/UserControl/AcceptMember.ascx" TagName="AcceptMember" TagPrefix="Accept" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <asp:HiddenField ClientIDMode="Static" ID="hdnGrpId" Value="" runat="server" />
          <asp:HiddenField ClientIDMode="Static" ID="hdnGrpName" Value="" runat="server" />
    <div id="divDeletesucess" class="modal backgroundoverlay" clientidmode="Static" runat="server"
        style="display: none;">
        <asp:UpdatePanel ID="upasss" runat="server" class="modal-dialog modal-dialog-centered">
            <ContentTemplate>
                <div id="divDeleteConfirm" runat="server" class="w-100" clientidmode="Static">
                    <div class="modal-content">
                        <div>
                            <b>
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                            </b>
                        </div>
                        <div class="modal-header">
                            <h5 class="modal-title">Delete Confirmation
                            </h5>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="Label3" runat="server" Text="Do you want to delete?"></asp:Label>
                        </div>
                        <div class="modal-footer border-top-0">
                            <asp:Button ID="lnkDeleteCancel" runat="server" class="add-scroller btn btn-outline-primary m-r-15" ClientIDMode="Static" Text="Cancel"
                                OnClientClick="javascript:divCancels();return false;"></asp:Button>
                            <asp:Button ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="javascript:divdeletes();"
                                CssClass="btn btn-primary success-popup" OnClick="lnkDeleteConfirm_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   <asp:UpdatePanel ID="upgroupsearch" runat="server">
      <ContentTemplate>
         
         <asp:HiddenField ID="hdnTempUserId" runat="server" Value="" ClientIDMode="Static" />
         <!--heading ends-->
         <div class="cls">
         </div>
         <!---Popups starts-->
         <div id="divFollUnfollPopup" class="modal backgroundoverlay" runat="server" clientidmode="Static">
            <div class="modal-dialog modal-dialog-centered">
               <div class="modal-content">
                  <div class="popHeading">
                     <asp:Label ID="lblTitle" runat="server"></asp:Label>
                  </div>
                  <div class="modal-header">
                     <strong>
                        <asp:Label ID="lblTitleGroup"  runat="server"></asp:Label>
                     </strong>
                  </div>
                  <div class="modal-body">
                     <strong>
                        <asp:Label ID="Label1" runat="server" Text="" ></asp:Label>
                     </strong>
                  </div>
                  <div class="modal-footer border-top-0">
                     <a href="#" clientidmode="Static" causesvalidation="false" class="btn add-scroller" onclick="Cancel();">
                     Cancel </a>
                     <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Yes" CssClass="btn btn-primary"
                        OnClick="lnkPopupOK_Click"></asp:LinkButton>
                  </div>
               </div>   
            </div>
         </div>
         <div id="divConnDisPopup" class="modal backgroundoverlay" runat="server"  clientidmode="Static">
            <div class="modal-dialog modal-dialog-centered">
               <div class="modal-content">
                  <div >
                     <b>
                        <asp:Label ID="lbl" runat="server"></asp:Label>
                     </b>
                  </div>
                  <div class="modal-header">
                
                        <h5 class="modal-title">Confirmation
                            
                            </h5>
                     
                  </div>
                  <div class="modal-body">
                     <p><asp:Label ID="lblConnDisconn" runat="server" Text="" ></asp:Label></p>
                  </div>   
                  <div class="modal-footer border-top-0">
                  
                     <asp:UpdatePanel ID="uplink" runat="server">
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="LinkButton1" />
                        </Triggers>
                        <ContentTemplate>
                           <asp:LinkButton ID="LinkButton1" runat="server"  class="btn  add-scroller btn-outline-primary m-r-15" ClientIDMode="Static" Text="Cancel"
                              OnClick="lnkCancel_Click"></asp:LinkButton>
                        </ContentTemplate>
                     </asp:UpdatePanel>
                        <asp:LinkButton ID="lnkConnDisconn" runat="server" ClientIDMode="Static" Text="Yes"
                        CssClass="btn btn-primary" OnClientClick="showLoader()" OnClick="lnkConnDisconn_Click"></asp:LinkButton>
                  </div>
               </div>
            </div>
         </div>
         <div id="divSuccess" runat="server" class="modal backgroundoverlay" style="
            display: none;" clientidmode="Static">
            <div class="modal-dialog modal-dialog-centered">
               <div class="modal-content">
                  <div class="modal-header">
                        <h5 class="modal-title">Success
                            </h5>
                  </div>
                  <div class="modal-body">
                      <p><asp:Label ID="lblSuccess" runat="server" Text="" ></asp:Label></p>
                  </div> 
                  <div class="modal-footer border-top-0"">
                     <a href="#" clientidmode="Static" causesvalidation="false"  class="btn add-scroller btn-outline-primary" onclick="javascript:Cancel();return false;">
                     Close </a>
                  </div>
               </div>   
            </div>
         </div>
         <!---Popups Ended-->
         <div class="main-section-inner" >
            <div class="panel-cover clearfix">
               <div class="full-width-con">    
                 <!--left filter further starts-->
                  <div class="form-inline">
                     <div class="w-100">
                      <div class="search-filter-con row">
                        <div class="col-lg-4 col-sm-12 margin-bottom-m col-12">
                          <asp:UpdatePanel ID="pnlSearch" runat="server" UpdateMode="Conditional">
                           <ContentTemplate> 
                            <span class="search-cover">
                            <asp:TextBox ID="txtGrpSearch" runat="server" autocomplete="off" placeholder="Search Group" ClientIDMode="Static" class="form-control p-r-0"
                             onkeydown="return postSearchGrp.bind(this)(event)">
                            </asp:TextBox>
                            <input type="button" id="searchBtn" class="search-btn-2" value="">
                            </span>

                            <asp:Button ID="btnGroupSearch" runat="server" ClientIDMode="Static" OnClick="btnGroupSearch_Click" Width="50px" Height="30px" Text="Search" Style="display: none" />
                            <asp:HiddenField ID="hdnSearch" runat="server" Value='' ClientIDMode="Static" />
                           </ContentTemplate>
                          </asp:UpdatePanel>
                        </div>  
                          <div class="search-filter-con group-lisitng col-sm-12  question-ans-page col-lg-5 col-12 margin-bottom-m">
                               <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="search-panel-container  m-0 p-0 w-100">
                                <ContentTemplate>
                                  <div class="search-cover">
                                      <ul class="ulblogcontaxt">
                                          <uc:UserControl_MultiSelect ID="lstSerchSubjCategory" ClientIDMode="Static" runat="server" />
                                          </ul>
                                      <div style="display: none;">
                                  <asp:Button ID="btnSave1" ClientIDMode="Static" runat="server"   OnClientClick="showLoader1();" OnClick="btnSave_Click" />
                                     </div>
                                  </div>

                                    
                                    </ContentTemplate>
                             </asp:UpdatePanel>

                          </div>                           
                          <div class="search-right text-lg-right col-lg-3 col-sm-12 margin-bottom-m col-12">
                                <a title="Create Groups" href="create_group.aspx" class="btn btn-outline-primary write-blog-btn">Create Group</a>
              
                              
                          </div><!-- filter-con ended --> 
                        </div>                       
                      </div>

                     <div class="clearfix"></div>
                     
                     <div style="display: none;">
                        <asp:ListView ID="lstcity" runat="server">
                           <LayoutTemplate>
                              <table width="100%" cellpadding="0" cellspacing="0">
                                 <tr id="itemPlaceHolder" runat="server">
                                 </tr>
                              </table>
                           </LayoutTemplate>
                           <ItemTemplate>
                              <div class="padding_top_style">
                                 <asp:HiddenField ID="hdnCityId" Value='<%# Eval("intCategoryId") %>' runat="server"
                                    ClientIDMode="Static" />
                                 <asp:CheckBox  CssClass="checkboxFiveSearch search_index" ID="ChkCity" Text='<%# String.Concat("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", Eval("strCategoryName"),"") %>'
                                    runat="server" AutoPostBack="true" OnCheckedChanged="Location_CheckedChange" />
                              </div>
                           </ItemTemplate>
                        </asp:ListView>
                     </div>
                      <div style="display: none;">
                     <p id="subsheading" class="subsubHeading">
                        Contexts
                     </p>
                          </div>
                     <div style="display: none;">
                        <asp:CheckBox  CssClass="checkboxFiveSearch search_index" ID="ChkCorporateLaw"
                           Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CorporateLaw" runat="server"
                           AutoPostBack="true" OnCheckedChanged="CorporateLaw_CheckedChange" />
                        <label for="ChkCorporateLaw">
                        </label>
                     </div>
                     <div style="display: none;">
                        <asp:CheckBox  CssClass="checkboxFiveSearch search_index" ID="ChkCriminalLaw"
                           Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CriminalLaw" runat="server" AutoPostBack="true"
                           OnCheckedChanged="CriminalLaw_CheckedChange" />
                        <label for="ChkCriminalLaw">
                        </label>
                     </div>
                     <div style="display: none;">
                        <asp:CheckBox  CssClass="checkboxFiveSearch search_index" ID="ChkFamilyLaw"
                           Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FamilyLaw" runat="server" AutoPostBack="true"
                           OnCheckedChanged="FamilyLaw_CheckedChange" />
                        <label for="ChkFamilyLaw">
                        </label>
                     
                     <div class="bgLine">
                     </div>
                     <asp:LinkButton ID="imgReset" CssClass="reset" runat="server" OnClick="imgReset_Click"
                        Style="text-decoration: none;"> Reset</asp:LinkButton>
                         </div>
                  </div>
                  <!--left filter further ends-->
                  <!--middle container starts-->
                  <div class="row create-group-list" >
                     <!--search result list starts-->
                     <div >
                        <asp:Label ID="lblMessage" runat="server" Text="No Group Found..!" class="inline-block pl-3" ForeColor="Red"
                           Visible="false"></asp:Label>
                     </div>
                     <asp:ListView ID="lstGroup" runat="server" OnItemCommand="lstGroup_ItemCommand" OnItemDataBound="lstGroup_ItemDataBound">
                        <LayoutTemplate>
                           <table cellpadding="0" cellspacing="0" width="100%">
                              <tr id="itemPlaceHolder" runat="server">
                              </tr>
                           </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                           <div class="col-sm-3" id="Grid" runat="server">
                              <div class="doc-card text-center">
                                    <div class="searchListNew list_style">
                                            <span class="img-cover">
                                                <asp:LinkButton ID="LinkButton2" CommandName="Details" runat="server">
                                                <img id="imgprofile" class="user-img" runat="server"
                                                    src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' />
                                                      </asp:LinkButton>
                                            </span>
                                       <h4>
                                          <asp:LinkButton ID="lblPostlink" Text='<%# Eval("strPostDescription") %>' CommandName="Details"
                                             CssClass="searchGrouplinnk un-anchor truncate cursor-pointer" runat="server"></asp:LinkButton>
                                 
                                       </h4>
                                       <p>
                                          <!-- <asp:Label ID="lblSummary" class="blog_by truncate" Text='<%# Eval("strSummary") %>' runat="server"></asp:Label>
                                    -->
                                       <asp:HiddenField ID="hdnId" Value='<%# Eval("Id") %>' runat="server" />
                                       <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' runat="server" />
                                       </p>
                           
                                       <span class="member-group">
                                          <span class="member-icon"><img src="images/member.svg" alt=""></span>
                                          <asp:Label ID="lblGroupMember" class="gray_color" Text="" runat="server"></asp:Label>
                                       </span>
                                        
                                       <asp:LinkButton ID="btnjoinreq" CssClass="btn hide-body-scroll btn-outline-primary" CommandName="JoinGroup" CausesValidation="false"
                                          runat="server" Text="Join"></asp:LinkButton>
                                       <asp:LinkButton ID="btnDelete" CssClass="btn hide-body-scroll btn-outline-primary"  Font-Underline="false"  Visible="false" 
                                          ClientIDMode="Static" OnClientClick='<%# "docdelete(" + Eval("ID") + ", \"" + Eval("strPostDescription").ToString() + "\" );" %>' Text="Delete"
                                          ToolTip="Delete"  
                                           CausesValidation="false" runat="server">
                                       </asp:LinkButton>
                                     
                                    </div>
                                    <div style="display: none" class="jury">
                                       <asp:Label ID="lblEmailId" Text='<%# Eval("vchrUserName") %>' runat="server"></asp:Label>
                                       <asp:Label ID="lblUserName" Text='<%# Eval("UserName") %>' runat="server"></asp:Label>
                                       <asp:Label ID="lblGrpsAccess" Text='<%# Eval("strAccess") %>' runat="server"></asp:Label>
                                    </div>
                              
                              </div>
                           </div>
                        </ItemTemplate>
                     </asp:ListView>
                     <div class="cls">
                     </div>
                     <div class="cls">
                     </div>
                     <div class="pagination_main_div">
                        <nav aria-label="Page navigation example">
                        <ul id="dvPage" runat="server" class="pagination"
                           clientidmode="Static">
                         <asp:LinkButton ID="lnkFirst" runat="server" CssClass="PagingFirst page-link first-page" OnClick="lnkFirst_Click"> <span class="lnr lnr-chevron-left"></span><span class="lnr lnr-chevron-left"></span> </asp:LinkButton>
                           <asp:LinkButton ID="lnkPrevious" runat="server" class="page-link previous-page" OnClick="lnkPrevious_Click"> <span class="lnr lnr-chevron-left"></span></asp:LinkButton>
                           <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand"
                              OnItemDataBound="rptDvPage_ItemDataBound">
                              <ItemTemplate>
                                 <li class="page-item">
                                    <asp:HiddenField ID="hdnItemCount" runat="server" Value='<%#Eval("intPageNo") %>'
                                       ClientIDMode="Static" />
                                    <asp:LinkButton ID="lnkPageLink" CssClass="page-link" runat="server" ClientIDMode="Static"
                                       CommandName="PageLink" Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
                                 </li>   
                              </ItemTemplate>
                           </asp:Repeater>
                           <asp:LinkButton ID="lnkNext" runat="server" class="page-link previous-page" OnClick="lnkNext_Click"><span class="lnr lnr-chevron-right"></span></asp:LinkButton>
                          <asp:LinkButton ID="lnkLast" runat="server" Style="background: #fff" OnClick="lnkLast_Click" class="page-link last-page"><span class="lnr lnr-chevron-right"></span> <span class="lnr lnr-chevron-right"></span></asp:LinkButton> 
                           <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
                           <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
                           <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
                           <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
                           <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
                           <asp:HiddenField ID="hdnEndPage" runat="server" ClientIDMode="Static" />
                        </ul>
                        </nav>
                     </div>
                     <!--search result list ends-->
                  </div>
                  <!--middle container ends-->
        
                  <div class="adv" style="display: none;">
                     <!--right side banner starts-->
                     <img src="images/adv1.jpg" /><br />
                     <br />
                     <img src="images/adv2.jpg" />
                     <!--right side banner ends-->
                  </div>
               </div>
            </div>
         </div>
         <!--pagination starts-->
         <div class="innerContainer">
            <div class="cls">
            </div>
         </div>
         <!--pagination ends-->
         <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
         <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
         <div class="cls">
         </div>
          <div style="display:none;">
         <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
               <div class="loader_home_gif">
                  <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/Loadgif.gif"
                     AlternateText="Loading ..." ToolTip="Loading ..." 
                     class="divProgress loader_bar" />
               </div>
            </ProgressTemplate>
         </asp:UpdateProgress>
              </div>
      </ContentTemplate>
      <Triggers>
         <asp:AsyncPostBackTrigger ControlID="lnkConnDisconn" />
      </Triggers>
   </asp:UpdatePanel>
   <script type="text/jscript">
       function postSearchGrp(e) {
           if (e.keyCode == 13 && $(this).val()) {
               $('#btnGroupSearch').click();
               showLoader1();
               return false;
           } else if (e.keyCode == 13) {
               return false;
           }
       }

       function divdeletes() {
          
           $('#divDeletesucess').css("display", "none");
           // showLoader1();
       }
       function showLoader() {
           $('#divConnDisPopup').hide();
           showLoader1();
       }
        function divCancels() {
            $('#hdnGrpId').val('');
            $('#hdnGrpName').val('');
            $('#divDeletesucess').hide();
      }
       function docdelete(grpid, grpName) {
           $('#hdnGrpId').val(grpid);
           $('#hdnGrpName').val(grpName)
           $('#divDeletesucess').css("display", "block");
       }
      function Cancel(e) {
          document.getElementById("divFollUnfollPopup").style.display = 'none';
          document.getElementById("divConnDisPopup").style.display = 'none';
          document.getElementById("divSuccess").style.display = 'none';
          return false;
      }
   </script>
   <script type="text/javascript">
       function selectControlOnChange(evt, params) {
            $('#selectControl').trigger('chosen:close');
            $('.chosen-drop').css('display','none');
            document.getElementById("btnSave1").click();
       }

      $(document).ready(function () {
          $('.Joincss').click(function (e) {
              $(this).css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $('#lnkConnDisconn').click(function (e) {
              $(this).css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $('#divConnDisPopup').center();
          createChosen();

           $("#dvPage a").on('click', function(event){showLoader1();});
          $('#selectControl').on('change', selectControlOnChange);

          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              $('.Joincss').click(function (e) {
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $('#lnkConnDisconn').click(function (e) {
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
              createChosen();
              $('#selectControl').on('change', selectControlOnChange);
              hideLoader1();

               $("#dvPage a").on('click', function(event){showLoader1();});
          });
      });
   </script>
</asp:Content>
