using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class News_Details : System.Web.UI.Page
{
    DA_Scrl_UserNewsListing objDANewsListing = new DA_Scrl_UserNewsListing();
    DO_Scrl_UserNewsListing objDONewsListing = new DO_Scrl_UserNewsListing();

    DataTable dt = new DataTable();
    DataTable dtRSSList = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["SubmitTime"] = DateTime.Now.ToString();
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"]);
            }
            BindNewsDetails();
        }
    }

    protected void BindNewsDetails()
    {
        if (Convert.ToString(Request.QueryString["NewsId"]) != null && Convert.ToString(Request.QueryString["NewsId"]) != "" && (!Page.IsPostBack))
        {
            int SrchPostNewsId = Convert.ToInt32(Request.QueryString["NewsId"]);
            objDONewsListing.ID = SrchPostNewsId;
        }
        else
        {
            int PostId = Convert.ToInt32(Request.QueryString["PostId"]);
            objDONewsListing.ID = PostId;
        }
        
        dt.Clear();        
        dt = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.GetNewsDtls);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Type"].ToString() == "RSS")
            {
                int SourceID = Convert.ToInt32(dt.Rows[0]["SourceID"]);
                dtRSSList.Clear();
                objDONewsListing.ID = SourceID;
                dtRSSList = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.SingleRecord);
                if (dtRSSList.Rows.Count > 0)
                {
                    if (dtRSSList.Rows[0]["Title"].ToString() != "" && dtRSSList.Rows[0]["Title"].ToString() != null)
                    {
                        lblSource.Text = dtRSSList.Rows[0]["Title"].ToString();
                        if ( dt.Rows[0]["Source_Link"].ToString() == null ||  dt.Rows[0]["Source_Link"].ToString() == "") {
                            lblSource.CssClass = "hover-same-color";
                        } else {
                            lblSource.Attributes.Add("href", dt.Rows[0]["Source_Link"].ToString());
                            lblSource.Attributes.Add("target", "_blank");    
                        }
                        
                    }
                    else
                    {
                        lblSource.Text = "";
                        lblSource.CssClass = "hover-same-color";
                    }
                }
                else
                {
                    lblSource.Text = "";
                    lblSource.CssClass = "hover-same-color";
                }
                //lblSource.Text = dtRSSList.Rows[0]["Title"].ToString();
                Session["Title"] = dt.Rows[0]["Title"].ToString();
                lblNewsHeading.Text = dt.Rows[0]["Title"].ToString();
                lblNewsDetails.Text = dt.Rows[0]["Content"].ToString();
                //lblSource.Text = dt.Rows[0]["Type"].ToString();
                dtAddedOn.Text = dt.Rows[0]["Created_timestamp"].ToString();                
            }
            else if (dt.Rows[0]["Type"].ToString() != "RSS")
            {
                //lblSource.Text = dt.Rows[0]["Type"].ToString();
                Session["Title"] = dt.Rows[0]["Title"].ToString();
                lblNewsHeading.Text = dt.Rows[0]["Title"].ToString();
                lblNewsDetails.Text = dt.Rows[0]["Content"].ToString();
                //lblSource.Text = dt.Rows[0]["Type"].ToString();
                dtAddedOn.Text = dt.Rows[0]["Created_timestamp"].ToString();
                if (dt.Rows[0]["Source_Link"].ToString() != "" && dt.Rows[0]["Source_Link"].ToString() != null)
                {
                    lblSource.Text = dt.Rows[0]["Type"].ToString();
                    lblSource.Attributes.Add("href", dt.Rows[0]["Source_Link"].ToString());
                    lblSource.Attributes.Add("target", "_blank");
                }
                else
                {
                    lblSource.Text = dt.Rows[0]["Type"].ToString();
                    lblSource.CssClass = "remove-hover-anchor";
                }
            }
            else
            {
                lblSource.Text = "";
                Session["Title"] = dt.Rows[0]["Title"].ToString();
                lblNewsHeading.Text = dt.Rows[0]["Title"].ToString();
                lblNewsDetails.Text = dt.Rows[0]["Content"].ToString();
                //lblSource.Text = dt.Rows[0]["Type"].ToString();
                dtAddedOn.Text = dt.Rows[0]["Created_timestamp"].ToString();
            }
            //Session["Title"] = dt.Rows[0]["Title"].ToString();
            //lblNewsHeading.Text = dt.Rows[0]["Title"].ToString();
            //lblNewsDetails.Text = dt.Rows[0]["Content"].ToString();
            //lblSource.Text = dt.Rows[0]["Type"].ToString();
            //dtAddedOn.Text = dt.Rows[0]["Created_timestamp"].ToString();
            //lstParentQADetails.DataSource = dt;
            //lstParentQADetails.DataBind();
        }
        else
        {
            // Didn't find the news article - redirect user back to News Listing Page
            Response.Redirect("AllNewsListing.aspx");
        }
    }

    protected void lnkBack_click(object sender, EventArgs e)
    {
        Response.Redirect("AllNewsListing.aspx");        
    }

    //protected void lblSource_click(object sender, EventArgs e)
    //{
    //    //Response.Redirect("AllNewsListing.aspx");
    //    int PostId = Convert.ToInt32(Request.QueryString["PostId"]);
    //    dt.Clear();
    //    objDONewsListing.ID = PostId;
    //    dt = objDANewsListing.GetDataTable(objDONewsListing, DA_Scrl_UserNewsListing.NewsListing.GetNewsDtls);
    //    if (dt.Rows.Count > 0)
    //    {
    //        if (dt.Rows[0]["Type"].ToString() == "RSS")
    //        {
    //            string link = dt.Rows[0]["Source_Link"].ToString();
    //            //lblSource.PostBackUrl = link;

    //            //Response.Write("<script language='javascript'>window.open('" + link + "','_blank','');");
    //            //Response.Write("</scr" + "ipt>");
    //            Response.Redirect(link);

    //            //lblSource.Attributes.Add("href", link);
    //            //lblSource.Attributes.Add("target", "_blank");
    //            //Response.Write("Hello");
    //            //Response.Write("<script>window.open('" + link + "','_blank');</script>");
    //            //"<script>window.open('PDF/" + result + "_" + liYear.Text + "_" + liQuater.Text + ".pdf','_blank');</script>"
    //            //Response.Write("<script>");
    //            //Response.Write("window.open("+link+",'_blank')");
    //            //Response.Write("</script>");
    //        }
    //    }
    //}
}