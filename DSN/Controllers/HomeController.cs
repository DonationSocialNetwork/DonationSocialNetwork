using System;
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

        private List<ExpenseViewModel> expenses;

        UserProfilesDataAccess userProfilesDataAccess = new UserProfilesDataAccess();

        public ActionResult Index()
        {
            return View(userProfilesDataAccess.GetUsers(1));
        }

        //public ActionResult Index()
        //{
        //    PopulateUsers();
        //    return View(users.GetRange(0,6));
        //}

        public ActionResult NetWork()
        {
            return View(userProfilesDataAccess.GetUsers());
        }

        //public ActionResult NetWork()
        //{
        //    PopulateUsers();
        //    return View(users);
        //}

        /*
        Expense(id, user_id, description, actual amount, balance amount, facilitator_id, approval status)
        */
        public ActionResult UserProfile(int userId)
        {
            PopulateUsers();
            dynamic userProfile = new ExpandoObject();
            userProfile.user = users.Find(x => x.Id.Equals(userId));
            userProfile.Expenses = userProfilesDataAccess.GetExpenseNeeds(userId);// expenses.FindAll(x => x.UserId.Equals(userId));
            return View( userProfile);
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
            int donorId = 1; //for time being
            try
            {
                userProfilesDataAccess.AddDonationRecord(donorId, expenseId, donationAmount, beneficiaryUserId);
            }
            catch (Exception e)
            {
                ViewBag.message = "Error!";
                return View();
            }
            ViewBag.message = "Suceess! Thanks for donating.";
            return View();
        }

        public ActionResult Pay()
        {
            return View();    
        }

        public void PopulateUsers()
        {
            if (users != null)
            {
                return;
            }
            users = new List<IndividualViewModel>();
            users.Add(new IndividualViewModel {Id = 1, Name = "Aditya", Title = "Student", Organisation = "MTB School", BalanceAmount = 1000});
            users.Add(new IndividualViewModel {Id = 2, Name = "Pallavi", Title = "Student", Organisation = "Kothari Primary School", BalanceAmount = 1000 });
            users.Add(new IndividualViewModel {Id = 3, Name = "Arvind", Title = "Student", Organisation = "MTB School", BalanceAmount = 1000 });
            users.Add(new IndividualViewModel {Id = 4, Name = "Parth", Title = "Student", Organisation = "MTB School", BalanceAmount = 1000 });
            users.Add(new IndividualViewModel {Id = 5, Name = "Komal", Title = "Student", Organisation = "MTB School", BalanceAmount = 1000 });
            users.Add(new IndividualViewModel {Id = 6, Name = "Pallavi", Title = "Student", Organisation = "MTB School", BalanceAmount = 1000 });
            users.Add(new IndividualViewModel {Id = 7, Name = "Samrudhdhi", Title = "Student", Organisation = "MTB School", BalanceAmount = 1000 });
            users.Add(new IndividualViewModel {Id = 8, Name = "Mira", Title = "Student", Organisation = "MTB School", BalanceAmount = 1000 });
            users.Add(new IndividualViewModel {Id = 9, Name = "Prerana", Title = "Student", Organisation = "MTB School", BalanceAmount = 1000 });
            users.Add(new IndividualViewModel {Id = 10, Name = "Payal", Title = "Student", Organisation = "MTB School", BalanceAmount = 1000 });
            users.Add(new IndividualViewModel {Id = 11, Name = "Vaishali", Title = "Student", Organisation = "MTB School", BalanceAmount = 1000 });
            PopulateExpenses();
        }

        public void PopulateExpenses()
        {
            if (expenses != null)
            {
                return;
            }
            expenses = new List<ExpenseViewModel>();
            expenses.Add(new ExpenseViewModel {Id = 1, UserId = 2, Title = "Admission fees", ActualAmout = 1000, BalanceAmount = 500});
            expenses.Add(new ExpenseViewModel { Id = 2, UserId = 2, Title = "Exam fees", ActualAmout = 1000, BalanceAmount = 500 });
            expenses.Add(new ExpenseViewModel { Id = 3, UserId = 2, Title = "School picnic", ActualAmout = 1000, BalanceAmount = 500 });

        }
    }
}