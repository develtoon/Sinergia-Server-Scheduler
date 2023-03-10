using OEPERU.Scheduler.Common.Configuration;
using System;

namespace OEPERU.Scheduler.Common.Core
{
    public class DataQueryInput
    {
        [TextoValidador(
            SQLInyeccion =true, XSS =true, esLongitudMinMax =true,longitudMax =200)]
        public string texto { get; set; }
        public string ordenamiento { get; set; }
        public int pagina { get; set; }
        public int tamanio { get; set; }

        public string idUsuario { get; set; }

        public DataQueryInput()
        {
            texto = string.Empty;
            ordenamiento = string.Empty;
            pagina = 1;
            idUsuario = string.Empty;
            tamanio = 0;
        }

        public DataQueryInput(string texto, string ordenamiento, int pagina, string idUsuario)
        {
            this.texto = texto;
            this.ordenamiento = ordenamiento;
            this.pagina = pagina;
            this.idUsuario = idUsuario;
        }
    }
}
