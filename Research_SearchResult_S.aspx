<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="Research_SearchResult_S.aspx.cs" Inherits="Research_SearchResult_S" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControl_MultiSelectjudges" Src="~/UserControl/MultiSelectJudges.ascx" %>
<%@ MasterType TypeName="Main" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
        <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
        <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
        <script type="text/javascript" src="<%=ResolveUrl("js/ddsmoothmenu.js")%>"></script>
        <script>
            $("#globalLoader").show();
        </script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div class="cls">
    </div>
    <div class="container-popupreserch modal_bg" id="divPopp" style="display: none;">
     <div class="popupreserch modal-dialog research_r" style="overflow:visible;">
      <div class="modal-header reserach_heading">
       Please Wait..
      </div>
     </div>
    </div>
    <div class="container-popuprewait" id="divWait" style="display: none;">
     <div class="popuprewait research_r">
      <img src="images/waitImg.gif" />
     </div>
    </div>--%>
    <!-- Modal start here  -->

   <asp:HiddenField ID="hdnAvoid" Value="false" runat="server" />
   <div id="successSaveSearch" runat="server" class="modal backgroundoverlay show" role="dialog" aria-labelledby="confirmationTitle" clientidmode="Static">
    <div class="modal-dialog modal-dialog-centered" role="document">
     <div class="modal-content">
      <div class="modal-header">
       <h5 id="successPopupTitle" class="modal-title">Success</h5>       
      </div>
      <div class="modal-body">
       <span id="successPopupMsg">Search saved successfully.</span>     
      </div>
      <div class="border-top-0 text-right padding-15">
       <a class="btn btn-primary add-scroller" onclick="$('#successSaveSearch').hide();" data-dismiss="modal" href="#">Ok</a>       
      </div>
     </div>       
    </div>
   </div><!-- modal-content ended -->
   <!-- Modal Ended here  -->

   <div id ="divSavesearch" clientidmode="Static" runat="server"  class="modal backgroundoverlay" role="dialog" aria-labelledby="confirmationTitle" aria-hidden="true" style="display: none;">
    <div class="modal-dialog modal-dialog-centered" >
     <div class="modal-content">
      <div class="modal-header">
       <h5 class="modal-title" id="confirmationTitle">Save Search</h5>
       <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="MessClose();">
        <span aria-hidden="true">&times;</span>
       </button>
      </div>
      <div class="modal-body">
       <div class="form-group">
        <label>Title</label>
        <asp:TextBox ID="txtsaveTitle" runat="server" placeholder="Enter title" oninput="CallSearchSave();"
         ValidationGroup="titles" autocomplete="off" ClientIDMode="Static"  class="forumTitlenew save_s form-control">             
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtsaveTitle" InitialValue="" Display="Dynamic" ClientIDMode="Static" 
         ValidationGroup="titles" ErrorMessage="Please Enter Title." ForeColor="Red" class="error_research_page">
        </asp:RequiredFieldValidator>
       </div>
      </div>
      
      <div class="modal-footer border-top-0 padding-top-0">
       <a href="#" ClientIDMode="Static" class="btn btn-outline-primary m-r-15" onclick="MessClose();">Cancel </a>  
       <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Save" ValidationGroup="titles" 
        CausesValidation="true" onClientClick="MessClose();showLoader1();" CssClass="btn btn-primary disabled" OnClick="lnkPopupOK_Click" >
       </asp:LinkButton>                
      </div>
     </div> 
    </div>
   </div>
   <!-- Modal ended Here -->

    <%--    <div id="" clientidmode="Static" runat="server" class="modal_bg Div_search_r" style="display: none;">
                    <div class="divsaveSearch modal-dialog modal-dialog-centered">
                        <div class="modal-header">
                            &nbsp;&nbsp; <b>Save Search</b>
                        </div>
                        
                        <br />
                      
                        <div class="cls"></div>
                        <div class="modal-footer save_search_popup research_f">
                          
                          
                        </div>
                        <br />
                    </div>
                </div>--%>

    <!--  <div class="innerContainer" id="documentContainer" style="margin-top: -40px;">
 
    </div> -->
        <div class="cls">
        </div>
        <div class="main-section-inner">
            <div class="panel-cover clearfix">
                <div class="innerGroupBox">
                    <div id="divsearchHeight" runat="server">            
                        <!--search box starts-->
                        <div class="research">
                         <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                          <ContentTemplate>
                           <div class="card">
                                <div class="card-body">
                                    <h4>Case Search</h4>
                                    <ul class="list-inline d-none d-sm-block">
                                        <li class="list-inline-item"><span class="custom-radio">
                                        <input type="radio" name="bn" id="0" value="F">
                                        <label for="0">Free Text</label>
                                    </span> </li>
                                        <li class="list-inline-item"> <span class="custom-radio">
                                        <input type="radio" name="bn" id="1" value="C">
                                        <label for="1">Citation</label>
                                    </span> </li>
                                        
                                        <li class="list-inline-item">
                                            <span class="custom-radio">
                                              <input type="radio" name="bn" id="3" value="T" >
                                        <label for="3">Target</label>
                                          </span>
                                        </li>
                                    </ul>
                                    <div class="form-group d-block select-wrapper d-sm-none">
                                     <asp:DropDownList ID="ddlSelect" runat="server" ClientIDMode="Static" CssClass="form-control">
                                      <asp:ListItem Text="Free Text" Value="F"></asp:ListItem>
                                      <asp:ListItem Text="Citation" Value="C"></asp:ListItem>
                                      <%--<asp:ListItem Text="Skorkel" Value="S"></asp:ListItem>--%>
                                      <asp:ListItem Text="Target" Value="T"></asp:ListItem>
                                     </asp:DropDownList>
                                    </div>
                                    <!--Free Text start here-->
                                    <div id="freetext" style="display: block;">
                                      
                                        <div id="divFreeTextSearchInput" class="form-group">
                                         <asp:UpdatePanel ID="ss" runat="server" UpdateMode="Conditional">
                                         <ContentTemplate>
                                         <%--<asp:Panel runat="server" DefaultButton="Button1">--%>
                                         <asp:TextBox ID="txtResearch" runat="server" type="text" AutoPostBack="false" OnTextChanged="txtResearch_TextChanged" CssClass="form-control"
                                             ClientIDMode="Static" placeholder="Enter Keywords" autocomplete="off" onkeyup="txtResearchRemoveError();"
                                             onKeyDown="javascript:return txtResearchkeydown(event);" onkeypress="txtResearchRemoveError();"
                                             oninput="postCommentInput.bind(this)(event)">
                                         </asp:TextBox>
                                         <ajax:AutoCompleteExtender ID="AutoCompleteExtender12" ServiceMethod="GetCompletionList" MinimumPrefixLength="3" 
                                          EnableCaching="false" CompletionSetCount="10" TargetControlID="txtResearch" CompletionInterval="100"
                                          OnClientItemSelected="autoCompleteEx_ItemSelected" OnClientShown="PopupShown" runat="server" FirstRowSelected="false"
                                          CompletionListCssClass="AutoCompletionList" CompletionListItemCssClass="AutoComp_listItem" 
                                          CompletionListHighlightedItemCssClass="AutoComp_itemHighlighted">
                                         </ajax:AutoCompleteExtender>
                                         <%--<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Style="display: none" />
                                         </asp:Panel>--%>
                                         </ContentTemplate>
                                         <%--<Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                         </Triggers>--%>
                                         </asp:UpdatePanel>
                                         <span class="icon-search" onclick="$('#btnSearch').click();"></span>
                                         <div style="display:none;">
                                          <asp:ImageButton runat="server" ID="btnSearch" ClientIDMode="Static" OnClick="btnSearch_Click"  />
                                             
                                         </div>
                                            <div style="display:none; color: red;" id="dvtxtInputError">Please enter some text or use target search</div>

                                         
                                        </div>
                                          
                                        <a id="aAdvancedSearch" data-toggle="collapse" href="#advancesearch" runat="server" clientidmode="Static" class="advsearch" role="button" aria-expanded="false" aria-controls="advancesearch">Advanced Search</a>
                                        <div class="collapse" id="advancesearch">
                                            <div class="form-group">
                                                <label for="nameofjudge" id="lbljudgename" runat="server" clientidmode="Static">Name of the Judges</label>
                                               
                                                <asp:UpdatePanel runat="server" ID="upJudges" ClientIDMode="Static" UpdateMode="Conditional">
                                                    <ContentTemplate>                             
                                                
                                                    <uc1:UserControl_MultiSelectJudges ID="txtJudgesMember" EnableViewState="false" ClientIDMode="Static" runat="server" placeholder="Select multiple subjects to add in search" />
                                                <!--   <asp:ImageButton runat="server" ID="imgBtntarget" ImageUrl="images/research-search.png" OnClientClick="javascript:targetCall();return true;" ClientIDMode="Static" class="reserachSearchImg research_r_l" OnClick="btnSearch_Click" /> -->
                                                <%--<asp:RequiredFieldValidator InitialValue="Enter the names of the Judges" ValidationGroup="InvMem" ClientIDMode="Static" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtJudgesMember" ErrorMessage="Enter members name" ForeColor="Red" class="search_r_s_l"></asp:RequiredFieldValidator>--%>
                                                
                                                <asp:Button runat="server" ID="btnJudges" ClientIDMode="Static" Style="display:none" OnClick="btnJudges_Click" />
                                                  </ContentTemplate> 
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnJudges" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <asp:HiddenField ID="hdnMembId" ClientIDMode="Static" runat="server" />
                                                <asp:HiddenField ID="hdnMembIdText" ClientIDMode="Static" runat="server" />
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-12">
                                                    <div class="form-group">
                                                        <label>Year</label>
                                                        <div class=" select-wrapper">
                                                          <asp:DropDownList ID="ddlYearFT" runat="server" ClientIDMode="Static" CssClass="yearList form-control">
                                                          </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-12">
                                                    <div class="form-group">
                                                        <label>Court</label>
                                                         <div class=" select-wrapper">
                                                            <asp:DropDownList ID="ddlCourtFT" runat="server" ClientIDMode="Static" CssClass="yearList year_l_r form-control">
                                                            </asp:DropDownList>
                                                         </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="nameofjudge" id="lblcontext">Context</label>
                                                
                                                <uc:UserControl_MultiSelect ID="txtSubjectList" ClientIDMode="Static" runat="server" placeholder="Select multiple subjects to add in search" onchange="getAllSubjectValue(this.id)"/>
                                                <asp:HiddenField ID="hdnsubject" ClientIDMode="Static" runat="server" />
                                                <asp:HiddenField ID="hdnsubjectText" ClientIDMode="Static" runat="server" />
                                            </div>
                                            <div class="text-right">
                                                <a href="#" class="btn btn-primary" onclick="$('#btnSearch').click();" >Search</a>
                                            </div>
                                        </div>
                                    </div>
                                    <!--Free Text End  here-->
                                    <div id="citation" runat="server" clientidmode="Static">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-12">
                                                <div class="form-group">
                                                    <label>Year</label>
                                                    <div class="select-wrapper">
                                                      <asp:DropDownList ID="ddlYear" runat="server" ClientIDMode="Static" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-12">
                                                <div class="form-group">
                                                    <label>Court</label>
                                                    <div class="select-wrapper">
                                                      <asp:DropDownList ID="ddlCourt" runat="server" ClientIDMode="Static" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                      
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-12"> <div class="form-group">
                                                    <label>Reporter Name</label>
                                                    <div class="select-wrapper">
                                                      <asp:DropDownList ID="ddlReporterName" runat="server" ClientIDMode="Static" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    </div>
                                                  </div>  
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-12">  <div class="form-group">
                                                    <label>Volume</label>
                                                    <div class="select-wrapper">
                                                      <asp:DropDownList ID="ddlVolumns" runat="server" ClientIDMode="Static" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-6 col-12">
                                                    <label>Page No.</label>
                                                    <div class="select-wrapper">
                                                      <asp:DropDownList ID="ddlPageNo" runat="server" ClientIDMode="Static" CssClass="form-control">
                                                      </asp:DropDownList>
                                                    </div>  
                                                </div>
                                            </div>
                                        </div>
                                        <div class="text-right">
                                            <a href="#" class="btn btn-primary" onclick="javascript:citationCall();$('#btnSearch').click();" >Search</a>
                                        </div>
                                    </div>
                                    <%--<div id="skorkel">
                                        <div class="form-group">
                                            <input type="text" name="Search" placeholder="Enter Keywords" class="form-control searchkey">
                                            <span class="icon-search"></span>
                                        </div>
                                    </div>
                                    <div id="divFreeSkorlTargetSearch" runat="server" clientidmode="Static">
                                        <div class="form-group">
                                            <label for="nameofjudge">Name of the Judges</label>
                                            <select data-placeholder="Enter the names of the judges" class="chosen-select researchSearchTxtInput margin_top_minu_f form-control" id="txtJudgesMember1" onchange="getAllMemberValue(this.id)" runat="server" clientidmode="Static" multiple tabindex="4">
                                            </select>
                                            <asp:ImageButton runat="server" ID="imgBtntarget1" ImageUrl="images/research-search.png" OnClientClick="javascript:targetCall();return true;" ClientIDMode="Static" class="reserachSearchImg research_r_l" OnClick="btnSearch_Click" />
                                            <asp:RequiredFieldValidator InitialValue="Enter the names of the Judges" ValidationGroup="InvMem" ClientIDMode="Static" ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtJudgesMember1" ErrorMessage="Enter members name" ForeColor="Red" class="search_r_s_l"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hdnMembIda" ClientIDMode="Static" runat="server" />
                                            <asp:HiddenField ID="hdnMembIdTexta" ClientIDMode="Static" runat="server" />
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-12">
                                                <div class="form-group">
                                                    <label>Year</label>
                                                    <asp:DropDownList ID="ddlYearFT1" runat="server" ClientIDMode="Static" CssClass="yearList form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 col-12">
                                                <div class="form-group">
                                                    <label>Court</label>
                                                    <asp:DropDownList ID="ddlCourtFT2" runat="server" ClientIDMode="Static" CssClass="form-control yearList year_l_r">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="nameofjudge">Context</label>
                                            <div class="provisionList pr_l_r research_result_m form-control">
                                                <span class="test_context">Context</span>
                                                <br />
                                            </div>
                                            <div id="MicroTag" class="subjectMacroTags border_none">
                                                <div class="researchTagAreaa research_selectbox" style="display: none;">
                                                    <ul>
                                                        <asp:UpdatePanel ID="uplstSubject" runat="server">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="lstSubjCategory" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                <asp:ListView ID="lstSubjCategory" runat="server" OnItemDataBound="LstSubjCategory_ItemDataBound" OnItemCommand="LstSubjCategory_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <div class="subjectTags">
                                                                            <div class="researchTag">
                                                                                <li id="SubLi" runat="server">
                                                                                    <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                                                    <asp:LinkButton ID="lnkCatName" CssClass="copr" Font-Underline="false" runat="server" Text='<%#Eval("strCategoryName")%>' CommandName="Subject Category"></asp:LinkButton>
                                                                                    <div class="corpClose">
                                                                                        <img src="images/research-tag-close.png" alt="no image."></div>
                                                                                </li>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ul>
                                                </div>
                                            </div>
                                            <div class="text-right">
                                                <a class="btn btn-primary" onclick="$('#btnSearch').click();" >Search</a>
                                            </div>
                                        </div>
                                        <!--left box starts-->
                                    </div>--%>
                                </div>
                              </div>
                          </ContentTemplate>
                          <%--<Triggers>
                           <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                          </Triggers>--%>
                         </asp:UpdatePanel>
                         <!--left box ends-->
                         <div id="divResearchResult">
                          <asp:UpdatePanel runat="server" ID="Up1" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                 <%--UpdateMode="Conditional"--%>
                                 <ContentTemplate>
                                     <div class="row justify-content-center mb-2" ID="divlbls" runat="server" Visible="false">
                                         <div class="col-lg-8 col-md-8 col-7 col-sm-8">
                                          <h4>
                                           <asp:Label ID="lblResultCount" runat="server" Text="0"></asp:Label><asp:HiddenField ID="hdnEnterKeyword" runat="server" ClientIDMode="Static" Value="" /> <%--(Entered Keyword or Search Attributes)--%> 
                                            Results for 
                                           <asp:Label id="lblEnterKeyword" runat="server" ClientIDMode="Static" Text="Entered Keyword or Search Attributes"></asp:Label>
                                          </h4>
                                         </div>
                                         <div class="col-sm-4 col-md-4 col-5 col-lg-4">
                                          <div class="row">
                                           
                                           <div class="col-lg-7 col-md-7 col-sm-7 col-12">
                                            <div id="smoothmenu1" class="ddsmoothmenu" style="display:none;">
                                            <div class="fliter-con">
                                             <div class="dropdown ">
                                               <a class="sortFilter d-flex align-items-center justify-content-start justify-content-lg-end justify-content-md-end justify-content-sm-end" id="sortFilter" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" role="button"> Sort by: &nbsp;<strong> Most Recent</strong> <span class="icon-caret-down"></span></a>
                                               <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                <asp:LinkButton ID="lnkMR" class="dropdown-item" runat="server" OnClick="lnkMR_click">Most Recent</asp:LinkButton>
                                                <asp:LinkButton class="dropdown-item" ID="lnkMS" runat="server" OnClick="lnkMS_click">Most Shared</asp:LinkButton>
                                                <asp:LinkButton class="dropdown-item" ID="lnkMD" runat="server" OnClick="lnkMD_click">Most Downloaded</asp:LinkButton>
                                                <asp:LinkButton class="dropdown-item" ID="lnkComment" runat="server" OnClick="lnkComment_click">Most Commented</asp:LinkButton>
                                               </div>
                                             </div>
                                            </div>
                                           </div>
                                           </div>
                                           <div class="col-lg-5 col-md-5 col-sm-5 col-12 text-right">
                                            <asp:LinkButton ID="lnkSAvesearch" runat="server" ClientIDMode="Static" class="hide-body-scroll" Text="Save this Search" Enabled="true" OnClick="btnSaveSearch_Click" OnClientClick="return btnSaveSearch_onclick()">
                                            </asp:LinkButton>
                                           </div>
                                          </div>
                                         </div>
                                        </div>
                                     <div class="research-list">
                                            <asp:ListView ID="lstSearchResult" runat="server">
                                                <ItemTemplate>
                                                    <div class="card card-list-con blog-list research-list">
                                                        <div class="list-group-item top-list">
                                                            <div class="post-con">
                                                           <!--      <div class="post-header">
                                                                    <span class="question-icon">
                                             <span class="icon"><img src="images/file.svg"></span>
                                                                    </span>
                                                                    <ul class="que-con">
                                                                        <li>Case</li>
                                                                    </ul>
                                                                </div> -->
                                                                <div class="post-body">
                                                                    <asp:HiddenField ID="judgeNames" Value='<%# Eval("judgeNames") %>' runat="server" ClientIDMode="Static" />
                                                                    <h3> 
                                                                        <div class="tabArrow mb-1" onclick="tabArrowNavi.bind(this)()">
                                                                            <asp:HiddenField ID="hdnDocIdArrow" Value='<%# Eval("docUid") %>' runat="server" ClientIDMode="Static" />
                                                                            <asp:Linkbutton ID="lblPartyName" OnClientClick="return false;" runat="server" Text='<%#Eval("title")%>' ClientIDMode="Static"></asp:Linkbutton>
                                                                        </div>
                                                                    </h3>
                                                                    <span class="date"> 
                                                                        <asp:Label ID="lblCourt" runat="server" Text='<%#Eval("court")%>' ClientIDMode="Static"></asp:Label>
                                                                        <asp:Label ID="lblYear" runat="server" Text='<%#Eval("year")%>' ClientIDMode="Static"></asp:Label>
                                                                    </span>
                                                                    <span class="date"><asp:Label ID="lblCitation" runat="server" Text='<%#Eval("citation")%>' ClientIDMode="Static"></asp:Label></span>
                                                                    <p class="tabHeadingNew d-none" onclick="headingNavi.bind(this)()">
                                                                        <asp:HiddenField ID="hdnDocId" Value='<%# Eval("docUid") %>' runat="server" ClientIDMode="Static" />
                                                                        <asp:LinkButton ID="lbldisplayContent" OnClientClick="return false;" runat="server" Text='<%#Eval("title")%>' ></asp:LinkButton>
                                                                        
                                                                    </p>
                                                                   
                                                                </div>
                                                                <div class="post-footer">
                                                                    <ul class="list-inline">

<!--                                                                       <li class="list-inline-item"><a class="active-toogle un-anchor"><span class="comment-btn"></span><asp:Label ID="lblcommentCount" runat="server" Text='<%#Eval("commentCount") + (((int)Eval("commentCount") == 1) ? " Comment" : " Comments") %>'></asp:Label></a></li> <%--&nbsp;Comments--%>-->
                                                                        
                                                                        <li class="list-inline-item"><div  class="active-toogle un-anchor"><span class="share-btn"></span><asp:Label ID="lblshareCount" runat="server" Text='<%#Eval("shareCount") + (((int)Eval("shareCount") == 1) ? " Share" : " Shares") %>'></asp:Label></div></li><%--&nbsp;Shares--%>
                                                                        <li class="list-inline-item"><div  class="active-toogle un-anchor"><span class="download-view"></span><asp:Label ID="lbldownloadCount" runat="server" Text='<%#Eval("downloadCount") + (((int)Eval("downloadCount") == 1) ? " Download" : " Downloads") %>'></asp:Label></div></li><%--&nbsp;Downloads--%>
                                                                        <li class="list-inline-item"><div class="active-toogle un-anchor"><span class="tag-btn"></span><asp:Label ID="lbltagCnt" runat="server" Text='<%#Eval("tagCnt") + ((Eval("tagCnt").ToString() == "1") ? " Tag" : " Tags") %>'></asp:Label></div></li> <%--&nbsp;Bookmarks--%>

                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--tab text starts-->
                                                    <!-- <div class="tabTxtBox list_style position_relative">
                                                        
                                                        <div class="tabTxt">
                                                            <div class="partyNameLabel">
                                                                
                                                            </div>
                                                            <div class="partyNameLabelTxt">
                                                                
                                                            </div>
                                                            <div class="partyNameLabel">
                                                               
                                                            </div>
                                                            <div class="cls">
                                                            </div>
                                                           
                                                            <div class="citedBy">
                                                                Cited By:
                                                                <asp:Label ID="lblCitedBy" runat="server" Text='<%#Eval("citedBy")%>'></asp:Label>
                                                                Cases
                                                            </div>
                                                            <div class="citedBy display_none">
                                                                Doc Type:
                                                                <asp:Label ID="lbldocType" runat="server" Text='<%#Eval("docType")%>'></asp:Label>
                                                            </div>
                                                            <div class="cls">
                                                            </div>
                                                            <div class="commentsTag">
                                                                <img src="images/comments_new.png" /> Comments:
                                                              
                                                            </div>
                                                            <div class="commentsTag">
                                                                <img src="images/share_new.png" />Share:
                                                                
                                                            </div>
                                                            <div class="commentsTag">
                                                                <img src="images/download_new.png" /> Download:
                                                                
                                                            </div>
                                                            <div class="commentsTag for_tag_image">
                                                                <img src="images/tags_new_one.png" />Total Tags:
                                                                
                                                            </div>
                                                        </div>
                                                        <div class="tabArrow">
                                                            
                                                            <asp:ImageButton  ImageUrl="~/images/view_more_page.png" />
                                                        </div>
                                                    </div> -->
                                                    <!--tab text ends-->
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                     
                                    
                                     <div id="pLoadMore" runat="server" align="center">
                                            <div id="imgLoadMore" class="lds-ellipsis" style="display:none;">
                                              <div></div>
                                              <div></div>
                                              <div></div>
                                              <div></div>
                                             </div>
                                            <asp:Button ID="imgLoadMore1" runat="server" ClientIDMode="Static" Style="display: none;"
                                                CssClass="imageLoadmore" OnClick="imgLoadMore_OnClick" Height="100px" Width="100px" />
                                        </div>
                                     
                                     <%--<p align="center">--%>
                                     <div style="display:none;">
                                         <asp:Label ID="lblNoMoreRslt" Text="No more results available..." runat="server" ClientIDMode="Static" ForeColor="Red" Visible="false"></asp:Label>
                                        </div>
                                     <%--</p>--%>
                                     <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
                                     </div>
                                 </ContentTemplate>
                                 <Triggers>
                                      <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                 </Triggers>
                          </asp:UpdatePanel>
                         </div>
                        </div>
                        <div class="TooltipDetailsPop" id="eventPop" ClientIDMode="Static">
                        </div>
                    </div>
                    <asp:HiddenField ID="CiYear" runat="server" ClientIDMode="Static" Value="Year" />
                    <asp:HiddenField ID="CiCourt" runat="server" ClientIDMode="Static" Value="Court" />
                    <asp:HiddenField ID="CiReport" runat="server" ClientIDMode="Static" Value="Reporter Name" />
                    <asp:HiddenField ID="CiVolume" runat="server" ClientIDMode="Static" Value="Volume" />
                    <asp:HiddenField ID="CiPage" runat="server" ClientIDMode="Static" Value="Page No." />
                    <asp:HiddenField ID="AdvSearchExpanded" runat="server" ClientIDMode="Static" Value="0" />
                    <asp:HiddenField ID="hdnSearchSelect" runat="server" ClientIDMode="Static" Value="" />
                    <asp:HiddenField ID="CiYearVal" runat="server" ClientIDMode="Static" Value="" />
                    <asp:HiddenField ID="CiCourtVal" runat="server" ClientIDMode="Static" Value="" />
                    <asp:HiddenField ID="CiReportVal" runat="server" ClientIDMode="Static" Value="" />
                    <asp:HiddenField ID="CiVolumeVal" runat="server" ClientIDMode="Static" Value="" />
                    <asp:HiddenField ID="CiPageVal" runat="server" ClientIDMode="Static" Value="" />
                    <!--left verticle search list ends-->
                </div>
            </div>
        <script type="text/javascript">
             function PopupShown(sender, args) {
                 sender._popupBehavior._element.style.zIndex = 99999999;
                 if (window.searchEnter) {
                     sender._popupBehavior._element.style.display = "none";
                 } else {
                     sender._popupBehavior._element.style.display = "block";
                 }
            }
            function autoCompleteEx_ItemSelected(sender, args) {
                showLoader1();
                __doPostBack(sender.get_element().name, "");
                //var results = eval('(' + args.get_value() + ')');
                //$get('txtResearch').value = results.Code;
            }
            function postCommentInput(e) {
                var msg = $(this).val().replace(/\n/g, "");
                if (msg != $(this).val()) { // Enter was pressed
                    $(this).val(msg);
                    window.location.href = $(this).parent().parent().children('.lnkEnterCommentcss').attr('href');
                    showLoader1();
                }
            }
        </script>
        <script type="text/javascript">       
            $(document).ready(function() {
            //if ($('#ddlSelect').val() == 'Select Search Type') {
            //    $('#txtResearch').val('');
            //    $('#txtResearch').attr("placeholder", "Please select 'Search Type' to start searching");
            //    $('#divCitationSearch').css('display', 'none');
            //    $('#divFreeSkorlTargetSearch').css('display', 'none');
            //    $('#pAdvSearch').css('display', 'none');
            //}
            //if ($('#ddlSelect').val() == 'C') {
            //    $('#divMainSearch').css('display', 'none');
            //    $('#divFreeSkorlTargetSearch').css('display', 'none');
            //    $('#divCitationSearch').css('display', 'block');
            //}
            //if ($('#ddlSelect').val() == 'T') {
            //    $('#divMainSearch').css('display', 'none');
            //    $('#divCitationSearch').css('display', 'none');
            //    $('#lbljudgename').css('display', 'none');
            //    $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
            //    $('#imgBtntarget').css('display', 'block');
            //}
            //if ($('#ddlSelect').val() == 'S') {
            //    $('#divCitationSearch').css('display', 'none');
            //}
            //$("#ddlSelect").change(function() {
            //    if ($('#ddlSelect').val() == 'Select Search Type') {
            //        $('#txtResearch').val('');
            //        $('#txtResearch').attr("placeholder", "Please select 'Search Type' to start searching");
            //        $('#divCitationSearch').css('display', 'none');
            //        $('#divFreeSkorlTargetSearch').css('display', 'none');
            //        $('#pAdvSearch').css('display', 'none');
            //    } else if ($('#ddlSelect').val() == 'C') {
            //        $('#hdnEnterKeyword').val('');
            //        $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
            //        $('#imgBtntarget').css('display', 'none');
            //        $('#txtResearch').val('');
            //        $('#txtResearch').attr("placeholder", "Enter Keywords");
            //        $('#divCitationSearch').css('display', 'block');
            //        $('#divFreeSkorlTargetSearch').css('display', 'none');
            //        $('#pAdvSearch').css('display', 'none');
            //        $('#divMainSearch').css('display', 'none');
            //        $('#txtJudgesMember').val('').trigger('chosen:updated');
            //        $('#hdnMembId').val('');
            //        $('#hdnMembIdText').val('');
            //        $('#hdnsubject').val('');
            //        $('#hdnsubjectText').val('');
            //        $('#txtSubjectList').val('').trigger('chosen:updated');
            //        $("#ddlYearFT option:first").attr("selected", true);
            //        $("#ddlCourtFT option:first").attr("selected", true);
            //        $("#ddlYear option:first").attr("selected", true);
            //        $("#ddlCourt option:first").attr("selected", true);
            //        $("#ddlReporterName option:first").attr("selected", true);
            //        $("#ddlVolumns option:first").attr("selected", true);
            //        $("#ddlPageNo option:first").attr("selected", true);
            //    } else if ($('#ddlSelect').val() == 'F') {
            //        $('#hdnEnterKeyword').val('');
            //        $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
            //        $('#imgBtntarget').css('display', 'none');
            //        $('#divFreeSkorlTargetSearch').css('display', 'none');
            //        $('#txtResearch').css('display', 'block');
            //        $('#btnSearch').css('display', 'block');
            //        //$('#btnSearch').css('margin-top', '-35px');
            //        $('#txtResearch').val('');
            //        $('#txtResearch').attr("placeholder", "Enter Keywords");
            //        $('#divCitationSearch').css('display', 'none');
            //        $('#pAdvSearch').css('display', 'block');
            //        $('#divMainSearch').css('display', 'block');
            //        $('#txtJudgesMember').val('').trigger('chosen:updated');
            //        $('#hdnMembId').val('');
            //        $('#hdnMembIdText').val('');
            //        $('#hdnsubject').val('');
            //        $('#hdnsubjectText').val('');
            //        $('#txtSubjectList').val('').trigger('chosen:updated');
            //        $("#ddlYearFT option:first").attr("selected", true);
            //        $("#ddlCourtFT option:first").attr("selected", true);
            //        $('#lbljudgename').css('display', 'block');
            //        $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
            //        $("#ddlYear option:first").attr("selected", true);
            //        $("#ddlCourt option:first").attr("selected", true);
            //        $("#ddlReporterName option:first").attr("selected", true);
            //        $("#ddlVolumns option:first").attr("selected", true);
            //        $("#ddlPageNo option:first").attr("selected", true);
            //        $('#anchorAdvnceSearch').text('Advanced Search');
            //    } else if ($('#ddlSelect').val() == 'S') {
            //        $('#hdnEnterKeyword').val('');
            //        $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
            //        $('#imgBtntarget').css('display', 'none');
            //        $('#divFreeSkorlTargetSearch').css('display', 'none');
            //        $('#txtResearch').val('');
            //        $('#txtResearch').attr("placeholder", "Enter Keywords");
            //        $('#divCitationSearch').css('display', 'none');
            //        $('#pAdvSearch').css('display', 'block');
            //        $('#divMainSearch').css('display', 'block');
            //        $('#txtJudgesMember').val('').trigger('chosen:updated');
            //        $('#hdnMembId').val('');
            //        $('#hdnMembIdText').val('');
            //        $('#hdnsubject').val('');
            //        $('#hdnsubjectText').val('');
            //        $('#txtSubjectList').val('').trigger('chosen:updated');
            //        $("#ddlYearFT option:first").attr("selected", true);
            //        $("#ddlCourtFT option:first").attr("selected", true);
            //        $('#lbljudgename').css('display', 'block');
            //        $('#lbljudgename').css('margin-left', '7px');
            //        $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
            //        $("#ddlYear option:first").attr("selected", true);
            //        $("#ddlCourt option:first").attr("selected", true);
            //        $("#ddlReporterName option:first").attr("selected", true);
            //        $("#ddlVolumns option:first").attr("selected", true);
            //        $("#ddlPageNo option:first").attr("selected", true);
            //        $('#anchorAdvnceSearch').text('Advanced Search');
            //    } else if ($('#ddlSelect').val() == 'T') {
            //        $('#hdnEnterKeyword').val('');
            //        $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
            //        $('#imgBtntarget').css('display', 'block');
            //        $('#divFreeSkorlTargetSearch').css('display', 'block');
            //        $('#txtResearch').val('');
            //        $('#txtResearch').attr("placeholder", "Enter Keywords");
            //        $('#divCitationSearch').css('display', 'none');
            //        $('#pAdvSearch').css('display', 'block');
            //        $('#divMainSearch').css('display', 'none');
            //        $('#hdnsubject').val('');
            //        $('#hdnsubjectText').val('');
            //        $('#txtSubjectList').val('').trigger('chosen:updated');
            //        $('#txtJudgesMember').val('').trigger('chosen:updated');
            //        $('#hdnMembId').val('');
            //        $('#hdnMembIdText').val('');
            //        $("#ddlYearFT option:first").attr("selected", true);
            //        $("#ddlCourtFT option:first").attr("selected", true);
            //        $('#lbljudgename').css('display', 'none');
            //        $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
            //        $("#ddlYear option:first").attr("selected", true);
            //        $("#ddlCourt option:first").attr("selected", true);
            //        $("#ddlReporterName option:first").attr("selected", true);
            //        $("#ddlVolumns option:first").attr("selected", true);
            //        $("#ddlPageNo option:first").attr("selected", true);
            //    }
            //});
            //$('#anchorAdvnceSearch').click(function() {
            //    if ($('#anchorAdvnceSearch').text() == 'Advanced Search') {
            //        $('#anchorAdvnceSearch').text('Hide');
            //    } else {
            //        $('#anchorAdvnceSearch').text('Advanced Search');
            //    }
            //    $('#divFreeSkorlTargetSearch').slideToggle('slow');
            //});
            $("#ddlYear").change(function() {
                //alert($("#ddlYear").val());
                $("#ddlCourt").prop("disabled", false);
                $("#ddlReporterName").prop("disabled", false)
                $("#ddlVolumns").prop("disabled", false)
                $("#ddlPageNo").prop("disabled", false)
                $("#CiYear").val($("#ddlYear option:selected").text());
                $("#CiYearVal").val($("#ddlYear option:selected").val());
                var listCourt = "";
                var listReport = "";
                var YearIDs = $("#ddlYear").val();
                if ($("#ddlYear").val() == 'None')
                    YearIDs = 0;

                $.ajax({
                    type: "POST",
                    url: "Research_SearchResult_S.aspx/GetCourts",
                    data: "{'yearId':" + (YearIDs) + "}",
                    contentType: "application/json; charset=utf-8",
                    global: false,
                    async: false,
                    dataType: "json",
                    success: function (response) {
                        if (response.d == "") {
                            response.d = "[]";
                        }

                        var data = eval('(' + response.d + ')');
                        listCourt = "<option value='0'>Court</option>";
                        listReport = "<option value='0'>Reporter Name</option>";
                        if (YearIDs != 0)
                            for (var i = 0; i < data.length; i++) {
                                listCourt += "<option value='" + data[i].intEqCourtId + "'>" + data[i].strEqCourt + "</option>";
                            }
                        $("#ddlCourt").html(listCourt);
                        $("#ddlReporterName").html(listReport);
                        $(".container-popuprewait").hide();

                        $.ajax({
                            type: "POST",
                            url: "Research_SearchResult_S.aspx/GetRCourts",
                            data: "{'yearId':" + (YearIDs) + "}",
                            contentType: "application/json; charset=utf-8",
                            global: false,
                            async: false,
                            dataType: "json",
                            success: function (response) {
                                if (response.d == "") {
                                    response.d = "[]";
                                }
                                var data = eval('(' + response.d + ')');
                                listReport = "<option value='0'>Reporter Name</option>";
                                for (var i = 0; i < data.length; i++) {
                                    listReport += "<option value='" + data[i].intReportId + "'>" + data[i].strReportName + "</option>";
                                }
                                $("#ddlReporterName").html(listReport);
                                $(".container-popuprewait").hide();
                                if ($setUpCitations) {
                                    if ($('#CiCourtVal').val() != "" && $('#CiCourtVal').val() != "0") {
                                        $("#ddlCourt").val($('#CiCourtVal').val()).change();
                                    } else if ($('#CiReportVal').val() != "" && $('#CiReportVal').val() != "0") {
                                        $("#ddlReporterName").val($('#CiReportVal').val()).change();
                                    } else {
                                        $setUpCitations = false;
                                    }
                                } else {
                                    $('#CiCourtVal').val('');
                                    $("#CiCourt").val("Court");
                                    $("#ddlCourt").trigger('change');
                                }
                            }
                        });
                    }
                });
            });
            $("#ddlCourt").change(function() {
                var listCourt = "";
                var listReport = "";
                $("#CiCourt").val($("#ddlCourt option:selected").text());
                $("#CiCourtVal").val($("#ddlCourt option:selected").val());
                var YearIDs = $("#ddlYear").val();
                if ($("#ddlYear").val() == 'None')
                    YearIDs = 0;
                $.ajax({
                    type: "POST",
                    url: "Research_SearchResult_S.aspx/GetReports",
                    data: "{'courtId':" + ($("#ddlCourt").val()) + ",'yearId':" + (YearIDs) + "}",
                    contentType: "application/json; charset=utf-8",
                    global: false,
                    async: false,
                    dataType: "json",
                    success: function (response) {
                        if (response.d == "") {
                            response.d = "[]";
                        }
                        var data = eval('(' + response.d + ')');
                        listReport = "<option value='0'>Reporter Name</option>";
                        for (var i = 0; i < data.length; i++) {
                            listReport += "<option value='" + data[i].intReportId + "'>" + data[i].strReportName + "</option>";
                        }
                        $("#ddlReporterName").html(listReport);
                        if ($setUpCitations) {
                            if ($('#CiReportVal').val() != "" && $('#CiReportVal').val() != "0") {
                                $("#ddlReporterName").val($('#CiReportVal').val()).change();
                            } else {
                                $setUpCitations = false;
                            }
                        } else {
                            $("#CiReport").val("Reporter Name");
                            $("#CiReportVal").val("");
                            $("#ddlReporterName").trigger('change');
                        }
                    }
                });
            });
            $("#ddlReporterName").change(function() {
                var listvolume = "";
                var listpage = "";
                var YearIDs = $("#ddlYear").val();
                if ($("#ddlYear").val() == 'None')
                    YearIDs = 0;
                $("#CiReport").val($("#ddlReporterName option:selected").text());
                $("#CiReportVal").val($("#ddlReporterName option:selected").val());
                $.ajax({
                    type: "POST",
                    url: "Research_SearchResult_S.aspx/GetVolumns",
                    data: "{'reportId':" + ($("#ddlReporterName").val()) + ",'yearId':" + (YearIDs) + "}",
                    contentType: "application/json; charset=utf-8",
                    global: false,
                    async: false,
                    dataType: "json",
                    success: function (response) {
                        if (response.d == "") {
                            response.d = "[]";
                        }
                        var data = eval('(' + response.d + ')');
                        //alert(data);
                        listvolume = "<option value='0'>Volume</option>";
                        listpage = "<option value='0'>Page No.</option>";
                        for (var i = 0; i < data.length; i++) {
                            listvolume += "<option value='" + data[i].intVolumeId + "'>" + data[i].strEqVolume + "</option>";
                        }
                        $("#ddlVolumns").html(listvolume);
                        $("#ddlPageNo").html(listpage);
                        $.ajax({
                            type: "POST",
                            url: "Research_SearchResult_S.aspx/GetPVolumns",
                            data: "{'reportId':" + ($("#ddlReporterName").val()) + ",'yearId':" + (YearIDs) + "}",
                            contentType: "application/json; charset=utf-8",
                            global: false,
                            async: false,
                            dataType: "json",
                            success: function (response) {
                                if (response.d == "") {
                                    response.d = "[]";
                                }
                                var data = eval('(' + response.d + ')');
                                listpage = "<option value='0'>Page No.</option>";
                                for (var i = 0; i < data.length; i++) {
                                    listpage += "<option value='" + data[i].intPageNo + "'>" + data[i].intPageNumber + "</option>";
                                }
                                $("#ddlPageNo").html(listpage);
                                if ($setUpCitations) {
                                    if ($('#CiVolumeVal').val() != "" && $('#CiVolumeVal').val() != "0") {
                                        $("#ddlVolumns").val($('#CiVolumeVal').val()).change();
                                    } else if ($('#CiPageVal').val() != "" && $('#CiPageVal').val() != "0") {
                                        $("#ddlPageNo").val($('#CiPageVal').val()).change();
                                    } else {
                                        $setUpCitations = false;
                                    }
                                }  else {
                                    $("#CiVolume").val("Volume");
                                    $("#CiVolumeVal").val("");
                                    $("#ddlVolumns").trigger('change');
                                }
                            }
                        });
                    }
                });
                
            });
            $("#ddlVolumns").change(function() {
                var listvolume = "";
                var listpage = "";
                var YearIDs = $("#ddlYear").val();
                if ($("#ddlYear").val() == 'None')
                    YearIDs = 0;
                $("#CiVolume").val($("#ddlVolumns option:selected").text());
                $("#CiVolumeVal").val($("#ddlVolumns option:selected").val());
                $.ajax({
                    type: "POST",
                    url: "Research_SearchResult_S.aspx/GetYears",
                    data: "{'volumeId':" + ($("#ddlVolumns").val()) + ",'yearId':" + (YearIDs) + "}",
                    contentType: "application/json; charset=utf-8",
                    global: false,
                    async: false,
                    dataType: "json",
                    success: function (response) {
                        if (response.d == "") {
                            response.d = "[]";
                        }
                        var data = eval('(' + response.d + ')');
                        listpage = "<option value='0'>Page No.</option>";
                        for (var i = 0; i < data.length; i++) {
                            listpage += "<option value='" + data[i].intPageNo + "'>" + data[i].intPageNumber + "</option>";
                        }
                        $("#ddlPageNo").html(listpage);
                        if ($setUpCitations) {
                            if ($('#CiPageVal').val() != "" && $('#CiPageVal').val() != "0") {
                                $("#ddlPageNo").val($('#CiPageVal').val()).change();
                            } else {
                                $setUpCitations = false;
                            }
                        } else {
                            $("#CiPage").val("Page No."); 
                            $("#CiPageVal").val("");
                            $("#ddlPageNo").trigger('change');
                        }
                    }
                });
            });
            $("#ddlPageNo").change(function() {
                $("#CiPage").val($("#ddlPageNo option:selected").text());
                $("#CiPageVal").val($("#ddlPageNo option:selected").val());
                $setUpCitations = false;

            });
        });
                $(document).ready(function () {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function() {
                //if ($('#ddlSelect').val() == 'Select Search Type') {
                //    $('#txtResearch').val('');
                //    $('#txtResearch').attr("placeholder", "Please select 'Search Type' to start searching");
                //    $('#divCitationSearch').css('display', 'none');
                //    $('#divFreeSkorlTargetSearch').css('display', 'none');
                //    $('#pAdvSearch').css('display', 'none');
                //}
                //if ($('#ddlSelect').val() == 'C') {
                //    $('#divMainSearch').css('display', 'none');
                //    $('#divFreeSkorlTargetSearch').css('display', 'none');
                //    $('#divCitationSearch').css('display', 'block');
                //}
                //if ($('#ddlSelect').val() == 'T') {
                //    $('#divMainSearch').css('display', 'none');
                //    $('#divCitationSearch').css('display', 'none');
                //    $('#lbljudgename').css('display', 'none');
                //    $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
                //    $('#imgBtntarget').css('display', 'block');
                //}
                //if ($('#ddlSelect').val() == 'S') {
                //    $('#divCitationSearch').css('display', 'none');
                //}
                //$("#ddlSelect").change(function() {
                //    if ($('#ddlSelect').val() == 'Select Search Type') {
                //        $('#txtResearch').val('');
                //        $('#txtResearch').attr("placeholder", "Please select 'Search Type' to start searching");
                //        $('#divCitationSearch').css('display', 'none');
                //        $('#divFreeSkorlTargetSearch').css('display', 'none');
                //        $('#pAdvSearch').css('display', 'none');
                //    } else if ($('#ddlSelect').val() == 'C') {
                //        $('#hdnEnterKeyword').val('');
                //        $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
                //        $('#imgBtntarget').css('display', 'none');
                //        $('#txtResearch').val('');
                //        $('#txtResearch').attr("placeholder", "Enter Keywords");
                //        $('#divCitationSearch').css('display', 'block');
                //        $('#divFreeSkorlTargetSearch').css('display', 'none');
                //        $('#pAdvSearch').css('display', 'none');
                //        $('#divMainSearch').css('display', 'none');
                //        $('#txtJudgesMember').val('').trigger('chosen:updated');
                //        $('#hdnMembId').val('');
                //        $('#hdnMembIdText').val('');
                //        $('#hdnsubject').val('');
                //        $('#hdnsubjectText').val('');
                //        $('#txtSubjectList').val('').trigger('chosen:updated');
                //        $("#ddlYearFT option:first").attr("selected", true);
                //        $("#ddlCourtFT option:first").attr("selected", true);
                //        $("#ddlYear option:first").attr("selected", true);
                //        $("#ddlCourt option:first").attr("selected", true);
                //        $("#ddlReporterName option:first").attr("selected", true);
                //        $("#ddlVolumns option:first").attr("selected", true);
                //        $("#ddlPageNo option:first").attr("selected", true);
                //    } else if ($('#ddlSelect').val() == 'F') {
                //        $('#hdnEnterKeyword').val('');
                //        $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
                //        $("#ddlCourt").prop("disabled", true)
                //        $("#ddlReporterName").prop("disabled", true)
                //        $("#ddlVolumns").prop("disabled", true)
                //        $("#ddlPageNo").prop("disabled", true)
                //        $('#imgBtntarget').css('display', 'none');
                //        $('#divFreeSkorlTargetSearch').css('display', 'none');
                //        $('#txtResearch').css('display', 'block');
                //        $('#btnSearch').css('display', 'block');
                //        //$('#btnSearch').css('margin-top', '-35px');
                //        $('#txtResearch').val('');
                //        $('#txtResearch').attr("placeholder", "Enter Keywords");
                //        $('#divCitationSearch').css('display', 'none');
                //        $('#pAdvSearch').css('display', 'block');
                //        $('#divMainSearch').css('display', 'block');
                //        $('#txtJudgesMember').val('').trigger('chosen:updated');
                //        $('#hdnMembId').val('');
                //        $('#hdnMembIdText').val('');
                //        $('#hdnsubject').val('');
                //        $('#hdnsubjectText').val('');
                //        $('#txtSubjectList').val('').trigger('chosen:updated');
                //        $("#ddlYearFT option:first").attr("selected", true);
                //        $("#ddlCourtFT option:first").attr("selected", true);
                //        $('#lbljudgename').css('display', 'block');
                //        $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
                //        $("#ddlYear option:first").attr("selected", true);
                //        $("#ddlCourt option:first").attr("selected", true);
                //        $("#ddlReporterName option:first").attr("selected", true);
                //        $("#ddlVolumns option:first").attr("selected", true);
                //        $("#ddlPageNo option:first").attr("selected", true);
                //        $('#anchorAdvnceSearch').text('Advanced Search');
                //    } else if ($('#ddlSelect').val() == 'S') {
                //        $('#hdnEnterKeyword').val('');
                //        $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
                //        $("#ddlCourt").prop("disabled", true)
                //        $("#ddlReporterName").prop("disabled", true)
                //        $("#ddlVolumns").prop("disabled", true)
                //        $("#ddlPageNo").prop("disabled", true)
                //        $('#imgBtntarget').css('display', 'none');
                //        $('#divFreeSkorlTargetSearch').css('display', 'none');
                //        $('#txtResearch').val('');
                //        $('#txtResearch').attr("placeholder", "Enter Keywords");
                //        $('#divCitationSearch').css('display', 'none');
                //        $('#pAdvSearch').css('display', 'block');
                //        $('#divMainSearch').css('display', 'block');
                //        $('#txtJudgesMember').val('').trigger('chosen:updated');
                //        $('#hdnMembId').val('');
                //        $('#hdnMembIdText').val('');
                //        $('#hdnsubject').val('');
                //        $('#hdnsubjectText').val('');
                //        $('#txtSubjectList').val('').trigger('chosen:updated');
                //        $("#ddlYearFT option:first").attr("selected", true);
                //        $("#ddlCourtFT option:first").attr("selected", true);
                //        $('#lbljudgename').css('display', 'block');
                //        $('#lbljudgename').css('margin-left', '7px');
                //        $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
                //        $("#ddlYear option:first").attr("selected", true);
                //        $("#ddlCourt option:first").attr("selected", true);
                //        $("#ddlReporterName option:first").attr("selected", true);
                //        $("#ddlVolumns option:first").attr("selected", true);
                //        $("#ddlPageNo option:first").attr("selected", true);
                //        $('#anchorAdvnceSearch').text('Advanced Search');
                //    } else if ($('#ddlSelect').val() == 'T') {
                //        $('#hdnEnterKeyword').val('');
                //        $('#lblEnterKeyword').text('Entered Keyword or Search Attributes');
                //        $("#ddlCourt").prop("disabled", true)
                //        $("#ddlReporterName").prop("disabled", true)
                //        $("#ddlVolumns").prop("disabled", true)
                //        $("#ddlPageNo").prop("disabled", true)
                //        $('#imgBtntarget').css('display', 'block');
                //        $('#divFreeSkorlTargetSearch').css('display', 'block');
                //        $('#txtResearch').val('');
                //        $('#txtResearch').attr("placeholder", "Enter Keywords");
                //        $('#divCitationSearch').css('display', 'none');
                //        $('#pAdvSearch').css('display', 'block');
                //        $('#divMainSearch').css('display', 'none');
                //        $('#hdnsubject').val('');
                //        $('#hdnsubjectText').val('');
                //        $('#txtSubjectList').val('').trigger('chosen:updated');
                //        $('#txtJudgesMember').val('').trigger('chosen:updated');
                //        $('#hdnMembId').val('');
                //        $('#hdnMembIdText').val('');
                //        $("#ddlYearFT option:first").attr("selected", true);
                //        $("#ddlCourtFT option:first").attr("selected", true);
                //        $('#lbljudgename').css('display', 'none');
                //        $('#divFreeSkorlTargetSearch').css('margin-top', '0px');
                //        $("#ddlYear option:first").attr("selected", true);
                //        $("#ddlCourt option:first").attr("selected", true);
                //        $("#ddlReporterName option:first").attr("selected", true);
                //        $("#ddlVolumns option:first").attr("selected", true);
                //        $("#ddlPageNo option:first").attr("selected", true);
                //    }
                //});

            });
            });
        </script>
        <script type="text/javascript">
            function DefaultCall() {
                //$('#divCitationSearch').css('display', 'none');
                //$('#divFreeSkorlTargetSearch').css('display', 'none');
            }

            function citationCall() {
                //$('#imgBtntarget').css('display', 'none');
                //$('#txtResearch').val('');
                //$('#txtResearch').attr("placeholder", "Enter Keywords");
                //$('#divCitationSearch').css('display', 'block');
                //$('#divFreeSkorlTargetSearch').css('display', 'none');
                //$('#pAdvSearch').css('display', 'none');
                //$('#divMainSearch').css('display', 'none');
                //$('#txtJudgesMember').val('').trigger('chosen:updated');
                //$('#hdnMembId').val('');
                //$('#hdnMembIdText').val('');
                //$('#hdnsubject').val('');
                //$('#hdnsubjectText').val('');
                //$('#txtSubjectList').val('').trigger('chosen:updated');
                //$("#ddlYearFT option:first").attr("selected", true);
                //$("#ddlCourtFT option:first").attr("selected", true);
            }

            function freeTextCall() {
                //$('#imgBtntarget').css('display', 'none');
                //$('#divFreeSkorlTargetSearch').css('display', 'none');
                //$('#txtResearch').val('');
                //$('#txtResearch').attr("placeholder", "Enter Keywords");
                //$('#divCitationSearch').css('display', 'none');
                //$('#pAdvSearch').css('display', 'block');
                //$('#divMainSearch').css('display', 'block');
                //$('#txtJudgesMember').val('').trigger('chosen:updated');
                //$('#hdnMembId').val('');
                //$('#hdnMembIdText').val('');
                //$('#hdnsubject').val('');
                //$('#hdnsubjectText').val('');
                //$('#txtSubjectList').val('').trigger('chosen:updated');
                //$("#ddlYearFT option:first").attr("selected", true);
                //$("#ddlCourtFT option:first").attr("selected", true);
                //$('#lbljudgename').css('display', 'block');
                //$('#divFreeSkorlTargetSearch').css('margin-top', '0px');
                //$("#ddlYear option:first").attr("selected", true);
                //$("#ddlCourt option:first").attr("selected", true);
                //$("#ddlReporterName option:first").attr("selected", true);
                //$("#ddlVolumns option:first").attr("selected", true);
                //$("#ddlPageNo option:first").attr("selected", true);
                //$('#anchorAdvnceSearch').text('Advanced Search');
            }

            function SkorkelCall() {
                //$('#imgBtntarget').css('display', 'none');
                //$('#divFreeSkorlTargetSearch').css('display', 'none');
                //$('#txtResearch').val('');
                //$('#txtResearch').attr("placeholder", "Enter Keywords");
                //$('#divCitationSearch').css('display', 'none');
                //$('#pAdvSearch').css('display', 'block');
                //$('#divMainSearch').css('display', 'block');
                //$('#txtJudgesMember').val('').trigger('chosen:updated');
                //$('#hdnMembId').val('');
                //$('#hdnMembIdText').val('');
                //$('#hdnsubject').val('');
                //$('#hdnsubjectText').val('');
                //$('#txtSubjectList').val('').trigger('chosen:updated');
                //$("#ddlYearFT option:first").attr("selected", true);
                //$("#ddlCourtFT option:first").attr("selected", true);
                //$('#lbljudgename').css('display', 'block');
                //$('#divFreeSkorlTargetSearch').css('margin-top', '0px');
                //$("#ddlYear option:first").attr("selected", true);
                //$("#ddlCourt option:first").attr("selected", true);
                //$("#ddlReporterName option:first").attr("selected", true);
                //$("#ddlVolumns option:first").attr("selected", true);
                //$("#ddlPageNo option:first").attr("selected", true);
                //$('#anchorAdvnceSearch').text('Advanced Search');
            }

            function targetCall() {
                //$('#imgBtntarget').css('display', 'block');
                //$('#divFreeSkorlTargetSearch').css('display', 'block');
                //$('#txtResearch').val('');
                //$('#txtResearch').attr("placeholder", "Enter Keywords");
                //$('#divCitationSearch').css('display', 'none');
                //$('#pAdvSearch').css('display', 'block');
                //$('#divMainSearch').css('display', 'none');
                //$('#lbljudgename').css('display', 'none');
                //$("#ddlYear option:first").attr("selected", true);
                //$("#ddlCourt option:first").attr("selected", true);
                //$("#ddlReporterName option:first").attr("selected", true);
                //$("#ddlVolumns option:first").attr("selected", true);
                //$("#ddlPageNo option:first").attr("selected", true);
            }
        </script>
        <script type="text/javascript">
            ddsmoothmenu.init({
                mainmenuid: "smoothmenu1", //menu DIV id
                orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
                className: 'ddsmoothmenu', //class added to menu's outer DIV
                //customtheme: ["#1c5a80", "#18374a"],
                contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
            })
        </script>
        <script type="text/javascript">
            function getAllMemberValue(ctrlId) {
                $('#divFreeSkorlTargetSearch').find('label.error').remove();
                var control = document.getElementById(ctrlId);
                var strSelTexts = '';
                var strTexts = '';
                var cnt = 0;
                for (var i = 0; i < control.length; i++) {
                    if (control.options[i].selected) {
                        if (cnt == 0) {
                            strSelTexts += control.options[i].value;
                            strTexts += control.options[i].text;
                        } else {
                            strSelTexts += ',' + control.options[i].value;
                            strTexts += ',' + control.options[i].text;
                        }
                        cnt++;
                    }
                }
                $('#hdnMembId').val(strSelTexts);
                $('#hdnMembIdText').val(strTexts);
            }

            function getAllSubjectValue(ctrlId) {
                $('#MicroTag').find('label.error').remove();
                var control = document.getElementById(ctrlId);
                var strSelTexts = '';
                var strTexts = '';
                var cnt = 0;
                for (var i = 0; i < control.length; i++) {
                    if (control.options[i].selected) {
                        if (cnt == 0) {
                            strSelTexts += control.options[i].value;
                            strTexts += control.options[i].text;
                        } else {
                            strSelTexts += ',' + control.options[i].value;
                            strTexts += ',' + control.options[i].text;
                        }
                        cnt++;
                    }
                }
                $('#hdnsubject').val(strSelTexts);
                $('#hdnsubjectText').val(strTexts);
            }

            function ClearAllFields() {
                //$('#imgBtntarget').css('display', 'none');
                //$('#divFreeSkorlTargetSearch').css('display', 'none');
                //$('#txtResearch').val('');
                //$('#txtResearch').attr("placeholder", "Enter Keywords");
                //$('#divCitationSearch').css('display', 'none');
                //$('#pAdvSearch').css('display', 'block');
                //$('#divMainSearch').css('display', 'block');
                //$('#txtJudgesMember').val('').trigger('chosen:updated');
                //$('#hdnMembId').val('');
                //$('#hdnMembIdText').val('');
                //$('#hdnsubject').val('');
                //$('#hdnsubjectText').val('');
                //$('#txtSubjectList').val('').trigger('chosen:updated');
                //$("#ddlYearFT option:first").attr("selected", true);
                //$("#ddlCourtFT option:first").attr("selected", true);
                //$('#lbljudgename').css('display', 'none');
                //$('#divFreeSkorlTargetSearch').css('margin-top', '0px');
                //$("#ddlYear option:first").attr("selected", true);
                //$("#ddlCourt option:first").attr("selected", true);
                //$("#ddlReporterName option:first").attr("selected", true);
                //$("#ddlVolumns option:first").attr("selected", true);
                //$("#ddlPageNo option:first").attr("selected", true);
                //$('#anchorAdvnceSearch').text('Advanced Search');
            }
        </script>
        <script type="text/javascript">
            var config = {
                '.chosen-select': {},
                '.chosen-select-deselect': { allow_single_deselect: true },
                '.chosen-select-no-single': { disable_search_threshold: 10 },
                '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                '.chosen-select-width': { width: "95%" }
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);
            }
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function() {
                var config = {
                    '.chosen-select': {},
                    '.chosen-select-deselect': { allow_single_deselect: true },
                    '.chosen-select-no-single': { disable_search_threshold: 10 },
                    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                    '.chosen-select-width': { width: "95%" }
                }
                for (var selector in config) {
                    $(selector).chosen(config[selector]);
                }
            });
        </script>
        <script type="text/javascript">
            function MessClose() {
                document.getElementById("divSavesearch").style.display = 'none';
            }
            
            function callDisable() {
                $("*").attr("disabled", "disabled");
            }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {

            //$(document.body).on('touchmove', onScroll); // for mobile
            //$(window).on('scroll', onScroll); 
            $(window).scroll(function () {
                var isProcessed = false;
                if ($(window).scrollTop() + window.innerHeight >= document.body.scrollHeight && !isProcessed) {
                //if ($(window).scrollTop() == $(document).height() - $(window).height() && !isProcessed) {
                    //console.log('isProcess --------->>>>>>>>', isProcessed);
                    isProcessed = true;
                    setTimeout(function () {
                        isProcessed = false;
                    }, 2000);
                    //if ($(document).height() <= $(window).scrollTop() + $(document).height()) {
                    document.getElementById('imgLoadMore').style.display = 'block';
                    var v = $("#lblNoMoreRslt").text();
                    var maxCount = $("#hdnMaxcount").val();
                    if (maxCount <= 10) {
                        $("#lblNoMoreRslt").css("display", "none");
                    } else {
                        if (v != "No more results available...") {
                            var elm = '#imgLoadMore1';
                            $(elm).click();
                        } else {
                            document.getElementById('imgLoadMore').style.display = 'none';
                        }
                    }
                }
                if (maxCount == '') {
                    document.getElementById('imgLoadMore').style.display = 'none';
                }
            });
            //var prm = Sys.WebForms.PageRequestManager.getInstance();
            //prm.add_endRequest(function() {
            //    $(window).scroll(function () {
            //        if ($(document).height() <= $(window).scrollTop() + $(window).height()) {
            //            document.getElementById('imgLoadMore').style.display = 'block';
            //            var v = $("#lblNoMoreRslt").text();
            //            var maxCount = $("#hdnMaxcount").val();
            //            if (maxCount <= 10) {
            //                $("#lblNoMoreRslt").css("display", "none");
            //            } else {
            //                if (v != "No more results available...") {
            //                    var elm = '#imgLoadMore1';
            //                    $(elm).click();
            //                } else {
            //                    document.getElementById('imgLoadMore').style.display = 'none';
            //                }
            //            }
            //        }
            //        if (maxCount == '') {
            //            document.getElementById('imgLoadMore').style.display = 'none';
            //        }
            //    });
            //});
        });
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
           
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function() {
                ddsmoothmenu.init({
                    mainmenuid: "smoothmenu1", //menu DIV id
                    orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
                    className: 'ddsmoothmenu', //class added to menu's outer DIV
                    contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
                })
                var ir = 0;
                $(".endronsecssimg").mouseover(function() {
                    $(this).parent().children(".endronsecss").css('display', 'block');
                });

                $(".endronsecss").mouseout(function() {
                    $(this).parent().children(".endronsecss").css('display', 'none');
                });
                $(".scroll").click(function(event) {
                    $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 500);
                });

                $(".showMouseOver").mouseover(function() {
                    $(this).parent().children(".imageRolloverBg").css('display', 'block');
                });
                $(".showMouseOver").mouseout(function() {
                    $(this).parent().children(".imageRolloverBg").css('display', 'none');
                });

                $(".photoIcon").mouseover(function() {
                    $("#imgCamera").css('display', 'block');
                });
                $(".photoIcon").mouseout(function() {
                    $("#imgCamera").css('display', 'none');
                });
                $("#FileUplogo").change(function(event) {
                    var tmppath = URL.createObjectURL(event.target.files[0]);
                    var ext = $('#FileUplogo').val().split('.').pop().toLowerCase();
                    if (ext != '') {
                        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp']) == -1) {
                            if (ext == 'pdf' || ext == 'xlxs' || ext == 'txt' || ext == 'doc' || ext == 'docx' || ext == 'xlx' || ext == 'odt') {
                                alert('Please select image or video.');
                            } else {
                                $("#Mediavideo").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
                                $("#imgselect").css("display", "none");
                                $("#imgBtnDelete").css("display", "block");
                            }
                        } else {
                            $("#imgselect").fadeIn("fast").attr('src', URL.createObjectURL(event.target.files[0]));
                            $("#Mediavideo").css("display", "none");
                            $("#imgBtnDelete").css("display", "block");
                        }
                    } else {
                        $("#imgselect").css("display", "none");
                        $("#Mediavideo").css("display", "none");
                        $("#imgBtnDelete").css("display", "none");
                    }
                });
                $("#uploadDoc").change(function(event) {
                    var tmppath = URL.createObjectURL(event.target.files[0]);
                    $("#lblfilenamee").text($("#uploadDoc").val().split('\\').pop());
                });
                $("#imgGroup").click(function() {
                    $("#divfrdgrp").removeClass("fGroupBox frd").addClass("fGroupBox grp");
                    $("#divgroupSection").show();
                    $("#divFriendSection").hide();
                    return false;
                });
                $("#imgFriend").click(function() {
                    $("#divfrdgrp").removeClass("fGroupBox grp").addClass("fGroupBox frd");
                    $("#divFriendSection").show();
                    $("#divgroupSection").hide();
                    return false;
                });
            });
        });
        </script>
        <script type="text/javascript">

            function setJudges() {
                //debugger;
                var judgeInts = [];
                var judgeNames = [];
                if ($('#selectControl1_chosen').find('.chosen-results').find('.result-selected').length > 0) {
                    $('#selectControl1_chosen').find('.chosen-results').find('.result-selected').map(function () {
                        judgeNames.push(this.innerText);
                        judgeInts.push($(this).attr('data-option-array-index'));
                    });
                }
                $('#hdnMembId').val(judgeInts.join(','));
                $('#hdnMembIdText').val(judgeNames.join(','));
            }

            function btnSearchClickFn() {      
                $('#txtResearch').blur();
                setJudges();
                $("#divPopp").show();
                if ($('#ddlSelect').val() == "F") {
                    if ($('#txtResearch').val() == "") {
                        $('#dvtxtInputError').show();
                        hideLoader1();
                        return false;
                    }
                } else if ($('#ddlSelect').val() == "T") {
                    if ($('#lbljudgename').parent().find('#selectControl1').val() == null &&
                        $('#lblcontext').parent().find('#selectControl').val() == null &&
                        $("#ddlYearFT").val() == $("#ddlYearFT option:first").val() &&
                        $("#ddlCourtFT").val() == $("#ddlCourtFT option:first").val()) {
                        return false;
                    }
                }
                showLoader1();
                return true;
            }

            $(document).ready(function() {
                $('#btnSearch').click(btnSearchClickFn);
                $('#ImageButton1').click(function () {
                    $("#divPopp").show();
                });
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function() {
                    $('#btnSearch').click(btnSearchClickFn);
                    $('#ImageButton1').click(function() {
                        $("#divPopp").show();
                    });
                });
            });

            function functionCall() {
                $(".divPopp").show();
            }

            function functionHideCall() {
                $("#divPopp").hide();
            }
        </script>
        <script type="text/javascript">

        var config = {
            '.chosen-select': {},
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "95%" }
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

            var config = {
                '.chosen-select': {},
                '.chosen-select-deselect': { allow_single_deselect: true },
                '.chosen-select-no-single': { disable_search_threshold: 10 },
                '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                '.chosen-select-width': { width: "95%" }
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);
            }
        });
        </script>
        <script type="text/javascript">
        function getAllMemberValue(ctrlId) {
            $('#divFreeSkorlTargetSearch').find('label.error').remove();
            var control = document.getElementById(ctrlId);
            var strSelTexts = '';
            var strTexts = '';
            var cnt = 0;
            for (var i = 0; i < control.length; i++) {
                if (control.options[i].selected) {
                    if (cnt == 0) {
                        strSelTexts += control.options[i].value;
                        strTexts += control.options[i].text;
                    } else {
                        strSelTexts += ',' + control.options[i].value;
                        strTexts += ',' + control.options[i].text;
                    }
                    cnt++;
                }
            }
            $('#hdnMembId').val(strSelTexts);
            $('#hdnMembIdText').val(strTexts);
        }

        function getAllSubjectValue(ctrlId) {
            $('#MicroTag').find('label.error').remove();
            var control = document.getElementById(ctrlId);
            var strSelTexts = '';
            var strTexts = '';
            var cnt = 0;
            for (var i = 0; i < control.length; i++) {
                if (control.options[i].selected) {
                    if (cnt == 0) {
                        strSelTexts += control.options[i].value;
                        strTexts += control.options[i].text;
                    } else {
                        strSelTexts += ',' + control.options[i].value;
                        strTexts += ',' + control.options[i].text;
                    }
                    cnt++;
                }
            }
            $('#hdnsubject').val(strSelTexts);
            $('#hdnsubjectText').val(strTexts);
        }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#btnJudges').click()
            var ID = "#" + $("#hdnTabId").val();
            $(ID).focus();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function() {
                var ID = "#" + $("#hdnTabId").val();
                $(ID).focus();
            });
        });

        function ShowLoading(id) {
            location.href = '#' + id;
        }

        function btnSaveSearch_onclick() {
           // debugger;
            document.getElementById("divSavesearch").style.display = 'block';
            $('#txtsaveTitle').val('');

         return false;
        }

        //  function show(select_item) {
	    //  if (ddlSelect == "Free Text") {
		//  divFreeTextSearchInput.style.visibility='visible';
		//	divFreeTextSearchInput.style.display='block';
		//	//Form.fileURL.focus();
		//} 
		//else if(ddlSelect == "Citation"){
		//	citation.style.visibility='visible';
        //  citation.style.display = 'block';
        //  divFreeTextSearchInput.style.visibility='hidden';
		//	divFreeTextSearchInput.style.display='none';
		//}
        //}

        //(radio_value == 'F') {
        //            $('#divFreeTextSearchInput').show();
        //            $("#freetext").show();
        //            $("#citation").hide();
        //        } else if (radio_value == 'C') {
        //            $("#freetext").hide();
        //            $("#citation").show();
        //        } else if (radio_value == 'S') {
        //            $('#divFreeTextSearchInput').show();
        //            $("#freetext").show();
        //            $("#citation").hide();               
        //        } else if (radio_value == 'T') {
        //            $('#divFreeTextSearchInput').hide();
        //            $("#freetext").show();
        //            $("#citation").hide();
        //        }
        </script>
        <script type="text/javascript">

        function txtResearchRemoveError() {
            if ($("#txtResearch").val() != "") {
                $('#dvtxtInputError').hide();
            }
        }
       
        function txtResearchkeydown(event) {                
            if (event.keyCode == 13) {                
                window.searchEnter = true;
                $('#ctl00_ContentPlaceHolder1_hdnAvoid').val("true");
                $('#btnSearch').click();
                return false;
            } else {
                window.searchEnter = false;
            }            
        }
      
        function HidedivFT() {
            $('#anchorAdvnceSearch').text('Advanced Search');
            $('#divFreeSkorlTargetSearch').css('display', 'none');
        }

        function CallSearchSave() {
            //debugger;
            var txt =$("#<%=txtsaveTitle.ClientID %>");
            var btn = $("#<%=lnkPopupOK.ClientID %>");
            if (txt.val() == "") {
                btn.addClass('disabled');
            } else {
                 btn.removeClass('disabled');
            }       
        }
        </script>
        <%--<script type="text/javascript">
        $(function() {
            $("#lblEnterKeyword").mouseover(function() {
                var table;
                if ($('#ddlSelect').val() == 'F') {
                    table = '<div> <label id="lblTitle">' + $('#hdnEnterKeyword').val() + ' </label></div>';
                } else if ($('#ddlSelect').val() == 'C') {
                    table = '<div> <label id="lblTitle">' + $('#hdnEnterKeyword').val() + ' </label></div>';
                } else if ($('#ddlSelect').val() == 'S') {
                    table = '<div> <label id="lblTitle">' + $('#hdnEnterKeyword').val() + ' </label></div>';
                } else if ($('#ddlSelect').val() == 'T') {
                    table = '<div> <label id="lblTitle">' + $('#hdnEnterKeyword').val() + ' </label></div>';
                }
                $('#eventPop').html(table);
                document.getElementById('eventPop').style.display = "block";
            });
            $('#lblEnterKeyword').mouseout(function() {
                document.getElementById('eventPop').style.display = "none";
            });
        });
        window.onmousemove = function(e) {
            $("#eventPop").offset({ right: e.pageX, top: e.pageY + 15 });
        }
        </script>--%>
        <script type="text/javascript">
            $setUpCitations = false;
              function headingNavi() {
                     window.location = "/Research-Case-Details.aspx?CTid=1&cId=" + $(this).children('#hdnDocId').val();
                }
                function tabArrowNavi() {
                     window.location = "/Research-Case-Details.aspx?CTid=1&cId=" + $(this).children('#hdnDocIdArrow').val();
                }
            //$(document).ready(function () {
              
            //$('.tabHeadingNew').click(function() {
            //    window.location = "Research-Case-Details.aspx?CTid=1&cId=" + $(this).children('#hdnDocId').val();
            //});
            //$('.tabArrow').click(function() {
            //    window.location = "Research-Case-Details.aspx?CTid=1&cId=" + $(this).children('#hdnDocIdArrow').val();
            //});

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function() {
                //$('.tabHeadingNew').click(function() {
                //    window.location = "Research-Case-Details.aspx?CTid=1&cId=" + $(this).children('#hdnDocId').val();
                //});
                //$('.tabArrow').click(function() {
                //    window.location = "Research-Case-Details.aspx?CTid=1&cId=" + $(this).children('#hdnDocIdArrow').val();
                //});
            });

                function clearCitation() {
                var listCourt = "<option value='0'>Court</option>";
                var listReport = "<option value='0'>Reporter Name</option>";
                var listvolume = "<option value='0'>Volume</option>";
                var listpage = "<option value='0'>Page No.</option>";
                $("#ddlCourt").html(listCourt);
                $("#ddlReporterName").html(listReport);
                $("#ddlVolumns").html(listvolume);
                $("#ddlPageNo").html(listpage);
                selectFirstOption("ddlYear");
                selectFirstOption("ddlCourt");
                selectFirstOption("ddlReporterName");
                selectFirstOption("ddlVolumns");
                selectFirstOption("ddlPageNo");
                $("#CiCourt").val("Court");
                $("#CiCourtVal").val("");
                $("#CiReport").val("Reporter Name");
                $("#CiReportVal").val("");
                $("#CiVolume").val("Volume");
                $("#CiVolumeVal").val("");
                $("#CiPage").val("Page No."); 
                $("#CiPageVal").val(""); 
                $("#CiYear").val("Year"); 
                $("#CiYearVal").val(""); 
                $setUpCitations = false;
            }
            function clearAllSearch() {
                clearFreeTextBox();
                clearCitation();
                clearAdvancedSearch();
                $('#AdvSearchExpanded').val('0');
                $('#hdnSearchSelect').val("");
            }
            function clearFreeTextBox() {
                $('#txtResearch').val('');
            }

            function selectFirstOption(id) {
                 $("#"+id).val($("#"+ id + " option:first").val());
            }

            function clearAdvancedSearch() {
                $('#lbljudgename').parent().find('#selectControl1').val('').trigger('chosen:updated');
                selectFirstOption("ddlYearFT");
                selectFirstOption("ddlCourtFT");
                $('#lblcontext').parent().find('#selectControl').val('').trigger('chosen:updated');
            }

            function checkAdvancedSearchOn(radio_value) {
                if (radio_value == 'T' || ($('#hdnEnterKeyword').val() != "" && $('#AdvSearchExpanded').val() == '1')) {
                    $('#aAdvancedSearch').hide();
                    $('#advancesearch').addClass('show');
                } else {
                    $('#aAdvancedSearch').show();
                    $('#advancesearch').removeClass('show');
                }
                
            }

            // Setup which radio button to click
            if ($('#hdnSearchSelect').val() != "") {
                $("input[name='bn'][value='"+ $('#hdnSearchSelect').val() +"']").click();
            } else {
                $("input[name='bn'][value='F']").click();
            }

            function setUpCitationDropdowns() {
                //debugger;
                //console.log("here");
                $setUpCitations = true;
                if ($('#CiYearVal').val() != "") {
                    $('#ddlYear').val($('#CiYearVal').val()).change();
                } else {
                    $setUpCitations = false;
                }
            }

            $("#ddlSelect").change(function () {
                $("input[name='bn'][value='" + $("#ddlSelect").val() + "']").click();
            });

            //Added by sumit
            $("input[name$='bn']").click(function() {
                var radio_value = $(this).val();
                $('#ddlSelect').val(radio_value);
                $('#hdnRadioOption').val(radio_value);
                if ($('#hdnEnterKeyword').val() == "" || radio_value != $('#hdnSearchSelect').val()) {
                    clearAllSearch();
                } else if (radio_value == 'C') {
                    setUpCitationDropdowns();
                }
                checkAdvancedSearchOn(radio_value);
                if (radio_value == 'F') {
                    $('#divFreeTextSearchInput').show();
                    $("#freetext").show();
                    $("#citation").hide();
                } else if (radio_value == 'C') {
                    $("#freetext").hide();
                    $("#citation").show();
                } else if (radio_value == 'S') {
                    $('#divFreeTextSearchInput').show();
                    $("#freetext").show();
                    $("#citation").hide();               
                } else if (radio_value == 'T') {
                    $('#divFreeTextSearchInput').hide();
                    $("#freetext").show();
                    $("#citation").hide();
                }
            });
            $('[name="bn"]:checked').trigger('click');

            $('.advsearch').click(function() {
                $(this).hide();
                $('#AdvSearchExpanded').val('1');
            });
            $("#globalLoader").hide();
        //});
        </script>
    </asp:Content>
