using System.Runtime.Serialization;

namespace CollegeABCApp.Model
{
    [DataContract]
    public class StudentModel
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "StudentName")]
        public string StudentName { get; set; }

        [DataMember(Name = "Middlename")]
        public string Middlename { get; set; }

        [DataMember(Name = "Lastname")]
        public string Lastname { get; set; }

        [DataMember(Name = "Phone")]
        public string Phone { get; set; }

        [DataMember(Name = "Gender")]
        public string Gender { get; set; }

        [DataMember(Name = "Address")]
        public string Address { get; set; }

      


        [DataMember(Name = "Cityid")]
        public int Cityid { get; set; }

        [DataMember(Name = "StateId")]
        public int StateId { get; set; }

        [DataMember(Name = "CountryId")]
        public int CountryId { get; set; }


        [DataMember(Name = "Zipcode")]
        public int Zipcode { get; set; }
    }
}
