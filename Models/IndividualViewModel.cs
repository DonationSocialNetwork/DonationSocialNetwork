using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSN.Models
{
    public class IndividualViewModel : UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Title { get; set; }

        public string Organisation { get; set; }

        public int BalanceAmount { get; set; }
    }
}
