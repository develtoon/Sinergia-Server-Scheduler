using Microsoft.Extensions.Configuration;
using System.IO;

namespace OEPERU.Scheduler.Common.Configuration
{
    public class Mensaje
    {
        private static IConfigurationRoot Configuration { get; } =
           ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());

        #region Configuracion
        public static string TimeZone { get; } = Configuration.GetSection("Configuracion")["TimeZone"];

        public static string ApiSeguridad { get; } = Configuration.GetSection("Configuracion")["ApiSeguridad"];
        public static string ApiSeguridadAuth { get; } = Configuration.GetSection("Configuracion")["ApiSeguridadAuth"];
       
        #endregion

        #region Data 
        public static string Tamanio { get; } = Configuration.GetSection("Data")["Tamanio"];
        public static string IdiomaDefecto { get; } = Configuration.GetSection("Data")["IdiomaDefecto"];

        #endregion

        public static string Mostrar(string nombre) {
            return Configuration.GetSection("Mensajes")[nombre];
        }

        #region JWT
        public static string JWTKey { get; } = Configuration.GetSection("Jwt")["Key"];
        public static string JWTIssuer { get; } = Configuration.GetSection("Jwt")["Issuer"];

        #endregion

        #region TiempoEspera
        public static string TiempoEsperaBuscador { get; } = Configuration.GetSection("TiempoEspera")["TiempoEsperaBuscador"];
        #endregion

        
       

    }
}
