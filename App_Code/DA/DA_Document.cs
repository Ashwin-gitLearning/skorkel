using System.Data;
using System.Data.SqlClient;
using SqlConn;
using System;

/// <summary>
/// Summary description for DA_Document
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_Document
    {
        public DA_Document()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum Document
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5, UpdateDocContent = 6
        };

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_Document objDocument, Document flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
                
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelDocumentUpload", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objDocument.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = objDocument.CurrentPage;
            da.SelectCommand.Parameters.Add("@DocId", SqlDbType.BigInt).Value = objDocument.DocId;
            da.SelectCommand.Parameters.Add("@StudentId", SqlDbType.BigInt).Value = objDocument.StudentId;
            da.SelectCommand.Parameters.Add("@DocumentTitle", SqlDbType.VarChar,200).Value = objDocument.DocTitle;
            da.SelectCommand.Parameters.Add("@FilePath", SqlDbType.VarChar, 200).Value = objDocument.FilePath;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_Document(DO_Document objDocument, Document flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelDocumentUpload", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@DocId", SqlDbType.BigInt).Value = objDocument.DocId;
            cmd.Parameters.Add("@StudentId", SqlDbType.BigInt).Value = objDocument.StudentId;
            cmd.Parameters.Add("@DocumentTitle", SqlDbType.VarChar, 200).Value = objDocument.DocTitle;
            cmd.Parameters.Add("@strDocContent", SqlDbType.Text).Value = objDocument.DocContent;
            cmd.Parameters.Add("@FilePath", SqlDbType.VarChar, 200).Value = objDocument.FilePath;
            cmd.Parameters.Add("@intMicroTag", SqlDbType.Int).Value = objDocument.MicroTag;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objDocument.AddedBy;
           // cmd.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = objDocument.ModifiedOn;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = objDocument.ModifiedBy;

            objDocument.intDocOutId = Convert.ToInt32(cmd.ExecuteScalar());    
            //cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }

    }
}