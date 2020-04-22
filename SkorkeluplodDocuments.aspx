<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
   CodeFile="SkorkeluplodDocuments.aspx.cs" Inherits="SkorkeluplodDocuments" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
   <script src="js/jquery.filedrop.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div >
      <div class="innerDocumentContainerSpc new_added">
         <div class="innerContainer">
            <div class="innerGroupBox">
               <div class="skorelScoreMenu">
                  <div class="mobile_tab_icon">
                     <div class="tab_name"></div>
                     <div class="tab_icon">
                        <i class="fa fa-ellipsis-v" aria-hidden="true"></i>
                     </div>
                  </div>
                  <ul class="mobile_m_skorkel">
                     <li title="My Score"><a href="my-points.aspx" class=" myscoreactive"><img src="images/achivements-icon.png" /><span>Score</span></a>
                     </li>
                     <li title="My Tags"><a href="my-tag.aspx" ><img src="images/tags_new.png" /><span>Tags</span></a></li>
                     <li title="My Documents"><a href="my-documents.aspx" ><img src="images/documents_new.png" /><span>Documents</span></a></li>
                     <li title="My Bookmarks "><a href="my-bookmarks.aspx" ><img src="images/bookmark_new.png" /><span>Bookmarks</span></a></li>
                     <li title="My Search"><a href=" my-saved-searches.aspx" ><span><img src="images/search_new.png">Search</span></a></li>
                  </ul>
               </div>
               <div class="skorkelBox margin_top_t">
                  <div class="skorkelUpload margin_top_t">
                     <div >
                        <asp:Label ID="lblMessage" runat="server" class="font_size display_none"></asp:Label>
                     </div>
                     <div class="dropnDrag" id="dvDest">
                        <p class="uploadDragDrop">
                           Drag and Drop File<br />
                           +
                        </p>
                        <p class="font_size ">
                           <asp:Label ID="lblfilenamee" runat="server" ClientIDMode="Static">
                           </asp:Label>
                           <asp:FileUpload ID="upload" runat="server" CssClass="uploadcss" ClientIDMode="Static" />
                           <asp:HiddenField ID="hdnUploadFile" runat="server" ClientIDMode="Static" />
                           <asp:HiddenField ID="hdnUploadFile1" runat="server" ClientIDMode="Static" />
                        </p>
                        <br />
                        <asp:Label ID="lblDocName" runat="server" Text=""></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkDelete" ClientIDMode="Static" class="document_upload_delete" runat="server" OnClientClick="javascript:return confirm('Do you want to delete?');"
                           OnClick="lnkDelete_Click">Delete</asp:LinkButton>
                     </div>
                     <asp:TextBox ID="txtDocTitle" runat="server" ClientIDMode="Static" CssClass="uploadTxtField" placeholder="Title"
                        MaxLength="50"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" class="document_upload_error"
                        ControlToValidate="txtDocTitle" Display="Dynamic" ValidationGroup="t" ErrorMessage="Please enter Title"
                        ForeColor="Red"></asp:RequiredFieldValidator>
                     <textarea rows="10" class="uploadTxtField font_size" id="txtDescrition" runat="server"  placeholder="Description"></textarea>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" class="document_upload_error"
                        runat="server" ControlToValidate="txtDescrition" Display="Dynamic" ValidationGroup="t"
                        ErrorMessage="Please enter Description" ForeColor="Red"></asp:RequiredFieldValidator>
                     <br />
                     <asp:TextBox ID="txtAuthors" runat="server" ClientIDMode="Static" CssClass="uploadTxtField" placeholder="Authors name seperate by Comma"
                        MaxLength="50"  ></asp:TextBox>
                     <p class="addcontect padding_left" >
                        <b>Add context for this document</b>
                     </p>
                     <div class="categoryBox padding_left">
                        <asp:UpdatePanel ID="UpdateSub" runat="server">
                           <ContentTemplate>
                              <div class="categoryTxt upload_skorkel">
                                 <ul>
                                    <asp:ListView ID="lstSubjCategory" runat="server" OnItemCommand="LstSubjCategory_ItemCommand"
                                       OnItemDataBound="LstSubjCategory_ItemDataBound" GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder1"
                                       ItemPlaceholderID="itemPlaceHolder1">
                                       <LayoutTemplate>
                                          <table width="90%">
                                             <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                          </table>
                                       </LayoutTemplate>
                                       <GroupTemplate>
                                          <tr>
                                             <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                          </tr>
                                       </GroupTemplate>
                                       <ItemTemplate>
                                          <li id="SubLi" runat="server">
                                             <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                             <asp:HiddenField ID="hdnCountSub" runat="server" Value='<%#Eval("CountSub")%>' />
                                             <asp:LinkButton ID="lnkCatName" ForeColor="#646161"
                                                Font-Underline="false" runat="server" Text='<%#Eval("strCategoryName")%>' CommandName="Subject Category"></asp:LinkButton>
                                          </li>
                                       </ItemTemplate>
                                    </asp:ListView>
                                 </ul>
                              </div>
                              <div class="cls">
                              </div>
                              <div class="viewAll">
                                 <asp:LinkButton ID="lnkViewSubj" Text="View all" Font-Underline="false" runat="server"
                                    OnClick="lnkViewSubj_Click" ></asp:LinkButton>
                              </div>
                           </ContentTemplate>
                        </asp:UpdatePanel>
                     </div>
                     <div class="cls">
                     </div>
                     <p class="addcontect padding_left">
                        <b>Downloadable Documents</b>
                     </p>
                     <div class="uploadRadio">
                        <asp:CheckBox ID="rdDownloadYes" Text="&nbsp;Yes" GroupName="Download" ClientIDMode="Static"
                           runat="server" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="rdDownloadNo" Text="&nbsp;No" GroupName="Download" ClientIDMode="Static"
                           runat="server" />
                     </div>
                     <div class="cls">
                     </div>
                     <p class="addcontect padding_left" >
                        <b>Mark as</b>
                     </p>
                     <div class="uploadRadio">
                        <asp:CheckBoxList ID="ddlintDocsSee" runat="server" RepeatDirection="Horizontal"
                           class="display_inline">
                           <asp:ListItem Text="Private Document" Value="Private"></asp:ListItem>
                        </asp:CheckBoxList>
                     </div>
                     <div class="cls">
                     </div>
                     <asp:LinkButton ID="btnSave" CssClass="vote1" runat="server" 
                        Text="Upload Document" ValidationGroup="t" OnClick="btnSave_Click"></asp:LinkButton>
                  </div>
               </div>
            </div>
            <div class="cls">
            </div>
         </div>
         <!--left box ends-->
      </div>
   </div>
   <script type="text/javascript">
      $(document).ready(function () {
          if ($("#lblNotifyCount").text() == '0') {
              document.getElementById("divNotification1").style.display = "none";
          } else {
          }
          if ($("#lblInboxCount").text() == '0') {
              document.getElementById("divInbox").style.display = "none";
          } else {
      
          }
          $("#uploadTrigger").click(function () {
              $("#uploadDoc").click();
          });

            $(document).on('click', '.mobile_tab_icon', function() {
             $('.mobile_m_skorkel').slideToggle('slow');
            });
      
      });
      
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          var box;
          box = document.getElementById("dvDest");
          box.addEventListener("dragenter", OnDragEnter, false);
          box.addEventListener("dragover", OnDragOver, false);
          box.addEventListener("drop", OnDrop, false);
      });
      function OnDragEnter(e) {
          e.stopPropagation();
          e.preventDefault();
      }
      function OnDragOver(e) {
          e.stopPropagation();
          e.preventDefault();
      }
      function OnDrop(e) {
          e.stopPropagation();
          e.preventDefault();
          selectedFiles = e.dataTransfer.files;
          $('#lblfilenamee').text(selectedFiles[0].name);
          $('#hdnUploadFile1').val(selectedFiles[0].name);
          $('#upload').hide();
          var data = new FormData();
          for (var i = 0; i < selectedFiles.length; i++) {
              data.append(selectedFiles[i].name, selectedFiles[i]);
          }
          $.ajax({
              type: "POST",
              url: "handler/UploadDocument.ashx",
              contentType: false,
              processData: false,
              data: data,
              success: function (result) {
                  $('#hdnUploadFile').val(result);
                  $('#lnkDelete').show();
              },
              error: function () {
                  alert("There was error uploading files!");
              }
          });
      }
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          $('#rdDownloadYes').click(function () {
              $('#rdDownloadNo').prop('checked', false);
          });
          $('#rdDownloadNo').click(function () {
              $('#rdDownloadYes').prop('checked', false);
          });
      });
   </script>
</asp:Content>
