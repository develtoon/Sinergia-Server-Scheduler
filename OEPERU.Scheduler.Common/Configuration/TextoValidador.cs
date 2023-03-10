using System;

namespace OEPERU.Scheduler.Common.Configuration
{
    public class TextoValidador : Attribute
    {
        //Validar Obligatorio texto
        public bool esObligatorio { get; set; }
        public string obligatorioError { get; set; }
        public string accion { get; set; } //para id

        //validar Cantidad exacta
        public bool esLongitudExacta { get; set; }
        public int longitudExacta { get; set; }
        public string longitudExactaError { get; set; }

        //validar Cantidad Min Max
        public bool esLongitudMinMax { get; set; }
        public int longitudMin { get; set; }
        public int longitudMax { get; set; }
        public string longitudMinMaxError { get; set; }        

        #region EthicalHacking

        //Validar texto XSS
        public bool XSS { get; set; }
        public string XSSError { get; set; }

        //Validar texto SQL inyeccion
        public bool SQLInyeccion { get; set; }
        public string SQLInyeccionError { get; set; }

        #endregion

        public TextoValidador()
        {
            esObligatorio = false;
            obligatorioError = string.Empty;
            accion = string.Empty;

            esLongitudExacta = false;
            longitudExacta = 0;
            longitudExactaError = string.Empty;

            esLongitudMinMax = false;
            longitudMin = 0;
            longitudMax = 0;
            longitudMinMaxError = string.Empty;
            

            XSS = false;
            XSSError = string.Empty;

            SQLInyeccion = false;
            SQLInyeccionError = string.Empty;
        }

    }
}
