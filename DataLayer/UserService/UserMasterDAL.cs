using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.UserDB;

namespace DataLayer.UserService
{
    public class UserMasterDAL : UserMasterDALi, IDisposable
    {
        EDBContext _db;

        public UserMasterDAL()
        {
            _db = new EDBContext();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);

        }

        /// <summary>
        /// Store  data in database 
        /// </summary>
        /// <param name="uMaster"></param>
        /// <returns>return 1 if added 
        /// else throw error.
        /// </returns>
        public int UserDataInsert(UserMaster uMaster)
        {
            try
            {
                _db.UserMasters.Add(uMaster);
                _db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        /// <summary>
        /// Get user list 
        /// </summary>
        /// <returns>user list</returns>
        public List<UserMaster> GetUserList()
        {
            try
            {
                List<UserMaster> userData = _db.UserMasters.ToList();
                return userData;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="uID">User ID</param>
        /// <returns>Return 1 if deleted.</returns>
        public int DeleteUserData(int uID)
        {
            try
            {
                UserMaster userMaster = _db.UserMasters.Where(m => m.uID == uID).FirstOrDefault();
                if (userMaster == null)
                {
                    return 2;
                }
                else
                {
                    _db.UserMasters.Remove(userMaster);
                    _db.SaveChanges();

                    return 1;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Get single user data for edit
        /// </summary>
        /// <param name="uID">user id</param>
        /// <returns>User data</returns>
        public UserMaster GetUserDataById(int uID)
        {
            try
            {
                return _db.UserMasters.Where(m => m.uID == uID).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateUserDetails(UserMaster userMaster)
        {
            try
            {
                _db.Entry(userMaster).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
