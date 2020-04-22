using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Jobs_Details : System.Web.UI.Page
{
    DA_Scrl_JobsListing objDAJobsListing = new DA_Scrl_JobsListing();
    DO_Scrl_JobsListing objDOJobsListing = new DO_Scrl_JobsListing();

    DO_Scrl_JobCandidate objDOJobCandidate = new DO_Scrl_JobCandidate();
    DA_Scrl_JobCandidate objDAJobCandidate = new DA_Scrl_JobCandidate();

    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["SubmitTime"] = DateTime.Now.ToString();
        if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
        {
            ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
        }
        ddUploader1.URL = "handler/UploadResume.ashx";
        ddUploader1.MaxFileSize = "5242880";
        ddUploader1.DocType = "resume";
        ddUploader1.ShowDelete = true;
        BindJobsDetail();
    }

    protected void BindJobsDetail()
    {
        int JobId = Convert.ToInt32(Request.QueryString["JobId"]);
        dt.Clear();
        objDOJobsListing.ID = JobId;
        dt = objDAJobsListing.GetDataTable(objDOJobsListing, DA_Scrl_JobsListing.JobsListing.SingleRecord);
        objDOJobCandidate.Job_ID = JobId;
        objDOJobCandidate.User_ID = Convert.ToInt32(ViewState["UserID"]);
        int AppliedStatus = objDAJobCandidate.GetAppliedStatus(objDOJobCandidate, DA_Scrl_JobCandidate.JobCandidate.AppliedStatus);
        if (dt.Rows.Count > 0)
        {
            if (AppliedStatus == 0)
            {
                lnkApply.Visible = true;
                divuploadIOS.Visible = true;
                span_status.Visible = false;
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
        }
        else
        {
            // Didn't find the news article - redirect user back to Jobs Listing Page
            Response.Redirect("AllJobsListing.aspx");
        }
    }

    protected void lnkSubmit_Click(object sender, EventArgs e)
    {
        bool fileUploaded = ddUploader1.UploadFile();

        if (!fileUploaded)
        {
            lblErrorMsg.Visible = true;
            lblErrorMsg.Text = ddUploader1.ErrorMesssage;
            lblErrorMsg.Text = "Please select the file to upload";
            lblErrorMsg.CssClass = "RedErrormsg";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "docpop", "OverlayBody();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess2", "$('#jobModal').modal('show');", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "chosen", "createChosen();", true);
            return;
        }
        else
        {
            lblErrorMsg.Visible = false;
            lblErrorMsg.Text = "";
        }
        ddUploader1.SetValues(ddUploader1.FileName, ddUploader1.UploadedFileName);

        objDOJobCandidate.Resume_path = ddUploader1.UploadedFileName;
        objDOJobCandidate.Resume_file_title = ddUploader1.FileName;
        objDOJobCandidate.User_ID = Convert.ToInt32(ViewState["UserID"]);
        objDOJobCandidate.Job_ID = Convert.ToInt32(hdnPostUpdateId.Value);
        int JobsValue = objDAJobCandidate.AddEditJobCandidate(objDOJobCandidate, DA_Scrl_JobCandidate.JobCandidate.AddJobCandidate);
        ddUploader1.Reset();
        //jobModal.Style.Add("display", "none");
       
        if (JobsValue == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "YerAlert", "showSuccessPopup('Success','Job candidate record already exists.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess1", "showSuccessPopup('Success','Job applied successfully.');", true);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
        RedirectToDocument();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAddUserSuccess3", "$('#jobModal').modal('hide');", true);
        int JobId = Convert.ToInt32(Request.QueryString["JobId"]);
        Response.Redirect("Jobs-Details.aspx?JobId=" + JobId);
    }

    protected void RedirectToDocument()
    {
        int JobId = Convert.ToInt32(Request.QueryString["JobId"]);
        Response.Redirect("Jobs-Details.aspx?JobId=" + JobId);
        //Response.Redirect("AllJobsListing.aspx");
    }

    protected void upldrFile_delete(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "docpopup", "OverlayBody();", true);
    }
}