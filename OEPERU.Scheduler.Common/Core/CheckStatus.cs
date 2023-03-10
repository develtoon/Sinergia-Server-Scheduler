using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OEPERU.Scheduler.Common.Core
{
    public class CheckStatus
    {
        [NotMapped]
        public string id { set; get; }
        [NotMapped]
        public string codigo { set; get; }
        public string apiEstado { get; set; }
        public string apiMensaje { get; set; }

        public CheckStatus(string apiEstado)
        {
            id = string.Empty;
            codigo = string.Empty;
            this.apiEstado = apiEstado;
            apiMensaje = string.Empty;
        }

        public CheckStatus(string apiEstado, string apiMensaje)
        {
            id = string.Empty;
            codigo = string.Empty;
            this.apiEstado = apiEstado;
            this.apiMensaje = apiMensaje;
        }

        public CheckStatus()
        {
            id = "0";
            apiEstado = Status.Error;
            codigo = string.Empty;
            apiMensaje = string.Empty;
        }

        public CheckStatus(string id, string codigo, string apiEstado, string apiMensaje)
        {
            this.id = id;
            this.codigo =codigo;
            this.apiEstado = apiEstado;
            this.apiMensaje = apiMensaje;
        }

        public CheckStatus(Dictionary<string, object> diccionario)
        {

            id = (string)(diccionario["id"]);
            codigo = (string)diccionario["codigo"];
            apiEstado = (string)diccionario["apiEstado"];
            apiMensaje = (string)diccionario["apiMensaje"];

        }
    }
}
