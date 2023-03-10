using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store PersonaEmpresa information")]
    [Table("personaempresa")]
    public class PersonaEmpresa: BaseEntityLog
    {
        [Key]
        [Column("idpersonaempresa")]
        public Guid Id { get; set; }

        [Column("idpersona")]
        public Guid IdPersona { get; set; }

        [Column("idempresa")]
        public Guid IdEmpresa { get; set; }

    }
}
