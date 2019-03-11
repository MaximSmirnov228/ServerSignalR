using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerNetCore.Models
{
    public class Token
    {
        public int TokenId { get; set; }
        public Guid Guid { get; set; }
        public bool IsActive { get; set; }
        public string CompName { get; set; }
        public string UserId { get; set; }

    }
}
