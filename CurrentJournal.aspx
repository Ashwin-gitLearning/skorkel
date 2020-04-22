<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CurrentJournal.aspx.cs" Inherits="CurrentJournal" %>
<%@ Register TagPrefix="usc" TagName="UserControl_DragNDrop" Src="~/UserControl/DragNDrop.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlMsg" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnJournalId" runat="server" />
            <div class="main-section-inner">
                    <!--                    <a href="#" class="back-link">&larr; Back to Q &amp; A</a>-->
                    <div class="panel-cover clearfix">
                        <div class="full-width-con journal-con super-admin">
                           
                         <div class="journal-comp">
                          <div class="btn-title-con m-t-15-minus">
                            <div><h5 class="card-title ">Issue of Jan 2018</h5></div>
                           
                              <div class="">
                                  
                            <div class="material-toggle press">
                                                <input type="checkbox" id="checked" class="cbx " checked="">
                                                <label for="checked" class="lbl"></label>
                                              </div>
                                
                                
                            </div>
                              
                        </div><!-- btn-title-con ended -->
                         </div><!-- journal-comp ended -->  
                             
                         <div class="super-admin-comp">   
                          <div class="btn-title-con m-t-15-minus">
                            <div><h5 id="txtJournalName" runat="server" class="card-title "></h5></div>
                            <div class="m-download-con ml-2 mt-0 d-flex align-items-center">

                                 
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
                                                            <asp:LinkButton ID="lnkCancel" runat="server" CssClass="btn btn-outline-primary m-r-15 add-scroller" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-primary hide-body-scroll" ValidationGroup="article" OnClick="lnkSave_Click">Submit</asp:LinkButton><%--OnClientClick="showLoader1();"--%>
                                                        </div>
                                                            </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                <ul class="list-inline d-flex">
                                    <li class="list-inline-item">
                                        
                                        <asp:LinkButton runat="server" ID="lnkDownloadAll" OnClick="lnkDownloadAll_Click" CssClass="d-flex">
                                        <span class="lnr lnr-cloud-download hide-ios"></span> &nbsp;<span class="display-none-m">Download All</span></asp:LinkButton>
                                    </li>
                                   
                                </ul>
                                                                   <asp:LinkButton OnClick="lnkAddArticle_Click" ID="lnkAddArticle" runat="server" CssClass="btn hide-button-ios btn-outline-primary hide-body-scroll ml-2">Add Article</asp:LinkButton>
                                                                   <a href="javascript:void(0);" id="lnkuploadIOS" Class="btn btn-outline-primary hide-body-scroll">Add Article</a>
                                
                                <div class="modal fade" id="docModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
                                    <div class="modal-dialog modal-dialog-centered" role="document">
                                                            <div class="modal-content">
                                                                <div class="modal-header">
                                                                    <h5 class="modal-title" id="exampleModalLabel">Add Article</h5>
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                              <span aria-hidden="true">×</span>
                                                                            </button>
                                                                </div>
                                                                <div class="modal-body text-left ">
                                                                    <form action="" method="">
                                                                        <div class="form-group">
                                                                            <label for="textbox">Article Title</label>
                                                                            <input type="text" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter Name Of Your Article">
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label for="textarea">Upload Documents</label>
                                                                            <div class="form-group drag-demo"></div>
                                                                            <span class="grey-text font-size-12" >Note: Only PDF Support, Max File Size 5MB</span>
                                                                        </div>
                                                                        
                                                                        <div class="form-group">
                                                                           <div class="custom-checkbox">
                                                                                    <input id="i2" type="checkbox" checked="">
                                                                                    <label for="i2" class="font-size-12 line-height-16 text-transform-normal">I affirm that this article represents bonafide work done by me and is my own original creation. I understand that I am personally and exclusively liable for any claims raised over the authenticity and originality of the content contained in my article</label>
                                                                                </div>
                                                                        </div>


                                                                       

                                                                        <div class="submit-con p-both-15">
                                                                            <button class="btn btn-outline-primary m-r-15">Cancel</button>
                                                                            <button class="btn btn-primary">Save</button>
                                                                        </div>
                                                                    </form>
                                                                </div>

                                                            </div>
                                                        </div>
                                </div>
                                <!-- Modal ended -->
                                
                            </div>
                        </div><!-- btn-title-con ended -->
                         </div><!-- super-admin-comp ended -->
                            <asp:ListView ID="lstArticles" runat="server" OnItemCommand="lstArticles_ItemCommand"  OnItemDataBound="lstArticles_ItemDataBound">
                                <ItemTemplate>
                                    <div class="card card-list-con">
                                        <div class="list-group-item top-list">
                                            <div class="post-con journal-con">
                                                <asp:HiddenField ID="hdnArticleId" runat="server" Value='<%#Eval("ArticleID") %>' />
                                                <asp:HiddenField ID="hdnFilePath" runat="server" Value='<%# Eval("FilePath") %>' />
                                                <div class="post-header d-inline-block w-100">

                                                    <span class="user-name-date no-margin hover-title">
                                                        <asp:LinkButton ID="lnkArtTItle" CommandName="ArticleDetails" runat="server" class="user-name line-height-17 mb-1" Text='<%#Eval("ArtTitle") %>'></asp:LinkButton>
                                                        by <asp:LinkButton ID="lnkAddedBy" href='<%# "Profile2.aspx?RegId="+Eval("AddedByID") %>' runat="server" CssClass="remove-a-color" Text='<%# Eval("AddedByName") %>'></asp:LinkButton>
                                                    </span>
                                                    <span class="more-btn float-right hide-ios">

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
                                                        <li class="list-inline-item"><span id="spnLike" runat="server" class="active-toogle"><span class="like-btn"></span> <%#Eval("Likes") %> <%# ((int)Eval("Likes") == 1) ? "Like" : "Likes" %></span></li>
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
    </script>

</asp:Content>
