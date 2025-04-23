using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM_System.Models
{

    internal class Report
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Nature { get; set; }
        public int[] EmployeesInvolved { get; set; }
    }
}
