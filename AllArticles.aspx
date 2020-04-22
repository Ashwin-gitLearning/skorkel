<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="AllArticles.aspx.cs" Inherits="AllArticles" %>
<%@ Register TagPrefix="usc" TagName="UserControl_DragNDrop" Src="~/UserControl/DragNDrop.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlMsg" runat="server">
        <ContentTemplate>
            <div class="main-section-inner">
                    <!--                    <a href="#" class="back-link">&larr; Back to Q &amp; A</a>-->
                    <div class="panel-cover clearfix">
                        <div class="full-width-con journal-con super-admin">
                           <div class="d-flex justify-content-between">
                         <div class="form-inline col-lg-4 clearfix margin-bottom-m p-0 col-sm-6 col-6">
                              <div class="search-filter-con w-100">
                                    <asp:UpdatePanel ID="pnlSearch" runat="server" UpdateMode="Conditional" class="search-bar w-100">
                                     <ContentTemplate>
                                      <div class="w-100 search-cover">
                                       <asp:TextBox ID="txtArticleSearch" runat="server" autocomplete="off" placeholder="Search Articles" ClientIDMode="Static" class="form-control p-r-0"
                                        onkeydown="return postSearchArticle.bind(this)(event)">
                                       </asp:TextBox>
                                       <input type="button" id="searchBtn" class="search-btn-2" value="">
                                      </div>
                                      <asp:Button ID="btnArticleSearch" runat="server" ClientIDMode="Static" OnClick="btnArticleSearch_Click" Width="50px" Height="30px" Text="Search" Style="display: none" />
                                      <asp:HiddenField ID="hdnSearch" runat="server" Value='' ClientIDMode="Static" />
                                     </ContentTemplate>
                                    </asp:UpdatePanel>
                              
                               </div>

                           </div>
                          <div class="super-admin-comp col-lg-4 clearfix margin-bottom-m p-0 col-sm-6 col-6 text-right">   
                       
                            <div class="m-download-con ml-2 mt-0">

                                 
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
                                                            <asp:LinkButton ID="lnkSave" CssClass="btn btn-primary hide-body-scroll" ValidationGroup="article" OnClick="lnkSave_Click" runat="server">Submit</asp:LinkButton>
                                                        </div>
                                                            </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                
                                                                   <asp:LinkButton OnClick="lnkAddArticle_Click" ID="lnkAddArticle" runat="server" CssClass="btn btn-outline-primary hide-body-scroll ml-2">Add Article</asp:LinkButton>
                                
                               
                                
                            </div>
                         </div><!-- super-admin-comp ended -->

</div>
                             
                         
                            <asp:ListView ID="lstArticles" runat="server" OnItemCommand="lstArticles_ItemCommand" OnItemDataBound="lstArticles_ItemDataBound">
                                <ItemTemplate>
                                    <div class="card card-list-con">
                                        <div class="list-group-item top-list">
                                            <div class="post-con journal-con">
                                                <asp:HiddenField ID="hdnArticleId" runat="server" Value='<%#Eval("ArticleID") %>' />
                                                <asp:HiddenField ID="hdnFilePath" runat="server" Value='<%# Eval("FilePath") %>' />
                                                <asp:HiddenField ID="hdnJournalId" runat="server" Value='<%# Eval("JournalID") %>' />
                                                <div class="post-header d-inline-block w-100">

                                                    <span class="user-name-date no-margin hover-title">
                                                        <asp:LinkButton ID="lnkArtTItle" CommandName="ArticleDetails" runat="server" class="user-name mb-1" Text='<%#Eval("ArtTitle") %>'></asp:LinkButton>
                                                        <ul class="small-date">
                                                            <li>
                                                                by <asp:LinkButton ID="lnkAddedBy" href='<%# "Profile2.aspx?RegId="+Eval("AddedByID") %>' runat="server" CssClass="remove-a-color" Text='<%# Eval("AddedByName") %>'></asp:LinkButton>
                                                            </li>
                                                            <li>
                                                                <span ><%# Eval("JournalTitle") %></span>
                                                            </li>
                                                        </ul>
                                                          
                                                    </span>
                                                   
                                                    <span class="more-btn float-right">

                                                        <span class="dropdown">
                                                            <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                <img src="images/more.svg" alt="" class="more-btn" />
                                                            </a>

                                                            <span class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                                                <asp:LinkButton ID="lnkDownLoad" runat="server" CssClass="dropdown-item" CommandName="Download"><span class="lnr icon-down-arrow"></span> Download</asp:LinkButton>
                                                               <%-- <a class="dropdown-item" href="#"><span class="lnr lnr-trash"></span>Delete</a>--%>
                                                            </span>
                                                        </span>
                                                    </span>
                                                </div>
                                                <!-- post-header ended -->

                                                <div class="post-footer">
                                                    <ul class="list-inline">
                                                        <li class="list-inline-item"><span id="spnLike" runat="server"   class="active-toogle"><span class="like-btn"></span> <%#Eval("Likes") %> <%# ((int)Eval("Likes") == 1) ? "Like" : "Likes" %></span></li>
                                                        <li class="list-inline-item"><span href="#" class="active-toogle"><span class="comment-btn"></span><%#Eval("Comments") %> <%# ((int)Eval("Comments") == 1) ? "Comment" : "Comments" %></span></li>
                                                    </ul>
                                                </div>
                                                <!-- post-footer ended -->

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
                                            <!--pagination end-->
                                             </div>  
                        </div><!-- full-width-con ended -->
                    </div><!-- panel-cover ended -->
             
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" >
        function OverlayBody() {
            $('#bodyelement').addClass("remove-scroller");
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

        function postSearchArticle(e) {
         if (e.keyCode == 13 && $(this).val()) {
             $('#btnArticleSearch').click();
             showLoader1();
             return false;
         } else if (e.keyCode == 13) {
             return false;
         }
     }
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
