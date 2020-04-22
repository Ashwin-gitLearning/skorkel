using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class SA_Jobs_Details : System.Web.UI.Page
{
    DA_Scrl_JobsListing objDAJobsListing = new DA_Scrl_JobsListing();
    DO_Scrl_JobsListing objDOJobsListing = new DO_Scrl_JobsListing();

    DO_Scrl_JobCandidate objDOJobCandidate = new DO_Scrl_JobCandidate();
    DA_Scrl_JobCandidate objDAJobCandidate = new DA_Scrl_JobCandidate();

    DataTable dt = new DataTable();
    DataTable dtCand = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
            }
            hdnTotalItemJobs.Value = "12";
            BindJobsDetail();
        }
    }

    protected void BindJobsDetail()
    {
        dt.Clear();
        dtCand.Clear();

        objDOJobCandidate.CurrentPage = Convert.ToInt32(hdnCurrentPageJobs.Value);
        objDOJobCandidate.CurrentPageSize = Convert.ToInt32(hdnTotalItemJobs.Value);

        int JobId = Convert.ToInt32(Request.QueryString["JobId"]);
        objDOJobsListing.ID = JobId;
        objDOJobCandidate.Job_ID = JobId;

        int AppliedStatus = objDAJobCandidate.GetAppliedStatus(objDOJobCandidate, DA_Scrl_JobCandidate.JobCandidate.AppliedCountStatus);
        dt = objDAJobsListing.GetDataTable(objDOJobsListing, DA_Scrl_JobsListing.JobsListing.SingleRecord);

        dtCand = objDAJobCandidate.GetDataTable(objDOJobCandidate, DA_Scrl_JobCandidate.JobCandidate.GetCandidateDetails);

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Status"].ToString() == "A")
            {
                chkToggle.Checked = true;
            }
            else
            {
                chkToggle.Checked = false;
            }
            if (Convert.ToString(dt.Rows[0]["StartingSalary"]) != "" && dt.Rows[0]["StartingSalary"] != null && Convert.ToString(dt.Rows[0]["EndingSalary"]) != "" && dt.Rows[0]["EndingSalary"] != null)
            {
                li_salary.Visible = true;
            }
            if (Convert.ToString(dt.Rows[0]["StartDuration"]) != "" && dt.Rows[0]["StartDuration"] != null && Convert.ToString(dt.Rows[0]["EndDuration"]) != "" && dt.Rows[0]["EndDuration"] != null)
            {
                li_duration.Visible = true;
            }
            if (Convert.ToString(dt.Rows[0]["Location"]) != "" && dt.Rows[0]["Location"] != null)
            {
                li_location.Visible = true;
            }
            if (Convert.ToString(dt.Rows[0]["JobType"]) != "" && dt.Rows[0]["JobType"] != null)
            {
                li_jobtype.Visible = true;
            }
            hdnPostUpdateId.Value = dt.Rows[0]["ID"].ToString();
            lblTitle.Text = dt.Rows[0]["Title"].ToString();
            dtAddedOn.Text = dt.Rows[0]["Created_timestamp"].ToString();
            lblStartingSalary.Text = dt.Rows[0]["StartingSalary"].ToString();
            lblEndingSalary.Text = dt.Rows[0]["EndingSalary"].ToString();
            lblLocation.Text = dt.Rows[0]["Location"].ToString();
            lblJobType.Text = dt.Rows[0]["JobType"].ToString();
            lblStartDuration.Text = dt.Rows[0]["StartDuration"].ToString();
            lblEndDuration.Text = dt.Rows[0]["EndDuration"].ToString();
            lblDescription.Text = dt.Rows[0]["Description"].ToString();
            lblCounter.Text = Convert.ToString(AppliedStatus);
            hdnJobsIdToggle.Value = dt.Rows[0]["ID"].ToString();

            if (dtCand.Rows.Count > 0)
            {
                card_title.Visible = true;
                lstCandidateDetails.Visible = true;
                lstCandidateDetails.DataSource = dtCand;
                lstCandidateDetails.DataBind();
                dvPageJobs.Visible = true;
                BindRptPagerJobs(Convert.ToInt32(hdnTotalItemJobs.Value), Convert.ToInt32(hdnCurrentPageJobs.Value), Convert.ToInt32(dtCand.Rows[0]["Maxcount"]));
            }
            else
            {
                //lblmsg.Visible = true;
                card_title.Visible = false;
                lstCandidateDetails.Visible = false;
                lstCandidateDetails.DataSource = null;
                lstCandidateDetails.DataBind();
                dvPageJobs.Visible = false;
            }
        }
        else
        {
            // Didn't find the news article - redirect user back to Jobs Listing Page
            Response.Redirect("SA_JobsListing.aspx");
        }
        //else
        //{
        //    lblTitle.Text = "";
        //    dtAddedOn.Text = "";
        //    lblStartingSalary.Text = "";
        //    lblEndingSalary.Text = "";
        //    lblLocation.Text = "";
        //    lblJobType.Text = "";
        //    lblStartDuration.Text = "";
        //    lblEndDuration.Text = "";
        //    lblDescription.Text = "";
        //}
    }

    protected void Check_Click(object sender, EventArgs e)
    {
        bool? checkTgl = null;
        var chkToggle = (CheckBox)sender;
        //HiddenField hdnPostId = e.Item.FindControl("hdnPostId") as HiddenField;
        var reminderHiddenField = (HiddenField)chkToggle.NamingContainer.FindControl("hdnJobsIdToggle");
        if (chkToggle.Checked == true)
        {
            checkTgl = true;
            objDOJobsListing.ID = Convert.ToInt32(reminderHiddenField.Value);
            objDAJobsListing.chk_JobListing(objDOJobsListing, DA_Scrl_JobsListing.JobsListing.UpdateJobStatus, Convert.ToBoolean(checkTgl));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "YerAlert", "showSuccessPopup('Success','Status has been updated successfully.')", true);
        }
        else
        {
            checkTgl = false;
            objDOJobsListing.ID = Convert.ToInt32(reminderHiddenField.Value);
            objDAJobsListing.chk_JobListing(objDOJobsListing, DA_Scrl_JobsListing.JobsListing.UpdateJobStatus, Convert.ToBoolean(checkTgl));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "YerAlert", "showSuccessPopup('Success','Status has been updated successfully.')", true);
        }
        BindJobsDetail();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        dt.Clear();
        objDOJobsListing.ID = Convert.ToInt32(hdnPostUpdateId.Value);
        dt = objDAJobsListing.GetDataTable(objDOJobsListing, DA_Scrl_JobsListing.JobsListing.SingleRecord);
        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "456", "ShowEditPost(" +
                "'" + Convert.ToString(hdnPostUpdateId.Value) + "', " +
                "'" + Convert.ToString(dt.Rows[0]["Title"]) + "', " +
                "'" + Convert.ToString(dt.Rows[0]["Description"]) + "', " +
                "'" + Convert.ToString(dt.Rows[0]["Location"]) + "', " +
                "'" + Convert.ToString(dt.Rows[0]["JobType"]) + "', " +
                "'" + Convert.ToString(dt.Rows[0]["StartingSalary"]) + "', " +
                "'" + Convert.ToString(dt.Rows[0]["EndingSalary"]) + "', " +
                "'" + Convert.ToString(dt.Rows[0]["StartDuration"]) + "', " +
                "'" + Convert.ToString(dt.Rows[0]["EndDuration"]) + "' " + ");", true);
        }
    }

    protected void lnkCreate_Click(object sender, EventArgs e)
    {
        if (!SaveLink())
        {

        }
        //clear();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
    }

    protected bool SaveLink()
    {
        DataTable dtJobs = new DataTable();
        dtJobs.Clear();

        objDOJobsListing.ID = Convert.ToInt32(hdnintStatusUpdateId.Value);
        objDOJobsListing.Title = Validations.validateHtmlInput(Convert.ToString(txtJobTitle.Text.Replace("'", "''").Trim()));
        objDOJobsListing.Description = Convert.ToString(txtJobDescription.Value);
        objDOJobsListing.Location = txtLocation.Text;
        objDOJobsListing.JobType = drpJobType.SelectedItem.Text;
        //objDOJobsListing.StartingSalary = string.IsNullOrWhiteSpace(SalaryFrom.Text) ? Convert.ToDecimal(0) : Convert.ToDecimal(SalaryFrom.Text);
        //objDOJobsListing.EndingSalary = string.IsNullOrWhiteSpace(SalaryTo.Text) ? Convert.ToDecimal(0) : Convert.ToDecimal(SalaryTo.Text);
        objDOJobsListing.StartingSalary = SalaryFrom.Text;
        objDOJobsListing.EndingSalary = SalaryTo.Text;
        objDOJobsListing.StartDuration = DurationFrom.Text;
        objDOJobsListing.EndDuration = DurationTo.Text;

        objDAJobsListing.Edit_JobsListing(objDOJobsListing, DA_Scrl_JobsListing.JobsListing.UpdateJobByID);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess1", "$('#jobModal').modal('hide');showSuccessPopup('Success','Record updated successfully.');", true);

        hdnintStatusUpdateId.Value = "";
        BindJobsDetail();
        
        return true;
    }

    protected void lstCandidateDetails_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnimgCand = (HiddenField)e.Item.FindControl("hdnimgCand");
        HtmlImage imgCandprofile = (HtmlImage)e.Item.FindControl("imgCandprofile");

        if (hdnimgCand.Value != "" && hdnimgCand.Value != null)
        {
            string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgCand.Value);
            if (!File.Exists(imgPathPhysical))
            {
                imgCandprofile.Src = "images/profile-photo.png";
            }
            else
            {
                imgCandprofile.Src = "CroppedPhoto/" + hdnimgCand.Value;
            }
        }
        else
        {
            imgCandprofile.Src = "images/profile-photo.png";
        }
    }

    protected void lstCandidateDetails_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        //LinkButton lnkDownload = (LinkButton)e.Item.FindControl("lnkDownload");
        if (e.CommandName == "DownloadCV")
        {
            HiddenField hdnResumePath = (HiddenField)e.Item.FindControl("hdnResumePath");
            Label CandName = (Label)e.Item.FindControl("CandName");

            string strURL = "~\\Resumes\\" + hdnResumePath.Value;
            string ext = System.IO.Path.GetExtension(hdnResumePath.Value);
            string filename = CandName.Text.ToString().Replace(" ", "-").Replace("/", "-").Replace(".", "-").Replace(":", "-") + ext;
            Response.Redirect("handler/DownloadFile.ashx?path=" + strURL + "&filename=" + filename);
        }
    }

    #region Paging For Jobs

    protected void BindRptPagerJobs(Int64 PageSize, Int64 CurrentPage, Int64 MaxCount)
    {
        if (MaxCount > 0 && CurrentPage > 0 && PageSize > 0)
        {
            Int64 DisplayPage = 10;
            Int64 totalPage = (MaxCount / PageSize) + ((MaxCount % PageSize) == 0 ? 0 : 1);
            Int64 StartPage = (((CurrentPage / DisplayPage) - ((CurrentPage % DisplayPage) == 0 ? 1 : 0)) * DisplayPage) + 1;
            Int64 EndPage = ((CurrentPage / DisplayPage) + ((CurrentPage % DisplayPage) == 0 ? 0 : 1)) * DisplayPage;

            hdnNextPageJobs.Value = (CurrentPage + 1).ToString();
            hdnPreviousPageJobs.Value = (CurrentPage - 1).ToString();
            hdnEndPageJobs.Value = totalPage.ToString();

            if (totalPage < EndPage)
            {
                if (totalPage != StartPage)
                {
                    EndPage = totalPage;
                    hdnEndPageJobs.Value = EndPage.ToString();
                }
                else
                {
                    StartPage = StartPage - DisplayPage;
                    StartPage++;
                    EndPage = totalPage;
                    hdnEndPageJobs.Value = EndPage.ToString();
                }
            }
            else
            {
                if (Convert.ToInt32(hdnNextPageJobs.Value) == totalPage)
                {
                    StartPage++;
                    EndPage = totalPage;
                    hdnEndPageJobs.Value = EndPage.ToString();
                }
            }

            if (totalPage == 1)
            {
                dvPageJobs.Visible = false;
                rptDvPageJobs.DataSource = null;
                rptDvPageJobs.DataBind();
            }
            else
            {
                DataTable dtPage = new DataTable();
                DataColumn PageNo = new DataColumn();
                PageNo.DataType = System.Type.GetType("System.String");
                PageNo.ColumnName = "intPageNo";
                dtPage.Columns.Add(PageNo);

                for (Int64 i = StartPage; i <= EndPage; i++)
                {
                    dtPage.Rows.Add(i.ToString());
                    hdnLastPageJobs.Value = i.ToString();
                }

                rptDvPageJobs.DataSource = dtPage;
                rptDvPageJobs.DataBind();

                if (CurrentPage > 1)
                {
                    lnkPreviousJobs.Visible = true;
                    //hdnPreviousPage.Value = (StartPage - 1).ToString();
                    hdnPreviousPageJobs.Value = (CurrentPage - 1).ToString();
                }
                else
                {
                    lnkPreviousJobs.Visible = false;
                }
                if (totalPage >= EndPage)
                {
                    lnkNextJobs.Visible = true;
                    //hdnNextPage.Value = (EndPage + 1).ToString();
                    hdnLastPageJobs.Value = totalPage.ToString();
                }
                else
                {
                    lnkNextJobs.Visible = true;
                }
            }
        }

    }
    protected void lnkNextJobs_Click(object sender, EventArgs e)
    {
        //divDeletesuccessJobs.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPageJobs.Value) >= Convert.ToInt32(hdnNextPageJobs.Value))
        {
            hdnCurrentPageJobs.Value = hdnNextPageJobs.Value;
            //if (Convert.ToString(ViewState["ViewAll"]) == "1")
            //{
            //    BindAllJobsDetails();
            //}
            //else
            //{
            BindJobsDetail();
            //}
        }
    }
    protected void lnkPreviousJobs_Click(object sender, EventArgs e)
    {
        //divDeletesuccessJobs.Style.Add("display", "none");
        if (hdnPreviousPageJobs.Value != "0")
        {
            hdnCurrentPageJobs.Value = hdnPreviousPageJobs.Value;
            //if (Convert.ToString(ViewState["ViewAll"]) == "1")
            //{
            //    BindAllJobsDetails();
            //}
            //else
            //{
            BindJobsDetail();
            //}
        }
    }
    protected void rptDvPageJobs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkPageLinkJobs = (LinkButton)e.Item.FindControl("lnkPageLinkJobs");
            if (lnkPageLinkJobs != null)
            {
                if (hdnCurrentPageJobs.Value == lnkPageLinkJobs.Text)
                {
                    lnkPageLinkJobs.Enabled = false;
                    if (ViewState["lnkPageLinkJobs"] != null)
                    {
                        if (lnkPageLinkJobs.Text == "1")
                        {
                            ViewState["lnkPageLinkJobs"] = null;
                        }
                    }
                }
                else
                {
                    lnkPageLinkJobs.Enabled = true;
                }

                if (hdnCurrentPageJobs.Value == "1")
                {
                    ViewState["lnkPageLinkJobs"] = "PageCount";
                }

            }
        }
    }
    protected void rptDvPageJobs_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //divDeletesuccessJobs.Style.Add("display", "none");
        if (e.CommandName == "PageLink")
        {
            LinkButton lnkPageLinkJobs = (LinkButton)e.Item.FindControl("lnkPageLinkJobs");
            if (lnkPageLinkJobs != null)
            {
                hdnCurrentPageJobs.Value = lnkPageLinkJobs.Text;
                lnkPageLinkJobs.Style.Add("color", "#141414 !important");
                lnkPageLinkJobs.Style.Add("text-decoration", "none !important");

                if (lnkPageLinkJobs.Text == "")
                {
                    hdnCurrentPageJobs.Value = "1";
                }
                if (lnkPageLinkJobs.Text != "1")
                {
                    //imgPaging.Style.Add("opacity", "1.2");
                }
                else
                {
                    //imgPaging.Style.Add("opacity", "0.2");
                }
                //if (Convert.ToString(ViewState["ViewAll"]) == "1")
                //{
                //    BindAllJobsDetails();
                //}
                //else
                //{
                BindJobsDetail();
                //}
            }
        }
    }

    #endregion
}