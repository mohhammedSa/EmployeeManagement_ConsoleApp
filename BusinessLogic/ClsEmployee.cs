using System;
using System.Data;
using DataAccess;

namespace EmployeeBusiness
{
    public class ClsEmployee
    {
        enum enMode
        {
            enAddMode, enUpdateMode
        }

        private enMode Mode;
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? EmployeeManagerId { get; set; }
        public string? EmployeeManagerName { get; set; }
        public decimal EmployeeSalary { get; set; }

        public ClsEmployee()
        {
            this.EmployeeId = -1;
            this.EmployeeName = "";
            this.EmployeeManagerId = -1;
            this.EmployeeSalary = 0;
            Mode = enMode.enAddMode;
        }

        private ClsEmployee(int id, string name, int? managerId, string? managerName, decimal salary)
        {
            EmployeeId = id;
            EmployeeName = name;
            EmployeeManagerId = managerId;
            EmployeeSalary = salary;
            EmployeeManagerName = managerName;

            Mode = enMode.enUpdateMode;
        }

        public static ClsEmployee? Find(int id)
        {
            string employeeName = "";
            int? managerId = 0;
            string? managerName = "";
            decimal salary = 0;


            if (ClsEmployeeDataAccess.GetEmployeeInfoById(id, ref employeeName, ref managerId, ref managerName, ref salary))
                return new ClsEmployee(id, employeeName, managerId, managerName, salary);

            return null;
        }


        private bool _AddNewEmployee()
        {
            EmployeeId = ClsEmployeeDataAccess.Add(
             EmployeeName,
                 EmployeeManagerId,
                 EmployeeManagerName,
                 EmployeeSalary);

            return EmployeeId != -1;
        }

        private bool _UpdateEmployee()
        {
            return ClsEmployeeDataAccess.Update(EmployeeId, EmployeeName, EmployeeManagerId, EmployeeSalary);
        }
        public bool Save()
        {
            if (Mode == enMode.enAddMode)
            {
                if (_AddNewEmployee())
                {
                    Mode = enMode.enUpdateMode;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return _UpdateEmployee();
            }
        }

        public static bool DeleteEmployee(int id)
        {
            return ClsEmployeeDataAccess.Delete(id);
        }

        public static DataTable GetEmployees()
        {
            return ClsEmployeeDataAccess.EmployeesList();
        }

        public static bool IsEmployeeExists(int id)
        {
            return ClsEmployeeDataAccess.IsEmployeeExists(id);
        }
    }
}