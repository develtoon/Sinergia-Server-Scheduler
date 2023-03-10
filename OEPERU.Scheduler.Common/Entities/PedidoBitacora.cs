using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store PedidoBitacora information")]
    [Table("pedidobitacora")]
    public class PedidoBitacora: BaseEntityLog
    {
        [Key]
        [Column("idpedidobitacora")]
        public Guid Id { get; set; }

        [Column("comentario")]
        public string Comentario { get; set; }

        [Column("idusuario")]
        public Guid IdUsuario { get; set; }

        [Column("idpedido")]
        public Guid IdPedido { get; set; }
    }
}
