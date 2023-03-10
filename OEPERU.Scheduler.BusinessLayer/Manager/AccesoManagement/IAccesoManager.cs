using OEPERU.Scheduler.BusinessLayer.Core;
using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OEPERU.Scheduler.BusinessLayer.Manager.AccesoManagement
{
    public interface IAccesoManager : IActionManager
    {
        Task<CheckStatus> Validate(string token, string api, string recurso, string accion);       

    }
}
