using OEPERU.Scheduler.Common.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TimeZoneConverter;

namespace OEPERU.Scheduler.Common.Core
{
    public class BaseEntityLog : BaseEntity
    {
        [Column("usuariocreacion")]
        public string UsuarioCreacion { get; set; }
        [Column("fechacreacion")]
        public DateTime? FechaCreacion { get; set; }
        [Column("usuarioedicion")]
        public string UsuarioEdicion { get; set; }
        [Column("fechaedicion")]
        public DateTime? FechaEdicion { get; set; }

        public BaseEntityLog()
        {
            DateTime fechaActual = new DateTime();
            try
            {
                fechaActual = TimeZoneInfo.ConvertTime(DateTime.Now, TZConvert.GetTimeZoneInfo(Mensaje.TimeZone));
            }
            catch
            {
                fechaActual = DateTime.Now;
            }           
            FechaCreacion = fechaActual;
            FechaEdicion = fechaActual;
        }
    }
}
