using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM_System.Models
{
    public enum Role {
        SysAdmin = 0, HRManager, Supervisor, Employee
    }

    internal class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public Employee MyEmployeeData { get; set; }
    }
}
