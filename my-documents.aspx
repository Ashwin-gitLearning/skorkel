<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="my-documents.aspx.cs" Inherits="my_documents" %>

<%@ Register TagPrefix="usc" TagName="UserControl_DragNDrop" Src="~/UserControl/DragNDrop.ascx" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-section-inner">

        <!--inner container ends-->
        <asp:Panel ID="PnlDocuments" UpdateMode="Conditional" Visible="true" runat="server">
            <div class="custom-nav-container tab-pane container ative show" id="document">
                <div class="">
                    <div class="btn-title-con m-t-15-minus align-items-start">
                        <div>
                            <h5 class="card-title">My Documents</h5>
                        </div>
                        <div>
                            <asp:LinkButton class="btn hide-body-scroll btn-outline-primary" ID="lnkuploadDoc" runat="server" ClientIDMode="Static"
                                Text="Upload Document" OnClick="lnkuploadDoc_click"></asp:LinkButton>
                            <asp:LinkButton class="btn hide-body-scroll btn-outline-primary" ID="lnkuploadIOS" runat="server" ClientIDMode="Static"
                                Text="Upload Document"></asp:LinkButton>

                            <%--OnClientClick="CallUploadDoc();"--%>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="Panel1" UpdateMode="Always" Visible="true" runat="server">
                        <ContentTemplate>
                            <div class="row" id="myDocument" runat="server">
                                <asp:HiddenField ID="hdnintdocIdelete" Value="" ClientIDMode="Static" runat="server" />
                                <asp:HiddenField ID="hdnstrdocDescriptiondele" Value="" ClientIDMode="Static" runat="server" />
                                <asp:HiddenField ID="hdnfilestrFilePathe" Value="" ClientIDMode="Static" runat="server" />
                                <asp:ListView ID="LstDocument" runat="server" GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder1"
                                    OnItemCommand="LstDocument_ItemCommand" OnItemDataBound="LstDocument_ItemDataBound"
                                    ItemPlaceholderID="itemPlaceHolder1">
                                    <LayoutTemplate>
                                        <table id="tblDoc" cellpadding="0" cellspacing="0" class="position_static">
                                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                        </table>
                                    </LayoutTemplate>
                                    <GroupTemplate>
                                        <tr>
                                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                        </tr>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <div class="col-sm-2 col-6 m-box">
                                            <div class="doc-card">
                                                <span class="more-btn float-right" runat="server" id="editUserComment">
                                                    <span class="dropdown">
                                                        <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                            <img src="images/more.svg" alt="" class="more-btn">
                                                        </a>
                                                        <asp:HiddenField ID="hdnDocId" Value='<%#Eval("intDocId") %>' runat="server" />
                                                        <asp:HiddenField ID="hdnDocsSee" Value='<%#Eval("intDocsSee") %>' runat="server" />
                                                        <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
                                                        <asp:HiddenField ID="hdnMaxcount" Value='<%#Eval("Maxcount") %>' runat="server" ClientIDMode="Static" />
                                                        <asp:HiddenField ID="hdnstrFilePath" Value='<%#Eval("strFilePath") %>' runat="server"
                                                            ClientIDMode="Static" />
                                                        <asp:HiddenField ID="hdnstrDocTitle" Value='<%#Eval("strDocTitle") %>' runat="server"
                                                            ClientIDMode="Static" />
                                                        <asp:HiddenField ID="hdnIsDocsDownload" Value='<%#Eval("IsDocsDownload") %>' runat="server"
                                                            ClientIDMode="Static" />
                                                        <span class="dropdown-menu" id="updoc" onclick="updocClick.bind(this)()" aria-labelledby="dropdownMenuLink" x-placement="bottom-start" style="position: absolute; transform: translate3d(0px, 17px, 0px); top: 0px; left: 0px; will-change: transform;">
                                                            <asp:HiddenField ID="hdnintdocIdelet" Value='<%# Eval("intDocId") %>' ClientIDMode="Static"
                                                                runat="server" />
                                                            <asp:HiddenField ID="hdnstrdocDescriptiondel" Value='<%# Eval("strDocTitle") %>'
                                                                ClientIDMode="Static" runat="server" />
                                                            <asp:HiddenField ID="hdnfilestrFilePath" Value='<%# Eval("strFilePath") %>' ClientIDMode="Static"
                                                                runat="server" />
                                                            <asp:LinkButton ID="lnkEditDoc" CommandName="EditDocs" runat="server" CssClass="hide-body-scroll dropdown-item"><span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDeleteDoc" Visible="true" CssClass="dropdown-item hide-body-scroll" OnClientClick="javascript:docdelete();return false;"
                                                                ToolTip="Delete" Text="Delete" CommandName="DeleteDocs" CausesValidation="false"
                                                                runat="server"><span class="lnr lnr-trash"></span> Delete                                       
                                                            </asp:LinkButton>
                                                            <%--  <a class="dropdown-item" href="#"><span class="lnr lnr-trash"></span>Delete</a>--%>
                                                        </span>
                                                    </span>
                                                </span>
                                                <img src="images/document.png" alt="" class="doc-img" />
                                                <hr>
                                                <asp:Label ID="lblDocument" class="doc-text truncate" runat="server" Text='<%#Eval("strDocTitle") %>' Style="display: none;"></asp:Label>
                                                <asp:HyperLink ID="lblDocument1" ClientIDMode="Static" class="doc-text truncate" Target="_blank" ToolTip="Download file" NavigateUrl='<%# "~/UploadDocument/"+Eval("strFilePath")%>'
                                                    Text='<%#Eval("strDocTitle") %>' Font-Size="Small" runat="server"></asp:HyperLink>
                                            </div>

                                        </div>

                                    </ItemTemplate>

                                </asp:ListView>

                                <div id="divEmptyResult" class="mlr-15 blank-page-message card card-list-con" clientidmode="static" runat="server">
                                    <p>You have not uploaded any documents yet - Please upload your documents here.</p>
                                </div>
                            </div>

                            <div class="modal backgroundoverlay show" id="divDocumentUplaod" runat="server" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="exampleModalLabel">Add Document</h5>
                                            <asp:LinkButton OnClick="lnkCancelDoc_Click" ID="closedo" CssClass="close" runat="server">
                                                                <span aria-hidden="true">&times;</span>
                                            </asp:LinkButton><%--OnClientClick="javascript:callDoccancel();"--%>
                                        </div>
                                        <div class="modal-body text-left ">

                                            <div class="form-group ">
                                                <usc:UserControl_DragNDrop OnDelete="upldrFile_delete" ID="ddUploader1" ClientIDMode="Static" runat="server" />
                                                <!--- <span class="grey-text mt-1 inline-block font-size-12">Note: Only PDF or DOC/DOCX file Support, Max File Size 5MB</span> -->
                                                <div id="uploadPopopError" runat="server" forecolor="Red" class="errorMsg error-msz" clientidmode="static" style="display: none;">Test</div>
                                            </div>

                                            <div class="form-group">
                                                <label for="textbox">Name of Document</label>
                                                <asp:TextBox ID="txtDocTitle" autocomplete="off" runat="server" MaxLength="50" ClientIDMode="Static" class="form-control"
                                                    ValidationGroup="docUpload" placeholder="Enter Document Name"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="textarea">Description</label>
                                                <textarea id="txtDescrition" autocomplete="off" rows="5" runat="server" maxlength="500" placeholder="Description" class="form-control"></textarea>
                                            </div>
                                            <div class="form-group remove-flex">
                                                <label for="textbox">Context</label>
                                                <uc:UserControl_MultiSelect class="form-control" ID="lstSubjCategory" ClientIDMode="Static" runat="server" />
                                            </div>


                                            <div class="form-group">
                                                <label for="textarea">Access</label><br />

                                                <div class="form-check form-check-inline radio-itmes">
                                                    <div class="custom-checkbox">
                                                        <asp:CheckBox ID="imgPrivate" CssClass="custom-checkbox" Text="Make This Private" ClientIDMode="Static"
                                                            runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-check form-check-inline radio-itmes">
                                                    <div class="custom-checkbox">
                                                        <asp:CheckBox ID="imgDownload" CssClass="custom-checkbox" Text="Downloadable" ClientIDMode="Static"
                                                            runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <p>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDocTitle"
                                                    Display="Dynamic" ValidationGroup="docUpload" ErrorMessage="Please Enter Document Name"
                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                            </p>
                                            <p>
                                                <asp:Label runat="server" Visible="false" ForeColor="Red" CssClass="RedErrormsg" Text="" ID="lblErrorMsg"></asp:Label>
                                            </p>
                                            <div class="submit-con">
                                                <asp:LinkButton ID="lnkCancelDoc" CssClass="btn btn-outline-primary  add-scroller m-r-15" runat="server" Text="Cancel"
                                                    ClientIDMode="Static" OnClick="lnkCancelDoc_Click"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkSave" CssClass="btn btn-primary" runat="server" Text="Save"
                                                    ValidationGroup="docUpload" OnClick="btnSave_Click" OnClientClick="javascript:callDocSave();" ClientIDMode="Static"></asp:LinkButton>
                                                <%--OnClientClick="javascript:callDocSave();"OnClientClick="javascript:callDoccancel();"--%>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </asp:Panel>
        <script type="text/javascript">
            function callDocSave() {
                if ($('#txtDocTitle').val() != '') {
                    showLoader1();
                }
            }
            function docdelete() {
                $('#divDeletesucess').css("display", "block");
                $('#AddWorkExp').css("display", "none");
            }
            function OverlayBody() {
                $('#bodyelement').addClass("remove-scroller");
            }
            function updocClick(docid, desc, filepath) {
                $('#hdnintdocIdelete').val($(this).children('#hdnintdocIdelet').val());
                $('#hdnstrdocDescriptiondele').val($(this).children('#hdnstrdocDescriptiondel').val());
                $('#hdnfilestrFilePathe').val($(this).children('#hdnfilestrFilePath').val());
            }
            function divCancels() {
                $('#hdnintPostId').val('');
                $('#hdnintPostceIdelet').val('');
                $('#hdnintacheIdelet').val('');
                $('#hdnintedueIdelet').val('');
                $('#hdnintdocIdelete').val('');
                $('#divDeletesucess').css("display", "none");
                $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 0px #00B7E5");
                $('#divDeletesucess').hide();
            }
        </script>
        <!---Sucess Popup-->
        <div id="divDeletesucess" class="modal backgroundoverlay" clientidmode="Static" runat="server"
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
                                <h5 class="modal-title">Delete Confirmation
                                </h5>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblConnDisconn" runat="server" Text="Do you want to delete?"></asp:Label>
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
    </div>
    <!-- main-section-inner ended here -->
</asp:Content>
