using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using DA_SKORKEL;
using Ionic.Zip;

public partial class JournalListing : System.Web.UI.Page
{
    DA_SAJournal objDASAJor = new DA_SAJournal();
    DO_SAJournal objDOSAJor = new DO_SAJournal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdnTotalItem.Value = "10";
            LoadActiveJournals();
            ddUploader1.URL = "handler/UploadArticle.ashx";
            ddUploader1.MaxFileSize = "5242880";
            ddUploader1.DocType = "article";
            ddUploader1.ShowDelete = true;
        }
    }

    protected void LoadActiveJournals()
    {
        objDOSAJor.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOSAJor.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        objDOSAJor.ActiveStatus = true;
        DataTable dt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.FilteredJournals);
        if (dt.Rows.Count > 0)
        {
            lstJournals.DataSource = dt;
            lstJournals.DataBind();
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
        }
        else
        {
            lstJournals.DataSource = null;
            lstJournals.DataBind();
        }
       
    }
    protected void lstJournals_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnJournalId = e.Item.FindControl("hdnJournalId") as HiddenField;
        LinkButton lnkJournal = e.Item.FindControl("lnkJournal") as LinkButton;
        if (e.CommandName == "Details")
        {
            Response.Redirect("JournalDetails.aspx?JrnlId=" + hdnJournalId.Value);
        }
        else if (e.CommandName == "Download")
        {
            string filename = System.IO.Path.GetFileName("") + ".zip";
            int journalID = Convert.ToInt32(hdnJournalId.Value);
            objDOSAJor.JournalID = journalID;
            objDOSAJor.CurrentPage = 1;
            objDOSAJor.CurrentPageSize = 100;
            DataTable dtArt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.GetArticles);
            string directory = Context.Server.MapPath("~\\Downloads\\");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string f = lnkJournal.Text.Replace(" ", "-").Replace("/", "-").Replace(".", "-").Replace(":", "-") + ".zip";
            using (ZipFile zip = new ZipFile())
            {
              string filePath = Context.Server.MapPath("~\\Articles\\");
                foreach (DataRow dr in dtArt.Rows)
                {

                    zip.AddFile(filePath + dr["FilePath"].ToString(), "").FileName = dr["ArtTitle"].ToString().Replace(" ", "-").Replace("/", "-").Replace(".", "-").Replace(":", "-") + ".pdf";
                }  
                string zipFile = directory + f;
                zip.Save(zipFile);
            }
            string strURL = "~\\Downloads\\" + f;
            Response.Redirect("handler/DownloadFile.ashx?path=" + strURL + "&filename=" + f);
        }
    }
   


    protected void lnkAddArticle_Click(object sender, EventArgs e)
    {
        docModal1.Style.Add("display", "block");
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Clear();
        docModal1.Style.Add("display", "none");
    }

    protected void lnkSave_Click(object sender, EventArgs e)
    {
        if (!ddUploader1.UploadFile())
        {
            lblErrorUpload.Text = "Please upload article document.";
            lblErrorUpload.Visible = true;
            return;
        }
        ddUploader1.SetValues(ddUploader1.FileName, ddUploader1.UploadedFileName);
        if (!chkConfirm.Checked)
        {
            lblErrorConf.Text = "Please confirm.";
            lblErrorConf.Visible = true;
            return;
        }
        objDOSAJor.IntUserId = Convert.ToInt32(Session["ExternalUserId"]);
        objDOSAJor.FilePath = ddUploader1.UploadedFileName;
        objDOSAJor.Title = txtTitle.Text.Trim();
        objDOSAJor.ArticleWordCount = APIUtil.getArticleWordCount(objDOSAJor.FilePath);
        objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.AddArticle);
        Clear();
        docModal1.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScripts1", "hideLoader1();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "sshol", "showSuccessPopup('Success','Article added successfully.');", true);
    }
    private void Clear()
    {
        txtTitle.Text = "";
        lblErrorUpload.Text = "";
        lblErrorUpload.Visible = false;
        lblErrorConf.Text = "";
        lblErrorConf.Visible = false;
        ddUploader1.Reset();
        chkConfirm.Checked = true;

    }
    protected void BindRptPager(Int64 PageSize, Int64 CurrentPage, Int64 MaxCount)
    {
        if (MaxCount > 0 && CurrentPage > 0 && PageSize > 0)
        {
            Int64 DisplayPage = 10;
            Int64 totalPage = (MaxCount / PageSize) + ((MaxCount % PageSize) == 0 ? 0 : 1);
            Int64 StartPage = (((CurrentPage / DisplayPage) - ((CurrentPage % DisplayPage) == 0 ? 1 : 0)) * DisplayPage) + 1;
            Int64 EndPage = ((CurrentPage / DisplayPage) + ((CurrentPage % DisplayPage) == 0 ? 0 : 1)) * DisplayPage;

            hdnNextPage.Value = (CurrentPage + 1).ToString();
            hdnPreviousPage.Value = (CurrentPage - 1).ToString();
            hdnEndPage.Value = totalPage.ToString();
            if (totalPage < EndPage)
            {
                if (totalPage != StartPage)
                {
                    EndPage = totalPage;
                    hdnEndPage.Value = EndPage.ToString();
                }
                else
                {
                    StartPage = StartPage - DisplayPage;
                    StartPage++;
                    EndPage = totalPage;
                    hdnEndPage.Value = EndPage.ToString();
                }
            }
            else
            {
                if (Convert.ToInt32(hdnNextPage.Value) == totalPage)
                {
                    StartPage++;
                    EndPage = totalPage;
                    hdnEndPage.Value = EndPage.ToString();
                }
            }

            if (totalPage == 1)
            {
                dvPage.Visible = false;
                rptDvPage.DataSource = null;
                rptDvPage.DataBind();
            }
            else
            {
                dvPage.Visible = true;
                DataTable dtPage = new DataTable();
                DataColumn PageNo = new DataColumn();
                PageNo.DataType = System.Type.GetType("System.String");
                PageNo.ColumnName = "intPageNo";
                dtPage.Columns.Add(PageNo);

                for (Int64 i = StartPage; i <= EndPage; i++)
                {
                    dtPage.Rows.Add(i.ToString());
                    hdnLastPage.Value = i.ToString();
                }
                rptDvPage.DataSource = dtPage;
                rptDvPage.DataBind();
                if (CurrentPage > 1)
                {
                    lnkPrevious.Visible = true;
                    hdnPreviousPage.Value = (CurrentPage - 1).ToString();
                }
                else
                {
                    lnkPrevious.Visible = false;
                }
                if (totalPage >= EndPage)
                {
                    lnkNext.Visible = true;
                    hdnLastPage.Value = totalPage.ToString();
                }
                else
                {
                    lnkNext.Visible = true;
                }
            }
        }
    }
    protected void rptDvPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            if (lnkPageLink != null)
            {
                if (hdnCurrentPage.Value == lnkPageLink.Text)
                {
                    lnkPageLink.Enabled = false;
                    if (ViewState["lnkPageLink"] != null)
                    {
                        if (lnkPageLink.Text == "1")
                        {
                            ViewState["lnkPageLink"] = null;
                        }
                    }
                }
                else
                {
                    lnkPageLink.Enabled = true;
                }

                if (hdnCurrentPage.Value == "1")
                {
                    ViewState["lnkPageLink"] = "PageCount";
                }

            }
        }

    }
    protected void rptDvPage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //divDeletesucess.Style.Add("display", "none");
        if (e.CommandName == "PageLink")
        {
            LinkButton lnkPageLink = (LinkButton)e.Item.FindControl("lnkPageLink");
            if (lnkPageLink != null)
            {
                hdnCurrentPage.Value = lnkPageLink.Text;
                lnkPageLink.Style.Add("color", "#141414 !important");
                lnkPageLink.Style.Add("text-decoration", "none !important");

                if (lnkPageLink.Text == "")
                {
                    hdnCurrentPage.Value = "1";
                }
                if (lnkPageLink.Text != "1")
                {
                    //imgPaging.Style.Add("opacity", "1.2");
                }
                else
                {
                    //imgPaging.Style.Add("opacity", "0.2");
                }
                LoadActiveJournals();
                //}

            }
        }
    }
    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        //divDeletesucess.Style.Add("display", "none");

        if (hdnPreviousPage.Value != "0")
        {
            hdnCurrentPage.Value = hdnPreviousPage.Value;
            LoadActiveJournals();
        }
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
      //  divDeletesucess.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            //imgPaging.Style.Add("opacity", "1.2");
            hdnCurrentPage.Value = hdnNextPage.Value;
            LoadActiveJournals();
        }
    }

}