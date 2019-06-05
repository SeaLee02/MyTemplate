using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.DataAccess.Framework
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=JwtWebApi")
        {

        }

    }
}
