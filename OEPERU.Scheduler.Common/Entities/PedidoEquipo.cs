using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store PedidoEquipo information")]
    [Table("pedidoequipo")]
    public class PedidoEquipo : BaseEntityLog
    {
        [Key]
        [Column("idpedidoequipo")]
        public Guid Id { get; set; }

        [Column("tipo")]
        public int Tipo { get; set; }

        [Column("idusuario")]
        public Guid? IdUsuario { get; set; }

        [Column("fechainicio")]
        public DateTime FechaInicio { get; set; }

        [Column("fechafin")]
        public DateTime FechaFin { get; set; }

        [Column("fechaconforme")]
        public DateTime FechaConforme { get; set; }

        [Column("idpedido")]
        public Guid IdPedido { get; set; }

        [Column("estado")]
        public int Estado { get; set; }
    }
}
