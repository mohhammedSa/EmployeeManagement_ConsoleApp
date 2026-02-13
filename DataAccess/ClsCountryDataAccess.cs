using System;
using System.Data;
using MySqlConnector;

namespace DataAccess
{
    public class ClsCountryDataAccess
    {
        public static DataTable CountrisList()
        {
            DataTable dt = new();
            MySqlConnection connection = new MySqlConnection(ClsDataSccessSettings.ConnectionString());
            string query = "select * from Countries order by CountryName";
            MySqlCommand command = new(query, connection);

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
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

        public static bool FindCountryByname(ref int Id, string name, ref string? countryCode, ref string? countruPhoneCode)
        {
            bool isFound = false;
            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());
            string query = "select * from Countries where CountryName=@NAME";
            MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@NAME", name);

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    Id = (int)reader["CountryID"];
                    name = (string)reader["CountryName"];
                    countryCode = reader["Code"] == DBNull.Value ? null : (string)reader["Code"];
                    countruPhoneCode = reader["PhoneCode"] == DBNull.Value ? null : (string)reader["PhoneCode"];
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Find by name: " + e.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool FindCountryByID(int id, ref string name, ref string? countryCode, ref string? countruPhoneCode)
        {
            bool isFound = false;
            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());
            string query = "select * from Countries where CountryID=@ID";
            MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@ID", id);

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    name = (string)reader["CountryName"];
                    countryCode = reader["Code"] == DBNull.Value ? null : (string)reader["Code"];
                    countruPhoneCode = reader["PhoneCode"] == DBNull.Value ? null : (string)reader["PhoneCode"];
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Find by id" + e.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static bool isCountryExits(string name)
        {
            bool isExist = false;
            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());
            string query = "select exists (select 1 from Countries where CountryName=@NAME) as found";
            // string query = "select CountryID from Countries where CountryName=@NAME";
            MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@NAME", name);

            try
            {
                connection.Open();
                object? result = command.ExecuteScalar();
                isExist = Convert.ToInt32(result) == 1;
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }

        public static bool isCountryExits(int id)
        {
            bool isExist = false;
            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());
            string query = "select exists (select 1 from Countries where CountryID=@ID) as found";
            // string query = "select CountryID from Countries where CountryName=@NAME";
            MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@ID", id);

            try
            {
                connection.Open();
                object? result = command.ExecuteScalar();
                isExist = Convert.ToInt32(result) == 1;
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
            return isExist;
        }

        public static int Add(string name, string? code, string? phoneCode)
        {
            int id = -1;
            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());
            string query = "insert into Countries (CountryName,Code,PhoneCode) values (@NAME, @Code, @PhoneCode ); select last_insert_id()";
            MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@NAME", name);
            command.Parameters.AddWithValue("@Code", code);
            command.Parameters.AddWithValue("@PhoneCode", phoneCode);

            try
            {
                connection.Open();
                id = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
            return id;
        }

        public static bool Update(int id, string name, string? code, string? phoneCode)
        {
            int RowAffected = 0;
            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());
            string query = "update Countries set CountryName = @NAME , Code = @Code, PhoneCode=@PhoneCode where CountryID=@ID";
            MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@NAME", name);
            command.Parameters.AddWithValue("@Code", code);
            command.Parameters.AddWithValue("@PhoneCode", phoneCode);

            try
            {
                connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
            return RowAffected > 0;
        }

        public static bool Delete(int id)
        {
            int rowAffected = 0;
            MySqlConnection connection = new(ClsDataSccessSettings.ConnectionString());
            string query = "delete from Countries where CountryID=@ID";
            MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@ID", id);

            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
            return rowAffected > 0;
        }
    }
}