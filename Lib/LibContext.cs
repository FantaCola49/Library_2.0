using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class LibContext : IDbContext
    {
        public mainEntities DbContext { get; }

        public LibContext()
        {
                DbContext = new mainEntities();
        }
    }
}
