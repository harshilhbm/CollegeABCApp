using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CollegeABCApp.Model
{
    [DataContract]
    public class StateMaster
    {
        [DataMember(Name = "StateId")]

        public int StateId { get; set; }

        [DataMember(Name = "StateName")]
        public string StateName { get; set; }

        [DataMember(Name = "CountryId")]
        public int CountryId { get; set; }
    }
}
