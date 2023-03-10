using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store CatalogoEstado information")]
    [Table("catalogoestado")]
    public class CatalogoEstado : BaseEntity
    {
        [Key]
        [Column("idcatalogoestado")]
        public Guid Id { get; set; }

        [Column("codigo")]
        public int Codigo { get; set; }

        [Column("valor")]
        public int Valor { get; set; }
    }
}
