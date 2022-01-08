using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.UserDB
{
    public class UserMaster
    {
        [Key]
        public int uID { get; set; }
        public string uName { get; set; }
        public string uEmail { get; set; }
        public string uPhoto { get; set; }
    }
}
