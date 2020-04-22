<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
   CodeFile="AllBlogs.aspx.cs" Inherits="AllBlogs" %>
    <%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
    <%@ Register Src="~/UserControl/BlogPopulerPost.ascx" TagName="BlogPopulerPost" TagPrefix="BlogPopulerPost" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
    <%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
    <%@ MasterType TypeName="Main" %>
                <asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
                </asp:Content>
                <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                    <asp:UpdatePanel ID="upMain" runat="server">
                        <ContentTemplate>
                            <!--inner container ends-->
                            <div class="innerDocumentContainerSpc">
                                <div id="divSuccess" runat="server" class="modal backgroundoverlay" clientidmode="Static" style="display: none;">
                                 <div class="modal-dialog modal-dialog-centered">
                                  <div class="modal-content">
                                   <div class="modal-header">
                                    <h5 class="modal-title">Success  
                                    </h5>
                                   </div>
                                   <div class="modal-body">
                                    <asp:Label ID="lblSuccess" runat="server" Text="Successfully deleted."> </asp:Label>
                                   </div>   
                                   <div class="modal-footer border-top-0">
                                    <asp:Button ClientIDMode="Static" class="btn btn-primary add-scroller" id="btnOk" runat="server" Text="Ok" OnClientClick="javascript:CloseMsg();"></asp:Button><%--OnClick="btnOk_click"--%>
                                   </div>
                                  </div>
                                 </div>
                                </div>
                                <div id="divDeletesucess" clientidmode="Static" runat="server" class="EditProfilepopupHome modal_bg modal backgroundoverlay" style="display: none;">
                                    <div id="divDeleteConfirm" runat="server" class="modal-dialog modal-dialog-centered" clientidmode="Static">
                                      <div class="modal-content">
                                        <b>
                                           <asp:Label ID="lbl" runat="server"></asp:Label>
                                        </b>
                                        <div class="modal-header">
                                            <h5 class="modal-title">Delete Confirmation
                                             </h5>                    
                                        </div>
                                        <div class="modal-body">
                                          <asp:Label ID="lblConnDisconn" runat="server" Text="Do you want to delete?" ></asp:Label>
                                        </div>  
                                        <div class="modal-footer border-top-0">
                                            <asp:LinkButton ID="lnkDeleteCancel" runat="server" class="add-scroller btn btn-outline-primary m-r-15" ClientIDMode="Static" Text="Cancel" OnClientClick="javascript:divCancels();"  OnClick="lnkDeleteCancel_Click" ></asp:LinkButton>
                                            <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" CssClass="btn btn-primary success-popup" OnClick="lnkDeleteConfirm_Click"></asp:LinkButton>                                            
                                        </div>
                                      </div>
                                    </div>
                                </div>
                                <div class="main-section-inner">
                                  <span onclick="showRight();" class="m-aside-trigger mt-0 mr-0">
                                    <span class="lnr lnr-arrow-left"></span>
                                    <span class="avatar-text">Popular Posts</span>
                                  </span>
                                    <div class="panel-cover clearfix">
                                        <div class="full-width-con">
                                            <!---Center Panel--> 
                                            <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                           <asp:AsyncPostBackTrigger ControlID="lnkAllBlog" />
                                                       </Triggers>
                                                       <Triggers>
                                                           <asp:AsyncPostBackTrigger ControlID="lnkRecentBlogs" />
                                                       </Triggers>
                                                <ContentTemplate>
                                            <div class="center-panel">
                                                <!---Search and filter section--> 
                                               <div class="form-inline">
                                                <div class="w-100">
                                                <!--search box starts-->   
                                                  <div class="search-filter-con blog-list-filter row">
                                                        <asp:HiddenField ID="hdnSubject" ClientIDMode="Static" runat="server" />
                                                         <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                         <div class="col-lg-4 clearfix margin-bottom-m  col-sm-12 col-12">
                                                             <asp:Panel ID="pnlEnterComment" runat="server" ClientIDMode="Predictable" DefaultButton="lnkEnterComment">
                                                           <asp:UpdatePanel ID="er" runat="server" class="w-100 search-cover">
                                                               <Triggers>
                                                                   <asp:AsyncPostBackTrigger ControlID="txtblogsearch" />
                                                               </Triggers>
                                                             
                                                                 <ContentTemplate>
                                                                     
                                                                     <asp:TextBox rows="1" cols="30" TextMode="multiline" 
                                                                     ID="txtblogsearch" ClientIDMode="Static" CssClass="border-0 form-control p-r-0 w-100 box-shadow-none height-moz" runat="server" CausesValidation="true" placeholder="Search Blogs" onkeydown="return postCommentNew.bind(this)(event)" oninput="postCommentInput.bind(this)(event)" autocomplete="off"></asp:TextBox>
                                                                     <input type="button" id="searchBtn" class="search-btn-2" value="">
                                                                      <asp:LinkButton ID="lnkEnterComment" CommandArgument='<%# Eval("Id") %>'
                                                                       CssClass="lnkEnterCommentcss display-none" CommandName="EnterComment" runat="server" Text="Enter">
                                                                      </asp:LinkButton>
                                                                     <%--onclick="showLoader()"--%>
                                                                     <br />
                                                                     <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtblogsearch" Display="Dynamic" ValidationGroup="blogg" ErrorMessage="Please enter keyword" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                 
                                                                 </ContentTemplate>
                                                                  
                                                              
                                                           </asp:UpdatePanel>
                                                           </asp:Panel>
                                                         
                                                          </div>
                                                     
                                                        <!---Filter-->     
                                               
                                                              <!---Autocomplete-->
                                                               <asp:UpdatePanel ID="UpdatePanel1" class=" col-lg-4 col-sm-12  clearfix margin-bottom-m  col-12" runat="server">
                                                                   
                                                                   <ContentTemplate>
                                                                       <ul class="ulblogcontaxt">
                                                                           <uc:UserControl_MultiSelect ID="lstSerchSubjCategory" ClientIDMode="Static" runat="server" />
                                                                           <%--<asp:ListView ID="lstSerchSubjCategory" runat="server" OnItemDataBound="lstSerchSubjCategory_ItemDataBound">
                                                                               <ItemTemplate>
                                                                                   <li id="SubLi" runat="server" class="subli">
                                                                                       <asp:HiddenField ID="hdnSubCatId" ClientIDMode="Static" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                                                       <asp:LinkButton ID="lnkCatName" Style="text-decoration: none;" Font-Underline="false" ClientIDMode="Static" OnClientClick="return false;" runat="server" Text='<%#Eval("strCategoryName")%>' CommandName="Subject Category"></asp:LinkButton>
                                                                                   </li>
                                                                               </ItemTemplate>
                                                                           </asp:ListView>--%>
                                                                       </ul>
                                                                       <div>
                                                                            <div class="display-none">
                                                                             <asp:LinkButton ID="btnSave" runat="server" ClientIDMode="Static" CssClass="searchBlog" OnClientClick="javascript:CallWritebolgs();showLoader1();" Text="SEARCH" ValidationGroup="blogg" OnClick="btnSave_Click" Style="display: none;"></asp:LinkButton>
                                                                             <asp:Button ID="btnSave1" runat="server" ClientIDMode="Static" ValidationGroup="blogg" OnClientClick="javascript:CallWritebolgs();showLoader1();" OnClick="btnSave_Click" />
                                                                           </div>
                                                                       </div>
                                                                   </ContentTemplate>


                                                              </asp:UpdatePanel>
                                                              <!---Tabbing-->
                                                               <div class="dropdown clearfix col-lg-4 col-sm-12 col-12 text-lg-right"> 
                                                                   <span class="sortFilter" id="sortFilter" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> 
                                                                       Filter by: &nbsp;<strong> <span id="sortFilterText" runat="server">Recent Blogs</span></strong> 
                                                                       <span class="icon-caret-down"></span></span>
                                                                   <div class="dropdown-menu" aria-labelledby="dropdownMenuButton" x-placement="bottom-start"> 

                                                                     <asp:LinkButton ID="lnkRecentBlogs" runat="server" Text="Recent Blogs" ClientIDMode="Static" CssClass="dropdown-item" OnClick="lnkRecentBlogs_Click" OnClientClick="showLoader1();"></asp:LinkButton>
                                                                      <asp:LinkButton ID="lnkAllBlog" runat="server" Text="My Blogs" ClientIDMode="Static" CssClass="dropdown-item" OnClick="lnkAllBlog_Click" OnClientClick="showLoader1();"></asp:LinkButton>
                                                                     
                                                                 
                                                                       <%--<a class="dropdown-item" href="#">Most Recent</a> 
                                                                       <a class="dropdown-item" href="#">Most Popular</a> --%>
                                                                   </div>
                                                               </div>
                                                         
                                                   
                                                   </div>
                                                </div>
                                                   <!--popular post starts-->
                                                   <div class="popularPost display-none">
                                                       <p class="popularHeading">
                                                           Popular Posts
                                                       </p>
                                                       <div>
                                                          
                                                       </div>
                                                   </div>
                                                   <!--popular post ends-->
                                               </div>
                                               <!---Search and filter section End-->

                                               <!---Post Section Start-->
                                               
                                                       <div class="innerContainerLeft">
                                                           <div class="subtitle">
                                                               <div class="recentBlogs ">
                                                                   <%--<asp:LinkButton ID="lnkAllBlog1" runat="server" Text="My Blogs" ClientIDMode="Static" CssClass="" OnClick="lnkAllBlog_Click"></asp:LinkButton>
                                                                   <asp:LinkButton ID="lnkRecentBlogs" runat="server" Text="Recent Blogs" ClientIDMode="Static" CssClass="crtRecentBtn" OnClick="lnkRecentBlogs_Click"></asp:LinkButton>
                                                               --%></div>
                                                         
                                                           </div>
                                                           <!--post title starts-->
                                                           <div class="cls"></div>
                                                           <div class="myBlogs">
                                                               <div class="no_blog_found text-center">
                                                                   <asp:Label ID="lblmsg" runat="server" Text="No Blog Found!" ForeColor="Red" Visible="false"></asp:Label>
                                                               </div>
                                                               <asp:ListView ID="lstAllBlogs" runat="server" OnItemCommand="lstAllBlogs_ItemCommand" OnItemDataBound="lstAllBlogs_ItemDataBound" GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                                                                   <GroupTemplate>
                                                                       <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                                   </GroupTemplate>
                                                                   <ItemTemplate>
                                                                       <!---Blog Post-->
                                                                       <div class="card card-list-con blog-list">
                                                                           <div class="list-group-item top-list">
                                                                              <div class="post-con">
                                                                       <!--           <div class="post-header">
                                                                                     <span class="question-icon">
                                                                                         <span class="icon"><img src="images/file.svg"></span>
                                                                                     </span>
                                                                                     <ul class="que-con">
                                                                                         <li>Blog</li>
                                                                                     </ul>

                                                                                 </div> -->
                                                                                 <asp:UpdatePanel ID="upDelete" runat="server">
                                                                                 <ContentTemplate>
                                                                                 <div class="post-body">
                                                                                    <div id="divEditDeleteList" Visible="false" runat="server" class="more-btn float-right">
                                                                                       <span class="dropdown show">
                                                                                         <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true"> <img src="images/more.svg" alt="" class="more-btn">
                                                                                         </a>

                                                                                         <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start">
                                                                                        
                                                                                          <asp:LinkButton ID="lnkEdit" Font-Underline="false" Visible="false" CssClass="edit dropdown-item" ClientIDMode="Static" ToolTip="Edit" CommandName="Edit Blog" CausesValidation="false" Text="Edit" runat="server"><span class="lnr lnr-pencil"> </span>Edit
                                                                                          </asp:LinkButton>
                                                                                          <asp:LinkButton ID="lnkDelete" Font-Underline="false" Visible="false" CssClass="edit hide-body-scroll dropdown-item" ClientIDMode="Static" ToolTip="Delete" CommandName="Delete Blog" CausesValidation="false" Text="Delete" runat="server"><span class="lnr lnr-trash"></span> Delete
                                                                                          </asp:LinkButton>
                                                                                         </span>
                                                                                        </span>
                                                                                    </div>
                                                                                    <asp:HiddenField ID="hdnBlogLikeUserId" runat="server" Value='<%#Eval("BlogLikeUserId") %>' />
                                                                                    <h3>
                                                                                     <asp:LinkButton ID="lnkBlogHeading" ToolTip="View Details" Font-Underline="false" runat="server" Text='<%#Eval("strBlogHeading")%>' CssClass="blogHeadingName blog_heading_style title-truncate" CommandName="BlogsDetails" ClientIDMode="Static"></asp:LinkButton>
                                                                                    </h3>
                                                                                    <ul class="small-date">
                                                                                     <li>
                                                                                      by
                                                                                      <asp:Label ID="lnkAddBy" runat="server" Text='<%#Eval("strAddedBy")%>'></asp:Label>
                                                                                     </li>
                                                                                     <li>
                                                                                      <asp:Label ID="lblDate" runat="server" Text='<%#Eval("dtAddedOn")%>'></asp:Label> 
                                                                                     </li>        
                                                                                    </ul>
                                                                                 
                                                                                     <div class="breakallwords breakalls postTxts">
                                                                                         <asp:HiddenField ID="hdnintBlogId" runat="server" Value='<%#Eval("intBlogId")%>' ClientIDMode="Static" />
                                                                                         <asp:HiddenField ID="hdnAddedBy" runat="server" Value='<%#Eval("intAddedBy")%>' ClientIDMode="Static" />
                                                                                     </div>
                                                                                    <p>
                                                                                         <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("strBlogTitle").ToString().Replace(Environment.NewLine,"<br />")%>'></asp:Label>
                                                                                     </p>
                                                                                
                                                                                    
                                                                                 </div>
                                                                                 <div class="post-footer li-flex-align">        
                                                                                  <ul class="list-inline li-flex-align">
                                                                                   <li class="list-inline-item">
                                                                                  
                                                                                     <asp:LinkButton ID="btnBlogLike" runat="server" class="default-cursor active-toogle" ToolTip="Like">
                                                                                      <span class="like-btn"></span>
                                                                                     </asp:LinkButton>
                                                                                     <asp:Label ID="lblTotalBloglike" ToolTip="View Likes" runat="server"
                                                                                      Text='<%#Eval("TotalLike") + (((int)Eval("TotalLike")==1)? " Like": " Likes")%>'>
                                                                                     </asp:Label>
                                                                                
                                                                                   </li>
                                                                                   <li class="list-inline-item">
                                                                                    <span class="comment-btn"></span>
                                                                                    <asp:Label ID="lnkComments" runat="server" CommandName="Comments"
                                                                                     Text='<%#Eval("CommentCount")  + ((Eval("CommentCount").ToString() == "1") ? " Comment" : " Comments") %>'>
                                                                                    </asp:Label> 
                                                                                   </li>
                                                                                 
                                                                                   <li class="list-inline-item"><span class="eye-view"></span>
                                                                                    <asp:Label ID="Label1" runat="server" 
                                                                                     Text='<%#Eval("ViewCount") + ((Eval("ViewCount").ToString() == "1") ? " View" : " Views") %>'></asp:Label>
                                                                                   </li>  
                                                                                  </ul>
                                                                                 </div>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                 <asp:AsyncPostBackTrigger ControlID="lnkDelete" />
                                                                                </Triggers>
                                                                               </asp:UpdatePanel>
                                                                              </div>
                                                                           </div>   
                                                                       </div>
                                                                       <!---Blog Post End-->
                                                                   </ItemTemplate>
                                                               </asp:ListView>
                                                               <div class="cls">
                                                               </div>
                                                               <div class="cls">
                                                               </div>
                                                           </div>
                                                           <div>
                                                               <asp:ValidationSummary ID="blogsummry" CssClass="RedErrormsg" runat="server" ValidationGroup="blog" ClientIDMode="Static" ForeColor="Red" Font-Names="Verdana" />
                                                           </div>
                                                           <div class="cls"></div>
                                                           <br />
                                                       </div>
                                                
                                               <!---Post Section Ended-->
                                            <!--pagination starts-->
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
                                             <!---Center Panel Ended-->
                                             <!---Right Panel Start-->
                                             <div class="right-panel-back-layer"></div>
                                             <div class="right-panel"> <span onclick="hideRight();" class="m-view back">Back <span class="lnr lnr-arrow-right"></span></span>
                                                 <div class="aside-bar">
                                                     <div class="card releated-section">
                                                         <div class="card-body">
                                                             <h4>Popular Posts</h4>
                                                               <asp:UpdatePanel ID="update" runat="server">
                                                                  <ContentTemplate>
                                                                      <BlogPopulerPost:BlogPopulerPost  ID="ucBlogPopulerPost" runat="server" AllowDirectUpdate="true" />
                                                                  </ContentTemplate>
                                                              </asp:UpdatePanel>
                                                         </div>
                                                     </div>
                                                 </div>
                                             </div>
                                            <!---Right Panel End-->
                                      

                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
                                    <!--left box ends-->
                                </div>
                                <!--left verticle search list ends-->
                            </div>
                            <%--<asp:UpdateProgress ID="updateProgress" runat="server">
                                <ProgressTemplate>
                                    <div class="loader_home_gif">
                                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/loadingImage.gif" AlternateText="Loading ..." ToolTip="Loading ..." class="divProgress loader_loading_p" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            <asp:AsyncPostBackTrigger ControlID="lnkNext" />
                            <asp:AsyncPostBackTrigger ControlID="lnkPrevious" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <script>
                    $(document).on('click', '.dropdown_m', function() {


                        $(".hide_show_m").slideToggle('slow');
                        $(".popular_cat").hide('slow');


                    });
                    $(document).on('click', '.popularHeading', function() {


                        $(".popular_cat").slideToggle('slow');
                        $(".hide_show_m").hide('slow');


                    });
                    </script>
                    <script type="text/javascript">
                    $(document).ready(function() {
                             function postCommentNew(e) {
                           if (e.keyCode == 13 && $(this).val()) {
                               window.location.href = $(this).parent().parent().children('.lnkEnterCommentcss').attr('href');
                               return false;
                           } else if (e.keyCode == 13) {
                               return false;
                           }
                           
                       }

                       function postCommentInput(e) {
                           var msg = $(this).val().replace(/\n/g, "");
                           if (msg != $(this).val()) { // Enter was pressed
                               $(this).val(msg);
                               window.location.href = $(this).parent().parent().children('.lnkEnterCommentcss').attr('href');
                               showLoader1();
                           }
                       }
                        if ($('#hdnCurrentPage').val() == '1') {
                            //$('#imgPaging').css("display", "none");
                            //$('#lnkprev').css("display", "block");
                        } else {
                            //$('#imgPaging').css("display", "block");
                            //$('#lnkprev').css("display", "none");
                        }

                        if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                            //$('#lnkNextshow').css("display", "block");
                            $('#lnkNext').css("display", "none");
                        }
                        $("#txtblogsearch").keydown(function(e) {
                            if (e.keyCode == 13) {
                                $('#btnSave1').click();
                                e.preventDefault();
                            }


                        });
                        createChosen();
                        $('#selectControl').on('change', function (evt, params) {
                            $('#selectControl').trigger('chosen:close');
                            $('.chosen-drop').css('display','none');
                            document.getElementById("btnSave1").click();

                        });

                        $("#dvPage a").on('click', function(event){showLoader1();});

                        
                        var prm = Sys.WebForms.PageRequestManager.getInstance();
                        prm.add_endRequest(function() {
                            if ($('#hdnCurrentPage').val() == '1') {
                                //$('#imgPaging').css("display", "none");
                                   //$('#lnkprev').css("display", "none");   
                            } else {
                                //$('#imgPaging').css("display", "block");
                            
                                //$('#lnkprev').css("display", "block");
                            }

                            if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                                //$('#lnkNextshow').css("display", "block");
                                $('#lnkNext').css("display", "none");
                            }
                            $("#txtblogsearch").keydown(function(e) {
                                if (e.keyCode == 13) {
                                    $('#btnSave1').click();
                                    e.preventDefault();
                                }
                            });
                            $('#searchBtn').on('click', function () {
                                document.getElementById("btnSave").click();

                            });

                            createChosen();
                            $('#selectControl').on('change', function (evt, params) {
                                $('#selectControl').trigger('chosen:close');
                                $('.chosen-drop').css('display','none');
                                document.getElementById("btnSave1").click();
                            });
          

                            $("#dvPage a").on('click', function(event){showLoader1();});
                           
                        });
                    });
                    </script>
                    <script type="text/javascript">
                    var strSelTexts = '';
                    $(document).ready(function() {
                        $('ul.ulblogcontaxt li').click(function() {
                            $(this).toggleClass('selectBlogLi unselectBlogLi');
                            if ($(this).hasClass("selectBlogLi")) {
                                $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                            } else {
                                $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                            }
                            AddSubjectCall($(this).children("#hdnSubCatId").val());
                        });

                        $('.postTxts').click(function() {
                            var BlogId = $(this).children("#hdnintBlogId").val();
                            var BlogUserId = $(this).children("#hdnAddedBy").val();
                            //window.location("BlogsComments.aspx?intBlogId=" + BlogId + "&hdnAddedBy=" + BlogUserId+"&Blogtype="+);
                        });

                    });
                    $(document).ready(function() {
                        var prm = Sys.WebForms.PageRequestManager.getInstance();
                        prm.add_endRequest(function() {
                            $('ul.ulblogcontaxt li').click(function() {
                                $(this).toggleClass('selectBlogLi unselectBlogLi');
                                if ($(this).hasClass("selectBlogLi")) {
                                    $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                                } else {
                                    $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                                }
                                AddSubjectCall($(this).children("#hdnSubCatId").val());
                            });

                        });
                        });


                    function AddSubjectCall(ids) {
                        var subVal = $("#hdnSubject").val();
                        if (subVal == '') {
                            $("#hdnSubject").val(ids);
                            //$('#btnTag').click();
                        } else {
                            strSelTexts = $("#hdnSubject").val();
                            strSelTexts += ',' + ids;
                            $("#hdnSubject").val(strSelTexts);
                            strSelTexts = '';
                            //$('#btnTag').click();
                        }
                    }

                    function CallTagSelections() {
                        var subVal = $("#hdnSubject").val().split(",");;
                        $.each(subVal, function(i) {
                            var sub = subVal[i];
                            var vCl = $('.unselectBlogLi').children("#hdnSubCatId").val();
                            var myElements = $(".unselectBlogLi");
                            for (var i = 0; i < myElements.length; i++) {
                                var dtt = myElements.eq(i).children('#hdnSubCatId').val();
                                if (sub == dtt) {
                                    myElements.eq(i).children('#lnkCatName').toggleClass("selectBlogcat unselectBlogcat");
                                }
                            }
                        });
                    }
                    </script>
                    <script type="text/javascript">

                    function CloseMsg() {
                        $('#divSuccess').css("display", "none");
                    }

                    function docdelete() {
                        $('#divDeletesucess').css("display", "block");
                    }

                    function divCancels() {
                        $('#divDeletesucess').css("display", "none");
                    }

                    function CallWritebolgs() {
                        // $('#btnSave').css("box-shadow", "0px 0px 5px #00B7E5");
                        // $('#btnSave').css("background", "#00A5AA");
                        if ($('#txtblogsearch').text() == '') {
                            setTimeout(
                                function() {
                                    // $('#btnSave').css("background", "#0096a1");
                                    // $('#btnSave').css("box-shadow", "0px 0px 0px #00B7E5");
                                }, 1000);
                        }
                    }
                    </script>
                    <script type="text/javascript">
                        function chosenOnChange() {
                            $('#selectControl').trigger('chosen:close');
                            document.getElementById("btnSave1").click();

                        }

                        $(document).ready(function () {
                            $('#searchBtn').on('click', function () {
                                document.getElementById("btnSave").click();

                            })

                        })
                        $('#btnSave').hide();
                    </script>
                    <%--<script type="text/javascript">
     $(function() {
        var $input = $("#<%=txtblogsearch.ClientID%>");
            $input.typeahead({
              source: function (request, response) {
                //debugger;
                $.ajax({
                    url: "handler/Serach.ashx",
                    //dataType: "jsonp",
                    data: {
                        q: request
                    },
                    success: function (data) {
                        var urlArr = [];
                        var dataArr = data.split("\n");
                        //  debugger;
                        var srchtxt = document.getElementById("txtblogsearch").value;
                        var Header = "";
                        var ppl = "People";
                        var org = "Organization";
                        var grp = "Groups";
                        var noRecord = "No Record";
                        var url = "";
                        var TopHeader = "";
                        var urlppl = "";
                        var urlgrp = "";
                        var urlorg = "";
                        var urlId = "";
                        //Add link for individual record
                        $.each(dataArr, function (i, value) {
                            Header = "";
                            //debugger
                            if (value.split(",")[6] == "People") {
                                urlId = "Home.aspx?RegId=" + value.split(",")[5];
                            }
                            else if (value.split(",")[6] == "Organization") {
                                urlId = "Home.aspx?RegId=" + value.split(",")[5];
                            }
                            else if (value.split(",")[6] == "Group") {
                                urlId = "Group-Profile.aspx?GrpId=" + value.split(",")[5];
                            } else if (value.split(",")[6] == "No Record") {
                                urlId = "#";
                            }
                            //Add link for header
                            if (value.split(",")[2] == "People") {
                                Header = "<h4 class='pt-1 pb-1 pl-2 pr-2'>" + ppl + "</h4>";
                                url = "SearchPeople.aspx";
                            }
                            else if (value.split(",")[2] == "Organization") {
                                Header = "<h4 class='pt-1 pb-1 pl-2 pr-2'>" + org + "</h4>";
                                url = "SearchOrganization.aspx";
                            }
                            else if (value.split(",")[2] == "Group") {
                                Header = "<h4 class='pt-1 pb-1 pl-2 pr-2'>" + grp + "</h4>";
                                url = "SearchGroup.aspx";
                            } else if (value.split(",")[2] == "No Record") {
                                Header = "<h4>" + noRecord + "</h4>";
                            }
                      
                            URL = '';
                            if (Header) {
                                URL = "<div onclick=\"Open('" + url + "');\">" + Header + "</div>";
                            }
                            URL += '<div class="media align-items-center" onclick="Open("' + urlId + '");" >' +
                                '<div class="noti-avtar mr-1 d-flex align-self-center"> <img src="CroppedPhoto/' + value.split(",")[1] + '" alt="Generic placeholder image"> </div>' +
                                '<div class="media-body">' +
                                '<h6 class="mt-0 mb-0">'+ value.split(",")[0] +'</h6>' +
                                value.split(",")[3] +
                                '</div>' +
                                '</div>';
                                
                            urlArr.push({name: URL, link: urlId});
                        });
                        response(urlArr);
                    },
                    error: function (error) {
                        console.log('ERROR', error)
                    }
                });
              },
                minLength: 3,
                changeInputOnMove: false,
                selectOnBlur : false,
                changeInputOnSelect: false,
                sorter: function (items) {
                    return items;
                },
                followLinkOnSelect : true,
                itemLink: function (val) {
                  return val.link;
                },
                items: 'all'
         });


      
     });
        
     function Open(URL) {
         window.location = URL;
     }  
     function CallUserMethod() {
         document.getElementById('btnAccept').click();
     }
    </script>--%>
   </asp:Content>