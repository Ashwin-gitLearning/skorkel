using DA_SKORKEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class SignUp3 : System.Web.UI.Page
{
    DO_LogDetails objLog = new DO_LogDetails();
    DA_Logdetails objLogD = new DA_Logdetails();
    DA_Scrl_UserEducationTbl objDAEdu = new DA_Scrl_UserEducationTbl();
    DO_Scrl_UserEducationTbl objDOEdu = new DO_Scrl_UserEducationTbl();
    DataTable dt = new DataTable();
    DA_ProfileDocuments objDAProDocs = new DA_ProfileDocuments();
    DO_ProfileDocuments objDoProDocs = new DO_ProfileDocuments();
    DO_Scrl_APILogDetailsTbl objAPILogDO = new DO_Scrl_APILogDetailsTbl();
    DA_Scrl_APILogDetailsTbl objAPILogDA = new DA_Scrl_APILogDetailsTbl();
    bool isFriend = false;

    static string APIURL = ConfigurationManager.AppSettings["APIURL"];
    static string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
    static string ISAPIResponse = ConfigurationManager.AppSettings["ISAPIResponse"];
    string UserTypeId = string.Empty, CurrentlyWork = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        divDeletesucess.Style.Add("display", "none");

        divSuccess.Style.Add("display", "none");
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        ViewState["BrowserName"] = browser.Browser;
        Session["SubmitTime"] = DateTime.Now.ToString();

        if (!IsPostBack)
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                //Newly Added
                if (Convert.ToString(Session["RegType"]) == "" || Session["RegType"] == null)
                {
                    if (Convert.ToString(ViewState["RegType"]) != "A")
                    {
                        ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
                    }
                    else
                    {
                        Response.Redirect("Landing.aspx", true);
                    }
                }
                else
                {
                    Response.Redirect("Landing.aspx", true);
                }
            }
            else
            {
                Response.Redirect("Landing.aspx", true);
            }

            if ((Request.QueryString["ActiveStatus"] != null && Request.QueryString["ActiveStatus"] != "") || (Request.QueryString["Status"] != null && Request.QueryString["Status"] != ""))
            {
                if (Request.QueryString["ActiveStatus"] == "Ac" && Request.QueryString["Status"] != "Update" && Request.QueryString["Status"] != "Cancel")
                {
                    divSuccess.Style.Add("display", "block");
                    //Master.BodyTag.Attributes.Add("class", "remove-scroller");
                    lblSuccess.Text = "Achievements saved successfully.";
                    divMain.Visible = false;
                }
                else if (Request.QueryString["Status"] == "Update")
                {
                    divSuccess.Style.Add("display", "block");
                    //Master.BodyTag.Attributes.Add("class", "remove-scroller");
                    lblSuccess.Text = "Achievements updated successfully.";
                    divMain.Visible = false;
                }
                else if (Request.QueryString["ActiveStatus"] == "E" && Request.QueryString["Status"] != "Update" && Request.QueryString["Status"] != "Cancel")
                {
                    //divSuccess.Style.Add("display", "block");
                    //Master.BodyTag.Attributes.Add("class", "remove-scroller");
                    //lblSuccess.Text = "Education saved successfully.";
                    divEducation.Style.Add("display", "none");
                    divAchivement.Style.Add("display", "block");
                    lnkSkip.Visible = true;
                    Achivement();
                }
                else
                {
                    divSuccess.Style.Add("display", "none");
                    lblSuccess.Text = "";
                }
            }
            else
            {
                divAchivement.Style.Add("display", "none");
                //divEducation.Style.Add("display", "none");
                divEducation.Style.Add("display", "block");
                divMain.Visible = false;
                if (divEducation.Visible == true)
                {
                    Education();
                }
            }
            // To check score is public or private
            if (Convert.ToBoolean(Session["isScorePrivate"]) == true)
            {
                cbScorePrivate.Checked = true;
            }
        }
    }

    protected void lnlAddEducation_Click(object sender, EventArgs e)
    {
        divEducation.Style.Add("display", "block");
        LoadEducation();
        clearEducation();
    }

    protected void clearEducation()
    {
        lblMessage.Text = "";
        LoadYear();
        ViewState["intEducationId"] = null;
        txtEducationFromdt.SelectedValue = "Year";
        txtEducationTodt.SelectedValue = "Year";
        txtEduDescription.InnerText = "";
        ddlToMonth.SelectedValue = "Month";
        ddlFromMonth.Value = "Month";
        txtTitle.Text = "";
        txtInstitute.Text = "";
        chkEducation.Checked = false;
        txtEducationTodt.SelectedIndex = 0;
        txtEducationTodt.Enabled = true;
        ddlToMonth.Enabled = true;
        ddlToMonth.SelectedIndex = 0;
        ViewState["Callsmoothmenu"] = "smooth";
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDegreeList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlCommand com = new SqlCommand())
            {
                //com.CommandText = "select strDegreeName from Scrl_InstituteDegreeTbl where " + "strDegreeName like @Search + '%'";
                com.CommandText = "Select Top 5 strDegreeName from Scrl_InstituteDegreeTbl where " + "strDegreeName Like '%' + LTRIM(RTRIM(@Search)) + '%' Order By strDegreeName";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> InstituteName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InstituteName.Add(sdr["strDegreeName"].ToString());
                    }
                }
                con.Close();
                return InstituteName;
            }
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompititionLists(string prefixText, int count, string contextKey)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlCommand com = new SqlCommand())
            {
                //com.CommandText = "select CompName,intMilestoneId from Scrl_CompetitionMasterTbl where " + "CompName like @Search + '%'";
                //com.CommandText = "Select CompName, intMilestoneId from Scrl_CompetitionMasterTbl where " + "LTRIM(RTRIM(CompName)) like LTRIM(RTRIM(@Search)) + '%' And competitiontype=@CompType";
                com.CommandText = "Select Top 5 CompName, intMilestoneId from Scrl_CompetitionMasterTbl where " + "LTRIM(RTRIM(CompName)) Like '%' + LTRIM(RTRIM(@Search)) + '%' And Competitiontype=@CompType Order By CompName";
                com.Parameters.AddWithValue("@Search", prefixText.Trim());
                com.Parameters.AddWithValue("@CompType", contextKey.Trim());
                com.Connection = con;
                con.Open();
                List<string> InstituteName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InstituteName.Add(sdr["CompName"].ToString());
                    }
                }
                con.Close();
                return InstituteName;
            }
        }
    }

    protected void lnkCancelEducation_Click(object sender, EventArgs e)
    {
        RedirectToEducation();
        //clearEducation();
        //divEducation.Style.Add("display", "none");
        ////ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation1", "CallEduLI();", true);
        //LoadEducation();
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        //divDeletesucess.Style.Add("display", "none");
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        //if (hdnintdocIdelete.Value != "")
        //{
        //    objDoProDocs.DocId = Convert.ToInt32(hdnintdocIdelete.Value);
        //    objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.DeleteDocument);
        //    string PathPhysical = Server.MapPath("~/UploadDocument/" + Convert.ToString(hdnfilestrFilePathe.Value));
        //    if (File.Exists(PathPhysical))
        //    {
        //        File.Delete(PathPhysical);
        //    }

        //    objLog.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        //    objLog.intActionId = Convert.ToInt32(hdnintdocIdelete.Value);
        //    objLog.strAction = "Document";
        //    objLog.strActionName = hdnstrdocDescriptiondele.Value;
        //    objLog.strIPAddress = ip;
        //    objLog.intDeletedBy = Convert.ToInt32(ViewState["UserID"]);
        //    objLog.SectionId = 10;   // Document
        //    objLogD.AddEditDel_Scrl_LogDetailsMaster(objLog, DA_Logdetails.LogDetails.Insert);
        //    hdnintdocIdelete.Value = "";
        //    hdnstrdocDescriptiondele.Value = "";
        //    hdnfilestrFilePathe.Value = "";
        //    BindDocuments();
        //    divDeletesucess.Style.Add("display", "none");
        //}
        //else if (hdnintworkeIdelet.Value != "")
        //{
        //    AddWorkExp.Style.Add("display", "none");
        //    objUserExpDO.intExperienceId = Convert.ToInt32(hdnintworkeIdelet.Value);
        //    objUserExpDO.dtFromDate = DateTime.Now;
        //    objUserExpDO.dtToDate = DateTime.Now;
        //    objUserExpDA.AddEditDel_Scrl_UserExperienceTbl(objUserExpDO, DA_Scrl_UserExperienceTbl.Scrl_UserExperienceTbl.Delete);
        //    LoadUserExp();
        //    TotalscoreCount();
        //    hdnintworkeIdelet.Value = "";
        //    //    divDeletesucess.Style.Add("display", "none");
        //}
        if (hdnintedueIdelet.Value != "")
        {
            objDOEdu.intEducationId = Convert.ToInt32(hdnintedueIdelet.Value);
            objDOEdu.dtDate = DateTime.Now;
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.Delete);
            LoadEducation();
            hdnintedueIdelet.Value = "";
        }
        else if (hdnintacheIdelet.Value != "")
        {
            objDOEdu.intAchivmentId = Convert.ToInt32(hdnintacheIdelet.Value);
            objDOEdu.dtDate = DateTime.Now;
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.DeleteAchivement);
            LoadAchivment();
            hdnintacheIdelet.Value = "";
            //  divDeletesucess.Style.Add("display", "none");
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation", "CallEduLI();", true);
    }

    protected void lnkAddMoreAchievement_Click(object sender, EventArgs e)
    {
        ClearAchivement();
        divAchivement.Style.Add("display", "block");
        add_achivement.Style.Add("display", "block");
        Achivement();
        lnkAddMoreAchievement.Visible = false;
        divHeadingLstAchievement.Visible = false;
        divHeaderLstAchievement.Visible = false;
        //divHeaderSubAchievement.Visible = false;
    }

    protected void lnkSaveAchivemnt_Click(object sender, EventArgs e)
    {
        objDOEdu = new DO_Scrl_UserEducationTbl();
        objDOEdu.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        int CompID = 0;
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (txtCompitition.Text != "" || txtCommitteeName.Text != "" || txtJournalName.Text != "" || ddlMilestone.SelectedItem.Text == "Student Body")
        {
            if (ddlMilestone.SelectedItem.Text != "Type of Milestone")
            {
                objDOEdu.intMilestoneId = Convert.ToInt32(ddlMilestone.SelectedValue);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "callSaveAch", "callSaveAch();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "saveAchi", "OverlayBody();", true);
                lblAchivmentmsg.Text = "Please select milestone.";
                return;
            }
            if (ddlPosition.SelectedItem.Text != "Position" && ddlMilestone.SelectedItem.Text != "Publications")
            {
                objDOEdu.intPostionId = Convert.ToInt32(ddlPosition.SelectedValue);
                objDOEdu.strPosition = ddlPosition.SelectedItem.Text;
            }
            else if (ddlPosition.Visible == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "callSaveAchs", "callSaveAch();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "saveAch", "OverlayBody();", true);
                lblAchivmentmsg.Text = "Please select position.";
                return;
            }

            objDOEdu.dtDate = DateTime.Now;
            objDOEdu.strEducationAchievement = "Achievement";
            objDOEdu.strIpAddress = ip;

            if (txtCompitition.Text.Trim() != "" && txtCompitition.Text.Trim() != null)
            {
                objDOEdu.strCompititionName = txtCompitition.Text.Trim();
                CompID = objDAEdu.GetintDataTableAchievement(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompSingleRecord);
                objDOEdu.CompID = CompID;
            }
            if (txtCommitteeName.Text.Trim() != "" && txtCommitteeName.Text.Trim() != null)
            {
                objDOEdu.strCompititionName = txtCommitteeName.Text.Trim();
            }
            if (txtJournalName.Text.Trim() != "" && txtJournalName.Text.Trim() != null)
            {
                objDOEdu.strCompititionName = txtJournalName.Text.Trim();
                CompID = objDAEdu.GetintDataTableAchievement(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompSingleRecord);
                objDOEdu.CompID = CompID;
            }
            objDOEdu.CompLavel = objDAEdu.GetintDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetCompLavel);

            objDOEdu.strMilestone = ddlMilestone.SelectedItem.Text;
            objDOEdu.strAdditionalAward = txtAdditionalAwrd.Text.Trim();
            objDOEdu.strAchDescription = txtAchivDescription.InnerText.Trim();
            objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);

            if (ViewState["hdnintAchivmentId"] == null)
            {
                objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.InsertAchivement);
                //RedirectToAchivements("true");
                ClearAchivement();
                lnkSkip.Visible = false;
                Achivement();
                lnkAddMoreAchievement.Visible = true;
            }
            else
            {
                objDOEdu.intAchivmentId = Convert.ToInt32(ViewState["hdnintAchivmentId"]);
                objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.UpdateAchivement);
                lnkSaveAchivemnt.Text = "Save";
                lnkSkip.Visible = false;
                ClearAchivement();
                Achivement();
                //RedirectToAchivements("update");
            }
            string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
            if (ISAPIURLACCESSED != "0")
            {
                try
                {
                    String url = APIURL + "updateUserEducationDetails.action?" +
                                "uid=" + UserTypeId + Convert.ToInt32(ViewState["UserID"]) +
                                "&educationId=" + objDOEdu.intOutId +
                                "&degree=" + objDOEdu.strDegree +
                                "&institution=" + objDOEdu.strInstituteName +
                                "&year=" + objDOEdu.intYear +
                                "&educationType=" + objDOEdu.strEducationAchievement +
                                "&achievementMarked=" + 1 +
                                "&description=" + objDOEdu.strSpclLibrary;

                    HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url);
                    myRequest1.Method = "GET";
                    if (ISAPIResponse != "0")
                    {
                        WebResponse myResponse1 = myRequest1.GetResponse();
                        StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
                        String result = sr.ReadToEnd();
                        objAPILogDO.strAPIType = "Profile Achivement";
                        objAPILogDO.strResponse = result;
                        objAPILogDO.strIPAddress = ip;
                        objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
                        objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
                    }
                }
                catch { }
            }
            //RedirectToAchivements("true");
            //LoadAchivment();
            //ClearAchivement();
            //divAchivement.Style.Add("display", "none");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAchi4", "CallAchLI();", true);
            divHeadingLstAchievement.Visible = true;
            divHeaderLstAchievement.Visible = true;
            //divHeaderSubAchievement.Visible = true;
            divlstAchievement.Style.Add("display", "block");
            card_list_con.Visible = true;
            add_achivement.Style.Add("display", "none");
            submit_con.Visible = true;
            lnkAddMoreAchievement.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "saveAchiv", "OverlayBody();", true);
            return;
        }
    }

    protected void lnkFinish_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx?ActiveStatus=P&FirstLogin=true");
    }

    //protected void lnkContinue_Click(object sender, EventArgs e)
    //{
    //    if (ddlMilestone.SelectedItem.Text == "Publications")
    //    {
    //        divddlPosition.Visible = false;
    //        ddlPosition.Visible = false;
    //        string Ids = "";
    //        Bind_ddlPosition(Ids);
    //        divJournalName.Visible = true;
    //        divCompetition.Visible = false;
    //        divAdditionalAwrd.Visible = false;
    //        divCommitteeName.Visible = false;
    //        divDescription.Visible = false;
    //        hdnCompetitionType.Value = "Publications";
    //        AutoCompleteExtender4.ContextKey = "Publications";
    //    }
    //    else if (ddlMilestone.SelectedItem.Text == "Moot Court Competition")
    //    {
    //        divddlPosition.Visible = true;
    //        ddlPosition.Visible = true;
    //        string Ids = "1,2,3,4,5";
    //        Bind_ddlPosition(Ids);
    //        divCompetition.Visible = true;
    //        divAdditionalAwrd.Visible = true;
    //        divDescription.Visible = true;
    //        divCommitteeName.Visible = false;
    //        divJournalName.Visible = false;
    //        hdnCompetitionType.Value = "Moot";
    //        AutoCompleteExtender3.ContextKey = "Moot";
    //    }
    //    else if (ddlMilestone.SelectedItem.Text == "Debate Competition")
    //    {
    //        divddlPosition.Visible = true;
    //        ddlPosition.Visible = true;
    //        string Ids = "1,2,3,6";
    //        Bind_ddlPosition(Ids);
    //        divCompetition.Visible = true;
    //        divAdditionalAwrd.Visible = true;
    //        divDescription.Visible = true;
    //        divCommitteeName.Visible = false;
    //        divJournalName.Visible = false;
    //        hdnCompetitionType.Value = "Debate";
    //        AutoCompleteExtender3.ContextKey = "Debate";
    //    }
    //    else if (ddlMilestone.SelectedItem.Text == "Alternate Dispute Resolution Competition")
    //    {
    //        divddlPosition.Visible = true;
    //        ddlPosition.Visible = true;
    //        string Ids = "1,2,3";
    //        Bind_ddlPosition(Ids);
    //        divCompetition.Visible = true;
    //        divAdditionalAwrd.Visible = true;
    //        divDescription.Visible = true;
    //        divCommitteeName.Visible = false;
    //        divJournalName.Visible = false;
    //        hdnCompetitionType.Value = "ADR";
    //        AutoCompleteExtender3.ContextKey = "ADR";
    //    }
    //    else if (ddlMilestone.SelectedItem.Text == "Committee Membership")
    //    {
    //        divddlPosition.Visible = true;
    //        ddlPosition.Visible = true;
    //        string Ids = "7,8,9,10,11";
    //        Bind_ddlPosition(Ids);
    //        divDescription.Visible = true;
    //        divCommitteeName.Visible = true;
    //        divCompetition.Visible = false;
    //        divAdditionalAwrd.Visible = false;
    //        divJournalName.Visible = false;
    //        hdnCompetitionType.Value = "Committee Membership";
    //        AutoCompleteExtender3.ContextKey = "Committee Membership";
    //    }
    //    else if (ddlMilestone.SelectedItem.Text == "Student Body")
    //    {
    //        divddlPosition.Visible = true;
    //        ddlPosition.Visible = true;
    //        string Ids = "8,9";
    //        Bind_ddlPosition(Ids);
    //        divDescription.Visible = true;
    //        divCompetition.Visible = false;
    //        divAdditionalAwrd.Visible = false;
    //        divCommitteeName.Visible = false;
    //        divJournalName.Visible = false;
    //        hdnCompetitionType.Value = "Student Body";
    //        AutoCompleteExtender3.ContextKey = "Student Body";
    //    }
    //    else
    //    {
    //        divddlPosition.Visible = true;
    //        ddlPosition.Visible = true;
    //    }
    //    divCtrls.Visible = true;
    //    lnkContinue.Visible = false;
    //    lnkSaveAchivemnt.Visible = true;
    //}




    protected void lnkCancelAchivemnt_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx?ActiveStatus=P&FirstLogin=true");
        //RedirectToAchivements("false");
        ClearAchivement();
        //divAchivement.Style.Add("display", "none");
        //LoadAchivment();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAchi1", "CallAchLI();", true);
    }

    protected void ddlMilestone_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = ddlMilestone.SelectedIndex;
        ClearAchivement();
        ddlMilestone.SelectedIndex = selectedIndex;
        if (ddlMilestone.SelectedItem.Text == "Moot Court Competition")
        {
            lnkSkip.Visible = true;
            if (lnkSaveAchivemnt.Visible == true)
            {
                //lnkContinue.Visible = false;
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "1,2,3,4,5";
                Bind_ddlPosition(Ids);
                divCompetition.Visible = true;
                divAdditionalAwrd.Visible = true;
                divDescription.Visible = true;
                divCommitteeName.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "Moot";
                AutoCompleteExtender3.ContextKey = "Moot";
                //lnkContinue.Visible = true;
            }
            else
            {
                //lnkContinue.Visible = true;
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
            }
        }
        else if (ddlMilestone.SelectedItem.Text == "Debate Competition")
        {
            lnkSkip.Visible = true;
            if (lnkSaveAchivemnt.Visible == true)
            {
                //lnkContinue.Visible = false;
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "1,2,3,6";
                Bind_ddlPosition(Ids);
                divCompetition.Visible = true;
                divAdditionalAwrd.Visible = true;
                divDescription.Visible = true;
                divCommitteeName.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "Debate";
                AutoCompleteExtender3.ContextKey = "Debate";
            }
            else
            {
                //lnkContinue.Visible = true;
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
            }
        }
        else if (ddlMilestone.SelectedItem.Text == "Alternate Dispute Resolution Competition")
        {
            lnkSkip.Visible = true;
            if (lnkSaveAchivemnt.Visible == true)
            {
                //lnkContinue.Visible = false;
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "1,2,3";
                Bind_ddlPosition(Ids);
                divCompetition.Visible = true;
                divAdditionalAwrd.Visible = true;
                divDescription.Visible = true;
                divCommitteeName.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "ADR";
                AutoCompleteExtender3.ContextKey = "ADR";
            }
            else
            {
                //lnkContinue.Visible = true;
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
            }
        }
        else if (ddlMilestone.SelectedItem.Text == "Publications")
        {
            lnkSkip.Visible = true;
            if (lnkSaveAchivemnt.Visible == true)
            {
                //lnkContinue.Visible = false;
                string Ids = "";
                //Bind_ddlPosition(Ids);
                divJournalName.Visible = true;
                divCompetition.Visible = false;
                divAdditionalAwrd.Visible = false;
                divCommitteeName.Visible = false;
                divDescription.Visible = false;
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
                hdnCompetitionType.Value = "Publications";
                AutoCompleteExtender4.ContextKey = "Publications";
            }
            else
            {
                //lnkContinue.Visible = true;
            }
        }
        else if (ddlMilestone.SelectedItem.Text == "Committee Membership")
        {
            lnkSkip.Visible = true;
            if (lnkSaveAchivemnt.Visible == true)
            {
                //lnkContinue.Visible = false;
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "7,8,9,10,11";
                Bind_ddlPosition(Ids);
                divDescription.Visible = true;
                divCommitteeName.Visible = true;
                divCompetition.Visible = false;
                divAdditionalAwrd.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "Committee Membership";
                AutoCompleteExtender3.ContextKey = "Committee Membership";
            }
            else
            {
                //lnkContinue.Visible = true;
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
            }
        }
        else if (ddlMilestone.SelectedItem.Text == "Student Body")
        {
            lnkSkip.Visible = true;
            if (lnkSaveAchivemnt.Visible == true)
            {
                //lnkContinue.Visible = false;
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "8,9";
                Bind_ddlPosition(Ids);
                divDescription.Visible = true;
                divCompetition.Visible = false;
                divAdditionalAwrd.Visible = false;
                divCommitteeName.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "Student Body";
                AutoCompleteExtender3.ContextKey = "Student Body";
            }
            else
            {
                //lnkContinue.Visible = true;
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
            }
        }
        else
        {
            string Ids = "";
            Bind_ddlPosition(Ids);
            divCompetition.Visible = false;
            divAdditionalAwrd.Visible = false;
            divCommitteeName.Visible = false;
            divDescription.Visible = false;
            divJournalName.Visible = false;
            //AutoCompleteExtender3.ContextKey = "0";
            //lnkContinue.Visible = false;
            lnkSkip.Visible = false;
            lnkSaveAchivemnt.Visible = false;
            divddlPosition.Visible = false;
            ddlPosition.Visible = false;
        }
    }

    protected void RedirectToAchivements(string flag)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("SignUp3.aspx?ActiveStatus=Ac&RegId=" + Request.QueryString["RegId"]);
        }
        if (flag == "true")
        {
            Response.Redirect("SignUp3.aspx?ActiveStatus=Ac&RegId=" + Request.QueryString["RegId"]);
        }
        else if (flag == "update")
        {
            Response.Redirect("SignUp3.aspx?ActiveStatus=Ac&RegId=" + Request.QueryString["RegId"] + "&Status=Update");
        }
        else
        {
            Response.Redirect("SignUp3.aspx?ActiveStatus=Ac&RegId=" + Request.QueryString["RegId"] + "&Status=Cancel");
        }
    }

    private void BindDocuments()
    {
        //if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        //{
        //    lnkuploadDoc.Visible = false;
        //    DocDO.AddedBy = Convert.ToInt32(ViewState["RegId"]);
        //    DocDO.FriendId = Convert.ToInt32(Session["ExternalUserId"]);
        //    dt = DocDA.GetDataTable(DocDO, DA_ProfileDocuments.DocumenTemp.GetPublicDocsForFriend);
        //}
        //else
        //{
        //    DocDO.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        //    dt = DocDA.GetDataTable(DocDO, DA_ProfileDocuments.DocumenTemp.Getdocuments);
        //}

        //if (dt.Rows.Count > 0)
        //{
        //    LstDocument.DataSource = dt;
        //    LstDocument.DataBind();
        //}
        //else
        //{
        //    LstDocument.DataSource = null;
        //    LstDocument.DataBind();
        //}
    }

    protected void lnkAddachive_Click(object sender, EventArgs e)
    {
        divAchivement.Style.Add("display", "block");
        LoadAchivment();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallAchiveMiddle();", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAchi2", "CallAchLI();", true);
        ClearAchivement();
    }

    protected void ClearAchivement()
    {
        ViewState["hdnintAchivmentId"] = null;
        txtCompitition.Text = "";
        ddlPosition.SelectedIndex = 0;
        ddlMilestone.SelectedIndex = 0;
        txtAdditionalAwrd.Text = "";
        txtAchivDescription.InnerText = "";
        txtCommitteeName.Text = "";
        lblAchivmentmsg.Text = "";
        ViewState["Callsmoothmenu"] = "smooth";
        txtJournalName.Text = "";
    }

    protected void lstAchivement_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlControl divAchivementED = (HtmlControl)e.Item.FindControl("divAchivementED");
        //if (hdnintAddedBy.Value != ViewState["UserID"].ToString())
        //{
        divAchivementED.Style.Add("display", "block");
        //}

        //if (ddlMilestone.SelectedItem.Text == "Publications")
        //{
        //    divddlPosition.Visible = false;
        //    ddlPosition.Visible = false;
        //    string Ids = "";
        //    Bind_ddlPosition(Ids);
        //    divJournalName.Visible = true;
        //    divCompetition.Visible = false;
        //    divAdditionalAwrd.Visible = false;
        //    divCommitteeName.Visible = false;
        //    divDescription.Visible = false;
        //    hdnCompetitionType.Value = "Publications";
        //    AutoCompleteExtender4.ContextKey = "Publications";
        //}
        //else if (ddlMilestone.SelectedItem.Text == "Moot Court Competition")
        //{
        //    divddlPosition.Visible = true;
        //    ddlPosition.Visible = true;
        //    string Ids = "1,2,3,4,5";
        //    Bind_ddlPosition(Ids);
        //    divCompetition.Visible = true;
        //    divAdditionalAwrd.Visible = true;
        //    divDescription.Visible = true;
        //    divCommitteeName.Visible = false;
        //    divJournalName.Visible = false;
        //    hdnCompetitionType.Value = "Moot";
        //    AutoCompleteExtender3.ContextKey = "Moot";
        //}
        //else if (ddlMilestone.SelectedItem.Text == "Debate Competition")
        //{
        //    divddlPosition.Visible = true;
        //    ddlPosition.Visible = true;
        //    string Ids = "1,2,3,6";
        //    Bind_ddlPosition(Ids);
        //    divCompetition.Visible = true;
        //    divAdditionalAwrd.Visible = true;
        //    divDescription.Visible = true;
        //    divCommitteeName.Visible = false;
        //    divJournalName.Visible = false;
        //    hdnCompetitionType.Value = "Debate";
        //    AutoCompleteExtender3.ContextKey = "Debate";
        //}
        //else if (ddlMilestone.SelectedItem.Text == "Alternate Dispute Resolution Competition")
        //{
        //    divddlPosition.Visible = true;
        //    ddlPosition.Visible = true;
        //    string Ids = "1,2,3";
        //    Bind_ddlPosition(Ids);
        //    divCompetition.Visible = true;
        //    divAdditionalAwrd.Visible = true;
        //    divDescription.Visible = true;
        //    divCommitteeName.Visible = false;
        //    divJournalName.Visible = false;
        //    hdnCompetitionType.Value = "ADR";
        //    AutoCompleteExtender3.ContextKey = "ADR";
        //}
        //else if (ddlMilestone.SelectedItem.Text == "Committee Membership")
        //{
        //    divddlPosition.Visible = true;
        //    ddlPosition.Visible = true;
        //    string Ids = "7,8,9,10,11";
        //    Bind_ddlPosition(Ids);
        //    divDescription.Visible = true;
        //    divCommitteeName.Visible = true;
        //    divCompetition.Visible = false;
        //    divAdditionalAwrd.Visible = false;
        //    divJournalName.Visible = false;
        //    hdnCompetitionType.Value = "Committee Membership";
        //    AutoCompleteExtender3.ContextKey = "Committee Membership";
        //}
        //else if (ddlMilestone.SelectedItem.Text == "Student Body")
        //{
        //    divddlPosition.Visible = true;
        //    ddlPosition.Visible = true;
        //    string Ids = "8,9";
        //    Bind_ddlPosition(Ids);
        //    divDescription.Visible = true;
        //    divCompetition.Visible = false;
        //    divAdditionalAwrd.Visible = false;
        //    divCommitteeName.Visible = false;
        //    divJournalName.Visible = false;
        //    hdnCompetitionType.Value = "Student Body";
        //    AutoCompleteExtender3.ContextKey = "Student Body";
        //}
        //else
        //{
        //    divddlPosition.Visible = true;
        //    ddlPosition.Visible = true;
        //}
        //divCtrls.Visible = true;
        ////lnkContinue.Visible = false;
        //lnkSaveAchivemnt.Visible = true;
    }

    protected void LoadMilestones()
    {
        objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        DataTable dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetMilestones);

        if (dt.Rows.Count > 0)
        {
            ddlMilestone.DataSource = dt;
            ddlMilestone.DataTextField = "strMilestoneName";
            ddlMilestone.DataValueField = "intMilestoneId";
            ddlMilestone.DataBind();

            if (ddlMilestone.SelectedItem.Text == "Publications")
            {
                divddlPosition.Visible = false;
                ddlPosition.Visible = false;
                string Ids = "";
                Bind_ddlPosition(Ids);
                divJournalName.Visible = true;
                divCompetition.Visible = false;
                divAdditionalAwrd.Visible = false;
                divCommitteeName.Visible = false;
                divDescription.Visible = false;
                hdnCompetitionType.Value = "Publications";
                AutoCompleteExtender4.ContextKey = "Publications";
            }
            else if (ddlMilestone.SelectedItem.Text == "Moot Court Competition")
            {
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "1,2,3,4,5";
                Bind_ddlPosition(Ids);
                divCompetition.Visible = true;
                divAdditionalAwrd.Visible = true;
                divDescription.Visible = true;
                divCommitteeName.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "Moot";
                AutoCompleteExtender3.ContextKey = "Moot";
            }
            else if (ddlMilestone.SelectedItem.Text == "Debate Competition")
            {
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "1,2,3,6";
                Bind_ddlPosition(Ids);
                divCompetition.Visible = true;
                divAdditionalAwrd.Visible = true;
                divDescription.Visible = true;
                divCommitteeName.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "Debate";
                AutoCompleteExtender3.ContextKey = "Debate";
            }
            else if (ddlMilestone.SelectedItem.Text == "Alternate Dispute Resolution Competition")
            {
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "1,2,3";
                Bind_ddlPosition(Ids);
                divCompetition.Visible = true;
                divAdditionalAwrd.Visible = true;
                divDescription.Visible = true;
                divCommitteeName.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "ADR";
                AutoCompleteExtender3.ContextKey = "ADR";
            }
            else if (ddlMilestone.SelectedItem.Text == "Committee Membership")
            {
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "7,8,9,10,11";
                Bind_ddlPosition(Ids);
                divDescription.Visible = true;
                divCommitteeName.Visible = true;
                divCompetition.Visible = false;
                divAdditionalAwrd.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "Committee Membership";
                AutoCompleteExtender3.ContextKey = "Committee Membership";
            }
            else if (ddlMilestone.SelectedItem.Text == "Student Body")
            {
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
                string Ids = "8,9";
                Bind_ddlPosition(Ids);
                divDescription.Visible = true;
                divCompetition.Visible = false;
                divAdditionalAwrd.Visible = false;
                divCommitteeName.Visible = false;
                divJournalName.Visible = false;
                hdnCompetitionType.Value = "Student Body";
                AutoCompleteExtender3.ContextKey = "Student Body";
            }
            else
            {
                divddlPosition.Visible = true;
                ddlPosition.Visible = true;
            }
            divCtrls.Visible = true;
            //lnkContinue.Visible = false;
            lnkSaveAchivemnt.Visible = true;
            //ddlMilestone.Items.Insert(0, new ListItem("Type of Milestone", "0"));
        }
        else
        {
            ddlMilestone.DataSource = null;
            ddlMilestone.DataBind();
        }
        //ddlPosition.Items.Insert(0, new ListItem("Position", "0"));        
    }

    protected void lstAchivement_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnintAchivmentId = (HiddenField)e.Item.FindControl("hdnintAchivmentId");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        objDOEdu.intAchivmentId = Convert.ToInt32(hdnintAchivmentId.Value);
        string selectedPos = null;
        if (e.CommandName == "EditAch")
        {
            //   PopUpCropImage.Style.Add("display", "none");
            divHeadingLstAchievement.Visible = false;
            divHeaderLstAchievement.Visible = false;
            //divHeaderSubAchievement.Visible = false;
            lnkSaveAchivemnt.Text = "Update";
            //lnkSkip.Visible = true;
            LoadMilestones();
            divAchivement.Style.Add("display", "block");
            add_achivement.Style.Add("display", "block");

            ViewState["hdnintAchivmentId"] = hdnintAchivmentId.Value;
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.SingleRecordAchivemrnt);

            if (dt.Rows.Count > 0)
            {
                //lnkContinue.Visible = false;
                lnkSaveAchivemnt.Visible = true;

                //txtCompitition.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                ddlMilestone.SelectedValue = Convert.ToString(dt.Rows[0]["intMilestoneId"]);
                txtAdditionalAwrd.Text = Convert.ToString(dt.Rows[0]["strAdditionalAward"]);
                txtAchivDescription.InnerText = Convert.ToString(dt.Rows[0]["strDescription"]);

                if (Convert.ToString(dt.Rows[0]["intPostionId"]) != "" && Convert.ToString(dt.Rows[0]["intPostionId"]) != null)
                {
                    try
                    {
                        divddlPosition.Visible = true;
                        divCtrls.Visible = true;
                        ddlPosition.Visible = true;
                        selectedPos = Convert.ToString(dt.Rows[0]["intPostionId"]);
                        //Bind_ddlPosition(ddlMilestone.SelectedValue);
                        //ddlPosition.SelectedValue = dt.Rows[0]["intPostionId"].ToString().Trim();
                    }
                    catch (Exception ex)
                    {
                    }
                }

                if (ddlMilestone.SelectedItem.Text == "Moot Court Competition")
                {
                    string Ids = "1,2,3,4,5";
                    Bind_ddlPosition(Ids, selectedPos);

                    //divCtrls.Visible = true;
                    divCompetition.Visible = true;
                    divAdditionalAwrd.Visible = true;
                    divDescription.Visible = true;
                    divCommitteeName.Visible = false;
                    divJournalName.Visible = false;
                    //divddlPosition.Visible = true;
                    //ddlPosition.Visible = true;
                    hdnCompetitionType.Value = "Moot";
                    AutoCompleteExtender3.ContextKey = "Moot";
                    txtCompitition.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                    ddlMilestone.Enabled = false;
                }
                else if (ddlMilestone.SelectedItem.Text == "Debate Competition")
                {
                    string Ids = "1,2,3,6";
                    Bind_ddlPosition(Ids, selectedPos);
                    divCompetition.Visible = true;
                    divAdditionalAwrd.Visible = true;
                    divDescription.Visible = true;
                    divCtrls.Visible = true;
                    divCommitteeName.Visible = false;
                    divJournalName.Visible = false;
                    divddlPosition.Visible = true;
                    ddlPosition.Visible = true;
                    hdnCompetitionType.Value = "Debate";
                    AutoCompleteExtender3.ContextKey = "Debate";
                    txtCompitition.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                    ddlMilestone.Enabled = false;
                }
                else if (ddlMilestone.SelectedItem.Text == "Alternate Dispute Resolution Competition")
                {
                    string Ids = "1,2,3";
                    Bind_ddlPosition(Ids, selectedPos);
                    divCtrls.Visible = true;
                    divCompetition.Visible = true;
                    divAdditionalAwrd.Visible = true;
                    divDescription.Visible = true;
                    divCommitteeName.Visible = false;
                    divJournalName.Visible = false;
                    divddlPosition.Visible = true;
                    ddlPosition.Visible = true;
                    hdnCompetitionType.Value = "ADR";
                    AutoCompleteExtender3.ContextKey = "ADR";
                    txtCompitition.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                    ddlMilestone.Enabled = false;
                }
                else if (ddlMilestone.SelectedItem.Text == "Publications")
                {
                    //string Ids = "";
                    //Bind_ddlPosition(Ids);
                    divJournalName.Visible = true;
                    divCompetition.Visible = false;
                    divAdditionalAwrd.Visible = false;
                    divCommitteeName.Visible = false;
                    divDescription.Visible = false;
                    divddlPosition.Visible = false;
                    hdnCompetitionType.Value = "Publications";
                    AutoCompleteExtender4.ContextKey = "Publications";
                    txtJournalName.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                    ddlMilestone.Enabled = false;
                }
                else if (ddlMilestone.SelectedItem.Text == "Committee Membership")
                {
                    string Ids = "7,8,9,10,11";
                    Bind_ddlPosition(Ids, selectedPos);
                    divCtrls.Visible = true;
                    divDescription.Visible = true;
                    divCommitteeName.Visible = true;
                    divCompetition.Visible = false;
                    divAdditionalAwrd.Visible = false;
                    divJournalName.Visible = false;
                    divddlPosition.Visible = true;
                    ddlPosition.Visible = true;
                    hdnCompetitionType.Value = "Committee Membership";
                    AutoCompleteExtender3.ContextKey = "Committee Membership";
                    txtCompitition.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                    ddlMilestone.Enabled = false;
                }
                else if (ddlMilestone.SelectedItem.Text == "Student Body")
                {
                    string Ids = "8,9";
                    Bind_ddlPosition(Ids, selectedPos);
                    divCtrls.Visible = true;
                    divDescription.Visible = true;
                    divCompetition.Visible = false;
                    divAdditionalAwrd.Visible = false;
                    divCommitteeName.Visible = false;
                    divJournalName.Visible = false;
                    divddlPosition.Visible = true;
                    ddlPosition.Visible = true;
                    hdnCompetitionType.Value = "Student Body";
                    AutoCompleteExtender3.ContextKey = "Student Body";
                    txtCompitition.Text = Convert.ToString(dt.Rows[0]["strCompititionName"]);
                    ddlMilestone.Enabled = false;
                }
                else
                {
                    //string Ids = "";
                    //Bind_ddlPosition(Ids);
                    divCompetition.Visible = false;
                    divAdditionalAwrd.Visible = false;
                    divCommitteeName.Visible = false;
                    divDescription.Visible = false;
                    divJournalName.Visible = false;
                    divddlPosition.Visible = false;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallAchiveMiddle();", true);
        }
    }

    protected void lnkEducation_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            isFriend = true;
        }
        hdnActive.Value = "E";
        //divEducation.Style.Add("display", "none");
        Education();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallEducation", "CallEduLI();", true);
    }

    protected void Education()
    {
        if (isFriend)
        {
            lnlAddEducation.Visible = false;
        }
        else
        {
            lnlAddEducation.Visible = true;
        }
        lnkAchievements.CssClass = "nav-link";
        ////lnkEducation.Style.Add("border-bottom", "2px solid #00b6bc");
        ////lnkHome.Style.Add("border-bottom", "none");
        ////lnkDocuments.Style.Add("border-bottom", "none");
        ////lnkWorkex.Style.Add("border-bottom", "none");
        ////lnkAchievements.Style.Add("border-bottom", "none");
        ////PnlHome.Visible = false;
        //PnlDocuments.Visible = false;
        //lnkWorkex.CssClass = "nav-link";
        lnkEducation.CssClass = "nav-link active show";
        //lnkDocuments.CssClass = "nav-link";
        //PnlWorkex.Visible = false;
        PnlEduction.Visible = true;
        PnlAchivement.Visible = false;
        ////PnlEduction.Visible = true;
        ////PnlAchivement.Visible = false;
        LoadEducation();
        LoadYear();

    }

    protected void LoadYear()
    {
        DataTable dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.LoadYear);
        if (dt.Rows.Count > 0)
        {
            txtEducationFromdt.DataSource = dt;
            txtEducationFromdt.DataTextField = "intYear";
            txtEducationFromdt.DataValueField = "intYear";
            txtEducationFromdt.DataBind();
            txtEducationFromdt.Items.Insert(0, new ListItem("Year"));

            txtEducationTodt.DataSource = dt;
            txtEducationTodt.DataTextField = "intYear";
            txtEducationTodt.DataValueField = "intYear";
            txtEducationTodt.DataBind();
            txtEducationTodt.Items.Insert(0, new ListItem("Year"));
        }
    }

    protected void lnkAchievements_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            isFriend = true;
        }
        hdnActive.Value = "Ac";
        Achivement();
        divAchivement.Style.Add("display", "none");
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallAchi", "CallAchLI();", true);
    }

    protected void Achivement()
    {
        //Master.BodyTag.Attributes.Add("class", "add-scroller");
        lnkAchievements.CssClass = "nav-link active show";
        // lnkHome.Style.Add("border-bottom", "none");
        // lnkDocuments.Style.Add("border-bottom", "none");
        if (isFriend)
            lnkAddachive.Visible = false;
        else
            lnkAddachive.Visible = true;
        //lnkWorkex.CssClass = "nav-link";
        lnkEducation.CssClass = "nav-link";
        //lnkDocuments.CssClass = "nav-link";
        //  PnlHome.Visible = false;

        PnlEduction.Visible = false;
        PnlAchivement.Visible = true;
        LoadAchivment();
        LoadMilestones();
    }

    protected void Bind_ddlPosition(string Ids)
    {
        Bind_ddlPosition(Ids, null);
    }

    protected void Bind_ddlPosition(string Ids, string selectedPos)
    {
        ddlPosition.Items.Clear();
        //ddlPosition.Items.Add("Select State");
        objDOEdu.PositionIdList = Ids;
        DataTable dtt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetPositionList);

        if (dtt.Rows.Count > 0)
        {
            //divddlPosition.Visible = true;
            //ddlPosition.Visible = true;
            ddlPosition.DataSource = dtt;
            ddlPosition.DataTextField = "strPositionName";
            ddlPosition.DataValueField = "intPostionId";
            ddlPosition.DataBind();
            if (selectedPos != null)
            {
                ddlPosition.SelectedValue = selectedPos;
            }
        }
        else
        {
            ddlPosition.DataSource = null;
            ddlPosition.DataBind();
            ddlPosition.Items.Insert(0, "Position");
        }
    }

    protected void LoadEducation()
    {
        objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            lnlAddEducation.Visible = false;
            objDOEdu.intAddedBy = Convert.ToInt32(ViewState["RegId"]);
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetEducationOnly);
            if (dt.Rows.Count > 0)
            {
                lstEducation.DataSource = dt;
                lstEducation.DataBind();
            }
            else
            {
                lstEducation.DataSource = null;
                lstEducation.DataBind();
            }
        }
        else
        {
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetEducationOnly);
            if (dt.Rows.Count > 0)
            {
                lstEducation.DataSource = dt;
                lstEducation.DataBind();
            }
            else
            {
                lstEducation.DataSource = null;
                lstEducation.DataBind();
            }
        }
    }

    protected void LoadAchivment()
    {
        objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            lnkAddachive.Visible = false;
            objDOEdu.intAddedBy = Convert.ToInt32(ViewState["RegId"]);
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetAchivementOnly);
            if (dt.Rows.Count > 0)
            {
                lstAchivement.DataSource = dt;
                lstAchivement.DataBind();
            }
            else
            {
                lstAchivement.DataSource = null;
                lstAchivement.DataBind();
            }
        }
        else
        {
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetAchivementOnly);
            if (dt.Rows.Count > 0)
            {
                lstAchivement.DataSource = dt;
                lstAchivement.DataBind();
            }
            else
            {
                lstAchivement.DataSource = null;
                lstAchivement.DataBind();
            }
        }
        //divDeletesucess.Style.Add("display", "none");
    }

    protected void RedirectToEducation()
    {
        if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
        {
            Response.Redirect("SignUp3.aspx?ActiveStatus=E&RegId=" + Request.QueryString["RegId"]);
        }
        Response.Redirect("SignUp3.aspx?ActiveStatus=E");
    }

    protected void lnkSaveEducation_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        objDOEdu.intRegistrationId = Convert.ToInt32(ViewState["UserID"]);
        string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ip == null)
            ip = Request.ServerVariables["REMOTE_ADDR"];

        if (txtTitle.Text != "")
        {
            if (Validations.validateJavaScriptInput(txtTitle.Text))
            {
                objDOEdu.strDegree = txtTitle.Text.Trim().Replace("'", "''");
                objDOEdu.intDegreeId = objDAEdu.GetintDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetDeegreeid);
            }
            else
            {
                lblMessage.Text = "Degree is not valid.";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Degree is not valid');</script>", false);
                return;
            }
        }
        else
            return;

        if (txtInstitute.Text != "")
        {
            if (Validations.validateJavaScriptInput(txtInstitute.Text))
            {
                objDOEdu.strInstituteName = txtInstitute.Text.Trim().Replace("'", "''");
                objDOEdu.intInstituteId = objDAEdu.GetintDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.GetInstituteid);
            }
            else
            {
                lblMessage.Text = "Institute Name is not valid.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "edupopu12p", "OverlayBody();", true);
                //  ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('Institute Name is not valid');</script>", false);
                return;
            }

        }
        else
            return;

        //if (txtDescription.InnerText != "Description")
        //    objDOEdu.strSpclLibrary = txtEduDescription.InnerText.Trim();
        if (txtEducationFromdt.Text.Trim() != "Year")
            objDOEdu.intYear = Convert.ToInt32(txtEducationFromdt.Text.Trim());
        else
            return;
        if (txtEducationTodt.Text.Trim() != "Year")
            objDOEdu.intToYear = Convert.ToInt32(txtEducationTodt.Text.Trim());
        else
            return;

        if (ddlFromMonth.Value == "Jan")
        {
            objDOEdu.intMonth = 1;
        }
        else if (ddlFromMonth.Value == "Feb")
        {
            objDOEdu.intMonth = 2;
        }
        else if (ddlFromMonth.Value == "Mar")
        {
            objDOEdu.intMonth = 3;
        }
        else if (ddlFromMonth.Value == "Apr")
        {
            objDOEdu.intMonth = 4;
        }
        else if (ddlFromMonth.Value == "May")
        {
            objDOEdu.intMonth = 5;
        }
        else if (ddlFromMonth.Value == "Jun")
        {
            objDOEdu.intMonth = 6;
        }
        else if (ddlFromMonth.Value == "Jul")
        {
            objDOEdu.intMonth = 7;
        }
        else if (ddlFromMonth.Value == "Aug")
        {
            objDOEdu.intMonth = 8;
        }
        else if (ddlFromMonth.Value == "Sep")
        {
            objDOEdu.intMonth = 9;
        }
        else if (ddlFromMonth.Value == "Oct")
        {
            objDOEdu.intMonth = 10;
        }
        else if (ddlFromMonth.Value == "Nov")
        {
            objDOEdu.intMonth = 11;
        }
        else if (ddlFromMonth.Value == "Dec")
        {
            objDOEdu.intMonth = 12;
        }
        else
        {
            objDOEdu.intMonth = 1;
        }

        if (ddlToMonth.SelectedValue == "Jan")
        {
            objDOEdu.intToMonth = 1;
        }
        else if (ddlToMonth.SelectedValue == "Feb")
        {
            objDOEdu.intToMonth = 2;
        }
        else if (ddlToMonth.SelectedValue == "Mar")
        {
            objDOEdu.intToMonth = 3;
        }
        else if (ddlToMonth.SelectedValue == "Apr")
        {
            objDOEdu.intToMonth = 4;
        }
        else if (ddlToMonth.SelectedValue == "May")
        {
            objDOEdu.intToMonth = 5;
        }
        else if (ddlToMonth.SelectedValue == "Jun")
        {
            objDOEdu.intToMonth = 6;
        }
        else if (ddlToMonth.SelectedValue == "Jul")
        {
            objDOEdu.intToMonth = 7;
        }
        else if (ddlToMonth.SelectedValue == "Aug")
        {
            objDOEdu.intToMonth = 8;
        }
        else if (ddlToMonth.SelectedValue == "Sep")
        {
            objDOEdu.intToMonth = 9;
        }
        else if (ddlToMonth.SelectedValue == "Oct")
        {
            objDOEdu.intToMonth = 10;
        }
        else if (ddlToMonth.SelectedValue == "Nov")
        {
            objDOEdu.intToMonth = 11;
        }
        else if (ddlToMonth.SelectedValue == "Dec")
        {
            objDOEdu.intToMonth = 12;
        }
        else
        {
            objDOEdu.intToMonth = 1;
        }

        objDOEdu.intToYear = Convert.ToInt32(txtEducationTodt.Text);
        //if (chkAtPresent.Checked == true)
        //{
        //    objDOEdu.intToMonth = DateTime.Now.Month;
        //}

        if (Convert.ToInt32(objDOEdu.intToYear) < Convert.ToInt32(objDOEdu.intYear))
        {
            lblMessage.Text = "From date should not be greater than to date.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "edu13popup", "OverlayBody();", true);
            return;
        }
        else if (Convert.ToInt32(objDOEdu.intToYear) == Convert.ToInt32(objDOEdu.intYear))
        {
            if (Convert.ToInt32(objDOEdu.intToMonth) <= Convert.ToInt32(objDOEdu.intMonth))
            {
                lblMessage.Text = "From date should not be greater than to date.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "edupopup13", "OverlayBody();", true);
                return;
            }
        }

        if (txtPercentile.Text != null && txtPercentile.Text != "")
        {
            objDOEdu.stud_percentile = Convert.ToDecimal(txtPercentile.Text);
        }

        objDOEdu.strEducationAchievement = "Education";
        objDOEdu.strIpAddress = ip;
        objDOEdu.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        objDOEdu.dtDate = DateTime.Now;
        objDOEdu.strSpclLibrary = txtEduDescription.InnerText.Trim();

        if (cbScorePrivate.Checked)
        {
            objDOEdu.strIsScorePublic = 1;
            Session["isScorePrivate"] = 1;
        }
        else
        {
            objDOEdu.strIsScorePublic = 0;
            Session["isScorePrivate"] = 0;
        }

        if (ViewState["intEducationId"] == null)
        {
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.Insert);
        }
        else
        {
            objDOEdu.intEducationId = Convert.ToInt32(ViewState["intEducationId"]);
            objDAEdu.AddEditDel_Scrl_UserEducationTbl(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.Update);
            objDOEdu.intOutId = 1;
        }
        if (objDOEdu.intOutId == 0)
        {
            lblMessage.Text = "Record Already Inserted..!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "edupopup", "OverlayBody();", true);
            return;
        }

        string ISAPIURLACCESSED = ConfigurationManager.AppSettings["ISAPIURLACCESSED"];
        //if (ISAPIURLACCESSED != "0")
        //{
        //    try
        //    {
        //        StringBuilder url = new StringBuilder();
        //        url.Append(APIURL);
        //        url.Append("updateUserEducationDetails.action?");
        //        url.Append("uid=");
        //        url.Append(UserTypeId + Convert.ToInt32(ViewState["UserID"]));
        //        url.Append("&educationId=");
        //        url.Append(objDOEdu.intOutId);
        //        url.Append("&degree=");
        //        url.Append(objDOEdu.strDegree);
        //        url.Append("&institution=");
        //        url.Append(objDOEdu.strInstituteName);
        //        url.Append("&year=");
        //        url.Append(objDOEdu.intYear);
        //        url.Append("&educationType=");
        //        url.Append(objDOEdu.strEducationAchievement);
        //        url.Append("&achievementMarked=");
        //        url.Append(1);
        //        url.Append("&description=");
        //        url.Append(objDOEdu.strSpclLibrary);

        //        HttpWebRequest myRequest1 = (HttpWebRequest)WebRequest.Create(url.ToString());


        //        myRequest1.Method = "GET";
        //        if (ISAPIResponse != "0")
        //        {
        //            WebResponse myResponse1 = myRequest1.GetResponse();
        //            StreamReader sr = new StreamReader(myResponse1.GetResponseStream(), System.Text.Encoding.UTF8);
        //            String result = sr.ReadToEnd();
        //            objAPILogDO.strAPIType = "Profile Education";
        //            objAPILogDO.strResponse = result;
        //            objAPILogDO.strIPAddress = ip;
        //            objAPILogDO.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        //            objAPILogDA.AddEditDel_Scrl_APILogDetailsTbl(objAPILogDO, DA_Scrl_APILogDetailsTbl.Scrl_APILogDetailsTbl.Insert);
        //        }
        //    }
        //    catch { }
        //}
        RedirectToEducation();
    }

    protected void chkEducation_CheckedChanged1(object sender, EventArgs e)
    {
        if (chkEducation.Checked == true)
        {
            // txtEducationTodt.SelectedValue = DateTime.Now.Year.ToString();
            // txtEducationFromdt.SelectedIndex = txtEducationTodt.Items.Count - 1;
            txtEducationTodt.SelectedIndex = 1;//txtEducationTodt.Items.Count - 1;
            txtEducationTodt.Enabled = false;
            int i = DateTime.Now.Month;
            ddlToMonth.SelectedIndex = i;
            ddlToMonth.Enabled = false;
        }
        else
        {
            txtEducationTodt.SelectedIndex = 0;
            txtEducationTodt.Enabled = true;
            ddlToMonth.Enabled = true;
            ddlToMonth.SelectedIndex = 0;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "saveAchiv", "OverlayBody();", true);
    }

    protected void lstEducation_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        HtmlControl divEducationED = (HtmlControl)e.Item.FindControl("divEducationED");
        Label lblintToMonth = (Label)e.Item.FindControl("lblintToMonth");
        Label lblintToYear = (Label)e.Item.FindControl("lblintToYear");
        Label lblPresentDay = (Label)e.Item.FindControl("lblPresentDay");
        HiddenField hdnToMonth = (HiddenField)e.Item.FindControl("hdnToMonth");

        //if (hdnintAddedBy.Value != ViewState["UserID"].ToString())
        //{
        divEducationED.Style.Add("display", "none");
        //}
        if (hdnToMonth.Value != "")
        {
            if (Convert.ToInt32(hdnToMonth.Value) == DateTime.Now.Month)
            {
                lblintToMonth.Visible = false;
                lblintToYear.Visible = false;
                lblPresentDay.Text = "Present Day";
                // chkEducation.Checked = true;
            }
        }
        else
        {
            lblintToMonth.Visible = false;
            lblintToYear.Visible = false;
            lblPresentDay.Text = "Present Day";
            // chkEducation.Checked = true;
        }
    }

    [System.Web.Script.Services.ScriptMethod]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlCommand com = new SqlCommand())
            {
                //com.CommandText = "select strInstituteName from Scrl_InstituteNameTbl where " + "strInstituteName like @Search + '%'";
                com.CommandText = "Select Top 5 strInstituteName from Scrl_InstituteNameTbl where " + "strInstituteName Like '%' + LTRIM(RTRIM(@Search)) + '%' Order By strInstituteName";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> InstituteName = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        InstituteName.Add(sdr["strInstituteName"].ToString());
                    }
                }
                con.Close();
                return InstituteName;
            }
        }
    }

    protected void lstEducation_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnintEducationId = (HiddenField)e.Item.FindControl("hdnintEducationId");
        HiddenField hdnintAddedBy = (HiddenField)e.Item.FindControl("hdnintAddedBy");
        objDOEdu.intEducationId = Convert.ToInt32(hdnintEducationId.Value);
        if (e.CommandName == "EditEdu")
        {
            //  PopUpCropImage.Style.Add("display", "none");
            divEducation.Style.Add("display", "block");
            dt = objDAEdu.GetDataTable(objDOEdu, DA_Scrl_UserEducationTbl.Scrl_UserEducationTbl.SingleRecord);
            if (dt.Rows.Count > 0)
            {
                ViewState["intEducationId"] = dt.Rows[0]["intEducationId"].ToString();

                txtInstitute.Text = Convert.ToString(dt.Rows[0]["strInstituteName"]);
                txtTitle.Text = Convert.ToString(dt.Rows[0]["strDegree"]);
                string yearFromvalue = Convert.ToString(dt.Rows[0]["intYear"]);
                string yearTovalue = Convert.ToString(dt.Rows[0]["intToYear"]);
                int toMonth = Convert.ToInt16(dt.Rows[0]["intToMonth"]);
                int intToYear = Convert.ToInt16(dt.Rows[0]["intToYear"]);

                //if (DateTime.Now.Month == toMonth && ((DateTime.Now.Year - 1) == intToYear))
                if (DateTime.Now.Month == toMonth && ((DateTime.Now.Year) == intToYear))
                {
                    chkEducation.Checked = true;
                }

                txtEducationTodt.SelectedIndex = txtEducationTodt.Items.IndexOf(txtEducationTodt.Items.FindByValue(yearTovalue));
                txtEducationFromdt.SelectedIndex = txtEducationFromdt.Items.IndexOf(txtEducationFromdt.Items.FindByValue(yearFromvalue));
                txtEduDescription.InnerText = Convert.ToString(dt.Rows[0]["strSpclLibrary"]);
                SelectEducationMonth(dt);

                if (chkEducation.Checked)
                {
                    txtEducationTodt.Enabled = false;
                    ddlToMonth.Enabled = false;
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "CallEducationMiddle();", true);
        }
    }

    public void SelectEducationMonth(DataTable dtt)
    {
        if (dtt.Rows[0]["intMonth"].ToString() == "1")
        {
            ddlFromMonth.Value = "Jan";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "2")
        {
            ddlFromMonth.Value = "Feb";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "3")
        {
            ddlFromMonth.Value = "Mar";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "4")
        {
            ddlFromMonth.Value = "Apr";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "5")
        {
            ddlFromMonth.Value = "May";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "6")
        {
            ddlFromMonth.Value = "Jun";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "7")
        {
            ddlFromMonth.Value = "Jul";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "8")
        {
            ddlFromMonth.Value = "Aug";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "9")
        {
            ddlFromMonth.Value = "Sep";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "10")
        {
            ddlFromMonth.Value = "Oct";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "11")
        {
            ddlFromMonth.Value = "Nov";
        }
        else if (dtt.Rows[0]["intMonth"].ToString() == "12")
        {
            ddlFromMonth.Value = "Dec";
        }
        else
        {
            ddlFromMonth.Value = "Jan";
        }

        if (dtt.Rows[0]["intToMonth"].ToString() == "1")
        {
            ddlToMonth.SelectedValue = "Jan";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "2")
        {
            ddlToMonth.SelectedValue = "Feb";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "3")
        {
            ddlToMonth.SelectedValue = "Mar";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "4")
        {
            ddlToMonth.SelectedValue = "Apr";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "5")
        {
            ddlToMonth.SelectedValue = "May";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "6")
        {
            ddlToMonth.SelectedValue = "Jun";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "7")
        {
            ddlToMonth.SelectedValue = "Jul";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "8")
        {
            ddlToMonth.SelectedValue = "Aug";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "9")
        {
            ddlToMonth.SelectedValue = "Sep";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "10")
        {
            ddlToMonth.SelectedValue = "Oct";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "11")
        {
            ddlToMonth.SelectedValue = "Nov";
        }
        else if (dtt.Rows[0]["intToMonth"].ToString() == "12")
        {
            ddlToMonth.SelectedValue = "Dec";
        }
        else
        {
            ddlToMonth.SelectedValue = "Jan";
        }
    }

    //protected void chkAtPresent_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkAtPresent.Checked == true)
    //    {
    //        txtToYear.SelectedIndex = 1;// txtToYear.Items.Count - 1;
    //        // txtToYear.Text = DateTime.Now.Year.ToString();
    //        txtToYear.Enabled = false;
    //        int i = DateTime.Now.Month;
    //        toMonth.SelectedIndex = i;
    //        toMonth.Enabled = false;
    //    }
    //    else
    //    {
    //        txtToYear.SelectedIndex = 0;
    //        txtToYear.Enabled = true;
    //        toMonth.Enabled = true;
    //        toMonth.SelectedIndex = 0;
    //    }
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "wrkExCur", "OverlayBody();", true);
    //}

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    //  string FilePath = hdnFilePath.Value;
    //    // string DocName = hdnDocName.Value;
    //    string Error = hdnErrorMsg.Value;
    //    bool fileUploaded = ddUploader1.UploadFile();
    //    // string filename = hdnUploadFile.Value;
    //    if (!fileUploaded)
    //    {

    //        lblErrorMsg.Visible = true;
    //        lblErrorMsg.Text = ddUploader1.ErrorMesssage;
    //        lblErrorMsg.CssClass = "RedErrormsg";
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "docpopup", "OverlayBody();", true);
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
    //        return;
    //    }
    //    else
    //    {
    //        lblErrorMsg.Visible = false;
    //        lblErrorMsg.Text = "";
    //    }

    //    if (txtDocTitle.Text != "Title")
    //    {
    //        objDoProDocs.DocTitle = txtDocTitle.Text.Trim().Replace("'", "''");
    //    }
    //    if (txtDescription.Value != "Description")
    //    {
    //        objDoProDocs.strDescrition = txtDescrition.Value.Trim().Replace("'", "''");
    //    }

    //    objDoProDocs.FilePath = ddUploader1.UploadedFileName;
    //    objDoProDocs.strDocName = ddUploader1.FileName;
    //    objDoProDocs.intDocsSee = imgPrivate.Checked ? "Private" : "Public";

    //    objDoProDocs.IsDocsDownload = imgDownload.Checked ? "Y" : "N";
    //    string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //    if (ip == null)
    //        objDoProDocs.IpAddress = Request.ServerVariables["REMOTE_ADDR"];

    //    objDoProDocs.intDocumentTypeID = 1;
    //    objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
    //    if (ViewState["DocIdEdit"] != null)
    //    {
    //        objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
    //        objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.Update);
    //    }
    //    else
    //    {
    //        objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.Add);
    //    }

    //    ViewState["DocId"] = objDoProDocs.DocId;
    //    List<KeyValuePair<string, string>> subJectValues = lstSubjCategory.GetSelectedValues();

    //    if (subJectValues != null && subJectValues.Count > 0)
    //    {
    //        string[] subJectCateValues = subJectValues.Select(s => (string)s.Value).ToArray();
    //        if (ViewState["DocIdEdit"] != null)
    //        {
    //            objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
    //            objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.DeleteTempTable);
    //        }

    //        //    string[] totalSubjects = hdnSubject.Value.Split(',');
    //        var dictionary = new Dictionary<int, int>();

    //        Dictionary<string, int> counts = subJectCateValues.ToArray().GroupBy(x => x)
    //                                      .ToDictionary(g => g.Key,
    //                                                    g => g.Count());

    //        foreach (var v in counts)
    //        {
    //            if (v.Value == 1)
    //            {
    //                if (ViewState["DocIdEdit"] != null)
    //                {
    //                    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
    //                }
    //                else
    //                {
    //                    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
    //                }
    //                objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
    //                objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
    //                objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
    //            }
    //            else if (v.Value == 3)
    //            {
    //                if (ViewState["DocIdEdit"] != null)
    //                {
    //                    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
    //                }
    //                else
    //                {
    //                    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
    //                }
    //                objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
    //                objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
    //                objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
    //            }
    //            else if (v.Value == 5)
    //            {

    //                if (ViewState["DocIdEdit"] != null)
    //                {
    //                    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
    //                }
    //                else
    //                {
    //                    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
    //                }
    //                objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
    //                objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
    //                objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
    //            }
    //            else if (v.Value == 7)
    //            {

    //                if (ViewState["DocIdEdit"] != null)
    //                {
    //                    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
    //                }
    //                else
    //                {
    //                    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
    //                }
    //                objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
    //                objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
    //                objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
    //            }
    //            else if (v.Value == 9)
    //            {
    //                if (ViewState["DocIdEdit"] != null)
    //                {
    //                    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocIdEdit"]);
    //                }
    //                else
    //                {
    //                    objDoProDocs.DocId = Convert.ToInt32(ViewState["DocId"]);
    //                }
    //                objDoProDocs.intSubjectCategoryId = Convert.ToInt32(v.Key);
    //                objDoProDocs.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
    //                objDAProDocs.AddEditDel_Document(objDoProDocs, DA_ProfileDocuments.Document.ADDSelectedSubCatId);
    //            }

    //        }
    //    }
    //    RedirectToDocument();
    //    //clearDoc();
    //    //BindDocuments();
    //    //Session["SubmitTime"] = DateTime.Now.ToString();
    //    //ViewState["docPathNew"] = null;
    //    //divDocumentUplaod.Style.Add("display", "none");
    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "DocLI12", "CallDocLI();", true);
    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
    //}

    //private void BindSubjectList()
    //{
    //    DataTable dtSub = new DataTable();

    //    dtSub = DAobjCategory.GetDataTable(objCategory, DA_CategoryMaster.CategoryMaster.AllRecord);
    //    if (dtSub.Rows.Count > 0)
    //    {
    //        lstSubjCategory.DataTextField = "strCategoryName";
    //        lstSubjCategory.DataValueField = "intCategoryId";
    //        lstSubjCategory.DataSource = dtSub;
    //        lstSubjCategory.RefreshList();
    //    }
    //    else
    //    {
    //        lstSubjCategory.DataSource = null;
    //        lstSubjCategory.RefreshList();
    //    }
    //}
    //protected void clearDoc()
    //{
    //    ddUploader1.Reset();
    //    BindSubjectList();
    //    txtDescrition.Value = "";
    //    txtDocTitle.Text = "";
    //    lblErrorMsg.Visible = false;
    //    lblErrorMsg.Text = "";
    //    //    lblfilenamee.Text = "";
    //    //    lnkDeleteDoc123.Style.Add("display", "none");
    //    //    ViewState["Callsmoothmenu"] = "smooth";
    //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts", "groupConnChange();", true);
    //}


    protected void lnkSkipEducation_Click(object sender, EventArgs e)
    {
        Response.Redirect("SignUp3.aspx?ActiveStatus=E");
    }

    protected void btndisclaimer_Click(object sender, EventArgs e)
    {
        divDisclaimerPopup.Style.Add("display", "block");
        //divDisclaimerPopup.Attributes.Add("class", "display-blockk");
    }
}