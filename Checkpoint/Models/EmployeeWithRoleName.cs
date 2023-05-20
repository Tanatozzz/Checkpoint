using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkpoint.Singletons;

namespace Checkpoint.Models
{
    public class EmployeeWithRoleName : Employee
    {
        public string RoleTitle { get; set; }
    }
}
