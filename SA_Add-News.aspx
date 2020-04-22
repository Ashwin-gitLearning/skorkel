<%@ Page Title="" Language="C#" MasterPageFile="~/Main_Super.master" AutoEventWireup="true" CodeFile="SA_Add-News.aspx.cs" Inherits="SA_Add_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" Runat="Server">
 <%--<script src='https://cloud.tinymce.com/stable/tinymce.min.js'></script>--%>
 <script type="text/javascript" src="Scripts/tinymce/tinymce.min.js"></script>
 <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
 <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
 <script type="text/javascript">
     $(document).ready(function () {

         initTinyMce();

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(initTinyMce);

         function initTinyMce() {
             tinymce.init({
             selector: '#CKDescription',         
             toolbar: "styleselect | bold italic | strikethrough | alignleft aligncenter alignright alignjustify | numlist bullist outdent indent | link | media | image | removeformat",
             plugins: "image link media lists preview",
             automatic_uploads: true,         
             file_picker_types: 'image',
             resize: false,
             // and here's our custom image picker
             file_picker_callback: function (cb, value, meta) {
                 var input = document.createElement('input');
                 input.setAttribute('type', 'file');
                 input.setAttribute('accept', 'image/*');

                 // Note: In modern browsers input[type="file"] is functional without 
                 // even adding it to the DOM, but that might not be the case in some older
                 // or quirky browsers like IE, so you might want to add it to the DOM
                 // just in case, and visually hide it. And do not forget do remove it
                 // once you do not need it anymore.

                 input.onchange = function () {
                     var file = this.files[0];

                     var reader = new FileReader();
                     reader.onload = function () {
                         // Note: Now we need to register the blob in TinyMCEs image blob
                         // registry. In the next release this part hopefully won't be
                         // necessary, as we are looking to handle it internally.
                         var id = 'blobid' + (new Date()).getTime();
                         var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                         var base64 = reader.result.split(',')[1];
                         var blobInfo = blobCache.create(id, file, base64);
                         blobCache.add(blobInfo);

                         // call the callback and populate the Title field with the file name
                         cb(blobInfo.blobUri(), { title: file.name });
                     };
                     reader.readAsDataURL(file);
                 };
                 input.click();
             },
             menubar: false,
             style_formats: [
             {title: 'Normal', inline: 'span', styles: {'font-size':'14px'}},
             {title: 'Heading 1', inline: 'span', styles: {'font-size': '18px'}},
             {title: 'Heading 2', inline: 'span', styles: {'font-size': '16px'}}  
             ],

             statusbar: false,
             media_dimensions: false,
             media_poster: false,
             media_alt_source: false,
             image_dimensions: false,
             image_description: false,
             link_title: false,
             auto_resize: true,
             //valid_elements: 'blockquote,iframe,div[*],span[*],p[*],b,strong,i,em,ul,li,ol,h4',
             //extended_valid_elements: "a[href|target=_blank],img[*]",
             object_resizing: false,
             content_css: "css/style.css"  
             });
         }
     });
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="main-section-inner">

  <div class="panel-cover clearfix">
   <asp:UpdatePanel ID="upmain" runat="server">
    <ContentTemplate>
     <div class="full-width-con">
       <div class="card padding-15"> 
        <form action="" method="post">
         <div class="form-group">
          <label for="textbox">Title of News Article</label>
          <asp:TextBox ID="txtTitle" runat="server" clientidmode="Static" class="form-control" autocomplete="off" maxlength="200" aria-describedby="emailHelp" placeholder="Enter Title" />
          <asp:RequiredFieldValidator ID="rfdTitle" runat="server" ControlToValidate="txtTitle" Display="Dynamic" ValidationGroup="vldNews" 
           ErrorMessage="Please enter news title" ForeColor="Red" ClientIDMode="Static"></asp:RequiredFieldValidator>
         </div>
         <div class="form-group">
          <label for="">Description</label>
          <textarea ID="CKDescription" runat="server" clientidmode="Static" class="form-control textarea-editor" placeholder="Enter Description"></textarea>
          <asp:RequiredFieldValidator ID="rfdCKDesc" runat="server" ControlToValidate="CKDescription"
           Display="Dynamic" ValidationGroup="vldNews" ErrorMessage="Please enter description" ForeColor="Red" ClientIDMode="Static" />
         </div>
        
         <div class="form-group">
          <label for="">Source of Article<span class="grey-text font-300"> Optional*</span></label>
          <div class="row">
          
           <div class="col-6">
            <%--<input type="text" class="form-control" placeholder="Source Title">--%>
            <asp:TextBox ID="txtSourceField" runat="server" clientidmode="Static" class="form-control" maxlength="50" aria-describedby="emailHelp" placeholder="Enter Source Title" />
           </div>
           
           <div class="col-6">
            <%--<input type="text" class="form-control" placeholder="Source Link">--%>
            <asp:TextBox id="txtSourceLink" runat="server" clientidmode="Static" class="form-control" aria-describedby="emailHelp" placeholder="Enter Source Link" 
             maxlength="200" /><%--onblur="if(this.value!=''){validateURL(this.value)};"--%>
           </div>               
          </div>
             <div class="error_message" id="dvsourceError"></div>
         </div>
        
        <div class="submit-con">
         <%--<button class="btn btn-outline-primary m-r-15">Cancel</button>
         <button class="btn btn-primary">Publish</button>--%>
         <asp:Button ID="btnCancel" runat="server" type="Reset" Text="Cancel" class="btn btn-outline-primary margin m-r-15" ClientIDMode="Static" OnClientClick="javascript:callClose();">
         </asp:Button>
         <asp:LinkButton ID="lnkPublish" runat="server" Text="Publish" CssClass="btn btn-primary" ValidationGroup="vldNews" ClientIDMode="Static" 
          OnClientClick="javascript:return CallPostNews();" OnClick="lnkPublish_Click">
         </asp:LinkButton>
        </div>
       </form>
      </div> 
     </div>
       <!-- full-width-con ended -->
    </ContentTemplate>
    <Triggers>
     <asp:AsyncPostBackTrigger ControlID="lnkPublish" />
    </Triggers>
   </asp:UpdatePanel>
  </div>
   <!-- panel-cover ended -->
 </div>
 <script type="text/javascript">
     function validateURL(item) {
         //debugger;
         //var urlregex = new RegExp("^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&amp;%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&amp;%\$#\=~_\-]+))*$");
         var regex = /^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$/g;
         var m;
         //var urlregex = new RegExp("^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$");
         //var urlregex = new RegExp("^[a-zA-Z0-9]+\:\/\/[a-zA-Z0-9]+\.[-a-zA-Z0-9]+\.?[a-zA-Z0-9]+$|^[a-zA-Z0-9]+\.[-a-zA-Z0-9]+\.[a-zA-Z0-9]+$");
         //alert(urlregex.test($(item).val()));
         //return urlregex.test($(item).val());
         //console.log("gsdfgsd", item, urlregex.test(item));

         while ((m = regex.exec(item)) !== null) {
             // This is necessary to avoid infinite loops with zero-width matches
             if (m.index === regex.lastIndex) {
                 regex.lastIndex++;
             }
             return true;
         }
         return false;
     }
    
   
    function callClose() {
        $('#txtTitle').val('');
        $('#CKDescription').val('');
        $('#txtSourceField').val('');
        $('#txtSourceLink').val('');     
        location.href="sa_newslisting.aspx"
     }
     //function callSave() {
     //   tinyMCE.triggerSave(true,true);
     //   //alert('-->'+tinyMCE.activeEditor.getContent());
     //}
     function CallPostNews() {
         //debugger;
         if ($('#txtSourceField').val() != "" || $('#txtSourceLink').val() != "") {
             if ($('#txtSourceField').val() == '' || $('#txtSourceLink').val() == "") {
                 $('#dvsourceError').text('Please enter both Source Title and Source Link.');
                 //alert('Must enter both');
                 return false;
             } else if (!validateURL($('#txtSourceLink').val())) {
                 $('#dvsourceError').text('Enter Valid Source Link');
                 //alert('Must enter both');
                 return false;
             } else {
                 if ($('#txtSourceLink').val().indexOf('http') != 0) {
                     $('#txtSourceLink').val("http://" + $('#txtSourceLink').val());
                 }
             }      
         }
         $('#dvsourceError').text('');
          tinyMCE.triggerSave(true,true);

          if ($('#CKDescription').val() == '') {
              $('#divSaveimage').css("display", "none");

              setTimeout(
                  function () {
                      $('#divSaveimage').css("display", "none");                          
                  }, 1000);

          } else {
              showLoader1();
          }             
     }
 </script>
</asp:Content>

