using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSN.Models
{
    public class NetworkViewModel
    {
        public List<IndividualViewModel> Individuals { get; set; }

        public List<OrganizationViewModel> Organizations { get; set; }
    }
}
