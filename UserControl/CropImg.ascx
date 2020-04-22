<%@ Control Language="C#" AutoEventWireup="true" Inherits="CropImg" CodeFile="CropImg.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script src="<%=ResolveUrl("../js/jquery.Jcrop.min.js") %>" type="text/javascript"></script>
<div>
    <br />
    <br />
    <table width="100%">
       
        <tr>
            <td>
                <img scr="" clientidmode="Static" id="imgViewImg" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
               <div class="img_control_home"> <img scr="" clientidmode="Static" id="newimg" runat="server" /> </div>

                <asp:Image ID="cropedImage"
                    runat="server" Visible="False" ClientIDMode="Static" />
           
                <asp:Button CssClass="submitButton cancelcropdivs crop_btn_c" ID="btnDeleteimg" runat="server"
                    Visible="false" Text="Delete" Style="display: block; margin-top: 0px; margin-left: 0px;"
                    OnClick="btnDeleteimg_Click" />&nbsp;
                     <asp:FileUpload BackColor="white" ForeColor="Black" ID="fileup1" runat="server" ClientIDMode="Static" /><br />
            
                <asp:Label ID="lblmsg" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
           
            </td>
        </tr>
        <tr>
            <td>
               
            
                <a id="uploadImage" runat="server" style="display: block;" onclick="CallUploadimg()"
                    clientidmode="Static" class="submitButton cancelcropdivs upload_scroll">Upload</a>
                <asp:Button runat="server" ID="btnDummy" ClientIDMode="Static" Style="display: none;"
                    OnClick="uploadImage_Click" CssClass="submitButton cancelcropdivs" />
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="20%">
        <tr>
            <td>
                <asp:Button CssClass="submitButton cancelcropdivs" ID="SaveImg" runat="server" Text="Crop Picture" OnClientClick="javascript:callCropPic();"
                    Style="display: none; margin-top: -45px;" OnClick="SaveImg_Click" ClientIDMode="Static" />
            </td>
            <td>
                <asp:Button CssClass="submitButton cancelcropdivs" ID="btnSave" runat="server" Text="Save Picture"
                    OnClientClick="javascript:CallSaveProcess();" ClientIDMode="Static" Style="display: none;
                    margin-top: -30px; margin-left: -4px;" OnClick="btnSave_Click" />
            </td>
            <td>
                <asp:Button CssClass="submitButton cancelcropdivs" ID="btnCancel" runat="server" ClientIDMode="Static" OnClientClick="javascript:callResetPic();"
                    Text="Reset" Style="display: none; margin-top: -30px;" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
    <div id="divSaveimage" style="display: none; margin: -35px 0px 0px 275px">
        <img id="imgSave" src="images/Loadgif.gif" />
    </div>
    <%--Start for normal crop image     --%>
    <br />
    <%--End for normal crop image     --%>
    <asp:HiddenField ID="X" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="Y" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="W" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="H" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnErrorMsg" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnPhotoPath" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnPhotoName" runat="server" ClientIDMode="Static" />
    <script type="text/javascript">
     $(document).ready(function(){
        $('.upload_scroll').click(function(){
            alert("The paragraph was clicked.");
       });
     });
    </script>
    <script type="text/javascript">
   
            $(function ($) {
                $('#newimg').Jcrop({
                    onSelect: updateCoords,
                    aspectRatio: 1,
                    minSize: [50, 50]
                });
            });
            function updateCoords(c) {
                $('#X').val(c.x);
                $('#Y').val(c.y);
                $('#H').val(c.h);
                $('#W').val(c.w);
            }
            function hidePopUpShare() {
                window.parent.$find('PopUpShare').hide();
            }
</script>
    <script type="text/javascript">
        $(document).ready(function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                    $('#newimg').Jcrop({
                        onSelect: updateCoords,
                        aspectRatio: 1,
                        minSize: [50, 50]
                });
                function updateCoords(c) {
                    $('#X').val(c.x);
                    $('#Y').val(c.y);
                    $('#H').val(c.h);
                    $('#W').val(c.w);
                }
                $('#uploadImage').click(function () {
                    $("#hdnErrorMsg").val('');
                    $("#hdnPhotoPath").val('');
                    $("#hdnPhotoName").val('');
                    var fileUpload = $("#fileup1").get(0);
                    var files = fileUpload.files;
                    var data = new FormData();
                    for (var i = 0; i < files.length; i++) {
                        data.append(files[i].name, files[i]);
                    }
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
                                CallUploadimg();
                            }
                            else {
                                var v = result.split(":");
                                $("#hdnPhotoPath").val(v[0]);
                                $("#hdnPhotoName").val(v[1]);
                                document.getElementById('btnDummy').click();
                            }
                        },
                        error: function () {
                            alert("There was error uploading files!");
                        }
                    });
                });
            });
        
        });
</script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#uploadImage').click(function () {
                $("#hdnErrorMsg").val('');
                $("#hdnPhotoPath").val('');
                $("#hdnPhotoName").val('');
                var fileUpload = $("#fileup1").get(0);
                var files = fileUpload.files;
                var data = new FormData();
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }
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
                            CallUploadimg();
                        }
                        else {
                            var v = result.split(":");
                            $("#hdnPhotoPath").val(v[0]);
                            $("#hdnPhotoName").val(v[1]);
                            document.getElementById('btnDummy').click();
                        }
                    },
                    error: function () {
                        alert("There was error uploading files!");
                    }
                });
            });
        });
        function CallSaveProcess() {
            $(".divProgress").css("display", "none");
            $('#divSaveimage').css('display', 'block');
            $('#btnSave').css("background", "#19A0AA");
            $('#btnSave').css("box-shadow", "0px 0px 5px #00B7E5");
        }
        function callCropPic() {
            $(".divProgress").css("display", "none");
            $('#SaveImg').css("background", "#19A0AA");
            $('#SaveImg').css("box-shadow", "0px 0px 5px #00B7E5");
        }
        function callResetPic() {
            $(".divProgress").css("display", "none");
            $('#btnCancel').css("background", "#19A0AA");
            $('#divSaveimage').css('display', 'block');
            $('#btnCancel').css("box-shadow", "0px 0px 5px #00B7E5");
        }
        function CallUploadimg() {
            $('#uploadImage').css("background", "#19A0AA");
            $('#uploadImage').css("box-shadow", "0px 0px 5px #00B7E5");
            if ($('#lblmsg').text() == 'Please select image.') {
                setTimeout(
                function () {
                    $('#uploadImage').css("background", "#0096a1");
                    $('#uploadImage').css("box-shadow", "0px 0px 0px #00B7E5");
                }, 1000);
            }
        }
    </script>
</div>
