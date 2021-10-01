using CollegeABCApp.Model;
using CollegeABCApp.Utility;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CollegeABCApp.Translators
{
    public static class UserTranslator
    {
        public static StudentModelList TranslateAsUser(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new StudentModelList();
            if (reader.IsColumnExists("Id"))
                item.Id = SqlHelper.GetNullableString(reader, "Id");

            if (reader.IsColumnExists("StudentName"))
                item.StudentName = SqlHelper.GetNullableString(reader, "StudentName");

            if (reader.IsColumnExists("Middlename"))
                item.Middlename = SqlHelper.GetNullableString(reader, "Middlename");

            if (reader.IsColumnExists("Lastname"))
                item.Lastname = SqlHelper.GetNullableString(reader, "Lastname");

            if (reader.IsColumnExists("Phone"))
                item.Phone = SqlHelper.GetNullableString(reader, "Phone");

            if (reader.IsColumnExists("Gender"))
                item.Gender = SqlHelper.GetNullableString(reader, "Gender");

            if(reader.IsColumnExists("Address"))
                item.Address = SqlHelper.GetNullableString(reader, "Address");

            if (reader.IsColumnExists("Cityid"))
                item.Cityid = SqlHelper.GetNullableInt32(reader, "Cityid");

            if (reader.IsColumnExists("StateId"))
                item.StateId = SqlHelper.GetNullableInt32(reader, "StateId");

            if (reader.IsColumnExists("CountryId"))
                item.CountryId = SqlHelper.GetNullableInt32(reader, "CountryId");

            if (reader.IsColumnExists("City"))
                item.City = SqlHelper.GetNullableString(reader, "City");

            if (reader.IsColumnExists("State"))
                item.State = SqlHelper.GetNullableString(reader, "State");

            if (reader.IsColumnExists("Country"))
                item.Country = SqlHelper.GetNullableString(reader, "Country");

            if (reader.IsColumnExists("Zipcode"))
                item.Zipcode = SqlHelper.GetNullableInt32(reader, "Zipcode");


            return item;
        }

        public static List<StudentModelList> TranslateAsStudentList(this SqlDataReader reader)
        {
            var list = new List<StudentModelList>();
            while (reader.Read())
            {
                list.Add(TranslateAsUser(reader, true));
            }
            return list;
        }

      
    }
}
