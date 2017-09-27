using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MvcFileManager.Models;


namespace MvcFileManager.Data_Access_Layer
{
    public class UserBusinessLayer
    {
        public UserProfile GetUser(int id)
        {
            UsersContext uc = new UsersContext();
            return uc.UserProfiles.Find(id);
        }

        public List<UserProfile> GetUsers()
        {
            UsersContext uc = new UsersContext();
            return uc.UserProfiles.ToList();
        }

    }
}