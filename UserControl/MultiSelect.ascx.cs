using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public delegate void DataChange(List<object> values);
public partial class UserControl_MultiSelect : System.Web.UI.UserControl
{
    public BaseDataBoundControl dc;
    public object DataSource;
    public string DataTextField = "text";
    public string DataValueField = "value";
    public event DataChange ValueChanged;
    public object Select;
    private string[] selectedValue = null;
    public string selectPlaceholder ="Select Some Options";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Select = selectControl;
            selectControl.DataSource = DataSource;
            selectControl.DataTextField = DataTextField;
            selectControl.DataValueField = DataValueField;
            selectControl.DataBind();
            SetValues(selectedValue);
           // selectControl.Items.Insert(0, new ListItem(""));
        }
        dvdummyinput.InnerText = selectPlaceholder;
    }
    public List<KeyValuePair<string,string>> GetSelectedValues()
    {
        ListItemCollection items = this.selectControl.Items;
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
            selectControl.DataSource = DataSource;
            selectControl.DataTextField = DataTextField;
            selectControl.DataValueField = DataValueField;
            selectControl.DataBind();
          //  selectControl.Items.Insert(0, new ListItem(""));
        }
        selectControl.SelectedIndex = -1;
    }
    public void SetValues(string[] values)
    {
        if (values == null || values.Count() == 0)
        {
            selectControl.SelectedIndex = -1;
        }
        else
        {
            selectedValue = values;
            ListItemCollection items = this.selectControl.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (values.Contains(items[i].Value))
                    items[i].Selected = true;
            }
        }
    }
}