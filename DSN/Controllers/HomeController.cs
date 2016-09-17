﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSN.DAL;
using DSN.Models;

namespace DSN.Controllers
{
    
    public class HomeController : Controller
    {
        private List<IndividualViewModel> users;

        private List<NeedViewModel> expenses;

        UserProfilesDataAccess userProfilesDataAccess = new UserProfilesDataAccess();

        public int UserId
        {
            get
            {
                return (int)Session[Constants.UserId];
            }
        }
         
        public ActionResult Index()
        {
            return View(userProfilesDataAccess.GetNetwork(UserId));
        }

        //public ActionResult Index()
        //{
        //    PopulateUsers();
        //    return View(users.GetRange(0,6));
        //}

        public ActionResult NetWork()
        {
            return View(userProfilesDataAccess.GetNetwork());
        }


        //public ActionResult NetWork()
        //{
        //    PopulateUsers();
        //    return View(users);
        //}

        public ActionResult MyDonations()
        {
            return View(userProfilesDataAccess.GetDonations(this.UserId));
        }

        public ActionResult ReceivedDonations()
        {
            return View(userProfilesDataAccess.GetReceivedDonations(this.UserId));
        }

        public ActionResult UserProfile(int userId)
        {
            dynamic userProfile = new ExpandoObject();
            UserViewModel user = userProfilesDataAccess.GetUser(userId);
            userProfile.user = user;
            userProfile.Expenses = userProfilesDataAccess.GetExpenseNeeds(userId);
            if (user.GetType() == typeof(IndividualViewModel))
            {
                return View("UserProfile", userProfile);
            }
            else
            {
                return View("OrganizationProfile", userProfile);
            }
        }

        public ActionResult DonatePay(int beneficiaryUserId, int expenseId)
        {
            PopulateUsers();
            dynamic donationInfo = new ExpandoObject();
            donationInfo.beneficiaryUser = users.Find(x => x.Id.Equals(beneficiaryUserId));
            var Expenses = userProfilesDataAccess.GetExpenseNeeds(beneficiaryUserId);
            donationInfo.expense = Expenses.Find(x => x.Id.Equals(expenseId));
            return View(donationInfo);
        }

        public ActionResult DonatePayResult(int beneficiaryUserId, int expenseId, int donationAmount)
        {
            int donorId = this.UserId;
            try
            {
                userProfilesDataAccess.AddDonationRecord(donorId, expenseId, donationAmount, beneficiaryUserId);
            }
            catch (Exception e)
            {
                ViewBag.message = "Error!";
                return View();
            }
            ViewBag.message = "Success! Thanks for donating.";
            return View();
        }

        public ActionResult Approvals()
        {
            return View(userProfilesDataAccess.GetApprovals(UserId));
        }

        public ActionResult MyAccount()
        {
            return View();
        }

        public ActionResult Approve(int needId)
        {
            userProfilesDataAccess.Approve(needId);
            List<ApprovalViewModel> approvals = userProfilesDataAccess.GetApprovals(UserId);
            return RedirectToAction("Approvals");
        }

        public ActionResult Pay()
        {
            return View();    
        }

        public ActionResult NeedsApproved()
        {
            return View(userProfilesDataAccess.GetApprovedNeedsDonationStatus(UserId));
        }

        public void PopulateUsers()
        {
            if (users != null)
            {
                return;
            }
            users = new List<IndividualViewModel>();
            users.Add(new IndividualViewModel {Id = 1, Name = "Aditya", Title = "Student", Organisation = "MTB School"});
            users.Add(new IndividualViewModel {Id = 2, Name = "Pallavi", Title = "Student", Organisation = "Kothari Primary School"});
            users.Add(new IndividualViewModel {Id = 3, Name = "Prerana", Title = "Student", Organisation = "Jnanpith School" });
            users.Add(new IndividualViewModel {Id = 4, Name = "Raghunath", Title = "Student", Organisation = "Golden Kids School" });
            users.Add(new IndividualViewModel {Id = 5, Name = "Samrudhdhi", Title = "Student", Organisation = "Navodaya School" });
            users.Add(new IndividualViewModel {Id = 6, Name = "Anup", Title = "Student", Organisation = "Vidya Mandir" });
            users.Add(new IndividualViewModel {Id = 7, Name = "Tushar", Title = "Student", Organisation = "Shivaji High School" });
            users.Add(new IndividualViewModel {Id = 8, Name = "Vishal", Title = "Student", Organisation = "Anjuman High school" });
            PopulateExpenses();
        }

        public void PopulateExpenses()
        {
            if (expenses != null)
            {
                return;
            }
            expenses = new List<NeedViewModel>();
            expenses.Add(new NeedViewModel {Id = 1, UserId = 2, Title = "Admission fees", ActualAmout = 1000, BalanceAmount = 500});
            expenses.Add(new NeedViewModel { Id = 2, UserId = 2, Title = "Exam fees", ActualAmout = 1000, BalanceAmount = 500 });
            expenses.Add(new NeedViewModel { Id = 3, UserId = 2, Title = "School picnic", ActualAmout = 1000, BalanceAmount = 500 });

        }
    }
}