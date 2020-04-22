using System;
using System.Data;
using DA_SKORKEL;
using System.Net.Mail;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Xml;
using System.Web.UI.HtmlControls;

public partial class UserControl_BlogArchives : System.Web.UI.UserControl
{
    DO_NewBlogs objblogdo = new DO_NewBlogs();
    DA_NewBlogs objblogda = new DA_NewBlogs();

    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["ExternalUserId"]) != "" && Session["ExternalUserId"] != null)
            {
                ViewState["UserID"] = Convert.ToInt32(Session["ExternalUserId"].ToString());
            }

            if (Convert.ToString(Session["UserTypeId"]) != "" && Session["UserTypeId"] != null)
                ViewState["FlagUser"] = Convert.ToInt32(Session["UserTypeId"].ToString());

            BindArchives();
        }
    }
    private void BindArchives()
    {
        objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        dt = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetArchives);
        // Clear the TreeView if there are another datas in this TreeView
        TreeArchives.Nodes.Clear();
        TreeArchives.ForeColor = System.Drawing.Color.Gray;
        TreeNode node = default(TreeNode);
        TreeNode subNode = default(TreeNode);
        foreach (DataRow row in dt.Rows)
        {
            //search in the treeview if any country is already present
            node = Searchnode(row[1].ToString(), TreeArchives);
            if (node != null)
            {
                //Country is already present
                subNode = new TreeNode(row[0].ToString());
                //Add cities to country
                node.ChildNodes.Add(subNode);
            }
            else
            {
                node = new TreeNode(row[1].ToString());
                subNode = new TreeNode(row[0].ToString());
                //Add cities to country
                node.ChildNodes.Add(subNode);
                TreeArchives.Nodes.Add(node);
            }
        }
        //if (expandAll)
        //{
        //    // Expand the TreeView
        //    trv.ExpandAll();
        //}

        //objblogdo.intAddedBy = Convert.ToInt32(ViewState["UserID"]);
        //dt = objblogda.GetDataTable(objblogdo, DA_NewBlogs.Blog.GetArchives);
        //if (dt.Rows.Count > 0)
        //{
        //    string NameYear = "";
        //    //Loop for binding Parent Values
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        TreeNode headnode = new TreeNode();
        //        if (NameYear != dt.Rows[i]["NameYear"].ToString())
        //        {
        //            headnode.Text = dt.Rows[i]["NameYear"].ToString();
        //            TreeArchives.Nodes.Add(headnode);
        //            NameYear = dt.Rows[i]["NameYear"].ToString();
        //        }
        //        TreeNode childnode = new TreeNode();
        //        childnode.Text = dt.Rows[i]["NameMonth"].ToString();
        //        headnode.ChildNodes.Add(childnode);
        //    }
        //}
        //else
        //{
        //    TreeArchives.DataSource = null;
        //    TreeArchives.DataBind();
        //}
    }
    private TreeNode Searchnode(string nodetext, TreeView trv)
    {
        TreeNode node1 = null;
        foreach (TreeNode node in trv.Nodes)
        {
            if (node.Text == nodetext)
            {
                node1 = node;
            }
            else
                node1 = null;

        }
        return node1;
    }
}