using System.ComponentModel.DataAnnotations.Schema;

namespace OEPERU.Scheduler.Common.Core
{
    public class BaseInputEntity
    {
        [NotMapped]
        public string idUsuario { get; set; }
        [NotMapped]
        public string nombreUsuario { get; set; }
        [NotMapped]
        public string ipUsuario { get; set; }

        public BaseInputEntity() { 
         
            idUsuario = string.Empty;
            nombreUsuario = string.Empty;
            ipUsuario = string.Empty;
        }
    }
}
