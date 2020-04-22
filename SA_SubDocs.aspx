<%@ Page Title="" Language="C#" MasterPageFile="~/Main_Super.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="SA_SubDocs.aspx.cs" Inherits="SA_SubDocs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlMsg" runat="server">
        <ContentTemplate>
            <div class="main-section-inner">
          
                <div class="panel-cover clearfix">
                    <div class="full-width-con journal-con super-admin">

                        <div class="super-admin-comp form-inline mb-0">
                            <div class="search-filter-con row btn-title-con m-t-15-minus">
                                <div  class="col-sm-6 col-xs-12 col-12">
                                    <h5 class="card-title ">Verification Documents</h5>
                                </div>
                                <asp:UpdatePanel ID="pnlSearch" class="search-bar col-sm-4 clearfix margin-bottom-m col-xs-12 col-12" runat="server" UpdateMode="Conditional">
                                 <ContentTemplate>
                     
                                  <div class="w-100 search-cover">
                                     <asp:TextBox ID="txtPplSearch" runat="server" autocomplete="off" placeholder="Search People" ClientIDMode="Static" class="form-control p-r-0"
                                      onkeydown="return postSearchPpl.bind(this)(event)">
                                     </asp:TextBox>
                                     <input type="button" id="searchBtn" class="search-btn-2" value="" onclick="callPplSearch();">
                                  </div>   
                    
        
                                  <asp:Button ID="btnPplSearch" runat="server" ClientIDMode="Static" OnClick="btnPplSearch_Click" Width="50px" Height="30px" Text="Search" Style="display: none" />
                                  <asp:HiddenField ID="hdnSearch" runat="server" Value='' ClientIDMode="Static" />
                                 </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <!-- btn-title-con ended -->
                        </div>
                        <!-- super-admin-comp ended -->
                        
                        <asp:ListView ID="lstSubDocs" runat="server" OnItemDataBound="lstSubDocs_ItemDataBound" OnItemCommand="lstSubDocs_ItemCommand">
                            <ItemTemplate>
                                <div class="card card-list-con margin-bottom-30">
                                    <div class="list-group-item top-list">
                                       <div class="post-body p-3">
                                           <div class="media align-items-center">
                                              <%--<img class="mr-3 search-avatar rounded-circle" src="images/avatar3.jpg" alt="">--%>
                                              <asp:ImageButton id="imgprofile" runat="server" class="mr-3 img-avatar rounded-circle" ImageUrl='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' 
                                               CommandName="Download" />
                                              <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
                                              <div class="media-body">
                                               <h5 class="mt-0 mb-0">
                                                <asp:LinkButton ID="lblName" runat="server" class="un-anchor truncate cursor-pointer" Text='<%# Eval("UserName") %>' CommandName="Download">
                                                </asp:LinkButton>
                                               </h5>
                                               
                                                    <asp:HiddenField ID="hdnFilePath" runat="server" Value='<%# Eval("FilePath") %>' />
                                                  <asp:HiddenField ID="hdnFileName" runat="server" Value='<%# Eval("FileName") %>' />
                                               <span class="member-group">
                                               
                                                <span>
                                                    <span class="mr-1">Submitted on <%# Eval("DateAdded") %></span>
                                                 
                                                </span>
                                               </span>
                                              </div>
                                             </div>
                                            <!-- post-header ended -->
                                           
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
     
       
    </script>
    <script type="text/javascript">

        function callPplSearch() {
            if ($("#txtPplSearch").val()) {
                $('#btnPplSearch').click();
               showLoader1();
               return false;
            }
            return false;
        }
        function postSearchPpl(e) {
           if (e.keyCode == 13 && $(this).val()) {
               $('#btnPplSearch').click();
               showLoader1();
               return false;
           } else if (e.keyCode == 13) {
               return false;
           }
       }
        $(document).ready(function () {
            hideLoader1();
            if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
              $('#lnkNext').css("display", "none");
            }
         
          $("#dvPage a").on('click', function (event) { showLoader1(); });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                hideLoader1();
                if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                  $('#lnkNext').css("display", "none");
              }

              $("#dvPage a").on('click', function (event) { showLoader1(); });
            });
      });
    </script>
</asp:Content>
