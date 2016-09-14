﻿using System;
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
    public class UserProfilesDataAccess: DataAccessBase
    {
        public UserViewModel  GetUsers()
        {
            UserViewModel users = new UserViewModel
            {
                Individuals = new List<IndividualViewModel>(),
                Organizations = new List<OrganizationViewModel>()
            };
            using (IDataReader reader= ExecuteReader(Constants.SPROC.GetNetwork,DbCommandType.StoredProcedure))
            {
                while (reader.Read())
                {
                    IndividualViewModel individual = new IndividualViewModel
                    {
                        Id = (int)reader[Constants.Individual.Id],
                        Name = (string)reader[Constants.Individual.Name],
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

        public UserViewModel GetUsers(int id)
        {
            UserViewModel users = new UserViewModel
            {
                Individuals = new List<IndividualViewModel>(),
                Organizations = new List<OrganizationViewModel>()
            };
            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter(Constants.Individual.Id, id)
            };
            using (IDataReader reader = ExecuteReader(Constants.SPROC.GetUsersNetwork, DbCommandType.StoredProcedure, parameters))
            {
                while (reader.Read())
                {
                    IndividualViewModel individual = new IndividualViewModel
                    {
                        Id = (int)reader[Constants.Individual.Id],
                        Name = (string)reader[Constants.Individual.Name],
                        Title = (string)reader[Constants.Individual.Title],
                        Organisation = (string)reader[Constants.Individual.Organization]
                    };
                    users.Individuals.Add(individual);
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        OrganizationViewModel organization = new OrganizationViewModel
                        {
                            Id = (int)reader[Constants.Organization.Id],
                            Name = (string)reader[Constants.Organization.Name],
                            Description = (string)reader[Constants.Organization.Description]
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
            using (IDataReader reader = ExecuteReader(Constants.SPROC.GetApprovals, DbCommandType.StoredProcedure, parameters))
            {
                while (reader.Read())
                {
                    ApprovalViewModel approval = new ApprovalViewModel
                    {
                        NeedId = (int)reader[Constants.Need.Id],
                        NeedTitle = (string)reader[Constants.Need.Title],
                        ActualAmount = (int)reader[Constants.Need.ActualAmount],
                        UserId = (int)reader[Constants.Need.User_Id],
                        UserName = (string)reader[Constants.Individual.Name],
                        ApprovalStatus = ((string)reader[Constants.Need.Approval_Status])
                    };
                    listOfApprovals.Add(approval);
                }
            }
            return listOfApprovals;
        }

        //To-do: move to Logic layer
        public List<ApprovalViewModel> GetPendingApprovals(int approverId)
        {
            List<ApprovalViewModel> listOfApprovals = GetApprovals(approverId);
            List<ApprovalViewModel> listOfPendingApprovals = listOfApprovals.FindAll(x => x.ApprovalStatus.Equals(Constants.Approval.PendingApprovalCode));
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
            List<ApprovalViewModel> listOfApprovedNeeds = listOfApprovals.FindAll(x => x.ApprovalStatus.Equals(Constants.Approval.ApprovedCode));
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
            using ( IDataReader reader = ExecuteReader(Constants.SPROC.Approve, DbCommandType.StoredProcedure, parameters))
            {
                if (reader.RecordsAffected > 0)
                {
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        
    }
}