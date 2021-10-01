using CollegeABCApp.Model;
using CollegeABCApp.Translators;
using CollegeABCApp.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CollegeABCApp.Repository
{
    public class StudentDbClient
    {

        public List<StudentModelList> GetAllStudent(string connString)
        {
            return SqlHelper.ExtecuteProcedureReturnData<List<StudentModelList>>(connString,
                "GetStudents", r => r.TranslateAsStudentList());
        }

        public StudentModel GetStudent(string id, string connString)
        {
            StudentModel student = new StudentModel();

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Student_Master WHERE Id= @Id", con);
                    cmd.Parameters.Add(new SqlParameter("Id", id));
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            // write the data on to the screen    
                            student.Id = Convert.ToString(rdr["Id"]);
                            student.StudentName = Convert.ToString(rdr["StudentName"]);
                            student.Middlename = Convert.ToString(rdr["Middlename"]);
                            student.Lastname = Convert.ToString(rdr["Lastname"]);
                            student.Phone = Convert.ToString(rdr["Phone"]);
                            student.Gender = Convert.ToString(rdr["Gender"]);
                            student.Address = Convert.ToString(rdr["Address"]);
                            student.Cityid = Convert.ToInt32(rdr["Cityid"]);
                            student.StateId = Convert.ToInt32(rdr["StateId"]);
                            student.CountryId = Convert.ToInt32(rdr["CountryId"]);
                            student.Zipcode = Convert.ToInt32(rdr["Zipcode"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.StackTrace));
            }
            return student;
        }

        public List<CountryMaster> GetCountry(string connString)
        {
            List<CountryMaster> countryList = new List<CountryMaster>();
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM CountryMaster", con);
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            CountryMaster country = new CountryMaster();
                            country.CountryId = Convert.ToInt32(rdr["CountryId"]);
                            country.CountryName = Convert.ToString(rdr["CountryName"]);
                            countryList.Add(country);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.StackTrace));
            }
            return countryList;
        }

        public List<CountryMaster> GetCountryById(string id, string connString)
        {
            List<CountryMaster> countryList = new List<CountryMaster>();
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM CountryMaster WHERE CountryId= @Id", con);
                    cmd.Parameters.Add(new SqlParameter("Id", id));
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            CountryMaster country = new CountryMaster();
                            country.CountryId = Convert.ToInt32(rdr["CountryId"]);
                            country.CountryName = Convert.ToString(rdr["CountryName"]);
                            countryList.Add(country);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.StackTrace));
            }
            return countryList;
        }


        public List<StateMaster> GetState(string id, string connString)
        {
            List<StateMaster> stateList = new List<StateMaster>();
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM StateMaster WHERE CountryId= @Id", con);
                    cmd.Parameters.Add(new SqlParameter("Id", id));
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            StateMaster state = new StateMaster();
                            state.StateId = Convert.ToInt32(rdr["StateId"]);
                            state.StateName = Convert.ToString(rdr["StateName"]);
                            state.CountryId = Convert.ToInt32(rdr["CountryId"]);
                            stateList.Add(state);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.StackTrace));
            }
            return stateList;
        }

        public List<CityMaster> GetCity(string id, string connString)
        {
            List<CityMaster> cityList = new List<CityMaster>();
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM CityMaster WHERE StateId= @Id", con);
                    cmd.Parameters.Add(new SqlParameter("Id", id));
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            CityMaster city = new CityMaster();
                            city.Cityid = Convert.ToInt32(rdr["Cityid"]);
                            city.CityName = Convert.ToString(rdr["CityName"]);
                            city.StateId = Convert.ToInt32(rdr["StateId"]);
                            city.CountryId = Convert.ToInt32(rdr["CountryId"]);
                            cityList.Add(city);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.StackTrace));
            }
            return cityList;
        }


        public string SaveStudent(StudentModel model, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@StudentName",model.StudentName),
                new SqlParameter("@Middlename",model.Middlename),
                new SqlParameter("@Lastname",model.Lastname),
                new SqlParameter("@Phone",model.Phone),
                new SqlParameter("@Gender",model.Gender),
                new SqlParameter("@Address",model.Address),
                new SqlParameter("@Cityid",model.Cityid),
                new SqlParameter("@StateId",model.StateId),
                new SqlParameter("@CountryId",model.CountryId),
                new SqlParameter("@Zipcode",model.Zipcode),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "SaveStudent", param);
            return (string)outParam.Value;
        }

        public string DeleteStudent(string id, string connString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@Id",id),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "DeleteStudent", param);
            return (string)outParam.Value;
        }
    }
}
