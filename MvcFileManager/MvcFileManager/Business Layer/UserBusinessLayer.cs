using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MvcFileManager.Data_Access_Layer;
using MvcFileManager.Models;


namespace MvcFileManager.Business_Layer
{using MvcFileManager.Models;
    public class UserBusinessLayer
    {
        public UserProfile GetUser(int id)
        {
            //UsersContext uc = new UsersContext();
            FileManagerDAL fmDAL = new FileManagerDAL();
            return fmDAL.UserProfiles.Find(id);
        }

        public List<UserProfile> GetUsers()
        {
            FileManagerDAL fmDAL = new FileManagerDAL();
            //UsersContext uc = new UsersContext();
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
            else if (user.isDeleteUser || !user.isSearchPM)
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