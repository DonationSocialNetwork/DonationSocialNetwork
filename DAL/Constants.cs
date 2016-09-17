using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSN.DAL
{
    public static class Constants
    {
        public static class UserType
        {
            public const string Organization = "organization";
            public const string Individual = "individual";
        }
        public static class Approval
        {
            public const string ApproverId = "approver_id";

            public const string PendingApproval = "Pending Approval";

            public const string Approved = "Approved";

            public const string PendingApprovalCode = "P";

            public const string ApprovedCode = "A";
        }
        

        public static class SPROC
        {
            public const string GetUsersNetwork = "GetUsersNetwork";

            public const string GetNetwork = "GetNetwork";

            public const string GetApprovals = "GetApprovals";

            public const string Approve = "Approve";

            public const string GetApprovedNeedsDonationStatus = "GetApprovedNeedsDonationStatus";

            public const string GetUserType = "GetUserType";
        }

        public static class Parameters
        {
            public const string NeedId = "needId";
        }

        public static class ConnectionStrings
        {
            public const string DSNDb = "DSNDb";

            public static string StringNotFound = "Connection string not found.";
        }

        public static class Individual
        {
            public const string Id = "Id";

            public const string Name = "Name";

            public const string Title = "Title";

            public const string Organization = "Organization";
        }

        public static class Organization
        {
            public const string Id = "Id";

            public const string Name = "Name";

            public const string Description = "Description";
        }

        public static class Need
        {
            public const string Id = "Id";

            public const string Title = "Title";

            public const string ActualAmount = "Actual Amount";

            public const string User_Id = "User_Id";

            public const string Approval_Status = "Approval_Status";
            public const string BalanceAmount = "Balance Amount";
        }
    }
}
