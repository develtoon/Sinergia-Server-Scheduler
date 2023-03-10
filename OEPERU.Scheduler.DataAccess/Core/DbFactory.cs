using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OEPERU.Administracion.DataAccess.Core;
using OEPERU.Scheduler.DataAccess.Core;

namespace OEPERU.Administracion.DataAccess.Core
{
    public class DbFactory : IDbFactory, IDisposable
    {
        private DataContext _dataContext;
        public DbFactory()
        {
            _dataContext = new DataContext();
        }

        public DataContext GetDataContext
        {
            get
            {
                return _dataContext;
            }
        }

        #region Disposing 

        private bool isDisposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                }
            }
            isDisposed = true;
        }

        #endregion
    }
}
