using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class SA_ArticleListing : System.Web.UI.Page
{
    DA_SAJournal objDASAJor = new DA_SAJournal();
    DO_SAJournal objDOSAJor = new DO_SAJournal();
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

            hdnTotalItem.Value = "10";
            LoadAllArticles();
            BindJournalInactiveList();
        }
    }

    protected void BindJournalInactiveList()
    {
        objDOSAJor.CurrentPage = 1;
        objDOSAJor.CurrentPageSize = 100;
        objDOSAJor.ActiveStatus = false;
        DataTable dt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.FilteredJournals);
        if (dt.Rows.Count > 0)
        {
            ddlJournals.DataTextField = "journaltitle";
            ddlJournals.DataValueField = "journalid";
            ddlJournals.DataSource = dt;
            ddlJournals.DataBind();
            ddlJournals.Items.Insert(0, new ListItem("Please Select Journal", "0"));
        }
        else
        {
            ddlJournals.DataSource = null;
            ddlJournals.DataBind();
        }
    }

    protected void LoadAllArticles()
    {
        objDOSAJor.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOSAJor.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        DataTable dtArt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.AllArticles);
        if (dtArt.Rows.Count > 0)
        {
            lstArticles.DataSource = dtArt;
            lstArticles.DataBind();
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dtArt.Rows[0]["Maxcount"]));
        }
    }

    protected void lstArticles_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        LinkButton lblArtTitle = e.Item.FindControl("lblArtTitle") as LinkButton;
        if (e.CommandName == "AddToJournal")
        {
            HiddenField hdnArticleID = (HiddenField)e.Item.FindControl("hdnArticleID");
            hdnArticleIdGlob.Value = hdnArticleID.Value;
            txtPublishTitle.Text = lblArtTitle.Text;
            docModal.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "atSub", "OverlayBody();", true);
        }
        else if (e.CommandName == "Download")
        {
            HiddenField hdnFilePath = (HiddenField)e.Item.FindControl("hdnFilePath");
            string strURL = "~\\Articles\\" + hdnFilePath.Value;
            string filename = lblArtTitle.Text.ToString().Replace(" ", "-").Replace("/", "-").Replace(".", "-").Replace(":", "-") + ".pdf";
            Response.Redirect("handler/DownloadFile.ashx?path=" + strURL + "&filename=" + filename);
        }
    }

    protected void lstArticles_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DataRowView drv = e.Item.DataItem as DataRowView;
        int articleId = Convert.ToInt32(drv["articleid"]);
        objDOSAJor.ArticleID = articleId;
        LinkButton lnkAddToJournal = (LinkButton)e.Item.FindControl("lnkAddToJournal");
        DataTable dt = objDASAJor.GetDataTable(objDOSAJor, DA_SAJournal.Case.JournalsOfArticles);
        //lnkAddToJournal.Visible = false;
        if (dt.Rows.Count > 0)
        {
            ListView ls = e.Item.FindControl("lstJournal") as ListView;
            ls.DataSource = dt;
            ls.DataBind();
            lnkAddToJournal.Visible = false;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        docModal.Style.Add("display", "none");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtPublishTitle.Text) && ddlJournals.SelectedIndex <= 0)
        {
            return;
        }
        objDOSAJor.ArticleID = Convert.ToInt32(hdnArticleIdGlob.Value);
        objDOSAJor.Title = txtPublishTitle.Text;
        objDOSAJor.JournalID = Convert.ToInt32(ddlJournals.SelectedValue);
        objDASAJor.AddEditJournals(objDOSAJor, DA_SAJournal.Case.PublishJournal);
        Clear();
        LoadAllArticles();
        docModal.Style.Add("display", "none");
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "shwc", "showSuccessPopup('Success','Article added successfully to the journal.');", true);
    }
    private void Clear()
    {
        txtPublishTitle.Text = "";
        ddlJournals.SelectedValue = "0";
    }

    protected void lstJournal_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnJournalID = (HiddenField)e.Item.FindControl("hdnJournalId");
        Response.Redirect("SA_JournalDetails.aspx?JrnlId=" + hdnJournalID.Value);
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
                LoadAllArticles();
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
            LoadAllArticles();
        }
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        //  divDeletesucess.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            //imgPaging.Style.Add("opacity", "1.2");
            hdnCurrentPage.Value = hdnNextPage.Value;
            LoadAllArticles();
        }
    }
}