using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_GroupUploadDoc
/// </summary>
public class DA_GroupUploadDoc
{
    DO_GroupUploadDoc objUploadDoc = new DO_GroupUploadDoc();
    SqlCommand cmd = new SqlCommand();
	public DA_GroupUploadDoc()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public enum GroupUploadDoc
    {
        GetLikeComment=1,GetComment=2,InsertLike=3,InsertComment=4
    }

    public enum OrgUploadDoc
    {
        GetLikeComment = 1, GetComment = 2, InsertLike = 3, InsertComment = 4, GetGrpLikeComment = 5, GetGrpComment = 6, InsertGrpLike = 7, InsertGrpComment = 8, GetLikeUser = 9, GetLikeUsergrp =10
    }
    

    public DataTable GetGroupDocDataTable(DO_GroupUploadDoc objUploadDoc, GroupUploadDoc flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_GetGroupUploadDocument", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@intUploadDocId ", SqlDbType.Int).Value = objUploadDoc.intUploadDocId;
        da.SelectCommand.Parameters.Add("@intGroupId ", SqlDbType.Int).Value = objUploadDoc.intGroupId;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objUploadDoc.intAddedBy;
        da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = objUploadDoc.strIpAddress;
        da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 2000).Value = objUploadDoc.strComment;

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

    public void AddEditDel_GroupUpDocument(DO_GroupUploadDoc objUploadDoc, GroupUploadDoc flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();

        cmd = new SqlCommand("Scrl_GetGroupUploadDocument", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@intUploadDocId", SqlDbType.BigInt).Value = objUploadDoc.intUploadDocId;
        cmd.Parameters.Add("@intGroupId", SqlDbType.BigInt).Value = objUploadDoc.intGroupId;
        cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objUploadDoc.intAddedBy;
        cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = objUploadDoc.strIpAddress;
        cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 2000).Value = objUploadDoc.strComment;
        //cmd.Parameters.Add("@strLink", SqlDbType.VarChar, 200).Value = objUploadDoc.strLink;
        //cmd.Parameters.Add("@strMessage", SqlDbType.VarChar, 200).Value = objUploadDoc.strMessage;
        objUploadDoc.intUploadCommentLikeId = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);
    }

    public DataTable GetGroupOrgDocDataTable(DO_GroupUploadDoc objUploadDoc, OrgUploadDoc flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_GetGroupOrgUploadDocument", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@intUploadDocId ", SqlDbType.Int).Value = objUploadDoc.intUploadDocId;
        da.SelectCommand.Parameters.Add("@intGroupId ", SqlDbType.Int).Value = objUploadDoc.intGroupId;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objUploadDoc.intAddedBy;
        da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = objUploadDoc.strIpAddress;
        da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 2000).Value = objUploadDoc.strComment;

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

    public void AddEditDel_GroupOrgUpDocument(DO_GroupUploadDoc objUploadDoc, OrgUploadDoc flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();

        cmd = new SqlCommand("Scrl_GetGroupOrgUploadDocument", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@intUploadDocId", SqlDbType.BigInt).Value = objUploadDoc.intUploadDocId;
        cmd.Parameters.Add("@intGroupId", SqlDbType.BigInt).Value = objUploadDoc.intGroupId;
        cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objUploadDoc.intAddedBy;
        cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = objUploadDoc.strIpAddress;
        cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 2000).Value = objUploadDoc.strComment;
        //cmd.Parameters.Add("@strLink", SqlDbType.VarChar, 200).Value = objUploadDoc.strLink;
        //cmd.Parameters.Add("@strMessage", SqlDbType.VarChar, 200).Value = objUploadDoc.strMessage;
        objUploadDoc.intUploadCommentLikeId = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);
    }

}