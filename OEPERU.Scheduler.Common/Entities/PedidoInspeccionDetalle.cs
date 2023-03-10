using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store PedidoInspeccionDetalle information")]
    [Table("pedidoinspecciondetalle")]
    public class PedidoInspeccionDetalle : BaseEntity
    {
        [Key]
        [Column("idpedidoinspecciondetalle")]
        public Guid Id { get; set; }

        [Column("tipo")]
        public int Tipo { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("valor")]
        public int Valor { get; set; }

        [Column("idpedido")]
        public Guid IdPedido { get; set; }

        public PedidoInspeccionDetalle()
        {
            this.Tipo = 0;
            this.Nombre = string.Empty;
            this.Valor = 0;
            
        }

    }
}
