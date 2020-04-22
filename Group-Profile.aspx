<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Group-Profile.aspx.cs" MasterPageFile="~/Main.master"
    Inherits="Group_Profile" %>

<%@ Register Src="~/UserControl/Groups.ascx" TagName="GroupDetails" TagPrefix="Group" %>
<%@ Register Src="~/UserControl/Groups-m.ascx" TagName="GroupDetailsM" TagPrefix="GroupM" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
    <script src="<%=ResolveUrl("js/croppie.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("js/jquery.jcarousel.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("js/jquery.custom-scrollbar.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/chosen.jquery.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("docsupport/prism.js")%>" type="text/javascript"></script>
    <link href="css/croppie.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnFileData" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnPhotoName" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnAutoreqJoin" ClientIDMode="Static" runat="server" />

        <div class="main-section-inner">
            <div id="divGroupSucces" class="modal_bg" runat="server" style="display: none;" clientidmode="Static">
                <div class="modal-dialog">
                    <div class="modal-header">
                        <strong>&nbsp;&nbsp;
                     <asp:Label ID="lblSuccMess" runat="server" Text="Updated Succesfully."></asp:Label>
                        </strong>
                    </div>
                    <div class="modal-footer">
                        <a clientidmode="Static" class="cancel_btn margin_right_l" onclick="javascript:MesProfileClose();">Close </a>
                    </div>
                </div>
            </div>
            <!--left box starts-->
            <div id="divheight" runat="server" class="panel-cover clearfix">
                <div id="divSecondWall" runat="server" clientidmode="Static" class="width_h">
                    <!---Center Panel-->
                    <div class="center-panel">
                        <div class="custom-nav-con group-page-tab">
                            <!---Tabs Name Start-->
                            <GroupM:GroupDetailsM ID="grpDetailsM" runat="server" />
                            <ul class="custom-nav-control nav nav-tabs " id="ulGroupMenu" runat="server" ClientIDMode="Static">
                                <li class="nav-item">
                                    <asp:LinkButton ID="lnkProfile" runat="server" Text="Profile" ClientIDMode="Static"
                                        class="forumstabAcitve nav-link" OnClick="lnkProfile_Click"></asp:LinkButton>
                                </li>
                                <li id="DivHome" runat="server" class="nav-item">

                                    <asp:LinkButton ID="lnkHome" runat="server" Text="Wall" ClientIDMode="Static" OnClick="lnkHome_Click" class="nav-link"></asp:LinkButton>

                                </li>
                                <li id="DivForumTab" runat="server" clientidmode="Static" class="nav-item">

                                    <asp:LinkButton ID="lnkForumTab" runat="server" Text="Forums" ClientIDMode="Static"
                                        OnClick="lnkForumTab_Click" class="nav-link"></asp:LinkButton>

                                </li>
                                <li id="DivUploadTab" runat="server" clientidmode="Static" class="nav-item">

                                    <asp:LinkButton ID="lnkUploadTab" runat="server" Text="Uploads" ClientIDMode="Static"
                                        OnClick="lnkUploadTab_Click" class="nav-link"></asp:LinkButton>

                                </li>
                                <li id="DivPollTab" runat="server" clientidmode="Static" class="nav-item">

                                    <asp:LinkButton ID="lnkPollTab" runat="server" Text="Polls" ClientIDMode="Static"
                                        OnClick="lnkPollTab_Click" class="nav-link"></asp:LinkButton>

                                </li>
                                <li id="DivEventTab" runat="server" clientidmode="Static" class="nav-item">

                                    <asp:LinkButton ID="lnkEventTab" runat="server" Text="Events" ClientIDMode="Static"
                                        OnClick="lnkEventTab_Click" class="nav-link"></asp:LinkButton>

                                </li>
                                <li id="DivMemberTab" runat="server" clientidmode="Static" class="nav-item">

                                    <asp:LinkButton ID="lnkMemberTab" runat="server" Text="Members" ClientIDMode="Static"
                                        OnClick="lnkEventMemberTab_Click" class="nav-link"></asp:LinkButton>

                                </li>
                            </ul>

                            <!---Tabs Name Ended-->

                            <!---Details Container-->
                            <div class="tab-content m-t-15">
                                <div class="custom-nav-container tab-pane container active">
                                    <div class="card-list-con">
                                        <div class="card top-list">
                                            <div class="post-con">
                                                <div class="post-body p-b-15">
                                                    <!---Edit and Delete Buttons dot net code-->
                                                    <!--           <span class="more-btn float-right">
                                             <span class="dropdown">
                                                      <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                          <img src="images/more.svg" alt="" class="more-btn">
                                                      </a>
                                                      <span class="dropdown-menu" aria-labelledby="dropdownMenuLink" x-placement="bottom-start">
                                                         <asp:Label ID="lblGroupCategory" runat="server" Text=""></asp:Label>
                                                         <asp:LinkButton ID="lnkEdit" CssClass="dropdown-item educationYear education_edit" ClientIDMode="Static" S
                                                            Text="Edit" OnClick="lnkEdit_Click" runat="server">
                                                            <span class="lnr lnr-pencil"></span> Edit
                                                         </asp:LinkButton>
                                                        </span>
                                                </span>
                                          </span> -->
                                                    <!---Profile Info Start-->
                                                    <div id="divMainProfile" class="whitebg panel-body" runat="server">
                                                        <h3>
                                                            <asp:Label ID="lblTitle" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                                                        </h3>
                                                        <p>
                                                            <asp:Label ID="lblDescription" runat="server" ClientIDMode="Static"
                                                                Text=""></asp:Label>
                                                        </p>
                                                        <div class="chip-cover m-t-10 m-b-5" id="divlstProfSubjCategory" runat="server">
                                                            <asp:ListView ID="LstProfSubjCategory" runat="server" OnItemDataBound="LstSubjCategory_ItemDataBound"
                                                                GroupItemCount="3" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1">
                                                                <LayoutTemplate>
                                                                    <table class="widtth_sixtypercent">
                                                                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <GroupTemplate>
                                                                    <tr>
                                                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                                                    </tr>
                                                                </GroupTemplate>
                                                                <ItemTemplate>
                                                                    <div class="chip" id="SubLi" runat="server">
                                                                        <asp:HiddenField ID="hdnCountSub" runat="server" Value='<%#Eval("CountSub")%>' />
                                                                        <asp:HiddenField ID="hdnSubCatId" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                                        <asp:LinkButton ID="lnkCatName" ForeColor="#646161" Font-Underline="false" runat="server"
                                                                            Text='<%#Eval("strCategoryName")%>' class="un-anchor" CommandName="Subject Category"></asp:LinkButton>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                         
                                                        <p>
                                                            <strong>Access: </strong>
                                                            <asp:Label ID="rbProfileJoin" runat="server" ClientIDMode="Static">
                                                            </asp:Label>
                                                        </p>
                                                    </div>
                                                    <!---Profile Info Ended-->
                                                    <!---Profile Edit Start-->
                                                    <asp:UpdatePanel ID="pnlGeneral" runat="server" Visible="true">
                                                        <ContentTemplate>
                                                        <div id="divProfileTitle" runat="server">
                                                            <div class="createForumBox members padding-0 Mem profileSection global-form">
                                                                <div class="card-body padding-left-0 padding-top-0">
                                                                    <h5 class="card-title">Update Group
                                                                    </h5>
                                                                    <div class="media-body media-middle">
                                                                        <div class="form-group">
                                                                            <label for="textbox">Group Name</label>
                                                                            <asp:TextBox autocomplete="off" ID="txtTitle" MaxLength="50" Enabled="False" runat="server" ClientIDMode="Static"
                                                                                CssClass="uploadTxtField form-control profile" placeholder="Title/Name"></asp:TextBox>
                                                                        </div>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" class="please_enter_group" runat="server" ControlToValidate="txtTitle" Display="Dynamic"
                                                                            ValidationGroup="cg" ErrorMessage="Please enter group name" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        <div class="form-group">
                                                                            <label>Group Description</label>
                                                                            <textarea rows="10" disabled="disabled" class="uploadDescriptionTxt form-control profile" id="txtPurpose"
                                                                                cols="0" runat="server" onfocus="if(this.value=='Description') this.value='';" maxlength="500"
                                                                                onblur="if(this.value=='') this.value='Description';" value="Description"></textarea>
                                                                        </div>
                                                                        <asp:HiddenField ID="hdnTitle" runat="server" />
                                                                        <!--search result starts-->
                                                                        <div class="form-group">
                                                                            <label>
                                                                                Context
                                                                            </label>

                                                                            <div class="categoryBox">
                                                                                <asp:UpdatePanel ID="UpdateSub" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <uc:UserControl_MultiSelect class="form-control" ID="LstSubjCategory" ClientIDMode="Static" runat="server" />
                                                                                        <div class="cls">
                                                                                        </div>

                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>




                                                                        <div class="form-group">
                                                                            <label class="" for="exampleCheck1">Members</label>
                                                                            <uc:UserControl_MultiSelect class="form-control" ID="txtInviteMember" ClientIDMode="Static" runat="server" />
                                                                            <%--  <select data-placeholder="Enter members name here" class="chosen-select MySkorkeltxtFieldselect divnewGroupDescp form-control"
                                       id="txtInviteMember" onchange="getAllMemberValue(this.id)" runat="server" clientidmode="Static"
                                       multiple  tabindex="4">
                                    </select>--%>
                                                                            <%--    <asp:RequiredFieldValidator InitialValue="Enter members name here" ValidationGroup="cgroup"
                                       ClientIDMode="Static" ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtInviteMember"
                                       ErrorMessage="Enter members name" ForeColor="Red" class="members_name"></asp:RequiredFieldValidator>--%>
                                                                            <asp:HiddenField ID="hdnMembId" ClientIDMode="Static" runat="server" />
                                                                        </div>
                                                                        <asp:UpdatePanel ID="idd" runat="server">
                                                                            <ContentTemplate>
                                                                                <div class="form-group">
                                                                                         <label for="Access">Access</label>
                                                                                    <br />
                                                                                    <ul class="list-inline d-sm-block"> 
                                                                               
                                                                                    <li class="list-inline-item inline-block">
                                                                                        <span class="custom-radio">
                                                                                            <asp:ImageButton ID="imgAutojoin" class="input-align-a" runat="server" ClientIDMode="Static" OnClientClick="checkimages();return false" /><label>&nbsp;&nbsp;Auto Join</label>
                                                                                        </span>
                                                                                    
                                                                                    </li>
                                                                                    <li class="list-inline-item inline-block">
                                                                                        <span class="custom-radio">
                                                                                            <asp:ImageButton ID="imgReqjoin" runat="server" class="input-align-a" ClientIDMode="Static" OnClientClick="checkremoveimages();return false;" />
                                                                                            <label>&nbsp;&nbsp;Request to Join</label>
                                                                                        </span>
                                                                                    </li>    
                                                                                    </ul>
                                                                                </div>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="imgAutojoin" EventName="Click" />
                                                                                <asp:AsyncPostBackTrigger ControlID="imgReqjoin" EventName="Click" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                        <!--search result ends-->
                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="cropper-modal modal" style="position: fixed; top: 0; right: 0; bottom: 0; left: 0; z-index: 1050; display: none; outline: 0; background: #000000de;">
                                                                    <div class="modal-dialog modal-dialog-centered"">
                                                                        <div class="modal-content">
                                                                            <div class="modal-header">
                                                                                <h5 class="modal-title" id="exampleModalLabel">Change Image</h5>
                                                                                <button type="button" class="close">
                                                                                    <span onclick="CloseImage()" aria-hidden="true">&times;</span>
                                                                                </button>
                                                                            </div>

                                                                            <div class="modal-body">
                                                                                <div class="custom-modal">
                                                                                    <div id="cropper-wrapper" class="" style="position: relative; height: 500px; width: 450px; overflow: hidden; margin: 0 auto;">
                                                                                        <div class="" style="position: absolute; width: 100%; height: 100%; left: 0; top: 0; box-shadow: 0 0 8px 2px rgba(100, 100, 100, .4);">
                                                                                            <div id="cropContainer1"></div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="submit-con">
                                                                                    <a href="javascript:void(0);" id="cnclImage" onclick="javascript:CloseImage(); return false" class="btn btn-outline-primary m-r-15">Cancel</a>
                                                                                    <a  href="javascript:void(0);" id="svImage" class="btn btn-primary resize-done">Save</a>
                                                                                </div>

                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div style="display: none">
                                                                    <input name="upload" onchange="ShowCropper.bind(this)();" type="file" id="fileinput" />
                                                                </div>
                                                                <div class="img-con padding-top-0 padding-right-0">
                                                                    <div>
                                                                        <asp:Label ID="lblSuccMessage" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div class="img-cover">
                                                                        <img id="imgUser" runat="server" clientidmode="Static" class="user-img" />
                                                                        <asp:LinkButton ID="lnkImageEdit" CssClass="img-cropper-trigger" runat="server" ToolTip="Change Profile Image"
                                                                            ClientIDMode="Static" OnClientClick="javascript:CallCameraload(); return false;">
                                      <span class="lnr lnr-camera"></span>
                                                                        </asp:LinkButton>
                                                                        <%-- <a href="#" class="img-cropper-trigger" data-toggle="modal" data-target="#imgCropper">
                                      <span class="lnr lnr-camera"></span>
                                  </a>--%>
                                                                        <asp:ImageButton ID="ImgRemovePic" ToolTip="Remove Profile Picture." class="delete_cross" runat="server" Style="display: none;" ClientIDMode="Static" OnClientClick="removeimages();return false;"
                                                                            ImageUrl="images/Delete.gif" />
                                                                    </div>


                                                                    <%--     <div class="img-cover">
                                                                        <img id="imgUser" runat="server" clientidmode="Static" alt="" class="user-img" />
                                                                        <a href="#" class="img-cropper-trigger" data-toggle="modal" data-target="#imgCropper">
                                                                            <span class="lnr lnr-camera"></span>
                                                                        </a>
                                                                    </div>
                                                                    <asp:ImageButton ID="ImgRemovePic" ToolTip="Remove Profile Picture." runat="server"
                                                                        Visible="true" class="remove_img" CausesValidation="false"
                                                                        ClientIDMode="Static" OnClientClick="removeimages();return false;" ImageUrl="images/Delete.gif" />
                                                                    <div class="file">
                                                                        <label for="file-upload" class="custom-file-upload">
                                                                            Add Group Picture
                                                                        </label>
                                                                        <asp:FileUpload ID="FileUpload1" ClientIDMode="Static" runat="server" CompleteBackColor="#FFFFFF" />
                                                                    </div>
                                                                    <asp:LinkButton ID="uploadimage" runat="server" ClientIDMode="Static" Text="Upload"
                                                                        OnClick="fileuplad_onload" CssClass="connect" class="file_upload"></asp:LinkButton>--%>
                                                                </div>
                                                                <div class="clearfix"></div>
                                                            </div>
                                                            <!---Cancel and Update buttons-->
                                                            <div id="divSaveCancel" class="submit-con margin-bottom-10" runat="server">
                                                                <asp:LinkButton ID="btnCancelExperience" CommandName="Join" CausesValidation="false" runat="server" Text="Cancel" class="btn btn-outline-primary m-r-15"
                                                                    OnClick="btnCancel_Click"></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkCreateGroup" Text="Update" runat="server" ClientIDMode="Static"
                                                                    OnClientClick="javascript:CallEditSavegr();" ValidationGroup="cg" OnClick="btnSaveGroup_Click"
                                                                    class="btn btn-primary"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                            </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <!---Profile Edit Ended-->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!---Details Container Ended-->
                    </div>
                    <!---Center Panel Ended-->
                    <!---Right Profile Panel-->
                   
                        <Group:GroupDetails ID="grpDetails" runat="server" />
                   
                    <!---Right Profile Panel Ended-->

                </div>
            </div>
            <asp:HiddenField ID="hdnMemberName" runat="server" ClientIDMode="Static" Value="" />
            <asp:HiddenField ID="hdnMemberId" runat="server" ClientIDMode="Static" Value="" />
            <!--box starts-->
  
        <script type="text/javascript">

            function clearCropper() {
                //debugger;
                if (typeof $uploadCrop != 'undefined')
                {
                    $uploadCrop.croppie('destroy');
                }
                $('#hdnFileData').val('');
                $('#hdnPhotoName').val('');
            }

            function CloseImage() {
                $(".cropper-modal").hide();
                $("#fileinput").val('');
            }
            function ShowCropper() {
                if (this.value) {
                    $('#svImage').css('pointer-events', 'none');
                    url = this.files[0];

                    var allowedFileTypes = ['jpg', 'jpeg', 'png'];
                    $("#hdnErrorMsg").val('');
                    $("#hdnPhotoPath").val('');
                    $("#hdnPhotoName").val(this.files[0].name);
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
                                //     $.ajax({
                                //type: "POST",
                                //url: "handler/ProfileImageHandler.ashx",
                                //contentType: false,
                                //processData: false,
                                //data: data,
                                //success: function (result) {
                                //    if (result == 'Please select image.') {
                                //        $("#lblmsg").text(result);
                                //        $("#hdnErrorMsg").val(result)
                                //       // CallUploadimg();
                                //         $(".cropper-modal").hide();
                                //        showSuccessPopup("Error", result);
                                //           $("#fileinput").val('');

                                //    }
                                //    else {
                                //        var v = result.split(":");
                                //        $("#hdnPhotoPath").val(v[0]);
                                //        $("#hdnPhotoName").val(v[1]);

                                //    }
                                //},
                                //error: function () {
                                //     $(".cropper-modal").hide();
                                //    showSuccessPopup('Error', "There was an error while selecting File!");
                                //       $("#fileinput").val('');
                                //}
                                //    });
                                if (typeof $uploadCrop != 'undefined') {
                                    $uploadCrop.croppie('destroy');
                                }
                                $(".cropper-modal").show();
                                $('#svImage').css('pointer-events', 'auto');

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
                                        $("#imgUser").attr('src', resp);
                                        $(".cropper-modal").hide();
                                    });
                                });
                            }
                        };
                        img.src = fr.result; // is the data URL because called with readAsDataURL
                    };
                }
            }
            function CallCameraload() {
                $('#fileinput').trigger('click');
            }
            $(document).ready(function () {
                clearCropper();
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    clearCropper();
                    $("#imgAutojoin").attr('src', 'images/checked2.png');
                    $("#imgReqjoin").attr('src', 'images/unchecked2.png');
                });

            });
            function checkimages() {
               
                    $("#hdnAutoreqJoin").val('1');
                    $("#imgAutojoin").attr('src', 'images/checked2.png');
                    $("#imgReqjoin").attr('src', 'images/unchecked2.png');  
            }
            function checkremoveimages() {
               
                    $("#hdnAutoreqJoin").val('2');
                    $("#imgReqjoin").attr('src', 'images/checked2.png');
                    $("#imgAutojoin").attr('src', 'images/unchecked2.png');
            }
            $(document).ready(function () {
                
                $("#FileUpload1").change(function (event) {
                    var tmppath = URL.createObjectURL(event.target.files[0]);
                    var ext = $('#FileUpload1').val().split('.').pop().toLowerCase();
                    if (ext != '') {
                        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp']) == -1) {
                            if (ext == 'pdf' || ext == 'xlxs' || ext == 'txt' || ext == 'doc' || ext == 'docx' || ext == 'xlx' || ext == 'odt') {
                                alert('Please select image.');
                            } else {
                                alert('Please select image.');
                            }
                        } else {
                            $("#imgUser").attr('src', URL.createObjectURL(event.target.files[0]));
                            $("#ImgRemovePic").css("display", "block");
                        }
                    } else {
                        alert('Please select image.');
                    }
                });
                $(document).on('click', '.mobile_tab_icon', function () {
                    $('ul.group_p').slideToggle('slow');
                });

            });
            function removeimages() {
                $("#imgUser").attr('src', 'images/groupPhoto.jpg');
                $('#FileUpload1').val('');
                $("#ImgRemovePic").css("display", "none");
                return false;
            }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $(".categoryTxt ul li").click(function () {
                    $(this).toggleClass("gray");
                });
                $(".viewAll a").click(function () {
                    $(this).parent().parent().toggleClass("categoryBoxToggle");
                    var abc = this.text;
                    if (abc == "Close") {
                        $(this).text("View all");
                    }
                    if (abc == "View all") {
                        $(this).text("Close");
                    }
                });
            });
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $(":input[data-watermark]").each(function () {
                    $(this).val($(this).attr("data-watermark"));
                    $(this).bind('focus', function () {
                        if ($(this).val() == $(this).attr("data-watermark")) $(this).val('');
                    });
                    $(this).bind('blur', function () {
                        if ($(this).val() == '') $(this).val($(this).attr("data-watermark"));
                        $(this).css('color', '#a8a8a8');
                    });
                });
            });
        </script>
        <script type="text/javascript">
            function ShowLoading(id) {
                location.href = '#' + id;
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
        </script>
        <script type="text/javascript">
            function MesProfileClose() {
                document.getElementById("divGroupSucces").style.display = 'none';
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
            $(document).ready(function () {
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
            });
        </script>
        <script type="text/javascript">
            function getAllMemberValue(ctrlId) {
                $('#tdDepartments').find('label.error').remove();
                var control = document.getElementById(ctrlId);
                var strSelTexts = '';
                var cnt = 0;
                for (var i = 0; i < control.length; i++) {
                    if (control.options[i].selected) {
                        if (cnt == 0) {
                            strSelTexts += control.options[i].value;
                        }
                        else {
                            strSelTexts += ',' + control.options[i].value;
                        }
                        cnt++;
                    }
                }
                $('#hdnMembId').val(strSelTexts);
            }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                var ID = "#" + $("#hdnTabId").val();
                $(ID).focus();
            });
            function ShowLoading(id) {
                location.href = '#' + id;
            }
            function CallEditSavegr() {
                $('#lnkCreateGroup').css("box-shadow", "0px 0px 5px #00B7E5");
                if ($('#txtTitle').text().trim() == '') {
                    setTimeout(
                        function () {
                            $('#lnkCreateGroup').css("box-shadow", "0px 0px 0px #00B7E5");
                        }, 1000);
                }
            }
        </script>

</asp:Content>
