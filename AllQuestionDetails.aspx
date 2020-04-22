<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
   CodeFile="AllQuestionDetails.aspx.cs" Inherits="AllQuestionDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
<%@ MasterType TypeName="Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:UpdatePanel ID="update" runat="server">
      <ContentTemplate>       
         <!--Delete Success Popup-->
         <div id="divDeletesucess" clientidmode="Static" runat="server" class="EditProfilepopupHome modal_bg modal backgroundoverlay"
          style="display: none;">
          <div id="divDeleteConfirm" runat="server" class="confirmDeletes modal-dialog modal-dialog-centered" clientidmode="Static">
           <div class="modal-content">
            <div >
               <b>
                  <asp:Label ID="lbl" runat="server"></asp:Label>
               </b>
            </div>
            <div class="modal-header">
               <h5 class="modal-title">Delete Confirmation
                                        </h5>
            </div>
            <div class="modal-body">
                <asp:Label ID="lblConnDisconn" runat="server" Text="Do you want to delete?" ></asp:Label>
            </div> 
            <div class="modal-footer border-top-0">
              
               <asp:LinkButton ID="lnkDeleteCancel"  class="add-scroller btn btn-outline-primary m-r-15" runat="server" ClientIDMode="Static" Text="Cancel"
                  OnClientClick="javascript:divCancels();return false;"></asp:LinkButton>
                   <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes"
                  CssClass="btn btn-primary success-popup" OnClick="lnkDeleteConfirm_Click"></asp:LinkButton>
            </div>
           </div>  
          </div>
         </div>
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
             <asp:Button ClientIDMode="Static" class="btn btn-primary  add-scroller" id="btnOk" runat="server" Text="Ok" OnClientClick="javascript:CloseMsg();"></asp:Button><%--OnClick="btnOk_click"--%>
            </div>
           </div>
          </div>
         </div>
         <%--<div id="divDeletesucess" clientidmode="Static" runat="server" class="EditProfilepopupHome modal_bg modal backgroundoverlay" style="display: none;">
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
                      <asp:Label ID="lblConnDisconn" runat="server" Text="Do you want to Delete ?" ></asp:Label>
                    </div>  
                    <div class="modal-footer border-top-0">
                        <asp:LinkButton ID="lnkDeleteCancel" runat="server" class="add-scroller btn btn-outline-primary m-r-15" ClientIDMode="Static" Text="Cancel" OnClientClick="javascript:divCancels();return false;"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" CssClass="btn btn-primary add-scroller" OnClick="lnkDeleteConfirm_Click"></asp:LinkButton>                                            
                    </div>
                  </div>
                </div>
         </div>--%>
         <!--Delete Success End-->
         <div class="main-section-inner">
            <div class="panel-cover clearfix">
                <asp:UpdatePanel ID="up1" runat="server" class="all_qustion_details_page" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkAllBlog" />
                    </Triggers>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lnkRecentBlogs" />
                    </Triggers>
                    <ContentTemplate>
               <div class="full-width-con">         
                        
                        <div class="form-inline">
                           <div class="w-100">
                           <!-- <div>
                              <a class="postNewQuestion" href="post-new-question.aspx"><b>Post New Question</b></a>
                           </div> -->
                           <div class="search-filter-con row">
                              <!--search box starts-->
                             
                              <div class="col-lg-4 col-sm-12 margin-bottom-m col-12">
                                 <asp:Panel ID="pnlEnterComment" runat="server" ClientIDMode="Predictable" DefaultButton="lnkEnterComment">
                               <asp:UpdatePanel ID="up12" runat="server" class="search-cover">
                                   <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="txtSearchQuestion" />
                                  </Triggers>
                                  <ContentTemplate>
                                     <asp:TextBox  rows="1" cols="30" TextMode="multiline" autocomplete="off" ID="txtSearchQuestion" ClientIDMode="Static" CssClass="border-0 form-control p-r-0 w-100 box-shadow-none height-moz" runat="server" placeholder="Search Q & A" onkeydown="return postCommentNew.bind(this)(event)" oninput="postCommentInput.bind(this)(event)">
                                     </asp:TextBox>
                                      <input type="button" id="searchBtn" class="search-btn-2" value="">
                                        <asp:LinkButton ID="lnkEnterComment" CommandArgument='<%# Eval("Id") %>'
                                                                       CssClass="lnkEnterCommentcss display-none" CommandName="EnterComment" runat="server" Text="Enter">
                                                                      </asp:LinkButton>
<%--                                      <span class="m-view m-filter-btn"><span class="filter-icon"></span> &nbsp; Filter</span>--%>
                                     <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSearchQuestion"
                                        Display="Dynamic" ValidationGroup="t" ErrorMessage="Please enter keyword" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                  </ContentTemplate>
                               </asp:UpdatePanel>
                                </asp:Panel>
                               </div>
                              
                              
                              <!--search box End-->
                              <!--Narrow Your Search-->
                          
                                       <asp:UpdatePanel ID="UpdateSub" class="hide_show_m fliter-con question-ans-page col-sm-12 col-lg-5 col-12 margin-bottom-m" runat="server">
                                          <%--<Triggers>
                                           <asp:AsyncPostBackTrigger ControlID="lstSerchSubjCategory" />
                                          </Triggers>--%>
                                        <ContentTemplate>
                                         <asp:HiddenField ID="hdnSubject" ClientIDMode="Static" runat="server" />
                                         <ul class="ulblogcontaxt w-100">
                                          <uc:UserControl_MultiSelect ID="lstSerchSubjCategory" ClientIDMode="Static" runat="server" />
                        
                                         </ul>
                                         <%--<div class="view_allqa">
                                          <asp:LinkButton ID="lnkViewSubj" Text="View all" Font-Underline="false" runat="server"
                                           OnClick="lnkViewSubj_Click"></asp:LinkButton>
                                         </div>--%>
                                         <%--<asp:LinkButton ID="btnSave" ClientIDMode="Static" CssClass="searchBlog" runat="server" OnClientClick="javascript:CallWriteQA();"
                                          Text="SEARCH" ValidationGroup="t" OnClick="btnSave_Click"></asp:LinkButton>--%>
                                         <div>
                                          <div class="display-none">
                                           <%--<asp:LinkButton ID="lnkViewSubj" runat="server" ClientIDMode="Static" CssClass="searchBlog" Text="View all" Font-Underline="false" OnClientClick="javascript:CallWritebolgs();showLoader1();" OnClick="lnkViewSubj_Click" ValidationGroup="t" Style="display: none;"></asp:LinkButton>--%>
                                           <asp:Button ID="btnSave" ClientIDMode="Static" runat="server" ValidationGroup="t" CssClass="searchBlog" OnClientClick="javascript:CallWritebolgs();showLoader1();" OnClick="btnSave_Click" />
                                          </div>
                                         </div> 
                                        </ContentTemplate>
                                       </asp:UpdatePanel>
                                           
                                          <!--Narrow Your Search End-->

                                            <div class="dropdown col-lg-3 col-sm-12 col-12 text-lg-right">
                                                <span class="sortFilter" id="sortFilter" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"> 
                                                 Sort by: &nbsp; <strong> <span id="sortFilterText" runat="server">Most Recent</span></strong><span class="icon-caret-down"></span></span>
                                                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                 <asp:LinkButton ID="lnkAllBlog" runat="server" Text="Most Recent" ClientIDMode="Static" OnClientClick="showLoader1();"
                                                  OnClick="chkRecent_CheckedChanged" CssClass="dropdown-item"></asp:LinkButton>
                                                 <asp:LinkButton ID="lnkRecentBlogs" runat="server" Text="Most Popular" ClientIDMode="Static" OnClientClick="showLoader1();"
                                                  OnClick="chkMostPopular_CheckedChanged" CssClass="dropdown-item"></asp:LinkButton>
                                                    
                                                </div>
                                            </div>      
                                       
                  
                           </div>                          
                          
                           <div style="display: none;">
                            <asp:Button ID="btnSave1" ClientIDMode="Static" runat="server" ValidationGroup="t"  OnClientClick="javascript:CallWriteQA();showLoader1();" OnClick="btnSave_Click" />
                           </div>
                           <div style="display: none;">
                            <asp:Button ID="btnTag" ClientIDMode="Static" runat="server" OnClick="btnTag_Click" />
                           </div>
                           </div>
                        </div>
                        <!--search box ends-->
                        <!--left box starts-->
                        
                              <div>
                                 <div class="cls">
                                 </div>
                                 <asp:UpdatePanel ID="UpdateParentQADetails"  runat="server" ClientIDMode="Static">
                                    <ContentTemplate>
                                       <div class="not_found_r">
                                          <asp:Label ID="lblmsg" runat="server" Text="No Record Found..!" ForeColor="Red" Visible="false"></asp:Label>
                                       </div>
                                       <asp:HiddenField ID="hdnDeletePostQuestionID"  ClientIDMode="Static"
                                          runat="server" />
                                       <asp:HiddenField ID="hdnstrQuestionDescription"  ClientIDMode="Static"
                                          runat="server" />
                                       <div class="qabox " >
                                          <asp:ListView ID="lstParentQADetails" runat="server" OnItemDataBound="lstParentQADetails_ItemDataBound"
                                             OnItemCommand="lstParentQADetails_ItemCommand">
                                             <ItemTemplate>
                                                <asp:HiddenField ID="hdnPostQuestionID" Value='<%# Eval("intPostQuestionId") %>' ClientIDMode="Static"
                                                   runat="server" />
                                                <asp:HiddenField ID="hdnAddedBy" runat="server" Value='<%#Eval("intAddedBy")%>' />
                                                <!---Question List Start-->
                                                <div class="card card-list-con">
                                                   <div class="list-group-item top-list">
                                                      <div class="post-con">
                                                         <!---Post Header-->
                                                         <div class="post-header">
                                                            <span class="question-icon">
                                                                <span class="icon"> ?</span>
                                                            </span>
                                                            <ul class="que-con">
                                                                <li>Question</li>
                                                                <li id="liSubjCategory" runat="server">
                                                                     <asp:ListView ID="lstSubjCategory" runat="server" OnItemDataBound="lstSubjCategory_ItemDataBound">
                                                                        <ItemTemplate>
                                                                           <span id="SubLi" runat="server">
                                                                              <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                                              <asp:Label ID="lnkCatName"
                                                                                 Font-Underline="false" runat="server" Text='<%#Eval("strCategoryName")%>'></asp:Label>
                                                                           </span>
                                                                        </ItemTemplate>
                                                                     </asp:ListView>
                                                                  </li>
                                                            </ul>
                                                  
                                                         </div> 
                                                         <!---Post Header End--> 
                                                         <!---Post Body Text--> 
                                                         <div class="post-body mt-2">  
                                                         	  <!---Edit button start-->
                                                            <%--<div class="more-btn float-right">--%>
                                                             <div id="divEditDeleteList" Visible="false" runat="server" class="more-btn edit-btn-fix float-right">
                                                             <span class="dropdown">
                                                              <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                               <img src="images/more.svg" alt="" class="more-btn">
                                                              </a>

                                                              <span class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                                               <asp:HiddenField ID="hdnintPostQuestionIdelet" Value='<%# Eval("intPostQuestionId") %>' ClientIDMode="Static"
                                                                runat="server"  />
                                                               <asp:HiddenField ID="lnkstrQuestionDescription" runat="server" Value='<%#Eval("strQuestionDescription") %>' ClientIDMode="Static"></asp:HiddenField>
                                                               <asp:LinkButton ID="lnkEdit" Font-Underline="false"  Visible="false"
                                                                ClientIDMode="Static" ToolTip="Edit" Text="Edit"
                                                                class="edit dropdown-item"  CommandName="Edit QA" CausesValidation="false" runat="server"> <span class="lnr lnr-pencil"> </span> Edit
                                                               </asp:LinkButton>
                                                               <asp:LinkButton ID="lnkDelete" Font-Underline="false"  Visible="false"
                                                                ClientIDMode="Static" ToolTip="Delete" OnClientClick="javascript:docdelete();return false;" 
                                                                class="edit dropdown-item  hide-body-scroll" CommandName="Delete QA" CausesValidation="false" runat="server"  Text="Delete"> <span class="lnr lnr-trash"></span> Delete
                                                               </asp:LinkButton>
                                                              </span>
                                                             </span>
                                                             </div>
                                                            <%--</div>--%>
                                                            <h4 class="text-justify"> 
                                                             <asp:LinkButton ID="Label1" Font-Underline="false" CommandName="QADetails" CssClass="commentQA remove-anchor moreQA cursor-pointer"
                                                              runat="server" Text='<%#Eval("strQuestionDescription") %>'></asp:LinkButton> 
                                                            </h4>
                                                            <ul class="small-date question-details-date">  
                                                             <li>by <asp:LinkButton ID="Label2" Font-Underline="false" runat="server" class="un-anchor" Text='<%#Eval("AuthorName") %>' CommandName="GoToProfile"></asp:LinkButton></li>
                                                             <li><asp:Label ID="lblDate" runat="server" Text='<%#Eval("dtAddedOn") %>' class="small-date"></asp:Label></li>
                                                            </ul>  
                                                                                                                                    
                                                         </div>    
                                                         <!---Post Body Text End-->
                                                         <div class="categoryBoxQA" id="divCategorys" >
                                                            <div class="qsscreenBox">
                                                               <%--<span class="">
                                                                  
                                                               </span>--%>
                                                            </div>
                                                            <div class=""  id="Div1">
                                                            </div>
                                                            <div class="cls"></div>
                                                            <!---Post Like share details Start-->
                                                            <div class="post-footer">
                                                               <ul class="list-inline d-lg-flex d-sm-flex align-items-center">
                                                                  <li id="liLike" runat="server" class="list-inline-item d-inline-flex align-items-center">
                                                                     <span class="like-btn"></span>
                                                                     <asp:Label ID="lblTotallike" runat="server" Text="" ToolTip="Like"></asp:Label>
                                                                    
                                                                  </li>
                                                                  <li class="list-inline-item d-inline-flex align-items-center">
                                                                     <span class="share-btn"></span>
                                                                     <asp:Label ID="lblShare" runat="server" Text="" ToolTip="Shared"></asp:Label>
                                                                     
                                                                    
                                                                  </li>
                                                                  <li class="list-inline-item d-inline-flex align-items-center">
                                                                     <span class="edit-comment-btn"></span>
                                                                     <asp:Label ID="lblreply" runat="server" Text="" ToolTip="Add Answers"></asp:Label>
                                                                     
                                                                  </li>
                                                                  
                                                                    <li class="list-inline-item  write-ans">
                                                                        <asp:LinkButton CommandName="QADetails" runat="server" ID="lnkWriteAnswer" ClientIDMode="Static" CssClass="active-toogle">
                                                                            <span class="icon-answer-edit-icon"></span> &nbsp;Write Answer
                                                                        </asp:LinkButton>
                                                                    </li>                                         
                                                               </ul>
                                                            </div>
                                                            <!---Post Like share details Start-->
                                                         </div>
                                                         <div class="cls"></div>
                                                      </div>
                                                   </div>
                                                </div>
                                                <!---Question List End-->
                                             </ItemTemplate>
                                          </asp:ListView>
                                       </div>
                                    </ContentTemplate>
                                 </asp:UpdatePanel>
                              </div>
                           
                              <!--left verticle search list ends-->
               
                  <!---Pagination Start-->
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
                  <!---Pagination End-->
               </div>
                    </ContentTemplate>
                 </asp:UpdatePanel>
            </div>
         </div>         
         
      </ContentTemplate>
      <Triggers>
         <asp:AsyncPostBackTrigger ControlID="btnSave" />
         <asp:AsyncPostBackTrigger ControlID="lnkDeleteConfirm"/>
         <asp:AsyncPostBackTrigger ControlID="btnTag"/>
         <asp:AsyncPostBackTrigger ControlID="lnkNext"/>
         <asp:AsyncPostBackTrigger ControlID="lnkPrevious"/>
      </Triggers>
   </asp:UpdatePanel>
    <script>
     $(document).on('click', '.dropdown_m', function() {
  
     
        $(".hide_show_m").slideToggle('slow');
          $(".popular_cat").hide('slow');
     

       });
        $(document).on('click', '.popularHeading', function ()
        {   
     
         $(".popular_cat").slideToggle('slow');
         $(".hide_show_m").hide('slow');
     
       });
     function CloseMsg() {
          $('#divSuccess').css("display", "none");
      }    

   </script>
   <script type="text/javascript">
      $(document).ready(function () {
          $(".categoryTxt ul li").click(function () {
              // $(this).toggleClass("gray");
          });
      });
      function CallTop() {
          $("html, body").animate({ scrollTop: 0 }, 10);
      }
   </script>
   <script type="text/javascript">
      $(document).ready(function () {
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
          var showChar = 150;
          var ellipsestext = "...";
          var moretext = "Read more";
          var lesstext = "Less";
          $('.moreQA').each(function () {
              var content = $(this).html();
              if (content.length > showChar) {
                  var c = content.substr(0, showChar);
                  var h = content.substr(showChar - 1, content.length - showChar);
                  var html = c + '<span class="moreelipses">' + ellipsestext + '</span>&nbsp;<span class="morecontent"><span style="display:none;">' + h + '</span>&nbsp;&nbsp;<a href="" class="morelinkQA">' + moretext + '</a></span>';
                  $(this).html(html);
              }
          });
          $(".morelinkQA").click(function () {
              if ($(this).hasClass("less")) {
                  $(this).removeClass("less");
                  $(this).html(moretext);
              } else {
                  $(this).addClass("less");
                  $(this).html(lesstext);
              }
              $(this).parent().prev().toggle();
              $(this).prev().toggle();
              return false;
          });
      
          if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
              $('#lnkNextshow').css("display", "block");
              $('#lnkNext').css("display", "none");
          }
          $("#txtSearchQuestion").keydown(function (e) {
              if (e.keyCode == 13) {
                  $('#btnSave1').click();
                  e.preventDefault();
              }
          });

          $('#searchBtn').on('click', function () {
            document.getElementById("btnSave").click();
          })

          createChosen();
        $('#selectControl').on('change', function (evt, params) {
            $('#selectControl').trigger('chosen:close');
            $('.chosen-drop').css('display','none');
            document.getElementById("btnSave1").click();

          });
          $("#dvPage a").on('click', function(event){showLoader1();});

          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                  $('#lnkNextshow').css("display", "block");
                  $('#lnkNext').css("display", "none");
              }
              $("#txtSearchQuestion").keydown(function (e) {
                  if (e.keyCode == 13) {
                      $('#btnSave1').click();
                      e.preventDefault();
                  }
              });


              $('.moreQA').each(function () {
                  var content = $(this).html();
                  if (content.length > showChar) {
                      var c = content.substr(0, showChar);
                      var h = content.substr(showChar - 1, content.length - showChar);
                      var html = c + '<span class="moreelipses">' + ellipsestext + '</span>&nbsp;<span class="morecontent"><span span style="display:none;">' + h + '</span>&nbsp;&nbsp;<a href="" class="morelinkQA">' + moretext + '</a></span>';
                      $(this).html(html);
                  }
              });
              $(".morelinkQA").click(function () {
                  if ($(this).hasClass("less")) {
                      $(this).removeClass("less");
                      $(this).html(moretext);
                  } else {
                      $(this).addClass("less");
                      $(this).html(lesstext);
                  }
                  $(this).parent().prev().toggle();
                  $(this).prev().toggle();
                  return false;
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
      $(document).ready(function () {
          $('ul.ulblogcontaxt li').click(function (e) {
              e.preventDefault();
              $(this).toggleClass('selectBlogLi unselectBlogLi');
              if ($(this).hasClass("selectBlogLi")) {
                  $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
              } else {
                  $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
              }
              AddSubjectCall($(this).children("#hdnSubCatId").val());
          });
      
      });
      
      $(document).ready(function () {
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              $('ul.ulblogcontaxt li').click(function (e) {
                  $(this).toggleClass('selectBlogLi unselectBlogLi');
                  if ($(this).hasClass("selectBlogLi")) {
                      $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                  } else {
                      $(this).children("#lnkCatName").toggleClass("selectBlogcat unselectBlogcat");
                  }
                  AddSubjectCall($(this).children("#hdnSubCatId").val());
                 e.preventDefault();
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
          var subVal = $("#hdnSubject").val().split(","); ;
          $.each(subVal, function (i) {
              var sub=subVal[i];
              var vCl = $('.unselectBlogLi').children("#hdnSubCatId").val();
              var myElements = $(".unselectBlogLi");
              for (var i = 0; i < myElements.length; i++) {
                  var dtt=myElements.eq(i).children('#hdnSubCatId').val();
                  if (sub == dtt) {
                      myElements.eq(i).children('#lnkCatName').toggleClass("selectBlogcat unselectBlogcat");
                  } 
              }
          });
      }
   </script>
   <script type="text/javascript">
      function CallWriteQA() {
          $('#btnSave').css("box-shadow", "0px 0px 5px #00B7E5");
          $('#btnSave').css("background", "#00A5AA");
          if ($('#txtblogsearch').text() == '') {
              setTimeout(
              function () {
                  $('#btnSave').css("background", "#0096a1");
                  $('#btnSave').css("box-shadow", "0px 0px 0px #00B7E5");
              }, 1000);
          }
      }
      function docdelete() {
          $('#divDeletesucess').css("display", "block");
      }
      function divCancels() {
          $('#divDeletesucess').css("display", "none");
      }
      $(document).ready(function () {
          $('#lnkDelete').click(function () {
               $('#hdnDeletePostQuestionID').val($(this).parent().children('#hdnintPostQuestionIdelet').val());
              $('#hdnstrQuestionDescription').val($(this).parent().children('#lnkstrQuestionDescription').val());
              $('#divDeletesucess').css("display", "block");
          });
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function () {
              $('#lnkDelete').click(function () {
                   $('#hdnDeletePostQuestionID').val($(this).parent().children('#hdnintPostQuestionIdelet').val());
                    $('#hdnstrQuestionDescription').val($(this).parent().children('#lnkstrQuestionDescription').val());
                  $('#divDeletesucess').css("display", "block");
              });
              //$("span.ediDel").click(function () {
              //    $('#hdnDeletePostQuestionID').val($(this).children('#hdnintPostQuestionIdelet').val());
              //    $('#hdnstrQuestionDescription').val($(this).children('#lnkstrQuestionDescription').val());
              //});
          });
      
          //$("span.ediDel").click(function () {
          //    $('#hdnDeletePostQuestionID').val($(this).children('#hdnintPostQuestionIdelet').val());
          //    $('#hdnstrQuestionDescription').val($(this).children('#lnkstrQuestionDescription').val());
          //});
      });
   </script>
</asp:Content>
