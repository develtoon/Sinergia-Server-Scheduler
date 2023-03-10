using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store CatalogoDetalle information")]
    [Table("catalogodetalle")]
    public class CatalogoDetalle : BaseEntityLog
    {
        [Key]
        [Column("idcatalogodetalle")]
        public Guid Id { get; set; }

        [Column("codigo")]
        public int Codigo { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("tipo")]
        public int Tipo { get; set; }

        [Column("valor")]
        public string Valor { get; set; }

        [Column("idcatalogo")]
        public Guid IdCatalogo { get; set; }

        [Column("estado")]
        public int Estado { get; set; }
    }
}
