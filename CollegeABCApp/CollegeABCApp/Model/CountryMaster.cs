using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CollegeABCApp.Model
{
    [DataContract]
    public class CountryMaster
    {
        [DataMember(Name = "CountryId")]
        public int CountryId { get; set; }

        [DataMember(Name = "CountryName")]
        public string CountryName { get; set; }
    }
}
