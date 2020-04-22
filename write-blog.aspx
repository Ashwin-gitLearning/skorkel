<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
   CodeFile="write-blog.aspx.cs" Inherits="write_blog" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
    <%@ Register Src="~/UserControl/BlogPopulerPost.ascx" TagName="BlogPopulerPost" TagPrefix="BlogPopulerPost" %>
        <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
            <%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
                <asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server">
                    <!DOCTYPE html>
                    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
                    <script src="docsupport/chosen.jquery.js" type="text/javascript"></script>
                    <script src="docsupport/prism.js" type="text/javascript"></script>
                </asp:Content>
                <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
                    <!--heading ends-->
                
                    
                        <div class="innerContainer">
                          
                            <!--inner container ends-->
                            

                                <div class="main-section-inner">
                                <span class="m-aside-trigger mt-0 mb-0">
                                     <span class="lnr lnr-arrow-left"></span>
                                      <span class="avatar-text">Popular Posts</span>
                                </span>
                                    <div class="back-link-cover">
                                        <a href="AllBlogs.aspx" class="back-link"><span class="lnr lnr-arrow-left"></span> Back to Blogs</a>
                                    </div>
                                    <div class="panel-cover clearfix">
                                        <div class="center-panel">
                                            <asp:UpdatePanel ID="upmain" runat="server">
                                                <ContentTemplate>
                                                    <div class="card">
                                                        <div class="card-body">
                                                            <div class="form-group">
                                                                <label for="exampleFormControlTextarea1">Blog Title</label>
                                                                <asp:TextBox autocomplete="off" ID="txtTitle" CssClass="entryBlogTitle ip form-control" maxlength="500" ClientIDMode="Static" runat="server" placeholder="Title" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" Display="Dynamic" ValidationGroup="Blog" ErrorMessage="Please add title" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                <div>
                                                                    <asp:ValidationSummary ID="blogsummry" CssClass="RedErrormsg padding_in_blog" runat="server" ValidationGroup="blog" ClientIDMode="Static" ForeColor="Red" Font-Names="Verdana" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label for="exampleFormControlTextarea1">Description</label>
                                                                <textarea rows="5" maxlength="10000"  class="form-control" clientidmode="Static" id="CKDescription" placeholder="Start writing blog here..." runat="server"></textarea>
                                                                <asp:Label ID="lblSuMess" runat="server" Text=""></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CKDescription" Display="Dynamic" ValidationGroup="Blog" ErrorMessage="Please add description" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group margin-bottom-0 remove-flex position-relative">
                                                                <label>Classify Your Blog Post</label>
                                                               <%-- <select data-placeholder="Type to add a tag" class="chosen-select multipleSubjects form-control" id="txtSubjectList" onchange="getAllSubjectValue(this.id)" runat="server" clientidmode="Static" multiple tabindex="4">
                                                                </select>--%>
                                                                  <ul class="ulblogcontaxt">
                                                                 <uc:UserControl_MultiSelect class="form-control" ID="txtSubjectList" ClientIDMode="Static" runat="server"  />
                                                                      </ul>
                                                                <asp:HiddenField ID="hdnsubject" ClientIDMode="Static" runat="server" />
                                                            </div>
                                                            <div style="display:none;">
                                                                <asp:Button ID="btnSaveBlogs" runat="server" ClientIDMode="Static" ValidationGroup="Blog" Text="." OnClick="lnkSubmitBlog_Click" />
                                                            </div>
                                                            <!-- <p class="upload_pic_blog">
                                                                                                            Upload Picture
                                                                                                        </p>
                                                                                                        <asp:FileUpload ID="UploadPics" class="UploadPics" ClientIDMode="Static" runat="server" /> -->
                                                        </div>
                                                        <div class="card-footer text-right">
                                                            <asp:LinkButton ID="btnCancel" Text="Cancel" CssClass="btn btn-outline-primary m-r-15" runat="server"  href="AllBlogs.aspx"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkSubmitBlog" runat="server" ClientIDMode="Static" ValidationGroup="Blog" OnClientClick="javascript:CallWritebolgs();" CssClass="menuPublish btn btn-primary" OnClick="lnkSubmitBlog_Click">
                                                                <!-- <img src="images/topArrowPublish.png" align="absmiddle" /> -->Post
                                                            </asp:LinkButton>
                                                            <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
                                                            <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lnkSubmitBlog" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="right-panel-back-layer"></div>
                                        <div class="right-panel"> <span class="m-view back">Back <span class="lnr lnr-arrow-right"></span></span>
                                            <div class="aside-bar">
                                                <!-- card ended-->
                                                <div class="card releated-section">
                                                    <div class="card-body">
                                                        <asp:UpdatePanel ID="up1" class="blog_left" runat="server">
                                                            <ContentTemplate>
                                                                <div class="popularPost">
                                                                    <h4>Popular Posts</h4>
                                                                    <div class="hide_show_m">
                                                                        <BlogPopulerPost:BlogPopulerPost ID="ucBlogPopulerPost" runat="server" AllowDirectUpdate="true" />
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--DIV SUCESS HERE-->
                                <div id="divSuccess" runat="server" style=" display: none;" class="modal backgroundoverlay"  clientidmode="Static">
                                    <div class=" modal-dialog modal-dialog-centered">
                                        <div class="modal-content">
                                        <div class="modal-header">
                                             <h5 class="modal-title">Success</h5>
                                         </div>
                                            <div class="modal-body">
                                          
                                               <asp:Label ID="lblSuccess"  runat="server" Text="Blog added successfully."></asp:Label>
                                          
                                                </div>
                                        <div class="modal-footer border-top-0">
                                            <a href="AllBlogs.aspx" class="add-scroller a-tag-colcor btn btn-outline-primary">Close</a>
                                        </div>
                                             </div>
                                    </div>
                                </div>


                                <!--     <div class="blog_right_new_create">
                                <div class="create_blog_new">
                                    <div class="list_style margin_bottom">
                                        <div>
                                        </div>
                                        <div class="fieldTxt">
                                        </div>
                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        <p class="classify classify_blog">
                                            Classify Your Blog Post
                                        </p>
                                        <div class="blog_input_create">
                                        </div>
                                    </div>
                                </div>
                            </div> -->
                                <!--  <div class="cls">
                               </div> -->
                            
                        </div>
                        <script>
                        $(document).ready(function() {
                            $(document).on('click', '.popularHeading_post', function() {
                                $(".hide_show_m").slideToggle('slow');
                            });
                        });
                        </script>
                        <script type="text/javascript">
                        function messageClose() {
                            document.getElementById('divSuccess').style.display = 'none';
                        }

                        //function getAllSubjectValue(ctrlId) {
                        //    $('#MicroTag').find('label.error').remove();
                        //    var control = document.getElementById(ctrlId);
                        //    var strSelTexts = '';
                        //    var cnt = 0;
                        //    for (var i = 0; i < control.length; i++) {
                        //        if (control.options[i].selected) {
                        //            if (cnt == 0) {
                        //                strSelTexts += control.options[i].value;
                        //            } else {
                        //                strSelTexts += ',' + control.options[i].value;
                        //            }
                        //            cnt++;
                        //        }
                        //    }
                        //    $('#hdnsubject').val(strSelTexts);
                        //}
                        </script>
                        <script type="text/javascript">
                        //var config = {
                        //    '.chosen-select': {},
                        //    '.chosen-select-deselect': { allow_single_deselect: true },
                        //    '.chosen-select-no-single': { disable_search_threshold: 10 },
                        //    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                        //    '.chosen-select-width': { width: "95%" }
                        //}
                        //for (var selector in config) {
                        //    $(selector).chosen(config[selector]);
                        //}
                        </script>
                        <script type="text/javascript">
                        $(document).ready(function() {
                            //$('#divSuccess').center();
                           
                        });
                        </script>
                        <script type="text/javascript">
                        //var config = {
                        //    '.chosen-select': {},
                        //    '.chosen-select-deselect': { allow_single_deselect: true },
                        //    '.chosen-select-no-single': { disable_search_threshold: 10 },
                        //    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                        //    '.chosen-select-width': { width: "95%" }
                        //}
                        //for (var selector in config) {
                        //    $(selector).chosen(config[selector]);
                        //}
                        $(document).ready(function() {
                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                            prm.add_endRequest(function () {
                                createChosen();
                                //var config = {
                                //    '.chosen-select': {},
                                //    '.chosen-select-deselect': { allow_single_deselect: true },
                                //    '.chosen-select-no-single': { disable_search_threshold: 10 },
                                //    '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
                                //    '.chosen-select-width': { width: "95%" }
                                //}
                                //for (var selector in config) {
                                //    $(selector).chosen(config[selector]);
                                //}
                            });
                        });
                        </script>
                        <script type="text/javascript">
                        function getAllMemberValue(ctrlId) {
                            $('#divFreeSkorlTargetSearch').find('label.error').remove();
                            var control = document.getElementById(ctrlId);
                            var strSelTexts = '';
                            var cnt = 0;
                            for (var i = 0; i < control.length; i++) {
                                if (control.options[i].selected) {
                                    if (cnt == 0) {
                                        strSelTexts += control.options[i].value;
                                    } else {
                                        strSelTexts += ',' + control.options[i].value;
                                    }
                                    cnt++;
                                }
                            }
                            $('#hdnMembId').val(strSelTexts);
                        }

                        function getAllSubjectValue(ctrlId) {
                            $('#MicroTag').find('label.error').remove();
                            var control = document.getElementById(ctrlId);
                            var strSelTexts = '';
                            var cnt = 0;
                            for (var i = 0; i < control.length; i++) {
                                if (control.options[i].selected) {
                                    if (cnt == 0) {
                                        strSelTexts += control.options[i].value;
                                    } else {
                                        strSelTexts += ',' + control.options[i].value;
                                    }
                                    cnt++;
                                }
                            }
                            $('#hdnsubject').val(strSelTexts);
                        }
                        </script>
                        <script type="text/javascript">
                        $(document).ready(function() {
                            //                $("#txtTitle").keydown(function (e) {
                            //                    if (e.keyCode == 13) {
                            //                        e.preventdefault();
                            //                        return false;
                            //                    }
                            //                });
                        });

                        function CallWritebolgs() {
                            // $('#lnkSubmitBlog').css("box-shadow", "0px 0px 5px #00B7E5");
                            // $('#lnkSubmitBlog').css("background", "#00A5AA");
                            if ($('#txtTitle').val() == '' || $('#CKDescription').val() == '') {
                                setTimeout(
                                    function () {
                                        // $('#lnkSubmitBlog').css("background", "#0096a1");
                                        // $('#lnkSubmitBlog').css("box-shadow", "0px 0px 0px #00B7E5");
                                    }, 1000);
                            } else {
                                showLoader1();
                            }
                            }

                                 function CallOnRequestSucess() {
                                     $('#divSuccess').modal({ backdrop: "static" });
                            }

                            function callClose() {
                                
                                $('#txtSubjectList').val('').trigger('chosen:updated');
                                $('#txtTitle').val('');
                                $('#CKDescription').val('');
             
         }

                        </script>
                  
                </asp:Content>