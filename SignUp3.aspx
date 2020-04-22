<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp3.aspx.cs" Inherits="SignUp3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagPrefix="usc" TagName="UserControl_DragNDrop" Src="~/UserControl/DragNDrop.ascx" %>
<%@ Register TagPrefix="uc" TagName="UserControl_MultiSelect" Src="~/UserControl/MultiSelect.ascx" %>
<%@ Register TagPrefix="usc" TagName="UserControl_LandingUC" Src="~/UserControl/LandingUC.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0,  user-scalable=no, shrink-to-fit=no">
    <script type="text/javascript" src="<%=ResolveUrl("js/jquery.1.12.4.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("js/croppie.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveUrl("https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js")%>" lang="javascript"></script>
    <script type="text/javascript" src="<%=ResolveUrl("js/bootstrap.min.js")%>"></script>
    <script type="text/javascript" src="js/custom.js"></script>
    <script type="text/javascript" lang="javascript" src="<%=ResolveUrl("js/PopupCenter.js")%>"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/exif-js"></script>
    <script type="text/javascript" lang="javascript" src="<%=ResolveUrl("js/jquery.carouFredSel-6.2.1-packed.js")%>"></script>

    <link href="images/skorkel-fav-icon.png" rel="shortcut icon" type="image/png" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap-reboot.css" rel="stylesheet" />
    <link href="css/bootstrap-grid.css" rel="stylesheet" />
    <link href="css/font-icon.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="https://cdn.linearicons.com/free/1.0.0/icon-font.min.css" rel="stylesheet" />
    <link href="css/croppie.css" rel="stylesheet" />

    <script type="text/javascript">
        function showLoader1() {
            $("#globalLoader").show();
        }
        function hideLoader1() {
            $("#globalLoader").hide();
        }
        function OverlayBody() {
            $('#bodyelement').addClass("remove-scroller");
        }
        function PopupShown(sender, args) {
            sender._popupBehavior._element.style.zIndex = 99999999;
        }
        $(document).on('click', '#close-popup', function () {

            $(".backgroundoverlay").hide();
            $('body').removeClass('remove-scroller');
        });

        $(document).ready(function () {
            //radioButton();
            //$("span.ediDel").click(function () {
            //    $('#hdnintPostId').val($(this).children('#hdnintPostIdelet').val());
            //    $('#hdnstrPostDescriptiondele').val($(this).children('#hdnstrPostDescriptiondel').val());
            //});

            //$("span.ediDelc").click(function () {
            //    $('#hdnintPostceIdelet').val($(this).children('#hdnintPostcIdelet').val());
            //    $('#hdnstrPostDescriptioncedel').val($(this).children('#hdnstrPostDescriptioncdel').val());
            //});

            //$("#updoc").click(function () {
            //    $('#hdnintdocIdelete').val($(this).children('#hdnintdocIdelet').val());
            //    $('#hdnstrdocDescriptiondele').val($(this).children('#hdnstrdocDescriptiondel').val());
            //    $('#hdnfilestrFilePathe').val($(this).children('#hdnfilestrFilePath').val());
            //});

            //$("span.ediDelwork").click(function () {
            //    $('#hdnintworkeIdelet').val($(this).children('#hdnintworkIdelet').val());
            //    $('#hdnstrworkeDescriptiondel').val($(this).children('#hdnstrworkDescriptiondel').val());
            //});

            //$("span.updaEDU").click(function () {
            //    $('#hdnintedueIdelet').val($(this).children('#hdninteduIdelet').val());
            //    $('#hdnstredueDescriptiondel').val($(this).children('#hdnstreduDescriptiondel').val());
            //});

            $("span.updaAchi").click(function () {
                $('#hdnintacheIdelet').val($(this).children('#hdnintachIdelet').val());
                $('#hdnstracheDescriptiondel').val($(this).children('#hdnstrachDescriptiondel').val());
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                //radioButton();
                //$("span.ediDel").click(function () {
                //    $('#hdnintPostId').val($(this).children('#hdnintPostIdelet').val());
                //    $('#hdnstrPostDescriptiondele').val($(this).children('#hdnstrPostDescriptiondel').val());
                //});

                //$("span.ediDelc").click(function () {
                //    $('#hdnintPostceIdelet').val($(this).children('#hdnintPostcIdelet').val());
                //    $('#hdnstrPostDescriptioncedel').val($(this).children('#hdnstrPostDescriptioncdel').val());
                //});

                //$("#updoc").click(function () {
                //    $('#hdnintdocIdelete').val($(this).children('#hdnintdocIdelet').val());
                //    $('#hdnstrdocDescriptiondele').val($(this).children('#hdnstrdocDescriptiondel').val());
                //    $('#hdnfilestrFilePathe').val($(this).children('#hdnfilestrFilePath').val());
                //});

                //$("span.ediDelwork").click(function () {
                //    $('#hdnintworkeIdelet').val($(this).children('#hdnintworkIdelet').val());
                //    $('#hdnstrworkeDescriptiondel').val($(this).children('#hdnstrworkDescriptiondel').val());
                //});

                //$("span.updaEDU").click(function () {
                //    $('#hdnintedueIdelet').val($(this).children('#hdninteduIdelet').val());
                //    $('#hdnstredueDescriptiondel').val($(this).children('#hdnstreduDescriptiondel').val());
                //});

                $("span.updaAchi").click(function () {
                    $('#hdnintacheIdelet').val($(this).children('#hdnintachIdelet').val());
                    $('#hdnstracheDescriptiondel').val($(this).children('#hdnstrachDescriptiondel').val());
                });
            });
        });

        function CallAchiveMiddle() {
            var offset = $("#divAchivement").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $("#divAchivement").position().top + 800;
            $("html, body").animate({ scrollTop: $(window).height() + 2000 }, 10);
        }
        function callSaveAch() {
            // $('#lnkSaveAchivemnt').css("background", "#00A5AA");
            // $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 5px #00B7E5");
            //newly commented
            // || $('#ddlPosition').text() == 'Position'
            // || $('#lblAchivmentmsg').text() == 'Please select position.'
            if ($('#txtCompitition').text() == '' || $('#ddlMilestone').text() == 'Type of Milestone') {
                setTimeout(
                    function () {
                        // $('#lnkSaveAchivemnt').css("background", "#0096a1");
                        // $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
            if ($('#lblAchivmentmsg').text() == 'Please select milestone.') {
                setTimeout(
                    function () {
                        // $('#lnkSaveAchivemnt').css("background", "#0096a1");
                        // $('#lnkSaveAchivemnt').css("box-shadow", "0px 0px 0px #00B7E5");
                    }, 1000);
            }
        }

        function CalllnlAddEducation() {
            //$('#ctl00_ContentPlaceHolder1_lnlAddEducation').css("box-shadow", "0px 0px 2px #00B7E5");
            //$('#ctl00_ContentPlaceHolder1_lnlAddEducation').css("background", "#DCDBDB");
        }

        function CallEducationMiddle() {
            var offset = $("#txtEduDescription").offset();
            var posY = offset.top - $(window).scrollTop();
            var posX = offset.left - $(window).scrollLeft();
            var y = $(window).scrollTop();
            $("html, body").animate({ scrollTop: $(window).height() + 2000 }, 10);
        }

        function docdelete() {
            $('#divDeletesucess').css("display", "block");
            $('#AddWorkExp').css("display", "none");
        }

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

        function divdeletes() {
            // $('#lnkDeleteConfirm').css("box-shadow", "0px 0px 5px #00B7E5");
            $(".divProgress").css("display", "none");
            $('#divDeletesucess').css("display", "none");
        }

        function callCancelEdu() {
            $('#lnkCancelEducation').css("background", "#D0D0D0");
            $('#lnkCancelEducation').css("border", "2px solid #BCBDCE");
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
        //if (!isFloat($('#txtSourceLink').val())) {
        //  $('#dvsourceError').text('Enter only number or float value !');
        //  //alert('Must enter both');
        //  return false;
        //}

        function callCancelEdu() {
            $('#lnkCancelEducation').css("background", "#D0D0D0");
            $('#lnkCancelEducation').css("border", "2px solid #BCBDCE");
        }

        function CallEduLI() {
            //$("#PopUpCropImage").css("display", "none");
            //$("#imgUser").attr("src", $("#hdnImageProfile").val());
            //$('#lnkDeleteConfirm').css("box-shadow", "0px 0px 0px #00B7E5");
        }

        function CalllnkAddachive() {
            // $('#ctl00_ContentPlaceHolder1_lnkAddachive').css("box-shadow", "0px 0px 2px #00B7E5");
            // $('#ctl00_ContentPlaceHolder1_lnkAddachive').css("background", "#DCDBDB");
        }

        function isFloat(evt) {

            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert('Please enter only number or float value !');
                return false;
            }
            else {
                //if dot sign entered more than once then don't allow to enter dot sign again. 46 is the code for dot sign
                var parts = evt.srcElement.value.split('.');
                if (parts.length > 1 && charCode == 46) {
                    return false;
                }
                return true;
            }
        }

        //$('#txtPercentile').keypress(function(event) {
        // if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        //   event.preventDefault();
        // }
        //});

        function isNumberKey(evt, obj) {

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
            if (parseFloat(value) < min || isNaN(value))
                return 0;
            else if (parseFloat(value) > max)
                return 100;
            else return value;
        }
    </script>

</head>
<body class="remove-scroller">
    <form id="form1" runat="server">
        <usc:UserControl_LandingUC ID="UserControl1" ClientIDMode="Static" runat="server" />
        <asp:ScriptManager ID="scriptmgr" runat="server" EnablePageMethods="true" EnablePartialRendering="true">
        </asp:ScriptManager>

        <div class="main-section-inner">
            <asp:HiddenField ID="hdnActive" runat="server" />
            <div class="" visible="false" id="divMain" runat="server">
                <div class="custom-nav-con home-nav-tab">
                    <!-- Nav tabs -->
                    <ul class="custom-nav-control nav nav-tabs ">
                        <li class="nav-item">
                            <asp:LinkButton ID="lnkEducation" runat="server" class="nav-link" Text="Education" OnClick="lnkEducation_Click">
                            </asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="lnkAchievements" runat="server" class="nav-link" Text="Achievements" OnClick="lnkAchievements_Click">
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>

            <asp:UpdatePanel ID="uppanel" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
                <ContentTemplate>
                    <!--- Education panel -->
                    <asp:UpdatePanel ID="PnlEduction" runat="server">
                        <ContentTemplate>
                            <div class="custom-nav-container tab-pane container fade active show" id="education">
                                <div class="card-list-con blog-list">
                                    <div class="btn-title-con d-none">
                                        <div>
                                            <h5 class="card-title float-left">Education</h5>
                                        </div>
                                        <div>
                                            <asp:LinkButton ID="lnlAddEducation" runat="server" Text="+  Add" CssClass="btn hide-body-scroll btn-outline-primary float-right"
                                                OnClick="lnlAddEducation_Click" OnClientClick="CalllnlAddEducation();">
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <!---List View-->
                                    <asp:HiddenField ID="hdnintedueIdelet" Value="" ClientIDMode="Static" runat="server" />
                                    <asp:HiddenField ID="hdnstredueDescriptiondel" Value="" ClientIDMode="Static" runat="server" />
                                    <asp:ListView ID="lstEducation" runat="server" OnItemDataBound="lstEducation_ItemDataBound" OnItemCommand="lstEducation_ItemCommand" CellPadding="9">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnintEducationId" Value='<%#Eval("intEducationId") %>' runat="server" />
                                            <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
                                            <asp:HiddenField ID="hdnToMonth" Value='<%#Eval("ToMonth") %>' runat="server" />
                                            <div class="card top-list d-none">
                                                <div class="post-con">
                                                    <div class="post-body">
                                                        <span id="divEducationED" runat="server" class="more-btn float-right">
                                                            <span class="dropdown">
                                                                <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                    <img src="images/more.svg" alt="" class="more-btn">
                                                                </a>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="updaEDU" class="dropdown-menu updaEDU" aria-labelledby="dropdownMenuLink" x-placement="bottom-start"><asp:HiddenField ID="hdninteduIdelet" Value='<%# Eval("intEducationId") %>' ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnstreduDescriptiondel" Value='<%# Eval("strInstituteName") %>' ClientIDMode="Static" runat="server" />
                                                                    <asp:LinkButton ID="lnkEditDoc" runat="server" Font-Underline="false" Visible="true" ToolTip="Edit" class="hide-body-scroll dropdown-item"
                                                                        CommandName="EditEdu" CausesValidation="false">
                     <span class="lnr lnr-pencil"></span> Edit              
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkDeleteDoc" runat="server" Visible="true" ToolTip="Delete" class="hide-body-scroll dropdown-item"
                                                                        CommandName="DeleteAch" CausesValidation="false" OnClientClick="javascript:docdelete();return false;">
                     <span class="lnr lnr-trash"></span> Delete                                 
                                                                    </asp:LinkButton>
                                                                </span>
                                                            </span>
                                                        </span>
                                                        <h3>
                                                            <asp:Label ID="lblInstituteName" runat="server" Text='<%#Eval("strInstituteName") %>' />, <span class="normal-font">
                                                                <asp:Label ID="lblEducationAchievement" runat="server" Text='<%#Eval("strDegree") %>' /></span>
                                                        </h3>
                                                        <span class="small-date">
                                                            <asp:Label ID="lblFromMonth" runat="server" Text='<%#Eval("intMonth") %>' />
                                                            <asp:Label ID="lblFromYear" runat="server" Text='<%#Eval("intYear") %>' />
                                                            -
                  <asp:Label ID="lblintToMonth" runat="server" Text='<%#Eval("intToMonth") %>' />
                                                            <asp:Label ID="lblintToYear" runat="server" Text='<%#Eval("intToYear") %>' />
                                                            <asp:Label ID="lblPresentDay" runat="server" />
                                                        </span>
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
                                    <div class="modal backgroundoverlay privacy-policy-page disclaimer-popup" id="divDisclaimerPopup" runat="server">
                                        <div class="modal-dialog modal-dialog-centered">
                                            <div class="w-100">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h5 class="modal-title">Disclaimer
                                                        </h5>
                                                        <div class="float-left close-page-popup" data-dismiss="modal">x</div>
                                                    </div>
                                                    <div class="modal-body">
                                                        Please note that you will not be eligible to obtain jobs and internships on Skorkel until you fill out your details and 
                                generate your own Skorkel Score. We urge you to do so at the earliest.
                                                    </div>
                                                    <div class="submit-con m-b-15">
                                                        <%--<div class="modal-close-login btn btn-outline-primary add-scroller" data-dismiss="modal">
                                Ok</div>--%>
                                                        <asp:LinkButton ID="lnkSkipEducation" runat="server" ClientIDMode="Static" OnClick="lnkSkipEducation_Click" CssClass="hide-body-scroll btn btn-outline-primary m-r-15" Text="Ok" Visible="true"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal sign-up login-modal backgroundoverlay" id="divEducation" runat="server" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="overflow: auto;">
                                        <div class="modal-dialog modal-dialog-centered" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header border-0">
                                                    <img src="images/blue-logo.svg" alt="" class="blue-logo" />
                                                </div>
                                                <div class="modal-body login-container text-left">
                                                    <h3 class="text-center">Welcome to Skorkel</h3>
                                                    <p class="text-center">Enter your Educational Details</p>


                                                    <asp:UpdatePanel ID="upedu" runat="server" class="mt-4 login-form-con w-100">
                                                        <ContentTemplate>
                                                            <div class="progess-con">
                                                                <span class="grey-text">Step 1 of 2</span>
                                                                <h4>Education</h4>
                                                                <div class="progress">
                                                                    <div class="progress-bar bg-primary" role="progressbar" style="width: 50%" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-row">
                                                                <div class="row w-100 form-group">
                                                                    <div class="input-group flex-row search-cover">
                                                                        <div class="input-group-prepend">
                                                                            <div class="input-group-text">
                                                                                <img src="images/lnr-apartment.svg">
                                                                            </div>
                                                                        </div>
                                                                        <ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionList" MinimumPrefixLength="1" EnableCaching="false"
                                                                            CompletionInterval="10" CompletionSetCount="1" TargetControlID="txtInstitute" OnClientShown="PopupShown" FirstRowSelected="false"
                                                                            CompletionListCssClass="AutoCompletionList" CompletionListItemCssClass="AutoComp_listItem" CompletionListHighlightedItemCssClass="AutoComp_itemHighlighted">
                                                                        </ajax:AutoCompleteExtender>
                                                                        <asp:TextBox ID="txtInstitute" runat="server" ValidationGroup="validationEdu" ClientIDMode="Static" placeholder="Enter Institute Name"
                                                                            class="form-control outline-trig">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInstitute" ForeColor="Red"
                          Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please enter Institute / University name.">
                         </asp:RequiredFieldValidator>--%>
                                                                </div>
                                                                <div class="row w-100 form-group">
                                                                    <div class="col-12 col-sm-6 col-xs-12 pr-0 pl-0 spacing-columns">
                                                                        <div class="input-group search-cover">
                                                                            <div class="input-group-prepend">
                                                                                <div class="input-group-text">
                                                                                    <img src="images/lnr-graduation-hat.svg">
                                                                                </div>
                                                                            </div>
                                                                            <asp:TextBox ID="txtTitle" runat="server" ClientIDMode="Static" placeholder="Degree" class="form-control outline-trig">
                                                                            </asp:TextBox>
                                                                            <ajax:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="GetDegreeList" MinimumPrefixLength="1"
                                                                                CompletionInterval="10" OnClientShown="PopupShown" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtTitle"
                                                                                FirstRowSelected="false" CompletionListCssClass="AutoCompletionList" CompletionListItemCssClass="AutoComp_listItem"
                                                                                CompletionListHighlightedItemCssClass="AutoComp_itemHighlighted">
                                                                            </ajax:AutoCompleteExtender>

                                                                        </div>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtTitle" ForeColor="Red" Display="Dynamic"
                              ValidationGroup="validationEdu" ErrorMessage="Please enter Degree.">
                             </asp:RequiredFieldValidator>  --%>
                                                                    </div>
                                                                    <div class="col-12 col-sm-5 col-xs-12 pr-0 pl-0">
                                                                        <div class="input-group search-cover">
                                                                            <div class="input-group-prepend">
                                                                                <div class="input-group-text">
                                                                                    <img src="images/Grade.svg">
                                                                                </div>
                                                                            </div>
                                                                            <asp:TextBox ID="txtPercentile" runat="server" ClientIDMode="Static" placeholder="Percentile" class="form-control outline-trig"
                                                                                onkeypress="return isNumberKey(event,this)" MaxLength="5" min="0.01" step="0.01" onkeyup="this.value = minmax(this.value, 0, 100)">
                                                                            </asp:TextBox>
                                                                            <%--<div class="error_message" id="dvsourceError"></div>--%>
                                                                        </div>
                                                                        <%-- <asp:RequiredFieldValidator ID="rfdPercentile" runat="server" ControlToValidate="txtPercentile" ForeColor="Red" Display="Dynamic"
                          ValidationGroup="validationEdu" ErrorMessage="Please enter Percentile.">
                         </asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                </div>
                                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" class=" w-100">
                                                                    <ContentTemplate>
                                                                        <div class="row w-100 mb-0 date-picker form-group">
                                                                            <div class="col-6 col-sm-3 pr-0 pl-0 spacing-columns">
                                                                                <div class="">
                                                                                    <select id="ddlFromMonth" runat="server" class="form-control" name="select">
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

                                                                            </div>
                                                                            <div class="col-6 col-sm-3 pr-0 pl-0">
                                                                                <div class="">
                                                                                    <asp:DropDownList ID="txtEducationFromdt" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <div class="date-picket-to">To</div>
                                                                            <div class="col-6 col-sm-3 pr-0 pl-0 spacing-columns">
                                                                                <div class="">
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
                                                                            </div>
                                                                            <div class="col-6 col-sm-3 pr-0 pl-0">
                                                                                <div class="">
                                                                                    <asp:DropDownList ID="txtEducationTodt" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlFromMonth"
                                Display="Dynamic" class="mr-2" ValidationGroup="validationEdu" ErrorMessage="Please select from month." InitialValue="Month" ForeColor="Red">                      
                              </asp:RequiredFieldValidator>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtEducationFromdt" ForeColor="Red"
                                Display="Dynamic" class="mr-2" ValidationGroup="validationEdu" ErrorMessage="Please select from year." InitialValue="Year">
                              </asp:RequiredFieldValidator>                       
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlToMonth"
                               Display="Dynamic" ValidationGroup="validationEdu" ErrorMessage="Please select to month." InitialValue="Month" class="mr-2" ForeColor="Red">                        
                              </asp:RequiredFieldValidator>                         
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtEducationTodt"
                               Display="Dynamic" class="mr-2" ValidationGroup="validationEdu" ErrorMessage="Please select to year."
                               InitialValue="Year" ForeColor="Red">
                              </asp:RequiredFieldValidator>--%>
                                                                            <asp:Label class="display-inline" ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                                                        </div>
                                                                        <div class="row mt-4 d-none">
                                                                            <div class="col-sm-12 pr-0 pl-0">
                                                                                <div class="custom-checkbox">
                                                                                    <asp:CheckBox ID="chkEducation" runat="server" AutoPostBack="true" ClientIDMode="Static" CssClass="custom-checkbox" Text="Currently studying"
                                                                                        OnCheckedChanged="chkEducation_CheckedChanged1" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                <div class="row mt-3 w-100 form-group">
                                                                    <div class="col-md-12 pl-0 pr-0">
                                                                        <textarea id="txtEduDescription" runat="server" rows="5" maxlength="500" class="form-control" placeholder="Description" clientidmode="Static" />
                                                                    </div>
                                                                </div>
                                                                <!---Score Private Public-->
                                                                <div class="row w-100 form-group">
                                                                    <div class="col-md-12 pl-0 pr-0">
                                                                        <asp:CheckBox ID="cbScorePrivate" CssClass="custom-checkbox" ClientIDMode="Static" Text="Make your score private" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="submit-con mt-0">
                                                                <asp:LinkButton ID="btndisclaimer" runat="server" OnClick="btndisclaimer_Click" ClientIDMode="Static" CssClass="hide-body-scroll btn btn-outline-primary m-r-15 skip-close" Text="Skip" Visible="true"></asp:LinkButton>
                                                                <%--<button id="btndisclaimer" class="add-scroller btn btn-outline-primary m-r-15 skip-close">Skip</button>--%>
                                                                <%--<asp:LinkButton ID="lnkCancelEducation" runat="server" Text="Cancel" CssClass="add-scroller btn btn-outline-primary m-r-15" ClientIDMode="Static"
                    OnClick="lnkCancelEducation_Click" OnClientClick="javascript:callCancelEdu();">
                   </asp:LinkButton>--%>
                                                                <asp:LinkButton ID="lnkSaveEducation" runat="server" Text="Next" CssClass="btn btn-primary" ClientIDMode="Static"
                                                                    ValidationGroup="validationEdu" OnClick="lnkSaveEducation_Click" OnClientClick="javascript:callSaveEdu();">
                                                                </asp:LinkButton>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnkSaveEducation" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!-- Education ended -->
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- Achievements Start -->
            <asp:UpdatePanel ID="PnlAchivement" runat="server">
                <ContentTemplate>
                    <div class="custom-nav-container tab-pane container active show fade" id="achievements">
                        <div class="card-list-con blog-list">
                            <div class="btn-title-con d-none">
                                <div>
                                    <h5 class="card-title float-left">Achievements</h5>
                                </div>
                                <div>
                                    <asp:LinkButton ID="lnkAddachive" runat="server" class="btn btn-outline-primary hide-body-scroll float-right" Text="+  Add"
                                        OnClientClick="CalllnkAddachive();" OnClick="lnkAddachive_Click">
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <!-- Achivement Modal -->
                            <div class="modal active show fade sign-up login-modal  backgroundoverlay" id="divAchivement" runat="server" clientidmode="Static" tabindex="-1"
                                role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="modal-dialog modal-dialog-centered">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="hdnCompetitionType" Value="0" runat="server" ClientIDMode="Static" />
                                        <div class="w-100" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header border-0">
                                                    <img src="images/blue-logo.svg" alt="" class="blue-logo" />
                                                </div>
                                                <div class="modal-body login-container text-left" id="add_achivement" runat="server">
                                                    <h3 class="text-center">Welcome to Skorkel</h3>
                                                    <p class="text-center">Enter your Achievements Details</p>
                                                    <div class="mt-4 login-form-con w-100">
                                                        <div class="form-row m-0">
                                                            <div class="progess-con">
                                                                <span class="grey-text">Step 2 of 2</span>
                                                                <h4>Achievements</h4>
                                                                <div class="progress">
                                                                    <div class="progress-bar bg-primary" role="progressbar" style="width: 100%" aria-valuenow="50" aria-valuemin="0"
                                                                        aria-valuemax="100">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row custom-select-con w-100">
                                                                <div class="col-sm-12 mb-sm-0 mb-6 pl-0 pr-0">
                                                                    <div class="form-group">
                                                                        <label class="achive-label">Achivements Category</label>
                                                                        <div class="select-wrapper">
                                                                            <asp:DropDownList ID="ddlMilestone" runat="server" ClientIDMode="Static" AutoPostBack="true" class="form-control"
                                                                                ValidationGroup="validationAchiv" OnSelectedIndexChanged="ddlMilestone_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <asp:Label ID="lblAchivmentmsg" runat="server" ForeColor="Red" ClientIDMode="Static"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 mb-sm-0 mb-6 pl-0 pr-0">
                                                                    <div class="form-group" id="divCompetition" runat="server" visible="false" clientidmode="Static">
                                                                        <asp:TextBox ID="txtCompitition" runat="server" placeholder="Choose Competition" MaxLength="100"
                                                                            ClientIDMode="Static" class="form-control">
                                                                        </asp:TextBox>
                                                                        <ajax:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false"
                                                                            CompletionListCssClass="AutoCompletionList" ServiceMethod="GetCompititionLists" MinimumPrefixLength="1"
                                                                            CompletionListItemCssClass="AutoComp_listItem" TargetControlID="txtCompitition" UseContextKey="true"
                                                                            CompletionInterval="10" OnClientShown="PopupShown" EnableCaching="false" CompletionSetCount="1"
                                                                            CompletionListHighlightedItemCssClass="AutoComp_itemHighlighted"
                                                                            ContextKey="<%# Eval(hdnCompetitionType.Value) %>">
                                                                        </ajax:AutoCompleteExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtCompitition"
                                                                            ForeColor="Red" Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please enter competition.">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-12 pl-0 pr-0" id="divddlPosition" runat="server" visible="false" clientidmode="Static">
                                                                    <div class="form-group">
                                                                        <div class="select-wrapper">
                                                                            <asp:DropDownList ID="ddlPosition" runat="server" ClientIDMode="Static" class="form-control" ValidationGroup="validationAchiv">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <p>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlPosition" ForeColor="Red"
                                                                                Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please select position."><%--InitialValue="Position"--%>
                                                                            </asp:RequiredFieldValidator>
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div id="divCtrls" class="w-100" runat="server" visible="false" clientidmode="Static">

                                                                <div class="form-group" id="divCommitteeName" runat="server" visible="false" clientidmode="Static">
                                                                    <!-- <label for="textbox">Committee Name</label> -->
                                                                    <asp:TextBox ID="txtCommitteeName" runat="server" MaxLength="100" autocomplete="off" placeholder="Committee Name"
                                                                        class="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfdCommittee" runat="server" ControlToValidate="txtCommitteeName"
                                                                        Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please enter Committee name." ForeColor="Red">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group" id="divJournalName" runat="server" visible="false" clientidmode="Static">
                                                                    <!--  <label for="textbox">Journal Name</label> -->
                                                                    <asp:TextBox ID="txtJournalName" runat="server" MaxLength="100" autocomplete="off" placeholder="Journal Name" class="form-control">
                                                                    </asp:TextBox>
                                                                    <ajax:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" FirstRowSelected="false"
                                                                        CompletionListCssClass="AutoCompletionList" ServiceMethod="GetCompititionLists" MinimumPrefixLength="1"
                                                                        CompletionListItemCssClass="AutoComp_listItem" TargetControlID="txtJournalName"
                                                                        CompletionInterval="10" OnClientShown="PopupShown" EnableCaching="false" CompletionSetCount="1"
                                                                        CompletionListHighlightedItemCssClass="AutoComp_itemHighlighted" UseContextKey="true"
                                                                        ContextKey="<%# Eval(hdnCompetitionType.Value) %>">
                                                                    </ajax:AutoCompleteExtender>
                                                                    <asp:RequiredFieldValidator ID="rfdJournal" runat="server" ControlToValidate="txtJournalName" Display="Dynamic"
                                                                        ValidationGroup="validationAchiv" ErrorMessage="Please enter Journal Name." ForeColor="Red">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group" id="divAdditionalAwrd" runat="server" visible="false" clientidmode="Static">
                                                                    <asp:TextBox ID="txtAdditionalAwrd" runat="server" MaxLength="100" autocomplete="off" placeholder="Additional Award"
                                                                        class="form-control">
                                                                    </asp:TextBox>
                                                                </div>
                                                                <div class="form-group" id="divDescription" runat="server" visible="false" clientidmode="Static">
                                                                    <textarea rows="5" id="txtAchivDescription" clientidmode="Static" runat="server" maxlength="500" placeholder="Description" class="form-control" />
                                                                </div>
                                                                <p>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlMilestone" ForeColor="Red"
                                                                        Display="Dynamic" ValidationGroup="validationAchiv" ErrorMessage="Please select milestone."><%--InitialValue="Type of Milestone"--%>
                                                                    </asp:RequiredFieldValidator>
                                                                </p>
                                                                <p></p>
                                                            </div>
                                                            <div class="submit-con  mt-0">
                                                                <asp:LinkButton ID="lnkSkip" runat="server" ClientIDMode="Static" CssClass="add-scroller btn btn-outline-primary m-r-15"
                                                                    Text="Skip" Visible="false" OnClick="lnkCancelAchivemnt_Click">
                                                                </asp:LinkButton>
                                                                <%--OnClientClick="javascript:callCancelAch();"--%>
                                                                <%--<asp:LinkButton ID="lnkContinue" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Text="Continue" 
                       OnClick="lnkContinue_Click" Visible="false">
                      </asp:LinkButton>--%>
                                                                <%--OnClientClick="javascript:callContinueAch();"--%>
                                                                <asp:LinkButton ID="lnkSaveAchivemnt" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Text="Next"
                                                                    ValidationGroup="validationAchiv" Visible="false" OnClick="lnkSaveAchivemnt_Click" OnClientClick="javascript:callSaveAch();">
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-body login-container text-left main-section w-100 position-static background-white" id="divlstAchievement" runat="server" style="display: none;">
                                                    <h3 class="text-center" id="divHeadingLstAchievement" runat="server">Welcome to Skorkel</h3>

                                                    <div class="mt-4 login-form-con w-100">
                                                        <div class="progess-con" id="divHeaderLstAchievement" runat="server">
                                                            <span class="grey-text">Step 2 of 2</span>
                                                            <h4>Achievements</h4>
                                                            <div class="progress">
                                                                <div class="progress-bar bg-primary" role="progressbar" style="width: 100%" aria-valuenow="50" aria-valuemin="0"
                                                                    aria-valuemax="100">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!--   <h5 class="text-center mb-2">Your Achievements</h5> -->
                                                        <div class="home-nav-tab">
                                                            <div class="custom-nav-container">
                                                                <div class="submit-con background-white m-0 p-0 pb-1" id="submit_con" runat="server">
                                                                    <asp:LinkButton ID="lnkAddMoreAchievement" runat="server" ClientIDMode="Static" CssClass="" Text="Add more Achievement"
                                                                        Visible="false" OnClick="lnkAddMoreAchievement_Click">
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <%--<a href="#" class="add-more-achivements">Add more achivement</a>--%>
                                                                <div class="card-list-con  background-white" id="card_list_con" runat="server" visible="false">

                                                                    <asp:HiddenField ID="hdnintacheIdelet" Value="" ClientIDMode="Static" runat="server" />
                                                                    <asp:HiddenField ID="hdnstracheDescriptiondel" Value="" ClientIDMode="Static" runat="server" />
                                                                    <asp:ListView runat="server" ID="lstAchivement" OnItemDataBound="lstAchivement_ItemDataBound" CellPadding="9"
                                                                        OnItemCommand="lstAchivement_ItemCommand">
                                                                        <ItemTemplate>
                                                                            <div class="card top-list box-shadow-none border-1px ">
                                                                                <div class="post-con">
                                                                                    <div class="post-body">
                                                                                        <span class="more-btn float-right" runat="server" id="divAchivementED">
                                                                                            <span class="dropdown">
                                                                                                <a href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                                    <img src="images/more.svg" alt="" class="more-btn">
                                                                                                </a>
                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span id="updaAchi" class="dropdown-menu updaAchi" aria-labelledby="dropdownMenuLink" x-placement="bottom-start"><asp:HiddenField ID="hdnintachIdelet" Value='<%# Eval("intAchivmentId") %>' ClientIDMode="Static" runat="server" />
                                                                                                    <!--                  <asp:LinkButton ID="lnkEditDoc" runat="server" Font-Underline="false" Visible="true" ToolTip="Edit" Text="Edit"
                                class="hide-body-scroll dropdown-item" CommandName="EditAch" CausesValidation="false">
                                <span class="lnr lnr-pencil"></span> Edit
                               </asp:LinkButton> -->
                                                                                                    <asp:LinkButton ID="lnkDeleteDoc" runat="server" Visible="true" ToolTip="Delete" Text="Delete" class="dropdown-item hide-body-scroll"
                                                                                                        OnClientClick="javascript:docdelete();return false;" CommandName="DeleteAch" CausesValidation="false">
                                <span class="lnr lnr-trash"></span> Delete                           
                                                                                                    </asp:LinkButton>
                                                                                                </span>
                                                                                            </span>
                                                                                        </span>
                                                                                        <asp:HiddenField ID="hdnintAchivmentId" Value='<%#Eval("intAchivmentId") %>' runat="server" ClientIDMode="Static" />
                                                                                        <asp:HiddenField ID="hdnintAddedBy" Value='<%#Eval("intAddedBy") %>' runat="server" />
                                                                                        <h3 runat="server" visible='<%# String.IsNullOrEmpty(Eval("strMilestone").ToString()) ? false : true %>'>
                                                                                            <asp:Label ID="lblMilestone" runat="server" Text='<%#Eval("strMilestone") %>' /></h3>
                                                                                        <ul class="small-datee">
                                                                                            <li runat="server" visible='<%# String.IsNullOrEmpty(Eval("strCompititionName").ToString()) ? false : true %>'>
                                                                                                <asp:Label ID="lblstrCompititionName" runat="server" Text='<%#Eval("strCompititionName") %>' /></li>
                                                                                            <li runat="server" visible='<%# String.IsNullOrEmpty(Eval("strPosition").ToString()) ? false : true %>'>
                                                                                                <asp:Label ID="lblstrPosition" runat="server" Text='<%#Eval("strPosition") %>' /></li>
                                                                                            <li runat="server" visible='<%# String.IsNullOrEmpty(Eval("strAdditionalAward").ToString()) ? false : true %>'>
                                                                                                <asp:Label ID="lblAdditionalAward" runat="server" Text='<%#Eval("strAdditionalAward") %>' /></li>
                                                                                        </ul>

                                                                                        <p class="mt-1" runat="server" visible='<%# String.IsNullOrEmpty(Eval("strDescription").ToString()) ? false : true %>'>
                                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("strDescription") %>' />
                                                                                        </p>

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>

                                                                    <div class="submit-con mt-0">
                                                                        <%--<asp:LinkButton ID="lnkSkipFinal" runat="server" ClientIDMode="Static" CssClass="add-scroller btn btn-outline-primary m-r-15"
                         Text="Skip" OnClick="lnkCancelAchivemnt_Click">
                        </asp:LinkButton>--%>
                                                                        <asp:LinkButton ID="lnkFinish" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Text="Finish"
                                                                            OnClick="lnkFinish_Click"><%--ValidationGroup="validationAchiv"--%> 
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
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

            <!---Sucess Popup-->
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
                            <asp:Button ClientIDMode="Static" class="btn btn-primary add-scroller" ID="btnOk" runat="server" Text="Ok" OnClientClick="javascript:CloseMsg();"></asp:Button><%--OnClick="btnOk_click"--%>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divDeletesucess" runat="server" class="modal backgroundoverlay" clientidmode="Static" style="display: none;">
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
        </div>
    </form>
</body>
</html>
<script>
    $(document).ready(function () {
        setFocus();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            setFocus();
        });
    });
    $(document).on('click', '.skip-close', function () {
        $(".close-popup-skip").hide();
        $('.disclaimer-popup').show();
    });
    $(document).on('click', '.close-page-popup', function () {
        $('.disclaimer-popup').hide();
    });
</script>
