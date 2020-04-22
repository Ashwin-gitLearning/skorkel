using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_Role
/// </summary>

namespace  DA_SKORKEL
{
    public class DA_Role
    {
        public DA_Role()
        {
            //
            // TODO: Add constructor logic here
            //
        }

       
        public enum Role
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, SingleRecord = 5,SelectUserType=6,drpUsertypewise=7,TagPanelwise=8,GetUserType=9
            , TagWiseBind = 10, GettagPermission = 11, GetDoctagPermission=12
        }

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_Role objRole, Role flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelUserRolePermission", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@UserTypeID", SqlDbType.Int).Value = objRole.UserTypeID;
            da.SelectCommand.Parameters.Add("@URPId", SqlDbType.Int).Value = objRole.URPId;
            da.SelectCommand.Parameters.Add("@TagName", SqlDbType.VarChar, 200).Value = objRole.TagName;
            da.SelectCommand.Parameters.Add("@TagCode", SqlDbType.Int).Value = objRole.TagCode;
            da.SelectCommand.Parameters.Add("@IsAll", SqlDbType.Bit).Value = objRole.IsAll;
            da.SelectCommand.Parameters.Add("@IsView", SqlDbType.Bit).Value = objRole.IsView;
            da.SelectCommand.Parameters.Add("@IsAdd", SqlDbType.Bit).Value = objRole.IsAdd;
            da.SelectCommand.Parameters.Add("@IsModify", SqlDbType.Bit).Value = objRole.IsModify;
            da.SelectCommand.Parameters.Add("@IsDelete", SqlDbType.Bit).Value = objRole.IsDelete;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDeleteRole(DO_Role objRole, Role flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelUserRolePermission", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@URPId", SqlDbType.Int).Value = objRole.URPId;
            cmd.Parameters.Add("@UserTypeID", SqlDbType.Int).Value = objRole.UserTypeID;
            cmd.Parameters.Add("@TagName", SqlDbType.VarChar, 200).Value = objRole.TagName;
            cmd.Parameters.Add("@TagCode", SqlDbType.Int).Value = objRole.TagCode;
            cmd.Parameters.Add("@IsAll", SqlDbType.Bit).Value = objRole.IsAll;
            cmd.Parameters.Add("@IsView", SqlDbType.Bit).Value = objRole.IsView;
            cmd.Parameters.Add("@IsAdd", SqlDbType.Bit).Value =objRole.IsAdd;
            cmd.Parameters.Add("@IsModify", SqlDbType.Bit).Value =objRole.IsModify;
            cmd.Parameters.Add("@IsDelete", SqlDbType.Bit).Value = objRole.IsDelete;
        }

        public DataTable GetDataTableTypeRole(DO_Role objRole, Role flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_InSertRegistrationSP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@RegistrationId", SqlDbType.Int).Value = objRole.AddedBy;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objRole.AddedBy;
           
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public DataSet GetDatasetRoleDetails(int ContentID,int ContentTypeID, Role flag)
        {
            DataSet dt = new DataSet();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelUserRolePermission", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@ContentId", SqlDbType.Int).Value = ContentID;
            da.SelectCommand.Parameters.Add("@ContentTypeID", SqlDbType.Int).Value = ContentTypeID;              
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}