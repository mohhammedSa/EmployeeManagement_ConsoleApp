using System;
using System.Data;
using EmployeeBusiness;

internal class EmployeeManagementApp
{
    static void TestFindEmployee(int id)
    {
        ClsEmployee? employee = ClsEmployee.Find(id);

        if (employee != null)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Employee Info:");
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Employee name         : {employee.EmployeeName}");
            Console.WriteLine($"Employee Manager Id   : {((employee.EmployeeManagerId == null) ? "null" : employee.EmployeeManagerId.ToString())} ");
            Console.WriteLine($"Employee Manager name : {((employee.EmployeeManagerName == null) ? "null" : employee.EmployeeManagerName)}");
            Console.WriteLine($"Employee Salary       : {employee.EmployeeSalary}");
            Console.WriteLine("---------------------------------");
        }
        else
            Console.WriteLine("\nEmployee does not found.");
    }

    public static void TestAddEmployee()
    {
        ClsEmployee Employee = new ClsEmployee
        {
            EmployeeName = "HmiQa",
            EmployeeManagerId = 5,
            EmployeeManagerName = "",
            EmployeeSalary = 1000
        };

        if (Employee.Save())
            Console.WriteLine($"Contact added successfully, With contactId: {Employee.EmployeeId}");
    }

    public static void TestUpdateEmployee(int id)
    {
        ClsEmployee? employee = ClsEmployee.Find(id);
        if (employee == null)
        {
            Console.WriteLine("Employee not found!");
        }
        else
        {
            employee.EmployeeName = "Adderahmane";
            employee.EmployeeManagerId = 10;
            employee.EmployeeSalary = 270000;

            if (employee.Save())
                Console.WriteLine("Employee updated succesfully.");
        }
    }

    public static void TestDeleteEmployee(int id)
    {
        if (ClsEmployee.IsEmployeeExists(id))
        {
            if (ClsEmployee.DeleteEmployee(id))
                Console.WriteLine("Employee deleted successfully.");
            else
                Console.WriteLine("Employee deleted failed.");
        }
        else
        {
            Console.WriteLine("Employee does not exist");
        }
    }

    public static void TestListEmployees()
    {
        DataTable dataTable = ClsEmployee.GetEmployees();
        Console.WriteLine("------------------------------------");
        Console.WriteLine("Employees Data: ");
        Console.WriteLine("------------------------------------");
        foreach (DataRow row in dataTable.Rows)
        {
            Console.WriteLine($"{row["EmployeeID"]} | {row["Name"]} | {(row["ManagerID"] == DBNull.Value ? "null" : row["ManagerID"])} | {row["Salary"]} ");
            Console.WriteLine("------------------------------------");
        }
    }

    public static void TestIsEmployeeExixts(int id)
    {
        if (ClsEmployee.IsEmployeeExists(id))
            Console.WriteLine("Employee Exists.");
        else
            Console.WriteLine("Employee does not exist.");
    }

    static void TestFindCoutryByID(int id)
    {
        ClsCountry? country = ClsCountry.FindCountryByID(id);
        if (country != null)
        {
            Console.WriteLine("Country found.");
            Console.WriteLine($"{country.CountryID}, {country.CountryName}, {country.CountryCode ?? "null"}, {country.CountryPhoneCode ?? "null"}");
        }
        else
            Console.WriteLine("Country does not found.");
    }

    static void TestFindCoutryByName(string name)
    {
        ClsCountry? country = ClsCountry.FindCountry(name);
        if (country != null)
        {
            Console.WriteLine("Country found.");
            Console.WriteLine($"{country.CountryID}, {country.CountryName}, {country.CountryCode ?? "null"}, {country.CountryPhoneCode ?? "null"}");
        }
        else
            Console.WriteLine("Country does not found.");
    }
    static void TestIsCountryExists(string name)
    {
        if (ClsCountry.isCountryExists(name))
            Console.WriteLine("Country exists.");
        else
            Console.WriteLine("Country does not exist");
    }
    static void TestIsCountryExists(int id)
    {
        if (ClsCountry.isCountryExists(id))
            Console.WriteLine("Country exists.");
        else
            Console.WriteLine("Country does not exist");
    }
    static void testAddNewCountry(string name, string Code, string PhoneCode)
    {
        ClsCountry country = new ClsCountry
        {
            CountryName = name,
            CountryCode = Code,
            CountryPhoneCode = PhoneCode
        };

        if (country.Save())
            Console.WriteLine("Country added successfully.");
        else
            Console.WriteLine("Country added failed.");
    }

    static void testUpdateCountry(int id, string name)
    {
        ClsCountry? country = ClsCountry.FindCountryByID(id);
        if (country != null)
        {
            country.CountryName = name;

            if (country.Save())
                Console.WriteLine("Country updated successfully.");
            else
                Console.WriteLine("Country updated failed.");
        }
        else
            Console.WriteLine("Country does not found.");

    }

    static void testDeleteCountry(int ID)
    {
        if (ClsCountry.isCountryExists(ID))
            if (ClsCountry.DeleteCountry(ID))
                Console.WriteLine("Country Deleted successfully.");

            else Console.WriteLine("Country Deleted Failed.");

        else Console.WriteLine("Country does not found.");
    }

    static void TestCountriesList()
    {
        Console.WriteLine("-------------------");
        Console.WriteLine("Countries list: ");
        Console.WriteLine("-------------------");
        foreach (DataRow country in ClsCountry.GetCountries().Rows)
        {
            Console.WriteLine($"{country["CountryID"]}, {country["CountryName"]} , {country.Field<string?>("Code")?.ToString() ?? "null"}, {country.Field<string?>("PhoneCode")?.ToString() ?? "null"}");
            Console.WriteLine("-------------------");
        }
    }
    static void Main()
    {
        // TestFindEmployee(1);
        // TestFindEmployee(3);
        // TestAddEmployee();
        // TestUpdateEmployee(23);
        // TestFindEmployee(23);
        // TestDeleteEmployee(19);
        // TestListEmployees();
        // TestIsEmployeeExixts(100);


        // TestCountriesList();
        // TestIsCountryExists("Morocco");
        // TestIsCountryExists("ghh");
        // TestFindCoutryByName("Morocco");
        // Console.WriteLine("-------------------------------");
        // TestFindCoutryByName("ghh");
        // TestIsCountryExists(5);
        // testDeleteCountry(5);
        // TestIsCountryExists(5);
        // testAddNewCountry("Qatar");
        // TestCountriesList();
        testAddNewCountry("Egypt", "122", "122");
        Console.WriteLine("-------------------------------");
        TestCountriesList();
    }
}