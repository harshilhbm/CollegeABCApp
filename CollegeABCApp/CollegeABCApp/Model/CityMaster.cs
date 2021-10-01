using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CollegeABCApp.Model
{
    [DataContract]
    public class CityMaster
    {
        [DataMember(Name = "Cityid")]
        public int Cityid { get; set; }

        [DataMember(Name = "CityName")]
        public string CityName { get; set; }

        [DataMember(Name = "StateId")]
        public int StateId { get; set; }

        [DataMember(Name = "CountryId")]
        public int CountryId { get; set; }
    }
}
