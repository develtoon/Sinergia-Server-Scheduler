using OEPERU.Scheduler.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OEPERU.Scheduler.Common.Entities
{
    [Description("To store Pedido information")]
    [Table("pedido")]
    public class Pedido : BaseEntityLog
    {
        [Key]
        [Column("idpedido")]
        public Guid Id { get; set; }

        [Column("codigo")]
        public string Codigo { get; set; }

        [Column("registro")]
        public string Registro { get; set; }

        [Column("ubigeo")]
        public string Ubigeo { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }

        [Column("referencia")]
        public string Referencia { get; set; }

        [Column("latitud")]
        public string Latitud { get; set; }

        [Column("longitud")]
        public string Longitud { get; set; }

        [Column("solicitante")]
        public string Solicitante { get; set; }

        [Column("tipodocumento")]
        public int TipoDocumento { get; set; }

        [Column("documento")]
        public string Documento { get; set; }

        [Column("contacto")]
        public string Contacto { get; set; }

        [Column("contactocorreo")]
        public string ContactoCorreo { get; set; }

        [Column("contactotelefono")]
        public string ContactoTelefono { get; set; }

        [Column("documentopendiente")]
        public bool DocumentoPendiente { get; set; }

        [Column("inspeccionconforme")]
        public bool InspeccionConforme { get; set; }

        [Column("reproceso")]
        public bool Reproceso { get; set; }

        [Column("standby")]
        public bool StandBy { get; set; }

        [Column("desestimado")]
        public bool Desestimado { get; set; }

        [Column("idempresa")]
        public Guid IdEmpresa { get; set; }

        [Column("idusuario")]
        public Guid IdUsuario { get; set; }

        [Column("estado")]
        public int Estado { get; set; }

        public Pedido()
        {
            this.Codigo = string.Empty;
            this.Registro = string.Empty;
            this.DocumentoPendiente = false;
            this.InspeccionConforme = false;
            this.Reproceso = false;
            this.StandBy = false;
            this.Desestimado = false;
        }

    }
}
