using System;

namespace OEPERU.Scheduler.Common.Configuration
{
    public class CatalogoEstadoValidador : Attribute
    {
        public int codigo { get; set; }
        public string mensajeError { get; set; }

        public CatalogoEstadoValidador()
        {
            codigo = 0;
            mensajeError = string.Empty;
        }
    }
}
