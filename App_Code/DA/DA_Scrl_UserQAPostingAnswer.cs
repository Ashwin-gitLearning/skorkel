using SqlConn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DA_Scrl_UserQAPostingAnswer
/// </summary>
public class DA_Scrl_UserQAPostingAnswer
{
    private SqlCommand cmd;
    public DA_Scrl_UserQAPostingAnswer()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public enum QuetionAns
    {
        AddAnsListDtl = 40, AnsListDtl = 41, GetTotalAnsLikeById =42, UpdateCommentById = 43, CommentsDtlsById = 44, DeleteAnsCmnts = 45, CommentsDtlsBytblId = 46,
        UpdateCommentBytblId = 47, GetUserdtlByCmnyId = 49
    }
    public void AddEditDel_Scrl_UserQueAnswerTbl(DO_Scrl_UserQAPostingAnswer objQueAns, QuetionAns Flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_AddEditDelQuetionAns", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
        cmd.Parameters.Add("@intAnswerId", SqlDbType.Int).Value = objQueAns.intAnswerId;
        cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objQueAns.intAnsAddedBy;
        cmd.Parameters.Add("@strRepLiShStatus", SqlDbType.VarChar, 2).Value = objQueAns.strAnsLiStatus;
        
        cmd.ExecuteScalar();
        co.CloseConnection(conn);
    }

    public int AddEditDel_Scrl_UserAddingAnsPost(DO_Scrl_UserQAPostingAnswer objQueAns, QuetionAns Flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_AddEditDelQuetionAns", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
        cmd.Parameters.Add("@intAnswerId", SqlDbType.Int).Value = objQueAns.intAnswerId;
        cmd.Parameters.Add("@strRepLiShStatus", SqlDbType.VarChar, 2).Value = objQueAns.strAnsLiStatus;
        cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500000000).Value = objQueAns.strComment;
        cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objQueAns.intAnsAddedBy;
        //cmd.ExecuteScalar();
        objQueAns.ResultId = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);
        
        return (objQueAns.ResultId);
    }

    public DataTable GetDataTable(DO_Scrl_UserQAPostingAnswer objcategory, DA_Scrl_UserQAPostingAnswer.QuetionAns flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelQuetionAns", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        
        da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcategory.intAnsAddedBy;
        da.SelectCommand.Parameters.Add("@intAnswerId", SqlDbType.Int).Value = objcategory.intAnswerId;
        
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

    public DataTable GetDataTableComments(DO_Scrl_UserQAPostingAnswer objcategory, DA_Scrl_UserQAPostingAnswer.QuetionAns flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelQuetionAns", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;

        //da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcategory.intAnsAddedBy;
        da.SelectCommand.Parameters.Add("@intAnswerId", SqlDbType.Int).Value = objcategory.intAnswerId;

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

    public DataTable GetDataTableCommentsByID(DO_Scrl_UserQAPostingAnswer objcategory, DA_Scrl_UserQAPostingAnswer.QuetionAns flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelQuetionAns", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;

        da.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = objcategory.ID;

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

    public void AddEditDel_Scrl_UserQueAnsDeleteCmntsTbl(DO_Scrl_UserQAPostingAnswer objQueAns, QuetionAns Flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_AddEditDelQuetionAns", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = objQueAns.ID;
        
        objQueAns.ID = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);
    }

    public void AddEditDel_Scrl_UserQueAnsUpdateCmntsTbl(DO_Scrl_UserQAPostingAnswer objQueAns, QuetionAns Flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_AddEditDelQuetionAns", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = objQueAns.ID;
        cmd.Parameters.Add("@strComment", SqlDbType.VarChar).Value = objQueAns.strComment;
        cmd.ExecuteScalar();
        
        co.CloseConnection(conn);
    }

}