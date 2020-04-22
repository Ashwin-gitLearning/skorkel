using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class SA_SubDocs : System.Web.UI.Page
{
    DA_SASubDoc objDASASubDoc = new DA_SASubDoc();
    DO_SASubDoc objDOSASubDoc = new DO_SASubDoc();
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
            LoadAllSubDocs();
        }
    }

    protected void LoadAllSubDocs()
    {
        objDOSASubDoc.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOSASubDoc.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        objDOSASubDoc.TxtSearch = Convert.ToString(hdnSearch.Value);
        DataTable dtSD = objDASASubDoc.GetDataTable(objDOSASubDoc, DA_SASubDoc.Case.GetArticles);
        if (dtSD.Rows.Count > 0)
        {
            lstSubDocs.DataSource = dtSD;
            lstSubDocs.DataBind();
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dtSD.Rows[0]["Maxcount"]));
        }
    }

    protected void lstSubDocs_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            HiddenField hdnFilePath = (HiddenField)e.Item.FindControl("hdnFilePath");
            LinkButton lblName = e.Item.FindControl("lblName") as LinkButton;
            string strURL = "~\\SubDoc\\" + hdnFilePath.Value;
            string filename = lblName.Text.ToString().Replace(" ", "-").Replace("/", "-").Replace(".", "-").Replace(":", "-") + ".pdf";
            Response.Redirect("handler/DownloadFile.ashx?path=" + strURL + "&filename=" + filename);
        }
    }

    protected void btnPplSearch_Click(object sender, EventArgs e)
    {
        hdnSearch.Value = txtPplSearch.Text.Trim();
        hdnCurrentPage.Value = "1";
        hdnTotalItem.Value = "10";
        LoadAllSubDocs();
    }

    protected void lstSubDocs_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ImageButton imgprofile = (ImageButton)e.Item.FindControl("imgprofile");
        HiddenField hdnimgprofile = (HiddenField)e.Item.FindControl("hdnimgprofile");
   
        if (imgprofile.ImageUrl == "" || imgprofile.ImageUrl == null || imgprofile.ImageUrl == "CroppedPhoto/")
        {
            imgprofile.ImageUrl = "images/profile-photo.png";
        }
        else
        {
            string imgPathPhysical = Server.MapPath("~/CroppedPhoto/" + hdnimgprofile.Value);
            if (File.Exists(imgPathPhysical))
            {
            }
            else
            {
                imgprofile.ImageUrl = "images/profile-photo.png";
            }
        }
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
                LoadAllSubDocs();
            }
        }
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        if (hdnPreviousPage.Value != "0")
        {
            hdnCurrentPage.Value = hdnPreviousPage.Value;
            LoadAllSubDocs();
        }
    }

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            hdnCurrentPage.Value = hdnNextPage.Value;
            LoadAllSubDocs();
        }
    }
}