using System;

namespace OEPERU.Scheduler.Common.Configuration
{
    public class EnteroValidador : Attribute
    {
        public bool esObligatorio { get; set; }
        public string obligatorioError { get; set; }
        public string accion { get; set; } //para id

        public EnteroValidador()
        {
            esObligatorio = false;
            obligatorioError = string.Empty;
            accion = string.Empty;
        }
    }
}
