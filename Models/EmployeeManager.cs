using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM_System.Models
{
    internal class EmployeeManager
    {
        private List<Employee> employees;
        
        // Store all the db commands here. Or submit them all in realtime.
        private List<MySql.Data.MySqlClient.MySqlCommand> dbCommands; 
        
        public void FetchEmployees(Key key)
        {
            if (key.hasAccess)
            {
                // Get employee data. Depends on the schema
                // Employees should be in the Employees List
            }
        }

        // Change this to a readonly view/span
        public Employee[] GetEmployees()
        {
            return employees.ToArray();
        }

        public bool AddNewEmployee(Employee employee)
        {
            if (!employees.Contains(employee))
            {
                employees.Add(employee);
                return true;
            }
            return false;
        }

        public bool DeleteEmployee(int emp_id)
        {
            foreach (Employee emp in employees)
            {
                if(emp.EmpId == emp_id)
                {
                    employees.Remove(emp);
                    return true;
                }
            }
            return false;
        }

        public void SubmitDataEdit(Employee edittedEmp)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                if(employees[i].EmpId == edittedEmp.EmpId)
                {
                    employees[i] = edittedEmp;
                }
            }
        }
    }
}
