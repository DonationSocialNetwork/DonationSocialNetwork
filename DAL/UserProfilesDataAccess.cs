using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSN.Models;

namespace DSN.DAL
{
    public class UserProfilesDataAccess : DataAccessBase
    {
        public NetworkViewModel GetNetwork()
        {
            NetworkViewModel users = new NetworkViewModel
            {
                Individuals = new List<IndividualViewModel>(),
                Organizations = new List<OrganizationViewModel>()
            };
            using (IDataReader reader = ExecuteReader(Constants.SPROC.GetNetwork, DbCommandType.StoredProcedure))
            {
                while (reader.Read())
                {
                    IndividualViewModel individual = new IndividualViewModel
                    {
                        Id = (int) reader[Constants.Individual.Id],
                        Name = (string) reader[Constants.Individual.Name],
                        Title = (string) reader[Constants.Individual.Title],
                        Organisation = (string) reader[Constants.Individual.Organization]
                    };
                    users.Individuals.Add(individual);
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        OrganizationViewModel organization = new OrganizationViewModel
                        {
                            Id = (int) reader[Constants.Organization.Id],
                            Name = (string) reader[Constants.Organization.Name],
                            Description = (string) reader[Constants.Organization.Description]
                        };
                        users.Organizations.Add(organization);
                    }
                }
            }
            return users;
        }

        public NetworkViewModel GetNetwork(int id)
        {
            NetworkViewModel users = new NetworkViewModel
            {
                Individuals = new List<IndividualViewModel>(),
                Organizations = new List<OrganizationViewModel>()
            };
            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter(Constants.Individual.Id, id)
            };
            using (
                IDataReader reader = ExecuteReader(Constants.SPROC.GetUsersNetwork, DbCommandType.StoredProcedure,
                    parameters))
            {
                while (reader.Read())
                {
                    IndividualViewModel individual = new IndividualViewModel
                    {
                        Id = (int) reader[Constants.Individual.Id],
                        Name = (string) reader[Constants.Individual.Name],
                        Title = (string) reader[Constants.Individual.Title],
                        Organisation = (string) reader[Constants.Individual.Organization]
                    };
                    users.Individuals.Add(individual);
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        OrganizationViewModel organization = new OrganizationViewModel
                        {
                            Id = (int) reader[Constants.Organization.Id],
                            Name = (string) reader[Constants.Organization.Name],
                            Description = (string) reader[Constants.Organization.Description]
                        };
                        users.Organizations.Add(organization);
                    }
                }
            }
            return users;
        }

        public List<ApprovalViewModel> GetApprovals(int approverId)
        {
            List<ApprovalViewModel> listOfApprovals = new List<ApprovalViewModel>();
            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter(Constants.Approval.ApproverId, approverId)
            };
            using (
                IDataReader reader = ExecuteReader(Constants.SPROC.GetApprovals, DbCommandType.StoredProcedure,
                    parameters))
            {
                while (reader.Read())
                {
                    ApprovalViewModel approval = new ApprovalViewModel
                    {
                        NeedId = (int) reader[Constants.Need.Id],
                        NeedTitle = (string) reader[Constants.Need.Title],
                        ActualAmount = (int) reader[Constants.Need.ActualAmount],
                        UserId = (int) reader[Constants.Need.User_Id],
                        UserName = (string) reader[Constants.Individual.Name],
                        ApprovalStatus = ((string) reader[Constants.Need.Approval_Status])
                    };
                    listOfApprovals.Add(approval);
                }
            }
            return listOfApprovals;
        }

        public List<ApprovalViewModel> GetApprovedNeedsDonationStatus(int approverId)
        {
            List<ApprovalViewModel> approvedNeedsWithDonationStatus = new List<ApprovalViewModel>();
            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter(Constants.Approval.ApproverId, approverId)
            };

            using (
                IDataReader reader = ExecuteReader(Constants.SPROC.GetApprovedNeedsDonationStatus,
                    DbCommandType.StoredProcedure, parameters))
            {
                while (reader.Read())
                {
                    ApprovalViewModel approval = new ApprovalViewModel
                    {
                        NeedId = (int) reader[Constants.Need.Id],
                        NeedTitle = (string) reader[Constants.Need.Title],
                        ActualAmount = (int) reader[Constants.Need.ActualAmount],
                        UserId = (int) reader[Constants.Need.User_Id],
                        UserName = (string) reader[Constants.Individual.Name],
                        BalanceAmount = (int) reader[Constants.Need.BalanceAmount]
                    };
                    approvedNeedsWithDonationStatus.Add(approval);
                }
            }

            return approvedNeedsWithDonationStatus;
        }

        //To-do: move to Logic layer
        public List<ApprovalViewModel> GetPendingApprovals(int approverId)
        {
            List<ApprovalViewModel> listOfApprovals = GetApprovals(approverId);
            List<ApprovalViewModel> listOfPendingApprovals =
                listOfApprovals.FindAll(x => x.ApprovalStatus.Equals(Constants.Approval.PendingApprovalCode));
            foreach (var item in listOfPendingApprovals)
            {
                item.ApprovalStatus = Constants.Approval.PendingApproval;
            }
            return listOfPendingApprovals;
        }

        //To-do: move to Logic layer
        public List<ApprovalViewModel> GetCompleteApprovals(int approverId)
        {
            List<ApprovalViewModel> listOfApprovals = GetApprovals(approverId);
            List<ApprovalViewModel> listOfApprovedNeeds =
                listOfApprovals.FindAll(x => x.ApprovalStatus.Equals(Constants.Approval.ApprovedCode));
            foreach (var item in listOfApprovedNeeds)
            {
                item.ApprovalStatus = Constants.Approval.Approved;
            }
            return listOfApprovedNeeds;
        }

        public bool Approve(int needId)
        {
            bool isSuccess = false;
            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter(Constants.Parameters.NeedId, needId)
            };
            using (
                IDataReader reader = ExecuteReader(Constants.SPROC.Approve, DbCommandType.StoredProcedure, parameters))
            {
                if (reader.RecordsAffected > 0)
                {
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        //func to return list of expense model view
        public List<NeedViewModel> GetExpenseNeeds(int userId)
        {
            var results = new List<NeedViewModel>();

            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter("userId", userId)
            };
            using (IDataReader reader = ExecuteReader("GetExpenseNeeds", DbCommandType.StoredProcedure, parameters))
            {
                while (reader.Read())
                {
                    NeedViewModel expense = new NeedViewModel
                    {
                        Id = (int) reader["Id"],
                        UserId = (int) reader["User_Id"],
                        Title = (string) reader["Title"],
                        Description = (string) reader["Description"],
                        ActualAmout = (int) reader["Actual Amount"],
                        BalanceAmount = (int) reader["Balance Amount"],
                        DueDate = ((DateTime) reader["Due Date"]).ToString("dd MMMM yyyy")
                    };
                    results.Add(expense);
                }
            }

            return results;
        }

        public void AddDonationRecord(int donorId, int expenseId, int donationAmount, int beneficiaryUserId)
        {
            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter("donorId", donorId),
                new SqlParameter("expenseId", expenseId),
                new SqlParameter("donationAmount", donationAmount),
                new SqlParameter("beneficiaryUserId", beneficiaryUserId)
            };
            int res = ExecuteStoredProcNonQuery("AddDonationRecord", parameters);
        }

        public List<DonationViewModel> GetDonations(int donorId)
        {

            var results = new List<DonationViewModel>();

            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter("donorId", donorId)
            };
            using (IDataReader reader = ExecuteReader("GetDonations", DbCommandType.StoredProcedure, parameters))
            {
                while (reader.Read())
                {
                    DonationViewModel donation = new DonationViewModel
                    {
                        expenseId = (int) reader["expenseId"],

                        expenseName = (string) reader["expenseName"],

                        beneficiaryId = (int) reader["beneficiaryId"],

                        beneficiaryName = (string) reader["beneficiaryName"],

                        donationTime = (DateTime) reader["donationTime"],

                        donationAmt = (int) reader["donationAmt"]

                    };

                    results.Add(donation);
                }
            }

            return results;

        }

        public List<RecDonationViewModel> GetReceivedDonations(int beneficiaryId)
        {

            var results = new List<RecDonationViewModel>();

            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter("beneficiaryId", beneficiaryId)
            };
            using (IDataReader reader = ExecuteReader("GetReceivedDonations", DbCommandType.StoredProcedure, parameters)
            )
            {
                while (reader.Read())
                {
                    RecDonationViewModel donation = new RecDonationViewModel
                    {
                        expenseId = (int) reader["expenseId"],

                        expenseName = (string) reader["expenseName"],

                        donorId = (int) reader["donorId"],

                        donorName = (string) reader["donorName"],

                        donationTime = (DateTime) reader["donationTime"],

                        donationAmt = (int) reader["donationAmt"]

                    };

                    results.Add(donation);
                }
            }

            return results;

        }

        string GetUserType(int userId)
        {
            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter("userId", userId)
            };
            string userType = "";
            using (
                IDataReader reader = ExecuteReader(Constants.SPROC.GetUserType, DbCommandType.StoredProcedure,
                    parameters))
            {
                while (reader.Read())
                {
                    userType = (string) reader["Type"];
                }
            }
            return userType;
        }

        public UserViewModel GetUser(int userId)
        {
            string userType = GetUserType(userId);
            if (userType.Equals(Constants.UserType.Organization))
            {
                return GetOrganization(userId);
            }
            else
            {
                return GetIndividual(userId);
            }
        }

        public IndividualViewModel GetIndividual(int userId)
        {
            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter("id", userId)
            };
            IndividualViewModel individual = null;
            using (IDataReader reader = ExecuteReader("GetIndividual", DbCommandType.StoredProcedure, parameters))
            {
                while (reader.Read())
                {
                    individual = new IndividualViewModel
                    {
                        Id = (int) reader[Constants.Individual.Id],
                        Name = (string) reader[Constants.Individual.Name],
                        Title = (string) reader[Constants.Individual.Title],
                        Organisation = (string) reader[Constants.Individual.Organization]
                    };
                }
            }
            return individual;
        }

        public OrganizationViewModel GetOrganization(int userId)
        {
            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter("id", userId)
            };
            OrganizationViewModel organization = null;
            using (IDataReader reader = ExecuteReader("GetOrganization", DbCommandType.StoredProcedure, parameters))
            {
                while (reader.Read())
                {
                    organization = new OrganizationViewModel
                    {
                        Id = (int) reader[Constants.Organization.Id],
                        Name = (string) reader[Constants.Organization.Name],
                        Description = (string) reader[Constants.Organization.Description]
                    };
                }
            }
            return organization;
        }
    }
}