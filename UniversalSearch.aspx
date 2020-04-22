<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="UniversalSearch.aspx.cs" Inherits="UniversalSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="main-section-inner">
                    
  <div class="panel-cover clearfix">
   <div class="full-width-con">
                            
    <div class="btn-title-con m-t-15-minus">
     <div>
      <h5 class="card-title">Search Results for '<asp:Label ID="lblSearch" runat="server" />'
      </h5>
     </div>
    </div>
     <!-- btn-title ended -->
    
    <div class="card card-list-con blog-list m-b-50"  id="divCases" runat="server" visible="false">
     <div class="list-group-item top-list">
         
      <div class="post-con">
       <div class="post-header d-flex justify-content-between">
        <div>
         <span class="question-icon">
          <span class="icon"><img src="images/file.svg"></span>
         </span>
         <ul class="que-con">
          <li>Case</li>
         </ul>
        </div>
        <div>
 <!--         <span class="float-right">
          <asp:LinkButton ID="lnkCasesResults" runat="server" Text="View More Results" OnClick="lnkCasesResults_Click"></asp:LinkButton>
         </span> -->
        </div>
       </div>
       <asp:HiddenField ID="hdnMaxcount" runat="server" ClientIDMode="Static" Value="" />
       <asp:UpdatePanel ID="pnlParentCasesResults" runat="server">
        <ContentTemplate>
         <asp:ListView ID="lstParentCasesResults" runat="server" OnItemCommand="lstParentCasesResults_ItemCommand">
          <ItemTemplate> 
           <asp:HiddenField ID="hdnPostCaseID" runat="server" ClientIDMode="Static" Value='<%#Eval("docUid")%>' />
           
           <div class="case-list">
            <div class="post-body mt-3">
             <h3 class="mb-1">
               <%--Text='<%#Eval("strQuestionDescription")%>' CommandName="QuestionDetails"--%>
              <asp:LinkButton ID="lnkCaseTitle" runat="server" class="black-text-anchor" Text='<%#Eval("title")%>' CommandName="CaseDetails">               
              </asp:LinkButton>
             </h3>
             <span class="date" runat="server" Visible='<%# String.IsNullOrEmpty(Eval("court").ToString()) && String.IsNullOrEmpty(Eval("year").ToString()) ? false : true %>'>
              <asp:Label ID="lblCourtState" runat="server" ClientIDMode="Static" Text='<%#Eval("court")%>'></asp:Label>&nbsp;
              <asp:Label ID="lblCourtYear" runat="server" ClientIDMode="Static" Text='<%#Eval("year")%>'></asp:Label>
             </span>
            </div>
            <div class="post-footer  pb-2">
             <ul class="list-inline">
              <%--<li class="list-inline-item">
               <a href="#" class="active-toogle"><span class="share-btn"></span> 5 Shares</a>
              </li>
              <li class="list-inline-item">
               <a href="#" class="active-toogle"><span class="download-view"></span> 5 Downloads</a>
              </li>
              <li class="list-inline-item">
               <a href="#" class="active-toogle"><span class="tag-btn"></span> 5 Tags</a>
              </li>--%>
              <li class="list-inline-item"><div class="active-toogle un-anchor"><span class="share-btn"></span><asp:Label ID="lblshareCount" runat="server" Text='<%#Eval("shareCount") + (((int)Eval("shareCount") == 1) ? " Share" : " Shares") %>'></asp:Label></div></li>
              <li class="list-inline-item"><div class="active-toogle un-anchor"><span class="download-view"></span><asp:Label ID="lbldownloadCount" runat="server" Text='<%#Eval("downloadCount") + (((int)Eval("downloadCount") == 1) ? " Download" : " Downloads") %>'></asp:Label></div></li>
              <li class="list-inline-item"><div class="active-toogle un-anchor"><span class="tag-btn"></span><asp:Label ID="lbltagCnt" runat="server" Text='<%#Eval("tagCnt") + ((Eval("tagCnt").ToString() == "1") ? " Tag" : " Tags") %>'></asp:Label></div></li>
             </ul>
            </div>
           </div>
            <!-- case list ended -->
          </ItemTemplate>
         </asp:ListView>
        </ContentTemplate>
       </asp:UpdatePanel>

       <%--<div class="case-list">
        <div class="post-body">
         <h3><a href="research-detail.html">RADHAKANTA BHOI AND OTHERS</a></h3>
         <span class="date">IN THE HIGH COURT OF ORISSA 2016</span>
        </div>
        <div class="post-footer">
         <ul class="list-inline">
          <li class="list-inline-item"><a href="#" class="active-toogle"><span class="share-btn"></span> 5 Shares</a></li>
          <li class="list-inline-item"><a href="#" class="active-toogle"><span class="download-view"></span> 5 Downloads</a></li>
          <li class="list-inline-item"><a href="#" class="active-toogle"><span class="tag-btn"></span> 5 Tags</a></li>
         </ul>
        </div>
       </div>--%>
        <!-- case list ended -->

       <%--<div class="case-list">
        <div class="post-body">
         <h3><a href="research-detail.html">RADHAKANTA BHOI AND OTHERS</a></h3>
         <span class="date">IN THE HIGH COURT OF ORISSA 2016</span>
        </div>
        <div class="post-footer">
         <ul class="list-inline">
          <li class="list-inline-item"><a href="#" class="active-toogle"><span class="share-btn"></span> 5 Shares</a></li>
          <li class="list-inline-item"><a href="#" class="active-toogle"><span class="download-view"></span> 5 Downloads</a></li>
          <li class="list-inline-item"><a href="#" class="active-toogle"><span class="tag-btn"></span> 5 Tags</a></li>
         </ul>
        </div>
       </div>--%>
        <!-- case list ended -->

       <div class="case-list border-top-1px">
        <div class="padding-15 text-center">
         <asp:LinkButton ID="lnkCasesResultsBottom" runat="server" EnableViewState="false" class="view-more-universal" Text="View More Results" OnClick="lnkCasesResultsBottom_Click"></asp:LinkButton>
        </div>
       </div>        
      </div>
       <!-- post con ended here --> 
         
     </div>    
    </div>
     <!-- card-list-con ended -->    
      
    <div class="card card-list-con blog-list m-b-50" id="divQuestions" runat="server" visible="false">
     <div class="list-group-item top-list">
                                
      <div class="post-con">                                        
       <div class="post-header d-flex justify-content-between">
        <div>
         <span class="question-icon">
          <span class="icon"> ?</span>
         </span>
         <ul class="que-con">
          <li>Questions</li>
         </ul>
        </div>
        <div>
         <%--<span class="float-right"><a href="#">See All 5 Results</a></span>--%>
      <!--    <span class="float-right">
          <asp:LinkButton ID="lnkResults" runat="server" Text="View More Results" OnClick="lnkResults_Click"></asp:LinkButton>
         </span> -->
        </div>
       </div>
       <asp:UpdatePanel ID="pnlParentResults" runat="server">
        <ContentTemplate>
         <asp:ListView ID="lstParentResults" runat="server" OnItemDataBound="lstParentResults_ItemDataBound" OnItemCommand="lstParentResults_ItemCommand">
          <ItemTemplate> 
           <asp:HiddenField ID="hdnPostQuestionID" runat="server" Value='<%# Eval("intPostQuestionId") %>' ClientIDMode="Static" />
           <asp:HiddenField ID="hdnAddedBy" runat="server" Value='<%#Eval("intAddedBy")%>' />
           <div class="case-list">       
            <div class="post-body">
             <h3 class="mb-1">
              <%--<a href="research-detail.html" class="black-text-anchor"></a>--%>
              <asp:LinkButton ID="lnkQuestionTitle" runat="server" class="black-text-anchor" Text='<%#Eval("strQuestionDescription")%>' CommandName="QuestionDetails">               
              </asp:LinkButton>          
             </h3>
             <%--<span class="date">4 Jun</span>--%>
             <asp:Label ID="lblDate" runat="server" class="date" Text='<%#Eval("dtAddedOn")%>'></asp:Label>
            </div>
            <div class="post-footer">
             <ul class="list-inline">
              <li class="list-inline-item">
               <%--<a href="#" class="active-toogle"><span class="like-btn"></span> 10 Likes</a>--%>
               <%--<asp:LinkButton ID="lnkLikebtn" runat="server" class="active-toogle"><span class="like-btn"></span> 10 Likes</asp:LinkButton>--%>
               <span class="d-flex align-items-center">
                <asp:LinkButton ID="lnkLikebtn" runat="server" class="active-toogle" Font-Underline="false">
                 <span class="like-btn"></span>               
                </asp:LinkButton>
                
                <span class="d-flex"><%--Text='<%#Eval("Likes") %>'--%>
                    <%--Text='<%# ((int)Eval("Likes") == 1) ? "Like" : "Likes" %>'--%>
                    <%--Text='<%#Eval("Shares") %>'
                    Text='<%# ((int)Eval("Shares") == 1) ? "Share" : "Shares" %>'
                    Text='<%#Eval("Answers") %>'
                    Text='<%# ((int)Eval("Answers") == 1) ? "Answer" : "Answers" %>'--%>
                 <asp:Label ID="lblLikePost" runat="server"
                  ClientIDMode="Static"></asp:Label>&nbsp;
                  <%--<asp:Label ID="lblLikePostText" runat="server" ClientIDMode="Static"></asp:Label>--%>
                </span>
               </span>
              </li>
              <li class="list-inline-item">
               <%--<a href="#" class="active-toogle" data-toggle="modal" data-target="#sharemodal">
                <span class="share-btn"></span> 5 Shares</a>--%>
               <%--<asp:LinkButton ID="lnkSharebtn" runat="server" class="active-toogle" data-toggle="modal" data-target="#sharemodal">
                <span class="share-btn"></span> 5 Shares
               </asp:LinkButton>--%>
               <span class="d-flex align-items-center">
                <asp:LinkButton ID="lnkSharebtn" runat="server" class="active-toogle" Font-Underline="false" data-toggle="modal" data-target="#sharemodal">
                 <span class="share-btn"></span>               
                </asp:LinkButton>
                
                <span class="d-flex">
                 <asp:Label ID="lblShare" runat="server" 
                  ClientIDMode="Static"></asp:Label>&nbsp;
                  <%--<asp:Label ID="lblShareText" runat="server" ClientIDMode="Static"></asp:Label>--%>
                </span>
               </span>
              </li>
              <li class="list-inline-item">
               <%--<a href="#" class="active-toogle">
                <span class="edit-comment-btn"></span> 43 Answers</a>--%>
               <%--<asp:LinkButton ID="lnkAnswerbtn" runat="server" class="active-toogle">
                <span class="edit-comment-btn"></span> 43 Answers
               </asp:LinkButton>--%>
               <span class="d-flex align-items-center">
                <asp:LinkButton ID="lnkAnswerbtn" runat="server" class="active-toogle" Font-Underline="false">
                 <span class="edit-comment-btn"></span>               
                </asp:LinkButton>
                
                <span class="d-flex">
                 <asp:Label ID="lblAnswer" runat="server" 
                  ClientIDMode="Static"></asp:Label>&nbsp;
                  <%--<asp:Label ID="lblAnswerText" runat="server" ClientIDMode="Static"></asp:Label>--%>
                </span>
               </span>
              </li>
             </ul>
            </div>
           </div>
           <%--<div class="case-list">       
            <div class="post-body">
             <h4>
              <a href="research-detail.html" class="black-text-anchor">
               Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, 
               quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
              </a>
             </h4>
             <span class="date">4 Jun</span>
            </div>
            <div class="post-footer">
             <ul class="list-inline">
              <li class="list-inline-item"><a href="#" class="active-toogle"><span class="like-btn"></span> 10 Likes</a></li>
              <li class="list-inline-item"><a href="#" class="active-toogle" data-toggle="modal" data-target="#sharemodal"><span class="share-btn"></span> 5 Shares</a></li>
              <li class="list-inline-item"><a href="#" class="active-toogle"><span class="edit-comment-btn"></span> 43 Answers</a></li>
             </ul>
            </div>
           </div>
           <div class="case-list">
            <div class="post-body">
             <h4>
              <a href="research-detail.html" class="black-text-anchor">
               Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, 
               quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
              </a>
             </h4>
             <span class="date">4 Jun</span>
            </div>
            <div class="post-footer">
             <ul class="list-inline">
              <li class="list-inline-item"><a href="#" class="active-toogle"><span class="like-btn"></span> 10 Likes</a></li>
              <li class="list-inline-item"><a href="#" class="active-toogle" data-toggle="modal" data-target="#sharemodal"><span class="share-btn"></span> 5 Shares</a></li>
              <li class="list-inline-item"><a href="#" class="active-toogle"><span class="edit-comment-btn"></span> 43 Answers</a></li>
             </ul>
            </div>
           </div>--%>
          </ItemTemplate>
         </asp:ListView>
        </ContentTemplate>
       </asp:UpdatePanel>
       <div class="case-list border-top-1px">
        <div class="padding-15 text-center">
         <asp:LinkButton ID="lnkResultsBottom" class="view-more-universal" runat="server" Text="View More Results" OnClick="lnkResultsBottom_Click"></asp:LinkButton>
        </div>
       </div>
      </div>
       <!-- post con ended here -->                                
                                
     </div>    
    </div>
     <!-- card-list-con ended -->    
    
    <div class="card card-list-con blog-list m-b-50" id="divArticles" runat="server" visible="false">
     <div class="list-group-item top-list">         
      <div class="post-con">                 
       <div class="post-header d-flex justify-content-between">
        <div>
         <span class="question-icon">
          <span class="icon"><img src="images/file.svg"></span>
         </span>
         <ul class="que-con">
          <li>Journal Articles</li>
         </ul>
        </div>
        <div>
<!--          <span class="float-right">
          <asp:LinkButton ID="lnkArticleTopResults" runat="server" Text="View More Results" OnClick="lnkArticleResults_Click"></asp:LinkButton>
         </span> -->
        </div>
       </div>
          <asp:UpdatePanel ID="pnlParentArticlesResults" runat="server">
        <ContentTemplate>
          <asp:ListView ID="lstParentArticlesResults" runat="server" OnItemCommand="lstParentArticlesResults_ItemCommand"
              OnItemDataBound="lstParentArticlesResults_ItemDataBound">
                                <ItemTemplate>
                                    <div class="case-list">
                                        
                                                <asp:HiddenField ID="hdnArticleId" runat="server" Value='<%#Eval("ArticleID") %>' />
                                                <asp:HiddenField ID="hdnJournalId" runat="server" Value='<%#Eval("JournalID") %>' />
                                                <asp:HiddenField ID="hdnFilePath" runat="server" Value='<%# Eval("FilePath") %>' />
                                                <div class="post-body">

                                                    <h3 class="mb-1">
                                                        <asp:LinkButton ID="lnkArtTItle" CommandName="ArticleDetails" runat="server" class="user-name mb-1" Text='<%#Eval("ArtTitle") %>'></asp:LinkButton>
                                                    </h3>
                                                    <ul class ="small-date">
                                                        <li>
                                                            <span>
                                                                by 
                                                                <asp:LinkButton ID="lnkAddedBy" href='<%# "Profile2.aspx?RegId="+Eval("AddedByID") %>' runat="server" CssClass="remove-a-color" Text='<%# Eval("AddedByName") %>'></asp:LinkButton>

                                                            </span>
                                                        </li>
                                                        <li>
                                                            <span>
                                                                <%# Eval("JournalTitle") %>
                                                            </span>
                                                        </li>
                                                    </ul>
                                                    
                                                    
                                                 
                                                </div>
                                                <!-- post-body ended -->

                                                <div class="post-footer pt-0">
                                                    <ul class="list-inline">
                                                        <li class="list-inline-item"><span id="spnLike" runat="server"  class="active-toogle"><span class="like-btn"></span> <%#Eval("Likes") %> <%# ((int)Eval("Likes") == 1) ? "Like" : "Likes" %></span></li>
                                                        <li class="list-inline-item"><span href="#" class="active-toogle"><span class="comment-btn"></span><%#Eval("Comments") %> <%# ((int)Eval("Comments") == 1) ? "Comment" : "Comments" %></span></li>
                                                    </ul>
                                                </div>
                                                <!-- post-footer ended -->

                                      
                                    </div>
                                    <!-- case-list ended -->
                                </ItemTemplate>
                            </asp:ListView>
             </ContentTemplate>
       </asp:UpdatePanel>
       <div class="case-list border-top-1px">
        <div class="padding-15 text-center">
         <asp:LinkButton ID="lnkArticleBottomResults" class="view-more-universal" runat="server" Text="View More Results" OnClick="lnkArticleResults_Click"></asp:LinkButton>
        </div>
       </div>                     
      </div>
       <!-- post con ended here -->         
     </div>    
    </div>
     <!-- card-list-con ended -->    
    
    <div class="card card-list-con blog-list m-b-50" id="divBlogs" runat="server" visible="false">
     <div class="list-group-item top-list">         
      <div class="post-con">             
       <div class="post-header d-flex justify-content-between">
        <div>
         <span class="question-icon">
          <span class="icon"><img src="images/file.svg"></span>
         </span>
         <ul class="que-con">
          <li>Blogs</li>
         </ul>
        </div>
        <div>
        <%--<span class="float-right"><a href="#">See All 5 Results</a></span>--%>
    <!--      <span class="float-right">
          <asp:LinkButton ID="lnkBlogResults" runat="server" Text="View More Results" OnClick="lnkBlogResults_Click"></asp:LinkButton>
         </span> -->
        </div>
       </div>
       
       <asp:UpdatePanel ID="pnlParentBlogsResults" runat="server">
        <ContentTemplate>
         <asp:ListView ID="lstParentBlogsResults" runat="server" OnItemCommand="lstParentBlogsResults_ItemCommand"><%--OnItemDataBound="lstParentResults_ItemDataBound" --%>
          <ItemTemplate> 
           <asp:HiddenField ID="hdnPostBlogID" runat="server" ClientIDMode="Static" Value='<%# Eval("intBlogId") %>'/>
           <asp:HiddenField ID="hdnAddedBy" runat="server" Value='<%#Eval("intAddedBy")%>' />
            <div class="case-list">
             <div class="post-body mt-3">
              <%--<a href="#"><h4>5 Reasons Why People Like Civil Law.</h4></a>--%> 
               <h3 class="mb-1">
                <asp:LinkButton ID="lnkBlogsTitle" runat="server" class="black-text-anchor" CommandName="BlogDetails" Text='<%#Eval("strBlogHeading")%>'>
                </asp:LinkButton>
               </h3>
               <ul class="small-date">
                <li>by <asp:Label ID="lblCreatedBy" runat="server" class="date" Text='<%#Eval("strAddedBy")%>'></asp:Label></li>
                <li><asp:Label ID="lblDate" runat="server" class="date" Text='<%#Eval("dtAddedOn")%>'></asp:Label></li>
               </ul>          
               <p>
                <asp:Label ID="lblBlogsDescription" runat="server" Text='<%#Eval("strBlogTitle")%>'></asp:Label>
               </p>
             </div>
             <div class="post-footer pb-2 ">
              <ul class="list-inline">
               <li class="list-inline-item">
                <%--<a href="#" class="active-toogle"><span class="comment-btn"></span> 43 Comments</a>--%>
                <span class="d-flex align-items-center">
                 <asp:LinkButton ID="lnkCmntbtn" runat="server" class="active-toogle" Font-Underline="false">
                  <span class="comment-btn"></span>               
                 </asp:LinkButton>
                 
                 <span class="d-flex">
                  <asp:Label ID="lblCmnt" runat="server" Text='<%#Eval("CommentCount")  + ((Eval("CommentCount").ToString() == "1") ? " Comment" : " Comments") %>'
                   ClientIDMode="Static"></asp:Label>&nbsp;
                   <%--<asp:Label ID="lblShareText" runat="server" ClientIDMode="Static"></asp:Label>--%>
                 </span>
                </span>
               </li>
               <li class="list-inline-item">
                <%--<a href="#" class="un-anchor active-toogle"><span class="eye-view"></span> 5 Views</a>--%>
                <span class="d-flex align-items-center">
                 <asp:LinkButton ID="lnkEyeView" runat="server" class="un-anchor active-toogle" Font-Underline="false">
                  <span class="eye-view"></span>               
                 </asp:LinkButton>
                 
                 <span class="d-flex">
                  <asp:Label ID="lblView" runat="server" Text='<%#Eval("ViewCount") + ((Eval("ViewCount").ToString() == "1") ? " View" : " Views") %>'
                   ClientIDMode="Static"></asp:Label>&nbsp;
                   <%--<asp:Label ID="lblShareText" runat="server" ClientIDMode="Static"></asp:Label>--%>
                 </span>
                </span>
               </li>
              </ul>
             </div>
            </div>
          </ItemTemplate>
         </asp:ListView>
        </ContentTemplate>
       </asp:UpdatePanel>
       <%--<div class="case-list">
        <div class="post-body">
         <a href="#"><h3>5 Reasons Why People Like Civil Law.</h3></a> 
          <ul class="small-date">
           <li>by Rajat Jain</li>
           <li>4 June 2018</li>
          </ul>          
          <p>
           Like to pop in a CD and have a better quality of life, and even self improvement? There are three ways you can use music to accomplish this and even self 
            improvement? There are three ways you can use music to accomplish this.
          </p>
        </div>
        <div class="post-footer">
         <ul class="list-inline">
          <li class="list-inline-item">
           <a href="#" class="active-toogle"><span class="comment-btn"></span> 43 Comments</a>
          </li>
          <li class="list-inline-item">
           <a href="#" class="un-anchor active-toogle"><span class="eye-view"></span> 5 Views</a>
          </li>
         </ul>
        </div>
       </div>
       <div class="case-list">
        <div class="post-body">
         <a href="#"><h3>5 Reasons Why People Like Civil Law.</h3></a> 
          <ul class="small-date">
           <li>by Rajat Jain</li>
           <li>4 June 2018</li>
          </ul>          
          <p>
           Like to pop in a CD and have a better quality of life, and even self improvement? There are three ways you can use music to accomplish this and even self 
            improvement? There are three ways you can use music to accomplish this.
          </p>
        </div>
        <div class="post-footer">
         <ul class="list-inline">
          <li class="list-inline-item">
           <a href="#" class="active-toogle"><span class="comment-btn"></span> 43 Comments</a>
          </li>
          <li class="list-inline-item">
           <a href="#" class="un-anchor active-toogle"><span class="eye-view"></span> 5 Views</a>
          </li>
         </ul>
        </div>
       </div>  --%>     
       <div class="case-list border-top-1px">
        <div class="padding-15 text-center">
         <asp:LinkButton ID="lnkBlogsResultsBottom" class="view-more-universal" runat="server" Text="View More Results" OnClick="lnkBlogsResultsBottom_Click"></asp:LinkButton>
        </div>
       </div>           
      </div>
       <!-- post con ended here -->     
     </div>    
    </div>
     <!-- card-list-con ended -->    
    
    <div class="card card-list-con blog-list m-b-50" id="divNews" runat="server" visible="false">
     <div class="list-group-item top-list">
      <div class="post-con">             
       <div class="post-header d-flex justify-content-between">
        <div>
         <span class="question-icon">
          <span class="icon"><img src="images/file.svg"></span>
         </span>
         <ul class="que-con">
          <li>News</li>
         </ul>
        </div>
        <div>
     <!--     <span class="float-right">
          <asp:LinkButton ID="lnkNewsResults" runat="server" Text="View More Results" OnClick="lnkNewsResults_Click"></asp:LinkButton>
         </span> -->
        </div>
       </div>
       <asp:UpdatePanel ID="pnlParentNewsResults" runat="server">
        <ContentTemplate>
         <asp:ListView ID="lstParentNewsResults" runat="server" OnItemCommand="lstParentNewsResults_ItemCommand" OnItemDataBound="lstParentNewsResults_ItemDataBound">
          <ItemTemplate>
           <asp:HiddenField ID="hdnNewsID" runat="server" ClientIDMode="Static" Value='<%#Eval("ID")%>' />
           <%--<asp:HiddenField ID="hdnNewsAddedBy" runat="server" />--%>
            <div class="case-list">
             <div class="post-body p-b-10">
              <%--<a href="#"><h3>5 Reasons Why People Like Civil Law.</h3></a>--%> 
              <h3 class="mb-1">
               <asp:LinkButton ID="lnkNewsTitle" runat="server" class="black-text-anchor" Text='<%#Eval("Title")%>' CommandName="NewsDetails"><%--Text='<%#Eval("strQuestionDescription")%>'--%>               
               </asp:LinkButton>          
              </h3>
              <p class="d-flex news-img-inversal">
               <asp:Label ID="lblNewsDescription" runat="server" class="moreQA"></asp:Label><%--Text='<%#Eval("Content")%>'--%>
              </p>
             </div>          
            </div>
          </ItemTemplate>
         </asp:ListView>
        </ContentTemplate>
       </asp:UpdatePanel>
       <div class="case-list border-top-1px">
        <div class="padding-15 text-center">
         <asp:LinkButton ID="lnkNewsResultsBottom" class="view-more-universal" runat="server" Text="View More Results" OnClick="lnkNewsResultsBottom_Click"></asp:LinkButton>
        </div>
       </div>
       <%--<div class="case-list">
        <div class="post-body p-b-10">
         <a href="#"><h3>5 Reasons Why People Like Civil Law.</h3></a> 
         <p>
          Like to pop in a CD and have a better quality of life, and even self improvement? There are three ways you can use music to accomplish this and even self 
           improvement? There are three ways you can use music to accomplish this.
         </p>
        </div>          
       </div>
       <div class="case-list">
        <div class="post-body p-b-10">
         <a href="#"><h3>5 Reasons Why People Like Civil Law.</h3></a> 
         <p>
          Like to pop in a CD and have a better quality of life, and even self improvement? There are three ways you can use music to accomplish this and even self 
           improvement? There are three ways you can use music to accomplish this.
         </p>
        </div>          
       </div>
       <div class="case-list">
        <div class="post-body p-b-10">
         <a href="#"><h3>5 Reasons Why People Like Civil Law.</h3></a> 
         <p>
          Like to pop in a CD and have a better quality of life, and even self improvement? There are three ways you can use music to accomplish this and even self 
           improvement? There are three ways you can use music to accomplish this.
         </p>
        </div>          
       </div>--%>
                     
      </div>
       <!-- post con ended here -->                  
     </div>    
    </div>
     <!-- card-list-con ended -->    
    
    <div class="card card-list-con blog-list m-b-50" id="divUsers" runat="server" visible="false">
     <div class="list-group-item top-list">
      <asp:HiddenField ID="hdnTempUserId" runat="server" Value="" ClientIDMode="Static" />                         
      <div class="post-con">             
       <div class="post-header d-flex justify-content-between">
        <div>
         <span class="question-icon">
          <span class="icon">
           <img src="images/profile-photo.png" class="user-icon">           
          </span>
         </span>
         <ul class="que-con">
          <li>Users</li>
         </ul>
        </div>
        <div>
       <!--   <span class="float-right">
          <asp:LinkButton ID="lnkUsersResults" runat="server" Text="View More Results" OnClick="lnkUsersResults_Click"></asp:LinkButton>
         </span> -->
        </div>
       </div>
       <asp:UpdatePanel ID="pnlParentUsersResults" runat="server">
        <ContentTemplate>
         <asp:ListView ID="lstParentUsersResults" runat="server" OnItemDataBound="lstParentUsersResults_ItemDataBound" OnItemCommand="lstParentUsersResults_ItemCommand">
          <ItemTemplate>
           <%--<asp:HiddenField ID="hdnNewsID" runat="server" ClientIDMode="Static" Value='<%#Eval("ID")%>' /--%>
           <div class="case-list">
            <div class="post-body pb-3">
             <div class="media align-items-center">
              <%--<img class="mr-3 search-avatar rounded-circle" src="images/avatar3.jpg" alt="">--%>
              <asp:ImageButton id="imgprofile" runat="server" class="mr-3 search-avatar rounded-circle" ImageUrl='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' 
               CommandName="Details" />
              <asp:HiddenField ID="hdnimgprofile" runat="server" ClientIDMode="Static" Value='<%# Eval("vchrPhotoPath") %>' />
              <div class="media-body">
               <h5 class="mt-0 mb-0">
                <asp:LinkButton ID="lblPostlink" runat="server" class="un-anchor truncate cursor-pointer" Text='<%# Eval("strPostDescription") %>' CommandName="Details">
                </asp:LinkButton>
               </h5>
               <p class="br-display-none mb-2"> 
                <asp:Label ID="lblName" Text='<%# Eval("UserName") %>' runat="server"></asp:Label>
              </p>  
                <asp:HiddenField ID="hdnstrOthers" Value='<%#Eval("strOthers") %>' runat="server" />
                <asp:HiddenField ID="hdnintUserTypeID" Value='<%#Eval("intUserTypeID") %>' runat="server" />
                <asp:HiddenField ID="hdnRegistrationId" Value='<%#Eval("intRegistrationId") %>' runat="server" />

               
               <span class="member-group">
                <span class="member-icon">
                 <img src="images/member.svg" alt="">
                </span>
                <span>
                 <asp:Label ID="lblFriends" runat="server" class="mr-1"></asp:Label>
                </span>
               </span>
              </div>
             </div>
            </div>                                 
           </div> 
          </ItemTemplate>
         </asp:ListView>
        </ContentTemplate>
       </asp:UpdatePanel>
       <%--<div class="case-list">
        <div class="post-body p-b-10">
         <div class="media align-items-center">
          <img class="mr-3 search-avatar rounded-circle" src="images/avatar3.jpg" alt="">
          <div class="media-body">
           <h5 class="mt-0">Steve Ross</h5>
           <span class="member-group">
           <span class="member-icon">
            <img src="images/member.svg" alt=""></span>
           <span>3 Friends</span></span>
          </div>
         </div>
        </div>                                 
       </div>
       <div class="case-list">
        <div class="post-body p-b-10">
         <div class="media align-items-center">
          <img class="mr-3 search-avatar rounded-circle" src="images/avatar3.jpg" alt="">
          <div class="media-body">
           <h5 class="mt-0">Steve Ross</h5>
           <span class="member-group">
           <span class="member-icon">
            <img src="images/member.svg" alt=""></span>
           <span>3 Friends</span></span>
          </div>
         </div>
        </div>                                 
       </div>--%>
                                     
       <div class="case-list border-top-1px">
        <div class="padding-15 text-center">
         <asp:LinkButton ID="lnkUsersResultsBottom" class="view-more-universal" runat="server" Text="View More Results" OnClick="lnkUsersResultsBottom_Click"></asp:LinkButton>
        </div>
       </div>                                            
      </div>
       <!-- post con ended here -->                                                                
     </div>    
    </div>
     <!-- card-list-con ended -->    
    
    <div class="card card-list-con blog-list m-b-50" id="divGroups" runat="server" visible="false">
     <div class="list-group-item top-list">
                                
      <div class="post-con">             
       <div class="post-header d-flex justify-content-between">
        <div>
         <span class="question-icon">
          <span class="icon"><img src="images/groupPhoto.jpg" class="user-icon"></span>
         </span>
         <ul class="que-con">
          <li>Groups</li>
         </ul>
        </div>
        <div>
     <!--     <span class="float-right">
          <asp:LinkButton ID="lnkGroupsResults" runat="server" Text="View More Results" OnClick="lnkGroupsResults_Click"></asp:LinkButton>
         </span> -->
        </div>
       </div>
       <asp:UpdatePanel ID="pnlParentGroupsResults" runat="server">
        <ContentTemplate>
         <asp:ListView ID="lstParentGroupsResults" runat="server" OnItemDataBound="lstParentGroupsResults_ItemDataBound" OnItemCommand="lstParentGroupsResults_ItemCommand">
          <ItemTemplate>                                 
           <div class="case-list">
            <div class="post-body pb-3">
             <div class="media align-items-center">
              <%--<img class="mr-3 search-avatar rounded-circle" src="images/avatar-main.jpg" alt="" >--%>
              <%--<span class="mr-3 search-avatar rounded-circle">--%>
               <asp:LinkButton ID="LinkButton2" CommandName="Details" runat="server">
                <img id="imgprofile" class="mr-3 search-avatar rounded-circle" runat="server" src='<%# "CroppedPhoto/"+Eval("vchrPhotoPath")%>' />
               </asp:LinkButton>
              <%--</span>--%>
              <div class="media-body">
               <h3>
                <asp:LinkButton ID="lblPostlink" runat="server" Text='<%# Eval("strPostDescription") %>' CommandName="Details"
                 CssClass="searchGrouplinnk un-anchor truncate cursor-pointer"></asp:LinkButton>
               </h3>
               <p class="mb-2 empty-div-hide">
                <asp:Label ID="lblDescription" runat="server" class="mr-1" Text='<%# Eval("strSummary") %>'></asp:Label>
               </p>  
                <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("Id") %>' />
                <asp:HiddenField ID="hdnRegistrationId" runat="server" Value='<%#Eval("intRegistrationId") %>' />
                <span class="member-group">
                 <span class="member-icon">
                  <img src="images/member.svg" alt="">
                 </span>
                 <span><asp:Label ID="lblGroupMember" runat="server" class="mr-1"></asp:Label></span>
                </span>
              </div>
             </div>
            </div>                                 
           </div>
          </ItemTemplate>
         </asp:ListView>
        </ContentTemplate>
       </asp:UpdatePanel>
       <%--<div class="case-list">
        <div class="post-body p-b-10">
         <div class="media align-items-center">
          <img class="mr-3 search-avatar rounded-circle" src="images/avatar-main.jpg" alt="" >
          <div class="media-body">
           <h5 class="mt-0 m-b-0">Steve Ross</h5>
           <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit</p>  
            <span class="member-group">
             <span class="member-icon">
              <img src="images/member.svg" alt="">
             </span>
             <span>3 Members</span>
            </span>
          </div>
         </div>
        </div>                                 
       </div>
       <div class="case-list">
        <div class="post-body p-b-10">
         <div class="media align-items-center">
          <img class="mr-3 search-avatar rounded-circle" src="images/avatar-main.jpg" alt="" >
          <div class="media-body">
           <h5 class="mt-0 m-b-0">Steve Ross</h5>
           <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit</p>  
            <span class="member-group">
             <span class="member-icon">
              <img src="images/member.svg" alt="">
             </span>
             <span>3 Members</span>
            </span>
          </div>
         </div>
        </div>                                 
       </div>--%>
                                        
       <div class="case-list border-top-1px">
        <div class="padding-15 text-center">
         <asp:LinkButton ID="lnkGroupsResultsBottom" class="view-more-universal" runat="server" Text="View More Results" OnClick="lnkGroupsResultsBottom_Click"></asp:LinkButton>
        </div>
       </div>
                                            
      </div>
       <!-- post con ended here -->                                
     </div>    
    </div>
     <!-- card-list-con ended -->                           
   </div>
    <!-- full-width-con ended -->
  </div>
   <!-- panel-cover ended -->                  
 </div>
  <!-- main-section inner ended -->

  <!---Read More Less-->
  <script type="text/javascript">
     function postCommentInput(e) {         
         var msg = $(this).val().replace(/\n/g, "");
         if (msg != $(this).val()) { // Enter was pressed
             $(this).val(msg);
             window.location.href = $(this).parent().parent().children('.lnkEnterCommentcss').attr('href');
             showLoader1();
         }
     }
     $(document).ready(function () {

        var showChar = 300;
        var ellipsestext = "...";
        var moretext = "Read more";
        var lesstext = "Less";
        $('.moreQA').each(function () {
            var content = $(this).html();
            if (content.length > showChar) {
                var c = content.substr(0, showChar);
                var h = content.substr(showChar - 1, content.length - showChar);
                var html = c + '<span class="moreelipses">' + ellipsestext + '</span>';
                $(this).html(html);
            }
        });
        $(".morelinkQA").click(function () {
            if ($(this).hasClass("less")) {
                $(this).removeClass("less");
                $(this).html(moretext);
            } else {
                $(this).addClass("less");
                $(this).html(lesstext);
            }
            $(this).parent().prev().toggle();
            $(this).prev().toggle();
            return false;
        });
         
     });
    </script> 
</asp:Content>

