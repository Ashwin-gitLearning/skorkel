<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="my-points.aspx.cs"
   Inherits="my_points" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headMain" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div class="main-section-inner">
      <div class="panel-cover clearfix">
         <div class="full-width-con">
            <div> <h5 class="card-title float-left">My Score</h5></div><div class="clearfix"></div>
            <div class="row">
               <!---Score div-->
               <div class="skorkelBox for_score col-sm-12">
                  <div class="card score-board">

                     <div class="score-con d-flex">
                        <div><img src="images/score-icon-1.svg" alt=""></div>
                        <div class="score-number d-flex">
                             <div class="score-value"><asp:Label ID="lbltotalScore" runat="server"></asp:Label></div>
                             <div class="your-score">Your Score</div>
                             <div class="good-bad-con">
                             <span class="badge badge-success">Good</span>
                             </div>
                        </div> 
                     </div> 

                     <div class="score-personal">
                         <ul class="list-inline">                 
                             <li class="list-inline-item ">
                                 <div class="box">
                                     <div>
                                       <img src="images/score-avatar.svg" alt="">
                                     </div>
                                     
                                     <div class="fact-con">
                                         <div id="defactoPoint" runat="server" class="fact-no"></div>
                                         <div  class="fact-text">De Facto</div>
                                     </div>
                                 </div>
                             </li>
                             
                             <li class="list-inline-item ">
                                 <div class="box">
                                     <div>
                                       <img src="images/score-pluse.svg" alt="">
                                     </div>
                                     
                                     <div class="fact-con">
                                         <div id="persPoints" runat="server" class="fact-no"></div>
                                         <div class="fact-text">Personal</div>
                                     </div>
                                 </div>
                             </li>  
                         </ul>
                     </div>

                  </div>
               </div>
               <!---Score div Ended-->
               <div class="clearfix"></div>
               <div id="divpoint" runat="server" class="col-sm-12 score-tab">
                  <div class="score-tab-inner">
                     <div class="tabBoxSk" id="facto">
                        <div class="cls"></div>
                        <ul class="custom-nav-control nav nav-tabs">
                           <li class="nav-item">   
                              <a href="#" class="cursor-pointer nav-link active">
                                 De Facto
                              </a>   
                           </li>
                           <li class=" nav-item " onclick="showpersonal()">
                              <a href="#" class="cursor-pointer nav-link">
                                 Personal
                              </a>
                           </li>
                           <div class="cls"></div>
                        </ul>
                        <div class="defactoBox">
                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                              <ContentTemplate>
                                 <div class="custom-nav-container">
                                    <div class=" row score-grid" id="categoryTxt">
                                       <asp:ListView ID="lstDeFacto" runat="server" OnItemDataBound="lstDeFacto_ItemDataBound">
                                          <ItemTemplate>
                                             <div class="col-sm-3 score-cover">
                                                <div class="card">
                                                   <div class="score-box">
                                                   
                                                      <asp:Label ID="lblEducation"  class="heading" runat="server" Text='<%#Eval("Education")%>'></asp:Label>
                                                  
                                                      <span class="score-icon-con">
                                                       <!--   <span><img src="images/graph.svg" alt=""></span> -->

                                                         <asp:Label ID="lblintScore" runat="server" class="score-icon-text" Text='<%#Eval("Points")%>'></asp:Label>
                                                      </span>
                                                   </div>
                                                </div>
                                             </div>
                                          </ItemTemplate>
                                       </asp:ListView>
                                    </div>
                                 </div>
                                 <div class="cls">
                                 </div>
                              </ContentTemplate>
                           </asp:UpdatePanel>
                        </div>
                     </div>
                     <!--de facto ends-->
                     <!--de personal starts-->
                     <div class="tabBoxSk" id="personal">
                        <div class="cls"></div>
                        <div class="custom-nav-control nav nav-tabs">
                           <li class="nav-item">
                              <a class="cursor-pointer nav-link " onclick="showfacto()">
                                 De Facto
                              </a>
                           </li>
                           <li class="nav-item">
                              <a class="cursor-pointer nav-link active">
                                 Personal
                              </a>
                           </li>
                           <div class="cls"></div>
                        </div>

                        <div class="defactoBox custom-nav-container">
                           <div class=" row score-grid" id="categoryTxt">
                           <div class="col-sm-3 score-cover">
                              <div class="card">
                                 <div class="score-box">
                                    <span class="heading">
                                       Facts
                                    </span>
                                    <span class="score-icon-con">
                                      <!--  <span><img src="images/graph.svg" alt=""></span> -->
                                       <asp:Label ID="lblFactScore" runat="server" class="score-icon-text"  Text="0" > </asp:Label>
                                    </span><span class="clearfix"></span>
                                    <span class="subcount">
                                       <asp:Label ID="lblFactCount" runat="server" Text="0 Facts" > </asp:Label>
                                    </span>
                                 </div>
                              </div>
                           </div>
                           <div class="col-sm-3 score-cover">
                               <div class="card">
                                 <div class="score-box">
                                    <span class="heading">
                                       Issue
                                    </span>
                                    <span class="score-icon-con">
                                       <!-- <span><img src="images/graph.svg" alt=""></span> -->
                                       <asp:Label ID="lblIssueScore" runat="server" class="score-icon-text" Text="0" > </asp:Label>
                                    </span>
                                    <span class="subcount">
                                       <asp:Label ID="lblIssueCount" runat="server" Text="0 Issues" > </asp:Label>
                                    </span>
                                 </div>   
                               </div>     
                           </div>
                             <div class="col-sm-3 score-cover">
                               <div class="card">
                                 <div class="score-box">
                                    <span class="heading">
                                       Obiter Dictum
                                    </span>
                                     <span class="score-icon-con">
                                       <!-- <span><img src="images/graph.svg" alt=""></span> -->
                                       <asp:Label ID="lblOrbiterScore" runat="server" class="score-icon-text" Text="0" > </asp:Label>
                                     </span>
                                    <span class="subcount">
                                       <asp:Label ID="lblOrbiterCount" runat="server" Text="0 Obiter Dicta" > </asp:Label>
                                    </span>
                                 </div>
                              </div>      
                           </div>
                           <div class="col-sm-3 score-cover">
                               <div class="card">
                                 <div class="score-box">
                                    <span class="heading">
                                       Precedent
                                    </span>
                                    <span class="score-icon-con">
                                      <!--  <span><img src="images/graph.svg" alt=""></span> -->
                                       <asp:Label ID="lblPrecedentScore" runat="server" class="score-icon-text" Text="0" > </asp:Label>
                                    </span>
                                    <span class="subcount">
                                       <asp:Label ID="lblPrecedentCount" runat="server" Text="0 Precedents" > </asp:Label>
                                    </span>
                                 </div>
                              </div>      
                           </div>
                           <div class="col-sm-3 score-cover">
                               <div class="card">
                                 <div class="score-box">
                                    <span class="heading">
                                       Ratio Decidendi
                                    </span>
                                    <span class="score-icon-con">
                                      <!--  <span><img src="images/graph.svg" alt=""></span> -->
                                       <asp:Label ID="lblRatioScore" runat="server"  class="score-icon-text" Text="0" > </asp:Label>
                                    </span>
                                    <span class="subcount">
                                       <asp:Label ID="lblRatioCount" runat="server" Text="0 Rationes Decidendi" > </asp:Label>
                                    </span>
                                 </div>
                              </div>      
                           </div>
                           <div class="col-sm-3 score-cover">
                              <div class="card">
                                 <div class="score-box">
                                    <span class="heading">
                                       Important Paragraph
                                    </span>
                                    <span class="score-icon-con">
                                       <!-- <span><img src="images/graph.svg" alt=""></span> -->
                                       <asp:Label ID="lblImportScore" runat="server" class="score-icon-text" Text="0" > </asp:Label>
                                    </span>
                                    <span class="subcount">
                                       <asp:Label ID="lblImportCount" runat="server" Text="0 Important Paragraphs" > </asp:Label>
                                    </span>
                                 </div>
                              </div>      
                           </div>
                           <div class="col-sm-3 score-cover">
                              <div class="card">
                                 <div class="score-box">
                                    <span class="heading">
                                       Summary
                                    </span>
                                    <span class="score-icon-con">
                                       <!-- <span><img src="images/graph.svg" alt=""></span> -->
                                       <asp:Label ID="lblSummaryScore" runat="server" class="score-icon-text" Text="0" > </asp:Label>
                                    </span>
                                    <span class="subcount">
                                       <asp:Label ID="lblSummaryCount" runat="server" Text="0 Summaries" > </asp:Label>
                                    </span>
                                 </div>
                              </div>      
                           </div>
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
               <div class="cls">
               </div>
            </div>
            <!--left box ends-->
         </div>
         <!--left verticle search list ends-->
      </div>
      <!--left verticle search list ends-->
   </div>
   <script language="javascript">
       function showfacto() {
            $('#personal').hide();
          // $('#personal').removeClass('active')
           $('#facto').show();
          // $('#facto').addClass('active')

          //document.getElementById('facto').style.display = "block";
          //document.getElementById('personal').style.display = "none";
      }
       function showpersonal() {
           $('#facto').hide();
        //   $('#facto').removeClass('active')
           $('#personal').show();
         //  $('#personal').addClass('active')
          //document.getElementById('personal').style.display = "block";
          //document.getElementById('facto').style.display = "none";

      }
       $(document).on('click', '.mobile_tab_icon', function() {
             $('.mobile_m_skorkel').slideToggle('slow');
            });

      $(".score-tab-inner li a").click(function() {
     //  $(this).parent().addClass('active').siblings().removeClass('active');

       });
   </script>
</asp:Content>