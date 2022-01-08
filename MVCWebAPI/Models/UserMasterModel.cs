using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebAPI.Models
{
    public class UserMasterModel
    {
        public int uID { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage ="User Name can not be null.")]
        public string uName { get; set; }

        [DisplayName("User Email")]
        [Required(ErrorMessage ="User Email can not be null.")]
        public string uEmail { get; set; }

        public string uPhoto { get; set; }

        //user photo upload

        public HttpPostedFileBase uPhotoFile { get; set; }
    }
}