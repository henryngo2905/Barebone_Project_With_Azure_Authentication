using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barebone.Web.Models
{
    public class AadGroupMember
    {
        public Guid ObjectId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string UserPrincipalName { get; set; }
        public string Email { get; set; }
        public string OfficeLocation { get; set; }
        public string BusinessPhone { get; set; }
        public string MobilePhone { get; set; }
        public string JobTitle { get; set; }
        public string UserType { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }
        public List<AadGroup> GroupMemberships { get; set; }

    }

    public class AadGroup
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
    }

}