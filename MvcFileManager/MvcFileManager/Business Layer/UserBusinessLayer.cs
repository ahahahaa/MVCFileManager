using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MvcFileManager.Models;
using MvcFileManager.Data_Access_Layer;


namespace MvcFileManager.Business_Layer
{
    [Authorize]
    public class UserBusinessLayer
    {
        public UserProfile GetUser(int id)
        {
            FileManagerDAL fmDAL = new FileManagerDAL();
            return fmDAL.UserProfiles.Find(id);
        }

        public UserProfile GetUserByUserName(string username)
        {
            FileManagerDAL fmDAL = new FileManagerDAL();
            return fmDAL.UserProfiles.Where(c => c.UserName == username).FirstOrDefault();
        }

        public List<UserProfile> GetUsers()
        {
            FileManagerDAL fmDAL = new FileManagerDAL();
            return fmDAL.UserProfiles.ToList();
        }

        public UserProfile ModifyUser(UserProfile user)
        {
            FileManagerDAL fmDAL = new FileManagerDAL();
            fmDAL.Entry(user).State = EntityState.Modified;
            fmDAL.SaveChanges();
            return user;
        }

        public UserStatus GetUserStatus(UserProfile user)
        {
            if (user.isAdmin)
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if (!user.isDeleteUser && user.isSearchPM)
            {
                if (user.isUploadPM && user.isModifyPM)
                {
                    if (user.isDeletePM)
                    {
                        return UserStatus.AuthenticatedVipUser;
                    }
                    else
                    {
                        return UserStatus.AuthenticatedUser;
                    }
                }
                else
                {
                    return UserStatus.AuthenticatedVisitor;
                }
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }
    }
}