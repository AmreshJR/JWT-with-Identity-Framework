using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Dto
{
    public class DtoUpdateRole
    {
        public string UserName { get; set; }
        public string OldRole { get; set; }
        public string NewRole { get; set; }
        public string Email { get; set; }
        public string AuthId { get; set; }
    }
}
