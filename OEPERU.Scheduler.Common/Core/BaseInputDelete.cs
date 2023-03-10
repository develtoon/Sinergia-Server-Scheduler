using System;
using System.Collections.Generic;
using System.Text;

namespace OEPERU.Scheduler.Common.Core
{
    public class BaseInputDelete
    {
        public string id { get; set; }
        public string idUsuario { get; set; }

        public BaseInputDelete() {
            id = string.Empty;
            idUsuario = string.Empty;
        }

        public BaseInputDelete(string id)
        {
            this.id = id;
            idUsuario = string.Empty;
        }

        public BaseInputDelete(string id,string idUsuario)
        {
            this.id = id;
            this.idUsuario = idUsuario;
        }
    }
}
