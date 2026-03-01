using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_281_Project
{
    #region Employee
    internal class Employee 
    {
        private string employeeName;
        private string employeeSurname;
        private int employeeID;
        private double salary;
        private int hoursWorked;

        public List<Employee> employees { get; set; }

        public Employee()
        {

        }
        public Employee(string employeeName, string employeeSurname, int employeeID, int hoursWorked)
        {
            this.employeeName = employeeName;
            this.employeeSurname = employeeSurname;
            this.employeeID = employeeID;
            this.hoursWorked = hoursWorked;
        }


        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }
        public string EmployeeSurname
        {
            get { return employeeSurname; }
            set { employeeSurname = value; }
        }
        public int EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }
        public int HoursWorked
        {
            get { return hoursWorked; }
            set { hoursWorked = value; }
        }

        public double Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public virtual void calcSalary()
        {

        }
    }//End of employee class
    #endregion

}// End of namespace
