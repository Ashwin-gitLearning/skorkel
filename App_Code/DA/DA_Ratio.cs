using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_Ratio
/// </summary>
public class DA_Ratio
{
    private SqlCommand cmd;
    public DA_Ratio()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public enum Ratio
    {
        Add = 1, Update = 2, Delete = 3, GetSingleRatio = 4, GetRatioByCaseId = 5, GetAllRatio = 6, GetParentChildRatio = 7,GetTagTypeId=8,GetParentidByRatioId=9,GetIssueTitleBytagTypeId=10
    }

    public void AddEditDel_Ratio(DO_Ratio objRatio, Ratio flag)
    {   
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        cmd = new SqlCommand("[Scrl_AddEditDelRatio", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@intRatioId", SqlDbType.BigInt).Value = objRatio.intRatioId;
        cmd.Parameters.Add("@intDocId", SqlDbType.BigInt).Value = objRatio.CaseId;
        cmd.Parameters.Add("@intDocTypeId", SqlDbType.Int).Value = objRatio.ContentTypeID;
        cmd.Parameters.Add("@strTagDescription", SqlDbType.VarChar, 4000).Value = objRatio.strTagDescription;
        cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRatio.intAddedBy;
        cmd.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objRatio.StartIndex;
        cmd.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objRatio.EndIndex;

        cmd.Parameters.Add("@intTagType", SqlDbType.Int).Value = objRatio.intTagType;
        cmd.Parameters.Add("@strRatioTitle", SqlDbType.VarChar, 500).Value = objRatio.strRatioTitle;
        cmd.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objRatio.intModifiedBy;
        cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objRatio.strIpAddress;
        cmd.Parameters.Add("@intParentId", SqlDbType.BigInt).Value = objRatio.intParentId;

        cmd.ExecuteNonQuery();
        co.CloseConnection(conn);
    }

    public DataTable GetDataTable(DO_Ratio objRatio, Ratio flag)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelRatio", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@intRatioId", SqlDbType.BigInt).Value = objRatio.intRatioId;
        da.SelectCommand.Parameters.Add("@intDocId", SqlDbType.BigInt).Value = objRatio.CaseId;
        da.SelectCommand.Parameters.Add("@intDocTypeId", SqlDbType.Int).Value = objRatio.ContentTypeID;
        da.SelectCommand.Parameters.Add("@strTagDescription", SqlDbType.VarChar, 4000).Value = objRatio.strTagDescription;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRatio.intAddedBy;
        da.SelectCommand.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objRatio.StartIndex;
        da.SelectCommand.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objRatio.EndIndex;

        da.SelectCommand.Parameters.Add("@intTagType", SqlDbType.Int).Value = objRatio.intTagType;
        da.SelectCommand.Parameters.Add("@strRatioTitle", SqlDbType.VarChar, 500).Value = objRatio.strRatioTitle;
        da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objRatio.intModifiedBy;
        da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objRatio.strIpAddress;
        da.SelectCommand.Parameters.Add("@intParentId", SqlDbType.BigInt).Value = objRatio.intParentId;
        da.SelectCommand.Parameters.Add("@strNewRatioTitle", SqlDbType.VarChar, 500).Value = objRatio.strNewRatioTitle;

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

    public DataTable GetMarkDataTable(DO_Ratio objRatio, Ratio flag)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelDocMark", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@intRatioId", SqlDbType.BigInt).Value = objRatio.intRatioId;
        da.SelectCommand.Parameters.Add("@intDocId", SqlDbType.BigInt).Value = objRatio.CaseId;
        da.SelectCommand.Parameters.Add("@intDocTypeId", SqlDbType.Int).Value = objRatio.ContentTypeID;
        da.SelectCommand.Parameters.Add("@strTagDescription", SqlDbType.VarChar, 4000).Value = objRatio.strTagDescription;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRatio.intAddedBy;
        da.SelectCommand.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objRatio.StartIndex;
        da.SelectCommand.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objRatio.EndIndex;

        da.SelectCommand.Parameters.Add("@intTagType", SqlDbType.Int).Value = objRatio.intTagType;
        da.SelectCommand.Parameters.Add("@strRatioTitle", SqlDbType.VarChar, 500).Value = objRatio.strRatioTitle;
        da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objRatio.intModifiedBy;
        da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objRatio.strIpAddress;
        da.SelectCommand.Parameters.Add("@intParentId", SqlDbType.BigInt).Value = objRatio.intParentId;
        da.SelectCommand.Parameters.Add("@strNewRatioTitle", SqlDbType.VarChar, 500).Value = objRatio.strNewRatioTitle;

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

    public DataSet GetDataSet(DO_Ratio objRatio, Ratio flag)
    {
        DataSet ds = new DataSet();

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelRatio", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@intRatioId", SqlDbType.BigInt).Value = objRatio.intRatioId;
        da.SelectCommand.Parameters.Add("@intDocId", SqlDbType.BigInt).Value = objRatio.CaseId;
        da.SelectCommand.Parameters.Add("@intDocTypeId", SqlDbType.Int).Value = objRatio.ContentTypeID;
        da.SelectCommand.Parameters.Add("@strTagDescription", SqlDbType.VarChar, 4000).Value = objRatio.strTagDescription;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRatio.intAddedBy;
        da.SelectCommand.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objRatio.StartIndex;
        da.SelectCommand.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objRatio.EndIndex;

        da.SelectCommand.Parameters.Add("@intTagType", SqlDbType.Int).Value = objRatio.intTagType;
        da.SelectCommand.Parameters.Add("@strRatioTitle", SqlDbType.VarChar, 500).Value = objRatio.strRatioTitle;
        da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objRatio.intModifiedBy;
        da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objRatio.strIpAddress;
        da.SelectCommand.Parameters.Add("@intParentId", SqlDbType.BigInt).Value = objRatio.intParentId;

        da.Fill(ds);
        co.CloseConnection(conn);
        return ds;
    }
}