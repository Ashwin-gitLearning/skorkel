using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public delegate void DataChange1(List<object> values);
public partial class UserControl_MultiSelectJudges : System.Web.UI.UserControl
{
    public BaseDataBoundControl dc;
    public object DataSource;
    public string DataTextField = "text";
    public string DataValueField = "value";
    public event DataChange1 ValueChanged;
    public object Select;
    private string[] selectedValue = null;
    public string selectPlaceholder ="Select Some Options";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Select = selectControl1;
            selectControl1.DataSource = DataSource;
            selectControl1.DataTextField = DataTextField;
            selectControl1.DataValueField = DataValueField;
            selectControl1.DataBind();
            SetValues(selectedValue);
           // selectControl.Items.Insert(0, new ListItem(""));
        }
        dvdummyinput1.InnerText = selectPlaceholder;
    }
    public List<KeyValuePair<string,string>> GetSelectedValues()
    {
        ListItemCollection items = this.selectControl1.Items;
        List<KeyValuePair<string, string>> selectedValues = new List<KeyValuePair<string, string>>();
        foreach (ListItem item in items)
        {
            if (item.Selected)
                selectedValues.Add(new KeyValuePair<string, string>( item.Text, item.Value));
        }
        
        return selectedValues;
    }
    public void RefreshList()
    {
        selectedValue = null;
        if (DataSource != null)
        {
            selectControl1.DataSource = DataSource;
            selectControl1.DataTextField = DataTextField;
            selectControl1.DataValueField = DataValueField;
            selectControl1.DataBind();
          //  selectControl.Items.Insert(0, new ListItem(""));
        }
        selectControl1.SelectedIndex = -1;
    }
    public void SetValues(string[] values)
    {
        if (values == null || values.Count() == 0)
        {
            selectControl1.SelectedIndex = -1;
        }
        else
        {
            selectedValue = values;
            ListItemCollection items = this.selectControl1.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (values.Contains(items[i].Value))
                    items[i].Selected = true;
            }
        }
    }
}