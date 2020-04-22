
/// <summary>
/// Summary description for DO_Case
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_BlogLikeShare
    {
        public DO_BlogLikeShare()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public int CitationId { get; set; }
        public string ContentTitle { get; set; }
        public int ContentTypeId { get; set; }
        public int Caseid { get; set; }
        public int AddedBy { get; set; }


        public int Juridictionid { get; set; }
        public int CitedById { get; set; }
        public int JudgeNameId { get; set; }
        public int CitesId { get; set; }

        public int intBlogLikeShareId { get; set; }
        public string strRepLiShStatus { get; set; }
        public int intCommentID { get; set; }
        public int intRegistrationId { get; set; }
        public int intAddedBy { get; set; }
        public string strIpAddress { get; set; }
        public string strInviteeShare { get; set; }
        public string strLink { get; set; }
        public string strMessage { get; set; }

        public string intTagType { get; set; }
        public int intBlogId { get; set; }
        public int intBlogHeadingLikeId { get; set; }
        public string strBlogTitle { get; set; }

    }
}