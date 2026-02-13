using System.Data;
using DataAccess;
using MySqlConnector;

namespace EmployeeBusiness
{
    public class ClsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public enum enMode
        {
            enAddMode,
            enUpdateMode
        }
        public enMode mode;

        public ClsCountry()
        {
            CountryID = -1;
            CountryName = "";
            mode = enMode.enAddMode;
        }

        private ClsCountry(int id, string name)
        {
            CountryID = id;
            CountryName = name;
            mode = enMode.enUpdateMode;
        }

        public static DataTable GetCountries()
        {
            return ClsCountryDataAccess.CountrisList();
        }

        public static ClsCountry? FindCountryByID(int id)
        {
            string name = "";
            if (ClsCountryDataAccess.FindCountryByID(id, ref name))
                return new ClsCountry(id, name);
            else
                return null;
        }

        public static ClsCountry? FindCountry(string name)
        {
            int id = -1;
            if (ClsCountryDataAccess.FindCountryByname(ref id, name))
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

        private bool _AddCountry()
        {
            this.CountryID = ClsCountryDataAccess.Add(CountryName);
            return CountryID != -1;
        }

        private bool _UpdateCountry()
        {
            return ClsCountryDataAccess.Update(CountryID, CountryName);
        }

        public bool Save()
        {
            switch (mode)
            {
                case enMode.enAddMode:
                    if (_AddCountry())
                    {
                        mode = enMode.enUpdateMode;
                        return true;
                    }
                    else
                        return false;

                case enMode.enUpdateMode:
                    return _UpdateCountry();
            }
            return false;
        }
    }
}