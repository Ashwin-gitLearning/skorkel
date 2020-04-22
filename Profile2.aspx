<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="Profile2.aspx.cs" Inherits="Profile2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="usc" TagName="UserControl_DragNDrop" Src="~/UserControl/DragNDrop.ascx" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
<%@ MasterType TypeName="Main" %>
<asp:Content ContentPlaceHolderID="headMain" runat="server" ID="contentHead">
    <script src="<%=ResolveUrl("js/jquery.1.12.4.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("js/croppie.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js")%>" type="text/javascript" language="javascript"></script>
    <script src="<%=ResolveUrl("js/bootstrap.min.js")%>" type="text/javascript"></script>
    <%--<script src="<%=ResolveUrl("js/phoneno-all-numeric-validation.js")%>" type="text/javascript"></script>--%>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <title>Skorkel</title>
  
    <link rel="stylesheet" href="https://cdn.linearicons.com/free/1.0.0/icon-font.min.css">
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/croppie.css" rel="stylesheet">
    <link href="css/bootstrap-reboot.css" rel="stylesheet">
    <link href="css/bootstrap-grid.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="css/font-icon.css" rel="stylesheet">
    <link href="css/font-icon.css" rel="stylesheet">
<!--     <link href="css/chosen.css" rel="stylesheet"> -->
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <asp:HiddenField ID="hdnUserID" runat="server" ClientIDMode="Static" Value="" />
    <div class="main-section-inner">
        <div class="modal fade show backgroundoverlay" id="docModal1" runat="server" clientidmode="static">
                                        <div class="modal-dialog modal-dialog-centered" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title" id="exampleModalLabel">Submited Verification Document</h5>
                                                    <asp:LinkButton CssClass="close" ID="lnkCross" runat="server" OnClick="lnkSubDocCancel_Click" >
                                                        <span aria-hidden="true">×</span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="modal-body text-left ">
                                                    
                                                        
                                                        <div class="form-group">
                                                            <label for="textarea">Upload Document</label>
                                                            <usc:UserControl_DragNDrop ID="ddUploaderSubmitDoc" ClientIDMode="Static" runat="server" />
                                                            <div class="grey-text font-size-12">Note: Only DOC/PDF Support, Max File Size 5MB</div>
                                                            <asp:Label Visible="false" ID="lblErrorUpload" ForeColor="Red" CssClass="RedErrormsg" Text="" runat="server"></asp:Label>
                                                        </div>

                                                        <div class="form-check p-0">
                                                            <div class="font-size-12 line-height-16 text-transform-normal custom-checkbox">
                                                                <asp:CheckBox ID="chkConfirm" CssClass="custom-checkbox-inline input-m-align" runat="server" Text="I affirm that this document is valid and all contents are authentic." Checked="true" ValidationGroup="article" />
                                                            </div>
                                                              <asp:Label Visible="false" ID="lblErrorConf" ForeColor="Red" CssClass="RedErrormsg display-inline pl-4" Text="" runat="server"></asp:Label>
                                                        </div>




                                                        <div class="submit-con">
                                                            <asp:LinkButton ID="lnkCancelSubDoc" runat="server" OnClick="lnkSubDocCancel_Click" CssClass="btn btn-outline-primary m-r-15 add-scroller">Cancel</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkSave" CssClass="btn btn-primary hide-body-scroll" ValidationGroup="article" OnClick="lnkSubDocSave_Click" runat="server">Submit</asp:LinkButton>
                                                        </div>
                                                            
                                                </div>

                                            </div>
                                        </div>
                                    </div>
        <asp:HiddenField ID="hdnActive" runat="server" />
        <!--                    <a href="#" class="back-link">&larr; Back to Q &amp; A</a>-->
        <div class="panel-cover clearfix">
            <div class="center-panel w-100">

                <div class="row  mb-4">
                <div class="col-lg-3 col-md-3 col-12 col-sm-6">
                    <div class="card user-details newCard">
                  <div class="global-form">
                     <div class="img-con padding-top-0">
                         <span class="img-cover">
                                     <img id="imgUser" runat="server" class="user-img" clientidmode="Static" />
                                  <asp:LinkButton ID="lnkImageEdit" CssClass="img-cropper-trigger hide-body-scroll" runat="server" ToolTip="Change Profile Image"
                              ClientIDMode="Static" OnClientClick="javascript:CallCameraload(); return false;">
                                      <span class="lnr lnr-camera"></span>
                                         </asp:LinkButton>
                                  </span>
                     </div>

   
                  </div>
                   <span class="user-name">
                            <asp:Label ID="lblUserProfName" runat="server" Text=""></asp:Label>
                            <asp:LinkButton ID="lnkEditProfile" OnClientClick="CallUserJSON(); return false;" Font-Underline="false" Visible="true" ToolTip="Edit" 
                                                                             Text="" class="font-size-12 edit-profile-text  pt-1 pb-2 hide-body-scroll" CausesValidation="false" runat="server"> <img  src="images/edit-pencil.svg" alt="">&nbsp; &nbsp; Edit Profile
                                                                         </asp:LinkButton>
                        </span>
                  <ul class="list-inline">
                     <li class="list-inline-item d-none">
                        <span class="endor-cover">
                           <span class="icon">
                           <img src="images/thumb-color.svg" alt=""></span>
                           <span class="endors-text">
                              <strong>
                                 <asp:LinkButton ID="lblEndorseCount" runat="server" CssClass="text_decoration_color" OnClick="lblEndorseCount_click">
                                 </asp:LinkButton>
                              </strong>
                              Endorsements
                           </span>
                        </span>
                     </li>
                     <li class="list-inline-item w-100" id="liscore" runat="server">
                        <span class="endor-cover">
                           <span class="icon">
                           <img src="images/shape-2.svg"></span> 
                           <span class="endors-text">
                              <h1>
                                 <asp:LinkButton ID="lblMessCount" OnClientClick="return false;" runat="server" CssClass="text_decoration_color un-anchor" Text=""></asp:LinkButton>
                              </h1>
                              Score
                           </span>
                           <a href="#" data-toggle="modal" onclick="$('#divScorePopup').show(); return false;" class="infoicon hide-body-scroll">
                               <img src="images/info.svg" alt="info"/>
                           </a>
                        </span>
                     </li>
                  </ul>
                   <!---Score Private Public-->

                  <div class="private-score">
                       <div class="custom-checkbox" id="liScorePrivate" runat="server">
                          <span class="custom-checkbox">
                            <%--<input id="private" type="checkbox" name="private">--%>
                              <asp:CheckBox ID="cbScorePrivate" AutoPostBack="true" OnCheckedChanged="cbScorePrivate_CheckedChanged" CssClass="custom-checkbox" ClientIDMode="Static" text="Make your score private" runat="server" />
                            <%--<label for="cbScorePrivate">Private Score</label>--%>
                          </span>
                      </div> 
                  </div>
                  <asp:LinkButton ID="lnkSubmitDocs" runat="server" OnClick="lnkSubDoc_Click" ClientIDMode="Static"  class="btn btn-outline-primary"> Submit Verified Documents</asp:LinkButton>
               </div>
                </div>
            <div class="col-lg-6 col-md-6 col-12 col-sm-6" id="divScoreCard" runat="server">
                <div class="row height-50 mb-15  mobile-remove-margin">
                    <div class="col-lg-6 col-md-6 col-6 col-sm-6">
                        <div class="card cardScNew card-new-top">
                              <div class="card-body">
                                  <div class="cardScNew-inner">
                                      <div class="icon">
                                          <img src="images/College_icon.svg" alt="college Rank">
                                      </div>
                                      <div class="cardScNew-inner-text">
                                        <h4 id="AScore" runat="server" clientidmode="static">0</h4>
                                          <span>College Score</span>
                                      </div>
                                  </div>
                              </div>
                        </div>
                    </div>
                       <div class="col-lg-6 col-md-6 col-6 col-sm-6">
                        <div class="card cardScNew card-new-top">
                              <div class="card-body">
                                  <div class="cardScNew-inner">
                                      <div class="icon">
                                          <img src="images/activity.svg" alt="college Rank">
                                      </div>
                                      <div class="cardScNew-inner-text">
                                        <h4 id="CScore" runat="server" clientidmode="static">0</h4>
                                          <span>Activity in College</span>
                                      </div>
                                  </div>
                              </div>
                        </div>
                    </div>
                </div>
                <div class="row height-50  mobile-remove-margin">
                    <div class="col-lg-6 col-md-6 col-6 col-sm-6">
                        <div class="card cardScNew card-new-bottom">
                              <div class="card-body">
                                  <div class="cardScNew-inner">
                                      <div class="icon">
                                          <img src="images/rank_in_college.svg" alt="college Rank">
                                      </div>
                                      <div class="cardScNew-inner-text">
                                        <h4 id="BScore" runat="server" clientidmode="static">0</h4>
                                          <span>Rank in College</span>
                                      </div>
                                  </div>
                              </div>
                        </div>
                    </div>
                       <div class="col-lg-6 col-md-6 col-6 col-sm-6">
                        <div class="card cardScNew card-new-bottom">
                              <div class="card-body">
                                  <div class="cardScNew-inner pr-0">
                                      <div class="icon d-flex align-items-center">
                                          <img src="images/Sk_lg.png" alt="college Rank">
                                      </div>
                                      <div class="cardScNew-inner-text  pr-0">
                                        <h4 id="DScore" runat="server" clientidmode="static">0</h4>
                                          <span>Skorkel Contribution</span>
                                      </div>
                                  </div>
                              </div>
                        </div>
                    </div>
                </div>
            </div>
              </div>
                <div class="">
                    <div class="custom-nav-con home-nav-tab">
                        
                        <!-- Nav tabs -->
                        <ul class="custom-nav-control nav nav-tabs ">
                             <!-- <li class="nav-item">
                                <asp:LinkButton runat="server" ID="lnkDocuments" class="nav-link" Text="Documents"
                                    OnClick="lnkDocuments_Click"></asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton runat="server" ID="lnkWorkex" class="nav-link" Text="Work Experience"
                                    OnClick="lnkWorkex_Click"></asp:LinkButton>
                            </li> -->
                            <li class="nav-item">
                                <asp:LinkButton runat="server" ID="lnkEducation" class="nav-link" Text="Education"
                                    OnClick="lnkEducation_Click"></asp:LinkButton>
                            </li>
                            <li class="nav-item">
                                <asp:LinkButton runat="server" ID="lnkAchievements" class="nav-link" Text="Achievements"
                                    OnClick="lnkAchievements_Click"></asp:LinkButton>
                            </li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content">
                           
                            <!---Documents-->
                            <asp:Panel ID="PnlDocuments" UpdateMode="Conditional" Visible="false" runat="server">
                                <div class="custom-nav-container tab-pane container ative show d-none" id="document">
                                    <div class="">
                                        <div class="btn-title-con">
                                            <div> <h5 class="card-title float-left">Existing Documents</h5></div>
                                            <div>
                                                <asp:LinkButton class="btn hide-body-scroll btn-outline-primary float-right display-none-m" ID="lnkuploadDoc" runat="server" ClientIDMode="Static" OnClientClick="CallUploadDoc();"
                                                                 Text="+  Add" OnClick="lnkuploadDoc_click"></asp:LinkButton>

                                                             </div>

                                                
                                           
                                        </div>
                                        <asp:UpdatePanel ID="Panel1" UpdateMode="Always" Visible="true" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <asp:HiddenField ID="hdnintdocIdelete" Value="" ClientIDMode="Static" runat="server" />
                                                    <asp:HiddenField ID="hdnstrdocDescriptiondele" Value="" ClientIDMode="Static" runat="server" />
                                                    <asp:HiddenField ID="hdnfilestrFilePathe" Value="" ClientIDMode="Static" runat="server" />
                                                    <asp:ListView ID="LstDocument" runat="server" GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder1"
                                                        OnItemCommand="LstDocument_ItemCommand" OnItemDataBound="LstDocument_ItemDataBound"
                                                        ItemPlaceholderID="itemPlaceHolder1">
                                                        <LayoutTemplate>
                                                            <table id="tblDoc" cellpadding="0" cellspacing="0" class="position_static">
                                                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <GroupTemplate>
                                                            <tr>
                                                                <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                            </tr>
                                                        </GroupTemplate>
                                                        <ItemTemplate>
                                                            <div class="col-sm-3 col-6 m-box">
                                                                <div class="doc-card">
                                                                    <span class="more-btn float-right" runat="server" id="editUserComment">
                                                                        <span class="dropdown">
                                                                            <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                <img src="images/more.svg" alt="" class="more-btn">
                                                                            </a>
                                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HiddenField ID="hdnDocId" Value='<%#Eval("intDocId") %>' runat="server" />
                                                                            <asp:HiddenField ID="hdnDocsSee" Value='<%#Eval("intDocsSee") %>' runat="server" />
                                                                            <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
                                                                            <asp:HiddenField ID="hdnMaxcount" Value='<%#Eval("Maxcount") %>' runat="server" ClientIDMode="Static" />
                                                                            <asp:HiddenField ID="hdnstrFilePath" Value='<%#Eval("strFilePath") %>' runat="server"
                                                                                ClientIDMode="Static" />
                                                                            <asp:HiddenField ID="hdnstrDocTitle" Value='<%#Eval("strDocTitle") %>' runat="server"
                                                                                ClientIDMode="Static" />
                                                                            <asp:HiddenField ID="hdnIsDocsDownload" Value='<%#Eval("IsDocsDownload") %>' runat="server"
                                                                                ClientIDMode="Static" />
                                                                            <span class="dropdown-menu" id="updoc" onclick="updocClick.bind(this)()" aria-labelledby="dropdownMenuLink" x-placement="bottom-start" style="position: absolute; transform: translate3d(0px, 17px, 0px); top: 0px; left: 0px; will-change: transform;">
                                                                                <asp:HiddenField ID="hdnintdocIdelet" Value='<%# Eval("intDocId") %>' ClientIDMode="Static"
                                                                                    runat="server" />
                                                                                <asp:HiddenField ID="hdnstrdocDescriptiondel" Value='<%# Eval("strDocTitle") %>'
                                                                                    ClientIDMode="Static" runat="server" />
                                                                                <asp:HiddenField ID="hdnfilestrFilePath" Value='<%# Eval("strFilePath") %>' ClientIDMode="Static"
                                                                                    runat="server" />
                                                                                 <asp:LinkButton ID="lnkEditDoc"  CommandName="EditDocs" runat="server" CssClass="dropdown-item" ><span class="lnr lnr-pencil"></span> Edit</asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkDeleteDoc" Visible="true" CssClass="dropdown-item" OnClientClick="javascript:docdelete();return false;"
                                                                                    ToolTip="Delete" Text="Delete" CommandName="DeleteDocs" CausesValidation="false"
                                                                                    runat="server"><span class="lnr lnr-trash"></span> Delete                                       
                                                                                </asp:LinkButton>
                                                                                <%--  <a class="dropdown-item" href="#"><span class="lnr lnr-trash"></span>Delete</a>--%>
                                                                            </span>
                                                                        </span>
                                                                    </span>
                                                                    <img src="images/document.png" alt="" class="doc-img" />
                                                                    <hr>
                                                                     <asp:Label ID="lblDocument"  class="doc-text truncate" runat="server" Text='<%#Eval("strDocTitle") %>' Style="display: none;"></asp:Label>
                                                                    <asp:HyperLink ID="lblDocument1" ClientIDMode="Static" class="doc-text truncate" Target="_blank" ToolTip="Download file" NavigateUrl='<%# "~/UploadDocument/"+Eval("strFilePath")%>'
                                                                        Text='<%#Eval("strDocTitle") %>' Font-Size="Small" runat="server"></asp:HyperLink>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                         <!-- Modal -->
                                            <asp:UpdatePanel ID="hg" runat="server" ><ContentTemplate>
                                            <div class="modal backgroundoverlay show" id="divDocumentUplaod" runat="server" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog-centered" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="exampleModalLabel">Add Document</h5>
                                                            <asp:LinkButton OnClick="lnkCancelDoc_Click" OnClientClick="javascript:callDoccancel();"   ID="closedo" CssClass="close" runat="server">
                                                                <span aria-hidden="true">&times;</span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="modal-body text-left ">

                                                            <div class="form-group">
                                                                <usc:UserControl_DragNDrop OnDelete="upldrFile_delete"  ID="ddUploader1" ClientIDMode="Static" runat="server" />
                                                                <asp:Label runat="server" Visible="false"  ForeColor="Red" CssClass="RedErrormsg" Text="" ID="lblErrorMsg"></asp:Label>
                                                                <!---<span class="grey-text mt-1 inline-block font-size-12">Note: Only PDF or DOC/DOCX file Support, Max File Size 5MB</span>-->
                                                            </div>

                                                            <div class="form-group">
                                                                <label for="textbox">Name of Document</label>
                                                                <asp:TextBox ID="txtDocTitle" runat="server" MaxLength="50" autocomplete="off" ClientIDMode="Static" class="form-control"
                                                                    ValidationGroup="docUpload" placeholder="Enter Document Name"></asp:TextBox>
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDocTitle"
                                                                    Display="Dynamic" ValidationGroup="docUpload" ErrorMessage="Please enter document name."
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="textarea">Description</label>
                                                                <textarea id="txtDescrition" rows="5" runat="server" maxlength="500" placeholder="Description" class="form-control"></textarea>
                                                            </div>
                                                            <div class="form-group remove-flex">
                                                                <label for="textbox">Context</label>
                                                                <uc:UserControl_MultiSelect class="form-control" ID="lstSubjCategory" ClientIDMode="Static" runat="server"  />
                                                            </div>


                                                            <div class="form-group">
                                                               <label for="textarea">Access</label><br />

                                                                <div class="form-check form-check-inline radio-itmes">
                                                                   
                                                                          <asp:CheckBox ID="imgPrivate" CssClass="custom-checkbox" Text="Make This Private" ClientIDMode="Static" 
                                                                                                runat="server" />
                                                                   
                                                                </div>
                                                                <div class="form-check form-check-inline radio-itmes">
                                                                   
                                                                          <asp:CheckBox ID="imgDownload" CssClass="custom-checkbox" Text="Downloadable" ClientIDMode="Static" 
                                                                                                runat="server" />
                                                                    
                                                                </div>
                                                            </div>
                                                    
                                         
                                                            <div class="submit-con">
                                                                <asp:LinkButton ID="lnkCancelDoc" CssClass="btn btn-outline-primary  add-scroller m-r-15" runat="server" Text="Cancel"
                                                                    ClientIDMode="Static" OnClick="lnkCancelDoc_Click" OnClientClick="javascript:callDoccancel();"></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkSave" CssClass="btn  btn-primary" runat="server" Text="Save"
                                                                    ValidationGroup="docUpload" OnClick="btnSave_Click" ClientIDMode="Static" OnClientClick="javascript:callDocSave();"></asp:LinkButton>

                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                                </ContentTemplate></asp:UpdatePanel>
                                    </div>
                                </div>
                            </asp:Panel>
                             <!---Documents ended -->
                    
                            <!---Work Experience -->
                            <asp:UpdatePanel ID="PnlWorkex" runat="server">
                                <ContentTemplate>
                                    <div class="custom-nav-container tab-pane container fade  active show" runat="server" id="PnlWorkex1">
                                        <div class="card-list-con blog-list">
                                            <div class="btn-title-con ">
                                                <div>
                                                    <h5 class="card-title float-left">Work Experience</h5>
                                                </div>
                                                <div>
                                                    <asp:LinkButton class="btn btn-outline-primary float-right" ID="aAddworkExp" runat="server" ClientIDMode="Static" OnClientClick="CallaAddworkExp();"
                                                        Text="+  Add" OnClick="aAddworkExp_click"></asp:LinkButton>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hdnintworkeIdelet" Value="" ClientIDMode="Static" runat="server" />
                                            <asp:HiddenField ID="hdnstrworkeDescriptiondel" Value="" ClientIDMode="Static" runat="server" />
                                            <asp:ListView runat="server" ID="lstWorkExperience" OnItemDataBound="lstWorkExperience_ItemDataBound"
                                                OnItemCommand="lstWorkExperience_ItemCommand" CellPadding="9">
                                                <ItemTemplate>
                                                    <div class="card top-list">
                                                        <div class="post-con">
                                                            <div class="post-body">
                                                                <asp:HiddenField ID="hdnintExperienceId" Value='<%#Eval("intExperienceId") %>' runat="server" />
                                                                <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
                                                                <span class="more-btn float-right" runat="server" id="divUserexperenceED">
                                                                    <span class="dropdown">
                                                                        <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                            <img src="images/more.svg" alt="" class="more-btn">
                                                                        </a>
                                                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span onclick="selectWork.bind(this)()" class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start"><asp:LinkButton ID="lnkEditDoc" Font-Underline="false" Visible="true" ToolTip="Edit" 
                                                                                Text="Edit" class="hide-body-scroll dropdown-item" CommandName="EditExp" CausesValidation="false" runat="server"> <span class="lnr lnr-pencil"></span> Edit
                                                                            </asp:LinkButton>
                                                                            <asp:HiddenField ID="hdnintworkIdelet" Value='<%# Eval("intExperienceId") %>' ClientIDMode="Static"
                                                                                runat="server" />
                                                                            <asp:HiddenField ID="hdnstrworkDescriptiondel" Value='<%# Eval("strCompanyName") %>'
                                                                                ClientIDMode="Static" runat="server" />
                                                                            <asp:LinkButton ID="lnkDeleteDoc" Font-Underline="false" Visible="true" ToolTip="Delete"
                                                                                Text="Delete" class="hide-body-scroll dropdown-item" CommandName="DeleteExp" CausesValidation="false" runat="server"
                                                                               OnClientClick="javascript:docdelete();return false;"> <span class="lnr lnr-trash"></span> Delete
                                                                            </asp:LinkButton>
                                                                        </span>
                                                                    </span>
                                                                </span>
                                                                <h3>
                                                                    <asp:Label ID="lblCompanyName" runat="server" Text='<%#Eval("strCompanyName") %>' />, 
                                                            <span class="normal-font">
                                                                <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("strDesignation") %>' />
                                                            </span>
                                                                </h3>
                                                                <span class="small-date">
                                                                    <asp:Label ID="lblinFromtMonth" runat="server" Text='<%#Eval("inFromtMonth") %>' />
                                                                    <asp:Label ID="lblFromYear" runat="server" Text='<%#Eval("intFromYear") %>' />
                                                                    -
                                                            <asp:Label ID="lblintToMonth" runat="server" Text='<%#Eval("intToMonth") %>' />
                                                                    <asp:Label ID="lblToYear" runat="server" Text='<%#Eval("intToYear") %>' />
                                                                </span>
                                                                <p>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("strDescription") %>' />
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <hr />
                                            <div class="skill-card m-t-20">
                                                <div class="btn-title-con">
                                                    <h5>Skills</h5>
                                                    <asp:LinkButton ID="lnkAddskill" runat="server" OnClick="lnkAddSkill_Click" CssClass="btn hide-body-scroll btn-outline-primary"
                                                        Text="Add/Edit" OnClientClick="CalllnkAddskill();"></asp:LinkButton>
                                                    <!-- Modal -->
                                                    <div class="modal backgroundoverlay show" id="divAddskill" runat="server" tabindex="-1" role="dialog" aria-labelledby="addSkillModal" aria-hidden="true">
                                                        <asp:UpdatePanel ID="updateskills" runat="server" class="modal-dialog modal-dialog-centered">
                                                            <ContentTemplate>
                                                                <div class="w-100" role="document">
                                                                    <div class="modal-content">
                                                                        <div class="modal-header">
                                                                            <h5 class="modal-title" id="exampleModalLabel">Add Skills</h5>
                                                                            <asp:LinkButton runat="server" OnClick="lnkCancelSkill_Click" OnClientClick="javascript:callCancelSkill();" ID="closeskil" CssClass="close" >
                                                                                <span aria-hidden="true">&times;</span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                        <div class="modal-body text-left ">

                                                                            <div class="">
                                                                                <div class="chip-cover">
                                                                                    <asp:ListView ID="lstAreaSkill" runat="server" OnItemCommand="lstAreaSkill_ItemCommand"
                                                                                        GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                                                                                        <GroupTemplate>
                                                                                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                                                        </GroupTemplate>
                                                                                        <ItemTemplate>
                                                                                        
                                                                                            <asp:HiddenField ID="hdnintUserSkillId" runat="server" Value='<%#Eval("intUserSkillId")%>' />
                                                                                            <div class="chip">
                                                                                                       <span class="sscount">0</span> 
                                                                                                <%#Eval("strSkillName")%>   <asp:LinkButton ID="lnkdDelete" CssClass="close-chip" runat="server" CommandName="DeleteExp">
                                                                 <span class="lnr lnr-cross"></span>
                                                               </asp:LinkButton> </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>
                                                                                </div>
                                                                            </div>
                                                                            <p>
                                                                                <asp:Label ID="lblareaskill" ClientIDMode="Static" runat="server" ForeColor="Red"></asp:Label>
                                                                            </p>
                                                                            <div class="add-skill d-flex">
                                                                                <asp:TextBox ID="txtAreaExpert" OnKeyPress="DisablePostBack();" runat="server" placeholder="Enter Skill" MaxLength="50"
                                                                                    ValidationGroup="skills" class="form-control m-r-25" ClientIDMode="Static"></asp:TextBox>
                                                                                <asp:LinkButton ID="btnAreaExpSave" runat="server" Text="Add" ClientIDMode="Static"
                                                                                    OnClientClick="javascript:CallAddSkill(); showLoader1();" ValidationGroup="skills" CssClass="btn btn-primary"
                                                                                    OnClick="btnAreaExpSave_Click"></asp:LinkButton>

                                                                            </div>
                                                                            <div class="submit-con">
                                                                                <asp:LinkButton ID="lnkCancelSkill" runat="server" Text="Cancel" ClientIDMode="Static"
                                                                                    CssClass="btn add-scroller btn-outline-primary m-r-15" OnClick="lnkCancelSkill_Click" OnClientClick="javascript:callCancelSkill();"></asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkSaveSkill" runat="server" Text="Save" ClientIDMode="Static"
                                                                                    CssClass="btn add-scroller btn-primary" OnClick="lnkSaveSkill_Click" OnClientClick="javascript:showLoader1();"></asp:LinkButton><%--OnClientClick="javascript:callSaveSkill();"--%>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <!-- Modal Ended -->
                                                </div>
                                                <div class="card">
                                                    <div id="divexistingSkils" runat="server" class="chip-cover padding-15">
                                                        <asp:ListView ID="lstAreaExpert" runat="server" OnItemCommand="lstAreaExpert_ItemCommand"   OnItemDataBound="lstAreaExpert_ItemDataBound"
                                                            GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                                                              <GroupTemplate>
                                                         <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                      </GroupTemplate>
                                                            <ItemTemplate>
                                                                 
                                                                <asp:HiddenField ID="hdnintUserSkillId" runat="server" Value='<%#Eval("intUserSkillId")%>' />
                                                                 <div class="chip" runat="server" id="divEndr">
                                                                    <asp:Label ID="lblEndronseCount" runat="server" class="" Text="0"></asp:Label>
                                                                       <asp:Label ID="lblstrSkillName" runat="server" class="ssField" Text='<%#Eval("strSkillName")%>'></asp:Label>
                                                                     
                                                                      <%--<img id="imgPlus" runat="server" src="images/plus.png" clientidmode="Static" visible="false"
                                                               class="showendorse" />--%>
<asp:LinkButton ID="lnkEndrose" ClientIDMode="Static"  CssClass="" runat="server" Visible="false" 
                                                               CommandName="EndronseSkill" Text="Endorse"> <span data-toggle="tooltip" data-placement="top" title="Endorse" class="endorse"><span>+</span></span></asp:LinkButton>
                                                                          
                                                                     </div>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Modal -->
                                            <div class="modal backgroundoverlay " id="AddWorkExp" tabindex="-1" role="dialog" runat="server" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog-centered" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="exampleModalLabel">Work Experience</h5>
                                                            <asp:LinkButton runat="server"  OnClick="lnlCancel_Click" OnClientClick="javascript:callCancelExp();" id="closeWrk" CssClass="close" >
                                                                <span aria-hidden="true">&times;</span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="modal-body text-left ">
                                                            <asp:UpdatePanel ID="upmain" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="form-group">
                                                                        <label for="textbox">Company Name</label>
                                                                        <asp:TextBox ID="txtInstituteName" autocomplete="off" runat="server" ValidationGroup="workExp" placeholder="Enter Company Name" class="form-control"
                                                                            ClientIDMode="Static"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInstituteName"
                                                                                Display="Dynamic"  ValidationGroup="workExp" ErrorMessage="Please enter company name." EnableClientScript="true"
                                                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                <div id="dvSourceLinkErrorText" class="error_message" runat="server" ClientIDMode="Static"></div>
                                                                            
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="textbox">Position</label>
                                                                        <asp:TextBox ID="txtDegree" autocomplete="off" ClientIDMode="Static" runat="server" placeholder="Enter Position"
                                                                            class="form-control"></asp:TextBox>
                                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDegree"
                                                                            Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please enter position."
                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:UpdatePanel ID="upda" runat="server">
                                                                        <ContentTemplate>
                                                                            <div class="form-group mb-0">
                                                                                <label for="textarea">Timeframe</label>
                                                                                <div class="row custom-select-con">
                                                                                    <div class="col-sm-6 col-sm-0 mb-3">
                                                                                        <div class="select-wrapper">
                                                                                        <select class="form-control" name="select" id="fromMonth" runat="server">
                                                                                            <option>Month</option>
                                                                                            <option>Jan</option>
                                                                                            <option>Feb</option>
                                                                                            <option>Mar</option>
                                                                                            <option>Apr</option>
                                                                                            <option>May</option>
                                                                                            <option>Jun</option>
                                                                                            <option>Jul</option>
                                                                                            <option>Aug</option>
                                                                                            <option>Sep</option>
                                                                                            <option>Oct</option>
                                                                                            <option>Nov</option>
                                                                                            <option>Dec</option>
                                                                                        </select>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="fromMonth"
                                                                                        InitialValue="Month" Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please select from month."
                                                                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <div class="col-sm-6">
                                                                                        <div class="select-wrapper">
                                                                                        <asp:DropDownList ID="txtFromYear" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFromYear"
                                                                                         Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please enter from year."
                                                                                         InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label for="textarea">To</label>
                                                                                <div class="row custom-select-con">
                                                                                    <div class="col-sm-6 col-sm-0 mb-1">
                                                                                        <div class="select-wrapper">
                                                                                        <asp:DropDownList ID="toMonth" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                                            <asp:ListItem Selected="True">Month</asp:ListItem>
                                                                                            <asp:ListItem>Jan</asp:ListItem>
                                                                                            <asp:ListItem>Feb</asp:ListItem>
                                                                                            <asp:ListItem>Mar</asp:ListItem>
                                                                                            <asp:ListItem>Apr</asp:ListItem>
                                                                                            <asp:ListItem>May</asp:ListItem>
                                                                                            <asp:ListItem>Jun</asp:ListItem>
                                                                                            <asp:ListItem>Jul</asp:ListItem>
                                                                                            <asp:ListItem>Aug</asp:ListItem>
                                                                                            <asp:ListItem>Sep</asp:ListItem>
                                                                                            <asp:ListItem>Oct</asp:ListItem>
                                                                                            <asp:ListItem>Nov</asp:ListItem>
                                                                                            <asp:ListItem>Dec</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="toMonth"
                                                                            InitialValue="Month" Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please select to month."
                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <div class="col-sm-6">
                                                                                        <div class="select-wrapper">
                                                                                        <asp:DropDownList ID="txtToYear" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtToYear"
                                                                            Display="Dynamic" ValidationGroup="workExp" ErrorMessage="Please enter to year."
                                                                            InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <asp:Label ID="lblWrkMessage" runat="server" visible ="false" class="pl-3" ForeColor="Red"></asp:Label>
                                                                                    <div class="col-sm-12">
                                                                                        <div class="mt-3  custom-checkbox">
                                                                                            <asp:CheckBox ID="chkAtPresent" CssClass="custom-checkbox" Text="Currently employer" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkAtPresent_CheckedChanged"
                                                                                                runat="server" />
                                                                                        
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="chkAtPresent" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                    <div class="form-group">
                                                                        <label for="textarea">Description</label>
                                                                        <textarea maxlength="500" class="form-control" rows="5" id="txtDescription" runat="server" placeholder="Description"></textarea>
                                                                    </div>
                                                                 
                                                              
                                                                     
                                                                    <div class="submit-con">
                                                                        <asp:LinkButton runat="server" ID="LinkButton1" Text="Cancel" CssClass="btn btn-outline-primary m-r-15 add-scroller"
                                                                            OnClick="lnlCancel_Click" ClientIDMode="Static" Visible="true" OnClientClick="javascript:callCancelExp();"></asp:LinkButton>
                                                                        <asp:LinkButton runat="server" ID="lnlSaveExp" ValidationGroup="workExp" Text="Save" CssClass="btn btn-primary"
                                                                            ClientIDMode="Static" OnClick="lnlSaveExp_Click" OnClientClick="return callSaveExp();"></asp:LinkButton>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>

                                               
                                            </div>
                                             <!-- Modal Ended -->
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="lnlSaveExp" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <!---Work Experience ended -->  
                                
                            <!--- Education panel -->
                            <asp:UpdatePanel ID="PnlEduction" runat="server">
                                <ContentTemplate>
                                    <div class="custom-nav-container tab-pane container fade active show" id="education">
                                        <div class="card-list-con blog-list">
                                            <div class="btn-title-con ">
                                                <div>
                                                    <h5 class="card-title float-left">Education</h5>
                                                </div>
                                                <div>
                                                    <asp:LinkButton runat="server" ID="lnlAddEducation" Text="+  Add" CssClass="changes-position btn hide-body-scroll btn-outline-primary float-right"
                                                        OnClick="lnlAddEducation_Click" OnClientClick="CalllnlAddEducation();"></asp:LinkButton>
                                                </div>
                                            </div>
                                            <!---List View-->
                                            <asp:HiddenField ID="hdnintedueIdelet" Value="" ClientIDMode="Static" runat="server" />
                                            <asp:HiddenField ID="hdnstredueDescriptiondel" Value="" ClientIDMode="Static" runat="server" />
                                            <asp:ListView runat="server" ID="lstEducation" OnItemDataBound="lstEducation_ItemDataBound"
                                                OnItemCommand="lstEducation_ItemCommand" CellPadding="9">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnintEducationId" Value='<%#Eval("intEducationId") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnToMonth" Value='<%#Eval("ToMonth") %>' runat="server" />
                                                    <div class="card top-list">
                                                        <div class="post-con">
                                                            <div class="post-body">
                                                                <span class="more-btn float-right" runat="server" id="divEducationED">
                                                                    <span class="dropdown">
                                                                        <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                            <img src="images/more.svg" alt="" class="more-btn">
                                                                        </a>
                                                                       <span id="updaEDU" class="dropdown-menu updaEDU" aria-labelledby="dropdownMenuLink" x-placement="bottom-start"><asp:HiddenField ID="hdninteduIdelet" Value='<%# Eval("intEducationId") %>' ClientIDMode="Static"
                                                                                runat="server" />
                                                                            <asp:HiddenField ID="hdnstreduDescriptiondel" Value='<%# Eval("strInstituteName") %>'
                                                                                ClientIDMode="Static" runat="server" />
                                                                            <asp:LinkButton ID="lnkEditDoc" Font-Underline="false" Visible="true" ToolTip="Edit"
                                                                                CommandName="EditEdu" class="hide-body-scroll dropdown-item" CausesValidation="false" runat="server"><span class="lnr lnr-pencil"></span> Edit              
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkDeleteDoc" Visible="true" OnClientClick="javascript:docdelete();return false;"
                                                                                ToolTip="Delete" class="hide-body-scroll dropdown-item" CommandName="DeleteAch" CausesValidation="false"
                                                                                runat="server"><span class="lnr lnr-trash"></span> Delete                                 
                                                                            </asp:LinkButton>
                                                                        </span>
                                                                    </span>
                                                                </span>
                                                                
                                                                    <h3>
                                                                        <asp:Label ID="lblInstituteName" runat="server" Text='<%#Eval("strInstituteName") %>' />, <span class="normal-font">
                                                                            <asp:Label ID="lblEducationAchievement" runat="server" Text='<%#Eval("strDegree") %>' /></span> </h3>
                                                           
                                                                
                                                                <span class="small-date">
                                                                    <asp:Label ID="lblFromMonth" runat="server" Text='<%#Eval("intMonth") %>' />
                                                                    <asp:Label ID="lblFromYear" runat="server" Text='<%#Eval("intYear") %>' />
                                                                    -
                    <asp:Label ID="lblintToMonth" runat="server" Text='<%#Eval("intToMonth") %>' />
                                                                    <asp:Label ID="lblintToYear" runat="server" Text='<%#Eval("intToYear") %>' />
                                                                    <asp:Label ID="lblPresentDay" runat="server" /></span>
                                                                <p>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("strSpclLibrary") %>' />
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <!---List View End-->
                                            <!--- Modal -->
                                            <div class="modal backgroundoverlay " id="divEducation" runat="server" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">

                                                <div class="modal-dialog modal-dialog-centered" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="exampleModalLabel">Education</h5>
                                                            <asp:LinkButton OnClick="lnkCancelEducation_Click" OnClientClick="javascript:callCancelEdu();" CssClass="close" id="closeedu" runat="server" >
                                                                <span aria-hidden="true">&times;</span>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="modal-body text-left ">
                                                            <asp:UpdatePanel ID="upedu" runat="server">
                                                                <ContentTemplate>
                                                            <div class="form-group">
                                                                <label for="textbox">Institute Name</label>
                                                                
                                                                <ajax:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtInstitute"
                                                                    ID="AutoCompleteExtender11" OnClientShown="PopupShown" runat="server" FirstRowSelected="false" CompletionListCssClass="AutoCompletionList" CompletionListItemCssClass="AutoComp_listItem" CompletionListHighlightedItemCssClass="AutoComp_itemHighlighted">
                                                                </ajax:AutoCompleteExtender>
                                                                <asp:TextBox ID="txtInstitute"  runat="server" ValidationGroup="validationEdu" ClientIDMode="Static" placeholder="Enter Institute Name"
                                                                    class="form-control"></asp:TextBox>
                                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInstitute"
                                                                    Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please enter Institute / University name."
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="textbox">Degree</label>
                                                                <asp:TextBox ID="txtTitle" runat="server" ClientIDMode="Static" placeholder="Degree"
                                                                    class="form-control"></asp:TextBox>
                                                                <ajax:AutoCompleteExtender ServiceMethod="GetDegreeList" MinimumPrefixLength="1"
                                                                    CompletionInterval="100" OnClientShown="PopupShown" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtTitle"
                                                                    ID="AutoCompleteExtender22" runat="server" FirstRowSelected="false" CompletionListCssClass="AutoCompletionList" CompletionListItemCssClass="AutoComp_listItem" CompletionListHighlightedItemCssClass="AutoComp_itemHighlighted" >
                                                                </ajax:AutoCompleteExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtTitle"
                                                                    Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please enter Degree."
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="textbox">Percentile</label>
                                                                <asp:TextBox ID="txtPercentile" runat="server" ClientIDMode="Static" placeholder="Percentile" class="form-control"                                                                    
                                                                 onkeypress="return isNumberKey(event,this)" maxlength="5" min="0.01" onkeyup="this.value = minmax(this.value, 0, 100)">
                                                                </asp:TextBox>
                                                            <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
                                <input type="text" name ="" value="" id="price" maxlength="5" min="0.01" step="0.01" onkeyup="this.value = minmax(this.value, 0, 100)">--%>
                                                                <%--<ajax:AutoCompleteExtender ServiceMethod="GetDegreeList" MinimumPrefixLength="1"
                                                                    CompletionInterval="10" OnClientShown="PopupShown" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtTitle"
                                                                    ID="AutoCompleteExtender5" runat="server" FirstRowSelected="false" CompletionListCssClass="AutoCompletionList" CompletionListItemCssClass="AutoComp_listItem" CompletionListHighlightedItemCssClass="AutoComp_itemHighlighted" >
                                                                </ajax:AutoCompleteExtender>--%>
                                                                    <asp:RequiredFieldValidator ID="rfdPercentile" runat="server" ControlToValidate="txtPercentile"
                                                                    Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please enter Percentile."
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="form-group">
                                                                        <label for="textarea">Timeframe</label>
                                                                        <div class="row custom-select-con">
                                                                            <div class="col-sm-6 mb-sm-0 mb-3">
                                                                                <div class="select-wrapper">
                                                                                <select class="form-control" name="select" id="ddlFromMonth" runat="server">
                                                                                    <option>Month</option>
                                                                                    <option>Jan</option>
                                                                                    <option>Feb</option>
                                                                                    <option>Mar</option>
                                                                                    <option>Apr</option>
                                                                                    <option>May</option>
                                                                                    <option>Jun</option>
                                                                                    <option>Jul</option>
                                                                                    <option>Aug</option>
                                                                                    <option>Sep</option>
                                                                                    <option>Oct</option>
                                                                                    <option>Nov</option>
                                                                                    <option>Dec</option>
                                                                                </select>
                                                                            </div>
                                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlFromMonth"
                                                                    Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select from month."
                                                                    InitialValue="Month" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="select-wrapper">
                                                                                <asp:DropDownList ID="txtEducationFromdt" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtEducationFromdt"
                                                                    Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select from year."
                                                                    InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label for="textarea">To</label>
                                                                        <div class="row custom-select-con">
                                                                            <div class="col-sm-6 mb-sm-0 mb-3">
                                                                                <div class="select-wrapper">
                                                                                <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                                    <asp:ListItem Selected="True">Month</asp:ListItem>
                                                                                    <asp:ListItem>Jan</asp:ListItem>
                                                                                    <asp:ListItem>Feb</asp:ListItem>
                                                                                    <asp:ListItem>Mar</asp:ListItem>
                                                                                    <asp:ListItem>Apr</asp:ListItem>
                                                                                    <asp:ListItem>May</asp:ListItem>
                                                                                    <asp:ListItem>Jun</asp:ListItem>
                                                                                    <asp:ListItem>Jul</asp:ListItem>
                                                                                    <asp:ListItem>Aug</asp:ListItem>
                                                                                    <asp:ListItem>Sep</asp:ListItem>
                                                                                    <asp:ListItem>Oct</asp:ListItem>
                                                                                    <asp:ListItem>Nov</asp:ListItem>
                                                                                    <asp:ListItem>Dec</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlToMonth"
                                                                    Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select to month."
                                                                    InitialValue="Month" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <div class="col-sm-6">
                                                                                <div class="select-wrapper">
                                                                                <asp:DropDownList ID="txtEducationTodt" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtEducationTodt"
                                                                    Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select to year."
                                                                    InitialValue="Year" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                            <asp:Label class="pl-3 display-inline" ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                                            <div class="col-sm-12">
                                                                                <div class="m-t-23px custom-checkbox">
                                                                                    <asp:CheckBox ID="chkEducation" AutoPostBack="true" CssClass="custom-checkbox" ClientIDMode="Static" text="Currently studying" OnCheckedChanged="chkEducation_CheckedChanged1"
                                                                                        runat="server" />
                                                                                    
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </ContentTemplate>
                                                               
                                                            </asp:UpdatePanel>
                                                            <div class="form-group">
                                                                <label for="textarea">Description</label>
                                                                <textarea rows="5" maxlength="500" class="form-control" id="txtEduDescription" runat="server" placeholder="Description"
                                                                    clientidmode="Static"></textarea>
                                                            </div>                                                            
                                                      
                                                            <div class="submit-con">
                                                             <asp:LinkButton runat="server" ID="lnkCancelEducation" Text="Cancel" CssClass="add-scroller btn btn-outline-primary m-r-15"
                                                              ClientIDMode="Static" OnClick="lnkCancelEducation_Click" OnClientClick="javascript:callCancelEdu();"></asp:LinkButton>
                                                             <asp:LinkButton runat="server" ID="lnkSaveEducation" Text="Save" CssClass=" btn btn-primary"
                                                              ClientIDMode="Static" ValidationGroup="validationEdu" OnClick="lnkSaveEducation_Click"
                                                              OnClientClick="javascript:callSaveEdu();"></asp:LinkButton>
                                                            </div>
                                                           </ContentTemplate>
                                                           <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnkCancelEducation" />
                                                           </Triggers>
                                                          </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <!-- Modal End -->
                                            <!--
                        <div class="card skill-card m-t-20">
                            <h5>Skills</h5>
                            
                        <div class="row">
                            <div class="col-sm-9">
                                <div class="form-group">
                                    <input type="text" class="form-control" placeholder="Enter Skill">
                                </div>
                            </div>
                        
                            <div class="col-sm-3">
                                <button class="btn btn-primary">Add</button>
                            </div>
                        
                        </div>
                            
                        <div class="submit-con text-left">
                                    <button class="btn btn-outline-primary m-r-15">Cancel</button>
                                    <button class="btn btn-primary">Save</button>
                                </div>
                            
                        </div>
                        -->
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!-- Education ended -->
                                
                                
                            <!-- Achievements Start -->
                            <asp:UpdatePanel ID="PnlAchivement" runat="server">
                             <ContentTemplate>
                              <div class="custom-nav-container tab-pane container active show fade" id="achievements">
                               <div class="card-list-con blog-list">
                                <div class="btn-title-con">
                                 <div>
                                  <h5 class="card-title float-left">Achievements</h5>
                                 </div>
                                 <div>
                                  <asp:LinkButton ID="lnkAddachive" runat="server" class="btn btn-outline-primary hide-body-scroll float-right changes-position"
                                   Text="+  Add" OnClientClick="CalllnkAddachive();" OnClick="lnkAddachive_Click"></asp:LinkButton>
                                 </div>
                                </div>                                          
                                            
                                <asp:HiddenField ID="hdnintacheIdelet" Value="" ClientIDMode="Static" runat="server" />
                                <asp:HiddenField ID="hdnstracheDescriptiondel" Value="" ClientIDMode="Static" runat="server" />
                                <asp:ListView runat="server" ID="lstAchivement" OnItemDataBound="lstAchivement_ItemDataBound"
                                 OnItemCommand="lstAchivement_ItemCommand" CellPadding="9">
                                 <ItemTemplate>
                                  <div class="card top-list">
                                   <div class="post-con">
                                    <div class="post-body">
                                     <span class="more-btn float-right" runat="server" id="divAchivementED">
                                      <span class="dropdown">
                                       <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        <img src="images/more.svg" alt="" class="more-btn">
                                       </a>
                                      <span id="updaAchi" class="dropdown-menu updaAchi" aria-labelledby="dropdownMenuLink" x-placement="bottom-start"><asp:HiddenField ID="hdnintachIdelet" Value='<%# Eval("intAchivmentId") %>' ClientIDMode="Static" runat="server" />
                                        <asp:LinkButton ID="lnkEditDoc" Font-Underline="false" Visible="true" ToolTip="Edit"
                                         Text="Edit" class="hide-body-scroll dropdown-item" CommandName="EditAch" CausesValidation="false" runat="server">
                                         <span class="lnr lnr-pencil"></span> Edit
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkDeleteDoc" Visible="true" OnClientClick="javascript:docdelete();return false;"
                                         ToolTip="Delete" Text="Delete" class="dropdown-item hide-body-scroll" CommandName="DeleteAch" CausesValidation="false"
                                         runat="server"><span class="lnr lnr-trash"></span> Delete                                
                                        </asp:LinkButton>
                                       </span>
                                      </span>
                                     </span>
                                     <asp:HiddenField ID="hdnintAchivmentId" Value='<%#Eval("intAchivmentId") %>' runat="server" ClientIDMode="Static" />                                            
                                     <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
                                     <h3>
                                      <asp:Label ID="lblMilestone" runat="server" Text='<%#Eval("strMilestone") %>' /> 
                                     </h3>
                                     <ul class="small-datee">
                                      <li runat="server" Visible='<%# String.IsNullOrEmpty(Eval("strCompititionName").ToString()) ? false : true %>'>
                                       <asp:Label ID="lblstrCompititionName" runat="server" Text='<%#Eval("strCompititionName") %>' />
                                      </li>
                                      <li runat="server" Visible='<%# String.IsNullOrEmpty(Eval("strPosition").ToString()) ? false : true %>'>
                                       <asp:Label ID="lblstrPosition" runat="server" Text='<%#Eval("strPosition") %>' />
                                      </li>
                                      <li runat="server" Visible='<%# String.IsNullOrEmpty(Eval("strAdditionalAward").ToString()) ? false : true %>'>
                                       <asp:Label ID="lblAdditionalAward" runat="server" Text='<%#Eval("strAdditionalAward") %>' />
                                      </li>
                                     </ul>
                                     <p class="mt-1">
                                      <asp:Label ID="Label2" runat="server" Text='<%#Eval("strDescription") %>' />
                                     </p>                                     
                                    </div>
                                   </div>
                                  </div>
                                 </ItemTemplate>
                                </asp:ListView>
                                 <!-- Modal -->
                                <div class="modal backgroundoverlay active show fade" id="divAchivement" runat="server" clientidmode="Static" tabindex="-1" 
                                 role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="modal-dialog  modal-dialog-centered">
                                  <ContentTemplate>
                                   <asp:HiddenField ID="hdnCompetitionType" Value="0" runat="server" ClientIDMode="Static" />
                                   <div class="w-100" role="document">
                                    <div class="modal-content">
                                     <div class="modal-header">
                                      <h5 class="modal-title" id="exampleModalLabel">Add Achievements</h5>
                                      <asp:LinkButton OnClick="lnkCancelAchivemnt_Click" CssClass="close" ID="closeach" 
                                       runat="server"><%--OnClientClick="javascript:callCancelAch();"--%>
                                       <span aria-hidden="true">&times;</span>
                                      </asp:LinkButton>
                                     </div>
                                     <div class="modal-body text-left">
                                      <div class="form-group">
                                       <div class="row custom-select-con">
                                        <div class="col-sm-12 mb-sm-0 mb-6">
                                          <label for="textarea">Milestone</label>      
                                         <div class="select-wrapper">
                                          <asp:DropDownList ID="ddlMilestone" runat="server" ClientIDMode="Static" class="form-control" ValidationGroup="validationAchiv"
                                           AutoPostBack="true" OnSelectedIndexChanged="ddlMilestone_SelectedIndexChanged">
                                          </asp:DropDownList>
                                         </div>
                                         <asp:Label ID="lblAchivmentmsg" runat="server" ForeColor="Red" ClientIDMode="Static"></asp:Label>
                                        </div>
                                        <div class="col-sm-12 pt-3" id="divddlPosition" runat="server" Visible="false" ClientIDMode="Static">
                                         <label for="textarea">Position</label>   
                                         <div class="select-wrapper">
                                          <asp:DropDownList ID="ddlPosition" runat="server" ClientIDMode="Static" class="form-control" ValidationGroup="validationAchiv">
                                          </asp:DropDownList>
                                         </div>
                                      
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlPosition" ForeColor="Red"
                                           Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please select position." InitialValue="Position">
                                          </asp:RequiredFieldValidator>
                                       
                                        </div>
                                       </div>
                                      </div>
                                      <div id="divCtrls" runat="server" visible="false" ClientIDMode="Static">
                                       <div class="form-group" id="divCompetition" runat="server" visible="false" ClientIDMode="Static">
                                        <label for="textarea">Competition</label>
                                        <asp:TextBox ID="txtCompitition" runat="server" placeholder="Name of Competition" MaxLength="100"
                                         ClientIDMode="Static" class="form-control"></asp:TextBox>
                                        <ajax:AutoCompleteExtender ID="AutoCompleteExtender33" runat="server" FirstRowSelected="false" CompletionListCssClass="AutoCompletionList" 
                                         ServiceMethod="GetCompititionLists" MinimumPrefixLength="1" CompletionListItemCssClass="AutoComp_listItem" TargetControlID="txtCompitition"  
                                         CompletionInterval="100" OnClientShown="PopupShown" EnableCaching="false" CompletionSetCount="10" CompletionListHighlightedItemCssClass="AutoComp_itemHighlighted"                                       
                                         UseContextKey="true" ContextKey="<%# Eval(hdnCompetitionType.Value) %>">                                         
                                        </ajax:AutoCompleteExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtCompitition" ForeColor="Red"
                                         Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please enter competition.">
                                        </asp:RequiredFieldValidator>
                                       </div>
                                       <div class="form-group" id="divCommitteeName" runat="server" visible="false" ClientIDMode="Static">
                                        <label for="textbox">Committee Name</label>
                                        <asp:TextBox ID="txtCommitteeName" runat="server" MaxLength="100" autocomplete="off" placeholder="Committee Name"
                                         class="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfdCommittee" runat="server" ControlToValidate="txtCommitteeName"
                                         Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please enter Committee name."
                                         ForeColor="Red"></asp:RequiredFieldValidator>
                                       </div>
                                       <div class="form-group" id="divJournalName" runat="server" visible="false" ClientIDMode="Static">
                                        <label for="textbox">Journal Name</label>
                                        <asp:TextBox ID="txtJournalName" runat="server" MaxLength="100" autocomplete="off" placeholder="Journal Name"
                                         class="form-control"></asp:TextBox>
                                        <ajax:AutoCompleteExtender ID="AutoCompleteExtender44" runat="server" FirstRowSelected="false" CompletionListCssClass="AutoCompletionList" 
                                         ServiceMethod="GetCompititionLists" MinimumPrefixLength="1" CompletionListItemCssClass="AutoComp_listItem" TargetControlID="txtJournalName"  
                                         CompletionInterval="100" OnClientShown="PopupShown" EnableCaching="false" CompletionSetCount="10" CompletionListHighlightedItemCssClass="AutoComp_itemHighlighted"                                       
                                         UseContextKey="true" ContextKey="<%# Eval(hdnCompetitionType.Value) %>">
                                        </ajax:AutoCompleteExtender>
                                        <asp:RequiredFieldValidator ID="rfdJournal" runat="server" ControlToValidate="txtJournalName"
                                         Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please enter Journal Name."
                                         ForeColor="Red"></asp:RequiredFieldValidator>
                                       </div>
                                       <div class="form-group" id="divAdditionalAwrd" runat="server" visible="false" ClientIDMode="Static">
                                        <label for="textarea">Additional Award</label>
                                        <asp:TextBox ID="txtAdditionalAwrd" runat="server" MaxLength="100" autocomplete="off" placeholder="Additional Award"
                                         class="form-control"></asp:TextBox>
                                       </div>
                                       <div class="form-group" id="divDescription" runat="server" visible="false" ClientIDMode="Static">
                                        <label for="textarea">Description</label>
                                        <textarea rows="5" id="txtAchivDescription" runat="server" maxlength="500" placeholder="Description" class="form-control">
                                        </textarea>
                                       </div>
                                       <p>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlMilestone" ForeColor="Red"
                                         Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please select milestone." InitialValue="Type of Milestone">
                                        </asp:RequiredFieldValidator>
                                       </p>                                                                 
                                       
                                       <p>                                                                       
                                       </p>
                                      </div>
                                      <div class="submit-con">
                                       <asp:LinkButton ID="LinkButton4" runat="server" ClientIDMode="Static" CssClass="add-scroller btn btn-outline-primary m-r-15"
                                        Text="Cancel" OnClick="lnkCancelAchivemnt_Click" Visible="false"></asp:LinkButton><%--OnClientClick="javascript:callCancelAch();"--%>
                                       <asp:LinkButton ID="lnkContinue" runat="server" ClientIDMode="Static" CssClass="btn btn-primary"
                                        Text="Continue" OnClick="lnkContinue_Click" Visible="false"></asp:LinkButton><%-- OnClientClick="javascript:callContinueAch();"--%>
                                       <asp:LinkButton ID="lnkSaveAchivemnt" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Visible="false"
                                        ValidationGroup="validationAchiv" Text="Save" OnClick="lnkSaveAchivemnt_Click" OnClientClick="javascript:callSaveAch();">
                                       </asp:LinkButton>
                                      </div>
                                     </div>
                                    </div>
                                   </div>
                                  </ContentTemplate>
                                 </asp:UpdatePanel>
                                </div>
                               </div>
                              </div>
                             </ContentTemplate>
                            </asp:UpdatePanel>
                            <!-- Achievements ended -->    

                              <div class="modal backgroundoverlay active show" id="divEditProfile" style="display:none;" runat="server" ClientIDMode="Static" >
                               <div class="modal-dialog modal-dialog-centered" role="document">
                                <div class="modal-content">
                                 <div class="modal-header">
                                     <h5 class="modal-title" id="examleModalLabel">Profile Edit                    <button type="button" class="close" id="close-popup" data-dismiss="modal" aria-label="Close">
                                         <span aria-hidden="true">&times;</span>
                                     </button>
                                 </div>
                                 <div id="mydiv" runat="server" class="modal-body text-left">                                     
                                     <div class="form-group">
                                      <label>Name</label>
                                      <asp:TextBox ID="txtName" runat="server" placeholder="Name" autocomplete="off" Maxlength="26" 
                                       class="form-control name-valid" onKeyDown="if(event.keyCode==13) return false;" ClientIDMode="Static">
                                      </asp:TextBox>
                                     </div>
                                     <div class="form-group">
                                      <label>Gender</label>
                                      <asp:TextBox ID="txtGender" class="form-control" runat="server" placeholder="Gender" onKeyDown="if(event.keyCode==13) return false;" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                     </div>
                                     <div class="form-group">
                                      <label>Language</label>
                                      <asp:TextBox ID="txtLanguage" class="form-control" runat="server" placeholder="Languages Known" onKeyDown="if(event.keyCode==13) return false;" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                     </div>
                                     <div class="form-group">
                                      <label>Email Id</label>
                                      <asp:TextBox ID="txtEmailId" class="form-control" runat="server" placeholder="Email Id" Enabled="false" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                     </div>
                                     <div class="form-group">
                                      <label>Phone Number</label>
                                       <%--<asp:TextBox ID="txtPhone" class="form-control" runat="server" ClientIDMode="Static" MaxLength="11" placeholder="Phone Number" 
                                       önKeyPress="javascript:phonenumber('<%= txtPhone.ClientID%>')"
                                       onKeyDown="if(event.keyCode==13) return false;" autocomplete="off"></asp:TextBox>--%>
                                       <asp:TextBox ID="txtPhone" class="form-control" runat="server" ClientIDMode="Static" placeholder="Phone Number" 
                                        onkeypress="return isNumber(event)" maxlength="11" autocomplete="off" onKeyDown="if(event.keyCode==13) return false;">
                                       </asp:TextBox>
                                     </div>
                                     <p>
                                      <asp:Label ID="lblProfilemsg" runat="server" ForeColor="Red"></asp:Label>
                                     </p>
                                     <div class="submit-con">                                     
                                      <asp:LinkButton ID="lnkCancelProfile" runat="server" ClientIDMode="Static" CssClass="add-scroller btn btn-outline-primary m-r-15"
                                       Text="Cancel" OnClientClick="javascript:callBodyEnable();return false;"></asp:LinkButton>
                                      <asp:LinkButton ID="lnkProfileSave" runat="server" ClientIDMode="Static" CssClass="add-scroller btn btn-primary"
                                       Text="Save" OnClick="lnkProfileSave_click"></asp:LinkButton>
                                     </div>
                                </div>
                               </div>
                              </div>
                                       
                             </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- center-panel ended -->
            <div class="right-panel-back-layer"></div>
               <div class="cropper-modal modal" style="position: fixed; top: 0; right: 0; bottom: 0; left: 0; z-index: 1050; display: none;  outline: 0; background: #000000de;">
                       <div class="modal-dialog modal-dialog-centered"" >
                           <div class="modal-content">
                              <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Change Image</h5>
                                      <button type="button" class="close">
                                              <span onclick="CancelCloseImage()" aria-hidden="true">&times;</span>
                                      </button>
                                  </div>
                                                                    
                                    <div class="modal-body">
                                         <div class="custom-modal">
                                          <div id="cropper-wrapper" class="" style="position: relative;height: 500px;  width: 450px; overflow: hidden; margin: 0 auto;"">
                                            <div class="" style="position: absolute; width: 100%; height: 100%; left: 0; top: 0; box-shadow: 0 0 8px 2px rgba(100, 100, 100, .4);">
                                                <div id="cropContainer1"></div>
                                            </div>
                                          </div>
                                       </div>
                                        <div class="submit-con">
                                            <asp:LinkButton runat="server" ID="cnclImage" OnClientClick="javascript:CancelCloseImage(); return false" class="btn btn-outline-primary m-r-15">Cancel</asp:LinkButton>
                                             <asp:LinkButton runat="server" ID="svImage" ClientIdMode="Static" class="btn btn-primary resize-done" OnClientClick="showLoader1();" OnClick="btncrpSave_Click">Save</asp:LinkButton>
                                            
                                         </div>
                                                                       
                                     </div>

                                   </div>
                              </div>
                         </div> 
               <div style="display: none">
                   <input name="upload" onchange="ShowCropper.bind(this)()" type="file" id="fileinput" />
               </div>

           
            <!-- right-panal ended -->
        </div>
        <!-- panel-cover ended -->
    </div>
    <asp:HiddenField ID="hdnTabIds" Value="0" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnPhoto" Value="" runat="server" ClientIDMode="Static" />
    <%--  <asp:HiddenField ID="hdnDocName" runat="server" ClientIDMode="Static" />--%>
    <asp:HiddenField ID="hdnErrorMsg" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnPhotoPath" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnFileData" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnPhotoName" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnImageProfile" runat="server" ClientIDMode="Static" />

    <!---close popups-->
    <!--     <script>
      $(document).ready(function(){

          $("#close-popup").click(function(){
            $(".backgroundoverlay").hide();
            $('body').addClass('animated fadeOut')
          });
            });
    </script> -->


    <!-- render-sectiona ended -->
    <script type="text/javascript">

        function alpha(e) {
            var k;
            document.all ? k = e.keyCode : k = e.which;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
        }

        function isNumberKey(evt, obj) {
            //debugger;
            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function minmax(value, min, max) {
            //debugger;
            if (parseFloat(value) < min || isNaN(value))
                return 0;
            else if (parseFloat(value) > max)
                return 100;
            else return value;
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function callCancelSkill() {
            $('#lnkCancelSkill').css("background", "#D0D0D0");
            $('#lnkCancelSkill').css("border", "2px solid #BCBDCE");
        }
        function callCancelEdu() {
            $('#lnkCancelEducation').css("background", "#D0D0D0");
            $('#lnkCancelEducation').css("border", "2px solid #BCBDCE");
        }
        function DisablePostBack() {
            if (event.keyCode == 13) {
                event.preventDefault();
            }
        }
        function CallCameraload() {
            $('#fileinput').trigger('click');
        }
        function CloseImage() {

            $(".cropper-modal").hide();
            $("#fileinput").val('');
            $('#divSuccess').css("display", "block");
            //showSuccessPopup('Success', "Profile image updated successfully.");
        }
        function CancelCloseImage() {

            $(".cropper-modal").hide();
            $("#fileinput").val('');
            $('#divSuccess').css("display", "none");
            //showSuccessPopup('Success', "Profile image updated successfully.");
        }
        function CallEducationMiddle() {
            var offset = $("#txtEduDescription").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $(window).scrollTop();
            $("html, body").animate({ scrollTop: $(window).height() + 2000 }, 10);
        }
        function CloseMsg() {
            $('#divSuccess').css("display", "none");
        }

        function ShowCropper() {
            if (this.value) {
                $('#svImage').css('pointer-events', 'none');
                url = this.files[0];

                var allowedFileTypes = ['jpg', 'jpeg', 'png'];
                $("#hdnErrorMsg").val('');
                $("#hdnPhotoPath").val('');
                $("#hdnPhotoName").val('');
                var fileUpload = $("#fileup1").get(0);
                var files = this.files;
                var data = new FormData();
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }
                ext = url.name.split('.').pop().toLowerCase();

                if ($.inArray(ext, allowedFileTypes) == -1) {
                    $(".cropper-modal").hide();
                    showSuccessPopup('Error', "You can only upload jpg and png files.");
                    $("#fileinput").val('');
                    return;
                }
                if (files[0].size > 3145728) {
                    $(".cropper-modal").hide();
                    showSuccessPopup('Error', "File size should be less than or equal to 3 MB.");
                    $("#fileinput").val('');
                    return;
                }

                var fr = new FileReader();
                fr.readAsDataURL(this.files[0]);
                fr.onload = function (e) {
                    var img = new Image;
                    img.onload = function () {
                        // alert(img.width); // image is loaded; sizes are available 
                        if (img.width < 200 || img.height < 200) {
                            $(".cropper-modal").hide();
                            showSuccessPopup('Error', "Please Select image with minimum 200 X 200 in Size.");
                            $("#fileinput").val('');
                        } else {
                            $.ajax({
                                type: "POST",
                                url: "handler/ProfileImageHandler.ashx",
                                contentType: false,
                                processData: false,
                                data: data,
                                success: function (result) {
                                    if (result == 'Please select image.') {
                                        $("#lblmsg").text(result);
                                        $("#hdnErrorMsg").val(result)
                                        // CallUploadimg();
                                        $(".cropper-modal").hide();
                                        showSuccessPopup("Error", result);
                                        $("#fileinput").val('');

                                    }
                                    else {
                                        var v = result.split(":");
                                        $('#svImage').css('pointer-events', 'auto');
                                        $("#hdnPhotoPath").val(v[0]);
                                        $("#hdnPhotoName").val(v[1]);

                                    }
                                },
                                error: function () {
                                    $(".cropper-modal").hide();
                                    showSuccessPopup('Error', "There was an error while selecting File!");
                                    $("#fileinput").val('');
                                }
                            });

                            if (typeof $uploadCrop != 'undefined') {
                                $uploadCrop.croppie('destroy');
                            }

                            $(".cropper-modal").show();

                            $uploadCrop = $('#cropContainer1').croppie({
                                enableExif: true,
                                viewport: {
                                    width: 400,
                                    height: 400,
                                    type: 'circle'
                                },
                                enableResize: false,
                                url: e.target.result
                            });
                            $('.resize-done').on('click', function (ev) {
                                $uploadCrop.croppie('result', {
                                    type: 'base64',
                                    size: { width: 200 }
                                }).then(function (resp) {

                                    $("#hdnFileData").val(resp);
                                    //$('#svImageActual').click();

                                    $(".cropper-modal").hide();
                                });
                            });


                        }
                    };
                    img.src = fr.result; // is the data URL because called with readAsDataURL
                };



            }
        }
        // $('#fileinput').change(function(evt) {
        //    showCropper(evt.target.files[0]);
        //});
        function callBodyEnable() {
            $('#divEditProfile').css('display', 'none');
        }
        function CallUserJSON() {
            $.ajax({
                url: 'Home.aspx/GetUserJSONdata',
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (data) {
                    $('#divEditProfile').css('display', 'block');
                    var parsed = $.parseJSON(data.d);
                    $.each(parsed, function (i, jsondata) {
                        $('#txtName').val(jsondata.NAME);
                        $('#txtGender').val(jsondata.chrSex);
                        $('#txtLanguage').val(jsondata.vchrLanguages);
                        $('#txtEmailId').val(jsondata.vchrUserName);
                        $('#txtPhone').val(jsondata.intMobile);
                    });
                }
            });
        }
        function selectWork() {
            $('#hdnintworkeIdelet').val($(this).children('#hdnintworkIdelet').val());
            $('#hdnstrworkeDescriptiondel').val($(this).children('#hdnstrworkDescriptiondel').val());
        }
        function PopupShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 99999999;
        }
        $(document).on('click', '#close-popup', function () {

            $(".backgroundoverlay").hide();
            $('body').removeClass('remove-scroller');

        });
        function CallEduLI() {
            //$("#PopUpCropImage").css("display", "none");
            //$("#imgUser").attr("src", $("#hdnImageProfile").val());
            //$('#lnkDeleteConfirm').css("box-shadow", "0px 0px 0px #00B7E5");
        }
        function callDoccancel() {
            // $('#lnkCancelDoc').css("background", "#D0D0D0");
            // $('#lnkCancelDoc').css("border", "2px solid #BCBDCE");
        }
        function CallDocMiddle() {
            var offset = $("#txtDocTitle").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $(window).scrollTop();
            var txt = document.getElementById("txtDocTitle");
            //  var p = GetScreenCordinates(txt);
            $("html, body").animate({ scrollTop: $(window).height() }, 10);
        }
        function CallUploadDoc() {
            $(".divProgress").css("display", "none");
            // $('#lnkuploadDoc').css("box-shadow", "0px 0px 2px #00B7E5");
            // $('#lnkuploadDoc').css("background", "#DCDBDB");
        }
        function CallDocLI() {
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
            // $("#imgUserm").attr("src", $("#hdnImageProfile").val());
        }
        function callDocSave() {
            // $('#LinkSave').css("box-shadow", "0px 0px 5px #00B7E5");
            // $('#LinkSave').css("background", "#00A5AA");
            if ($('#txtDocTitle').text() == '') {
                setTimeout(
                    function () {
                        // $('#LinkSave').css("background", "#0096a1");
                        // $('#LinkSave').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
            if ($('#lblfilenamee').text() == 'Please select file to upload.') {
                // $('#LinkSave').css("box-shadow", "0px 0px 5px #00B7E5");
                // $('#LinkSave').css("background", "#00A5AA");
                setTimeout(
                    function () {
                        // $('#LinkSave').css("background", "#0096a1");
                        // $('#LinkSave').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
        }
        function divdeletes() {
            // $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 5px #00B7E5");
            $(".divProgress").css("display", "none");
            $('#divDeletesucess').css("display", "none");
        }
        function callSaveAch() {
            // $('#lnkSaveAchivemnt').css("background", "#00A5AA");
            // $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtCompitition').text() == '' || $('#ddlMilestone').text() == 'Type of Milestone' || $('#ddlPosition').text() == 'Position') {
                setTimeout(
                    function () {
                        // $('#lnkSaveAchivemnt').css("background", "#0096a1");
                        // $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
            if ($('#lblAchivmentmsg').text() == 'Please select milestone.' || $('#lblAchivmentmsg').text() == 'Please select position.') {
                setTimeout(
                    function () {
                        // $('#lnkSaveAchivemnt').css("background", "#0096a1");
                        // $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
        }

        function callContinueAch() {
            // $('#lnkSaveAchivemnt').css("background", "#00A5AA");
            // $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 5px #00B7E5");
            //if ($('#txtCompitition').text() == '' || $('#ddlMilestone').text() == 'Type of Milestone' || $('#ddlPosition').text() == 'Position') {
            //    setTimeout(
            //        function () {
            //            // $('#lnkSaveAchivemnt').css("background", "#0096a1");
            //            // $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 0px #00B7E5");
            //        }, 1000);
            //}
            //if ($('#lblAchivmentmsg').text() == 'Please select milestone.' || $('#lblAchivmentmsg').text() == 'Please select position.') {
            //    setTimeout(
            //        function () {
            //            // $('#lnkSaveAchivemnt').css("background", "#0096a1");
            //            // $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 0px #00B7E5");
            //        }, 1000);
            //}
            debugger;
            document.getElementById("divddlPosition").style.display = "block";
        }
    </script>
    <script type="text/javascript">
        function CallMessageNotification() {
            if ($("#lblNotifyCount").text() == '0') {
                document.getElementById("divNotification1").style.display = "none";
            }
            if ($("#lblInboxCount").text() == '0') {
                document.getElementById("divInbox").style.display = "none";
            }
        }
        function CalllnkAddachive() {
            // $('#ctl00_ContentPlaceHolder1_lnkAddachive').css("box-shadow", "0px 0px 2px #00B7E5");
            // $('#ctl00_ContentPlaceHolder1_lnkAddachive').css("background", "#DCDBDB");
        }
        function CallAchiveMiddle() {
            var offset = $("#divAchivement").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $("#divAchivement").position().top + 800;
            $("html, body").animate({ scrollTop: $(window).height() + 2000 }, 10);
        }
        function CallAchLI() {
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
            // $("#imgUserm").attr("src", $("#hdnImageProfile").val());
        }
        function CalllnlAddEducation() {
            //$('#ctl00_ContentPlaceHolder1_lnlAddEducation').css("box-shadow", "0px 0px 2px #00B7E5");
            //$('#ctl00_ContentPlaceHolder1_lnlAddEducation').css("background", "#DCDBDB");
        }
        function CalllnkAddskill() {
            // $('#ctl00_ContentPlaceHolder1_lnkAddskill').css("box-shadow", "0px 0px 2px #00B7E5");
            // $('#ctl00_ContentPlaceHolder1_lnkAddskill').css("background", "#DCDBDB");
        }
        function docdelete() {
            $('#divDeletesucess').css("display", "block");
            $('#AddWorkExp').css("display", "none");
        }
        function groupConnChange() {
            $("#imgGroup").click(function () {
                $("#divfrdgrp").removeClass("fGroupBox frd").addClass("fGroupBox grp");
                $("#divgroupSection").show();
                $("#divFriendSection").hide();

                return false;
            });
            $("#imgFriend").click(function () {
                $("#divfrdgrp").removeClass("fGroupBox grp").addClass("fGroupBox frd");
                $("#divFriendSection").show();
                $("#divgroupSection").hide();
                return false;
            });
            if ($("#lblNotifyCount").text() == '0') {
                document.getElementById("divNotification1").style.display = "none";
            }
            if ($("#lblInboxCount").text() == '0') {
                document.getElementById("divInbox").style.display = "none";
            }
        }
        function CallWorkMiddle() {
            var offset = $("#aAddworkExp").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $("#aAddworkExp").position().top - 500;
            $("html, body").animate({ scrollTop: y }, 10);
        }
        function CallExpNumaric() {
            //called when key is pressed in textbox
            $("#txtFromYear").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
            $("#txtToYear").keypress(function (e) {
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    return false;
                }
            });
        }

        function OverlayBody() {
            $('#bodyelement').addClass("remove-scroller");
        }
        function CallaAddworkExp() {
            OverlayBody();
            // $('#aAddworkExp').css("box-shadow", "0px 0px 2px #00B7E5");
            // $('#aAddworkExp').css("background", "#DCDBDB");
        }
        function callSaveEdu() {
            // $('#lnkSaveEducation').css("background", "#00A5AA");
            // $('#lnkSaveEducation').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtInstitute').text() == '' || $('#txtTitle').text() == '' || $('#ddlFromMonth').text() == 'Month'
                || $('#txtEducationFromdt').text() == 'Year' || $('#ddlToMonth').text() == 'Month' || $('#txtEducationTodt').text() == 'Year') {
                setTimeout(
                    function () {
                        // $('#lnkSaveEducation').css("background", "#0096a1");
                        // $('#lnkSaveEducation').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
        }
        function callSaveExp() {
            // $('#lnlSaveExp').css("background", "#00A5AA");
            // $('#lnlSaveExp').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#txtInstituteName').text() == '' || $('#txtDegree').text() == '' || $('#fromMonth').text() == 'Month'
                || $('#txtFromYear').text() == 'Year' || $('#toMonth').text() == 'Month' || $('#txtToYear').text() == 'Year') {
                setTimeout(
                    function () {
                        // $('#lnlSaveExp').css("background", "#0096a1");
                        // $('#lnlSaveExp').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }

        }
        function CallAddSkill() {
            $("#PopUpCropImage").css("display", "none");
            // $('#btnAreaExpSave').css("background", "#00A5AA");
            // $('#btnAreaExpSave').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#lblareaskill').text() == 'Please enter area of expertise.') {
                setTimeout(
                    function () {
                        // $('#btnAreaExpSave').css("background", "#0096a1");
                        // $('#btnAreaExpSave').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
        }
        function callCancelExp() {
            // $('#LinkButton1').css("background", "#D0D0D0");
            // $('#LinkButton1').css("border", "2px solid #BCBDCE");
        }
        function CallWorkLI() {
            $("#PopUpCropImage").css("display", "none");
            $("#imgUser").attr("src", $("#hdnImageProfile").val());
            // $("#imgUserm").attr("src", $("#hdnImageProfile").val());
            $("AddWorkExp").modal("show");
        }
    </script>
    <script type="text/javascript">
        function showSuccessPopup(title, msg) {
            $('#successPopupTitle').text(title);
            $("#successPopupMsg").text(msg);
            $("#successPopupModal").show();
        }

        $('#txtPercentile').keypress(function (event) {
            if (event.which == 8 || event.which == 0) {
                return true;
            }
            if (event.which < 46 || event.which > 59) {
                return false;
                //event.preventDefault();
            } // prevent if not number/dot

            if (event.which == 46 && $(this).val().indexOf('.') != -1) {
                return false;
                //event.preventDefault();
            } // prevent if already dot
        });
        function minmax(value, min, max) {
            if (parseFloat(value) < min || isNaN(value))
                return 0;
            else if (parseFloat(value) > max)
                return 100;
            else return value;
        }
        function updocClick(docid, desc, filepath) {
            $('#hdnintdocIdelete').val($(this).children('#hdnintdocIdelet').val());
            $('#hdnstrdocDescriptiondele').val($(this).children('#hdnstrdocDescriptiondel').val());
            $('#hdnfilestrFilePathe').val($(this).children('#hdnfilestrFilePath').val());
        }
        $(document).ready(function () {

            $('.name-valid').on('keypress', function (e) {
                var regex = new RegExp("^[a-zA-Z 0-9]*$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });
            radioButton();
            $("span.ediDel").click(function () {
                $('#hdnintPostId').val($(this).children('#hdnintPostIdelet').val());
                $('#hdnstrPostDescriptiondele').val($(this).children('#hdnstrPostDescriptiondel').val());
            });

            $("span.ediDelc").click(function () {
                $('#hdnintPostceIdelet').val($(this).children('#hdnintPostcIdelet').val());
                $('#hdnstrPostDescriptioncedel').val($(this).children('#hdnstrPostDescriptioncdel').val());
            });

            $("#updoc").click(function () {
                $('#hdnintdocIdelete').val($(this).children('#hdnintdocIdelet').val());
                $('#hdnstrdocDescriptiondele').val($(this).children('#hdnstrdocDescriptiondel').val());
                $('#hdnfilestrFilePathe').val($(this).children('#hdnfilestrFilePath').val());
            });

            $("span.ediDelwork").click(function () {
                $('#hdnintworkeIdelet').val($(this).children('#hdnintworkIdelet').val());
                $('#hdnstrworkeDescriptiondel').val($(this).children('#hdnstrworkDescriptiondel').val());
            });

            $("span.updaEDU").click(function () {
                $('#hdnintedueIdelet').val($(this).children('#hdninteduIdelet').val());
                $('#hdnstredueDescriptiondel').val($(this).children('#hdnstreduDescriptiondel').val());
            });

            $("span.updaAchi").click(function () {
                $('#hdnintacheIdelet').val($(this).children('#hdnintachIdelet').val());
                $('#hdnstracheDescriptiondel').val($(this).children('#hdnstrachDescriptiondel').val());
            });


            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                $('.name-valid').on('keypress', function (e) {
                    var regex = new RegExp("^[a-zA-Z 0-9]*$");
                    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                    if (regex.test(str)) {
                        return true;
                    }
                    e.preventDefault();
                    return false;
                });
                radioButton();
                $("span.ediDel").click(function () {
                    $('#hdnintPostId').val($(this).children('#hdnintPostIdelet').val());
                    $('#hdnstrPostDescriptiondele').val($(this).children('#hdnstrPostDescriptiondel').val());
                });

                $("span.ediDelc").click(function () {
                    $('#hdnintPostceIdelet').val($(this).children('#hdnintPostcIdelet').val());
                    $('#hdnstrPostDescriptioncedel').val($(this).children('#hdnstrPostDescriptioncdel').val());
                });

                $("#updoc").click(function () {
                    $('#hdnintdocIdelete').val($(this).children('#hdnintdocIdelet').val());
                    $('#hdnstrdocDescriptiondele').val($(this).children('#hdnstrdocDescriptiondel').val());
                    $('#hdnfilestrFilePathe').val($(this).children('#hdnfilestrFilePath').val());
                });

                $("span.ediDelwork").click(function () {
                    $('#hdnintworkeIdelet').val($(this).children('#hdnintworkIdelet').val());
                    $('#hdnstrworkeDescriptiondel').val($(this).children('#hdnstrworkDescriptiondel').val());
                });

                $("span.updaEDU").click(function () {
                    $('#hdnintedueIdelet').val($(this).children('#hdninteduIdelet').val());
                    $('#hdnstredueDescriptiondel').val($(this).children('#hdnstreduDescriptiondel').val());
                });

                $("span.updaAchi").click(function () {
                    $('#hdnintacheIdelet').val($(this).children('#hdnintachIdelet').val());
                    $('#hdnstracheDescriptiondel').val($(this).children('#hdnstrachDescriptiondel').val());
                });
            });
        });
        function divCancels() {
            $('#hdnintPostId').val('');
            $('#hdnintPostceIdelet').val('');
            $('#hdnintacheIdelet').val('');
            $('#hdnintedueIdelet').val('');
            $('#hdnintdocIdelete').val('');
            $('#divDeletesucess').css("display", "none");
            $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 0px #00B7E5");
            $('#divDeletesucess').hide();
        }

        //function TImeOut() {

        //    setTimeout(function () {  }, 0);
        //}

        function ddlTestClientClick(sender) {
            var ddl = $(sender);
            // check to see if our dropdown doesn't already has values
            // because we don't want to bind the dropdown list every time it's clicked
            // or the user will not be able to select anything in it 
            if (!ddl.find('option').length) {
                // This will make the form postback with the clientID of the dropdownlist
                __doPostBack(sender.attr('id'), '');
            }
        }
    </script>
    <div id="divSuccess" runat="server" class="modal backgroundoverlay" clientidmode="Static" style="display: none;">
     <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
       <div class="modal-header">
        <h5 class="modal-title">Success  
        </h5>
       </div>
       <div class="modal-body">
        <asp:Label ID="lblSuccess" runat="server" Text="Profile image updated successfully."> </asp:Label>
       </div>   
       <div class="modal-footer border-top-0">
        <asp:Button ClientIDMode="Static" class="btn btn-primary add-scroller" id="btnOk" runat="server" Text="Ok" onClick="btnSuccess_Click"></asp:Button><%--OnClick="btnOk_click"--%>
       </div>
      </div>
     </div>
    </div>
    <!---Sucess Popup-->

    <!---Score Popup-->
    <div id="divScorePopup" runat="server" class="modal backgroundoverlay" clientidmode="Static" style="display: none;">
     <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
       <div class="modal-header">
        <h5 class="modal-title">Skorkel Score – Algorithm 
        </h5>
       </div>
       <div class="modal-body score-main">
        

        
        <p><b>Your Skorkel Score (Max: 100 points) has 4 constituents:</b></p>
            <ul class="score-category">
              <li>(College where are you studying) = 20 Points</li>
              <li>(CGPA - As Class/Batch Percentile) = 40 Points</li>
              <li>(Extra Curricular Achievements) = 38 Points</li>
              <li>(Contributions to Skorkel Platform) = 2 Points</li>
            </ul>
        <ul class="score-info">    
        <li>(College): Tier 1 = 20 Points; Tier 2 = 17.5 Points; Tier 3 = 15 Points; Tier 4 = 10 Points </li>
        <li>(CGPA/Class Rank):  Class Rank Expressed as a Percentile x 0.4 = Points out of 40</li>
        <li>Extra Curricular Achievements: Summation of Points from Moots, Debates, ADR Competitions, Publications, Committee Membership (Expressed as a dynamic percentile WRT All users having a Skorkel Score) x 0.38 = Points out of 40</li>
        <ul>
           <li>Moots: Participation (Tier 1 = 50, Tier 2 = 25, Tier 3 = 15); Winners (Participation Point x 5); Runners-up / Best Memo / Best Speaker (Participation Point x 3)</li>

            <li>Debates: Participation (Tier 1 = 10, Tier 2 = 5, Tier 3 = 2.5); Winners (Participation Point x 5); Runners-up (Participation Points x 3); Break (Participation Points x 2)</li>

            <li>ADR Competitions: Participation (Tier 1 = 10, Tier 2 = 5, Tier 3 = 2.5); Winners (Participation Point x 5); Runners-up (Participation Points x 3)</li>

            <li>Publications/ Conference Papers: Tier 1 = 125; Tier 2 = 50</li>

            <li>Committee Membership: Membership (10 Points); Leadership Position - President/VP/Convenor/Joint Convenor (5 Points); committee points subject to a maximum of 15 Points in total</li>
             
            <li>Student Body President / Vice - President - 50 Points</li>
        </ul>

        <li>Contributions to Skorkel Platform: Summation of Points - (Expressed as a dynamic percentile WRT All users having a Skorkel Score) x 0.02 = Points out of 2</li>
        <ul>
             <li>
            Case Annotations: Facts / Issue / Obiter Dictum / Important Paragraph = 10 Points
            <li>Ratio Decidendi = 20 Points,</li>
            <li></b>Clarification:</b></li>
            <li>Judgment Length < 2000 words (i.e. roughly 4 pages) = No Points</li>
            <li>2000 < Judgment Length < 12000 words = Base Points</li>
            <li>12000 < Judgment Length < 24000 words = Base Points x 1.5</li>
            <li>24000 < Judgment Length = Base Points x 2</li>
             
            <li>Summary/Headnote = 40 Points</li>
            <li><b>Clarification:</b></li>
            <li>Judgment Length < 2000 words (i.e. roughly 4 pages) = No Points</li>
            <li>2000 < Judgment Length < 12000 words = Base Points</li>
            <li>12000 < Judgment Length < 24000 words = Base Points x 1.5</li>
            <li>24000 < Judgment Length = Base Points x 2</li>
            <li>No points given if summary/headnote is less than 100 words</li>
             
            <li>Ask Question in QA Section = 2.5 Points</li>
             
            <li>Answer Question in QA Section = 10 Points</li>
             
            <li>Write Blog (Minimum 200 Words) = 40 Points</li>
             
            <li>For each set of 20 upvotes across every contribution above= Base Point x 3</li>
                
            <li>Submit Article (on pre-released limited set of topics, minimum 200 words) = 40 Points</li>
             
            <li>Selection of Article for Publication in AILSJ (All India Law Students Journal) = 200 Points </li>
      </ul> 
     </ul>   
        <p>*Since scores are dynamic and relative, they are updated once every day - at 4 AM.</li>
        <p>*All achievements/credentials/academic particulars shall be verified by Skorkel - Users must upload all necessary supporting documents onto their Skorkel Portal (“Upload Documents” button on “Skorkel Score” Section). Any user found to be posting falsified claims shall be permanently suspended and disbarred from using the platform.</p>

       </div>   
       <div class="modal-footer border-top-0">
        <asp:Button ClientIDMode="Static" class="btn btn-primary add-scroller" id="btnScore" runat="server" Text="Ok" onClientClick="$('#divScorePopup').hide(); return false;"></asp:Button><%--OnClick="btnOk_click"--%>
       </div>
      </div>
     </div>
    </div>

    <!---Sucess Popup-->
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
                                OnClientClick="javascript:divCancels();return false;"></asp:Button>
                            <asp:Button ID="lnkDeleteConfirm" runat="server" ClientIDMode="Static" Text="Yes" OnClientClick="javascript:divdeletes();"
                                CssClass="btn btn-primary success-popup" OnClick="lnkDeleteConfirm_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>
        $(document).ready(function () {
            $('.about-us').click(function () {
                $('.about-us-page').addClass('display-blockk');
            });
            $('.privacy-policy').click(function () {
                $('.privacy-policy-page').addClass('display-blockk');
            });
            $('.terms-services').click(function () {
                $('.terms-services-page').addClass('display-blockk');
            });

            $('.close-page-popup').click(function () {
                $('.about-us-page, .privacy-policy-page, .terms-services-page').removeClass('display-blockk');
            });

        });
    </script>
</asp:Content>
