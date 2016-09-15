using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSN.Models
{
    public class ApprovalViewModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int NeedId { get; set; }

        public String NeedTitle { get; set; }

        public int ActualAmount { get; set; }

        public int BalanceAmount { get; set; }

        public string ApprovalStatus { get; set; }
    }
}
