<%@ Page Title="" Language="C#" MasterPageFile="~/Main_Super.master" AutoEventWireup="true" CodeFile="SA_NewsListing.aspx.cs" Inherits="SA_NewsListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="main-section-inner">

  <div class="panel-cover clearfix">
   <div class="full-width-con common-user">
    <asp:UpdatePanel ID="pnlParent" runat="server">
     <ContentTemplate>
      <asp:HiddenField ID="hdnintNewsDelete" runat="server" ClientIDMode="Static" Value='' />
      <asp:HiddenField ID="hdnintRssDelete" runat="server" ClientIDMode="Static" Value='' />
      <div class="">
       <div class="custom-nav-con news-page-tab">
        <!-- Nav tabs -->
        <ul id="tabs1" class="custom-nav-control nav nav-tabs news-tab-control">
       <li class="nav-item">
        <asp:LinkButton ID="lnkNews" runat="server" class="nav-link active show" Text="News"
         OnClick="lnkNews_Click"></asp:LinkButton>
       </li>
        <%--<a id="newsanchr" class="nav-link active show" data-toggle="tab" href="#news">News</a>
            <a id="rssanchr" class="nav-link" data-toggle="tab" href="#rss">RSS</a>--%>
       <li class="nav-item">
        <asp:LinkButton ID="lnkRss" runat="server" class="nav-link" Text="RSS"
         OnClick="lnkRss_Click"></asp:LinkButton>
       </li>
          
      </ul>
        <!-- Tab panes -->
        <div class="tab-content m-t-15">
         <asp:Panel ID="PnlNews" runat="server" UpdateMode="Conditional">
          <div id="news" class="custom-nav-container tab-pane container active show">

          <div class="journal-comp">
           <div class="btn-title-con m-t-15-minus">
            <div>
             <h5 class="card-title">News Feed</h5>
            </div>
            <div class="">
             <ul class="list-inline">
              <li class="list-inline-item">
               <a href="SA_Add-News.aspx" class="btn btn-outline-primary">Add News Article</a>
              </li>
             </ul>

            <!-- Modal ended -->
          </div>
         </div>
          <!-- btn-title-con ended -->
        </div>

        <div class="card-list-con">

         <asp:UpdatePanel ID="pnlParentNews" runat="server">
          <ContentTemplate>
           <asp:ListView ID="lstParentCrdDetails" runat="server" OnItemDataBound="lstParentCrdDetails_ItemDataBound" OnItemCommand="lstParentCrdDetails_ItemCommand">
            <%----%>
            <ItemTemplate>
            <asp:HiddenField ID="hdnPostID" runat="server" Value='<%#Eval("ID")%>' ClientIDMode="Static" />
             <div class="card top-list">
              <div class="post-con">
               <div class="post-body p-b-15">
                <span id="editDelLinkNews" runat="server" class="more-btn float-right">
                 <span class="dropdown">
                  <a href="#" id="dropdownMenuLinkNews" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                   <img src="images/more.svg" alt="" class="more-btn">
                  </a>
                  <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start" onclick="setNewsId.bind(this)()">
                   <asp:HiddenField ID="hdnNewsId" runat="server" ClientIDMode="Static" Value='<%#Eval("ID")%>' />
                   <asp:LinkButton Font-Underline="false" CssClass="dropdown-item" CommandName="Edit News" ID="lnkNews"
                    runat="server"><span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>
                   <asp:LinkButton Font-Underline="false" CssClass="dropdown-item hide-body-scroll" CommandName="Delete News" OnClientClick="javascript:docdeleteNews();" ID="lnkdeleteNews"
                    runat="server"><span class="lnr lnr-trash"></span> Delete</asp:LinkButton>
                  </span>
                 </span>
                </span>
                <asp:HiddenField ID="hdnNewsIdToggle" runat="server"  Value='<%#Eval("ID")%>' />
                <div Id="ToggleBtn" runat="server" class="float-right material-toggle" visible="false">               
                 <asp:CheckBox ID="chkToggle" runat="server" AutoPostBack="true" Text="&nbsp;"
                  Checked="true" OnCheckedChanged="Check_Click" />
                
                 <%--<asp:HiddenField ID="hf_reminderID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ReminderID") %>'/>--%>                 
                </div>
                <%--<asp:Label ID="lblHeading" runat="server" Text='<%#Eval("Title") %>' />--%>
                <h3>
                 <asp:Label ID="lblHeading" runat="server" Text='<%#Eval("Title") %>' />
                 <%--<asp:LinkButton ID="lblHeading" runat="server" Font-Underline="false" CommandName="NewsDetails" CssClass="commentQA remove-anchor moreQA cursor-pointer"
                    Text='<%#Eval("Title") %>'>
                 </asp:LinkButton>--%>
                </h3>
                
                <ul class="small-date">
                 <%--<li>By <asp:Label ID="lblUserName" runat="server" /></li>--%>
                 <li>Source: <asp:Label ID="lblSource" runat="server" /></li><%--Text='<%#Eval("Type") %>'--%>
                 <li><asp:Label ID="dtAddedOn" runat="server" Text='<%#Eval("Created_timestamp") %>' /></li>
                </ul>
                <p class="m-t-5">
                 <asp:Label ID="lblDtlNews" runat="server" CssClass="commentQA remove-anchor moreQA" />
                </p>               
               </div>

              </div>
             </div> <!-- card ended -->

            </ItemTemplate>
           </asp:ListView>

          </ContentTemplate>
          <%--<Triggers>
           <asp:AsyncPostBackTrigger ControlID="chkToggle" />
          </Triggers>--%>
         </asp:UpdatePanel>

        </div>  
        <!-- card-list-con ended -->
       </div>
       </asp:Panel>
         <!-- custom-nav-container ended here -->
       <asp:Panel ID="PnlRss" runat="server" UpdateMode="Conditional">
        <div ID="rss" class="custom-nav-container tab-pane container active show">
        <%--<div id="rss" class="custom-nav-container tab-pane container fade">--%>
        <div>
         <div class="btn-title-con m-t-15-minus">
          <div>
           <h5 class="card-title">RSS</h5>
          </div>
          <div class="">
           <ul class="list-inline">
            <li class="list-inline-item">
             <a href="#" class="btn btn-outline-primary hide-body-scroll" data-toggle="modal" data-target="#rssModal"
              onclick="$('#dvSourceLinkErrorText').text(''); $('#lnkPublish').text('Publish');">Add RSS</a>
            </li>
           </ul>

           <div id="rssModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
            <div class="modal-dialog modal-dialog-centered" role="document">
             <div class="modal-content">
              <div class="modal-header">
               <h5 class="modal-title" id="exampleModalLabel">Add RSS Link</h5>
               <button type="button" class="close" data-dismiss="modal" onclick="javascript:callClose();" aria-label="Close">
                <span aria-hidden="true">×</span>
               </button>
              </div>
              <div class="modal-body text-left">
               <form action="" method="">
                <div class="form-group">
                 <label for="textbox">RSS Source Field</label>
                 <asp:TextBox id="txtSourceField" runat="server" maxlength="50" class="form-control" aria-describedby="emailHelp" placeholder="RSS Source Field" 
                  ClientIDMode="Static" autocomplete="off" />
                 <asp:RequiredFieldValidator ID="rfdtxtSourceField" runat="server" ControlToValidate="txtSourceField" Display="Dynamic" ValidationGroup="vldRss" 
                  ErrorMessage="Please enter RSS source title" ForeColor="Red" ClientIDMode="Static"></asp:RequiredFieldValidator>
                 <%--<input type="text" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="RSS Source Field">--%>
                 
                </div>
                <div class="form-group">
                 <label for="textarea">RSS Source Links</label>
                 <asp:TextBox id="txtSourceLink" runat="server" class="form-control" aria-describedby="emailHelp" placeholder="RSS Source Link"
                  ClientIDMode="Static" autocomplete="off" />
                 <div id="dvSourceLinkErrorText" class="error_message" runat="server" ClientIDMode="Static"></div>
                 <asp:RequiredFieldValidator ID="rfdtxtSourceLink" runat="server" ControlToValidate="txtSourceLink" Display="Dynamic" ValidationGroup="vldRss" 
                  ErrorMessage="Please enter correct RSS link" ForeColor="Red" ClientIDMode="Static"></asp:RequiredFieldValidator>
                 <%--<input type="text" class="form-control" id="exampleInputEmail11" aria-describedby="emailHelp" placeholder="Rss Source Links">--%>
                </div>
                <div class="submit-con">
                 <asp:HiddenField ID="hdnintStatusUpdateId" ClientIDMode="Static" Value="" runat="server" />
                 <%--<button class="btn btn-outline-primary m-r-15">Cancel</button>
                 <button class="btn btn-primary">Publish</button>--%>
                 <asp:Button ID="btnCancel" runat="server" type="Reset" Text="Cancel" class="btn btn-outline-primary m-r-15" data-dismiss="modal" OnClientClick="javascript:callClose();"></asp:Button>
                 <asp:LinkButton ID="lnkPublish" runat="server" Text="Publish" CssClass="btn hide-body-scroll btn-primary" ClientIDMode="Static" OnClientClick="javascript:messageClose();" 
                  OnClick="lnkPublish_Click" ValidationGroup="vldRss" />
                </div>
               </form>
              </div>
             </div>
            </div>
           </div>
            <!-- Modal ended -->
          </div>
         </div>
          <!-- btn-title-con ended -->
        </div>

        <div class="card-list-con">

         <asp:UpdatePanel ID="pnlRssFeed" runat="server">
          <ContentTemplate>
           <asp:ListView ID="lstRssFeed" runat="server" OnItemDataBound="lstRssFeed_ItemDataBound" OnItemCommand="lstRssFeed_ItemCommand">            
            <ItemTemplate>
             <asp:HiddenField ID="hdnPostUpdateId" runat="server" ClientIDMode="Static" Value='<%#Eval("ID")%>' />
             <div class="card top-list">
              <div class="post-con">
               <div class="post-body p-b-15">
                <span class="more-btn float-right">
                 <span id="editDelLinkRss" runat="server" class="dropdown">
                  <a href="#" id="dropdownMenuLinkRss" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                   <img src="images/more.svg" alt="" class="more-btn">
                  </a>
                  <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" onclick="setRssId.bind(this)()">  
                   <asp:HiddenField ID="hdnRssId" runat="server" ClientIDMode="Static" Value='<%#Eval("ID")%>' />
                   <asp:LinkButton Font-Underline="false" CssClass="dropdown-item" CommandName="Edit Rss" ID="lnkRss"
                    runat="server"><span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>
                   <asp:LinkButton Font-Underline="false" CssClass="dropdown-item hide-body-scroll" CommandName="Delete Rss" OnClientClick="javascript:docdeleteRss();" ID="lnkdeleteRss"
                    runat="server"><span class="lnr lnr-trash"></span> Delete</asp:LinkButton>
                  </span>
                 </span>
                </span>
                   
                <h3><asp:Label ID="lblTitleRss" runat="server" Text='<%#Eval("Title") %>' /></h3>
                
                <p class="m-t-5"><asp:LinkButton ID="lnkSrcLinkRss" href='<%#Eval("Link") %>' target="_blank" runat="server" Text='<%#Eval("Link") %>' /></p>
                <ul class="small-date">
                 <%--<li>Source: <asp:Label ID="lblSource" runat="server" Text='<%#Eval("Type") %>' /></li>--%>
                 <asp:Label ID="dtAddedOn" class="inline-block mt-1" runat="server" Text='<%#Eval("Created_timestamp") %>' />
                </ul>
               </div>
              </div>
             </div> <!-- card ended --> 

            </ItemTemplate>
           </asp:ListView>
          </ContentTemplate>
          <Triggers>
           <%--<asp:AsyncPostBackTrigger ControlID="lnkPublish" />--%>
           <asp:AsyncPostBackTrigger ControlID="lnkRss" />
           <%--<asp:AsyncPostBackTrigger ControlID="lnkdeleteRss" />--%>
          </Triggers>
         </asp:UpdatePanel>                                               
                                                
        </div>  
         <!-- card-list-con ended -->
       </div>
       </asp:Panel>
          <!-- custom-nav-container ended here -->
        </div>
         <!-- tab-content ended -->
       </div>
      </div>
      <!---Pagination Start-->
       <div class="pagination_main_div">
        <nav aria-label="Page navigation example">
         <ul id="dvPage" runat="server" class="pagination" clientidmode="Static" visible="false">
          <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click" ClientIDMode="Static" class="page-link">                              
           <span class="lnr lnr-chevron-left">
          </asp:LinkButton>
                             
          <asp:Repeater ID="rptDvPage" runat="server" OnItemCommand="rptDvPage_ItemCommand" OnItemDataBound="rptDvPage_ItemDataBound">
           <ItemTemplate>
            <li class="page-item">
             <asp:LinkButton ID="lnkPageLink" runat="server" ClientIDMode="Static" CommandName="PageLink" class="page-link" Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
            </li>
           </ItemTemplate>
          </asp:Repeater>
          <asp:LinkButton ID="lnkNext" runat="server" class="page-link" OnClick="lnkNext_Click" ClientIDMode="Static"> <span class="lnr lnr-chevron-right"></span> </asp:LinkButton>
          
          <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnNextPage" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnLastPage" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnPreviousPage" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
          <asp:HiddenField ID="hdnEndPage" runat="server" ClientIDMode="Static" />
         </ul>
         <ul id="dvPageRss" runat="server" class="pagination" clientidmode="Static" visible="false">
          <asp:LinkButton ID="lnkPreviousRss" runat="server" OnClick="lnkPreviousRss_Click" ClientIDMode="Static" class="page-link">                              
           <span class="lnr lnr-chevron-left">
          </asp:LinkButton>
                             
          <asp:Repeater ID="rptDvPageRss" runat="server" OnItemCommand="rptDvPageRss_ItemCommand" OnItemDataBound="rptDvPageRss_ItemDataBound">
           <ItemTemplate>
            <li class="page-item">
             <asp:LinkButton ID="lnkPageLinkRss" runat="server" ClientIDMode="Static" CommandName="PageLink" class="page-link" Text='<%#Eval("intPageNo") %>'></asp:LinkButton>
            </li>
           </ItemTemplate>
          </asp:Repeater>
          <asp:LinkButton ID="lnkNextRss" runat="server" class="page-link" OnClick="lnkNextRss_Click" ClientIDMode="Static"> <span class="lnr lnr-chevron-right"></span> </asp:LinkButton>
          
          <asp:HiddenField ID="hdnTotalItemRss" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnNextPageRss" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnLastPageRss" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnPreviousPageRss" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnCurrentPageRss" runat="server" ClientIDMode="Static" Value="1" />
          <asp:HiddenField ID="hdnEndPageRss" runat="server" ClientIDMode="Static" />
         </ul>
        </nav>  
       </div>
      <!---Pagination End-->
     </ContentTemplate>
     <Triggers>
      <asp:AsyncPostBackTrigger ControlID="lnkDeleteNewsConfirm"/>
      <asp:AsyncPostBackTrigger ControlID="lnkNext"/>
      <asp:AsyncPostBackTrigger ControlID="lnkPrevious"/>
      <asp:AsyncPostBackTrigger ControlID="lnkNextRss"/>
      <asp:AsyncPostBackTrigger ControlID="lnkPreviousRss"/>
     </Triggers>
    </asp:UpdatePanel>
   </div>
    <!-- full-width-con ended -->
  </div>
   <!-- panel-cover ended -->
 </div>
 <div id="divDeletesucessNews" class="modal backgroundoverlay" clientidmode="Static" runat="server"
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
       <asp:Button ID="lnkDeleteNewsConfirm" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="javascript:divdeletes();"
        CssClass="btn btn-primary success-popup" OnClick="lnkDeleteNewsConfirm_Click"></asp:Button>
      </div>

     </div>
    </div>
   </ContentTemplate>
  </asp:UpdatePanel>
 </div>
   <%--</ContentTemplate>
  </asp:UpdatePanel>
 </div>--%>

 <div id="divDeletesucessRss" class="modal backgroundoverlay" clientidmode="Static" runat="server"
  style="display: none;">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="modal-dialog modal-dialog-centered">
   <ContentTemplate>
    <div id="div2" runat="server" class="w-100" clientidmode="Static">
     <div class="modal-content">
      <div>
       <b>
        <asp:Label ID="lbl3" runat="server"></asp:Label>
       </b>
      </div>
      <div class="modal-header">
       <h5 class="modal-title"> Delete confirmation
       </h5>
      </div>
      <div class="modal-body">
       <asp:Label ID="lbl4" runat="server" Text="Do you want to delete?"></asp:Label>
      </div>
      <div class="modal-footer border-top-0">
       <asp:Button ID="lnkRssDeleteCancel" runat="server" class="add-scroller btn btn-outline-primary m-r-15" ClientIDMode="Static" Text="Cancel"
        OnClientClick="javascript:divCancelRss();return false;"></asp:Button>
       <asp:Button ID="lnkDeleteRssConfirm" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="javascript:divdeletesRss();"
        CssClass="btn btn-primary add-scroller" OnClick="lnkDeleteRssConfirm_Click"></asp:Button>
      </div>

     </div>
    </div>
   </ContentTemplate>
  </asp:UpdatePanel>
 </div>
 <script type="text/javascript">
  function setNewsId() {
      $('#hdnintNewsDelete').val($(this).children('#hdnNewsId').val());
  }
  function setRssId() {
      $('#hdnintRssDelete').val($(this).children('#hdnRssId').val());
  }
  function divCancels() {
      $('#divDeletesucessNews').hide();
  }
  function divCancelRss() {
      document.getElementById('rssModal').style.display = 'none';
      $('#divDeletesucessRss').css("display", "none");
      //$('#rssModal').modal('toggle');
      //$('#divDeletesucessRss').hide();
  }
  function divdeletes() {
      // $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 5px #00B7E5");         
      $('#divDeletesucessNews').css("display", "none");
    }
  function divdeletesRss() {
      // $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 5px #00B7E5");         
      $('#divDeletesucessRss').css("display", "none");
     }
  function docdeleteNews() {
      $('#divDeletesucessNews').css("display", "block");
      // $('#AddWorkExp').css("display", "none");
  }
  function docdeleteRss() {
      $('#divDeletesucessRss').css("display", "block");
  }
  function callClose() {         
      $('#txtSourceField').val('');
      $('#txtSourceLink').val('');
      $('#hdnintStatusUpdateId').val('');
      $('#dvSourceLinkErrorText').text('');
      $('#rssModal').modal('hide');      
  }
  function messageClose() {          
   if ($('#txtSourceField').val() != "" && $('#txtSourceLink').val() != "") {
       $('#rssModal').modal('hide');
  }
  }     
  function ShowEditPost(strPostId, Title, Source_link) { 
         $('#dvSourceLinkErrorText').text('');
         $('#rssModal').modal('show');
         //ClearOrgText(true);
   
         $('#hdnintStatusUpdateId').val(strPostId);

         //$lat.val(Title);
         //document.getElementById(lat).value = Title;
         //document.getElementById(lon).value = Source_link;
         //$('#ctl00_ContentPlaceHolder1_txtSourceField').val(Title);
         //$('#ctl00_ContentPlaceHolder1_txtSourceLink').val(Source_link);
         $('#txtSourceField').val(Title)
         $('#txtSourceLink').val(Source_link)
   
         //$('#txtPostUpdate').val(strPostDescription);
         //if (strPostType && strPostType != '') {
         //    $('#ddlPostType').val(strPostType);
         //}
         document.getElementById('rssModal').style.display = 'none';
         $('#rssModal').modal('toggle');
  }
 </script>
 <script type="text/javascript">
    $(document).ready(function () {
        var showChar = 150;
        var ellipsestext = "";
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
            //$('#lnkNextshow').css("display", "block");
            $('#lnkNext').css("display", "none");
        }
        if ($('#hdnCurrentPageRss').val() == $('#hdnEndPageRss').val()) {
            //$('#lnkNextshow').css("display", "block");
            $('#lnkNextRss').css("display", "none");
        }
        //$("#txtSearchQuestion").keydown(function (e) {
        //    if (e.keyCode == 13) {
        //        $('#btnSave1').click();
        //        e.preventDefault();
        //    }
        //});

        //$('#searchBtn').on('click', function () {
        //  document.getElementById("btnSave").click();
        //})

        //  createChosen();
        //$('#selectControl').on('change', function (evt, params) {
        //    $('#selectControl').trigger('chosen:close');
        //    $('.chosen-drop').css('display','none');
        //    document.getElementById("btnSave1").click();
   
        //  });
        $("#dvPage a").on('click', function (event) { showLoader1(); });
        $("#dvPageRss a:enabled").on('click', function(event){showLoader1();});

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            if ($('#hdnCurrentPage').val() == $('#hdnEndPage').val()) {
                //$('#lnkNextshow').css("display", "block");
                $('#lnkNext').css("display", "none");
            }
            if ($('#hdnCurrentPageRss').val() == $('#hdnEndPageRss').val()) {
                //$('#lnkNextshow').css("display", "block");
                $('#lnkNextRss').css("display", "none");
            }
            //$("#txtSearchQuestion").keydown(function (e) {
            //    if (e.keyCode == 13) {
            //        $('#btnSave1').click();
            //        e.preventDefault();
            //    }
            //});



            // $('#searchBtn').on('click', function () {
            //      document.getElementById("btnSave").click();

            //});

            //createChosen();
            //  $('#selectControl').on('change', function (evt, params) {
            //      $('#selectControl').trigger('chosen:close');
            //      $('.chosen-drop').css('display','none');
            //      document.getElementById("btnSave1").click();

            //});
                    var showChar = 150;
        var ellipsestext = "";
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
    

            //});

            //createChosen();
            //  $('#selectControl').on('change', function (evt, params) {
            //      $('#selectControl').trigger('chosen:close');
            //      $('.chosen-drop').css('display','none');
            //      document.getElementById("btnSave1").click();

            //});

            $("#dvPage a").on('click', function (event) { showLoader1(); });
            $("#dvPageRss a:enabled").on('click', function(event){showLoader1();});
    
        });
    });
 </script>
</asp:Content>

