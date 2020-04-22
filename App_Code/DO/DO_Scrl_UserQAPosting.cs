using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_Scrl_UserQAPosting
/// </summary>
namespace DA_SKORKEL
{
    public class DO_Scrl_UserQAPosting
    {
        public DO_Scrl_UserQAPosting()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int intPostQuestionId { get; set; }
        public string strQuestionDescription { get; set; }
        public string strInvitee { get; set; }
        public int intContextId { get; set; }
        public string strFileName { get; set; }
        public string strFilePath { get; set; }
        public int intAddedBy { get; set; }
        public int intModifiedBy { get; set; }
        public string strIPAddress { get; set; }
        public int intSubjectCategoryId { get; set; }
        public int DocId { get; set; }
        public int PostQuestionId { get; set; }
        public string strRepLiShStatus { get; set; }
        public string strComment { get; set; }
        public int PrvateMessage { get; set; }
        public string strSearch { get; set; }
        public string ID { get; set; }
        public int intQAReplyLikeShareId { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        public string strSharelink { get; set; }
        public string QuestionIdList { get; set; }
        public string MP { get; set; }
    }
}