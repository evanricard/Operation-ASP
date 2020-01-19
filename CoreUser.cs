using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationASP
{
    public class CoreUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash  { get; set; }

    }
}
