<%@ Page Title="" Language="C#" MasterPageFile="~/Main_Super.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="SA_ArticleListing.aspx.cs" Inherits="SA_ArticleListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlMsg" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnArticleIdGlob" runat="server" Value="" />
            <div class="main-section-inner">
                <!--                    <a href="#" class="back-link">&larr; Back to Q &amp; A</a>-->
                <div class="panel-cover clearfix">
                    <div class="full-width-con journal-con super-admin">

                        <div class="super-admin-comp">
                            <div class="btn-title-con m-t-15-minus">
                                <div>
                                    <h5 class="card-title ">Article Submissions</h5>
                                </div>

                            </div>
                            <!-- btn-title-con ended -->
                        </div>
                        <!-- super-admin-comp ended -->
                        <div class="modal show active backgroundoverlay" id="docModal" runat="server">
                            <div class="modal-dialog modal-dialog-centered" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabel">Add to Journal</h5>
                                  <!--       <asp:LinkButton ID="lnkClose" class="cross-btn" runat="server" OnClick="btnCancel_Click">
                                            <span aria-hidden="true">×</span>
                                        </asp:LinkButton> -->
                                    </div>
                                    <div class="modal-body text-left ">
                                        <form action="" method="">
                                            <div class="form-group">
                                                <label for="textbox">Article Title</label>
                                                <asp:textbox  CssClass="form-control" ID="txtPublishTitle" maxlength="100" placeholder="Please enter title of the article" runat="server"></asp:textbox>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPublishTitle"
                                                                    Display="Dynamic" ValidationGroup="article" ErrorMessage="Please enter article title."
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group">
                                                <label for="textbox">Select Journal</label>
                                               <asp:DropDownList ID="ddlJournals" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                                        </asp:DropDownList>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlJournals"
                                                                    Display="Dynamic" ValidationGroup="article" InitialValue="0" ErrorMessage="Please select journal."
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="submit-con">
                                                <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn add-scroller btn-outline-primary m-r-15">Cancel</asp:LinkButton>
                                                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" ValidationGroup="article" cssclass="btn btn-primary">Publish</asp:LinkButton>
                                            </div>
                                        </form>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <asp:ListView ID="lstArticles" runat="server" OnItemDataBound="lstArticles_ItemDataBound" OnItemCommand="lstArticles_ItemCommand">
                            <ItemTemplate>
                                <div class="card card-list-con">
                                    <div class="list-group-item top-list">
                                        <div class="post-con journal-con">

                                            <div class="post-header d-inline-block w-100">

                                                <span class="user-name-date no-margin hover-title">
                                                    <asp:LinkButton ID="lblArtTitle" runat="server" CommandName="Download" CssClass="user-name" Text='<%# Eval("Title") %>' ></asp:LinkButton>
                                                    
                                                    <ul class="small-date mt-2">
                                                        <li>
                                                            <span class="date mt-0">By <%# Eval("UserName") %></span>
                                                        </li>    
                                                        <li>
                                                            <span class="date mt-0"> <%# Eval("DateAdded") %></span>
                                                        </li>
                                                    </ul>        
                                                </span>
                                                <asp:HiddenField ID="hdnArticleID" runat="server" Value='<%# Eval("ArticleId") %>' />
                                                 <asp:HiddenField ID="hdnFilePath" runat="server" Value='<%# Eval("FilePath") %>' />
                                                <span class="more-btn float-right">

                                                    <span class="dropdown">
                                                        <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                            <img src="images/more.svg" alt="" class="more-btn">
                                                        </a>

                                                        <span class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                                            <asp:LinkButton runat="server" ID="lnkDownLoad" CssClass="dropdown-item" CommandName="Download"><span class="lnr icon-down-arrow"></span>&nbsp; Download</asp:LinkButton>
                                                        </span>
                                                    </span>
                                                </span>

                                            </div>
                                            <!-- post-header ended -->


                                            <div class="chip-cover p-b-r-l-15">
                                                <asp:ListView ID="lstJournal" OnItemCommand="lstJournal_ItemCommand" runat="server">
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="hdnJournalId" Value='<%# Eval("JournalID") %>' />
                                                        <asp:LinkButton ID="lnkJournalTital" runat="server" CssClass="chip"><%# Eval("JournalTitle") %> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <div class="text-right">
                                                    <asp:LinkButton runat="server" CommandName="AddToJournal" ID="lnkAddToJournal"><span class="icon-answer-edit-icon"></span>&nbsp;Add to Journal</asp:LinkButton>
                                                </div>
                                            </div>
                                            <!-- chip-cover ended -->

                                        </div>
                                        <!-- post-con ended -->
                                    </div>
                                    <!-- top-list ended -->
                                </div>
                                <!-- card ended -->
                            </ItemTemplate>
                        </asp:ListView>
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
                    </div>
                    <!-- full-width-con ended -->
                </div>
                <!-- panel-cover ended -->
            </div>

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
        //if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
        //        //$('#lnkNextshow').css("display", "block");
        //        $('#lnkNext').hide();
        //}
        //$("#dvPage a:enabled").on('click', function(event){showLoader1();});
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            //$("#dvPage a:enabled").on('click', function(event){showLoader1();});
            //if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
            //    //$('#lnkNextshow').css("display", "block");
            //    $('#lnkNext').hide();
            //}
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
              //$('#lnkNextshow').css("display", "block");
              $('#lnkNext').css("display", "none");
            }
          
          $("#dvPage a").on('click', function (event) { showLoader1(); });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                  //$('#lnkNextshow').css("display", "block");
                  $('#lnkNext').css("display", "none");
              }

              $("#dvPage a").on('click', function (event) { showLoader1(); });
            });
      });
    </script>
</asp:Content>
