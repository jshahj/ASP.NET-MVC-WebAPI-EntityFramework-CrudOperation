using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.UserDB;

namespace DataLayer.UserService
{
    public interface UserMasterDALi
    {
        //add user data
        int UserDataInsert(UserMaster uMaster);

        //get user list
        List<UserMaster> GetUserList();

        //delete user data
        int DeleteUserData(int uID);

        //Get single user data for edit

        /// <summary>
        /// Get single user data for edit
        /// </summary>
        /// <param name="uID">user id</param>
        /// <returns>User data</returns>
        UserMaster GetUserDataById(int uID);

        /// <summary>
        /// update user data
        /// </summary>
        /// <param name="userMaster">user details</param>
        /// <returns>return 1 if success then else return error</returns>
        int UpdateUserDetails(UserMaster userMaster);
    }
}
