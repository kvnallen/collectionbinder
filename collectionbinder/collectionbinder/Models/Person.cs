using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace collectionbinder.Models
{
    public class Person
    {
        public string Name { get; set; }

        public ICollection<Phone> Phones { get; set; }
        public ICollection<Email> Emails { get; set; }
    }
}