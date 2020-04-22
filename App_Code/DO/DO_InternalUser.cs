using System;


/// <summary>
/// Summary description for DO_City
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_InternalUser
    {
        public DO_InternalUser()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable Declartion

        public String Condition { get; set; } 
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public bool Sex { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public int ParentId { get; set; }
        public int UserType { get; set; }
        public string CompanyName { get; set; }
        public int CompannyId { get; set; }
        public int AddedBy { get; set; }
        public string IPAddress { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNo { get; set; }
        #endregion



    }


}