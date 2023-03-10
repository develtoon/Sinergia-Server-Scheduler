using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store PedidoControl information")]
    [Table("pedidocontrol")]
    public class PedidoControl : BaseEntityLog
    {
        [Key]
        [Column("idpedidocontrol")]
        public Guid Id { get; set; }

        [Column("fechainicio")]
        public DateTime? FechaInicio { get; set; }

        [Column("fechafin")]
        public DateTime? FechaFin { get; set; }

        [Column("idpedido")]
        public Guid IdPedido { get; set; }

        [Column("estadopedido")]
        public int EstadoPedido { get; set; }

        [Column("estado")]
        public int Estado { get; set; }
    }
}
