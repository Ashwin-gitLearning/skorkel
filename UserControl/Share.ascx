<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Share.ascx.cs" Inherits="UserControl_Share" %>
    <meta charset="utf-8">
    <div id="GrpPopUpShare" class="modal backgroundoverlay" runat="server" clientidmode="Static">
        <asp:UpdatePanel ID="up1" runat="server" class="modal-dialog modal-dialog-centered">
            <ContentTemplate>
                <div class="w-100">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"><asp:Label ID="lblTitle" Text="Share" runat="server"></asp:Label></h5>
                            <button type="button" id="close-popup" class="close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <asp:Label ID="lblTitleGroup" runat="server"></asp:Label>
                        <div class="modal-body group_text">
                            <div id="tdDepartment" class="form-group position-relative">
                                <label for="member">To</label>
                                <select data-placeholder="Enter members name here" class="chosen-select form-control" id="txtInviteMembers" onchange="getMultipleValues(this.id)" runat="server" multiple tabindex="4">
                                </select>
                                <asp:HiddenField ID="hdnInvId" ClientIDMode="Static" runat="server" />
                                <asp:Label ID="lblMess" ForeColor="Red" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="form-group">
                                <label for="Message">Message</label>
                                <textarea id="txtBody" runat="server" cols="20" rows="2" value="Message" onblur="if(this.value=='') this.value='Message';" onfocus="if(this.value=='Message') this.value='';" class="forumTitle form-control"></textarea>
                            </div>
                            <div class="form-group">
                                <label for="member">Link</label>
                                <asp:TextBox ID="txtLink" runat="server" value="Paste link" onblur="if(this.value=='') this.value='Paste link';" onfocus="if(this.value=='Paste link') this.value='';" class="forumTitlenew form-control" 
                        Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        
                        <asp:Label ID="lblMessAccept" runat="server"></asp:Label>
                        <asp:Label ID="lblMessReject" runat="server"></asp:Label>
                        <div class="modal-footer border-top-0">
                            <a onclick="CloseGroupPopup();" class="cancel_btn btn add-scroller btn-outline-primary" causesvalidation="false">
                                        Cancel</a>
                            <asp:LinkButton ID="lnkPopupOK" runat="server" ClientIDMode="Static" Text="Share" CssClass="joinBtn default_btn btn btn-primary add-scroller" OnClick="lnkPopupOK_Click"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkPopupOK" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
    function CloseGroupPopup() {
        document.getElementById("GrpPopUpShare").style.display = 'none';
    }
    </script>
    </div>
    <script type="text/javascript">
    function getMultipleValues(ctrlId) {
        $('#tdDepartment').find('label.error').remove();
        var control = document.getElementById(ctrlId);
        var strSelText = '';
        var cnt = 0;
        for (var i = 0; i < control.length; i++) {
            if (control.options[i].selected) {
                if (cnt == 0) {
                    strSelText += control.options[i].value;
                } else {
                    strSelText += ',' + control.options[i].value;
                }
                cnt++;
            }
        }
        $('#hdnInvId').val(strSelText);
    }
    </script>