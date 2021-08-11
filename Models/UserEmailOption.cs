using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class UserEmailOption
    {
        public List<String> ToEmails { get; set; }
        public string Subject { get; set; }
        public String Body { get; set; }

        public List<KeyValuePair<string,string >> Placeholder { get; set; }
    }


}
