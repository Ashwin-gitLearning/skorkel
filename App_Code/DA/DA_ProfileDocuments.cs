using System.Data;
using System.Data.SqlClient;
using SqlConn;
using System;

/// <summary>
/// Summary description for DA_ProfileDocuments
/// </summary>
namespace DA_SKORKEL
{
    public class DA_ProfileDocuments
    {
        public DA_ProfileDocuments()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region document and GroupUpload Documnet
        public enum Document
        {
            Add = 2, ADDSelectedSubCatId = 4, Update = 10, DeleteDocument = 12,DeleteTempTable=14,UpdateDocumentByDocId=19,DeleteUserDoc=20,SelectDoc=6,
        };
        public enum GropDocument
        {
            InsertGroupUploadDocs = 1, Getdocuments = 2, GetFolderDocs = 3, GetGetFolderDocsById = 4, GetInnerFolderDocs = 5, GetInnerDetails = 6, InsertFolderName = 7, GetFolderDetailsById = 8, GetAllFolderDetail = 9, GetDocsDetailsByFolderId=10,
            GetChildFolderName = 11, GetChildFolderNameByParentID = 12, GetTotalFolder = 13, GetTotalDocs = 14, GetCategoryNameByCatId = 15, DeleteTempTable = 16, RenameFolder = 17, DeleteDocFolder = 18, DeleteOldContext = 19, EditDocsByDocId = 20, EditSUbCatIdByDocsID = 21,
            GetSelectedValue = 22, Update = 23, DeleteDocumentByDocId=24
        };
        public enum DocumenTemp
        {
            AddTempSubCat = 1, GetDocumentSubCat = 3, GetTotalDocumetUploadByAddedBy = 5, EditRecordByDocId = 6, EditSubJectCatID = 7, GetDocumetTypeId = 8, GetCatNameByCatID = 9, GetDocsIsSaleByAddedBy = 10, Getdocuments = 11, GetSubCatExist = 13, GetIsSaleDetails = 15, GetCatname = 16, GetIsDownload = 17, GetSubcatSelectedForPreview = 18,
            GetMyDocDetailByAddedBy = 20, GetDocsSaleByAddedBy = 21, GetTotalDocSale = 22, GetPublishDocsByAddedBy = 23, GetTotalPublishDocs = 24, GetTopFourDocs = 25, GetTotalDocs = 26, GetSelectedValue = 28, GetPublicDocsForFriend = 29,GetDocSubcategory=30
        };
        public enum UploadDocument
        {
            AddTempSubCat = 1, InsertGroupUploadDocs = 2, GetDocumentSubCat = 3, ADDSelectedSubCatId = 4, GetTotalDocumetUploadByAddedBy = 5, GetCatNameByCatID = 9, Getdocuments = 11, DeleteTempTable = 14, GetIsSaleDetails = 15
        }

        #endregion

        #region organization document and GroupUpload Documnet
        public enum OrgDocument
        {
            Add = 2, ADDSelectedSubCatId = 4, Update = 10, DeleteDocument = 12, DeleteTempTable = 14, UpdateDocumentByDocId = 19
        };
        public enum OrgDocumenTemp
        {
            AddTempSubCat = 1, GetDocumentSubCat = 3, GetTotalDocumetUploadByAddedBy = 5, EditRecordByDocId = 6, EditSubJectCatID = 7, GetDocumetTypeId = 8, GetCatNameByCatID = 9, GetDocsIsSaleByAddedBy = 10, Getdocuments = 11, GetSubCatExist = 13, GetIsSaleDetails = 15, GetCatname = 16, GetIsDownload = 17, GetSubcatSelectedForPreview = 18,
            GetMyDocDetailByAddedBy = 20, GetDocsSaleByAddedBy = 21, GetTotalDocSale = 22, GetPublishDocsByAddedBy = 23, GetTotalPublishDocs = 24, GetTopFourDocs = 25, GetTotalDocs = 26, GetSelectedValue = 27
        };
        public enum GropOrgDocument
        {
            InsertGroupUploadDocs = 1, Getdocuments = 2, GetFolderDocs = 3, GetGetFolderDocsById = 4, GetInnerFolderDocs = 5, GetInnerDetails = 6, InsertFolderName = 7, GetFolderDetailsById = 8, GetAllFolderDetail = 9, GetDocsDetailsByFolderId = 10,
            GetChildFolderName = 11, GetChildFolderNameByParentID = 12, GetTotalFolder = 13, GetTotalDocs = 14, GetCategoryNameByCatId = 15, DeleteTempTable = 16, GetAllFolderDetailByFolderId = 17, UpdateFolderName = 18, DeleteFolderByFolderId = 19, DeleteDocsById = 20, DeleteOldContext=21,
            EditSUbCatIdByDocsID = 22, GetSelectedValue = 23, Update = 24, DeleteDocumentByDocId = 25, EditDocsByDocId = 26, UpdateDocumentByDocId = 27, GetSingleFolderDetail=28,UpdateFolderId=29,GetDocDetailsByUploadDocId=30,GetUploadDocSubContaxt=31,GetSelectedDocContext=32
        };
        public enum OrgUploadDocument
        {
            AddTempSubCat = 1, InsertGroupUploadDocs = 2, GetDocumentSubCat = 3, ADDSelectedSubCatId = 4, GetTotalDocumetUploadByAddedBy = 5, GetCatNameByCatID = 9, Getdocuments = 11, DeleteTempTable = 14, GetIsSaleDetails = 15
        }

        #endregion
        SqlCommand cmd = new SqlCommand();

        #region document and GroupUpload Documnet
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
            da.SelectCommand.Parameters.Add("@DocumentTitle", SqlDbType.VarChar, 200).Value = objDocument.DocTitle;
            da.SelectCommand.Parameters.Add("@FilePath", SqlDbType.VarChar, 200).Value = objDocument.FilePath;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
        public void AddEditDel_Document(DO_ProfileDocuments objDocument, Document flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelContextDocument", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@DocId", SqlDbType.BigInt).Value = objDocument.DocId;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDocument.intAddedBy;
            cmd.Parameters.Add("@FilePath", SqlDbType.VarChar, 200).Value = objDocument.FilePath;
            cmd.Parameters.Add("@DocumentTitle", SqlDbType.VarChar, 200).Value = objDocument.DocTitle;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objDocument.ModifiedBy;
            cmd.Parameters.Add("@intDocumentTypeID", SqlDbType.Int).Value = objDocument.intDocumentTypeID;
            cmd.Parameters.Add("@strAuthors", SqlDbType.VarChar, 255).Value = objDocument.strAuthors;
            cmd.Parameters.Add("@IsDocsSale", SqlDbType.VarChar, 2).Value = objDocument.IsDocsSale;
            cmd.Parameters.Add("@IsDocsDownload", SqlDbType.VarChar, 2).Value = objDocument.IsDocsDownload;
            cmd.Parameters.Add("@intDocsSee", SqlDbType.VarChar, 200).Value = objDocument.intDocsSee;
            cmd.Parameters.Add("@Price", SqlDbType.Float).Value = objDocument.Price;
            cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objDocument.intSubjectCategoryId;
            cmd.Parameters.Add("@strDocName", SqlDbType.VarChar,500).Value = objDocument.strDocName;
            cmd.Parameters.Add("@strDescrition", SqlDbType.VarChar, 500).Value = objDocument.strDescrition;
            cmd.CommandTimeout = 120;// Setting command timeout to 2 minutes
            objDocument.DocId = Convert.ToInt32(cmd.ExecuteScalar());
       
            co.CloseConnection(conn);

        }
        public void AddEditDel_SubjCategoryDetails(DO_ProfileDocuments objcategory, DA_ProfileDocuments.DocumenTemp flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelContextDocument", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objcategory.intSubjectCategoryId;
          //  cmd.Parameters.Add("@RegistrationId", SqlDbType.VarChar, 200).Value = objcategory.RegistrationId;
            cmd.Parameters.Add("@AddedBy", SqlDbType.VarChar, 200).Value = objcategory.AddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objcategory.ModifiedBy;
             cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }
        public DataTable GetDataTable(DO_ProfileDocuments objcategory, DA_ProfileDocuments.DocumenTemp flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelContextDocument", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@DocId", SqlDbType.Int).Value = objcategory.DocId;
            da.SelectCommand.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objcategory.intSubjectCategoryId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcategory.AddedBy;
            da.SelectCommand.Parameters.Add("@FriendId", SqlDbType.BigInt).Value = objcategory.FriendId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public DataTable GetDataTable(DO_ProfileDocuments objcategory, DA_ProfileDocuments.Document flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelContextDocument", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@DocId", SqlDbType.Int).Value = objcategory.DocId;
            da.SelectCommand.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objcategory.intSubjectCategoryId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcategory.AddedBy;
            da.SelectCommand.Parameters.Add("@FriendId", SqlDbType.BigInt).Value = objcategory.FriendId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_GroupDocument(DO_ProfileDocuments objDocument, GropDocument flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelGroupDocument", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objDocument.RegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = objDocument.intGroupId;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDocument.intAddedBy;
            cmd.Parameters.Add("@strDocTitle", SqlDbType.VarChar, 200).Value = objDocument.DocTitle;
            cmd.Parameters.Add("@FilePath", SqlDbType.VarChar, 200).Value = objDocument.FilePath;
            cmd.Parameters.Add("@StrDocsDetails", SqlDbType.VarChar, 200).Value = objDocument.StrDocsDetails;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objDocument.ModifiedBy;
            cmd.Parameters.Add("@IsDocsSale", SqlDbType.VarChar, 2).Value = objDocument.IsDocsSale;
            cmd.Parameters.Add("@IsDocsDownload", SqlDbType.VarChar, 2).Value = objDocument.IsDocsDownload;
            cmd.Parameters.Add("@Price", SqlDbType.Float).Value = objDocument.Price;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objDocument.IpAddress;
            cmd.Parameters.Add("@IsFolder", SqlDbType.VarChar, 20).Value = objDocument.IsFolder;
            cmd.Parameters.Add("@intUploadDocId", SqlDbType.Int).Value = objDocument.UploadDocId;
            cmd.Parameters.Add("@strFolderName", SqlDbType.VarChar,100).Value = objDocument.strFolderName;
            cmd.Parameters.Add("@intFolderId", SqlDbType.Int).Value = objDocument.intFolderId;
            cmd.Parameters.Add("@intParentId", SqlDbType.Int).Value = objDocument.intParentId;
            cmd.Parameters.Add("@strAuthors", SqlDbType.VarChar, 255).Value = objDocument.strAuthors;
            cmd.Parameters.Add("@intDocsSee", SqlDbType.VarChar, 200).Value = objDocument.intDocsSee;
            cmd.Parameters.Add("@intDocumentTypeID", SqlDbType.Int).Value = objDocument.intDocumentTypeID;
            cmd.Parameters.Add("@strIsPublic", SqlDbType.VarChar, 500).Value = objDocument.strIsPublic;
            cmd.Parameters.Add("@strFolderDescription", SqlDbType.VarChar, 500).Value = objDocument.strFolderDescription;
            cmd.Parameters.Add("@strDocName", SqlDbType.VarChar, 500).Value = objDocument.strDocName;

            objDocument.DocId = Convert.ToInt32(cmd.ExecuteScalar());

            co.CloseConnection(conn);

        }
        public DataTable GetGrouDocumetDataTable(DO_ProfileDocuments objcategory, DA_ProfileDocuments.GropDocument flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelGroupDocument", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intUploadDocId ", SqlDbType.Int).Value = objcategory.UploadDocId;
            da.SelectCommand.Parameters.Add("@intFolderId ", SqlDbType.Int).Value = objcategory.intFolderId;
            da.SelectCommand.Parameters.Add("@intParentId ", SqlDbType.Int).Value = objcategory.intParentId;
            da.SelectCommand.Parameters.Add("@intGroupId ", SqlDbType.Int).Value = objcategory.intGroupId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcategory.AddedBy;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
        public void AddEditDel_GroupUploadDocument(DO_ProfileDocuments objDocument, UploadDocument flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
          
            cmd = new SqlCommand("Scrl_AddEditDelGroupUploadDocument", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intGroupDocId", SqlDbType.BigInt).Value = objDocument.intGroupDocId;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDocument.AddedBy;
            cmd.Parameters.Add("@FilePath", SqlDbType.VarChar, 200).Value = objDocument.FilePath;
            cmd.Parameters.Add("@DocumentTitle", SqlDbType.VarChar, 200).Value = objDocument.DocTitle;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objDocument.ModifiedBy;
            cmd.Parameters.Add("@intDocumentTypeID", SqlDbType.Int).Value = objDocument.intDocumentTypeID;
            cmd.Parameters.Add("@strAuthors", SqlDbType.VarChar, 255).Value = objDocument.strAuthors;
            cmd.Parameters.Add("@IsDocsSale", SqlDbType.VarChar, 2).Value = objDocument.IsDocsSale;
            cmd.Parameters.Add("@IsDocsDownload", SqlDbType.VarChar, 2).Value = objDocument.IsDocsDownload;
            cmd.Parameters.Add("@intDocsSee", SqlDbType.VarChar, 200).Value = objDocument.intDocsSee;
            cmd.Parameters.Add("@Price", SqlDbType.Float).Value = objDocument.Price;
            cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objDocument.intSubjectCategoryId;
            cmd.Parameters.Add("@strDocName", SqlDbType.VarChar, 500).Value = objDocument.strDocName;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = objDocument.intGroupId;
            objDocument.DocId = Convert.ToInt32(cmd.ExecuteScalar());

            co.CloseConnection(conn);

        }
        public DataTable GetGroupUploadTable(DO_ProfileDocuments objcategory, DA_ProfileDocuments.UploadDocument flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelGroupUploadDocument", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intGroupDocId", SqlDbType.Int).Value = objcategory.intGroupDocId;
            da.SelectCommand.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objcategory.intSubjectCategoryId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcategory.AddedBy;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.BigInt).Value = objcategory.intGroupId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
        #endregion

        #region organization document and GroupUpload Documnet
        public DataTable GetOrgDataTable(DO_Document objDocument, Document flag)
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
            da.SelectCommand.Parameters.Add("@DocumentTitle", SqlDbType.VarChar, 200).Value = objDocument.DocTitle;
            da.SelectCommand.Parameters.Add("@FilePath", SqlDbType.VarChar, 200).Value = objDocument.FilePath;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
        public void AddEditDel_OrgDocument(DO_ProfileDocuments objDocument, OrgDocument flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelOrgContextDocument", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@DocId", SqlDbType.BigInt).Value = objDocument.DocId;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDocument.intAddedBy;
            cmd.Parameters.Add("@FilePath", SqlDbType.VarChar, 200).Value = objDocument.FilePath;
            cmd.Parameters.Add("@DocumentTitle", SqlDbType.VarChar, 200).Value = objDocument.DocTitle;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objDocument.ModifiedBy;
            cmd.Parameters.Add("@intDocumentTypeID", SqlDbType.Int).Value = objDocument.intDocumentTypeID;
            cmd.Parameters.Add("@strAuthors", SqlDbType.VarChar, 255).Value = objDocument.strAuthors;
            cmd.Parameters.Add("@IsDocsSale", SqlDbType.VarChar, 2).Value = objDocument.IsDocsSale;
            cmd.Parameters.Add("@IsDocsDownload", SqlDbType.VarChar, 2).Value = objDocument.IsDocsDownload;
            cmd.Parameters.Add("@intDocsSee", SqlDbType.VarChar, 200).Value = objDocument.intDocsSee;
            cmd.Parameters.Add("@Price", SqlDbType.Float).Value = objDocument.Price;
            cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objDocument.intSubjectCategoryId;
            cmd.Parameters.Add("@strDocName", SqlDbType.VarChar, 500).Value = objDocument.strDocName;
            objDocument.DocId = Convert.ToInt32(cmd.ExecuteScalar());

            co.CloseConnection(conn);

        }
        public void AddEditDel_OrgSubjCategoryDetails(DO_ProfileDocuments objcategory, DA_ProfileDocuments.OrgDocumenTemp flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelOrgContextDocument", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objcategory.intSubjectCategoryId;
            //  cmd.Parameters.Add("@RegistrationId", SqlDbType.VarChar, 200).Value = objcategory.RegistrationId;
            cmd.Parameters.Add("@AddedBy", SqlDbType.VarChar, 200).Value = objcategory.AddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objcategory.ModifiedBy;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }
        public DataTable GetOrgDataTable(DO_ProfileDocuments objcategory, DA_ProfileDocuments.OrgDocumenTemp flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelOrgContextDocument", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@DocId", SqlDbType.Int).Value = objcategory.DocId;
            da.SelectCommand.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objcategory.intSubjectCategoryId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcategory.AddedBy;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }


        public void AddEditDel_GroupOrgDocument(DO_ProfileDocuments objDocument, GropOrgDocument flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelOrgGroupDocument", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            //cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objDocument.RegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = objDocument.intGroupId;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDocument.intAddedBy;
            cmd.Parameters.Add("@strDocTitle", SqlDbType.VarChar, 200).Value = objDocument.DocTitle;
            cmd.Parameters.Add("@FilePath", SqlDbType.VarChar, 200).Value = objDocument.FilePath;
            cmd.Parameters.Add("@StrDocsDetails", SqlDbType.VarChar, 200).Value = objDocument.StrDocsDetails;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objDocument.ModifiedBy;
            cmd.Parameters.Add("@IsDocsSale", SqlDbType.VarChar, 2).Value = objDocument.IsDocsSale;
            cmd.Parameters.Add("@IsDocsDownload", SqlDbType.VarChar, 2).Value = objDocument.IsDocsDownload;
            cmd.Parameters.Add("@Price", SqlDbType.Float).Value = objDocument.Price;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objDocument.IpAddress;
            cmd.Parameters.Add("@IsFolder", SqlDbType.VarChar, 20).Value = objDocument.IsFolder;
            cmd.Parameters.Add("@intUploadDocId", SqlDbType.Int).Value = objDocument.UploadDocId;
            cmd.Parameters.Add("@strFolderName", SqlDbType.VarChar, 100).Value = objDocument.strFolderName;
            cmd.Parameters.Add("@intFolderId", SqlDbType.Int).Value = objDocument.intFolderId;
            cmd.Parameters.Add("@intParentId", SqlDbType.Int).Value = objDocument.intParentId;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = objDocument.OrgId;
            cmd.Parameters.Add("@intGroupDocId", SqlDbType.Int).Value = objDocument.intGroupDocId;
            cmd.Parameters.Add("@strAuthors", SqlDbType.VarChar, 200).Value = objDocument.strAuthors;
            cmd.Parameters.Add("@intDocsSee", SqlDbType.VarChar,20).Value = objDocument.intDocsSee;
            cmd.Parameters.Add("@intDocumentTypeID", SqlDbType.Int).Value = objDocument.intDocumentTypeID;
            cmd.Parameters.Add("@strDocName", SqlDbType.VarChar, 500).Value = objDocument.strDocName;
            cmd.Parameters.Add("@strIsPublic", SqlDbType.VarChar, 500).Value = objDocument.strIsPublic;
            cmd.Parameters.Add("@strFolderDescription", SqlDbType.VarChar, 500).Value = objDocument.strFolderDescription;
            objDocument.DocId = Convert.ToInt32(cmd.ExecuteScalar());

            co.CloseConnection(conn);

        }
        public DataTable GetGroupOrgDocumetDataTable(DO_ProfileDocuments objcategory, DA_ProfileDocuments.GropOrgDocument flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelOrgGroupDocument", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intUploadDocId ", SqlDbType.Int).Value = objcategory.UploadDocId;
            da.SelectCommand.Parameters.Add("@intFolderId ", SqlDbType.Int).Value = objcategory.intFolderId;
            da.SelectCommand.Parameters.Add("@intParentId ", SqlDbType.Int).Value = objcategory.intParentId;
            da.SelectCommand.Parameters.Add("@intGroupId ", SqlDbType.Int).Value = objcategory.intGroupId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcategory.AddedBy;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.BigInt).Value = objcategory.OrgId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
        public void AddEditDel_GroupOrgUploadDocument(DO_ProfileDocuments objDocument, OrgUploadDocument flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelGroupOrgUploadDocument", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intGroupDocId", SqlDbType.BigInt).Value = objDocument.intGroupDocId;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDocument.AddedBy;
            cmd.Parameters.Add("@FilePath", SqlDbType.VarChar, 200).Value = objDocument.FilePath;
            cmd.Parameters.Add("@DocumentTitle", SqlDbType.VarChar, 200).Value = objDocument.DocTitle;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objDocument.ModifiedBy;
            cmd.Parameters.Add("@intDocumentTypeID", SqlDbType.Int).Value = objDocument.intDocumentTypeID;
            cmd.Parameters.Add("@strAuthors", SqlDbType.VarChar, 255).Value = objDocument.strAuthors;
            cmd.Parameters.Add("@IsDocsSale", SqlDbType.VarChar, 2).Value = objDocument.IsDocsSale;
            cmd.Parameters.Add("@IsDocsDownload", SqlDbType.VarChar, 2).Value = objDocument.IsDocsDownload;
            cmd.Parameters.Add("@intDocsSee", SqlDbType.VarChar, 200).Value = objDocument.intDocsSee;
            cmd.Parameters.Add("@Price", SqlDbType.Float).Value = objDocument.Price;
            cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objDocument.intSubjectCategoryId;
            cmd.Parameters.Add("@strDocName", SqlDbType.VarChar, 500).Value = objDocument.strDocName;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = objDocument.intGroupId;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = objDocument.OrgId;
            objDocument.DocId = Convert.ToInt32(cmd.ExecuteScalar());

            co.CloseConnection(conn);

        }
        public DataTable GetGroupOrgUploadTable(DO_ProfileDocuments objcategory, DA_ProfileDocuments.OrgUploadDocument flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelGroupOrgUploadDocument", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intGroupDocId", SqlDbType.Int).Value = objcategory.intGroupDocId;
            da.SelectCommand.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objcategory.intSubjectCategoryId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcategory.AddedBy;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.BigInt).Value = objcategory.intGroupId;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.BigInt).Value = objcategory.OrgId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        #endregion




    }
}