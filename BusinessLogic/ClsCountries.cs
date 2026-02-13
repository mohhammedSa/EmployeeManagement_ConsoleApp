using System.Data;
using DataAccess;
using MySqlConnector;

namespace EmployeeBusiness
{
    public class ClsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public string? CountryCode { get; set; }
        public string? CountryPhoneCode { get; set; }

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
            CountryCode = "";
            CountryPhoneCode = "";
            mode = enMode.enAddMode;
        }

        private ClsCountry(int id, string name, string? code, string? phoneCode)
        {
            CountryID = id;
            CountryName = name;
            CountryCode = code;
            CountryPhoneCode = phoneCode;
            mode = enMode.enUpdateMode;
        }

        public static DataTable GetCountries()
        {
            return ClsCountryDataAccess.CountrisList();
        }

        public static ClsCountry? FindCountryByID(int id)
        {
            string name = "";
            string? countryCode = "";
            string? countruPhoneCode = "";
            if (ClsCountryDataAccess.FindCountryByID(id, ref name, ref countryCode, ref countruPhoneCode))
                return new ClsCountry(id, name, countryCode, countruPhoneCode);
            else
                return null;
        }

        public static ClsCountry? FindCountry(string name)
        {
            int id = -1;
            string? countryCode = "";
            string? countryPhoneCode = "";
            if (ClsCountryDataAccess.FindCountryByname(ref id, name, ref countryCode, ref countryPhoneCode))
                return new ClsCountry(id, name, countryCode, countryPhoneCode);
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
            this.CountryID = ClsCountryDataAccess.Add(CountryName, CountryCode, CountryPhoneCode);
            return CountryID != -1;
        }

        private bool _UpdateCountry()
        {
            return ClsCountryDataAccess.Update(CountryID, CountryName, CountryCode, CountryPhoneCode);
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