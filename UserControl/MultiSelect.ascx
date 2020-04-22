<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MultiSelect.ascx.cs" Inherits="UserControl_MultiSelect" %>
<asp:UpdatePanel runat="server" ID="multi" >
    <ContentTemplate>
        <div class="multiselect-scroller">
            <div class="search-field dumyinput d-none">
                <div  id="dvdummyinput" runat="server" clientidmode="static" class="w-100" ></div>
            </div>
            <select id="selectControl" runat="server" multiple class="chosen-select d-none" data-placeholder='<%# selectPlaceholder %>'>
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

