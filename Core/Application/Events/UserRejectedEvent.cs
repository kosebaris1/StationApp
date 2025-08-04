using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events
{
    public class UserRejectedEvent
    {
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
