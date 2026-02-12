using System;
using System.Data;
using System.Data.SqlTypes;
using System.Numerics;
using MySqlConnector;

namespace DataAccess
{
    public class ClsEmployeeDataAccess
    {
        public static bool GetEmployeeInfoById(int id, ref string name, ref int? managerId, ref string? managerName, ref decimal salary)
        {
            bool isFound = false;

            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());

            // string query = "select * from Employees where EmployeeID=@ID";
            string query = "select ori.EmployeeID, ori.Name, ori.ManagerID, cp.Name as ManagerName, ori.Salary from Employees as ori left join Employees as cp on ori.ManagerID = cp.EmployeeID where ori.EmployeeID = @ID";

            MySqlCommand commande = new(query, connection);
            commande.Parameters.AddWithValue("@ID", id);

            try
            {
                connection.Open();
                MySqlDataReader reader = commande.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    id = (int)reader["EmployeeID"];
                    name = (string)reader["Name"];
                    managerId = reader["ManagerID"] == DBNull.Value ? null : (int)reader["ManagerID"];
                    managerName = reader["ManagerName"] == DBNull.Value ? null : (string)reader["ManagerName"];
                    salary = (decimal)reader["Salary"];
                }
                else
                    isFound = false;

                reader.Close();
            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int Add(string name, int? managerId, string? managerName, decimal salary)
        {
            int lastId = -1;
            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());

            string query = "insert into Employees (Name, ManagerID, Salary) values (@Name,@ManagerID,@Salary);"
            + "select LAST_INSERT_ID()";

            MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@ManagerID", managerId ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Salary", salary);

            try
            {
                connection.Open();
                object? result = command.ExecuteScalar();
                lastId = result != null ? Convert.ToInt32(result) : -1;
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
            return lastId;
        }

        public static bool Update(int id, string EmployeeName, int? managerId, decimal salary)
        {
            int rowsAffected = 0;
            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());
            string query = "update Employees set Name=@Name, ManagerID=@managerID, Salary=@salary where EmployeeID=@ID";

            MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@Name", EmployeeName);
            command.Parameters.AddWithValue("@managerID", managerId);
            command.Parameters.AddWithValue("@salary", salary);
            command.Parameters.AddWithValue("@ID", id);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected > 0;
        }

        public static bool Delete(int id)
        {
            int rowAffecrted = 0;
            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());
            string query = "delete from Employees where EmployeeID=@ID";
            MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@ID", id);

            try
            {
                connection.Open();
                rowAffecrted = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
            return rowAffecrted > 0;
        }

        public static DataTable EmployeesList()
        {
            DataTable dt = new();
            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());
            string query = "select * from Employees";
            MySqlCommand command = new(query, connection);

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool IsEmployeeExists(int id)
        {
            bool isFound = false;
            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());
            string query = "select exists (select 1 from Employees where EmployeeID=@ID) as found";
            MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@ID", id);

            try
            {
                connection.Open();
                object? result = command.ExecuteReader();
                isFound = Convert.ToInt32(result) == 1;
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
    }
}