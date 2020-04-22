<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DragNDrop.ascx.cs" Inherits="UserControl_DragNDrop" %>
<%--<asp:Panel runat="server" ID="dragDrop">--%>
        <div class="drag-demo" runat="server" ondragenter="OnDragEnter(event)" ondragover="OnDragOver(event)" ondrop="OnDrop(event);" id="dvDest">
            <div>
            <p class="uploadDragDrop" id="uploadDragDrop" runat="server">
                Drag and Drop File<br />
                Or
            </p>
            <p class="select-file-upload d-flex justify-content-center align-items-center">
                <asp:Label ID="lblfilenamee" class="truncate" runat="server" ClientIDMode="Static">
                </asp:Label>
                <asp:FileUpload onchange="hideUpload.bind(this)()" ID="upload" runat="server" CssClass="uploadcss" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnUploadFile" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnFileSize" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnDocType" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnDeleteVisible" runat="server" Value="" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnUploadFile1" runat="server" ClientIDMode="Static" />
                 <asp:HiddenField ID="hdnURL" runat="server" ClientIDMode="Static" />
                 <asp:HiddenField ID="hdnFileSelected" runat="server" ClientIDMode="Static" />
           
          
          
          
                        <asp:LinkButton ID="lnkDelete" ClientIDMode="Static" CssClass="document_upload_delete" runat="server" OnClick="lnkDelete_Click" ><span class="lnr lnr-trash"></span>
</asp:LinkButton>
 </p>
                </div>
        </div>
<%--</asp:Panel>--%>
<script type="text/javascript">
     //$(document).ready(function () {
     //     var box;
     //     box = document.getElementById("dvDest");
     //     box.addEventListener("dragenter", OnDragEnter, false);
     //     box.addEventListener("dragover", OnDragOver, false);
     //     box.addEventListener("drop", OnDrop, false);
     // });
      function OnDragEnter(e) {
        //  e.stopPropagation();
         // e.preventDefault();
        //  $(this).focus();
      }
      function OnDragOver(e) {
         // e.stopPropagation();
          e.preventDefault();
          //  $(this).focus();
      }
      function OnDrop(e) {
          //e.stopPropagation();
          e.preventDefault();
          selectedFiles = e.dataTransfer.files;
          uploadFiles(selectedFiles);
    }
    function reset(uploader) {
        $('#hdnUploadFile').val("");
        $('#lnkDelete').hide();
        $('#upload').show();
        $('#uploadDragDrop').show();
        $('#hdnFileSelected').val("");
        $('#lblfilenamee').text("");
        if (uploader) {
            $(uploader).val("");
        }
    }
    function uploadFiles(selectedFiles, uploader) {
        if ($('#uploadPopopError').length) {
            $('#uploadPopopError').hide();
        }
        if (selectedFiles.length > 1) {
              alert('Only one file can be uploaded a a time');
              return;
          }
          $('#lblfilenamee').text(selectedFiles[0].name);
          $('#hdnUploadFile1').val(selectedFiles[0].name);
         
          var data = new FormData();
          for (var i = 0; i < selectedFiles.length; i++) {
              data.append(selectedFiles[i].name, selectedFiles[i]);
        }
        let fileSize = 3145728;
        var givenSize = $('#hdnFileSize').val();
        if (givenSize != "") {
            fileSize = Number(givenSize);
        }
        if (selectedFiles[0].size > fileSize) {
            var sizeMsg = 'File size should be less than or equal to 3MB.';
            if (fileSize == "5242880") {
                sizeMsg = 'File size should be less than or equal to 5MB.'
            }
            if ($('#uploadPopopError').length) {
                $('#uploadPopopError').show();
                $('#uploadPopopError').text(sizeMsg);
            } else {
                alert(sizeMsg);
            }
            reset(uploader);
            return;
        }
        var urltToUpload = $('#hdnURL').val();
        if ( urltToUpload == "" ) {
            urltToUpload = "handler/FileUploadHandler.ashx";
        }
          $.ajax({
              type: "POST",
              url: urltToUpload,
              contentType: false,
              processData: false,
              data: data,
              success: function (result) {
                  //Only PDF or DOC / DOCX file Support, Max File Size 5MB 
                  if (result == 'File format not supported.' || result == "Video files not supported, Max File Size 5MB" || result == "File size should be less than or equal to 3MB.") {
                      if ($('#uploadPopopError').length) {
                          $('#uploadPopopError').show();
                          $('#uploadPopopError').text(result);
                      } else {
                          alert(result);
                      }
                      reset(uploader);
                  } else {
                      $('#hdnUploadFile').val(result.split(':')[0]);
                      $('#lnkDelete').show();
                      $('#upload').hide();
                      $('#uploadDragDrop').hide();
                      $('#hdnFileSelected').val("drag");
                  }
              },
              error: function () {
                  alert("There was error uploading files!");
                  reset(uploader);
              }
          });
    }
    function hideUpload(e) {
          $('#lblfilenamee').text(this.files[0].name);
        $('#hdnUploadFile1').val(this.files[0].name);
         $('#lnkDelete').show();
        $('#upload').hide();
        $('#uploadDragDrop').hide();
        uploadFiles(this.files, this);
         $('#hdnFileSelected').val("upload");
    }
</script>