using System.Data;
using System.Data.SqlClient;
using SqlConn;
using System;

/// <summary>
/// Summary description for DA_CaseInsert
/// </summary>
/// 
namespace DA_SKORKEL
{

    public class DA_ContentTagDef
    {
        public DA_ContentTagDef()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum ContentTagDef
        {
            Add = 1, GetContentTagDef = 2,Delete=3,GetDefExistByTag=4
        };


        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_TagDef(DO_ContentTagDef objTagDef, DA_ContentTagDef.ContentTagDef flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelContentTagDefinition", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@TagDefId", SqlDbType.BigInt).Value = objTagDef.TagDefId;
            cmd.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objTagDef.ContentId;
            cmd.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objTagDef.addedby;
            cmd.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objTagDef.ContentTypeID;
            cmd.Parameters.Add("@intStartIndex", SqlDbType.BigInt).Value = objTagDef.StartIndex;
            cmd.Parameters.Add("@intEndIndex", SqlDbType.BigInt).Value = objTagDef.EndIndex;
            cmd.Parameters.Add("@TaggedText", SqlDbType.VarChar, 8000).Value = objTagDef.TaggedText;
            cmd.Parameters.Add("@TagDefinition", SqlDbType.VarChar, 8000).Value = objTagDef.TagDef;
            cmd.Parameters.Add("@intTagType", SqlDbType.Int).Value = objTagDef.intTagType;
            //cmd.ExecuteNonQuery();
            objTagDef.TagDefOutId = Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_ContentTagDef objTagDef, DA_ContentTagDef.ContentTagDef flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelContentTagDefinition", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@TagDefId", SqlDbType.BigInt).Value = objTagDef.TagDefId;
            da.SelectCommand.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objTagDef.ContentId;
            da.SelectCommand.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objTagDef.addedby;
            da.SelectCommand.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objTagDef.ContentTypeID;
            da.SelectCommand.Parameters.Add("@TaggedText", SqlDbType.VarChar, 8000).Value = objTagDef.TaggedText;
            da.SelectCommand.Parameters.Add("@TagDefinition", SqlDbType.VarChar, 8000).Value = objTagDef.TagDef;
                
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}