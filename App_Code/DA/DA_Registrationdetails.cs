using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;



/// <summary>
/// Summary description for DA_Registrationdetails
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_Registrationdetails
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        private SqlCommand cmd;

        public enum RegistrationDetails
        {
            Add = 1, Update = 2, Delete = 3, AllReg = 4, SingleRecord = 5, DisableUser = 6, SearchText = 7, InstituteNameStuProfList = 8, GetApprovedStudentByInstitute = 9,
            InstituteStuProfAdd = 10,
            DeptList = 11// EmailVerification = 8
                , Update_InstituteId = 12, InsertInApprovalTable = 13, /*GetApprovedUserByLF = 14*/ UpdateNotifcationCount = 14, /*ApproveLawyer = 15*/ IsDuplicateInstitute = 15, Update_LFId = 16, InsertInLFApprovalTable = 17, ApproveStudent = 18,
            UpdateConnection = 19, GetJoinedOrganizations = 20, DeleteProfilePicture = 21, UpdateImg = 22, UserRecordByMail = 23, isScorePrivate = 24, saveChatbotQuestion = 25
        };
        public enum MyProfileConnections
        {
            AddFollow = 1, UpdateFolow = 2, GetFollowStatus = 3, GetFollowStatusByAddedBy = 4, GetIsConnByInviteduserId = 5
        }
        public enum OrgConnections
        {
            AddFollow = 1, UpdateFolow = 2, GetFollowStatus = 3, GetFollowStatusByAddedBy = 4, GetIsConnByInviteduserId = 5
        }
        public enum OrganizationJoin
        {
            AddJoin = 1, UpdateJoin = 2, GetJoinStatus = 3, GetJoinStatusByAddedBy = 4, GetIsConnByInviteduserId = 5, GetOrganizationName = 6
        }
        public enum OrgRegistrationDetails
        {
            Add = 1, Update = 2, Delete = 3, AllReg = 4, SingleRecord = 5, DisableUser = 6, SearchText = 7, InstituteNameStuProfList = 8, GetApprovedStudentByInstitute = 9,
            InstituteStuProfAdd = 10,
            DeptList = 11// EmailVerification = 8
                , Update_InstituteId = 12, InsertInApprovalTable = 13, /*GetApprovedUserByLF = 14*/ UpdateNotifcationCount = 14, ApproveLawyer = 15, Update_LFId = 16, InsertInLFApprovalTable = 17, ApproveStudent = 18,
            UpdateConnection = 19, GetJoinUnjoinStatus = 20, GetJoinedMembers = 22
        };

        public DA_Registrationdetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region DataTable

        public DataTable GetDataTable(DO_Registrationdetails objRegistration, RegistrationDetails flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_InSertRegistrationSP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@RegistrationId", SqlDbType.Int).Value = objRegistration.RegistrationId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            da.SelectCommand.Parameters.Add("@UserTypeId", SqlDbType.Int).Value = objRegistration.UserTypeId;
            da.SelectCommand.Parameters.Add("@FirstName", SqlDbType.VarChar, 2000).Value = objRegistration.FirstName;
            da.SelectCommand.Parameters.Add("@RollNo", SqlDbType.Int).Value = objRegistration.RollNo;
            da.SelectCommand.Parameters.Add("@Batch", SqlDbType.VarChar, 200).Value = objRegistration.Batch;
            da.SelectCommand.Parameters.Add("@PoliticalView", SqlDbType.VarChar, 200).Value = objRegistration.PoliticalView;
            da.SelectCommand.Parameters.Add("@Languages", SqlDbType.VarChar, 200).Value = objRegistration.Languages;
            da.SelectCommand.Parameters.Add("@NonAchedemicInterest", SqlDbType.VarChar, 200).Value = objRegistration.NonAchedemicInterest;
            da.SelectCommand.Parameters.Add("@OtherPer", SqlDbType.VarChar, 200).Value = objRegistration.OtherPersonal;
            da.SelectCommand.Parameters.Add("@Phone", SqlDbType.BigInt).Value = objRegistration.Phone;
            da.SelectCommand.Parameters.Add("@Mobile", SqlDbType.BigInt).Value = objRegistration.Mobile;
            da.SelectCommand.Parameters.Add("@Designation", SqlDbType.VarChar, 200).Value = objRegistration.Designation;
            da.SelectCommand.Parameters.Add("@University", SqlDbType.VarChar, 200).Value = objRegistration.University;
            da.SelectCommand.Parameters.Add("@Alumni", SqlDbType.VarChar, 200).Value = objRegistration.Alunmi;
            da.SelectCommand.Parameters.Add("@CVPath", SqlDbType.VarChar, 200).Value = objRegistration.CVPath;
            da.SelectCommand.Parameters.Add("@AkineTostatic", SqlDbType.VarChar, 200).Value = objRegistration.AkineTostatic;
            da.SelectCommand.Parameters.Add("@WorkedAt", SqlDbType.VarChar, 200).Value = objRegistration.WorkedAt;
            da.SelectCommand.Parameters.Add("@ProfAchievements", SqlDbType.VarChar, 200).Value = objRegistration.ProfAchievements;
            da.SelectCommand.Parameters.Add("@Publishings", SqlDbType.VarChar, 200).Value = objRegistration.Publishings;
            da.SelectCommand.Parameters.Add("@SubOfInterest", SqlDbType.VarChar, 200).Value = objRegistration.SubOfInterest;
            da.SelectCommand.Parameters.Add("@Goals", SqlDbType.VarChar, 200).Value = objRegistration.Goals;
            da.SelectCommand.Parameters.Add("@ExtraCredit", SqlDbType.VarChar, 200).Value = objRegistration.ExtraCredit;
            da.SelectCommand.Parameters.Add("@Leadership", SqlDbType.VarChar, 200).Value = objRegistration.Leadership;
            da.SelectCommand.Parameters.Add("@SubjectOfTought", SqlDbType.VarChar, 200).Value = objRegistration.SubOfTought;
            da.SelectCommand.Parameters.Add("@Legaldept", SqlDbType.VarChar, 200).Value = objRegistration.LegalDept;
            da.SelectCommand.Parameters.Add("@OtherLegalProject", SqlDbType.VarChar, 200).Value = objRegistration.OtherLegalProj;
            da.SelectCommand.Parameters.Add("@Photopath", SqlDbType.VarChar, 200).Value = objRegistration.PhotoPath;
            da.SelectCommand.Parameters.Add("@ParentId", SqlDbType.BigInt).Value = objRegistration.ParentId;
            da.SelectCommand.Parameters.Add("@DeptId", SqlDbType.BigInt).Value = objRegistration.DeptId;
            da.SelectCommand.Parameters.Add("@InstituteId", SqlDbType.Int).Value = objRegistration.InstituteId; //Added on 1 April 20 13
            da.SelectCommand.Parameters.Add("@LawFirmId", SqlDbType.Int).Value = objRegistration.LawFirmId; //Added on 3 April 20 13
            da.SelectCommand.Parameters.Add("@intNotificationCount", SqlDbType.Int).Value = objRegistration.intNotificationCount;
            da.SelectCommand.Parameters.Add("@InstituteName", SqlDbType.VarChar, 200).Value = objRegistration.InstituteName;
            da.SelectCommand.Parameters.Add("@NotificationDateTime", SqlDbType.DateTime).Value = objRegistration.notificationdatetime;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public DataTable GetDataTableRecord(DO_Registrationdetails objRegistration, RegistrationDetails flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_InSertRegistrationSP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar, 100).Value = objRegistration.UserName;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public DataTable GetUserSearch(int CurrentPage, int CurrentPageSize, string prefixText)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_UserSearch", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@txtSearch", SqlDbType.VarChar, 2000).Value = prefixText;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = CurrentPage;
            da.SelectCommand.Parameters.Add("@pagesize", SqlDbType.Int).Value = CurrentPageSize;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
        #endregion


        public DataSet GetDataSet(DO_Registrationdetails objRegistration, RegistrationDetails flag)
        {
            DataSet dt = new DataSet();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_InSertRegistrationSP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@RegistrationId", SqlDbType.Int).Value = objRegistration.RegistrationId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            da.SelectCommand.Parameters.Add("@RollNo", SqlDbType.Int).Value = objRegistration.RollNo;
            da.SelectCommand.Parameters.Add("@Batch", SqlDbType.VarChar, 200).Value = objRegistration.Batch;
            da.SelectCommand.Parameters.Add("@PoliticalView", SqlDbType.VarChar, 200).Value = objRegistration.PoliticalView;
            da.SelectCommand.Parameters.Add("@Languages", SqlDbType.VarChar, 200).Value = objRegistration.Languages;
            da.SelectCommand.Parameters.Add("@NonAchedemicInterest", SqlDbType.VarChar, 200).Value = objRegistration.NonAchedemicInterest;
            da.SelectCommand.Parameters.Add("@OtherPer", SqlDbType.VarChar, 200).Value = objRegistration.OtherPersonal;
            da.SelectCommand.Parameters.Add("@Phone", SqlDbType.BigInt).Value = objRegistration.Phone;
            da.SelectCommand.Parameters.Add("@Mobile", SqlDbType.BigInt).Value = objRegistration.Mobile;
            da.SelectCommand.Parameters.Add("@Designation", SqlDbType.VarChar, 200).Value = objRegistration.Designation;
            da.SelectCommand.Parameters.Add("@University", SqlDbType.VarChar, 200).Value = objRegistration.University;
            da.SelectCommand.Parameters.Add("@Alumni", SqlDbType.VarChar, 200).Value = objRegistration.Alunmi;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        #region Insert,Delete,Update
        public void AddEditDel_RegistrationDetails(DO_Registrationdetails objRegistration, RegistrationDetails flag)
        {
            conn = co.GetConnection();
            //conn.Open();
            cmd = new SqlCommand("scrl_InSertRegistrationSP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@RegistrationId", SqlDbType.Int).Value = objRegistration.RegistrationId;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 200).Value = objRegistration.FirstName;
            cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar, 200).Value = objRegistration.MiddleName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 200).Value = objRegistration.LastName;
            cmd.Parameters.Add("@Sex", SqlDbType.VarChar, 200).Value = objRegistration.Sex;
            cmd.Parameters.Add("@InstituteName", SqlDbType.VarChar, 200).Value = objRegistration.InstituteName;
            cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 200).Value = objRegistration.UserName;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 200).Value = objRegistration.Password;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar, 200).Value = objRegistration.Address;
            cmd.Parameters.Add("@intPinCode", SqlDbType.Int).Value = objRegistration.Pin;
            cmd.Parameters.Add("@City", SqlDbType.VarChar, 200).Value = objRegistration.City;
            cmd.Parameters.Add("@State", SqlDbType.VarChar, 200).Value = objRegistration.State;
            cmd.Parameters.Add("@Country", SqlDbType.VarChar, 200).Value = objRegistration.Country;
            cmd.Parameters.Add("@RollNo", SqlDbType.Int).Value = objRegistration.RollNo;
            cmd.Parameters.Add("@Batch", SqlDbType.VarChar, 200).Value = objRegistration.Batch;
            cmd.Parameters.Add("@PoliticalView", SqlDbType.VarChar, 200).Value = objRegistration.PoliticalView;
            cmd.Parameters.Add("@Languages", SqlDbType.VarChar, 200).Value = objRegistration.Languages;
            cmd.Parameters.Add("@NonAchedemicInterest", SqlDbType.VarChar, 200).Value = objRegistration.NonAchedemicInterest;
            cmd.Parameters.Add("@OtherPer", SqlDbType.VarChar, 200).Value = objRegistration.OtherPersonal;
            cmd.Parameters.Add("@UserTypeID", SqlDbType.Int).Value = objRegistration.UserTypeId;
            // cmd.Parameters.Add("@AddedOn", SqlDbType.DateTime).Value = objRegistration.AddedOn;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 200).Value = objRegistration.IpAddress;
            //cmd.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = objRegistration.ModifiedOn;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = objRegistration.ModifiedBy;
            cmd.Parameters.Add("@Phone", SqlDbType.BigInt).Value = objRegistration.Phone;
            cmd.Parameters.Add("@Mobile", SqlDbType.BigInt).Value = objRegistration.Mobile;
            cmd.Parameters.Add("@Designation", SqlDbType.VarChar, 200).Value = objRegistration.Designation;
            cmd.Parameters.Add("@University", SqlDbType.VarChar, 200).Value = objRegistration.University;
            cmd.Parameters.Add("@Alumni", SqlDbType.VarChar, 200).Value = objRegistration.Alunmi;
            cmd.Parameters.Add("@CVPath", SqlDbType.VarChar, 200).Value = objRegistration.CVPath;
            cmd.Parameters.Add("@AkineTostatic", SqlDbType.VarChar, 200).Value = objRegistration.AkineTostatic;
            cmd.Parameters.Add("@WorkedAt", SqlDbType.VarChar, 200).Value = objRegistration.WorkedAt;
            cmd.Parameters.Add("@ProfAchievements", SqlDbType.VarChar, 200).Value = objRegistration.ProfAchievements;
            cmd.Parameters.Add("@Publishings", SqlDbType.VarChar, 200).Value = objRegistration.Publishings;
            cmd.Parameters.Add("@SubOfInterest", SqlDbType.VarChar, 200).Value = objRegistration.SubOfInterest;
            cmd.Parameters.Add("@Goals", SqlDbType.VarChar, 200).Value = objRegistration.Goals;
            cmd.Parameters.Add("@ExtraCredit", SqlDbType.VarChar, 200).Value = objRegistration.ExtraCredit;
            cmd.Parameters.Add("@Leadership", SqlDbType.VarChar, 200).Value = objRegistration.Leadership;
            cmd.Parameters.Add("@Photopath", SqlDbType.VarChar, 200).Value = objRegistration.PhotoPath;
            cmd.Parameters.Add("@ParentId", SqlDbType.BigInt).Value = objRegistration.ParentId;
            cmd.Parameters.Add("@DeptId", SqlDbType.BigInt).Value = objRegistration.DeptId;
            cmd.Parameters.Add("@InstituteId", SqlDbType.Int).Value = objRegistration.InstituteId;
            cmd.Parameters.Add("@LawFirmId", SqlDbType.Int).Value = objRegistration.LawFirmId; //Added on 3 April 20 13

            cmd.Parameters.Add("@intLawRelated", SqlDbType.Int).Value = objRegistration.intLawRelated;
            cmd.Parameters.Add("@vchrRegistartionType", SqlDbType.VarChar, 2).Value = objRegistration.vchrRegistartionType;
            cmd.Parameters.Add("@strOthers", SqlDbType.VarChar, 200).Value = objRegistration.strOthers;
            cmd.Parameters.Add("@intNotificationCount", SqlDbType.Int).Value = objRegistration.intNotificationCount;

            objRegistration.RegOutId = Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);
        }

        public void AddEditDel_Profile(DO_Registrationdetails objRegistration, RegistrationDetails flag)
        {
            conn = co.GetConnection();
            //conn.Open();
            cmd = new SqlCommand("Scrl_AddEditDelProfile", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@RegistrationId", SqlDbType.BigInt).Value = objRegistration.RegistrationId;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 200).Value = objRegistration.FirstName;
            cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar, 200).Value = objRegistration.MiddleName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 200).Value = objRegistration.LastName;
            cmd.Parameters.Add("@Sex", SqlDbType.VarChar, 200).Value = objRegistration.Sex;
            //cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 200).Value = objRegistration.UserName;
            //cmd.Parameters.Add("@Password", SqlDbType.VarChar, 200).Value = objRegistration.Password;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar, 200).Value = objRegistration.Address;
            cmd.Parameters.Add("@intPinCode", SqlDbType.VarChar, 200).Value = objRegistration.Pin;
            cmd.Parameters.Add("@City", SqlDbType.VarChar, 200).Value = objRegistration.City;
            cmd.Parameters.Add("@State", SqlDbType.VarChar, 200).Value = objRegistration.State;
            cmd.Parameters.Add("@Country", SqlDbType.VarChar, 200).Value = objRegistration.Country;
            cmd.Parameters.Add("@RollNo", SqlDbType.Int).Value = objRegistration.RollNo;
            cmd.Parameters.Add("@Batch", SqlDbType.VarChar, 200).Value = objRegistration.Batch;
            cmd.Parameters.Add("@PoliticalView", SqlDbType.VarChar, 200).Value = objRegistration.PoliticalView;
            cmd.Parameters.Add("@Languages", SqlDbType.VarChar, 200).Value = objRegistration.Languages;
            cmd.Parameters.Add("@NonAchedemicInterest", SqlDbType.VarChar, 200).Value = objRegistration.NonAchedemicInterest;
            cmd.Parameters.Add("@OtherPer", SqlDbType.VarChar, 200).Value = objRegistration.OtherPersonal;
            cmd.Parameters.Add("@UserTypeID", SqlDbType.Int).Value = objRegistration.UserTypeId;
            // cmd.Parameters.Add("@AddedOn", SqlDbType.DateTime).Value = objRegistration.AddedOn;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 200).Value = objRegistration.IpAddress;
            //cmd.Parameters.Add("@ModifiedOn", SqlDbType.DateTime).Value = objRegistration.ModifiedOn;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = objRegistration.ModifiedBy;
            cmd.Parameters.Add("@InstituteName", SqlDbType.VarChar, 200).Value = objRegistration.InstituteName;
            cmd.Parameters.Add("@Phone", SqlDbType.BigInt).Value = objRegistration.Phone;
            cmd.Parameters.Add("@Mobile", SqlDbType.BigInt).Value = objRegistration.Mobile;
            cmd.Parameters.Add("@Designation", SqlDbType.VarChar, 200).Value = objRegistration.Designation;
            cmd.Parameters.Add("@University", SqlDbType.VarChar, 200).Value = objRegistration.University;
            cmd.Parameters.Add("@Alumni", SqlDbType.VarChar, 200).Value = objRegistration.Alunmi;
            cmd.Parameters.Add("@CVPath", SqlDbType.VarChar, 200).Value = objRegistration.CVPath;
            cmd.Parameters.Add("@AkineTostatic", SqlDbType.VarChar, 200).Value = objRegistration.AkineTostatic;
            cmd.Parameters.Add("@WorkedAt", SqlDbType.VarChar, 200).Value = objRegistration.WorkedAt;
            cmd.Parameters.Add("@ProfAchievements", SqlDbType.VarChar, 200).Value = objRegistration.ProfAchievements;
            cmd.Parameters.Add("@Publishings", SqlDbType.VarChar, 200).Value = objRegistration.Publishings;
            cmd.Parameters.Add("@SubOfInterest", SqlDbType.VarChar, 200).Value = objRegistration.SubOfInterest;
            cmd.Parameters.Add("@Goals", SqlDbType.VarChar, 200).Value = objRegistration.Goals;
            cmd.Parameters.Add("@ExtraCredit", SqlDbType.VarChar, 200).Value = objRegistration.ExtraCredit;
            cmd.Parameters.Add("@Leadership", SqlDbType.VarChar, 200).Value = objRegistration.Leadership;
            cmd.Parameters.Add("@Legaldept", SqlDbType.VarChar, 200).Value = objRegistration.LegalDept;
            cmd.Parameters.Add("@OtherLegalProject", SqlDbType.VarChar, 200).Value = objRegistration.OtherLegalProj;
            cmd.Parameters.Add("@SubjectOfTought", SqlDbType.VarChar, 200).Value = objRegistration.SubOfTought;
            cmd.Parameters.Add("@ApproveChildTblId", SqlDbType.Int).Value = objRegistration.ApproveChildTblId;
            cmd.Parameters.Add("@intInstituteId", SqlDbType.Int).Value = objRegistration.InstituteId;
            cmd.Parameters.Add("@intLawFirmId", SqlDbType.Int).Value = objRegistration.LawFirmId;
            cmd.Parameters.Add("@IsApproved", SqlDbType.Int).Value = objRegistration.IsApproved;
            cmd.Parameters.Add("@intLawRelated", SqlDbType.Int).Value = objRegistration.intLawRelated;
            cmd.Parameters.Add("@Photopath", SqlDbType.VarChar, 200).Value = objRegistration.PhotoPath;
            cmd.Parameters.Add("@vchrRegistartionType", SqlDbType.VarChar, 2).Value = objRegistration.vchrRegistartionType;
            cmd.Parameters.Add("@strOthers", SqlDbType.VarChar, 200).Value = objRegistration.strOthers;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        #endregion

        public void AddEditDel_Request(DO_Registrationdetails objRegistration, RegistrationDetails flag)
        {
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelRequests", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.BigInt).Value = objRegistration.RegistrationId;
            cmd.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = objRegistration.InvitedUserId;
            cmd.Parameters.Add("@intRequestInvitaionId", SqlDbType.Int).Value = objRegistration.intRequestInvitaionId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = objRegistration.IpAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetExistsRequest(DO_Registrationdetails objRegistration, RegistrationDetails flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelRequests", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.RegistrationId;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = objRegistration.InvitedUserId;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public DataTable GetStudentToApprove(DO_Registrationdetails objRegistration, RegistrationDetails flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelProfile", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            //   da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.RegistrationId;
            da.SelectCommand.Parameters.Add("@intInstituteId", SqlDbType.Int).Value = objRegistration.InstituteId;
            da.SelectCommand.Parameters.Add("@intLawFirmId", SqlDbType.Int).Value = objRegistration.LawFirmId;
            da.SelectCommand.Parameters.Add("@IsApproved", SqlDbType.Int).Value = objRegistration.IsApproved;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }


        public void AddEditDel_FollowAll(DO_Registrationdetails objRegistration, MyProfileConnections flag)
        {
            conn = co.GetConnection();
            //conn.Open();
            cmd = new SqlCommand("Scrl_AddEditFollowAll", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.ConnectRegistrationId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objRegistration.ModifiedBy;
            cmd.Parameters.Add("@intFollowId", SqlDbType.Int).Value = objRegistration.FollowId;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = objRegistration.IpAddress;
            objRegistration.FollowId = Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);
        }
        public DataTable GetFolowStatus(DO_Registrationdetails objRegistration, MyProfileConnections flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditFollowAll", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.ConnectRegistrationId;
            da.SelectCommand.Parameters.Add("@intFollowId", SqlDbType.Int).Value = objRegistration.FollowId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = objRegistration.intInvitedUserId;
            //   da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.RegistrationId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        #region Organization Follow status


        public void AddEditDel_OrgRequest(DO_Registrationdetails objRegistration, OrgRegistrationDetails flag)
        {
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditOrgDelRequests", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.BigInt).Value = objRegistration.RegistrationId;
            cmd.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = objRegistration.InvitedUserId;
            cmd.Parameters.Add("@intRequestInvitaionId", SqlDbType.Int).Value = objRegistration.intRequestInvitaionId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = objRegistration.IpAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetOrgExistsRequest(DO_Registrationdetails objRegistration, OrgRegistrationDetails flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditOrgDelRequests", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.RegistrationId;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = objRegistration.InvitedUserId;
            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = objRegistration.intGroupId;
            da.SelectCommand.Parameters.Add("@intOrgnisationID", SqlDbType.Int).Value = objRegistration.intOrgnisationID;
            da.SelectCommand.Parameters.Add("@strSearch", SqlDbType.VarChar, 500).Value = objRegistration.strSearch;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
        public void AddEditDel_OrgFollowAll(DO_Registrationdetails objRegistration, OrgConnections flag)
        {
            conn = co.GetConnection();
            //conn.Open();
            cmd = new SqlCommand("Scrl_OrgAddEditFollow", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.ConnectRegistrationId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objRegistration.ModifiedBy;
            cmd.Parameters.Add("@intFollowId", SqlDbType.Int).Value = objRegistration.FollowId;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = objRegistration.IpAddress;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = objRegistration.OrgId;
            objRegistration.FollowId = Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);
        }

        public DataTable GetOrgFolowStatus(DO_Registrationdetails objRegistration, OrgConnections flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_OrgAddEditFollow", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.ConnectRegistrationId;
            da.SelectCommand.Parameters.Add("@intFollowId", SqlDbType.Int).Value = objRegistration.FollowId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = objRegistration.intInvitedUserId;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.Int).Value = objRegistration.OrgId;
            //   da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.RegistrationId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
        #endregion

        #region Organization Join status
        public void AddEditDel_OrgJoin(DO_Registrationdetails objRegistration, OrganizationJoin flag)
        {
            conn = co.GetConnection();
            //conn.Open();
            cmd = new SqlCommand("Scrl_OrgAddEditOrgJoin", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.ConnectRegistrationId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objRegistration.ModifiedBy;
            cmd.Parameters.Add("@intOrgJoinId", SqlDbType.Int).Value = objRegistration.intOrgJoinId;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = objRegistration.IpAddress;
            cmd.Parameters.Add("@OrgId", SqlDbType.VarChar, 50).Value = objRegistration.OrgId;
            objRegistration.FollowId = Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);
        }

        public DataTable GetOrgjoinStatus(DO_Registrationdetails objRegistration, OrganizationJoin flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_OrgAddEditOrgJoin", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.ConnectRegistrationId;
            da.SelectCommand.Parameters.Add("@intOrgJoinId", SqlDbType.Int).Value = objRegistration.intOrgJoinId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = objRegistration.intInvitedUserId;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.Int).Value = objRegistration.OrgId;
            //   da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.RegistrationId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
        public DataTable GetOrgnizationName(DO_Registrationdetails objRegistration, OrganizationJoin flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_OrgAddEditOrgJoin", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.ConnectRegistrationId;
            da.SelectCommand.Parameters.Add("@intOrgJoinId", SqlDbType.Int).Value = objRegistration.intOrgJoinId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objRegistration.AddedBy;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = objRegistration.intInvitedUserId;
            da.SelectCommand.Parameters.Add("@intUserTypeID", SqlDbType.Int).Value = objRegistration.UserTypeId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        #endregion

        #region Added on 13.03.2020:: To check Education detail has been filled by user or not.
        public DataSet GetUserEducationDetailsCount(int RegistrationId, int Flag)
        {
            DataSet ds = new DataSet();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_UserEducationTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = RegistrationId;

            da.Fill(ds);
            co.CloseConnection(conn);
            return ds;
        }

        public DataSet checkScorePulic(DO_Registrationdetails objRegistration, RegistrationDetails flag)
        {
            DataSet ds = new DataSet();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_InSertRegistrationSP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@RegistrationId", SqlDbType.VarChar, 100).Value = objRegistration.RegistrationId;
            da.SelectCommand.Parameters.Add("@isScorePrivate", SqlDbType.Bit, 10).Value = objRegistration.isScorePrivate;
            //For Chatbot Question
            da.SelectCommand.Parameters.Add("@nvchrQuestion", SqlDbType.NVarChar, 4000).Value = objRegistration.Question;
            da.SelectCommand.Parameters.Add("@nvchrType", SqlDbType.NVarChar, 100).Value = objRegistration.Type;

            da.Fill(ds);
            co.CloseConnection(conn);
            return ds;
        }
        #endregion

    }

}
