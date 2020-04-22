<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="JournalListing.aspx.cs" Inherits="JournalListing" %>
<%@ Register TagPrefix="usc" TagName="UserControl_DragNDrop" Src="~/UserControl/DragNDrop.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlMsg" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnIsFiltered" runat="server" Value="false" />
            <div class="main-section-inner">

                <div class="panel-cover clearfix">
                    <div class="full-width-con super-admin">
                        <div class="super-admin-comp">
                            <div class="btn-title-con d-flex  m-t-15-minus">
                                <div>
                                    <h5 class="card-title ">Journals</h5>
                                </div>
                                <div class="">
                                 
                                            <asp:LinkButton OnClick="lnkAddArticle_Click" ID="lnkAddArticle" runat="server" CssClass="btn btn-outline-primary hide-button-ios hide-body-scroll">Add Article</asp:LinkButton>
                                            <a href="javascript:void(0);" id="lnkuploadIOS" Class="btn btn-outline-primary hide-body-scroll">Add Article</a>
                                 
                                    <div class="modal fade show backgroundoverlay" id="docModal1" runat="server">
                                        <div class="modal-dialog modal-dialog-centered" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title" id="exampleModalLabel">Add Article</h5>
                                                    <asp:LinkButton CssClass="close" ID="lnkCross" runat="server" OnClick="lnkCancel_Click" >
                                                        <span aria-hidden="true">×</span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="modal-body text-left ">
                                                    <asp:UpdatePanel ID="pnlArt" runat="server">
                                                        <ContentTemplate>
                                                        <div class="form-group">
                                                            <label for="textbox">Article Title</label>
                                                            <asp:TextBox  CssClass="form-control"  autocomplete="off" ID="txtTitle" MaxLength="100" ValidationGroup="article" runat="server" placeholder="Enter Name Of Your Article"></asp:TextBox>
                                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                                                    Display="Dynamic" ValidationGroup="article" ErrorMessage="Please enter document name."
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="textarea">Upload Documents</label>
                                                            <usc:UserControl_DragNDrop ID="ddUploader1" ClientIDMode="Static" runat="server" />
                                                            <div class="grey-text font-size-12">Note: Only PDF Support, Max File Size 5MB</div>
                                                            <asp:Label Visible="false" ID="lblErrorUpload" ForeColor="Red" CssClass="RedErrormsg" Text="" runat="server"></asp:Label>
                                                        </div>

                                                        <div class="form-check p-0">
                                                            <div class="font-size-12 line-height-16 text-transform-normal custom-checkbox">
                                                                <asp:CheckBox ID="chkConfirm" CssClass="custom-checkbox-inline input-m-align" runat="server" Text="I affirm that this article represents bonafide work done by me and is my own original creation. I understand that I am personally and exclusively liable for any claims raised over the authenticity and originality of the content contained in my article" Checked="true" ValidationGroup="article" />
                                                            </div>
                                                              <asp:Label Visible="false" ID="lblErrorConf" ForeColor="Red" CssClass="RedErrormsg display-inline pl-4" Text="" runat="server"></asp:Label>
                                                        </div>




                                                        <div class="submit-con">
                                                            <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click" CssClass="btn btn-outline-primary m-r-15 add-scroller">Cancel</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-primary hide-body-scroll" ValidationGroup="article" OnClick="lnkSave_Click">Submit</asp:LinkButton><%--OnClientClick="showLoader1();"--%>
                                                        </div>
                                                            </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <!-- Modal ended -->

                                </div>
                            </div>
                            <!-- btn-title-con ended -->
                        </div>
                        <!-- super-admin-comp ended -->
                        <asp:UpdatePanel ID="upJournals" runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="lstJournals"  OnItemCommand="lstJournals_ItemCommand" runat="server">
                                    <ItemTemplate>
                                        <div class="card card-list-con">
                                            <div class="list-group-item top-list">
                                                <div class="post-con">
<!--                                                     <div class="post-header justify-content-between">
                                                        <div>
                                                            <span class="question-icon">
                                                                <span class="icon">
                                                                    <img src="images/newspaper-blue.svg"></span>
                                                            </span>
                                                            <ul class="que-con">
                                                                <li>Journal</li>
                                                            </ul>
                                                        </div>
                                                       

                                                    </div> -->
                                                    
                                                    <div class="post-body p-b-15">
                                                        <div class="material-toggle super-admin-comp">
                                                            <%--  <asp:UpdatePanel ID="pnlUpdateCheck" runat="server">
                                                                    <ContentTemplate>--%>
                                                            <asp:HiddenField ID="hdnJournalId" Value='<%# Eval("JournalId") %>' runat="server" />
                                                            <%--    </ContentTemplate>
                                                                </asp:UpdatePanel>--%>
                                                            <%--   <label for="checkedActive" class="lbl"></label>--%>
                                                       </div>
                                                            <span class="more-btn float-right hide-ios">

                                                        <span class="dropdown">
                                                            <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                <img src="images/more.svg" alt="" class="more-btn">
                                                            </a>

                                                            <span class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                                                <asp:LinkButton ID="jhg" runat="server" CssClass="dropdown-item" CommandName="Download"><span class="lnr icon-down-arrow"></span> Download</asp:LinkButton>
                                                               
                                                            </span>
                                                        </span>
                                                    </span>                                                        
                                                        <h3>
                                                            <asp:LinkButton runat="server" ID="lnkJournal" CommandName="Details" Text='<%#Eval("JournalTitle") %>'></asp:LinkButton>
                                                        </h3>
                                                        
                                                        <p class="small-date light-black-text d-flex align-items-center m-t-10">
                                                            <img src="images/writing.svg" alt="" />&nbsp;  <%#Eval("ArticleCount") %> &nbsp; <span runat="server" id="ArticleTex"> <%# Convert.ToInt32(Eval("ArticleCount"))==1 ? " Article":" Articles"%></span>
                                                        </p>

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
                                                    <%--<asp:LinkButton ID="lnkNextshow" runat="server" OnClientClick="return false;" Style="display: none;" ClientIDMode="Static"><img class="opt" src="images/nextpaging.jpg" alt="" /></asp:LinkButton>--%>
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
                                             
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <!-- full-width-con ended -->
                </div>
                <!-- panel-cover ended -->

            </div>
   



            <!-- main-section inner ended -->

            <!-- main-section-inner ended -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function OverlayBody() {
            $('#bodyelement').addClass("remove-scroller");
        }
        function RemoveOverlay() {
            $('#bodyelement').removeClass("remove-scroller");
        }
        function ShowAddJournal() {
            $('#docModal1').show();
        }

        $('.selectedOption').click(function () {
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

</asp:Content>
