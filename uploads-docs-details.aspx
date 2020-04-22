<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="uploads-docs-details.aspx.cs"
   EnableEventValidation="false" Inherits="uploads_docs_details" %>
<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Src="~/UserControl/Groups-m.ascx" TagName="GroupDetailsM" TagPrefix="GroupM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
   <script src="<%=ResolveUrl("js/jquery.custom-scrollbar.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("Jsscroll/jquery.mCustomScrollbar.concat.min.js")%>" type="text/javascript"></script>
   <script src="<%=ResolveUrl("Jsscroll/jquery.mCustomScrollbar.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <!--inner container ends-->
      <asp:UpdatePanel ID="updates" runat="server">
         <ContentTemplate>
            <div class="main-section-inner">
               <!---Popup Div Start-->
               <div id="divDeletesucess" clientidmode="Static" runat="server" class="  modal backgroundoverlay EditProfilepopupHome"
                  style="display: none;">
                  <div id="divDeleteConfirm" runat="server" class="confirmDeletes modal-dialog modal-dialog-centered" clientidmode="Static">
                  
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
                              <asp:Label ID="Label4" runat="server" Text="Do you want to delete ?" ></asp:Label>
                           </div>   
                           <div class="modal-footer border-top-0">
                            
                              <asp:LinkButton ID="lnkDeleteCancel" runat="server" class="add-scroller btn btn-outline-primary  m-r-15" ClientIDMode="Static" Text="Cancel"
                                 OnClick="lnkDeleteCancel_Click"
                                 OnClientClick="divCancels();"></asp:LinkButton>
                                   <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes"
                                 CssClass="btn btn-primary success-popup" OnClick="lnkDeleteConfirm_Click"></asp:LinkButton>
                           </div>
                       
                     </div>
                  </div>
               </div>
               <div id="popup1" class="modal_bg" runat="server" style="display: none; ">
                  <div class="modal-dialog">
                     <div class="modal-header">
                        <a href="#" class="js-modal-close close" onclick="Closepopup();" >×</a>
                        <h3>
                           <asp:Label ID="lblTitles" runat="server"></asp:Label>
                        </h3>
                     </div>
                     <div class="modal-body">
                        <p>
                        <div id="divdisp1" name="divdisp" runat="server" clientidmode="Static" class="divdisp"
                           onkeypress="javascript:return false;" onkeydown="javascript:return false;">
                        </div>
                        <asp:GridView ID="GridView1" runat="server" Width="100px">
                           <RowStyle />
                           <EmptyDataRowStyle BackColor="Silver" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                              ForeColor="#003300" />
                           <HeaderStyle BackColor="#6699FF" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px"
                              VerticalAlign="Top" Wrap="True" />
                        </asp:GridView>
                        </p>
                     </div>
                     <div class="modal-footer"> <a href="#" class=" joinBtn default_btn js-modal-close" onclick="Closepopup();return false;">Close</a> </div>
                  </div>
               </div>
               <!---Popup Div Ended-->
               <!---Center Panel Start-->
               <div class="center-panel">
                  <div class="custom-nav-con group-page-tab">
                     <GroupM:GroupDetailsM ID="grpDetailsM" runat="server" />
                     <ul class="custom-nav-control nav nav-tabs ">
                        <li class="nav-item">
                           <asp:LinkButton ID="lnkProfile" class="nav-link" runat="server" Text="Profile" ClientIDMode="Static"
                              OnClick="lnkProfile_Click"></asp:LinkButton>
                        </li>
                        <li id="DivHome" runat="server" class="nav-item">
                           <div>
                              <asp:LinkButton ID="lnkHome" class="nav-link" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click"></asp:LinkButton>
                           </div>
                        </li>
                        <li id="DivForumTab" runat="server" clientidmode="Static" class="nav-item">
                           <div>
                              <asp:LinkButton ID="lnkForumTab" class="nav-link" runat="server" Text="Forums" ClientIDMode="Static"
                                 OnClick="lnkForumTab_Click"></asp:LinkButton>
                           </div>
                        </li>
                        <li id="DivUploadTab" runat="server" clientidmode="Static" class="nav-item">
                           <div>
                              <asp:LinkButton ID="lnkUploadTab"  runat="server" Text="Uploads" ClientIDMode="Static"
                                 class="forumstabAcitve nav-link" OnClick="lnkUploadTab_Click"></asp:LinkButton>
                           </div>
                        </li>
                        <li id="DivPollTab" runat="server" clientidmode="Static" class="nav-item">
                           <div>
                              <asp:LinkButton ID="lnkPollTab" class="nav-link" runat="server" Text="Polls" ClientIDMode="Static"
                                 OnClick="lnkPollTab_Click"></asp:LinkButton>
                           </div>
                        </li>
                        <li id="DivEventTab" runat="server" clientidmode="Static" class="nav-item">
                           <div>
                              <asp:LinkButton ID="lnkEventTab" class="nav-link" runat="server" Text="Events" ClientIDMode="Static"
                                 OnClick="lnkEventTab_Click"></asp:LinkButton>
                           </div>
                        </li>
                        <li id="DivMemberTab" runat="server" clientidmode="Static" class="nav-item">
                           <div>
                              <asp:LinkButton ID="lnkMemberTab" class="nav-link" runat="server" Text="Members" ClientIDMode="Static"
                                 OnClick="lnkEventMemberTab_Click"></asp:LinkButton>
                           </div>
                        </li>
                     </ul>
                     <div class="tab-content m-t-15">
                        <div class="btn-title-con">
                           <div>
                              <h5 class="card-title float-left">Uploads</h5>
                           </div>
                           <div>
                               <a id="btnCreateFolder" runat="server" class="btn display-none-m hide-body-scroll btn-outline-primary  m-r-15" href="#" clientidmode="Static"
                              >Create Folder</a>
                           <asp:LinkButton ID="btnSave" CssClass="display-none-m btn btn-outline-primary" runat="server" Text="Upload" ClientIDMode="Static" 
                            OnClick="btnSave_Click" OnClientClick="showLoader1();">
                           </asp:LinkButton>
                           </div>
                        </div>
                        <div class="innerGroupBoxnew marginTop25 paddingbox blue-border-top">
                           <div>
                              <!---Popup Start-->
                              <div id="divCancelPoll" class="modal backgroundoverlay" runat="server" style=" display:none;" clientidmode="Static">
                                 <div class=" modal-dialog modal-dialog-centered">
                                    <div class="modal-content">
                                       <div>
                                          <b>
                                             <asp:Label ID="lbl" runat="server"></asp:Label>
                                          </b>
                                       </div>
                                       <div class="modal-header">
                                          <h5 class="modal-title">Success</h5>
                                       </div>
                                       <div class="modal-body">
                                           <asp:Label ID="lblConnDisconn" runat="server" Text="" Font-Size="Small"></asp:Label>
                                       </div>   
                                       <div class="modal-footer border-top-0">
                                          
                                          <a href="#" clientidmode="Static" class="add-scroller btn btn-outline-primary m-r-15" causesvalidation="false"
                                             onclick="CancelDel();">Cancel </a>
                                             <asp:LinkButton ID="lnkConnDisconn" runat="server" ClientIDMode="Static" Text="Yes"
                                             CssClass="btn btn-primary add-scroller" OnClick="lnkConnDisconn_Click"></asp:LinkButton>
                                       </div>
                                    </div>   
                                 </div>
                              </div>
                             
                              <div id="DivDownload" class="modal_bg" runat="server" style="
                                 display: none;" clientidmode="Static">
                                 <div class="modal-dialog">
                                    <div>
                                       <b>
                                          <asp:Label ID="Label1" runat="server"></asp:Label>
                                       </b>
                                    </div>
                                    <div class="modal-header">
                                       <strong>
                                          &nbsp;&nbsp;
                                          <asp:Label ID="Label2" runat="server" Text="This File Format Different, You want to download ?"
                                             Font-Size="Small"></asp:Label>
                                       </strong>
                                    </div>
                                    <div class="modal-footer">
                                       <asp:LinkButton ID="lnkYesDownload" runat="server" ClientIDMode="Static" Text="Yes"
                                          CssClass="joinBtn" OnClick="lnkDownload_OnClick"></asp:LinkButton>
                                       <asp:LinkButton ID="lnkCloseDownload" runat="server" ClientIDMode="Static" Text="No"
                                          CssClass="joinBtn" OnClick="lnkCloseDownload_OnClick"></asp:LinkButton>
                                    </div>
                                 </div>
                              </div>
                              <!---Create folded success popup-->
                              <div id="divFolderSuccess" class="modal backgroundoverlay" runat="server" style="display: none;" clientidmode="Static">
                                 <div class="modal-dialog modal-dialog-centered">
                                    <div class="modal-content">
                                       <div class="modal-header">
                                          <h5 class="modal-title">Success</h5>
                                       </div>
                                       <div class="modal-body">
                                          <p>
                                             <asp:Label ID="lblSuccess"  runat="server" Text="Folder created successfully."
                                                ></asp:Label>
                                             <asp:Label ID="lblEditSuccess" runat="server"  Text="Folder updated successfully."
                                                ></asp:Label>
                                             <asp:Label ID="lblDelSuccess" runat="server" Text="Folder Delated successfully."
                                                ></asp:Label>
                                          </p>      
                                       </div>   
                                       <div class="modal-footer border-top-0">
                                          <a href="#" clientidmode="Static"  class="btn btn-outline-primary add-scroller" causesvalidation="false" onclick="javascript:MesCloseFolder();return false;">
                                          Close </a>
                                       </div>
                                    </div>   
                                 </div>
                              </div>
                              <!---Create folded success popup Ended-->
                              <p class="documentHeading">
                                 <asp:LinkButton ID="lnlBack" runat="server">Back to Library</asp:LinkButton>
                              </p>
                              <div >
                              <!---Create Folder Popup Start-->
                              <div id="dvPopup" class="modal backgroundoverlay" clientidmode="Static" runat="server" style="display: none">
                                 <div class="modal-dialog modal-dialog-centered">
                                    <div class="modal-content">
                                    <div class="modal-header">
                                       <h5 class="modal-title">Create Folder</h5>
                                    </div>
                                    <div class="modal-body">  
                                       <div class="form-group">     
                                          <ul class="list-inline d-none d-sm-block">
                                             <li class="list-inline-item">
                                                <span class="custom-radio">
                                                   <asp:RadioButton ID="rbPublic" runat="server" Text="" Checked="true" ClientIDMode="Static" />
                                                   <label for="rbPublic">Public</label>
                                                </span>
                                             </li>
                                             <li class="list-inline-item">
                                                <span class="custom-radio">
                                                   <asp:RadioButton ID="rbPrivate" runat="server" ClientIDMode="Static" />
                                                   <label for="rbPrivate">Private</label>
                                                </span>
                                             </li>   
                                          </ul>
                                       </div>
                                       <div class="form-group">
                                          <label for="folder name">Folder name </label>
                                          <asp:TextBox ID="txtDirectoryName" autocomplete="off" maxlength="50" runat="server" class="form-control" ValidationGroup="vg" Width="100%"
                                             placeholder="Folder Name" > </asp:TextBox>
                                         
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDirectoryName"
                                             Display="Dynamic" ValidationGroup="vg" ErrorMessage="Please enter folder name"
                                             ForeColor="Red"></asp:RequiredFieldValidator>
                                          <asp:Label ID="lblMess" ForeColor="Red" runat="server" Text=""></asp:Label>
                                      </div>
                                   
                                       <div class="form-group">
                                          <label for="Description"> Description </label>
                                          <textarea ID="txtDescription" autocomplete="off" maxlength="500" class="form-control" runat="server" TextMode="MultiLine" ClientIDMode="Static"
                                             placeholder="Description" 
                                             ></textarea>
                                       </div>   
                                      
                                    </div>
                                    <div class="modal-footer border-top-0 padding-top-0">
                                       <a id="btnCancel" href="javascript:void(0)" runat="server" clientidmode="Static"  class="btn btn-outline-primary m-r-15 add-scroller"
                                          onclick="$('#dvPopup').hide();">Cancel</a>
                                       <asp:LinkButton ID="lnkPopupOK" OnClick="lnkPopupOK_Click" ValidationGroup="vg" runat="server"
                                          OnClientClick="javascript:CallPopups();"
                                          ClientIDMode="Static" Text="Create Folder" CssClass="btn btn-primary mr-0"></asp:LinkButton>
                                       
                                       <div style="display: none;">
                                          <asp:Button ID="btnSave1" runat="server" Font-Bold="true" Text="Create Forum" ClientIDMode="Static"
                                             ValidationGroup="vg" OnClick="lnkPopupOK_Click" OnClientClick="javascript:CallPopups();" />
                                       </div>
                                    </div>
                                    </div>
                                 </div>
                              </div>
                                 <div class="addclass_g_u_d">
                                    <strong class="mb-2 inline-block">
                                       <asp:LinkButton class="cursor-pointer" ID="lnkFirst" runat="server" OnClick="lnkFirst_Click"> </asp:LinkButton>
                                       
                                    </strong>
                                   <asp:Label
                                       ID="lblFirst" runat="server" Visible="false" Text="&nbsp;>"><span class="lnr lnr-chevron-right"></span></asp:Label>
                                    <asp:LinkButton ID="lnkSecond" runat="server" OnClick="lnkSecond_Click"></asp:LinkButton>
                                    <asp:Label ID="lblSecond" runat="server" Visible="false" Text="&nbsp;>>" ></asp:Label>
                                    <asp:LinkButton ID="lnkThird" runat="server" OnClick="lnkThird_Click" ></asp:LinkButton>
                                    <asp:Label ID="lblThird" runat="server" Visible="false" Text="&nbsp;>>" ></asp:Label>
                                 </div>
                                 <div>
                                    <asp:ImageButton ID="imgBtn" Visible="false" runat="server" />
                                 </div>
                                 <div>
                                    <asp:Panel ID="pnlFolderDetails" runat="server">
                                       <asp:ListView ID="lstFolderDetails" runat="server" GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder1"
                                          OnItemCommand="lstFolderDetails_ItemCommand" OnItemDataBound="lstFolderDetails_ItemDatabound"
                                          ItemPlaceholderID="itemPlaceHolder1">
                                          <LayoutTemplate>
                                             <div>
                                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                             </div>
                                          </LayoutTemplate>
                                          <GroupTemplate>
                                             <div class="row">
                                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                             </div>
                                          </GroupTemplate>
                                      
                                          <ItemTemplate>
                                             <div id="tdfolder" runat="server" class="col-sm-3">
                                           
                                                <div class="doc-card uploads-doc mb-4">
                                                   <!---Edit and Delete Buttos-->
                                                   <div class="more-btn float-right">
                                                      <span class="dropdown show">
                                                        <a href="#" role="button" runat="server" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                                         <img src="images/more.svg" alt="" class="more-btn">
                                                        </a>

                                                        <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start" >
                                                            <asp:LinkButton ID="lnkEdit" Font-Underline="false" ToolTip="Edit" Text="Edit"
                                                               CommandName="EditFolder" CausesValidation="false" runat="server" class="hide-body-scroll dropdown-item"><span class="lnr lnr-pencil"></span> Edit
                                                            </asp:LinkButton>
                                                       
                                                        
                                                            <asp:LinkButton ID="lnkDelete" CommandName="DeleteFolder" CausesValidation="false"
                                                               Text="Delete" runat="server" OnClientClick="divCancels();" class="dropdown-item hide-body-scroll"><span class="lnr lnr-trash"></span> Delete                                   
                                                            </asp:LinkButton>
                                                       
                                                        </span>
                                                      </span> <div class="clearfix"></div> 


                                                   </div>
                                                
                                                   <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' ClientIDMode="Static"
                                                      runat="server" />
                                                   <asp:HiddenField ID="hdnFolderId" Value='<%#Eval("intFolderId") %>' ClientIDMode="Static"
                                                      runat="server" />
                                                   <asp:HiddenField ID="hdnFolderName" Value='<%#Eval("strFolderName") %>' ClientIDMode="Static"
                                                      runat="server" />
                                                   <asp:HiddenField ID="hdnstrIsPublic" Value='<%#Eval("strIsPublic") %>' ClientIDMode="Static"
                                                      runat="server" />
                                                   <asp:HiddenField ID="hdnstrFolderDescription" Value='<%#Eval("strFolderDescription") %>'
                                                      ClientIDMode="Static" runat="server" />
                                                   
                                                      <span>
                                                         <asp:ImageButton ID="imgBtn" ImageUrl="~/images/folder.jpg" CommandName="FolderDetails"
                                                            CssClass="imageLoadmore doc-img" ToolTip='<%#Eval("strFolderDescription") %>' runat="server" />
                                                      </span>
                                                      <div class="uploaded-folder pb-2">

                                                         <asp:LinkButton ID="lblFolderName"  class="cursor-pointer doc-text truncate" CommandName="FolderDetails" runat="server" Text='<%#Eval("strFolderName") %>'></asp:LinkButton>
                                                            <asp:Label ID="lblTotalDocs" class="folder-count" runat="server" Text=""></asp:Label>
                                                      </div>      
                                                        
                                                       
                                            
                                                     
                                                 
                                                  
                                                </div>
                                           
                                             </div> 
                                          </ItemTemplate>
                                      
                                       </asp:ListView>
                                    </asp:Panel>
                                    <!---Uploaded documents-->
                                    <asp:Panel ID="pnlLstDocument" runat="server" class="row">
                                       <asp:ListView ID="LstDocument" runat="server" GroupItemCount="5" GroupPlaceholderID="groupPlaceHolder1"
                                          OnItemCommand="LstDocument_ItemCommand" OnItemDataBound="LstDocument_ItemDatabound"
                                          ItemPlaceholderID="itemPlaceHolder1">
                                          <LayoutTemplate>
                                             <table cellpadding="0" cellspacing="0">
                                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                             </table>
                                          </LayoutTemplate>
                                          <GroupTemplate>
                                             <tr>
                                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                             </tr>
                                          </GroupTemplate>
                                          <ItemTemplate>
                                             <div id="tdDocs" runat="server" class="col-sm-3">
                                         
                                                <div class="doc-card uploads-doc mb-4">
                                                   <!---Edit Delete Documents-->
                                                   <span class="more-btn float-right">

                                                      <span class="dropdown show">
                                                        <a href="#" role="button" runat="server" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                                         <img src="images/more.svg" alt="" class="more-btn">
                                                        </a>

                                                        <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="top-start">
                                                           <asp:LinkButton ID="lnkEdit" Font-Underline="false" ToolTip="Edit" Text="Edit"
                                                            CommandName="EditDocs" CausesValidation="false" runat="server" class="dropdown-item">
                                                            <span class="lnr lnr-pencil"></span> Edit
                                                         </asp:LinkButton>
                                                    
                                                 
                                                         <asp:LinkButton ID="lnkDelete" CommandName="DeleteDocs" CausesValidation="false"
                                                            Text="Delete" runat="server" class="dropdown-item hide-body-scroll"> <span class="lnr lnr-trash"></span> Delete                         
                                                         </asp:LinkButton>
                                                        </span>
                                                      </span>  


                                                   </span>
                                                     <!---Edit Delete Documents Ended-->
                                                   <asp:HiddenField ID="hdnUploadDocsId" Value='<%#Eval("intUploadDocId") %>' ClientIDMode="Static"
                                                      runat="server" />
                                                   <asp:HiddenField ID="hdnFilePath" Value='<%#Eval("strFilePath") %>' ClientIDMode="Static"
                                                      runat="server" />
                                                   <asp:HiddenField ID="hdnstrDocTitle" Value='<%#Eval("strDocTitle") %>' ClientIDMode="Static"
                                                      runat="server" />
                                                   <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' ClientIDMode="Static"
                                                      runat="server" />
                                                   <asp:HiddenField ID="hdnintDocsSee" Value='<%#Eval("intDocsSee") %>' ClientIDMode="Static"
                                                      runat="server" />
                                                    <asp:HiddenField ID="hdnintDocumentTypeID" Value='<%#Eval("intDocumentTypeID") %>' ClientIDMode="Static"
                                                      runat="server" />
                                                   <%--  <div style="margin-left: 52px; position: absolute; margin-top: 44px;">
                                                      <asp:LinkButton ID="lblPrivew" runat="server" Style="color: #9C9C9C; text-decoration: none !important;"
                                                          CommandName="OpenDoc">Privew</asp:LinkButton>
                                                      </div>--%>
                                                         <span>
                                                         <img title='<%#Eval("strDocsDetails") %>' src="images/document.png" alt="" class="doc-img">
                                                         </span> 
                                                  
                                                 
                                                         <div class="clearfix"></div>
                                                         <div class="uploaded-folder pb-2">
                                                             <div id ="divDoctTypeOuter" class="global-after" runat="server" visible="false">
                                                                 <asp:Label id="divDocType" runat="server" ></asp:Label> 
                                                             </div>
                                                            <asp:LinkButton ID="hrp_DocPath" Font-Underline="false" ToolTip="Open File" Text='<%#Eval("strDocTitle") %>'
                                                            class="hrp_class cursor-pointer font-weight-normal doc-text truncate" CausesValidation="false"
                                                            ClientIDMode="Static" runat="server" CommandName="OpenDocDown"></asp:LinkButton>
                                                         </div>   
                                                      
                                                    
                                                  
                                                
                                                        
                                                 
                                                </div>
                                             
                                             </div>
                                          </ItemTemplate>
                                       </asp:ListView>
                                    </asp:Panel>
                                 </div>
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>   
               </div>
               <!---Center Panel Ended-->
               <!---Right panel Start-->
             
                 <asp:UpdatePanel ID="updatedoc" runat="server" UpdateMode="Conditional">
                     <ContentTemplate>
                        <Group:GroupDetails ID="grpDetails" runat="server" />
                     </ContentTemplate>
                  </asp:UpdatePanel>
             
               <!---Right Panel Ended-->
               <asp:HiddenField ID="hdnUptFolderName" runat="server" ClientIDMode="Static" />
               <asp:HiddenField ID="hdnFolderEditId" runat="server" ClientIDMode="Static" />
            </div>   
         </ContentTemplate>
         <Triggers>
         <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
         <asp:AsyncPostBackTrigger ControlID="lnkDeleteCancel" />
         <asp:AsyncPostBackTrigger ControlID="lnkPopupOK" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="lnkCloseDownload" />
         </Triggers>
      </asp:UpdatePanel>

   <script type="text/javascript">
      function CancelFolder() {
          document.getElementById("dvPopup").style.display = 'none';
          return true;
      }
        $(document).on('click', '.mobile_tab_icon', function() {
             $('ul.group_p').slideToggle('slow');
            });
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          $('#rbPublic').click(function () {
              $(this).prop('checked', true);
              $('#rbPrivate').prop('checked', false);
          });
          $('#rbPrivate').click(function () {
              $(this).prop('checked', true);
              $('#rbPublic').prop('checked', false);
          });
          $('#btnCreateFolder').click(function () {
              $('#dvPopup').show();
              $('#divDeletesucess').hide();
              $('#divCancelPoll').hide();
          });
          $('#btnCancel').click(function () {
              $('#dvPopup').hide();
              clearFolder();
          });
      });
      $(document).ready(function () {
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              $('#rbPublic').click(function () {
                  $(this).prop('checked', true);
                  $('#rbPrivate').prop('checked', false);
              });
              $('#rbPrivate').click(function () {
                  $(this).prop('checked', true);
                  $('#rbPublic').prop('checked', false);
              });
              $('#btnCreateFolder').click(function () {
                  $('#dvPopup').show();
                  $('#divDeletesucess').hide();
                  $('#divCancelPoll').hide();
              });
              $('#btnCancel').click(function () {
                  $('#dvPopup').hide();
                  clearFolder();
              });
          });
      });
      function clearFolder() {
          $('#ctl00_ContentPlaceHolder1_txtDirectoryName').val('');
          $('#txtDescription').val('');
          $('#rbPublic').prop('checked', true);
          $('#rbPrivate').prop('checked', false);
      }
   </script>
   <script type="text/javascript">
      function Opendiv(val) {
          $("#divFilePopup").dialog({
              title: val,
              buttons: {
                  Close: function () {
                      $(this).dialog('close');
                  }
              },
              modal: false
          });
          return false;
      }
   </script>
   <script type="text/ecmascript">
      $(document).ready(function () {
          $("#CKDescription").keyup(function () {
              $.ajax({
                  url: "handler/FileHandler.ashx?q=" + $("#CKDescription").val(),
                  success: function (data) { return true },
                  error: function (data) { return true }
              });
          });
      });
   </script>
   <script type="text/javascript">
      (function ($) {
          $(window).load(function () {
              $(".scrollbarD").mCustomScrollbar({
                  axis: "y"
              });
          });
      })(jQuery);
      
      $(document).ready(function () {
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              $(".scrollbarD").mCustomScrollbar({
                  axis: "y"
              });
          });
      });
      
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          $('#divCancelPoll').center();
      });
   </script>
   <script type="text/javascript">
      function docdelete() {
          $('#divDeletesucess').css("display", "block");
      }
      function divCancels() {
          $('#divDeletesucess').css("display", "none");
      }
   </script>
   <script type="text/javascript">
      $(window).load(function () {
          $(".demo").customScrollbar(); $("#scroll-button1").click(function () {
              $("#vertical-scrollbar-demo").customScrollbar("scrollTo", $("#vertical-scrollbar-demo p"));
          });
          $("#scroll-button2").click(function () {
              $("#horizontal-scrollbar-demo").customScrollbar("scrollTo", $("#horizontal-scrollbar-demo #no-lena"));
          });
          $("#scroll-button3").click(function () { $("#vertical-horizontal-scrollbar-demo").customScrollbar("scrollTo", $("#vertical-horizontal-scrollbar-demostrong")); });
      });
      function CancelDel() {
          document.getElementById("divCancelPoll").style.display = 'none';
          return false;
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
   <script type="text/javascript">
      function MesCloseFolder() {
          document.getElementById("divFolderSuccess").style.display = 'none';
      }
      function CallPopups() {
          $('#lnkPopupOK').css("box-shadow", "0px 0px 5px #00B7E5");
          if ($('#ctl00_ContentPlaceHolder1_txtDirectoryName').text() == '') {
              setTimeout(
              function () {
                  $('#lnkPopupOK').css("box-shadow", "0px 0px 0px #00B7E5");
              }, 1000);
          }
      }
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          $("span.spEditFolder").click(function () {
              $(this).children('.edite').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $("span.spDeleteFolder").click(function () {
              $(this).children('.editd').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $("span.spEditFile").click(function () {
              $(this).children('.edite').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $("span.spDeleteFile").click(function () {
              $(this).children('.editd').css("box-shadow", "0px 0px 5px #00B7E5");
          });
          $(document).ready(function () {
              $('#ctl00_ContentPlaceHolder1_txtDirectoryName').keypress(function (e) {
                  if (e.keyCode == 13) {
                      $('#btnSave1').click();
                      return false;
                  }
              });
              $('#lnkDeleteConfirm').click(function (e) {
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
          });
      
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              $('#lnkDeleteConfirm').click(function (e) {
                  $(this).css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spEditFolder").click(function () {
                  $(this).children('.edite').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spDeleteFolder").click(function () {
                  $(this).children('.editd').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spEditFile").click(function () {
                  $(this).children('.edite').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $("span.spDeleteFile").click(function () {
                  $(this).children('.editd').css("box-shadow", "0px 0px 5px #00B7E5");
              });
              $('#ctl00_ContentPlaceHolder1_txtDirectoryName').keypress(function (e) {
                  if (e.keyCode == 13) {
                      $('#btnSave1').click();
                      return false;
                  }
              });
          });
      
      });
      
   </script>
</asp:Content>
