using System;
using System.Collections.Generic;
using System.Text;

namespace OEPERU.Scheduler.Common.Core
{
    public class DataInd
    {
        public string apiEstado { get; set; }
        public string apiMensaje { get; set; }
        public IDictionary<string, object> data { get; set; }

        public DataInd()
        {
            data = new Dictionary<string, object>();
            apiEstado = Status.Ok;
            apiMensaje = string.Empty;
        }

        public DataInd(string apiEstado, string apiMensaje)
        {
            data = new Dictionary<string, object>();
            this.apiEstado = apiEstado;
            this.apiMensaje = apiMensaje;
        }
    }
}
