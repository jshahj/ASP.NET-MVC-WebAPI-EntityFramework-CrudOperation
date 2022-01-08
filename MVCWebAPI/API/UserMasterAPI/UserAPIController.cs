using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessLayer.UserDB;
using DataLayer.UserService;
using MVCWebAPI.Models;

namespace MVCWebAPI.API.UserMasterAPI
{
    public class UserAPIController : ApiController
    {
        UserMasterDALi _userDALi;

        public UserAPIController()
        {
            _userDALi = new UserMasterDAL();
        }

        // GET api/<controller>
        public List<UserMaster> Get()
        {
            try
            {
                return _userDALi.GetUserList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        // GET api/<controller>/5
        public UserMaster Get(int id)
        {
            try
            {
                return _userDALi.GetUserDataById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //save user data in database
        // POST api/<controller>
        public int Post()
        {
            try
            {
                //get all user data
                var userDetails = HttpContext.Current.Request;

                //check if file is selected or not
                //if not the return with 2 to show alert

                //if not file is selected then return 2
                if (userDetails.Files.Count <= 0)
                {
                    return 2;
                }
                else
                {
                    UserMaster userMaster = new UserMaster();
                    userMaster.uName = userDetails["userName"].ToString();
                    userMaster.uEmail = userDetails["userEmail"].ToString();

                    foreach (string file in userDetails.Files)
                    {
                        var postedFile = userDetails.Files[file];
                        var imageExtension = Path.GetExtension(postedFile.FileName);
                        if (imageExtension != ".jpg" && imageExtension != ".JPG" && imageExtension != ".PNG" && imageExtension != ".png" && imageExtension != ".jpeg" && imageExtension != ".JPEG")
                        {
                            //return 0 if file is not valid
                            return 0;
                        }
                        else
                        {
                            //create guid to change name for upload image
                            Guid fName = Guid.NewGuid();
                            var fileName = fName.ToString();
                            //Path.GetFileNameWithoutExtension(postedFile.FileName);
                            var exten = Path.GetExtension(postedFile.FileName);
                            fileName = fileName + exten;

                            //store file name
                            userMaster.uPhoto = "~/photos/" + fileName;

                            //upload file in folder
                            fileName = HttpContext.Current.Server.MapPath("~/Photos/" + fileName);
                            postedFile.SaveAs(fileName);

                            //add data in user master 
                            var retVal = _userDALi.UserDataInsert(userMaster);

                            //clear model state
                            ModelState.Clear();
                            return retVal;
                        }
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return 2;
        }


        //update data
        // PUT api/<controller>/5
        public int Put()
        {
            try
            {


                //get all user data
                var userDetails = HttpContext.Current.Request;

                UserMaster userMaster = new UserMaster();
                userMaster.uID = Convert.ToInt32(userDetails["userID"]);
                userMaster.uName = userDetails["userName"].ToString();
                userMaster.uEmail = userDetails["userEmail"].ToString();
                //check if file is selected or not
                //if not the return with 2 to show alert

                //if not file is selected then return 2
                if (userDetails.Files.Count <= 0)
                {

                    userMaster.uPhoto = userDetails["UserImageHidden"].ToString();
                }
                else
                {


                    foreach (string file in userDetails.Files)
                    {
                        var postedFile = userDetails.Files[file];
                        var imageExtension = Path.GetExtension(postedFile.FileName);
                        if (imageExtension != ".jpg" && imageExtension != ".JPG" && imageExtension != ".PNG" && imageExtension != ".png" && imageExtension != ".jpeg" && imageExtension != ".JPEG")
                        {
                            //return 0 if file is not valid
                            return 0;
                        }
                        else
                        {
                            //create guid to change name for upload image
                            Guid fName = Guid.NewGuid();
                            var fileName = fName.ToString();
                            //Path.GetFileNameWithoutExtension(postedFile.FileName);
                            var exten = Path.GetExtension(postedFile.FileName);
                            fileName = fileName + exten;

                            //store file name
                            userMaster.uPhoto = "~/photos/" + fileName;

                            //upload file in folder
                            fileName = HttpContext.Current.Server.MapPath("~/Photos/" + fileName);
                            postedFile.SaveAs(fileName);
                        }


                    }
                }
                //add data in user master 
                var retVal = _userDALi.UpdateUserDetails(userMaster);

                //clear model state
                return retVal;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // DELETE api/<controller>/5
        public int Delete(int id)
        {
            var retVal = _userDALi.DeleteUserData(id);
            if (retVal == 2)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }
}