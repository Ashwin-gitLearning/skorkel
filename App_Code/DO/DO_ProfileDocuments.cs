using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_ProfileDocuments
/// </summary>
namespace DA_SKORKEL
{
    public class DO_ProfileDocuments
    {
        public DO_ProfileDocuments()
        {
            //
            // TODO: Add constructor logic here

        }
        #region Properties
        public int DocId { get; set; }
        public int StudentId { get; set; }
        public string DocTitle { get; set; }
        public string FilePath { get; set; }
        public DateTime AddedOn { get; set; }
        public int MicroTag { get; set; }
        public int AddedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string IpAddress { get; set; }
        public int intDocOutId { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        public int intDocumentTypeID { get; set; }
        public string strAuthors { get; set; }
        public string IsDocsSale { get; set; }
        public string IsDocsDownload { get; set; }
        public string intDocsSee { get; set; }
        public int intAddedBy { get; set; }
        public double Price { get; set; }
        public int SubjCategoryId { get; set; }
        public int RegistrationId { get; set; }
        public int CategoryID { get; set; }
        public int intSubjectCategoryId { get; set; }
        public string strDocName { get; set; }
        public int intGroupId { get; set; }
        public string StrDocsDetails { get; set; }
        public string IsFolder { get; set; }
        public int UploadDocId { get; set; }
        public string strFolderName { get; set; }
        public int intFolderId { get; set; }
        public int intParentId { get; set; }
        public string strDescrition { get; set; }
        public int intGroupDocId { get; set; }
        public int OrgId { get; set; }
        public int FriendId { get; set; }
        public string strIsPublic { get; set; }
        public string strFolderDescription { get; set; }
       // public string strDocName { get; set; }

        #endregion
    }
}