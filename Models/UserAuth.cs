
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM_System.Models
{
    // Some implementation of not letting normal employees run DB update code
    internal struct Key
    {
        public readonly bool hasAccess;
        public Key(UserModel user)
        {
            hasAccess = user.Role == Role.HRManager || user.Role == Role.SysAdmin;
        }
    }
}
