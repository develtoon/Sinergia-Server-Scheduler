using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OEPERU.Scheduler.DataAccess.Core;

namespace OEPERU.Scheduler.DataAccess.Core
{
    public interface IDbFactory
    {

        DataContext GetDataContext { get; }
    }
}
