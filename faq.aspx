<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="faq.aspx.cs" Inherits="faq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMain" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-section-inner">

                    <div class="panel-cover clearfix">
                        <div class="full-width-con">
                
                            
                            <div class="card card-list-con tags-card">
                                <div class="list-group-item top-list">
                                    <div class="post-con">
                                        <div class="post-header faq-page">
                                            <span class="question-icon">
                                            <span class="icon">
                                            	?
                                            </span>
                                            </span>
                                            <ul class="que-con">
                                                <li><a href="#">How do I apply for jobs and internships?</a></li>
                                            </ul>
                                        </div>

                                        <div class="post-body">
                                            <p> Please click on <a href="AllJobsListing.aspx" target="_blank">Jobs</a> then click on an open listing and follow the steps prescribed.</p>

                                         

                                        </div>
                                    </div>
                                </div>
                            </div><!-- card ended -->
                            <div class="card card-list-con tags-card">
                                <div class="list-group-item top-list">
                                    <div class="post-con">
                                        <div class="post-header faq-page">
                                            <span class="question-icon">
                                            <span class="icon">
                                            	?
                                            </span>
                                            </span>
                                            <ul class="que-con">
                                                <li><a href="#">Does Skorkel guarantee jobs and internships?</a></li>
                                            </ul>
                                        </div>

                                        <div class="post-body">
                                            <p> No, Skorkel merely connects you with open positions at firms and companies across the country - If you meet the Skorkel Score (and other, as the case may be) criteria specified by the employer, your application will be forwarded to the employers - They have the option of ignoring your application or setting up an interview. Skorkel is in no way guaranteeing individual internships. What we are guaranteeing is that we will have a lot of positions up for grabs.</p>

                                           

                                        </div>
                                    </div>
                                </div>
                            </div><!-- card ended -->                            
                             <div class="card card-list-con tags-card">
                                <div class="list-group-item top-list">
                                    <div class="post-con">
                                        <div class="post-header faq-page">
                                            <span class="question-icon">
                                            <span class="icon">
                                            	?
                                            </span>
                                            </span>
                                            <ul class="que-con">
                                                <li><a href="#">How is Skorkel Score calculated?</a></li>
                                            </ul>
                                        </div>

                                        <div class="post-body">
                                            <p> You can view the Skorkel Score algorithm <a href="javascript:void();" class="hide-body-scroll" id="scorepopup">here</a>. The higher your score, the greater the likelihood of landing an internship/job of your choice.</p>

                                           

                                        </div>
                                    </div>
                                </div>
                            </div><!-- card ended -->                           
                            <div class="card card-list-con tags-card">
                                <div class="list-group-item top-list">
                                    <div class="post-con">
                                        <div class="post-header faq-page">
                                            <span class="question-icon">
                                            <span class="icon">
                                            	?
                                            </span>
                                            </span>
                                            <ul class="que-con">
                                                <li><a href="#">Is Skorkel guaranteeing the accuracy of the information being shared via the platform?</a></li>
                                            </ul>
                                        </div>

                                        <div class="post-body">
                                            <p> Skorkel is merely a forum for students to share their views, content, and academic contributions. As it is in essence a crowd-sourced platform, we cannot guarantee the veracity of any particular post - However, our moderation team has a strict no-abuse / cyberbullying policy and any such posts will be taken out immediately.</p>

                                            

                                        </div>
                                    </div>
                                </div>
                            </div><!-- card ended -->  
                            <div class="card card-list-con tags-card">
                                <div class="list-group-item top-list">
                                    <div class="post-con">
                                        <div class="post-header faq-page">
                                            <span class="question-icon">
                                            <span class="icon">
                                                ?
                                            </span>
                                            </span>
                                            <ul class="que-con">
                                                <li><a href="#">What is the "My Skorkel" section for?</a></li>
                                            </ul>
                                        </div>

                                        <div class="post-body">
                                            <p>My Skorkel is where students can save all their personal content - whether it be documents, bookmarks, saved searches, or tagged content for future reference.</p>

                                            

                                        </div>
                                    </div>
                                </div>
                            </div><!-- card ended -->                                       
                            <div class="card card-list-con tags-card">
                                <div class="list-group-item top-list">
                                    <div class="post-con">
                                        <div class="post-header faq-page">
                                            <span class="question-icon">
                                            <span class="icon">
                                                ?
                                            </span>
                                            </span>
                                            <ul class="que-con">
                                                <li><a href="#">How do I join a group?</a></li>
                                            </ul>
                                        </div>

                                        <div class="post-body">
                                            <p>Groups can be private (request to join) or public (anyone can join) - depending on the creator's content. In the list of groups, private/public differentiation is clearly visible.</p>

                                            

                                        </div>
                                    </div>
                                </div>
                            </div><!-- card ended -->                          
                        </div>
                        <!-- full-width-con ended -->
                    </div>
                    <!-- panel-cover ended -->
                </div>
                    <!---Score Popup-->
                <div  runat="server" class="modal backgroundoverlay scorepopup" clientidmode="Static">
                 <div class="modal-dialog modal-dialog-centered">
                  <div class="modal-content">
                   <div class="modal-header">
                    <h5 class="modal-title">Skorkel Score – Algorithm 
                    </h5>
                   </div>
                   <div class="modal-body score-main">
                    

                    
                    <p><b>Your Skorkel Score (Max: 100 points) has 4 constituents:</b></p>
                        <ul class="score-category">
                          <li>(College where are you studying) = 20 Points</li>
                          <li>(CGPA - As Class/Batch Percentile) = 40 Points</li>
                          <li>(Extra Curricular Achievements) = 38 Points</li>
                          <li>(Contributions to Skorkel Platform) = 2 Points</li>
                        </ul>
                    <ul class="score-info">    
                    <li>(College): Tier 1 = 20 Points; Tier 2 = 17.5 Points; Tier 3 = 15 Points; Tier 4 = 10 Points </li>
                    <li>(CGPA/Class Rank):  Class Rank Expressed as a Percentile x 0.4 = Points out of 40</li>
                    <li>Extra Curricular Achievements: Summation of Points from Moots, Debates, ADR Competitions, Publications, Committee Membership (Expressed as a dynamic percentile WRT All users having a Skorkel Score) x 0.38 = Points out of 40</li>
                    <ul>
                       <li>Moots: Participation (Tier 1 = 50, Tier 2 = 25, Tier 3 = 15); Winners (Participation Point x 5); Runners-up / Best Memo / Best Speaker (Participation Point x 3)</li>

                        <li>Debates: Participation (Tier 1 = 10, Tier 2 = 5, Tier 3 = 2.5); Winners (Participation Point x 5); Runners-up (Participation Points x 3); Break (Participation Points x 2)</li>

                        <li>ADR Competitions: Participation (Tier 1 = 10, Tier 2 = 5, Tier 3 = 2.5); Winners (Participation Point x 5); Runners-up (Participation Points x 3)</li>

                        <li>Publications/ Conference Papers: Tier 1 = 125; Tier 2 = 50</li>

                        <li>Committee Membership: Membership (10 Points); Leadership Position - President/VP/Convenor/Joint Convenor (5 Points); committee points subject to a maximum of 15 Points in total</li>
                         
                        <li>Student Body President / Vice - President - 50 Points</li>
                    </ul>

                    <li>Contributions to Skorkel Platform: Summation of Points - (Expressed as a dynamic percentile WRT All users having a Skorkel Score) x 0.02 = Points out of 2</li>
                    <ul>
                         <li>
                        Case Annotations: Facts / Issue / Obiter Dictum / Important Paragraph = 10 Points
                        <li>Ratio Decidendi = 20 Points,</li>
                        <li></b>Clarification:</b></li>
                        <li>Judgment Length < 2000 words (i.e. roughly 4 pages) = No Points</li>
                        <li>2000 < Judgment Length < 12000 words = Base Points</li>
                        <li>12000 < Judgment Length < 24000 words = Base Points x 1.5</li>
                        <li>24000 < Judgment Length = Base Points x 2</li>
                         
                        <li>Summary/Headnote = 40 Points</li>
                        <li><b>Clarification:</b></li>
                        <li>Judgment Length < 2000 words (i.e. roughly 4 pages) = No Points</li>
                        <li>2000 < Judgment Length < 12000 words = Base Points</li>
                        <li>12000 < Judgment Length < 24000 words = Base Points x 1.5</li>
                        <li>24000 < Judgment Length = Base Points x 2</li>
                        <li>No points given if summary/headnote is less than 100 words</li>
                         
                        <li>Ask Question in QA Section = 2.5 Points</li>
                         
                        <li>Answer Question in QA Section = 10 Points</li>
                         
                        <li>Write Blog (Minimum 200 Words) = 40 Points</li>
                         
                        <li>For each set of 20 upvotes across every contribution above= Base Point x 3</li>
                            
                        <li>Submit Article (on pre-released limited set of topics, minimum 200 words) = 40 Points</li>
                         
                        <li>Selection of Article for Publication in AILSJ (All India Law Students Journal) = 200 Points </li>
                  </ul> 
                 </ul>   
                    <p>*Since scores are dynamic and relative, they are updated once every day - at 4 AM.</li>
                    <p>*All achievements/credentials/academic particulars shall be verified by Skorkel - Users must upload all necessary supporting documents onto their Skorkel Portal (“Upload Documents” button on “Skorkel Score” Section). Any user found to be posting falsified claims shall be permanently suspended and disbarred from using the platform.</p>

                   </div>   
                   <div class="modal-footer border-top-0">
                    <asp:Button ClientIDMode="Static" class="btn btn-primary add-scroller" id="btnScore" runat="server" Text="Ok" ></asp:Button><%--OnClick="btnOk_click"--%>
                   </div>
                  </div>
                 </div>
                </div>
                <script>
                    $('#scorepopup').click(function(){
                        $('.scorepopup').toggleClass('display-blockk');
                    });
                    $('#btnScore').click(function(){
                        $('.scorepopup').removeClass('display-blockk');
                    })
                </script>

</asp:Content>

