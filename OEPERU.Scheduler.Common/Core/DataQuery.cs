using System.Collections.Generic;

namespace OEPERU.Scheduler.Common.Core
{
    public class DataQuery
    {
        public string apiEstado { get; set; }
        public string apiMensaje { get; set; }
        public IList<IDictionary<string, object>> data { get; set; }
        
        public long total { get; set; }

        public DataQuery()
        {
            data = new List<IDictionary<string, object>>();
            apiEstado = Status.Ok;
            total = 0;
            apiMensaje = string.Empty;
        }

        public DataQuery(string apiEstado, string apiMensaje)
        {
            data = new List<IDictionary<string, object>>();
            total = 0;
            this.apiEstado = apiEstado;            
            this.apiMensaje = apiMensaje;
        }
    }
}
