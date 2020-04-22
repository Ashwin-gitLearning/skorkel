using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_AddEditDelUserProject
    {
        private SqlCommand cmd;
        public enum AddEditDelUserProject
        {
            Add = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetRecord = 6, DeleteDocumnet = 7
        };

        public DA_Scrl_AddEditDelUserProject()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Scrl_AddEditDelUserProject(DO_Scrl_AddEditDelUserProject ObjScrl, AddEditDelUserProject Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelUserProject", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intProjectId", SqlDbType.Int).Value = ObjScrl.intProjectId;
            cmd.Parameters.Add("@intProjectAttachId", SqlDbType.Int).Value = ObjScrl.intProjectAttachId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl.intRegistrationId;
            cmd.Parameters.Add("@strProjectTitle", SqlDbType.VarChar, 100).Value = ObjScrl.strProjectTitle;
            cmd.Parameters.Add("@strProjectSummary", SqlDbType.VarChar, 1000).Value = ObjScrl.strProjectSummary;
            cmd.Parameters.Add("@strProjectDetails", SqlDbType.VarChar, 1000).Value = ObjScrl.strProjectDetails;
            cmd.Parameters.Add("@intSubjectId", SqlDbType.VarChar, 1000).Value = ObjScrl.intSubjectId;

            cmd.Parameters.Add("@strFileName1", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName1;
            cmd.Parameters.Add("@strFileName2", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName2;
            cmd.Parameters.Add("@strFileName3", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName3;
            cmd.Parameters.Add("@strFileName4", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName4;
            cmd.Parameters.Add("@strFileName5", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName5;
            cmd.Parameters.Add("@strFileName6", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName6;
            cmd.Parameters.Add("@strFileName7", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName7;
            cmd.Parameters.Add("@strFileName8", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName8;
            cmd.Parameters.Add("@strFileName9", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName9;
            cmd.Parameters.Add("@strFileName10", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName10;

            cmd.Parameters.Add("@strFileDescription1", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription1;
            cmd.Parameters.Add("@strFileDescription2", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription2;
            cmd.Parameters.Add("@strFileDescription3", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription3;
            cmd.Parameters.Add("@strFileDescription4", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription4;
            cmd.Parameters.Add("@strFileDescription5", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription5;
            cmd.Parameters.Add("@strFileDescription6", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription6;
            cmd.Parameters.Add("@strFileDescription7", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription7;
            cmd.Parameters.Add("@strFileDescription8", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription8;
            cmd.Parameters.Add("@strFileDescription9", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription9;
            cmd.Parameters.Add("@strFileDescription10", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription10;

            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl.intAddedBy;
            cmd.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = ObjScrl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_AddEditDelUserProject ObjScrl, AddEditDelUserProject Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelUserProject", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intProjectId", SqlDbType.Int).Value = ObjScrl.intProjectId;
            da.SelectCommand.Parameters.Add("@intProjectAttachId", SqlDbType.Int).Value = ObjScrl.intProjectAttachId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strProjectTitle", SqlDbType.VarChar, 100).Value = ObjScrl.strProjectTitle;
            da.SelectCommand.Parameters.Add("@strProjectSummary", SqlDbType.VarChar, 1000).Value = ObjScrl.strProjectSummary;
            da.SelectCommand.Parameters.Add("@strProjectDetails", SqlDbType.VarChar, 1000).Value = ObjScrl.strProjectDetails;
            da.SelectCommand.Parameters.Add("@intSubjectId", SqlDbType.VarChar, 1000).Value = ObjScrl.intSubjectId;

            da.SelectCommand.Parameters.Add("@strFileName1", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName1;
            da.SelectCommand.Parameters.Add("@strFileName2", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName2;
            da.SelectCommand.Parameters.Add("@strFileName3", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName3;
            da.SelectCommand.Parameters.Add("@strFileName4", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName4;
            da.SelectCommand.Parameters.Add("@strFileName5", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName5;
            da.SelectCommand.Parameters.Add("@strFileName6", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName6;
            da.SelectCommand.Parameters.Add("@strFileName7", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName7;
            da.SelectCommand.Parameters.Add("@strFileName8", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName8;
            da.SelectCommand.Parameters.Add("@strFileName9", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName9;
            da.SelectCommand.Parameters.Add("@strFileName10", SqlDbType.VarChar, 500).Value = ObjScrl.strFileName10;

            da.SelectCommand.Parameters.Add("@strFileDescription1", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription1;
            da.SelectCommand.Parameters.Add("@strFileDescription2", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription2;
            da.SelectCommand.Parameters.Add("@strFileDescription3", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription3;
            da.SelectCommand.Parameters.Add("@strFileDescription4", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription4;
            da.SelectCommand.Parameters.Add("@strFileDescription5", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription5;
            da.SelectCommand.Parameters.Add("@strFileDescription6", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription6;
            da.SelectCommand.Parameters.Add("@strFileDescription7", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription7;
            da.SelectCommand.Parameters.Add("@strFileDescription8", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription8;
            da.SelectCommand.Parameters.Add("@strFileDescription9", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription9;
            da.SelectCommand.Parameters.Add("@strFileDescription10", SqlDbType.VarChar, 50).Value = ObjScrl.strFileDescription10;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = ObjScrl.strIpAddress;

            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl.Currentpage; 
            da.SelectCommand.Parameters.Add("@pagesize", SqlDbType.Int).Value = ObjScrl.PageSize; 

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}