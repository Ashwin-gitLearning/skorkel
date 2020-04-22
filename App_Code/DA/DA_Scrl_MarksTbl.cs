using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_MarksTbl
    {
        private SqlCommand cmd;
        public enum Scrl_MarksTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5
        };
        public DA_Scrl_MarksTbl()
        { }
        public void AddEditDel_Scrl_MarksTbl(DO_Scrl_MarksTbl ObjScrl_MarksTbl, Scrl_MarksTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_MarksTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intMarksId", SqlDbType.Int).Value = ObjScrl_MarksTbl.intMarksId;
            cmd.Parameters.Add("@intClassId", SqlDbType.Int).Value = ObjScrl_MarksTbl.intClassId;
            cmd.Parameters.Add("@intSubjectId", SqlDbType.Int).Value = ObjScrl_MarksTbl.intSubjectId;
            cmd.Parameters.Add("@intStudentId", SqlDbType.Int).Value = ObjScrl_MarksTbl.intStudentId;
            cmd.Parameters.Add("@intProfesorId", SqlDbType.Int).Value = ObjScrl_MarksTbl.intProfesorId;
            cmd.Parameters.Add("@intMarks", SqlDbType.Int).Value = ObjScrl_MarksTbl.intMarks;
            cmd.Parameters.Add("@intOutOf", SqlDbType.Int).Value = ObjScrl_MarksTbl.intOutOf;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_MarksTbl.intAddedBy;
            cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_MarksTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_MarksTbl.intModifiedBy;
            cmd.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = ObjScrl_MarksTbl.strIPAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetDataTable(DO_Scrl_MarksTbl ObjScrl_MarksTbl, Scrl_MarksTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_MarksTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intMarksId", SqlDbType.Int).Value = ObjScrl_MarksTbl.intMarksId;
            da.SelectCommand.Parameters.Add("@intClassId", SqlDbType.Int).Value = ObjScrl_MarksTbl.intClassId;
            da.SelectCommand.Parameters.Add("@intSubjectId", SqlDbType.Int).Value = ObjScrl_MarksTbl.intSubjectId;
            da.SelectCommand.Parameters.Add("@intStudentId", SqlDbType.Int).Value = ObjScrl_MarksTbl.intStudentId;
            da.SelectCommand.Parameters.Add("@intProfesorId", SqlDbType.Int).Value = ObjScrl_MarksTbl.intProfesorId;
            da.SelectCommand.Parameters.Add("@intMarks", SqlDbType.Int).Value = ObjScrl_MarksTbl.intMarks;
            da.SelectCommand.Parameters.Add("@intOutOf", SqlDbType.Int).Value = ObjScrl_MarksTbl.intOutOf;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_MarksTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_MarksTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_MarksTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = ObjScrl_MarksTbl.strIPAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
