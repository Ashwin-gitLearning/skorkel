using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
public partial class UploadDocuments : System.Web.UI.Page
{
    DO_ProfileDocuments DocDO = new DO_ProfileDocuments();
    DA_ProfileDocuments DocDA = new DA_ProfileDocuments();

    DataTable dt = new DataTable();
    DA_ProfileDocuments objDAProDocs = new DA_ProfileDocuments();
    DO_ProfileDocuments objDoProDocs = new DO_ProfileDocuments();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
                //BindExperience();
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            if (Request.QueryString["GrpId"] != "" && Request.QueryString["GrpId"] != null)
            {
                ViewState["intGroupId"] = Request.QueryString["GrpId"];
            }

            HtmlGenericControl masterlbl = (HtmlGenericControl)Master.FindControl("lblmaster");
            masterlbl.InnerText = "Home";

            if (Request.QueryString["RegId"] != "" && Request.QueryString["RegId"] != null)
            {
                ViewState["UserID"] = Request.QueryString["RegId"];
                ViewState["RegId"] = Request.QueryString["RegId"];
                BindDocuments();
            }
            else
            {
                BindDocuments();
                lnkUpdateDetaila.Style.Add("display", "block");
            }
        }
    }

    private void BindDocuments()
    {
        DocDO.intGroupId =Convert.ToInt32(ViewState["intGroupId"]);
        DocDO.AddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = DocDA.GetGroupUploadTable(DocDO, DA_ProfileDocuments.UploadDocument.Getdocuments);

        if (dt.Rows.Count > 0)
        {
            LstDocument.DataSource = dt;
            LstDocument.DataBind();
        }
    }

    protected void LstDocument_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HiddenField hdnDocId = (HiddenField)e.Item.FindControl("hdnDocId");
        HyperLink hrp_DocPath = (HyperLink)e.Item.FindControl("hrp_DocPath");
        String FileExt = Path.GetExtension(@"UploadDocument/" + hrp_DocPath.Text.ToString());
        Panel pnlSale = (Panel)e.Item.FindControl("pnlSale");

        DocDO.AddedBy = Convert.ToInt32(Session["ExternalUserId"]);
        DocDO.intGroupDocId = Convert.ToInt32(hdnDocId.Value);
        dt = DocDA.GetGroupUploadTable(DocDO, DA_ProfileDocuments.UploadDocument.GetIsSaleDetails);
        if (dt.Rows.Count > 0)
        {
            pnlSale.Visible = true;
        }
        else
        {
            pnlSale.Visible = false;
        }
        //if (FileExt.ToLower() == ".txt" || FileExt.ToLower() == ".doc")
        //{

        //}
        //else
        //{

        //}
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        objDAProDocs.AddEditDel_GroupUploadDocument(objDoProDocs, DA_ProfileDocuments.UploadDocument.DeleteTempTable);
        Response.Redirect("Create-UploadDocuments.aspx?GrpId=" + ViewState["intGroupId"]+"");

    }

    protected void lnkMyprofile_Click(object sender, EventArgs e)
    {
        Response.Redirect("MyprofileWall.aspx?RegId=" + ViewState["RegId"]);
    }

    protected void lnkSkillSet_Click(object sender, EventArgs e)
    {
        Response.Redirect("skillset.aspx?RegId=" + ViewState["RegId"]);
    }

    protected void lnkEducation_Click(object sender, EventArgs e)
    {
        Response.Redirect("education.aspx?RegId=" + ViewState["RegId"]);
    }

    protected void lnkAchievements_Click(object sender, EventArgs e)
    {
        Response.Redirect("achievements.aspx?RegId=" + ViewState["RegId"]);
    }

    #region Tabs

    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Group-Home.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkForumTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("forum-landing-page.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkUploadTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("uploads-docs-details.aspx?GrpId=" + ViewState["intGroupId"]);
    }
    protected void lnkPollTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("Polls-Details.aspx?GrpId=" + ViewState["intGroupId"]);
    }
    protected void lnkEventMemberTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("groups-members.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkJobTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("jobs.aspx?GrpId=" + ViewState["intGroupId"]);
    }

    protected void lnkEventTab_Click(object sender, EventArgs e)
    {
        Response.Redirect("group-event-main.aspx?GrpId=" + ViewState["intGroupId"]);
    }


    #endregion
}