<%@ Page Title="" Language="C#" MasterPageFile="~/Main_Super.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="SA_JournalListing.aspx.cs" Inherits="SA_JournalListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlMsg" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnIsFiltered" runat="server" Value="false" />
            <asp:HiddenField ID="hdnJournalIDGlo" runat="server" Value="false" />
            <div class="main-section-inner">
                    
                    <div class="panel-cover clearfix">
                        <div class="full-width-con super-admin">   
                            <div class="super-admin-comp">
                             <div class="btn-title-con s-journal-heading m-t-15-minus">
                            <div><h5 class="card-title ">Existing Journals</h5></div>
                            <div class="">
                                <ul class="list-inline d-flex align-items-center">
                                    
                                    <li class="list-inline-item">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="">
                                                <ContentTemplate>
                                          <div class="dropdown  d-flex align-items-center">
                                            <span class="sortFilter" id="sortFilter" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Filter by: &nbsp; <strong id="txtFilter" runat="server"> Active Journals</strong> <span class="icon-caret-down align-items-center"></span></span>
                                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                <asp:LinkButton class="dropdown-item" runat="server" ID="LnkActive" OnClick="LnkActiveClick" Text="Active"></asp:LinkButton>
                                                <asp:LinkButton class="dropdown-item" ID="LnkInActive" runat="server" OnClick="LnkInActiveClick" Text="Inactive"></asp:LinkButton>

                                            </div>
                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                    </li>
                                    
                                    <li class="list-inline-item">
                                     <asp:LinkButton ID="lnkArts" runat="server" href="SA_ArticleListing.aspx" CssClass="m-r-10">Article Submission</asp:LinkButton>
                                     <asp:LinkButton ID="lnkAddJournal" OnClick="lnkAddJournal_Click" runat="server" CssClass="btn btn-outline-primary">Add Journal</asp:LinkButton>
                                    </li>
                                
                                </ul>
                                
                                <div class="modal fade show  backgroundoverlay" runat="server" id="docModal1" >
                                    <div class="modal-dialog modal-dialog-centered" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                             <h5 class="modal-title" id="exampleModalLabel">Add Journal</h5>
                                             <asp:LinkButton CssClass="close" ID="btnCross" runat="server" OnClick="btnCancel_Click">
                                              <span aria-hidden="true">×</span>
                                             </asp:LinkButton>
                                            </div>
                                            <div class="modal-body text-left ">
                                               <%-- <form action="" method="">--%>
                                                <asp:UpdatePanel ID="upAddJournal" runat="server">
                                                    <ContentTemplate>
                                                    <div class="form-group">

                                                     <%--<label for="textbox">Journal Title</label>
                                                     <asp:TextBox runat="server" CssClass="form-control" ID="txtTitle" placeholder="Enter Name Of the Journal"></asp:TextBox>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTitle"
                                                      Display="Dynamic" ValidationGroup="docUpload" ErrorMessage="Please enter document name." ForeColor="Red">
                                                     </asp:RequiredFieldValidator>--%>

                                                     <label for="textbox">Journal Title</label>
                                                     <asp:TextBox ID="txtTitle" autocomplete="off" MaxLength="100" runat="server" CssClass="form-control" ValidationGroup="SAJournal"  placeholder="Enter Name Of the Journal"></asp:TextBox>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTitle"
                                                      Display="Dynamic" ValidationGroup="SAJournal" ErrorMessage="Please enter journal title." ForeColor="Red">
                                                     </asp:RequiredFieldValidator>

                                                    </div>

                                                    <asp:UpdatePanel ID="upda" runat="server">
                                                        <ContentTemplate>
                                                            <div class="form-group">
                                                                <label for="textarea">Timeframe</label>
                                                                <div class="row custom-select-con">
                                                                    <div class="col-sm-6 col-sm-0 mb-3">
                                                                        <asp:DropDownList ID="fromMonth" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                            <asp:ListItem  Text="Month" value="0"></asp:ListItem>
                                                                            <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                                                            <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                                                            <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                                                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                                            <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                                                            <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                                                            <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                                                            <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                                                            <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                                                            <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                                                            <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="fromMonth"
                                                                            Display="Dynamic" ValidationGroup="SAJournal" ErrorMessage="Please enter month."
                                                                            InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <asp:DropDownList ID="txtFromYear" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromYear"
                                                                            Display="Dynamic" ValidationGroup="SAJournal" ErrorMessage="Please enter year."
                                                                            InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Label ID="lblErrorMsg" runat="server" CssClass="error_message"></asp:Label>
                                                        </ContentTemplate>

                                                    </asp:UpdatePanel>
                                                    <div class="submit-con">
                                                        <asp:LinkButton ID="btnCancel"  OnClick="btnCancel_Click" runat="server" class="btn btn-outline-primary m-r-15">Cancel</asp:LinkButton>
                                                        <asp:LinkButton ID="btnSave" ValidationGroup="SAJournal" OnClick="btnSave_Click" runat="server" class="btn btn-primary">Save</asp:LinkButton>
                                                    </div>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                              <%--  </form>--%>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <!-- Modal ended -->
                                
                            </div>
                        </div><!-- btn-title-con ended -->
                            </div><!-- super-admin-comp ended -->
                            <asp:UpdatePanel ID="upJournals" runat="server">
                                <ContentTemplate>
                                    <asp:ListView ID="lstJournals" OnItemDataBound="lstJournals_ItemDataBound"  OnItemCommand="lstJournals_ItemCommand" runat="server">
                                        <ItemTemplate>
                                            <div class="card card-list-con">
                                                <div class="list-group-item top-list">
                                                    <div class="post-con">
                                                        <div class="post-header justify-content-between">
                                                            <div>
                                                                <span class="question-icon">
                                                                    <span class="icon">
                                                                        <img src="images/file.svg"></span>
                                                                </span>
                                                                <ul class="que-con">
                                                                    <li>Journal</li>
                                                                </ul>
                                                            </div>
                                                            <div class="material-toggle super-admin-comp">
                                                                
                                                              <%--  <asp:UpdatePanel ID="pnlUpdateCheck" runat="server">
                                                                    <ContentTemplate>--%>
                                                                <asp:HiddenField ID="hdnJournalId" Value='<%# Eval("JournalId") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnMonth" Value='<%# Eval("month") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnYear" Value='<%# Eval("year") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnTitle" Value='<%# Eval("title") %>' runat="server" />
                                                                        <asp:CheckBox ID="checkedActive" AutoPostBack="true" runat="server" Checked='<%# Eval("Status") %>' OnCheckedChanged="checkedActive_CheckedChanged" CssClass="hide-body-scroll" Text="&nbsp;"/>
                                                                <%--    </ContentTemplate>
                                                                </asp:UpdatePanel>--%>
                                                             <%--   <label for="checkedActive" class="lbl"></label>--%>
                                                            </div>
                                                        </div>
                                                        <div class="post-body p-b-15">
                                                            <h3 class="d-flex align-items-center"><asp:LinkButton runat="server" Id="lnkJournal" CommandName="Details" ><%#Eval("JournalTitle") %></asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lnkEditJournal" CommandName="EditJournal"  CssClass="edit-title hide-body-scroll" ><span class="lnr lnr-pencil "></span></asp:LinkButton></h3>
                                                            <p class="small-date light-black-text d-flex align-items-center m-t-10">
                                                                <img src="images/writing.svg" alt="" />&nbsp;<span id="lblArtCounts" runat="server"></span>
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
                                             </div>  
                                </ContentTemplate>
                            </asp:UpdatePanel>   
                        </div>
                        <!-- full-width-con ended -->
                    </div>
                    <!-- panel-cover ended -->
                  
                </div><!-- main-section inner ended -->
                
            <!-- main-section-inner ended -->
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
