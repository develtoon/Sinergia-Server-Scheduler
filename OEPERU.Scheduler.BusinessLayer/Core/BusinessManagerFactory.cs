using OEPERU.Scheduler.BusinessLayer.Manager.AccesoManagement;
using OEPERU.Scheduler.BusinessLayer.Manager.ValidadorManagement;

namespace OEPERU.Scheduler.BusinessLayer.Core
{
    public class BusinessManagerFactory
    {      
        IValidadorManager _validadorManager;
        IAccesoManager _accesoManager;
        public BusinessManagerFactory(
            IValidadorManager validadorManager=null,
            IAccesoManager accesoManager=null)
        { 
            _validadorManager = validadorManager;
            _accesoManager = accesoManager;

        }

        public IValidadorManager GetValidadorManager()
        {
            return _validadorManager;
        }

        public IAccesoManager GetAccesoManager()
        {
            return _accesoManager;
        }
    }     
}
