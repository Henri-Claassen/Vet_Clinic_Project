using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    #region AssistantNurse
    internal class AssistantNurse : Employee
    {
        private string role;
        private const double rate = 200;

        public AssistantNurse()
        {

        }

        public AssistantNurse(string employeeName, string employeeSurname, int employeeID, int hoursWorked, string role) : base(employeeName, employeeSurname, employeeID, hoursWorked)
        {
            this.role = role;
        }

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        public override void calcSalary()
        {
            Salary = rate * HoursWorked;
        }

    }//End of AssistantNurse class
    #endregion

}//End of namespace
