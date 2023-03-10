using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store PedidoInspeccion information")]
    [Table("pedidoinspeccion")]
    public class PedidoInspeccion: BaseEntityLog
    {
        [Key]
        [Column("idpedido")]
        public Guid Id { get; set; }

        [Column("esdireccionvalida")]
        public bool EsDireccionValida { get; set; }

        [Column("comentario")]
        public string Comentario { get; set; }

        [Column("fechainicio")]
        public DateTime FechaInicio { get; set; }
        
        [Column("fechafin")]
        public DateTime? FechaFin { get; set; }

        [Column("horainicio")]
        public TimeSpan HoraInicio { get; set; }

        [Column("horafin")]
        public TimeSpan HoraFin { get; set; }

        [Column("esurgente")]
        public bool EsUrgente { get; set; }

        [Column("observacion")]
        public string Observacion { get; set; }

        [Column("estado")]
        public int Estado { get; set; }
    }
}
