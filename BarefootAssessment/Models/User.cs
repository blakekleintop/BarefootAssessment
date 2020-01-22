using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarefootAssessment.Models
{
    public class User
    {
        public string Name { get; set; }
        public User()
        {
        }
        public User(string name)
        {
            this.Name = name;
        }
        public string LoweredName
        {
            get { return this.Name.ToLower(); }
        }
    }
}
