using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store EmpresaCorrelativo information")]
    [Table("empresacorrelativo")]
    public class EmpresaCorrelativo : BaseEntityLog
    {
        [Key]
        [Column("idempresacorrelativo")]
        public Guid Id { get; set; }

        [Column("tipo")]
        public int Tipo { get; set; }

        [Column("contador")]
        public int Contador { get; set; }

        [Column("idempresa")]
        public Guid IdEmpresa { get; set; }
    }
}
