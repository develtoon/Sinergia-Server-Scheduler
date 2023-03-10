using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store Catalogo information")]
    [Table("catalogo")]
    public class Catalogo : BaseEntityLog
    {
        [Key]
        [Column("idcatalogo")]
        public Guid Id { get; set; }

        [Column("codigo")]
        public int Codigo { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("estado")]
        public int Estado { get; set; }
    }
}
