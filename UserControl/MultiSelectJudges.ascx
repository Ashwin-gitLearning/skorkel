<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultiSelectJudges.ascx.cs" Inherits="UserControl_MultiSelectJudges" %>
<asp:UpdatePanel runat="server" ID="multi1" updateMode="Conditional">
    <ContentTemplate>
        <div class="multiselect-scroller">
            <div class="search-field dumyinput d-none">
                <div  id="dvdummyinput1" runat="server" clientidmode="static" class="w-100" ></div>
            </div>
            <select id="selectControl1" runat="server" multiple class="chosen-select d-none" data-placeholder='<%# selectPlaceholder %>'>
            </select>
        </div>    
    </ContentTemplate>
</asp:UpdatePanel>
 <script src="<%=ResolveUrl("../js/chosen.jquery.min.js")%>" type="text/javascript"></script>
<script>
    function createChosen(chosenOnChange) {
        var chosenVar = $('.chosen-select').chosen({ allow_single_deselect: true });
        $('.dumyinput').hide();
        if (chosenOnChange) {
            chosenVar.change(chosenOnChange);
        }
       
    }

    $(document).ready(function () {
        $('.chosen-select').chosen({ allow_single_deselect: true });
        $('.dumyinput').hide();
    });
</script>

