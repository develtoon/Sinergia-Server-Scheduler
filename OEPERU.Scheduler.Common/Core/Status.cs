using System;
using System.Collections.Generic;
using System.Text;

namespace OEPERU.Scheduler.Common.Core
{
    public class Status
    {
        public static string Ok = "ok";
        public static string Error = "error";
        public static string Informacion = "informacion";
        public static string Advertencia = "advertencia";
        public static string Inautorizado = "inautorizado";

        public static string Pending = "Pendiente";
        public static string Active = "Activo";
        public static string Inactive = "Inactivo";
                
        public static string Crear = "Crear";
        public static string Editar = "Editar";
        public static string Eliminar = "Eliminar";

        public static string SinAcceso = "SinAcceso";
        public static string Eliminado = "Eliminado";

        public static string ApiEstado = "apiEstado";
        public static string ApiMensaje = "apiMensaje";
         

        public static int Activo = 1;
        public static int Inactivo = 2;        

        public static string BloqueadoEs = "bloqueado";
        public static string BloqueadoGer = "bloqueado";


        public static string Get = "get";
        public static string Post = "post";
        public static string Put = "put";
        public static string Delete = "delete";
    }

}

