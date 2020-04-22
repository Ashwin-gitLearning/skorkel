using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class SA_JobsListing : System.Web.UI.Page
{
    DA_Scrl_JobsListing objDAJobsListing = new DA_Scrl_JobsListing();
    DO_Scrl_JobsListing objDOJobsListing = new DO_Scrl_JobsListing();

    DO_Scrl_JobCandidate objDOJobCandidate = new DO_Scrl_JobCandidate();
    DA_Scrl_JobCandidate objDAJobCandidate = new DA_Scrl_JobCandidate();

    DataTable dt = new DataTable();
    string FilterText = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
            }
            else
            {
                Response.Redirect("~/SuperAdminLanding.aspx", true);
            }

            //hdnCurrentPageJobs.Value = "1";
            //hdnTotalItemJobs.Value = "10";
            hdnTotalItemJobs.Value = "15";
            txtFilter.InnerText = " All Jobs";

            BindAllJobsDetails();
        }
    }

    protected void BindAllJobsDetails()
    {
        objDOJobsListing.CurrentPage = Convert.ToInt32(hdnCurrentPageJobs.Value);
        objDOJobsListing.CurrentPageSize = Convert.ToInt32(hdnTotalItemJobs.Value);

        dt = objDAJobsListing.GetDataTable(objDOJobsListing, DA_Scrl_JobsListing.JobsListing.GetJobsListing);

        if (dt.Rows.Count > 0)
        {
            lstJobsListing.Visible = true;
            lstJobsListing.DataSource = dt;
            lstJobsListing.DataBind();
            dvPageJobs.Visible = true;
            BindRptPagerJobs(Convert.ToInt32(hdnTotalItemJobs.Value), Convert.ToInt32(hdnCurrentPageJobs.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
        }
        else
        {
            //lblmsg.Visible = true;
            lstJobsListing.Visible = false;
            lstJobsListing.DataSource = null;
            lstJobsListing.DataBind();            
            dvPageJobs.Visible = false;
        }
    }

    protected void BindFilteredJobsDetails()
    {
        objDOJobsListing.CurrentPage = Convert.ToInt32(hdnCurrentPageJobs.Value);
        objDOJobsListing.CurrentPageSize = Convert.ToInt32(hdnTotalItemJobs.Value);
        //objDOJobsListing.Status = Convert.ToString(hdnIsFiltered.Value == "active");
        objDOJobsListing.Status = Convert.ToString(hdnIsFiltered.Value);

        dt = objDAJobsListing.GetDataTable(objDOJobsListing, DA_Scrl_JobsListing.JobsListing.FilteredJobs);

        if (dt.Rows.Count > 0)
        {
            lstJobsListing.Visible = true;
            lstJobsListing.DataSource = dt;
            lstJobsListing.DataBind();
            dvPageJobs.Visible = true;
            BindRptPagerJobs(Convert.ToInt32(hdnTotalItemJobs.Value), Convert.ToInt32(hdnCurrentPageJobs.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
        }
        else
        {
            lstJobsListing.Visible = false;
            lstJobsListing.DataSource = null;
            lstJobsListing.DataBind();
            dvPageJobs.Visible = false;
        }

    }

    protected void LnkActiveClick(object sender, EventArgs e)
    {
        hdnCurrentPageJobs.Value = "1";
        //hdnIsFiltered.Value = "active";
        hdnIsFiltered.Value = "A";
        FilterText = "active";
        txtFilter.InnerText = "Active Jobs";
        ReLoadJobs();
    }
    protected void LnkInActiveClick(object sender, EventArgs e)
    {
        hdnCurrentPageJobs.Value = "1";
        //hdnIsFiltered.Value = "inactive";
        hdnIsFiltered.Value = "D";
        FilterText = "inactive";
        txtFilter.InnerText = "Inactive Jobs";
        ReLoadJobs();
    }

    private void ReLoadJobs()
    {
        if (hdnIsFiltered.Value != "false")
        {
            BindFilteredJobsDetails();
        }
        else
        {
            BindAllJobsDetails();
        }
    }

    protected void lnkCreate_Click(object sender, EventArgs e)
    {
        if (!SaveLink())
        {

        }
        clear();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
    }

    protected void clear()
    {
        txtJobTitle.Text = "";
        txtJobDescription.Value = "";
        txtLocation.Text = "";
        drpJobType.SelectedValue = "Internship";
        SalaryFrom.Text = "";
        SalaryTo.Text = "";
        DurationFrom.Text = "";
        DurationTo.Text = "";
    }

    protected bool SaveLink()
    {
        DataTable dtJobs = new DataTable();
        dtJobs.Clear();

        if (string.IsNullOrEmpty(hdnintStatusUpdateId.Value))
        {
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

            int JobsValue = objDAJobsListing.Add_JobsListing(objDOJobsListing, DA_Scrl_JobsListing.JobsListing.AddJobs);

            if (JobsValue == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "YerAlert", "showSuccessPopup('Success','Job record already exists.')", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess2", "$('#rssModal').modal('show'); $('#txtSourceLink').val('" + txtSourceLink.Text + "'); $('#txtSourceField').val('" + txtSourceField.Text + "'); ", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess1", "$('#jobModal').modal('hide');showSuccessPopup('Success','Record saved successfully.');", true);
            }
            BindAllJobsDetails();
        }
        else
        {
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
            BindAllJobsDetails();
        }
        return true;
    }
    //protected void lnkDeleteJobsConfirm_Click(object sender, EventArgs e)
    //{
    //    objDOJobsListing.ID = Convert.ToInt32(hdnintJobsDelete.Value);
    //    objDAJobsListing.DelByID_JobsListing(objDOJobsListing, DA_Scrl_JobsListing.JobsListing.DeleteJobsByID);
    //    divDeletesuccessJobs.Style.Add("display", "none");
    //    BindAllJobsDetails();
    //}

    protected void lstJobs_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnPostUpdateId = e.Item.FindControl("hdnPostUpdateId") as HiddenField;
        HtmlGenericControl ToggleBtn = (HtmlGenericControl)e.Item.FindControl("ToggleBtn");
        CheckBox chkToggle = e.Item.FindControl("chkToggle") as CheckBox;
        //LinkButton lblHeading = (LinkButton)e.Item.FindControl("lblHeading");

        chkToggle.CheckedChanged += new EventHandler(Check_Click);

        if (e.CommandName == "Edit Jobs")
        {
            lnkCreate.Text = "Update";
            ViewState["hdnPostUpdateId"] = "";
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

        if (e.CommandName == "JobsDetail")
        {
            Response.Redirect("SA_Jobs-Details.aspx?JobId=" + hdnPostUpdateId.Value);
        }
    }

    protected void lstJobs_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataTable dtlst = new DataTable();

        HtmlGenericControl ToggleBtn = (HtmlGenericControl)e.Item.FindControl("ToggleBtn");
        CheckBox chkToggle = e.Item.FindControl("chkToggle") as CheckBox;
        HiddenField hdnPostUpdateId = (HiddenField)e.Item.FindControl("hdnPostUpdateId");
        HtmlGenericControl li_salary = (HtmlGenericControl)e.Item.FindControl("li_salary");
        HtmlGenericControl li_location = (HtmlGenericControl)e.Item.FindControl("li_location");
        HtmlGenericControl li_jobtype = (HtmlGenericControl)e.Item.FindControl("li_jobtype");
        HtmlGenericControl li_duration = (HtmlGenericControl)e.Item.FindControl("li_duration");
        //HtmlGenericControl span_status = (HtmlGenericControl)e.Item.FindControl("span_status");
        Label lblCounter = (Label)e.Item.FindControl("lblCounter");

        int JobsId = Convert.ToInt32(hdnPostUpdateId.Value);
        dtlst.Clear();
        objDOJobsListing.ID = JobsId;
        objDOJobCandidate.Job_ID = JobsId;
        int AppliedStatus = objDAJobCandidate.GetAppliedStatus(objDOJobCandidate, DA_Scrl_JobCandidate.JobCandidate.AppliedCountStatus);
        dtlst = objDAJobsListing.GetDataTable(objDOJobsListing, DA_Scrl_JobsListing.JobsListing.SingleRecord);
        if (dtlst.Rows.Count > 0)
        {
            lblCounter.Text = Convert.ToString(AppliedStatus);
            
            if (dtlst.Rows[0]["Status"].ToString() == "A")
            {
                chkToggle.Checked = true;
            }
            else
            {
                chkToggle.Checked = false;
            }
            if(Convert.ToString(dtlst.Rows[0]["StartingSalary"]) != "" && dtlst.Rows[0]["StartingSalary"] != null && Convert.ToString(dtlst.Rows[0]["EndingSalary"]) != "" && dtlst.Rows[0]["EndingSalary"] != null)
            {
                li_salary.Visible = true;
            }
            if (Convert.ToString(dtlst.Rows[0]["StartDuration"]) != "" && dtlst.Rows[0]["StartDuration"] != null && Convert.ToString(dtlst.Rows[0]["EndDuration"]) != "" && dtlst.Rows[0]["EndDuration"] != null)
            {
                li_duration.Visible = true;
            }
            if (Convert.ToString(dtlst.Rows[0]["Location"]) != "" && dtlst.Rows[0]["Location"] != null)
            {
                li_location.Visible = true;
            }
            if (Convert.ToString(dtlst.Rows[0]["JobType"]) != "" && dtlst.Rows[0]["JobType"] != null)
            {
                li_jobtype.Visible = true;
            }
        }
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
        ReLoadJobs();
    }

    #region Paging For Jobs

    protected void BindRptPagerJobs(Int64 PageSize, Int64 CurrentPage, Int64 MaxCount)
    {
        if (MaxCount > 0 && CurrentPage > 0 && PageSize > 0)
        {
            //Int64 DisplayPage = 10;
            Int64 DisplayPage = 15;
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
                ReLoadJobs();
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
            ReLoadJobs();
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
                ReLoadJobs();
                //}
            }
        }
    }

    #endregion
}