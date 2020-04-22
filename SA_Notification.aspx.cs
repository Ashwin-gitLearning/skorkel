using System;
using System.Data;
using DA_SKORKEL;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class SA_Notification : System.Web.UI.Page
{
    DA_SANotification objDASANot = new DA_SANotification();
    DO_SANotification objDOSANot = new DO_SANotification();
    string FilterText = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        divSuccess.Style.Add("display", "none");
        divSuccessNoti.Style.Add("display", "none");
        if (!IsPostBack)
        {
            divSuccess.Style.Add("display", "none");
            divSuccessNoti.Style.Add("display", "none");
            if (Request.QueryString["DeleteNoti"] != null && Request.QueryString["DeleteNoti"] != "")
            {
                divSuccessNoti.Style.Add("display", "block");
                Master.BodyTag.Attributes.Add("class", "remove-scroller");
            }
            Session["SubmitTime"] = DateTime.Now.ToString();
            hdnTotalItem.Value = "10";
            LoadAllNotifications();
        }
     
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["SubmitTime"] = Session["SubmitTime"];
    }
    protected void LoadAllNotifications()
    {
        objDOSANot.CurrentPage = Convert.ToInt32(hdnCurrentPage.Value);
        objDOSANot.CurrentPageSize = Convert.ToInt32(hdnTotalItem.Value);
        DataTable dt = objDASANot.GetDataTable(objDOSANot, DA_SANotification.Case.AllNotifications);
        if (dt.Rows.Count > 0)
        {
            lstNotification.DataSource = dt;
            lstNotification.DataBind();
            BindRptPager(Convert.ToInt32(hdnTotalItem.Value), Convert.ToInt32(hdnCurrentPage.Value), Convert.ToInt32(dt.Rows[0]["Maxcount"]));
        }
        else
        {

            lstNotification.DataSource = null;
            lstNotification.DataBind();
        }

    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearNotification();
        docModal1.Style.Add("display", "none");
    }

    private void ReLoadNotification()
    {
     
            LoadAllNotifications();
     
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       
            if (txtNotification.InnerText != "Enter Notification here...")
            {
                if (txtNotification.InnerText.Trim() != "")
                {

                    btnSave.Enabled = false;
                    SaveNotification();
                    ClearNotification();
                    LoadAllNotifications();
                    docModal1.Style.Add("display", "none");
                   
                }
            }
       

    }

    private void SaveNotification()
    {   
        objDOSANot.NotificationDetail = txtNotification.InnerText.Trim();
        objDASANot.AddNotifications(objDOSANot, DA_SANotification.Case.AddNotification);
        btnSave.Enabled = false;
        Session["SubmitTime"] = DateTime.Now.ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallPostimg1", "CallPostimgs(),callClose(),CallOnRequestSucess();$('#docModal1').modal('hide');", true);
        divSuccess.Style.Add("display", "block");

    }
    protected void lnkAddNotification_Click(object sender, EventArgs e)
    {
        docModal1.Style.Add("display", "block");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "eu12p", "OverlayBody();", true);
    }
    protected void ClearNotification()
    {
        hdnNotificationIDGlo.Value = "";
        txtNotification.InnerText = "";
        ViewState["Submitted"] = "Submitted";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "eu12p", "RemoveOverlay();", true);
    }
    protected void LnkActiveClick(object sender, EventArgs e)
    {
        hdnCurrentPage.Value = "1";
        FilterText = "active";
        //txtFilter.InnerText = "Active Journals";
        ReLoadNotification();
    }

    protected void lstNotification_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnNotificationlId = e.Item.FindControl("hdnintNotificationID") as HiddenField;
        ViewState["hdnNotificationlId"] = hdnNotificationlId.Value;
    }

    protected void lnkDeleteConfirm_Click(object sender, EventArgs e)
    {
        
        hdnNotificationIDGlo.Value = ViewState["hdnNotificationlId"].ToString();
        objDOSANot.intNotificationId = Convert.ToInt32(hdnNotificationId.Value);
        objDASANot.DeleteNotifications(objDOSANot, DA_SANotification.Case.DeleteNotification);
        LoadAllNotifications();
        Response.Redirect("~/SA_Notification.aspx?DeleteNoti=true");
    }


    protected void lstNotification_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        HtmlGenericControl lblArtCounts = e.Item.FindControl("lblArtCounts") as HtmlGenericControl;
        DataRowView drv = e.Item.DataItem as DataRowView;

        HiddenField hdnNotificationlId = e.Item.FindControl("hdnintNotificationID") as HiddenField;
        ViewState["hdnNotificationlId"] = hdnNotificationlId.Value;


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
                ReLoadNotification();
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
            ReLoadNotification();
        }
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        //  divDeletesucess.Style.Add("display", "none");
        if (Convert.ToInt32(hdnEndPage.Value) >= Convert.ToInt32(hdnNextPage.Value))
        {
            //imgPaging.Style.Add("opacity", "1.2");
            hdnCurrentPage.Value = hdnNextPage.Value;
            ReLoadNotification();
        }
    }
}