using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store Empresa information")]
    [Table("empresa")]
    public class Empresa : BaseEntityLog
    {
        [Key]
        [Column("idempresa")]
        public Guid Id { get; set; }

        [Column("codigo")]
        public string Codigo { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("logo")]
        public string Logo { get; set; }

        [Column("logominiatura")]
        public string LogoMiniatura { get; set; }

        [Column("logooriginal")]
        public string LogoOriginal { get; set; }

        [Column("tipo")]
        public int Tipo { get; set; }

        [Column("tipodocumento")]
        public int TipoDocumento { get; set; }

        [Column("documento")]
        public string Documento { get; set; }

        [Column("razonsocial")]
        public string RazonSocial { get; set; }

        [Column("ruc")]
        public string Ruc { get; set; }

        [Column("correo")]
        public string Correo { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }

        [Column("nomenclatura")]
        public string Nomenclatura { get; set; }

        [Column("estado")]
        public int Estado { get; set; }
    }
}
