
using Newtonsoft.Json;
using System.Web.Mvc;

namespace Ctrip.Model
{
    public class Person1
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Name1 { get; set; }

        public Role Role { get; set; }
    }

    public enum Role
    {
        Admin, User, Guest
    }

    public class Person2
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address2 HomeAddress { get; set; }
    }

    [Bind(Include = "Country")]
    public class Address2
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}