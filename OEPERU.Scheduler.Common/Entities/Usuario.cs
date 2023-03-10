using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store Usuario information")]
    [Table("usuario")]
    public class Usuario : BaseEntityLog
    {
        [Key]
        [Column("idusuario")]
        public Guid Id { get; set; }

        [Column("codigo")]
        public string Codigo { get; set; }

        [Column("tipo")]
        public int Tipo { get; set; }

        [Column("contenido")]
        public string Contenido { get; set; }

        [Column("idpersona")]
        public Guid IdPersona { get; set; }

        [Column("estado")]
        public int Estado { get; set; }
    }
}
