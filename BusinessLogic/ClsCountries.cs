using System.Data;
using DataAccess;
using MySqlConnector;

namespace EmployeeBusiness
{
    public class ClsCountry
    {
        public int CountryID { get; }
        public string CountryName { get; set; }

        public ClsCountry()
        {
            CountryID = -1;
            CountryName = "";
        }

        private ClsCountry(int id, string name)
        {
            CountryID = id;
            CountryName = name;
        }

        public static DataTable GetCountries()
        {
            return ClsCountryDataAccess.CountrisList();
        }

        public static ClsCountry? FindCountry(string name)
        {
            int id = -1;
            if (ClsCountryDataAccess.FindCountry(ref id, ref name))
                return new ClsCountry(id, name);
            else
                return null;
        }

        public static bool isCountryExists(string name)
        {
            return ClsCountryDataAccess.isCountryExits(name);
        }

        public static bool isCountryExists(int id)
        {
            return ClsCountryDataAccess.isCountryExits(id);
        }

        public static bool DeleteCountry(int id)
        {
            return ClsCountryDataAccess.Delete(id);
        }

        public bool AddCountry()
        {
            return ClsCountryDataAccess.Add(CountryName) != 0;
        }
    }
}