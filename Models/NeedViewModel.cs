using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSN.Models
{
    public class NeedViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int ActualAmout { get; set; }

        public int BalanceAmount { get; set; }
    }
}
