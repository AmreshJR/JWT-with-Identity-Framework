using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Authentication
{
    public class UserByRole
    {
        [Key]
        public string UserName { get; set; }
    }
}
