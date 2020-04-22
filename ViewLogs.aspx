<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="ViewLogs.aspx.cs" Inherits="ViewLogs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" Runat="Server">
<!--   <link href="css/stylever-2.css" rel="stylesheet" type="text/css" />-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

  <div class="main-section-inner">
    <div class="panel-cover clearfix">
        <div class="full-width-con">
            
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <asp:ListView ID="lstViewLog" runat="server" OnItemDataBound="lstViewLog_ItemDataBound" OnItemCreated="lstViewLog_ItemCreated">
                <ItemTemplate>
                   
                    <div class="activity-cover">
                   <span class="date-clip"><asp:Label ID="lblDeletedOn" Text='<%#Eval("dtDeletedOn") %>' runat="server" ClientIDMode="Static"></asp:Label></span>
                    
                   <asp:ListView ID="lstChildViewLog" runat="server" OnItemCommand="lstChildViewLog_ItemCommand" DataKeyNames="LogId">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnLogId" Value='<%# Eval("LogId") %>' runat="server" />
                                <asp:HiddenField ID="hdnRegID" Value='<%# Eval("intDeletedBy") %>' runat="server" />
                                <asp:HiddenField ID="hdnstrAction" Value='<%# Eval("strAction") %>' runat="server" />
                                <asp:HiddenField ID="hdnstrActionName" Value='<%# Eval("strActionName") %>' runat="server" />
                                    <div class="card card-list-con">
                                    <div class="top-list activity-card">
                                    <div class="post-con">
                                        
                                        <div class="post-header">
                                         <span class="question-icon">
                                          <span class="icon">
                                           <asp:Image id="iconImage" runat="server" class="img-fluid rounded-circle" />
                                           <asp:Label ID="txtIcon" runat="server" Visible="false"></asp:Label>         
                                          </span>
                                         </span>

                                          <ul class="que-con">
                                           <li><asp:Label ID="lbldtlIcons" runat="server" class="remove-after-comma"></asp:Label>
                                               <asp:Label ID="lblnotificationname" class="remove-after-comma" runat="server"></asp:Label>
                                            <strong>
                                             <asp:LinkButton ID="lnkName" Text='<%# Eval("Deletedby") %>' ClientIDMode="Static" CommandName="Details" 
                                              ToolTip="View Profile" class="view_log_fonts" runat="server"></asp:LinkButton>
                                            </strong>
                                           </li>
                                           <li>
                                            <asp:Label ID="lblTime" Text='<%# Eval("DeletedTime") %>' ClientIDMode="Static" class="view_log_fonts" runat="server">
                                            </asp:Label>
                                           </li>
                                          </ul>
                                        </div><!-- post-header ended -->         
                         
                                        <div class="post-body">
                                         <p><asp:Label ID="lblComment" Text="" Visible="false" runat="server" ClientIDMode="Static"></asp:Label></p>
                                        </div><!-- post-body ended -->                        
                                  
                                    <asp:LinkButton ID="lnkShareDetail" CommandName="ShareDetails" class="view_log_fonts" runat="server" ClientIDMode="Static"></asp:LinkButton>
                                  
                                </div><!-- post-con ended -->
                              </div>
                            </div><!-- card ended -->
                    
                               
                          </ItemTemplate>
                        </asp:ListView>
                        
                    </div><!-- activity-cover ended -->
            
                </ItemTemplate>
            </asp:ListView>


            <div id="dvHeight" runat="server" class="adv">
            </div>
        </div>
        <!-- full-width-con ended -->
    </div>
    <!-- panel-cover ended -->
</div>
      <!-- main-section-inner ended -->
  
 
   <!--pagination starts-->
   <!--pagination ends-->
   <asp:HiddenField ID="hdnTotalItem" runat="server" ClientIDMode="Static" />
   <asp:HiddenField ID="hdnCurrentPage" runat="server" ClientIDMode="Static" Value="1" />
   <asp:HiddenField ID="hdnfullname" ClientIDMode="Static" runat="server" />
   <asp:HiddenField ID="hdnEmailId" ClientIDMode="Static" runat="server" />
 
</asp:Content>