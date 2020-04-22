<%@ Page Title="" Language="C#" MasterPageFile="~/Main_Super.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="SA_JournalDetails.aspx.cs" Inherits="SA_JournalDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlMsg" runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdnArticleGloId" Value='' />
            <asp:HiddenField runat="server" ID="hdnJournlaActive" Value="false" />
            <div class="main-section-inner">
                    <!--                    <a href="#" class="back-link">&larr; Back to Q &amp; A</a>-->
                    <div class="panel-cover clearfix">
                        <div class="full-width-con journal-con super-admin">
                           
                         <div class="journal-comp">
                          <div class="btn-title-con m-t-15-minus">
                            <div><h5 class="card-title ">Issue of Jan 2018</h5></div>
                           
                              <div class="">
                            <div class="material-toggle press">
                                                <input type="checkbox" id="checked" class="cbx hidden" checked="">
                                                <label for="checked" class="lbl"></label>
                                              </div>
                                
                                
                            </div>
                              
                        </div><!-- btn-title-con ended -->
                         </div><!-- journal-comp ended -->  
                             
                         <div class="super-admin-comp">   
                          <div class="btn-title-con   m-t-15-minus">
                            <div><h5 id="txtJournalName" runat="server" class="card-title "></h5></div>
                            <div class="m-download-con">
                                <ul class="list-inline" style="display:none">
                                    <li class="list-inline-item">
                                        <a href="#"></a>
                                        <a href="#"><img src="images/download_icon.svg"> Download All</a>
                                    </li>
                                   
                                </ul>
                                
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
                             <div class="material-toggle">
                                                                                <asp:CheckBox ID="checkedActive" AutoPostBack="true" runat="server" Checked='<%# Eval("Status") %>' OnCheckedChanged="checkedActive_CheckedChanged" CssClass=" hide-body-scroll" Text="&nbsp;"/>
                                                                               </div> 
                        </div><!-- btn-title-con ended -->

                         </div><!-- super-admin-comp ended -->

                            <asp:ListView ID="lstArticles" runat="server" OnItemDataBound="lstArticles_ItemDataBound" OnItemCommand="lstArticles_ItemCommand">
                                <ItemTemplate>
                                    <div class="card card-list-con">
                                        <div class="list-group-item top-list">
                                            <div class="post-con journal-con">

                                                <div class="post-header d-inline-block w-100">
                                                    <asp:HiddenField runat="server" ID="hdnArticleId" Value='<%# Eval("ArticleID") %>' />
                                                    <asp:HiddenField ID="hdnFilePath" runat="server" Value='<%# Eval("FilePath") %>' />
                                                    <span class="user-name-date no-margin hover-title">
                                                        <asp:LinkButton ID="lnkArtTItle" CommandName="Download" runat="server" class="user-name" Text='<%#Eval("ArtTitle") %>'></asp:LinkButton>
                                                        <span class="date">By <%#Eval("AddedByName") %></span>
                                                    </span>
                                                    <span class="more-btn float-right">

                                                        <span class="dropdown" id="divEdit" runat="server" >
                                                            <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                <img src="images/more.svg" alt="" class="more-btn" />
                                                            </a>

                                                            <span class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                                                <asp:LinkButton ID="lnkRemove" runat="server" CssClass="dropdown-item hide-body-scroll" OnClientClick="javascript:showDelete();"  CommandName="Remove"><span class="lnr lnr-trash"></span> Remove</asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDownload" runat="server" CssClass="dropdown-item hide-body-scroll"  CommandName="Download"><span class="lnr icon-down-arrow"></span>  Download</asp:LinkButton>
                                                               <%-- <a class="dropdown-item" href="#"><span class="lnr lnr-trash"></span>Delete</a>--%>
                                                            </span>
                                                        </span>
                                                    </span>
                                                </div>
                                                <!-- post-header ended -->

                                                <div class="post-footer">
                                                    <ul class="list-inline">
                                                        <li class="list-inline-item"><span  class="active-toogle"><span class="like-btn"></span> <%#Eval("Likes") %> <%# ((int)Eval("Likes") == 1) ? "Like" : "Likes" %></span></li>
                                                        <li class="list-inline-item"><span  class="active-toogle"><span class="comment-btn"></span><%#Eval("Comments") %> <%# ((int)Eval("Comments") == 1) ? "Comment" : "Comments" %></span></li>
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
                        
                        </div><!-- full-width-con ended -->
                    </div><!-- panel-cover ended -->
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" >
         function showDelete() {
            $('#divDeletesucess').css("display", "block");
        }
        function hideDelete() {
            $('#divDeletesucess').css("display", "none");
        }
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
                             OnClientClick="javascript:hideDelete();"   OnClick="lnkDeleteCancel_Click"></asp:Button>
                            <asp:Button ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="javascript:hideDelete();"
                                      OnClick="lnkDeleteConfirm_Click" CssClass="btn btn-primary add-scroller" ></asp:Button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
