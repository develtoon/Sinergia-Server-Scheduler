using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OEPERU.Scheduler.Common.Core
{
    public class SingleQuery
    {
        [NotMapped]
        public string apiEstado { get; set; }
        [NotMapped]
        public string apiMensaje { get; set; }
      
       

        public SingleQuery()
        {
            this.apiEstado = Status.Ok;            
            this.apiMensaje = String.Empty;
            
        }
    }
}
