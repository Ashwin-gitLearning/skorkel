using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_Scrl_UserForumPosting
/// </summary>
namespace DA_SKORKEL
{
    public class DO_Scrl_UserForumPosting
    {
        public DO_Scrl_UserForumPosting()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int intForumPostingId { get; set; }
        public int intForumReplyLikeShareId { get; set; }
        public int intRegistrationId { get; set; }
        public int intGroupId { get; set; }
        public string strTitle { get; set; }
        public string strDescription { get; set; }
        public string chkPrivateForum { get; set; }
        public string inviteMembers { get; set; }
        public string chkNotify { get; set; }
        public DateTime dtAddedOn { get; set; }
        public DateTime dtModifiedOn { get; set; }
        public int intAddedBy { get; set; }
        public int intModifiedBy { get; set; }
        public string strIpAddress { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        public string strComment { get; set; }
        public string strRepLiShStatus { get; set; }
        public int ForumReplyLikeShareId { get; set; }
        public int intSharedPostingId { get; set; }
        public string strCommentUserId { get; set; }

        public String strMessage { get; set; }
        public String strLink { get; set; }
        public String strInvitee { get; set; }
        public String strFriendList { get; set; }
        public int OrgId { get; set; }

        public string strPhotoPath { get; set; }
    }
}