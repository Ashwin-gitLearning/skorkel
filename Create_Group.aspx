<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"  CodeFile="Create_Group.aspx.cs" Inherits="Create_Group" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
<%@ Register Src="~/UserControl/CropImg.ascx" TagName="CropImage" TagPrefix="crp1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
   <script src="<%=ResolveUrl("docsupport/chosen.jquery.min.js")%>" type="text/javascript"></script>
     <script src="<%=ResolveUrl("js/croppie.min.js")%>" type="text/javascript"></script>
   <script type="text/javascript" src="<%=ResolveUrl("js/ddsmoothmenu.js")%>"></script>
    <link href="css/croppie.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <asp:HiddenField ID="hdnFileData" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnPhotoName" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="dfgg" runat="server"><ContentTemplate>
    <div id="divDeletesucess" runat="server" class="modal backgroundoverlay show" style="display: none;" role="dialog" aria-labelledby="confirmationTitle">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 id="successPopupTitle" class="modal-title">Success</h5>
                </div>
                <div class="modal-body">
                    <span id="successPopupMsg">Group created successfully.</span>
                </div>
                <div class="border-top-0 text-right padding-15">
                    <a class="btn btn-primary add-scroller" onclick="hideSsPopup();" data-dismiss="modal" href="#">OK</a>
                </div>
            </div>
            <!-- modal-content ended -->
        </div>
    </div>
    </ContentTemplate></asp:UpdatePanel>
      <div class="main-section-inner">
         <div class="panel-cover clearfix">
            <div class="innerSignUpTabs create_grp">
   
            
              
               <div class="cls">
               </div>
               <!--Center Panel Start-->
                <asp:UpdatePanel ID="test" ClientIDMode="Static" runat="server"><ContentTemplate>
               <div>
                    <div class="card"> 
                        <div class="global-form">
                       
                           <div class="card-body">
                              <h5 class="card-title">
                               New Group
                              </h5>
                               <div class="media-body media-middle">
                                 <div class="form-group">
                                    <label for="textbox">Group Name</label>
                                    <asp:TextBox ID="txtCreateGr" MaxLength="100" autocomplete="off" runat="server" ClientIDMode="Static" class="form-control"
                                       placeholder="Enter Group Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCreateGr"
                                       Display="Dynamic" ValidationGroup="cgroup" ErrorMessage="Please enter Group Name"
                                       ForeColor="Red"></asp:RequiredFieldValidator>
                                 </div>
                                 <div class="form-group">
                                    <label for="textarea">Description</label>
                                    <textarea maxlength="500" class="newGroupDescp form-control" id="txtPurpose" runat="server" placeholder="Tell me about group"></textarea>
                                 </div>
                                 <div class="form-group">
                                        <label class="" for="exampleCheck1">Context</label>
                                      <uc:UserControl_MultiSelect class="form-control" ID="lstCreateGroup" ClientIDMode="Static" runat="server"  />
                                       <%-- <input type="text" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Select Your Context"> --%>
                                 </div>
                                 <asp:HiddenField ID="hdnSubject" ClientIDMode="Static" runat="server" />
                              <%--   <asp:UpdatePanel ID="hhd" runat="server">
                                    <ContentTemplate>
                                       <ul class="context" id="ultags">
                                          <asp:ListView ID="lstCreateGroup" runat="server" OnItemDataBound="lstCreateGroup_ItemDataBound">
                                             <ItemTemplate>
                                                <li id="SubLi" runat="server" >
                                                   <asp:HiddenField ID="hdnSubCatId" ClientIDMode="Static" runat="server" Value='<%#Eval("intCategoryId")%>' />
                                                   <asp:Label ID="lnkCatName" ClientIDMode="Static" CssClass="unselectedtagnameGroup"
                                                      runat="server" Text='<%#Eval("strCategoryName")%>' CommandName="Subject Category"></asp:Label>
                                                </li>
                                             </ItemTemplate>
                                          </asp:ListView>
                                       </ul>
                                    </ContentTemplate>
                                    <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="lstCreateGroup" />
                                    </Triggers>
                                 </asp:UpdatePanel>--%>
                                   
                                 <div class="form-group">
                                       <label class="" for="exampleCheck1">Members</label>
                                      <uc:UserControl_MultiSelect class="form-control" ID="txtInviteMember" ClientIDMode="Static" runat="server"  />
                                  <%--  <select data-placeholder="Enter members name here" class="chosen-select MySkorkeltxtFieldselect divnewGroupDescp form-control"
                                       id="txtInviteMember" onchange="getAllMemberValue(this.id)" runat="server" clientidmode="Static"
                                       multiple  tabindex="4">
                                    </select>--%>
                                <%--    <asp:RequiredFieldValidator InitialValue="Enter members name here" ValidationGroup="cgroup"
                                       ClientIDMode="Static" ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtInviteMember"
                                       ErrorMessage="Enter members name" ForeColor="Red" class="members_name"></asp:RequiredFieldValidator>--%>
                                    <asp:HiddenField ID="hdnMembId" ClientIDMode="Static" runat="server" />
                                 </div>
                                 <asp:HiddenField ID="hdnAutoreqJoin" ClientIDMode="Static" runat="server" />
                                 <asp:UpdatePanel ID="idd" runat="server">
                                    <ContentTemplate>
                                       <div class="form-group">
                                          <label for="textarea">Access</label>
                                          <br>
                                          <ul class="list-inline d-sm-block">
                                            <li class="list-inline-item">
                                              <span class="custom-radio">
                                                <asp:ImageButton ID="imgAutojoin" runat="server" ClientIDMode="Static" class="input-align-a" OnClientClick="checkimages();return false;" />
                                            <label>&nbsp;&nbsp;Auto Join</label>
                                              </span>
                                            </li>  
                                            <li class="list-inline-item">
                                              <span class="custom-radio">
                                               <asp:ImageButton ID="imgReqjoin" runat="server" ClientIDMode="Static" class="input-align-a" OnClientClick="checkremoveimages();return false;" />
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
                              </div>
                           </div>
                            <div class="cropper-modal modal backgroundoverlay">
                       <div class="modal-dialog modal-dialog-centered"" >
                           <div class="modal-content">
                              <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Change Image</h5>
                                      <button type="button" class="close">
                                              <span onclick="CloseImage()" aria-hidden="true">&times;</span>
                                      </button>
                                  </div>
                                                                    
                                    <div class="modal-body">
                                         <div class="custom-modal">
                                          <div id="cropper-wrapper" class="" style="position: relative;height: 500px;  width: 450px; overflow: hidden; margin: 0 auto;">
                                            <div class="" style="position: absolute; width: 100%; height: 100%; left: 0; top: 0; box-shadow: 0 0 8px 2px rgba(100, 100, 100, .4);">
                                                <div id="cropContainer1"></div>
                                            </div>
                                          </div>
                                       </div>
                                        <div class="submit-con">
                                            <a  id="cnclImage" href="javascript:void(0);" onclick="javascript:CloseImage(); return false" class="btn btn-outline-primary m-r-15 add-scroller">Cancel</a>
                                            <a id="svImage" class="btn btn-primary resize-done add-scroller">Save</a>
                                         </div>
                                                                       
                                     </div>

                                   </div>
                              </div>
                         </div>
                               <div style="display: none">
                   <input name="upload" onchange="ShowCropper.bind(this)();" type="file" id="fileinput" />
               </div>
                           <!---Impage Upload Section Start-->
                           <div class="img-con">
                                <div class="img-cover">                        
                                    <img id="imgGroupUser" runat="server" clientidmode="Static" class="user-img" />
                                      <asp:LinkButton ID="lnkImageEdit" CssClass="img-cropper-trigger hide-body-scroll" runat="server" ToolTip="Change Profile Image"
                              ClientIDMode="Static" OnClientClick="javascript:CallCameraload(); return false;">
                                      <span class="lnr lnr-camera"></span>
                                         </asp:LinkButton>
                                   <%-- <a href="#" class="img-cropper-trigger" data-toggle="modal" data-target="#imgCropper">
                                      <span class="lnr lnr-camera"></span>
                                  </a>--%>
                                    <asp:ImageButton ID="ImgRemovePic" ToolTip="Remove Profile Picture." class="delete_cross" runat="server" Style="display: none; " ClientIDMode="Static" OnClientClick="removeimages();return false;"
                                       ImageUrl="images/Delete.gif" />
                                </div>  
                                 <div class="file_upload fileUpload">
                                    <div>
                                       <asp:Label ID="lblSuccMess" runat="server"></asp:Label>
                                    </div>
                                 </div>
                           
                           </div>  
                           <div class="clearfix"></div>
                          
                        </div>
                         <div class="submit-con m-b-25 p-both-15">
                              <asp:LinkButton href="Home.aspx" runat="server" ID="aCancel" CssClass="btn btn-outline-primary m-r-15">Cancel</asp:LinkButton>
                              <asp:LinkButton ID="lnkCreateGroup" Text="Create Group" runat="server" ClientIDMode="Static"
                                 OnClientClick="javascript:callSavegrp();" ValidationGroup="cgroup" OnClick="btnSaveGroup_Click"
                                 class="btn btn-primary"></asp:LinkButton>
                           </div> 
                 </div>
               </div>
                    </ContentTemplate></asp:UpdatePanel>
               <!--Center Panel Ended-->
               <!---Right Panel Start-->
               <div class="right-panel-back-layer"></div>
                  
               <!---Right Panel Ended-->
               <asp:HiddenField ID="hdnTabId" runat="server" ClientIDMode="Static" />
               <asp:HiddenField ID="hdnPostId" runat="server" ClientIDMode="Static" />
               <asp:HiddenField ID="hdnLoader" runat="server" ClientIDMode="Static" />
               <asp:HiddenField ID="hdncommentfocus" runat="server" ClientIDMode="Static" />
               <!--main section ends-->
            </div>
            <div class="cls">
            </div>
            <!--left verticle search list ends-->
         </div>
      </div>

   <script type="text/javascript">
       function hideSsPopup() {
           $('#ctl00_ContentPlaceHolder1_divDeletesucess').hide();
        }
       function CallCameraload() {
           $('#fileinput').trigger('click');
       }
       function CloseImage() {
           $(".cropper-modal").hide();
           $("#fileinput").val('');
       }

       function clearCropper() {

           if (typeof $uploadCrop != 'undefined') {
                               $uploadCrop.croppie('destroy');
                           }
           $('#hdnFileData').val('');
           $('#hdnPhotoName').val('');
       }

       function ShowCropper() {
           if (this.value) {
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
                                   $("#imgGroupUser").attr('src', resp);
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
       function callBodyDisable() {
           $("body").css("position", "fixed");
           $("body").css("overflow-y", "scroll");
       }
       function callBodyEnable() {
           $("body").css("position", "static");
           $("body").css("overflow-y", "auto");
       }
   </script>
   <script type="text/javascript">
       var strSelTexts = '';
       $(document).ready(function () {
           clearCropper();
           $('ul.context li').click(function () {
               $(this).toggleClass('selectedcreateGroup graycreateGroup');
               if ($(this).hasClass("selectedcreateGroup")) {
                   $(this).children(".unselectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
               } else {
                   $(this).children(".selectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
               }
               AddSubjectCall($(this).children("#hdnSubCatId").val());
           });
           $(document).on('click', '.tab_icon', function () {
               $('#homeId').slideToggle('slow');

           });

       });

       $(document).ready(function () {
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           prm.add_endRequest(function () {
               $('ul.context li').click(function () {
                   $(this).toggleClass('selectedcreateGroup graycreateGroup');
                   if ($(this).hasClass("selectedcreateGroup")) {
                       $(this).children(".unselectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
                   } else {
                       $(this).children(".selectedtagnameGroup").toggleClass("selectedtagnameGroup unselectedtagnameGroup");
                   }
                   AddSubjectCall($(this).children("#hdnSubCatId").val());
               });

           });
       });
       function AddSubjectCall(ids) {
           var subVal = $("#hdnSubject").val();
           if (subVal == '') {
               $("#hdnSubject").val(ids);
           } else {
               strSelTexts = $("#hdnSubject").val();
               strSelTexts += ',' + ids;
               $("#hdnSubject").val(strSelTexts);
               strSelTexts = '';
           }
       }
   </script>
   <script type="text/javascript">
       function changePhoto() { }
       $(document).ready(function () {
           $("#imgAutojoin").attr('src', 'images/checked2.png');
           $("#imgReqjoin").attr('src', 'images/unchecked2.png');
           $("#hdnAutoreqJoin").val('1');
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           prm.add_endRequest(function () {
               $("#imgAutojoin").attr('src', 'images/checked2.png');
               $("#imgReqjoin").attr('src', 'images/unchecked2.png');
           });
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
                       $("#imgGroupUser").attr('src', URL.createObjectURL(event.target.files[0]));
                       $("#ImgRemovePic").css("display", "block");
                   }
               } else {
                   alert('Please select image.');
               }
           });

       });
       function removeimages() {
           $("#imgGroupUser").fadeIn("fast").attr('src', 'images/groupPhoto.jpg');
           $('#FileUpload1').val('');
           $("#ImgRemovePic").css("display", "none");
           return false;
       }
       function checkimages() {
           if ($("#imgAutojoin").attr('src') == "images/checked2.png") {
               $("#hdnAutoreqJoin").val('1');
               $("#imgReqjoin").attr('src', 'images/unchecked2.png');
           } else {
               $("#hdnAutoreqJoin").val('1');
               $("#imgAutojoin").attr('src', 'images/checked2.png');
               $("#imgReqjoin").attr('src', 'images/unchecked2.png');
           }
       }
       function checkremoveimages() {
           if ($("#imgReqjoin").attr('src') == 'images/checked2.png') {
               $("#hdnAutoreqJoin").val('2');
               $("#imgAutojoin").attr('src', 'images/unchecked2.png');
           } else {
               $("#hdnAutoreqJoin").val('2');
               $("#imgReqjoin").attr('src', 'images/checked2.png');
               $("#imgAutojoin").attr('src', 'images/unchecked2.png');
           }
       }
   </script>
  
   <script type="text/javascript">
       $(document).ready(function () {
           if ($("#lblNotifyCount").text() == '0') {
               document.getElementById("divNotification1").style.display = "none";
           }
           if ($("#lblInboxCount").text() == '0') {
               document.getElementById("divInbox").style.display = "none";
           }
       });

       $(document).ready(function () {
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           prm.add_endRequest(function () {
               if ($("#lblNotifyCount").text() == '0') {
                   document.getElementById("divNotification1").style.display = "none";
               }
               if ($("#lblInboxCount").text() == '0') {
                   document.getElementById("divInbox").style.display = "none";
               }
           });
       });
   </script>
   <script type="text/javascript">
       ddsmoothmenu.init({
           mainmenuid: "smoothmenu1", //menu DIV id
           orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
           classname: 'ddsmoothmenu', //class added to menu's outer DIV
           contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
       })
       $(document).ready(function () {
           $(".photoIcon").mouseover(function () {
               $("#imgCamera").css('display', 'block');
           });
           $(".photoIcon").mouseout(function () {
               $("#imgCamera").css('display', 'none');
           });
       });
   </script>
   <script type="text/javascript">
       $(document).ready(function () {
           //document.getElementById('imgLoadMore').style.display = 'none';
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           prm.add_endRequest(function () {
               ddsmoothmenu.init({
                   mainmenuid: "smoothmenu1", //menu DIV id
                   orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
                   classname: 'ddsmoothmenu', //class added to menu's outer DIV
                   contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
               })

               $(".scroll").click(function (event) {
                   $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 500);
               });

               $(".photoIcon").mouseover(function () {
                   $("#imgCamera").css('display', 'block');
               });
               $(".photoIcon").mouseout(function () {
                   $("#imgCamera").css('display', 'none');
               });
           });

       });
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
       function callSavegrp() {
           debugger;
          if ($('#txtCreateGr').val() != '') {
               showLoader1();
           }

          $('#lnkCreateGroup').addClass('hide-body-scroll');
           if ($('#txtCreateGr').val() == '') {
           setTimeout(
                  function () {
                      $('#lnkCreateGroup').removeClass('hide-body-scroll');
                         $('body').removeClass('remove-scroller');
                  }, 1000);
          }
       }
       function OverlayBody() {
            $('#bodyelement').addClass("remove-scroller");
        }
       function callCancelgrp() {
           // $('#aCancel').css("background", "#D0D0D0");
           // $('#aCancel').css("border", "2px solid #BCBDCE");
       }
   </script>
</asp:Content>
