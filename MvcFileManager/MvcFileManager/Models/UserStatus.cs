using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcFileManager.Models
{
    public enum UserStatus
    {
        AuthenticatedAdmin,
        AuthenticatedVipUser,
        AuthenticatedUser,
        AuthenticatedVisitor,
        NonAuthenticatedUser
    }
}