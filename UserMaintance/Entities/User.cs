using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMaintance.Entities
{
    public class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Fullname { get; set; }
        }
}
