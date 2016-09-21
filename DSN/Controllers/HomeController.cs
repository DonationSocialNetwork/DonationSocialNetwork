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

        public ActionResult NetWork()
        {
            return View(userProfilesDataAccess.GetNetwork());
        }
        
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
            dynamic donationInfo = new ExpandoObject();
            //donationInfo.beneficiaryUser = users.Find(x => x.Id.Equals(beneficiaryUserId));
            donationInfo.beneficiaryUser = userProfilesDataAccess.GetUser(beneficiaryUserId) as IndividualViewModel;
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

        public ActionResult AddNeed()
        {
            return View();
        }

        public ActionResult Notifications()
        {
            List<Message> messages = new List<Message>
            {
                new Message {SenderName = "Akash", Body = "Thank you very much for donation."}
            };
            return View(messages);
        }
        
    }
}