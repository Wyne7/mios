using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIOS.Management.Core.Models
{
    public class UserInfo
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string role { get; set; }
    }

    public class RoleInfo
    {
        public string RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
    }
}
