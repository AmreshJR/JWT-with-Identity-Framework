using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.DataModels
{
    public class GetAllUser
    {   
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string StatusName { get; set; }

        public string AuthId { get; set; }
    }
}
