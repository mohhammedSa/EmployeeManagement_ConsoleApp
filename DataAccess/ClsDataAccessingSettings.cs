using System;

public class ClsDataSccessSettings
{
    public static string ConnectionString()
    {
        return "Server=localhost;" +
         "Database=Employees;" +
         " User Id=Hamouda;" +
          "Password=Hamouda123.@;" +
          " SslMode=None;";
    }

}