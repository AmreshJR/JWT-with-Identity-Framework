using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDAL.Constant
{
    public static class ResponseStatus
    {
        public const int sucess = 1;
        public const int fail = 2;
        public const int duplicate = 3;
    }
    public static class ResetResponse
    {
        public const int success = 1;
        public const int failed = 2;
        public const int tokenExpired = 3; 
    }
     
}
